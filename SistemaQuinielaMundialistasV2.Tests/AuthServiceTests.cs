using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;
using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;
using Xunit;

namespace SistemaQuinielaMundialistasV2.Tests;

public class AuthServiceTests
{
    private static (
        SqliteConnection Connection,
        IDbContextFactory<AppDbContext> Factory)
        CrearEntorno()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        var factory = new TestDbContextFactory(options);

        using var db = factory.CreateDbContext();
        db.Database.EnsureCreated();

        return (connection, factory);
    }

    private static Usuario CrearUsuario(bool activo = true)
    {
        return new Usuario
        {
            Nombre = "Kendall Test",
            Correo = "kendall@test.com",
            NombreUsuario = "kendall",
            Contrasena = PasswordService.Hash("Clave123"),
            PaisPreferido = "Costa Rica",
            Puntos = 0,
            Rol = "Usuario",
            Activo = activo
        };
    }

    [Fact]
    public async Task LoginAsync_UsuarioCorrecto_RetornaUsuario()
    {
        var (connection, factory) = CrearEntorno();

        using (connection)
        {
            using (var db = factory.CreateDbContext())
            {
                db.Usuarios.Add(CrearUsuario());
                db.SaveChanges();
            }

            var service = new AuthService(factory);

            var resultado = await service.LoginAsync("kendall", "Clave123");

            Assert.NotNull(resultado.Usuario);
            Assert.Null(resultado.Error);
            Assert.Equal("kendall", resultado.Usuario!.NombreUsuario);
        }
    }

    [Fact]
    public async Task LoginAsync_CorreoCorrecto_RetornaUsuario()
    {
        var (connection, factory) = CrearEntorno();

        using (connection)
        {
            using (var db = factory.CreateDbContext())
            {
                db.Usuarios.Add(CrearUsuario());
                db.SaveChanges();
            }

            var service = new AuthService(factory);

            var resultado =
                await service.LoginAsync("kendall@test.com", "Clave123");

            Assert.NotNull(resultado.Usuario);
            Assert.Null(resultado.Error);
            Assert.Equal("kendall@test.com", resultado.Usuario!.Correo);
        }
    }

    [Fact]
    public async Task LoginAsync_ContrasenaIncorrecta_RetornaError()
    {
        var (connection, factory) = CrearEntorno();

        using (connection)
        {
            using (var db = factory.CreateDbContext())
            {
                db.Usuarios.Add(CrearUsuario());
                db.SaveChanges();
            }

            var service = new AuthService(factory);

            var resultado =
                await service.LoginAsync("kendall", "Incorrecta");

            Assert.Null(resultado.Usuario);
            Assert.Equal(
                "Usuario/correo o contraseña incorrectos.",
                resultado.Error);
        }
    }

    [Fact]
    public async Task LoginAsync_UsuarioNoExiste_RetornaError()
    {
        var (connection, factory) = CrearEntorno();

        using (connection)
        {
            var service = new AuthService(factory);

            var resultado =
                await service.LoginAsync("noexiste", "Clave123");

            Assert.Null(resultado.Usuario);
            Assert.Equal(
                "Usuario/correo o contraseña incorrectos.",
                resultado.Error);
        }
    }

    [Fact]
    public async Task LoginAsync_UsuarioDesactivado_RetornaError()
    {
        var (connection, factory) = CrearEntorno();

        using (connection)
        {
            using (var db = factory.CreateDbContext())
            {
                db.Usuarios.Add(CrearUsuario(false));
                db.SaveChanges();
            }

            var service = new AuthService(factory);

            var resultado =
                await service.LoginAsync("kendall", "Clave123");

            Assert.Null(resultado.Usuario);
            Assert.Equal(
                "Este usuario está desactivado. Contacte al administrador.",
                resultado.Error);
        }
    }

    [Fact]
    public async Task LoginAsync_UsuarioMayusculas_Funciona()
    {
        var (connection, factory) = CrearEntorno();

        using (connection)
        {
            using (var db = factory.CreateDbContext())
            {
                db.Usuarios.Add(CrearUsuario());
                db.SaveChanges();
            }

            var service = new AuthService(factory);

            var resultado =
                await service.LoginAsync("KENDALL", "Clave123");

            Assert.NotNull(resultado.Usuario);
            Assert.Null(resultado.Error);
        }
    }

    [Fact]
    public async Task LoginAsync_UsuarioConEspacios_Funciona()
    {
        var (connection, factory) = CrearEntorno();

        using (connection)
        {
            using (var db = factory.CreateDbContext())
            {
                db.Usuarios.Add(CrearUsuario());
                db.SaveChanges();
            }

            var service = new AuthService(factory);

            var resultado =
                await service.LoginAsync("   kendall   ", "Clave123");

            Assert.NotNull(resultado.Usuario);
            Assert.Null(resultado.Error);
        }
    }

    private sealed class TestDbContextFactory
        : IDbContextFactory<AppDbContext>
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public TestDbContextFactory(
            DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        public AppDbContext CreateDbContext()
        {
            return new AppDbContext(_options);
        }
    }
}