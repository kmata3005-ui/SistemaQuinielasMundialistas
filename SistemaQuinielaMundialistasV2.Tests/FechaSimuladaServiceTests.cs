using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class FechaSimuladaServiceTests
{
    [Fact]
    public async Task ActualizarAsync_CreaConfiguracion()
    {
        var f=new TestDbFactory();
        var s=new FechaSimuladaService(f);
        var fecha=new DateTime(2026,7,10,14,0,0);
        await s.ActualizarAsync(fecha);
        Assert.Equal(fecha, await s.ObtenerAsync());
    }

    [Fact]
    public async Task ActualizarAsync_ModificaConfiguracionExistente()
    {
        var f=new TestDbFactory();
        var s=new FechaSimuladaService(f);
        await s.ActualizarAsync(new DateTime(2026,1,1));
        var nueva=new DateTime(2026,12,31,20,30,0);
        await s.ActualizarAsync(nueva);
        Assert.Equal(nueva, await s.ObtenerAsync());
    }

    [Fact]
    public async Task ObtenerAsync_SinConfiguracion_DevuelveFechaActual()
    {
        var s=new FechaSimuladaService(new TestDbFactory());
        var antes=DateTime.Now.AddSeconds(-2);
        var r=await s.ObtenerAsync();
        Assert.InRange(r,antes,DateTime.Now.AddSeconds(2));
    }
}