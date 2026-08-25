using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;
using Xunit;

namespace SistemaQuinielaMundialistasV2.Tests;

public class RankingServiceTests
{
    [Fact]
    public async Task Ranking_FiltraOrdenaYNumera()
    {
        var factory = new TestDbFactory();
        await using (var db = factory.CreateDbContext())
        {
            db.Usuarios.AddRange(
                new Usuario { Nombre="B", Correo="b@x.com", NombreUsuario="beta", Contrasena="x", Puntos=20, Activo=true },
                new Usuario { Nombre="A", Correo="a@x.com", NombreUsuario="alfa", Contrasena="x", Puntos=20, Activo=true },
                new Usuario { Nombre="Admin", Correo="ad@x.com", NombreUsuario="admin", Contrasena="x", Puntos=999, Rol="Administrador" },
                new Usuario { Nombre="Off", Correo="o@x.com", NombreUsuario="off", Contrasena="x", Puntos=500, Activo=false });
            await db.SaveChangesAsync();
        }
        var ranking = await new RankingService(factory).ObtenerRankingGlobalAsync();
        Assert.Equal(2, ranking.Count);
        Assert.Equal("alfa", ranking[0].NombreUsuario);
        Assert.Equal(1, ranking[0].Posicion);
        Assert.Equal("beta", ranking[1].NombreUsuario);
    }
}
