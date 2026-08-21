using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public class SessionService
{
    public Usuario? UsuarioActual { get; private set; }
    public bool EstaAutenticado => UsuarioActual is not null;
    public bool EsAdministrador => UsuarioActual?.Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase) == true;
    public event Action? Cambio;
    public void Iniciar(Usuario usuario) { UsuarioActual = usuario; Cambio?.Invoke(); }
    public void Cerrar() { UsuarioActual = null; Cambio?.Invoke(); }
}
