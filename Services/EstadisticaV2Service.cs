using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public sealed class EstadisticaV2Service(IDbContextFactory<AppDbContext> factory)
{
    public async Task<EstadisticasAdministrador> ObtenerAdministradorAsync()
    {
        await using var db = await factory.CreateDbContextAsync();

        var pronosticos = await db.Pronosticos
            .AsNoTracking()
            .Include(x => x.Usuario)
            .Include(x => x.Partido)!.ThenInclude(x => x!.SeleccionLocal)
            .Include(x => x.Partido)!.ThenInclude(x => x!.SeleccionVisitante)
            .ToListAsync();

        var partidos = await db.Partidos
            .AsNoTracking()
            .Include(x => x.SeleccionLocal)
            .Include(x => x.SeleccionVisitante)
            .ToListAsync();

        return new EstadisticasAdministrador
        {
            ResultadoMasRepetido = ResultadoMasRepetido(pronosticos),
            PartidoConMasAciertos = PartidoConMasAciertos(pronosticos),
            TopUsuarios = TopUsuariosConMasAciertos(pronosticos, 5),
            PartidoConMasPronosticos = PartidoConMasPronosticos(pronosticos),
            PromedioGoles = PromedioGoles(partidos),
            PartidoSinAciertos = PartidoSinAciertos(partidos, pronosticos)
        };
    }

    public async Task<EstadisticasUsuario> ObtenerUsuarioAsync(int usuarioId)
    {
        await using var db = await factory.CreateDbContextAsync();

        var pronosticosTodos = await db.Pronosticos
            .AsNoTracking()
            .Include(x => x.Partido)!.ThenInclude(x => x!.SeleccionLocal)
            .Include(x => x.Partido)!.ThenInclude(x => x!.SeleccionVisitante)
            .ToListAsync();

        var partidos = await db.Partidos
            .AsNoTracking()
            .Include(x => x.SeleccionLocal)
            .Include(x => x.SeleccionVisitante)
            .ToListAsync();

        var propios = pronosticosTodos
            .Where(x => x.UsuarioId == usuarioId &&
                        x.Partido is not null &&
                        EsFinalizado(x.Partido.Estado))
            .ToList();

        var sorpresas = CalcularSorpresas(partidos, pronosticosTodos);
        var agrupacionSorpresa = sorpresas
            .GroupBy(x => x.GanadorReal)
            .OrderByDescending(x => x.Count())
            .ThenBy(x => x.Key)
            .FirstOrDefault();

        int aciertos = propios.Count(x => x.PuntosObtenidos > 0);
        int exactos = propios.Count(x => x.PuntosObtenidos == 5);

        return new EstadisticasUsuario
        {
            EquipoMasApostado = EquipoMasApostado(pronosticosTodos),
            EquipoSorpresa = agrupacionSorpresa is null
                ? "Sin datos suficientes"
                : $"{agrupacionSorpresa.Key} ({agrupacionSorpresa.Count()} sorpresa(s))",
            CantidadSorpresas = agrupacionSorpresa?.Count() ?? 0,
            DetalleSorpresas = agrupacionSorpresa?.ToList() ?? new List<PartidoSorpresaDetalle>(),
            PronosticosFinalizados = propios.Count,
            Aciertos = aciertos,
            MarcadoresExactos = exactos,
            ProbabilidadAcierto = propios.Count == 0 ? 0 : aciertos * 100.0 / propios.Count
        };
    }

    private static string ResultadoMasRepetido(IEnumerable<Pronostico> datos)
    {
        var resultado = datos
            .GroupBy(x => $"{x.GolesLocalPronosticados}-{x.GolesVisitantePronosticados}")
            .OrderByDescending(x => x.Count())
            .ThenBy(x => x.Key)
            .FirstOrDefault();

        return resultado is null ? "Sin datos" : $"{resultado.Key} ({resultado.Count()} veces)";
    }

    private static string PartidoConMasPronosticos(IEnumerable<Pronostico> datos)
    {
        var partido = datos
            .Where(x => x.Partido is not null)
            .GroupBy(x => x.Partido!)
            .OrderByDescending(x => x.Count())
            .FirstOrDefault();

        return partido is null
            ? "Sin datos"
            : $"{partido.Key.NombrePartido} ({partido.Count()} pronósticos)";
    }

    private static string PartidoConMasAciertos(IEnumerable<Pronostico> datos)
    {
        var partido = datos
            .Where(x => x.PuntosObtenidos > 0 && x.Partido is not null)
            .GroupBy(x => x.Partido!)
            .OrderByDescending(x => x.Count())
            .FirstOrDefault();

        return partido is null
            ? "Sin datos"
            : $"{partido.Key.NombrePartido} ({partido.Count()} aciertos)";
    }

    private static List<TopUsuarioAciertos> TopUsuariosConMasAciertos(
        IEnumerable<Pronostico> datos,
        int cantidad)
    {
        return datos
            .Where(x => x.PuntosObtenidos > 0 && x.Usuario is not null && x.Usuario.Rol != "Administrador")
            .GroupBy(x => x.Usuario!)
            .Select(g => new
            {
                Usuario = g.Key,
                Aciertos = g.Count(),
                Puntos = g.Sum(x => x.PuntosObtenidos)
            })
            .OrderByDescending(x => x.Aciertos)
            .ThenByDescending(x => x.Puntos)
            .ThenBy(x => x.Usuario.NombreUsuario)
            .Take(cantidad)
            .Select((x, indice) => new TopUsuarioAciertos
            {
                Posicion = indice + 1,
                NombreUsuario = x.Usuario.NombreUsuario,
                Aciertos = x.Aciertos,
                Puntos = x.Puntos
            })
            .ToList();
    }

    private static double PromedioGoles(IEnumerable<Partido> partidos)
    {
        var finalizados = partidos.Where(x => EsFinalizado(x.Estado)).ToList();
        return finalizados.Count == 0
            ? 0
            : finalizados.Average(x => x.GolesLocal + x.GolesVisitante);
    }

    private static string PartidoSinAciertos(
        IEnumerable<Partido> partidos,
        IEnumerable<Pronostico> pronosticos)
    {
        var finalizados = partidos.Where(x => EsFinalizado(x.Estado)).ToList();

        foreach (var partido in finalizados)
        {
            var apuestas = pronosticos.Where(x => x.PartidoId == partido.Id).ToList();
            if (apuestas.Count > 0 && apuestas.All(x => x.PuntosObtenidos == 0))
            {
                return $"{partido.NombrePartido} ({partido.GolesLocal}-{partido.GolesVisitante})";
            }
        }

        return "No se encontró un partido finalizado sin aciertos.";
    }

    // Misma lógica de la Iteración 1: se considera apuesta por el equipo
    // cuando el marcador pronosticado lo da como ganador.
    private static string EquipoMasApostado(IEnumerable<Pronostico> datos)
    {
        var equipos = datos
            .Where(x => x.Partido is not null)
            .SelectMany(x =>
            {
                if (x.GolesLocalPronosticados > x.GolesVisitantePronosticados)
                    return new[] { x.Partido!.SeleccionLocal?.Nombre ?? "Local" };

                if (x.GolesVisitantePronosticados > x.GolesLocalPronosticados)
                    return new[] { x.Partido!.SeleccionVisitante?.Nombre ?? "Visitante" };

                return Array.Empty<string>();
            });

        var equipo = equipos
            .GroupBy(x => x)
            .OrderByDescending(x => x.Count())
            .ThenBy(x => x.Key)
            .FirstOrDefault();

        return equipo is null ? "Sin datos" : $"{equipo.Key} ({equipo.Count()} apuestas)";
    }

    // Adaptación directa de EquipoSorpresa de la V1:
    // un ganador es sorpresa cuando menos de la mitad de los pronósticos lo daban como ganador.
    private static List<PartidoSorpresaDetalle> CalcularSorpresas(
        IEnumerable<Partido> partidos,
        IEnumerable<Pronostico> pronosticos)
    {
        var resultado = new List<PartidoSorpresaDetalle>();

        foreach (var partido in partidos.Where(x => EsFinalizado(x.Estado)))
        {
            string? ganador = null;
            int seleccionGanadoraId = 0;

            if (partido.GolesLocal > partido.GolesVisitante)
            {
                ganador = partido.SeleccionLocal?.Nombre;
                seleccionGanadoraId = partido.SeleccionLocalId;
            }
            else if (partido.GolesVisitante > partido.GolesLocal)
            {
                ganador = partido.SeleccionVisitante?.Nombre;
                seleccionGanadoraId = partido.SeleccionVisitanteId;
            }

            if (string.IsNullOrWhiteSpace(ganador))
                continue;

            var apuestas = pronosticos.Where(x => x.PartidoId == partido.Id).ToList();
            if (apuestas.Count == 0)
                continue;

            int apoyoGanador = apuestas.Count(x =>
                seleccionGanadoraId == partido.SeleccionLocalId
                    ? x.GolesLocalPronosticados > x.GolesVisitantePronosticados
                    : x.GolesVisitantePronosticados > x.GolesLocalPronosticados);

            if (apoyoGanador * 2 >= apuestas.Count)
                continue;

            resultado.Add(new PartidoSorpresaDetalle
            {
                Partido = partido.NombrePartido,
                GanadorReal = ganador,
                Resultado = $"{partido.GolesLocal}-{partido.GolesVisitante}",
                TotalPronosticos = apuestas.Count,
                ApoyoGanador = apoyoGanador,
                PorcentajeApoyoGanador = apoyoGanador * 100.0 / apuestas.Count
            });
        }

        return resultado;
    }

    private static bool EsFinalizado(string? estado) =>
        string.Equals(estado?.Trim(), "Finalizado", StringComparison.OrdinalIgnoreCase);
}
