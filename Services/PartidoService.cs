using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public class PartidoService(IDbContextFactory<AppDbContext> factory)
{
    public async Task<List<Partido>> ObtenerTodosAsync()
    {
        await using var db = await factory.CreateDbContextAsync();
        return await db.Partidos
            .AsNoTracking()
            .Include(x => x.SeleccionLocal)
            .Include(x => x.SeleccionVisitante)
            .OrderBy(x => x.FechaHora)
            .ToListAsync();
    }

    public async Task<Partido?> ObtenerAsync(int id)
    {
        await using var db = await factory.CreateDbContextAsync();
        return await db.Partidos
            .AsNoTracking()
            .Include(x => x.SeleccionLocal)
            .Include(x => x.SeleccionVisitante)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<(bool Ok, string Mensaje)> ActualizarPorAdministradorAsync(
        int partidoId,
        DateTime fechaHora,
        string estado,
        int golesLocal,
        int golesVisitante,
        string anotadores)
    {
        await using var db = await factory.CreateDbContextAsync();
        var partido = await db.Partidos.FirstOrDefaultAsync(x => x.Id == partidoId);
        if (partido is null) return (false, "Partido no encontrado.");

        if (golesLocal < 0 || golesVisitante < 0)
            return (false, "Los goles no pueden ser negativos.");

        partido.FechaHora = fechaHora;
        partido.Estado = NormalizarEstado(estado);
        partido.GolesLocal = golesLocal;
        partido.GolesVisitante = golesVisitante;
        partido.Anotadores = anotadores?.Trim() ?? string.Empty;

        if (partido.Estado.Equals("Finalizado", StringComparison.OrdinalIgnoreCase))
        {
            await RecalcularPuntajesAsync(db, partido);
        }

        await db.SaveChangesAsync();
        return (true, "Partido actualizado correctamente.");
    }

    private static string NormalizarEstado(string? estado)
    {
        var valor = estado?.Trim() ?? string.Empty;
        if (valor.Equals("En curso", StringComparison.OrdinalIgnoreCase)) return "En curso";
        if (valor.Equals("Finalizado", StringComparison.OrdinalIgnoreCase)) return "Finalizado";
        return "Próximo";
    }

    private static async Task RecalcularPuntajesAsync(AppDbContext db, Partido partido)
    {
        var pronosticos = await db.Pronosticos
            .Where(x => x.PartidoId == partido.Id)
            .ToListAsync();

        foreach (var pronostico in pronosticos)
        {
            pronostico.PuntosObtenidos = CalcularPuntos(pronostico, partido);
        }

        // Guardamos primero los nuevos puntos de los pronósticos.
        await db.SaveChangesAsync();

        var usuariosIds = pronosticos
            .Select(x => x.UsuarioId)
            .Distinct()
            .ToList();

        foreach (var usuarioId in usuariosIds)
        {
            var usuario = await db.Usuarios
                .FirstAsync(x => x.Id == usuarioId);

            usuario.Puntos = await db.Pronosticos
                .Where(x => x.UsuarioId == usuarioId)
                .SumAsync(x => x.PuntosObtenidos);
        }
    }

    public static int CalcularPuntos(Pronostico pronostico, Partido partido)
    {
        if (pronostico.GolesLocalPronosticados == partido.GolesLocal &&
            pronostico.GolesVisitantePronosticados == partido.GolesVisitante)
            return 5;

        int signoPronostico = Math.Sign(pronostico.GolesLocalPronosticados - pronostico.GolesVisitantePronosticados);
        int signoReal = Math.Sign(partido.GolesLocal - partido.GolesVisitante);

        return signoPronostico == signoReal ? 2 : 0;
    }
}
