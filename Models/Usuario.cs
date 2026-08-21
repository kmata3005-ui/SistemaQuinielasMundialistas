using System.ComponentModel.DataAnnotations;

namespace SistemaQuinielaMundialistasV2.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Nombre { get; set; } = string.Empty;

    [Required, MaxLength(160)]
    public string Correo { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Contrasena { get; set; } = string.Empty;

    [MaxLength(80)]
    public string PaisPreferido { get; set; } = string.Empty;

    public int Puntos { get; set; }

    [Required, MaxLength(20)]
    public string Rol { get; set; } = "Usuario";

    public bool Activo { get; set; } = true;

    public ICollection<Pronostico> Pronosticos { get; set; } = new List<Pronostico>();
    public ICollection<QuinielaUsuario> Quinielas { get; set; } = new List<QuinielaUsuario>();
    public ICollection<UsuarioInsignia> Insignias { get; set; } = new List<UsuarioInsignia>();
}
