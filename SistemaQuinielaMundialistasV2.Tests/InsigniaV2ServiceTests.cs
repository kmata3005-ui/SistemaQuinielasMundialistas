using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class InsigniaV2ServiceTests
{
    [Fact]
    public async Task ObtenerDelUsuarioAsync_FiltraYOrdena()
    {
        var f=new TestDbFactory();
        await using(var db=f.CreateDbContext())
        {
            var u1=new Usuario {Nombre="U1",Correo="1@x.com",NombreUsuario="u1",Contrasena="x"};
            var u2=new Usuario {Nombre="U2",Correo="2@x.com",NombreUsuario="u2",Contrasena="x"};
            var z=new InsigniaEntidad {Nombre="Zeta"};
            var a=new InsigniaEntidad {Nombre="Alfa"};
            db.AddRange(u1,u2,z,a); await db.SaveChangesAsync();
            db.UsuarioInsignias.AddRange(
                new UsuarioInsignia {UsuarioId=u1.Id,InsigniaId=z.Id},
                new UsuarioInsignia {UsuarioId=u1.Id,InsigniaId=a.Id},
                new UsuarioInsignia {UsuarioId=u2.Id,InsigniaId=z.Id});
            await db.SaveChangesAsync();
        }
        await using var check=f.CreateDbContext();
        var id=check.Usuarios.Single(x=>x.NombreUsuario=="u1").Id;
        var r=await new InsigniaV2Service(f).ObtenerDelUsuarioAsync(id);
        Assert.Equal(new[]{"Alfa","Zeta"},r);
    }

    [Fact]
    public async Task ObtenerDelUsuarioAsync_SinInsignias_ListaVacia()
    {
        var r=await new InsigniaV2Service(new TestDbFactory()).ObtenerDelUsuarioAsync(999);
        Assert.Empty(r);
    }
}