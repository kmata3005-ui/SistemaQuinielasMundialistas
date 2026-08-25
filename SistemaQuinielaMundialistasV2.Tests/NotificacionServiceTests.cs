using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class NotificacionServiceTests
{
    [Fact]
    public async Task ObtenerPendientes24Horas_FiltraCorrectamente()
    {
        var f=new TestDbFactory();
        var inicio=new DateTime(2026,8,10,10,0,0);
        await new FechaSimuladaService(f).ActualizarAsync(inicio);

        await using(var db=f.CreateDbContext())
        {
            var l=new Seleccion{Nombre="CR"};
            var v=new Seleccion{Nombre="MX"};
            var u=new Usuario{Nombre="U",Correo="u@n.com",NombreUsuario="u",Contrasena="x"};
            db.AddRange(l,v,u); await db.SaveChangesAsync();

            var valido=new Partido{SeleccionLocalId=l.Id,SeleccionVisitanteId=v.Id,FechaHora=inicio.AddHours(5),Estado="Próximo"};
            var fuera=new Partido{SeleccionLocalId=l.Id,SeleccionVisitanteId=v.Id,FechaHora=inicio.AddHours(25),Estado="Próximo"};
            var curso=new Partido{SeleccionLocalId=l.Id,SeleccionVisitanteId=v.Id,FechaHora=inicio.AddHours(6),Estado="En curso"};
            var pronosticado=new Partido{SeleccionLocalId=l.Id,SeleccionVisitanteId=v.Id,FechaHora=inicio.AddHours(7),Estado="Próximo"};
            db.AddRange(valido,fuera,curso,pronosticado); await db.SaveChangesAsync();
            db.Pronosticos.Add(new Pronostico{UsuarioId=u.Id,PartidoId=pronosticado.Id,GolesLocalPronosticados=1,GolesVisitantePronosticados=0});
            await db.SaveChangesAsync();

            var r=await new NotificacionService(f,new FechaSimuladaService(f)).ObtenerPendientes24HorasAsync(u.Id);
            var unico=Assert.Single(r);
            Assert.Equal(valido.Id,unico.Id);
        }
    }

    [Fact]
    public async Task ObtenerPendientes24Horas_IncluyeLimiteExacto24Horas()
    {
        var f=new TestDbFactory();
        var inicio=new DateTime(2026,8,10,10,0,0);
        await new FechaSimuladaService(f).ActualizarAsync(inicio);
        int usuarioId, partidoId;

        await using(var db=f.CreateDbContext())
        {
            var l=new Seleccion{Nombre="A"}; var v=new Seleccion{Nombre="B"};
            var u=new Usuario{Nombre="U",Correo="u2@n.com",NombreUsuario="u2",Contrasena="x"};
            db.AddRange(l,v,u); await db.SaveChangesAsync();
            var p=new Partido{SeleccionLocalId=l.Id,SeleccionVisitanteId=v.Id,FechaHora=inicio.AddHours(24),Estado="Próximo"};
            db.Partidos.Add(p); await db.SaveChangesAsync();
            usuarioId=u.Id; partidoId=p.Id;
        }

        var r=await new NotificacionService(f,new FechaSimuladaService(f)).ObtenerPendientes24HorasAsync(usuarioId);
        Assert.Contains(r,x=>x.Id==partidoId);
    }
}