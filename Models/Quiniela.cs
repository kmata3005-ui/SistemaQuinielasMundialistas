using System.ComponentModel.DataAnnotations;

namespace SistemaQuinielaMundialistasV2.Models;

public class Quiniela
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    public bool EsPrivada { get; set; }
    public ICollection<QuinielaUsuario> Participantes { get; set; } = new List<QuinielaUsuario>();
    public ICollection<TimelineEvento> Eventos { get; set; } = new List<TimelineEvento>();
}
