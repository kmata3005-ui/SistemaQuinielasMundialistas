using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class PronosticoServiceMasivoTests
{
    private static async Task<(TestDbFactory F, Usuario U, Partido P)> PrepararAsync(
        DateTime? fechaPartido = null, string estado = "Próximo", string rol = "Usuario", bool activo = true)
    {
        var f = new TestDbFactory();
        await using var db = f.CreateDbContext();

        var local = new Seleccion { Nombre = "Costa Rica", Codigo = "CRC" };
        var visita = new Seleccion { Nombre = "Japón", Codigo = "JPN" };
        var u = new Usuario
        {
            Nombre = "Usuario Uno", Correo = "u1@test.com", NombreUsuario = "u1",
            Contrasena = "hash", Rol = rol, Activo = activo
        };
        db.AddRange(local, visita, u);
        await db.SaveChangesAsync();

        var p = new Partido
        {
            SeleccionLocalId = local.Id, SeleccionVisitanteId = visita.Id,
            FechaHora = fechaPartido ?? new DateTime(2026, 7, 10, 18, 0, 0),
            Estado = estado
        };
        db.Partidos.Add(p);
        await db.SaveChangesAsync();
        return (f, u, p);
    }

    private static async Task<PronosticoService> ServicioAsync(TestDbFactory f, DateTime ahora)
    {
        var fecha = new FechaSimuladaService(f);
        await fecha.ActualizarAsync(ahora);
        return new PronosticoService(f, fecha);
    }

    [Fact]
    public async Task RegistrarAsync_Valido_GuardaPronostico()
    {
        var (f,u,p)=await PrepararAsync();
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));

        var r=await s.RegistrarAsync(u.Id,p.Id,2,1);

        Assert.True(r.Ok);
        await using var db=f.CreateDbContext();
        var guardado=Assert.Single(db.Pronosticos);
        Assert.Equal(2,guardado.GolesLocalPronosticados);
        Assert.Equal(1,guardado.GolesVisitantePronosticados);
    }

    [Fact]
    public async Task RegistrarAsync_GolesNegativos_Rechaza()
    {
        var (f,u,p)=await PrepararAsync();
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));
        var r=await s.RegistrarAsync(u.Id,p.Id,-1,0);
        Assert.False(r.Ok);
        Assert.Contains("negativos",r.Mensaje);
    }

    [Fact]
    public async Task RegistrarAsync_UsuarioInexistente_Rechaza()
    {
        var (f,_,p)=await PrepararAsync();
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));
        var r=await s.RegistrarAsync(999,p.Id,1,0);
        Assert.False(r.Ok);
        Assert.Contains("inválido",r.Mensaje);
    }

    [Fact]
    public async Task RegistrarAsync_UsuarioDesactivado_Rechaza()
    {
        var (f,u,p)=await PrepararAsync(activo:false);
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));
        var r=await s.RegistrarAsync(u.Id,p.Id,1,0);
        Assert.False(r.Ok);
    }

    [Fact]
    public async Task RegistrarAsync_Administrador_Rechaza()
    {
        var (f,u,p)=await PrepararAsync(rol:"Administrador");
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));
        var r=await s.RegistrarAsync(u.Id,p.Id,1,0);
        Assert.False(r.Ok);
        Assert.Contains("administrador",r.Mensaje,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task RegistrarAsync_PartidoInexistente_Rechaza()
    {
        var (f,u,_)=await PrepararAsync();
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));
        var r=await s.RegistrarAsync(u.Id,999,1,0);
        Assert.False(r.Ok);
        Assert.Contains("no encontrado",r.Mensaje);
    }

    [Theory]
    [InlineData("Finalizado")]
    [InlineData("En curso")]
    public async Task RegistrarAsync_EstadoNoPermitido_Rechaza(string estado)
    {
        var (f,u,p)=await PrepararAsync(estado:estado);
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));
        var r=await s.RegistrarAsync(u.Id,p.Id,1,0);
        Assert.False(r.Ok);
        Assert.Contains("inició o finalizó",r.Mensaje);
    }

    [Fact]
    public async Task RegistrarAsync_FechaYaPaso_Rechaza()
    {
        var (f,u,p)=await PrepararAsync(new DateTime(2026,7,10,9,0,0));
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));
        var r=await s.RegistrarAsync(u.Id,p.Id,1,0);
        Assert.False(r.Ok);
    }

    [Fact]
    public async Task RegistrarAsync_Duplicado_Rechaza()
    {
        var (f,u,p)=await PrepararAsync();
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));
        Assert.True((await s.RegistrarAsync(u.Id,p.Id,1,0)).Ok);
        var segundo=await s.RegistrarAsync(u.Id,p.Id,2,2);
        Assert.False(segundo.Ok);
        Assert.Contains("Ya existe",segundo.Mensaje);
    }

    [Fact]
    public async Task ObtenerDisponiblesAsync_FiltraPronosticadosYOrdena()
    {
        var (f,u,p1)=await PrepararAsync(new DateTime(2026,7,10,18,0,0));
        await using(var db=f.CreateDbContext())
        {
            var sels=db.Selecciones.ToList();
            var p2=new Partido { SeleccionLocalId=sels[0].Id, SeleccionVisitanteId=sels[1].Id,
                FechaHora=new DateTime(2026,7,10,16,0,0), Estado="Próximo" };
            db.Partidos.Add(p2);
            db.Pronosticos.Add(new Pronostico { UsuarioId=u.Id, PartidoId=p1.Id,
                GolesLocalPronosticados=1,GolesVisitantePronosticados=0,FechaRegistro=DateTime.Now });
            await db.SaveChangesAsync();
        }
        var s=await ServicioAsync(f,new DateTime(2026,7,10,10,0,0));
        var r=await s.ObtenerDisponiblesAsync(u.Id);
        var unico=Assert.Single(r);
        Assert.Equal(new DateTime(2026,7,10,16,0,0),unico.FechaHora);
    }

    [Fact]
    public async Task ObtenerDelUsuarioAsync_FiltraYOrdenaDescendente()
    {
        var (f,u,p)=await PrepararAsync();
        await using(var db=f.CreateDbContext())
        {
            db.Pronosticos.Add(new Pronostico {UsuarioId=u.Id,PartidoId=p.Id,
                GolesLocalPronosticados=1,GolesVisitantePronosticados=0,
                FechaRegistro=new DateTime(2026,1,2)});
            await db.SaveChangesAsync();
        }
        var s=await ServicioAsync(f,new DateTime(2026,1,1));
        var r=await s.ObtenerDelUsuarioAsync(u.Id);
        Assert.Single(r);
        Assert.NotNull(r[0].Partido);
        Assert.NotNull(r[0].Partido!.SeleccionLocal);
    }
}