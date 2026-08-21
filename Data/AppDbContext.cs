using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Seleccion> Selecciones => Set<Seleccion>();
    public DbSet<Partido> Partidos => Set<Partido>();
    public DbSet<Pronostico> Pronosticos => Set<Pronostico>();
    public DbSet<Quiniela> Quinielas => Set<Quiniela>();
    public DbSet<QuinielaUsuario> QuinielaUsuarios => Set<QuinielaUsuario>();
    public DbSet<TimelineEvento> TimelineEventos => Set<TimelineEvento>();
    public DbSet<InsigniaEntidad> Insignias => Set<InsigniaEntidad>();
    public DbSet<UsuarioInsignia> UsuarioInsignias => Set<UsuarioInsignia>();
    public DbSet<ConfiguracionSistema> Configuraciones => Set<ConfiguracionSistema>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>().HasIndex(x => x.Correo).IsUnique();
        modelBuilder.Entity<Usuario>().HasIndex(x => x.NombreUsuario).IsUnique();
        modelBuilder.Entity<Seleccion>().HasIndex(x => x.Nombre).IsUnique();

        modelBuilder.Entity<QuinielaUsuario>()
            .HasKey(x => new { x.QuinielaId, x.UsuarioId });

        modelBuilder.Entity<QuinielaUsuario>()
            .HasOne(x => x.Quiniela)
            .WithMany(x => x.Participantes)
            .HasForeignKey(x => x.QuinielaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuinielaUsuario>()
            .HasOne(x => x.Usuario)
            .WithMany(x => x.Quinielas)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UsuarioInsignia>()
            .HasKey(x => new { x.UsuarioId, x.InsigniaId });

        modelBuilder.Entity<UsuarioInsignia>()
            .HasOne(x => x.Usuario)
            .WithMany(x => x.Insignias)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UsuarioInsignia>()
            .HasOne(x => x.Insignia)
            .WithMany(x => x.Usuarios)
            .HasForeignKey(x => x.InsigniaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Partido>()
            .HasOne(x => x.SeleccionLocal)
            .WithMany()
            .HasForeignKey(x => x.SeleccionLocalId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Partido>()
            .HasOne(x => x.SeleccionVisitante)
            .WithMany()
            .HasForeignKey(x => x.SeleccionVisitanteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Pronostico>()
            .HasIndex(x => new { x.UsuarioId, x.PartidoId })
            .IsUnique();

        modelBuilder.Entity<TimelineEvento>()
            .HasOne(x => x.Quiniela)
            .WithMany(x => x.Eventos)
            .HasForeignKey(x => x.QuinielaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
