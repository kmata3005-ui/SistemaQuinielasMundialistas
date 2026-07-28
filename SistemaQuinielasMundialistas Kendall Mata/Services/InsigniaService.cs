using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Models.Insignias;

namespace SistemaQuinielasMundialistas.Services
{
    /// <summary>
    /// Evalúa las reglas de todas las insignias usando polimorfismo.
    /// Para agregar una nueva insignia solo se crea otra clase derivada
    /// y se incorpora a la colección.
    /// </summary>
    public sealed class InsigniaService
    {
        private readonly List<Insignia> insignias = new()
        {
            new InsigniaLiderGlobal(),
            new InsigniaReyEmpates(),
            new InsigniaRachaDiez(),
            new InsigniaVerguenzaGlobal()
        };

        public List<InsigniaResultado> EvaluarYAsignar(
            List<Usuario> usuarios,
            List<Pronostico> pronosticos)
        {
            foreach (Usuario usuario in usuarios)
                usuario.Insignias.Clear();

            var resultados = new List<InsigniaResultado>();

            foreach (Insignia insignia in insignias)
            {
                Usuario? ganador = insignia.ObtenerGanador(usuarios, pronosticos);

                if (ganador != null)
                    ganador.Insignias.Add(insignia.Nombre);

                resultados.Add(new InsigniaResultado
                {
                    Insignia = insignia.Nombre,
                    Descripcion = insignia.Descripcion,
                    Usuario = ganador?.NombreUsuario ?? "Sin ganador todavía",
                    Puntos = ganador?.Puntos ?? 0
                });
            }

            AgregarInsigniasPrivadas(resultados, usuarios);
            return resultados;
        }

        private static void AgregarInsigniasPrivadas(List<InsigniaResultado> resultados, List<Usuario> usuarios)
        {
            QuinielaService quinielaService = new();
            foreach (Quiniela quiniela in quinielaService.ObtenerQuinielas().Where(q => q.EsPrivada))
            {
                List<Usuario> participantes = usuarios.Where(u => quiniela.ParticipanteIds.Contains(u.Id)).ToList();
                if (participantes.Count == 0) continue;

                Usuario lider = participantes.OrderByDescending(u => u.Puntos).ThenBy(u => u.NombreUsuario).First();
                Usuario ultimo = participantes.OrderBy(u => u.Puntos).ThenBy(u => u.NombreUsuario).First();
                string liderNombre = $"Líder privado - {quiniela.Nombre}";
                string verguenzaNombre = $"Vergüenza privada - {quiniela.Nombre}";

                lider.Insignias.Add(liderNombre);
                ultimo.Insignias.Add(verguenzaNombre);

                resultados.Add(new InsigniaResultado
                {
                    Insignia = liderNombre,
                    Descripcion = "Primer lugar de una quiniela privada.",
                    Usuario = lider.NombreUsuario,
                    Puntos = lider.Puntos
                });
                resultados.Add(new InsigniaResultado
                {
                    Insignia = verguenzaNombre,
                    Descripcion = "Último lugar de una quiniela privada.",
                    Usuario = ultimo.NombreUsuario,
                    Puntos = ultimo.Puntos
                });
            }
        }
    }
}
