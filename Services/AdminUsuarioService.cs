using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public class AdminUsuarioService(IDbContextFactory<AppDbContext> factory, PasswordService passwords)
{
    public async Task<List<Usuario>> ListarAsync()
    {
        await using var db = await factory.CreateDbContextAsync();
        return await db.Usuarios.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
    }
    public async Task CambiarEstadoAsync(int id)
    {
        await using var db = await factory.CreateDbContextAsync();
        var u = await db.Usuarios.FindAsync(id) ?? throw new InvalidOperationException("Usuario no encontrado.");
        if (u.Rol == "Administrador") throw new InvalidOperationException("El administrador principal no puede desactivarse.");
        u.Activo = !u.Activo; await db.SaveChangesAsync();
    }
    public async Task ResetearContrasenaAsync(int id, string nueva)
    {
        if (string.IsNullOrWhiteSpace(nueva) || nueva.Length < 8) throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");
        await using var db = await factory.CreateDbContextAsync();
        var u = await db.Usuarios.FindAsync(id) ?? throw new InvalidOperationException("Usuario no encontrado.");
        u.Contrasena = passwords.Hash(nueva); await db.SaveChangesAsync();
    }
}
