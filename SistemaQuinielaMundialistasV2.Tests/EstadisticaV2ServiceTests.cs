using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class EstadisticaV2ServiceTests
{
    [Fact]
    public async Task Administrador_SinDatos_DevuelveValoresVacios()
    {
        var r=await new EstadisticaV2Service(new TestDbFactory()).ObtenerAdministradorAsync();
        Assert.Equal("Sin datos",r.ResultadoMasRepetido);
        Assert.Equal("Sin datos",r.PartidoConMasAciertos);
        Assert.Equal("Sin datos",r.PartidoConMasPronosticos);
        Assert.Equal(0,r.PromedioGoles);
        Assert.Empty(r.TopUsuarios);
    }

    [Fact]
    public async Task Usuario_SinDatos_ProbabilidadCero()
    {
        var r=await new EstadisticaV2Service(new TestDbFactory()).ObtenerUsuarioAsync(1);
        Assert.Equal("Sin datos",r.EquipoMasApostado);
        Assert.Equal("Sin datos suficientes",r.EquipoSorpresa);
        Assert.Equal(0,r.ProbabilidadAcierto);
        Assert.Empty(r.DetalleSorpresas);
    }

    [Fact]
    public async Task Administrador_ConDatos_CalculaReportes()
    {
        var f=new TestDbFactory();
        await using(var db=f.CreateDbContext())
        {
            var cr=new Seleccion{Nombre="Costa Rica"};
            var mx=new Seleccion{Nombre="México"};
            var u1=new Usuario{Nombre="Uno",Correo="1@e.com",NombreUsuario="uno",Contrasena="x"};
            var u2=new Usuario{Nombre="Dos",Correo="2@e.com",NombreUsuario="dos",Contrasena="x"};
            var admin=new Usuario{Nombre="Admin",Correo="a@e.com",NombreUsuario="admin",Contrasena="x",Rol="Administrador"};
            db.AddRange(cr,mx,u1,u2,admin); await db.SaveChangesAsync();

            var p1=new Partido{SeleccionLocalId=cr.Id,SeleccionVisitanteId=mx.Id,Estado="Finalizado",GolesLocal=2,GolesVisitante=1,FechaHora=DateTime.Now};
            var p2=new Partido{SeleccionLocalId=mx.Id,SeleccionVisitanteId=cr.Id,Estado="Finalizado",GolesLocal=1,GolesVisitante=0,FechaHora=DateTime.Now};
            db.AddRange(p1,p2); await db.SaveChangesAsync();

            db.Pronosticos.AddRange(
                new Pronostico{UsuarioId=u1.Id,PartidoId=p1.Id,GolesLocalPronosticados=2,GolesVisitantePronosticados=1,PuntosObtenidos=5},
                new Pronostico{UsuarioId=u2.Id,PartidoId=p1.Id,GolesLocalPronosticados=2,GolesVisitantePronosticados=1,PuntosObtenidos=2},
                new Pronostico{UsuarioId=admin.Id,PartidoId=p1.Id,GolesLocalPronosticados=2,GolesVisitantePronosticados=1,PuntosObtenidos=5},
                new Pronostico
                {
                    UsuarioId = u1.Id,
                    PartidoId = p2.Id,
                    GolesLocalPronosticados = 0,
                    GolesVisitantePronosticados = 1,
                    PuntosObtenidos = 0
                }
           );
            await db.SaveChangesAsync();
        }

        var r=await new EstadisticaV2Service(f).ObtenerAdministradorAsync();
        Assert.Contains("2-1",r.ResultadoMasRepetido);
        Assert.Contains("Costa Rica vs México",r.PartidoConMasAciertos);
        Assert.Contains("pronósticos", r.PartidoConMasPronosticos);
        Assert.Equal(2.0,r.PromedioGoles);
        Assert.Contains("México vs Costa Rica",r.PartidoSinAciertos);
        Assert.DoesNotContain(r.TopUsuarios,x=>x.NombreUsuario=="admin");
        Assert.Equal("uno",r.TopUsuarios.First().NombreUsuario);
    }

    [Fact]
    public async Task Usuario_ConDatos_CalculaAciertosExactosYEquipoMasApostado()
    {
        var f=new TestDbFactory();
        int uid;
        await using(var db=f.CreateDbContext())
        {
            var cr=new Seleccion{Nombre="Costa Rica"};
            var mx=new Seleccion{Nombre="México"};
            var u=new Usuario{Nombre="Uno",Correo="u@e.com",NombreUsuario="uno",Contrasena="x"};
            db.AddRange(cr,mx,u); await db.SaveChangesAsync(); uid=u.Id;
            var p=new Partido{SeleccionLocalId=cr.Id,SeleccionVisitanteId=mx.Id,Estado="Finalizado",GolesLocal=2,GolesVisitante=1,FechaHora=DateTime.Now};
            db.Partidos.Add(p); await db.SaveChangesAsync();
            db.Pronosticos.AddRange(
                new Pronostico{UsuarioId=u.Id,PartidoId=p.Id,GolesLocalPronosticados=2,GolesVisitantePronosticados=1,PuntosObtenidos=5},
                new Pronostico{UsuarioId=999,PartidoId=p.Id,GolesLocalPronosticados=3,GolesVisitantePronosticados=1,PuntosObtenidos=2});
            await db.SaveChangesAsync();
        }

        var r=await new EstadisticaV2Service(f).ObtenerUsuarioAsync(uid);
        Assert.Equal(1,r.PronosticosFinalizados);
        Assert.Equal(1,r.Aciertos);
        Assert.Equal(1,r.MarcadoresExactos);
        Assert.Equal(100,r.ProbabilidadAcierto);
        Assert.Contains("Costa Rica",r.EquipoMasApostado);
    }

    [Fact]
    public async Task Usuario_Sorpresa_DetectaGanadorConMenosDeLaMitadDeApoyo()
    {
        var f=new TestDbFactory();
        int uid;
        await using(var db=f.CreateDbContext())
        {
            var cr=new Seleccion{Nombre="Costa Rica"};
            var mx=new Seleccion{Nombre="México"};
            var u1=new Usuario{Nombre="U1",Correo="1@s.com",NombreUsuario="u1",Contrasena="x"};
            var u2=new Usuario{Nombre="U2",Correo="2@s.com",NombreUsuario="u2",Contrasena="x"};
            var u3=new Usuario{Nombre="U3",Correo="3@s.com",NombreUsuario="u3",Contrasena="x"};
            db.AddRange(cr,mx,u1,u2,u3); await db.SaveChangesAsync(); uid=u1.Id;
            var p=new Partido{SeleccionLocalId=cr.Id,SeleccionVisitanteId=mx.Id,Estado="Finalizado",GolesLocal=0,GolesVisitante=1,FechaHora=DateTime.Now};
            db.Partidos.Add(p); await db.SaveChangesAsync();
            db.Pronosticos.AddRange(
                new Pronostico{UsuarioId=u1.Id,PartidoId=p.Id,GolesLocalPronosticados=1,GolesVisitantePronosticados=0,PuntosObtenidos=0},
                new Pronostico{UsuarioId=u2.Id,PartidoId=p.Id,GolesLocalPronosticados=2,GolesVisitantePronosticados=0,PuntosObtenidos=0},
                new Pronostico{UsuarioId=u3.Id,PartidoId=p.Id,GolesLocalPronosticados=0,GolesVisitantePronosticados=1,PuntosObtenidos=5});
            await db.SaveChangesAsync();
        }

        var r=await new EstadisticaV2Service(f).ObtenerUsuarioAsync(uid);
        Assert.Contains("México",r.EquipoSorpresa);
        Assert.Equal(1,r.CantidadSorpresas);
        var d=Assert.Single(r.DetalleSorpresas);
        Assert.Equal(1,d.ApoyoGanador);
        Assert.Equal(3,d.TotalPronosticos);
    }
}