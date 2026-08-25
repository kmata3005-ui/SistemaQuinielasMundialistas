using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class PartidoServiceMasivoTests
{
    private static async Task<(TestDbFactory F, Partido P, Usuario U)> PrepararAsync()
    {
        var f=new TestDbFactory();
        await using var db=f.CreateDbContext();
        var l=new Seleccion{Nombre="Argentina"};
        var v=new Seleccion{Nombre="Brasil"};
        var u=new Usuario{Nombre="Uno",Correo="uno@test.com",NombreUsuario="uno",Contrasena="x"};
        db.AddRange(l,v,u); await db.SaveChangesAsync();
        var p=new Partido{SeleccionLocalId=l.Id,SeleccionVisitanteId=v.Id,FechaHora=new DateTime(2026,8,1),Estado="Próximo"};
        db.Partidos.Add(p); await db.SaveChangesAsync();
        return (f,p,u);
    }

    [Fact]
    public async Task ObtenerTodosAsync_IncluyeSelecciones()
    {
        var (f,_,_)=await PrepararAsync();
        var r=await new PartidoService(f).ObtenerTodosAsync();
        var p=Assert.Single(r);
        Assert.Equal("Argentina",p.SeleccionLocal!.Nombre);
        Assert.Equal("Brasil",p.SeleccionVisitante!.Nombre);
    }

    [Fact]
    public async Task ObtenerAsync_ExistenteEInexistente()
    {
        var (f,p,_)=await PrepararAsync();
        var s=new PartidoService(f);
        Assert.NotNull(await s.ObtenerAsync(p.Id));
        Assert.Null(await s.ObtenerAsync(999));
    }

    [Fact]
    public async Task Actualizar_NoExiste_Rechaza()
    {
        var s=new PartidoService(new TestDbFactory());
        var r=await s.ActualizarPorAdministradorAsync(999,DateTime.Now,"Próximo",0,0,"");
        Assert.False(r.Ok);
    }

    [Fact]
    public async Task Actualizar_GolesNegativos_Rechaza()
    {
        var (f,p,_)=await PrepararAsync();
        var r=await new PartidoService(f).ActualizarPorAdministradorAsync(p.Id,DateTime.Now,"Próximo",-1,0,"");
        Assert.False(r.Ok);
        Assert.Contains("negativos",r.Mensaje);
    }

    [Theory]
    [InlineData(" en curso ","En curso")]
    [InlineData(" FINALIZADO ","Finalizado")]
    [InlineData("cualquier cosa","Próximo")]
    public async Task Actualizar_NormalizaEstado(string entrada,string esperado)
    {
        var (f,p,_)=await PrepararAsync();
        var r=await new PartidoService(f).ActualizarPorAdministradorAsync(p.Id,new DateTime(2026,9,1),entrada,1,0,"  Messi  ");
        Assert.True(r.Ok);
        await using var db=f.CreateDbContext();
        var guardado=(await db.Partidos.FindAsync(p.Id))!;
        Assert.Equal(esperado,guardado.Estado);
        Assert.Equal("Messi",guardado.Anotadores);
    }

    [Fact]
    public async Task Finalizar_RecalculaPronosticoExactoYPuntosUsuario()
    {
        var (f,p,u)=await PrepararAsync();
        await using(var db=f.CreateDbContext())
        {
            db.Pronosticos.Add(new Pronostico{UsuarioId=u.Id,PartidoId=p.Id,GolesLocalPronosticados=2,GolesVisitantePronosticados=1});
            await db.SaveChangesAsync();
        }

        var r=await new PartidoService(f).ActualizarPorAdministradorAsync(
            p.Id,new DateTime(2026,9,1),"Finalizado",2,1,"A");

        Assert.True(r.Ok);
        await using var check=f.CreateDbContext();
        Assert.Equal(5,check.Pronosticos.Single().PuntosObtenidos);
        Assert.Equal(5,(await check.Usuarios.FindAsync(u.Id))!.Puntos);
    }
}