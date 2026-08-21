using System.ComponentModel.DataAnnotations;

namespace SistemaQuinielaMundialistasV2.Models;

public class InsigniaEntidad
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<UsuarioInsignia> Usuarios { get; set; } = new List<UsuarioInsignia>();
}
