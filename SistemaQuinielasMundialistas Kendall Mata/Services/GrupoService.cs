using SistemaQuinielasMundialistas.Models;

namespace SistemaQuinielasMundialistas.Services
{
    public class GrupoService
    {
        private readonly PartidoService partidoService = new PartidoService();

        public List<string> ObtenerGrupos()
        {
            return partidoService.ObtenerPartidos()
                .Where(p => !string.IsNullOrWhiteSpace(p.Grupo))
                .Select(p => p.Grupo.Trim().ToUpperInvariant())
                .Distinct()
                .OrderBy(g => g)
                .ToList();
        }

        public List<PosicionGrupo> CalcularTabla(string grupo)
        {
            if (string.IsNullOrWhiteSpace(grupo))
                return new List<PosicionGrupo>();

            string grupoNormalizado = grupo.Trim().ToUpperInvariant();
            List<Partido> partidosGrupo = partidoService.ObtenerPartidos()
                .Where(p => string.Equals(p.Grupo?.Trim(), grupoNormalizado, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var tabla = partidosGrupo
                .SelectMany(p => new[] { p.EquipoLocal, p.EquipoVisitante })
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToDictionary(
                    equipo => equipo,
                    equipo => new PosicionGrupo { Grupo = grupoNormalizado, Equipo = equipo },
                    StringComparer.OrdinalIgnoreCase);

            foreach (Partido partido in partidosGrupo.Where(p => p.Estado == "Finalizado"))
            {
                if (!tabla.TryGetValue(partido.EquipoLocal, out PosicionGrupo? local) ||
                    !tabla.TryGetValue(partido.EquipoVisitante, out PosicionGrupo? visitante))
                    continue;

                local.PartidosJugados++;
                visitante.PartidosJugados++;
                local.GolesFavor += partido.GolesLocal;
                local.GolesContra += partido.GolesVisitante;
                visitante.GolesFavor += partido.GolesVisitante;
                visitante.GolesContra += partido.GolesLocal;

                if (partido.GolesLocal > partido.GolesVisitante)
                {
                    local.PartidosGanados++;
                    local.Puntos += 3;
                    visitante.PartidosPerdidos++;
                }
                else if (partido.GolesLocal < partido.GolesVisitante)
                {
                    visitante.PartidosGanados++;
                    visitante.Puntos += 3;
                    local.PartidosPerdidos++;
                }
                else
                {
                    local.PartidosEmpatados++;
                    visitante.PartidosEmpatados++;
                    local.Puntos++;
                    visitante.Puntos++;
                }
            }

            List<PosicionGrupo> ordenada = tabla.Values
                .OrderByDescending(p => p.Puntos)
                .ThenByDescending(p => p.DiferenciaGoles)
                .ThenByDescending(p => p.GolesFavor)
                .ThenBy(p => p.Equipo)
                .ToList();

            for (int i = 0; i < ordenada.Count; i++)
                ordenada[i].Posicion = i + 1;

            return ordenada;
        }
    }
}
