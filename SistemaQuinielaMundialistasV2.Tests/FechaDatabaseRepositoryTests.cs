using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Repositories;
using SistemaQuinielaMundialistasV2.Services;
using Xunit;

namespace SistemaQuinielaMundialistasV2.Tests;

public class FechaDatabaseRepositoryTests
{
    [Fact]
    public async Task FechaSimulada_CreaActualizaYObtiene()
    {
        var factory = new TestDbFactory();
        var service = new FechaSimuladaService(factory);
        var fecha1 = new DateTime(2026, 6, 10, 12, 0, 0);
        var fecha2 = fecha1.AddDays(2);
        await service.ActualizarAsync(fecha1);
        Assert.Equal(fecha1, await service.ObtenerAsync());
        await service.ActualizarAsync(fecha2);
        Assert.Equal(fecha2, await service.ObtenerAsync());
    }

    [Fact]
    public async Task DatabaseStatus_CuentaEntidades()
    {
        var factory = new TestDbFactory();
        await using (var db = factory.CreateDbContext())
        {
            db.Usuarios.Add(new Usuario { Nombre="A", Correo="a@a.com", NombreUsuario="a", Contrasena="x" });
            db.Selecciones.AddRange(new Seleccion { Nombre="CR" }, new Seleccion { Nombre="JP" });
            db.Partidos.Add(new Partido { SeleccionLocalId=1, SeleccionVisitanteId=2 });
            db.Quinielas.Add(new Quiniela { Nombre="Q" });
            await db.SaveChangesAsync();
            db.Pronosticos.Add(new Pronostico { UsuarioId=1, PartidoId=1 });
            await db.SaveChangesAsync();
        }
        var estado = await new DatabaseStatusService(factory).ObtenerEstadoAsync();
        Assert.Equal(1, estado.Usuarios); Assert.Equal(2, estado.Selecciones);
        Assert.Equal(1, estado.Partidos); Assert.Equal(1, estado.Pronosticos); Assert.Equal(1, estado.Quinielas);
    }

    [Fact]
    public async Task EfRepository_CRUD_Completo()
    {
        var factory = new TestDbFactory();
        var repo = new EfRepository<Seleccion>(factory);
        var item = new Seleccion { Nombre="Costa Rica", Codigo="CRC" };
        await repo.AddAsync(item);
        Assert.Single(await repo.GetAllAsync());
        var cargado = await repo.GetByIdAsync(item.Id);
        Assert.NotNull(cargado);
        cargado!.Nombre = "CR";
        await repo.UpdateAsync(cargado);
        Assert.Equal("CR", (await repo.GetByIdAsync(item.Id))!.Nombre);
        await repo.DeleteAsync(cargado);
        Assert.Empty(await repo.GetAllAsync());
        await repo.SaveChangesAsync();
    }
}
