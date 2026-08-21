namespace SistemaQuinielaMundialistasV2.Models;

public class Pronostico
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public int PartidoId { get; set; }
    public Partido? Partido { get; set; }
    public int GolesLocalPronosticados { get; set; }
    public int GolesVisitantePronosticados { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int PuntosObtenidos { get; set; }
}
