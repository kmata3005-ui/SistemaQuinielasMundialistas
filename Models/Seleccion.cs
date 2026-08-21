using System.ComponentModel.DataAnnotations;

namespace SistemaQuinielaMundialistasV2.Models;

public class Seleccion
{
    public int Id { get; set; }

    [Required, MaxLength(80)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(5)]
    public string Codigo { get; set; } = string.Empty;

    [MaxLength(120)]
    public string BanderaArchivo { get; set; } = string.Empty;
}
