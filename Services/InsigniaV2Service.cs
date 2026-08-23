using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;

namespace SistemaQuinielaMundialistasV2.Services;

public sealed class InsigniaV2Service(IDbContextFactory<AppDbContext> factory)
{
    public async Task<List<string>> ObtenerDelUsuarioAsync(int usuarioId)
    {
        await using var db = await factory.CreateDbContextAsync();

        return await db.UsuarioInsignias
            .AsNoTracking()
            .Where(x => x.UsuarioId == usuarioId)
            .Include(x => x.Insignia)
            .OrderBy(x => x.Insignia!.Nombre)
            .Select(x => x.Insignia!.Nombre)
            .ToListAsync();
    }
}
