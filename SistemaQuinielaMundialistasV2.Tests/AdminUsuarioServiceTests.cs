using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class AdminUsuarioServiceTests
{
    [Fact]
    public async Task ListarAsync_OrdenaPorId()
    {
        var f = new TestDbFactory();
        await using (var db = f.CreateDbContext())
        {
            db.Usuarios.AddRange(
                new Usuario { Id=2, Nombre="B", Correo="b@x.com", NombreUsuario="b", Contrasena="12345678" },
                new Usuario { Id=1, Nombre="A", Correo="a@x.com", NombreUsuario="a", Contrasena="12345678" });
            await db.SaveChangesAsync();
        }
        var s = new AdminUsuarioService(f);
        var r = await s.ListarAsync();
        Assert.Equal(new[] {1,2}, r.Select(x=>x.Id));
    }

    [Fact]
    public async Task CambiarEstadoAsync_AlternaEstado()
    {
        var f = new TestDbFactory();
        await using (var db=f.CreateDbContext()) {
            db.Usuarios.Add(new Usuario { Id=1, Nombre="A", Correo="a@x.com", NombreUsuario="a", Contrasena="x", Activo=true });
            await db.SaveChangesAsync();
        }
        var s = new AdminUsuarioService(f);
        await s.CambiarEstadoAsync(1);
        await using var check=f.CreateDbContext();
        Assert.False((await check.Usuarios.FindAsync(1))!.Activo);
    }

    [Fact]
    public async Task CambiarEstadoAsync_Admin_Lanza()
    {
        var f=new TestDbFactory();
        await using(var db=f.CreateDbContext()) {
            db.Usuarios.Add(new Usuario { Id=1, Nombre="Admin", Correo="a@x.com", NombreUsuario="admin", Contrasena="x", Rol="Administrador" });
            await db.SaveChangesAsync();
        }
        var s= new AdminUsuarioService(f);
        await Assert.ThrowsAsync<InvalidOperationException>(()=>s.CambiarEstadoAsync(1));
    }

    [Fact]
    public async Task CambiarEstadoAsync_Inexistente_Lanza()
    {
        var s= new AdminUsuarioService(new TestDbFactory());
        await Assert.ThrowsAsync<InvalidOperationException>(()=>s.CambiarEstadoAsync(99));
    }

    [Fact]
    public async Task ResetearContrasena_ValidaYActualiza()
    {
        var f=new TestDbFactory();
        await using(var db=f.CreateDbContext()) {
            db.Usuarios.Add(new Usuario { Id=1, Nombre="A", Correo="a@x.com", NombreUsuario="a", Contrasena="vieja" });
            await db.SaveChangesAsync();
        }
        var s = new AdminUsuarioService(f);
        await Assert.ThrowsAsync<ArgumentException>(()=>s.ResetearContrasenaAsync(1,"123"));
        await s.ResetearContrasenaAsync(1,"Nueva1234");
        await using var check=f.CreateDbContext();
        Assert.True(PasswordService.Verify(
     "Nueva1234",
     (await check.Usuarios.FindAsync(1))!.Contrasena));
    }

    [Fact]
    public async Task ResetearContrasena_Inexistente_Lanza()
    {
        var s=new AdminUsuarioService(new TestDbFactory());
        await Assert.ThrowsAsync<InvalidOperationException>(()=>s.ResetearContrasenaAsync(99,"Nueva1234"));
    }
}