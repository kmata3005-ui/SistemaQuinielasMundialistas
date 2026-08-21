using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public class AuthService(IDbContextFactory<AppDbContext> factory, PasswordService passwords)
{
    public async Task<(Usuario? Usuario, string? Error)> LoginAsync(string identificador, string contrasena)
    {
        await using var db = await factory.CreateDbContextAsync();
        string id = identificador.Trim().ToLower();
        var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario.ToLower() == id || x.Correo.ToLower() == id);
        if (usuario is null || !passwords.Verify(contrasena, usuario.Contrasena)) return (null, "Usuario/correo o contraseña incorrectos.");
        if (!usuario.Activo) return (null, "Este usuario está desactivado. Contacte al administrador.");
        return (usuario, null);
    }
}
