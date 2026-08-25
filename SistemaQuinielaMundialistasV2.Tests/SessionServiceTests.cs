using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class SessionServiceTests
{
    [Fact]
    public void NuevaSesion_NoDebeEstarAutenticada()
    {
        var service = new SessionService();
        Assert.False(service.EstaAutenticado);
        Assert.False(service.EsAdministrador);
        Assert.Null(service.UsuarioActual);
    }

    [Fact]
    public void Iniciar_DebeAutenticarUsuario()
    {
        var service = new SessionService();
        var usuario = new Usuario { Id = 1, Nombre = "Usuario Prueba", Rol = "Usuario" };
        service.Iniciar(usuario);
        Assert.True(service.EstaAutenticado);
        Assert.Equal(usuario, service.UsuarioActual);
        Assert.False(service.EsAdministrador);
    }

    [Fact]
    public void Iniciar_Administrador_DebeDetectarRol()
    {
        var service = new SessionService();
        service.Iniciar(new Usuario { Id = 1, Rol = "Administrador" });
        Assert.True(service.EstaAutenticado);
        Assert.True(service.EsAdministrador);
    }

    [Fact]
    public void EsAdministrador_DebeIgnorarMayusculas()
    {
        var service = new SessionService();
        service.Iniciar(new Usuario { Rol = "ADMINISTRADOR" });
        Assert.True(service.EsAdministrador);
    }

    [Fact]
    public void Cerrar_DebeEliminarSesion()
    {
        var service = new SessionService();
        service.Iniciar(new Usuario { Id = 1, Rol = "Usuario" });
        service.Cerrar();
        Assert.False(service.EstaAutenticado);
        Assert.False(service.EsAdministrador);
        Assert.Null(service.UsuarioActual);
    }

    [Fact]
    public void Iniciar_DebeDispararEventoCambio()
    {
        var service = new SessionService();
        bool ejecutado = false;
        service.Cambio += () => ejecutado = true;
        service.Iniciar(new Usuario());
        Assert.True(ejecutado);
    }

    [Fact]
    public void Cerrar_DebeDispararEventoCambio()
    {
        var service = new SessionService();
        service.Iniciar(new Usuario());
        bool ejecutado = false;
        service.Cambio += () => ejecutado = true;
        service.Cerrar();
        Assert.True(ejecutado);
    }
}
