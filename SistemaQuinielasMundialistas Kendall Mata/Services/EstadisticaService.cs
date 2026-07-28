using SistemaQuinielasMundialistas.Models;

namespace SistemaQuinielasMundialistas.Services
{
    public class EstadisticaService
    {
        public string EquipoMasApostado(IEnumerable<Pronostico> datos)
        {
            var equipo = datos.SelectMany(p => p.GolesLocalPronosticados > p.GolesVisitantePronosticados
                    ? new[] { p.EquipoLocal }
                    : p.GolesVisitantePronosticados > p.GolesLocalPronosticados ? new[] { p.EquipoVisitante } : Array.Empty<string>())
                .GroupBy(e => e).OrderByDescending(g => g.Count()).FirstOrDefault();
            return equipo == null ? "Sin datos" : $"{equipo.Key} ({equipo.Count()} apuestas)";
        }

        public string ResultadoMasRepetido(IEnumerable<Pronostico> datos)
        {
            var r = datos.GroupBy(p => $"{p.GolesLocalPronosticados}-{p.GolesVisitantePronosticados}")
                .OrderByDescending(g => g.Count()).FirstOrDefault();
            return r == null ? "Sin datos" : $"{r.Key} ({r.Count()} veces)";
        }

        public string PartidoConMasPronosticos(IEnumerable<Pronostico> datos)
        {
            var p = datos.GroupBy(x => new { x.PartidoId, x.EquipoLocal, x.EquipoVisitante })
                .OrderByDescending(g => g.Count()).FirstOrDefault();
            return p == null ? "Sin datos" : $"{p.Key.EquipoLocal} vs {p.Key.EquipoVisitante} ({p.Count()})";
        }

        public string PartidoConMasAciertos(IEnumerable<Pronostico> datos)
        {
            var p = datos.Where(x => x.PuntosObtenidos > 0)
                .GroupBy(x => new { x.PartidoId, x.EquipoLocal, x.EquipoVisitante })
                .OrderByDescending(g => g.Count()).FirstOrDefault();
            return p == null ? "Sin datos" : $"{p.Key.EquipoLocal} vs {p.Key.EquipoVisitante} ({p.Count()} aciertos)";
        }

        public string UsuarioConMasAciertos(IEnumerable<Pronostico> datos)
        {
            var u = datos.Where(x => x.PuntosObtenidos > 0)
                .GroupBy(x => x.NombreUsuario)
                .OrderByDescending(g => g.Count()).ThenByDescending(g => g.Sum(x => x.PuntosObtenidos)).FirstOrDefault();
            return u == null ? "Sin datos" : $"{u.Key} ({u.Count()} aciertos)";
        }

        public string EquipoSorpresa(IEnumerable<Partido> partidos, IEnumerable<Pronostico> pronosticos)
        {
            var finalizados = partidos.Where(p => PartidoService.NormalizarEstado(p.Estado) == "Finalizado").ToList();
            var sorpresas = new List<string>();
            foreach (var partido in finalizados)
            {
                string ganadorReal = partido.GolesLocal > partido.GolesVisitante ? partido.EquipoLocal :
                    partido.GolesVisitante > partido.GolesLocal ? partido.EquipoVisitante : "Empate";
                if (ganadorReal == "Empate") continue;

                var apuestas = pronosticos.Where(x => x.PartidoId == partido.Id).ToList();
                if (apuestas.Count == 0) continue;
                int apoyoGanador = apuestas.Count(x =>
                    ganadorReal == partido.EquipoLocal ? x.GolesLocalPronosticados > x.GolesVisitantePronosticados :
                    x.GolesVisitantePronosticados > x.GolesLocalPronosticados);
                if (apoyoGanador * 2 < apuestas.Count) sorpresas.Add(ganadorReal);
            }
            var equipo = sorpresas.GroupBy(x => x).OrderByDescending(g => g.Count()).FirstOrDefault();
            return equipo == null ? "Sin datos suficientes" : $"{equipo.Key} ({equipo.Count()} sorpresa(s))";
        }

        public double PromedioGoles(IEnumerable<Partido> partidos) =>
            partidos.Where(p => PartidoService.NormalizarEstado(p.Estado) == "Finalizado")
                .Select(p => p.GolesLocal + p.GolesVisitante).DefaultIfEmpty(0).Average();
    }
}
