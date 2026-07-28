using SistemaQuinielasMundialistas.Models;

namespace SistemaQuinielasMundialistas.Services
{
    public class CruceService
    {
        private readonly PartidoService partidoService = new PartidoService();
        private readonly GrupoService grupoService = new GrupoService();

        public List<CruceEliminatorio> ObtenerCruces()
        {
            ActualizarCruces();

            return partidoService.ObtenerPartidos()
                .Where(EsPartidoEliminatorio)
                .OrderBy(p => OrdenFase(p.Fase))
                .ThenBy(p => p.NumeroCruce)
                .Select(p => new CruceEliminatorio
                {
                    Numero = p.NumeroCruce,
                    Fase = p.Fase,
                    EquipoLocal = p.EquipoLocal,
                    EquipoVisitante = p.EquipoVisitante,
                    FechaHora = p.FechaHora,
                    Estado = p.Estado,
                    Resultado = ObtenerResultado(p),
                    Ganador = ObtenerGanador(p)
                })
                .ToList();
        }

        public void ActualizarCruces()
        {
            GenerarCuartosSiCorresponde();
            GenerarSemifinalesSiCorresponde();
            GenerarFinalSiCorresponde();
        }

        public void DefinirGanadorPorPenales(string fase, int numeroCruce, int penalesLocal, int penalesVisitante)
        {
            Partido? partido = partidoService.ObtenerPartidos()
                .FirstOrDefault(p => p.Fase == fase && p.NumeroCruce == numeroCruce);

            if (partido == null)
                throw new InvalidOperationException("No se encontró el cruce seleccionado.");

            if (partido.Estado != "Finalizado")
                throw new InvalidOperationException("El partido debe estar finalizado antes de registrar penales.");

            if (partido.GolesLocal != partido.GolesVisitante)
                throw new InvalidOperationException("Los penales solamente se registran cuando el marcador termina empatado.");

            if (penalesLocal < 0 || penalesVisitante < 0)
                throw new ArgumentException("Los goles por penales no pueden ser negativos.");

            if (penalesLocal == penalesVisitante)
                throw new ArgumentException("La tanda de penales debe tener un ganador.");

            partido.FueAPenales = true;
            partido.GolesPenalesLocal = penalesLocal;
            partido.GolesPenalesVisitante = penalesVisitante;
            partidoService.GuardarCambios();

            ActualizarCruces();
        }

        private void GenerarCuartosSiCorresponde()
        {
            List<Partido> partidos = partidoService.ObtenerPartidos();
            if (partidos.Any(p => p.Fase == "Cuartos de final"))
                return;

            string[] grupos = { "A", "B", "C", "D" };
            var tablas = grupos.ToDictionary(g => g, g => grupoService.CalcularTabla(g));

            if (tablas.Any(t => t.Value.Count < 2 || t.Value.Any(p => p.PartidosJugados == 0)))
                return;

            DateTime inicio = partidoService.ObtenerFechaSimulada().Date.AddDays(1).AddHours(14);
            CrearPartido("Cuartos de final", 1, tablas["A"][0].Equipo, tablas["B"][1].Equipo, inicio);
            CrearPartido("Cuartos de final", 2, tablas["B"][0].Equipo, tablas["A"][1].Equipo, inicio.AddHours(3));
            CrearPartido("Cuartos de final", 3, tablas["C"][0].Equipo, tablas["D"][1].Equipo, inicio.AddDays(1));
            CrearPartido("Cuartos de final", 4, tablas["D"][0].Equipo, tablas["C"][1].Equipo, inicio.AddDays(1).AddHours(3));
        }

        private void GenerarSemifinalesSiCorresponde()
        {
            List<Partido> partidos = partidoService.ObtenerPartidos();
            if (partidos.Any(p => p.Fase == "Semifinal"))
                return;

            List<Partido> cuartos = partidos
                .Where(p => p.Fase == "Cuartos de final")
                .OrderBy(p => p.NumeroCruce)
                .ToList();

            if (cuartos.Count != 4 || cuartos.Any(p => p.Estado != "Finalizado"))
                return;

            DateTime inicio = cuartos.Max(p => p.FechaHora).Date.AddDays(2).AddHours(15);
            CrearPartido("Semifinal", 1, ObtenerGanadorRequerido(cuartos[0]), ObtenerGanadorRequerido(cuartos[1]), inicio);
            CrearPartido("Semifinal", 2, ObtenerGanadorRequerido(cuartos[2]), ObtenerGanadorRequerido(cuartos[3]), inicio.AddDays(1));
        }

        private void GenerarFinalSiCorresponde()
        {
            List<Partido> partidos = partidoService.ObtenerPartidos();
            if (partidos.Any(p => p.Fase == "Final"))
                return;

            List<Partido> semifinales = partidos
                .Where(p => p.Fase == "Semifinal")
                .OrderBy(p => p.NumeroCruce)
                .ToList();

            if (semifinales.Count != 2 || semifinales.Any(p => p.Estado != "Finalizado"))
                return;

            DateTime fecha = semifinales.Max(p => p.FechaHora).Date.AddDays(3).AddHours(16);
            CrearPartido("Final", 1, ObtenerGanadorRequerido(semifinales[0]), ObtenerGanadorRequerido(semifinales[1]), fecha);
        }

        private void CrearPartido(string fase, int numero, string local, string visitante, DateTime fecha)
        {
            partidoService.AgregarPartido(new Partido
            {
                Fase = fase,
                NumeroCruce = numero,
                EquipoLocal = local,
                EquipoVisitante = visitante,
                FechaHora = fecha,
                GolesLocal = 0,
                GolesVisitante = 0,
                Anotadores = string.Empty
            });
        }

        private static bool EsPartidoEliminatorio(Partido partido) =>
            partido.Fase is "Cuartos de final" or "Semifinal" or "Final";

        private static string ObtenerGanadorRequerido(Partido partido)
        {
            string ganador = ObtenerGanador(partido);
            if (string.IsNullOrWhiteSpace(ganador) || ganador.StartsWith("Empate", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"El cruce {partido.Fase} {partido.NumeroCruce} requiere un ganador.");
            return ganador;
        }

        private static string ObtenerGanador(Partido partido)
        {
            if (partido.Estado != "Finalizado")
                return string.Empty;

            if (partido.GolesLocal > partido.GolesVisitante)
                return partido.EquipoLocal;

            if (partido.GolesVisitante > partido.GolesLocal)
                return partido.EquipoVisitante;

            if (partido.FueAPenales)
            {
                if (partido.GolesPenalesLocal > partido.GolesPenalesVisitante)
                    return partido.EquipoLocal;

                if (partido.GolesPenalesVisitante > partido.GolesPenalesLocal)
                    return partido.EquipoVisitante;
            }

            return "Empate: definir ganador por penales";
        }

        private static string ObtenerResultado(Partido partido)
        {
            if (partido.Estado != "Finalizado")
                return "Pendiente";

            string resultado = $"{partido.GolesLocal} - {partido.GolesVisitante}";
            if (partido.FueAPenales)
                resultado += $" (penales {partido.GolesPenalesLocal} - {partido.GolesPenalesVisitante})";

            return resultado;
        }

        private static int OrdenFase(string fase) => fase switch
        {
            "Cuartos de final" => 1,
            "Semifinal" => 2,
            "Final" => 3,
            _ => 99
        };
    }
}
