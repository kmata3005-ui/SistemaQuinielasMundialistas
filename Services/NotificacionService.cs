using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public class NotificacionService(
    IDbContextFactory<AppDbContext> factory,
    FechaSimuladaService fechaSimulada)
{
    public async Task<List<Partido>> ObtenerPendientes24HorasAsync(int usuarioId)
    {
        DateTime inicio = await fechaSimulada.ObtenerAsync();
        DateTime fin = inicio.AddHours(24);

        await using var db = await factory.CreateDbContextAsync();

        var pronosticados = await db.Pronosticos
            .Where(x => x.UsuarioId == usuarioId)
            .Select(x => x.PartidoId)
            .ToListAsync();

        return await db.Partidos
            .AsNoTracking()
            .Include(x => x.SeleccionLocal)
            .Include(x => x.SeleccionVisitante)
            .Where(x => x.FechaHora > inicio &&
                        x.FechaHora <= fin &&
                        x.Estado != "Finalizado" &&
                        x.Estado != "En curso" &&
                        !pronosticados.Contains(x.Id))
            .OrderBy(x => x.FechaHora)
            .ToListAsync();
    }
}
