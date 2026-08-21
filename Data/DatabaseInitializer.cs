using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        IDbContextFactory<AppDbContext> factory = services.GetRequiredService<IDbContextFactory<AppDbContext>>();
        await using AppDbContext context = await factory.CreateDbContextAsync();
        await context.Database.EnsureCreatedAsync();

        PasswordService passwords = services.GetRequiredService<PasswordService>();
        var usuarios = await context.Usuarios.ToListAsync();
        foreach (var usuario in usuarios.Where(x => !x.Contrasena.StartsWith("PBKDF2$", StringComparison.Ordinal)))
            usuario.Contrasena = passwords.Hash(usuario.Contrasena);

        if (!await context.Usuarios.AnyAsync(x => x.Rol == "Administrador"))
        {
            context.Usuarios.Add(new Usuario
            {
                Nombre = "Administrador del Sistema", Correo = "admin@quinielas.cr", NombreUsuario = "admin",
                Contrasena = passwords.Hash("Admin2026!"), PaisPreferido = "Costa Rica", Rol = "Administrador", Activo = true
            });
        }
        await context.SaveChangesAsync();
    }
}
