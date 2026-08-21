namespace SistemaQuinielaMundialistasV2.Models;

public class UsuarioInsignia
{
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public int InsigniaId { get; set; }
    public InsigniaEntidad? Insignia { get; set; }
    public DateTime FechaAsignacion { get; set; } = DateTime.Now;
}
