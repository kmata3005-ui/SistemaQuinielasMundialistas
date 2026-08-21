using System.ComponentModel.DataAnnotations;

namespace SistemaQuinielaMundialistasV2.Models;

public class TimelineEvento
{
    public int Id { get; set; }
    public int? QuinielaId { get; set; }
    public Quiniela? Quiniela { get; set; }
    public DateTime Fecha { get; set; }

    [Required, MaxLength(60)]
    public string Tipo { get; set; } = "Información";

    [Required, MaxLength(700)]
    public string Mensaje { get; set; } = string.Empty;
}
