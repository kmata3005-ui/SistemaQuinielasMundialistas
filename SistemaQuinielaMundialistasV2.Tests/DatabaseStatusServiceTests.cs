using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class DatabaseStatusServiceTests
{
    [Fact]
    public async Task ObtenerEstadoAsync_CuentaEntidades()
    {
        var f=new TestDbFactory();
        await using(var db=f.CreateDbContext())
        {
            db.Usuarios.Add(new Usuario { Nombre="U",Correo="u@x.com",NombreUsuario="u",Contrasena="x" });
            db.Selecciones.AddRange(new Seleccion {Nombre="CR"},new Seleccion {Nombre="JP"});
            db.Quinielas.Add(new Quiniela {Nombre="Q"});
            await db.SaveChangesAsync();
            var sels=db.Selecciones.ToList();
            db.Partidos.Add(new Partido {SeleccionLocalId=sels[0].Id,SeleccionVisitanteId=sels[1].Id,FechaHora=DateTime.Now});
            await db.SaveChangesAsync();
        }
        var r=await new DatabaseStatusService(f).ObtenerEstadoAsync();
        Assert.Equal(1,r.Usuarios);
        Assert.Equal(1,r.Partidos);
        Assert.Equal(0,r.Pronosticos);
        Assert.Equal(1,r.Quinielas);
        Assert.Equal(2,r.Selecciones);
    }
}