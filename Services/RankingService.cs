using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public sealed class RankingService(IDbContextFactory<AppDbContext> factory)
{
    public async Task<List<RankingItem>> ObtenerRankingGlobalAsync()
    {
        await using var db = await factory.CreateDbContextAsync();

        var usuarios = await db.Usuarios
            .AsNoTracking()
            .Where(x => x.Activo && x.Rol != "Administrador")
            .Include(x => x.Insignias)
                .ThenInclude(x => x.Insignia)
            .OrderByDescending(x => x.Puntos)
            .ThenBy(x => x.NombreUsuario)
            .ToListAsync();

        return usuarios.Select((usuario, indice) => new RankingItem
        {
            Posicion = indice + 1,
            UsuarioId = usuario.Id,
            NombreUsuario = usuario.NombreUsuario,
            Nombre = usuario.Nombre,
            Pais = usuario.PaisPreferido,
            Puntos = usuario.Puntos,
            Insignias = usuario.Insignias
                .Where(x => x.Insignia is not null)
                .Select(x => x.Insignia!.Nombre)
                .OrderBy(x => x)
                .ToList()
        }).ToList();
    }
}
