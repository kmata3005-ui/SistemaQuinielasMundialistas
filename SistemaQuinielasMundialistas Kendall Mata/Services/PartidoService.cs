using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Repositories;

namespace SistemaQuinielasMundialistas.Services
{
    public class PartidoService
    {
        private static readonly TimeSpan DuracionPartido = TimeSpan.FromHours(2);

        private readonly IRepository<Partido> repository = new JsonRepository<Partido>("partidos.json");
        private readonly FechaSimuladaService fechaSimuladaService = new FechaSimuladaService();
        private readonly List<Partido> partidos;

        public PartidoService()
        {
            partidos = repository.GetAll();
            ActualizarEstadosAutomaticos();
        }

        public List<Partido> ObtenerPartidos() => partidos;

        public DateTime ObtenerFechaSimulada() => fechaSimuladaService.ObtenerFecha();

        public void EstablecerFechaSimulada(DateTime fecha)
        {
            fechaSimuladaService.EstablecerFecha(fecha);
            ActualizarEstadosAutomaticos();
        }

        public void AvanzarHoras(int horas)
        {
            fechaSimuladaService.AvanzarHoras(horas);
            ActualizarEstadosAutomaticos();
        }

        public void AvanzarDias(int dias)
        {
            fechaSimuladaService.AvanzarDias(dias);
            ActualizarEstadosAutomaticos();
        }

        public void RestablecerFechaReal()
        {
            fechaSimuladaService.RestablecerAFechaReal();
            ActualizarEstadosAutomaticos();
        }

        public void AgregarPartido(Partido partido)
        {
            Validar(partido);
            partido.Id = partidos.Count == 0 ? 1 : partidos.Max(p => p.Id) + 1;
            partido.Estado = CalcularEstado(partido, ObtenerFechaSimulada());
            partidos.Add(partido);
            Guardar();
        }

        public void EliminarPartido(Partido partido)
        {
            partidos.Remove(partido);
            Guardar();
        }

        public void ActualizarPartido(Partido original, Partido actualizado)
        {
            Validar(actualizado);
            original.EquipoLocal = actualizado.EquipoLocal;
            original.EquipoVisitante = actualizado.EquipoVisitante;
            original.FechaHora = actualizado.FechaHora;
            original.GolesLocal = actualizado.GolesLocal;
            original.GolesVisitante = actualizado.GolesVisitante;
            original.Anotadores = actualizado.Anotadores;
            original.Grupo = actualizado.Grupo;
            original.FueAPenales = actualizado.FueAPenales;
            original.GolesPenalesLocal = actualizado.GolesPenalesLocal;
            original.GolesPenalesVisitante = actualizado.GolesPenalesVisitante;
            original.Estado = CalcularEstado(original, ObtenerFechaSimulada());
            Guardar();
        }

        public bool AceptaPronosticos(Partido partido)
        {
            ActualizarEstado(partido);
            return partido.Estado == "Próximo";
        }

        public bool ActualizarEstadosAutomaticos()
        {
            DateTime fechaSimulada = ObtenerFechaSimulada();
            bool huboCambios = false;

            foreach (Partido partido in partidos)
            {
                string nuevoEstado = CalcularEstado(partido, fechaSimulada);
                if (!string.Equals(partido.Estado, nuevoEstado, StringComparison.Ordinal))
                {
                    partido.Estado = nuevoEstado;
                    huboCambios = true;
                }
            }

            if (huboCambios)
            {
                Guardar();
            }

            return huboCambios;
        }

        public void ActualizarEstado(Partido partido)
        {
            string nuevoEstado = CalcularEstado(partido, ObtenerFechaSimulada());
            if (partido.Estado != nuevoEstado)
            {
                partido.Estado = nuevoEstado;
                Guardar();
            }
        }

        public static string CalcularEstado(Partido partido, DateTime fechaSimulada)
        {
            if (fechaSimulada < partido.FechaHora)
            {
                return "Próximo";
            }

            if (fechaSimulada < partido.FechaHora.Add(DuracionPartido))
            {
                return "En curso";
            }

            return "Finalizado";
        }

        public void GuardarCambios()
        {
            Guardar();
        }

        public static string NormalizarEstado(string estado)
        {
            string valor = (estado ?? string.Empty).Trim().ToLowerInvariant();
            return valor switch
            {
                "finalizado" or "finalizdo" or "terminado" => "Finalizado",
                "en curso" or "encurso" or "jugando" => "En curso",
                _ => "Próximo"
            };
        }

        private static void Validar(Partido partido)
        {
            if (string.IsNullOrWhiteSpace(partido.EquipoLocal) ||
                string.IsNullOrWhiteSpace(partido.EquipoVisitante))
            {
                throw new ArgumentException("Los dos equipos son obligatorios.");
            }

            if (partido.EquipoLocal.Equals(partido.EquipoVisitante, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Un equipo no puede jugar contra sí mismo.");
            }
        }

        private void Guardar() => repository.SaveAll(partidos);
    }
}
