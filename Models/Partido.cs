using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaQuinielaMundialistasV2.Models;

public class Partido
{
    public int Id { get; set; }
    public int SeleccionLocalId { get; set; }
    public Seleccion? SeleccionLocal { get; set; }
    public int SeleccionVisitanteId { get; set; }
    public Seleccion? SeleccionVisitante { get; set; }
    public DateTime FechaHora { get; set; }

    [Required, MaxLength(30)]
    public string Estado { get; set; } = "Próximo";

    [MaxLength(10)]
    public string Grupo { get; set; } = string.Empty;

    [MaxLength(40)]
    public string Fase { get; set; } = string.Empty;

    public int NumeroCruce { get; set; }
    public int GolesLocal { get; set; }
    public int GolesVisitante { get; set; }
    public bool FueAPenales { get; set; }
    public int GolesPenalesLocal { get; set; }
    public int GolesPenalesVisitante { get; set; }

    [MaxLength(1000)]
    public string Anotadores { get; set; } = string.Empty;

    public ICollection<Pronostico> Pronosticos { get; set; } = new List<Pronostico>();

    [NotMapped]
    public string NombrePartido => $"{SeleccionLocal?.Nombre ?? "Local"} vs {SeleccionVisitante?.Nombre ?? "Visitante"}";
}
