using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class QuinielaV2ServiceTests
{
    [Fact]
    public async Task ObtenerParaUsuario_FiltraPrivadasYRanking()
    {
        var f=new TestDbFactory();
        int usuarioId;
        await using(var db=f.CreateDbContext())
        {
            var u1=new Usuario {Nombre="Uno",Correo="1@x.com",NombreUsuario="uno",Contrasena="x",Puntos=10};
            var u2=new Usuario {Nombre="Dos",Correo="2@x.com",NombreUsuario="dos",Contrasena="x",Puntos=30};
            var admin=new Usuario {Nombre="Admin",Correo="a@x.com",NombreUsuario="admin",Contrasena="x",Puntos=999,Rol="Administrador"};
            var quinielaPublica = new Quiniela { Nombre = "Publica", EsPrivada = false };
            var privada=new Quiniela {Nombre="Privada",EsPrivada=true};
            var ajena=new Quiniela {Nombre="Ajena",EsPrivada=true};
            db.AddRange(u1, u2, admin, quinielaPublica, privada, ajena); ; await db.SaveChangesAsync();
            usuarioId=u1.Id;
            db.QuinielaUsuarios.AddRange(
                new QuinielaUsuario { QuinielaId = quinielaPublica.Id, UsuarioId = u1.Id },
new QuinielaUsuario { QuinielaId = quinielaPublica.Id, UsuarioId = u2.Id },
new QuinielaUsuario { QuinielaId = quinielaPublica.Id, UsuarioId = admin.Id },
                new QuinielaUsuario {QuinielaId=privada.Id,UsuarioId=u1.Id},
                new QuinielaUsuario {QuinielaId=ajena.Id,UsuarioId=u2.Id});
            await db.SaveChangesAsync();
        }
        var r=await new QuinielaV2Service(f).ObtenerParaUsuarioAsync(usuarioId);
        Assert.Equal(2,r.Count);
        Assert.DoesNotContain(r,x=>x.Nombre=="Ajena");
        var pub=r.Single(x=>x.Nombre=="Publica");
        Assert.Equal(new[]{"dos","uno"},pub.Participantes.Select(x=>x.NombreUsuario));
        Assert.True(r.Single(x=>x.Nombre=="Privada").PerteneceUsuario);
    }

    [Fact]
    public async Task ObtenerParaUsuario_SinDatos_ListaVacia()
    {
        var r=await new QuinielaV2Service(new TestDbFactory()).ObtenerParaUsuarioAsync(1);
        Assert.Empty(r);
    }
}