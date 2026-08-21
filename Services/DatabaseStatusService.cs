using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;

namespace SistemaQuinielaMundialistasV2.Services;

public sealed class DatabaseStatusService(IDbContextFactory<AppDbContext> contextFactory)
{
    public async Task<DatabaseStatus> ObtenerEstadoAsync()
    {
        await using AppDbContext context = await contextFactory.CreateDbContextAsync();
        return new DatabaseStatus(
            await context.Usuarios.CountAsync(),
            await context.Partidos.CountAsync(),
            await context.Pronosticos.CountAsync(),
            await context.Quinielas.CountAsync(),
            await context.Selecciones.CountAsync());
    }
}

public sealed record DatabaseStatus(
    int Usuarios,
    int Partidos,
    int Pronosticos,
    int Quinielas,
    int Selecciones);
