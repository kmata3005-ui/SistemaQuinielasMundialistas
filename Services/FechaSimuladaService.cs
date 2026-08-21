using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;

namespace SistemaQuinielaMundialistasV2.Services;

public class FechaSimuladaService(IDbContextFactory<AppDbContext> factory)
{
    public async Task<DateTime> ObtenerAsync()
    {
        await using var db = await factory.CreateDbContextAsync();
        var configuracion = await db.Configuraciones.AsNoTracking().FirstOrDefaultAsync();
        return configuracion?.FechaSimulada ?? DateTime.Now;
    }

    public async Task ActualizarAsync(DateTime nuevaFecha)
    {
        await using var db = await factory.CreateDbContextAsync();
        var configuracion = await db.Configuraciones.FirstOrDefaultAsync();

        if (configuracion is null)
        {
            configuracion = new Models.ConfiguracionSistema { Id = 1, FechaSimulada = nuevaFecha };
            db.Configuraciones.Add(configuracion);
        }
        else
        {
            configuracion.FechaSimulada = nuevaFecha;
        }

        await db.SaveChangesAsync();
    }
}
