using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public class PronosticoService(
    IDbContextFactory<AppDbContext> factory,
    FechaSimuladaService fechaSimulada)
{
    public async Task<List<Partido>> ObtenerDisponiblesAsync(int usuarioId)
    {
        DateTime ahora = await fechaSimulada.ObtenerAsync();

        await using var db = await factory.CreateDbContextAsync();

        var partidosPronosticados = await db.Pronosticos
            .Where(x => x.UsuarioId == usuarioId)
            .Select(x => x.PartidoId)
            .ToListAsync();

        return await db.Partidos
            .AsNoTracking()
            .Include(x => x.SeleccionLocal)
            .Include(x => x.SeleccionVisitante)
            .Where(x => x.FechaHora > ahora &&
                        x.Estado != "Finalizado" &&
                        !partidosPronosticados.Contains(x.Id))
            .OrderBy(x => x.FechaHora)
            .ToListAsync();
    }

    public async Task<List<Pronostico>> ObtenerDelUsuarioAsync(int usuarioId)
    {
        await using var db = await factory.CreateDbContextAsync();

        return await db.Pronosticos
            .AsNoTracking()
            .Include(x => x.Partido)!.ThenInclude(x => x!.SeleccionLocal)
            .Include(x => x.Partido)!.ThenInclude(x => x!.SeleccionVisitante)
            .Where(x => x.UsuarioId == usuarioId)
            .OrderByDescending(x => x.FechaRegistro)
            .ToListAsync();
    }

    public async Task<(bool Ok, string Mensaje)> RegistrarAsync(
        int usuarioId,
        int partidoId,
        int golesLocal,
        int golesVisitante)
    {
        if (golesLocal < 0 || golesVisitante < 0)
            return (false, "Los goles pronosticados no pueden ser negativos.");

        DateTime ahora = await fechaSimulada.ObtenerAsync();
        await using var db = await factory.CreateDbContextAsync();

        var usuario = await db.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == usuarioId);
        if (usuario is null || !usuario.Activo)
            return (false, "Usuario inválido o desactivado.");

        if (usuario.Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
            return (false, "El administrador no puede realizar pronósticos.");

        var partido = await db.Partidos.FirstOrDefaultAsync(x => x.Id == partidoId);
        if (partido is null) return (false, "Partido no encontrado.");

        if (partido.Estado.Equals("Finalizado", StringComparison.OrdinalIgnoreCase) ||
            partido.Estado.Equals("En curso", StringComparison.OrdinalIgnoreCase) ||
            partido.FechaHora <= ahora)
            return (false, "El partido ya inició o finalizó. No se aceptan pronósticos.");

        bool existe = await db.Pronosticos.AnyAsync(x => x.UsuarioId == usuarioId && x.PartidoId == partidoId);
        if (existe)
            return (false, "Ya existe un pronóstico para este partido.");

        db.Pronosticos.Add(new Pronostico
        {
            UsuarioId = usuarioId,
            PartidoId = partidoId,
            GolesLocalPronosticados = golesLocal,
            GolesVisitantePronosticados = golesVisitante,
            FechaRegistro = ahora,
            PuntosObtenidos = 0
        });

        await db.SaveChangesAsync();
        return (true, "Pronóstico registrado correctamente.");
    }
}
