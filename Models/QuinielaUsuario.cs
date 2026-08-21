namespace SistemaQuinielaMundialistasV2.Models;

public class QuinielaUsuario
{
    public int QuinielaId { get; set; }
    public Quiniela? Quiniela { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public DateTime FechaIngreso { get; set; } = DateTime.Now;
}
