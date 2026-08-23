using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public sealed class QuinielaV2Service(IDbContextFactory<AppDbContext> factory)
{
    public async Task<List<QuinielaResumen>> ObtenerParaUsuarioAsync(int usuarioId)
    {
        await using var db = await factory.CreateDbContextAsync();

        var quinielas = await db.Quinielas
            .AsNoTracking()
            .Include(x => x.Participantes)
                .ThenInclude(x => x.Usuario)
                    .ThenInclude(x => x!.Insignias)
                        .ThenInclude(x => x.Insignia)
            .OrderByDescending(x => x.EsPrivada)
            .ThenBy(x => x.Nombre)
            .ToListAsync();

        return quinielas
            .Where(q => !q.EsPrivada || q.Participantes.Any(p => p.UsuarioId == usuarioId))
            .Select(q =>
            {
                var participantes = q.Participantes
                    .Where(p => p.Usuario is not null && p.Usuario.Activo && p.Usuario.Rol != "Administrador")
                    .OrderByDescending(p => p.Usuario!.Puntos)
                    .ThenBy(p => p.Usuario!.NombreUsuario)
                    .ToList();

                return new QuinielaResumen
                {
                    Id = q.Id,
                    Nombre = q.Nombre,
                    Descripcion = q.Descripcion,
                    EsPrivada = q.EsPrivada,
                    PerteneceUsuario = q.Participantes.Any(p => p.UsuarioId == usuarioId),
                    Participantes = participantes.Select((p, indice) => new QuinielaRankingItem
                    {
                        Posicion = indice + 1,
                        NombreUsuario = p.Usuario!.NombreUsuario,
                        Nombre = p.Usuario.Nombre,
                        Puntos = p.Usuario.Puntos,
                        Insignias = p.Usuario.Insignias
                            .Where(i => i.Insignia is not null)
                            .Select(i => i.Insignia!.Nombre)
                            .OrderBy(x => x)
                            .ToList()
                    }).ToList()
                };
            })
            .ToList();
    }
}
