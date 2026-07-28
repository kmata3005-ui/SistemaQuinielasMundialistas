using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Repositories;

namespace SistemaQuinielasMundialistas.Services
{
    public class TimelineService
    {
        private readonly IRepository<TimelineEvento> repository =
            new JsonRepository<TimelineEvento>("timeline.json");

        private readonly List<TimelineEvento> eventos;

        public TimelineService()
        {
            eventos = repository.GetAll();
            GenerarEventosInicialesSiEsNecesario();
        }

        public List<TimelineEvento> ObtenerEventos()
        {
            GenerarEventosAutomaticos();
            return eventos
                .OrderByDescending(e => e.Fecha)
                .ToList();
        }

        public List<TimelineEvento> ObtenerEventosPorQuiniela(int? quinielaId)
        {
            GenerarEventosAutomaticos();
            IEnumerable<TimelineEvento> consulta = eventos;

            if (quinielaId.HasValue)
                consulta = consulta.Where(e => e.QuinielaId == quinielaId.Value);

            return consulta
                .OrderByDescending(e => e.Fecha)
                .ToList();
        }

        public void AgregarEvento(int? quinielaId, string quiniela, string tipo, string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
                throw new ArgumentException("El mensaje del evento es obligatorio.");

            TimelineEvento evento = new()
            {
                Id = eventos.Count == 0 ? 1 : eventos.Max(e => e.Id) + 1,
                QuinielaId = quinielaId,
                Quiniela = string.IsNullOrWhiteSpace(quiniela) ? "General" : quiniela,
                Fecha = DateTime.Now,
                Tipo = string.IsNullOrWhiteSpace(tipo) ? "Información" : tipo,
                Mensaje = mensaje.Trim()
            };

            eventos.Add(evento);
            Guardar();
        }


        private void GenerarEventosAutomaticos()
        {
            UsuarioService usuarioService = new();
            PronosticoService pronosticoService = new();
            QuinielaService quinielaService = new();
            List<Usuario> usuarios = usuarioService.ObtenerUsuarios();
            List<Pronostico> pronosticos = pronosticoService.ObtenerPronosticos();
            List<Quiniela> quinielas = quinielaService.ObtenerQuinielas();
            bool cambio = false;

            Usuario? lider = usuarios.OrderByDescending(u => u.Puntos).ThenBy(u => u.NombreUsuario).FirstOrDefault();
            if (lider != null)
                cambio |= AgregarSiNoExiste(null, "Ranking global", "Nuevo líder",
                    $"{lider.NombreUsuario} es líder global con {lider.Puntos} puntos.");

            foreach (Pronostico p in pronosticos.Where(x => x.PuntosObtenidos == 5).OrderByDescending(x => x.FechaRegistro).Take(12))
            {
                cambio |= AgregarSiNoExiste(null, "General", "Marcador exacto",
                    $"{p.NombreUsuario} acertó el marcador exacto de {p.EquipoLocal} vs {p.EquipoVisitante}.", p.FechaRegistro);
            }

            foreach (Quiniela q in quinielas.Where(x => x.EsPrivada))
            {
                Usuario? liderPrivado = usuarios.Where(u => q.ParticipanteIds.Contains(u.Id))
                    .OrderByDescending(u => u.Puntos).ThenBy(u => u.NombreUsuario).FirstOrDefault();
                if (liderPrivado != null)
                    cambio |= AgregarSiNoExiste(q.Id, q.Nombre, "Nuevo líder privado",
                        $"{liderPrivado.NombreUsuario} lidera {q.Nombre} con {liderPrivado.Puntos} puntos.");
            }

            if (cambio) Guardar();
        }

        private bool AgregarSiNoExiste(int? quinielaId, string quiniela, string tipo, string mensaje, DateTime? fecha = null)
        {
            if (eventos.Any(e => e.QuinielaId == quinielaId && e.Tipo == tipo && e.Mensaje == mensaje)) return false;
            eventos.Add(new TimelineEvento
            {
                Id = eventos.Count == 0 ? 1 : eventos.Max(e => e.Id) + 1,
                QuinielaId = quinielaId,
                Quiniela = quiniela,
                Fecha = fecha ?? DateTime.Now,
                Tipo = tipo,
                Mensaje = mensaje
            });
            return true;
        }

        private void GenerarEventosInicialesSiEsNecesario()
        {
            if (eventos.Count > 0)
                return;

            QuinielaService quinielaService = new();
            UsuarioService usuarioService = new();

            List<Quiniela> quinielas = quinielaService.ObtenerQuinielas();
            List<Usuario> usuarios = usuarioService.ObtenerUsuarios();
            Usuario? lider = usuarios.OrderByDescending(u => u.Puntos).FirstOrDefault();
            Usuario? ultimo = usuarios.OrderBy(u => u.Puntos).FirstOrDefault();

            DateTime fechaBase = DateTime.Now.AddMinutes(-30);

            foreach (Quiniela quiniela in quinielas.Take(5))
            {
                eventos.Add(new TimelineEvento
                {
                    Id = eventos.Count + 1,
                    QuinielaId = quiniela.Id,
                    Quiniela = quiniela.Nombre,
                    Fecha = fechaBase.AddMinutes(eventos.Count * 3),
                    Tipo = "Quiniela",
                    Mensaje = $"La quiniela {quiniela.Nombre} está disponible para sus participantes."
                });
            }

            if (lider != null)
            {
                eventos.Add(new TimelineEvento
                {
                    Id = eventos.Count + 1,
                    QuinielaId = null,
                    Quiniela = "Ranking global",
                    Fecha = DateTime.Now.AddMinutes(-8),
                    Tipo = "Nuevo líder",
                    Mensaje = $"{lider.NombreUsuario} ocupa el primer lugar con {lider.Puntos} puntos."
                });
            }

            if (ultimo != null)
            {
                eventos.Add(new TimelineEvento
                {
                    Id = eventos.Count + 1,
                    QuinielaId = null,
                    Quiniela = "Ranking global",
                    Fecha = DateTime.Now.AddMinutes(-4),
                    Tipo = "Mensaje de la vergüenza",
                    Mensaje = $"{ultimo.NombreUsuario} se encuentra en el último lugar del ranking."
                });
            }

            Guardar();
        }

        private void Guardar()
        {
            repository.SaveAll(eventos);
        }
    }
}
