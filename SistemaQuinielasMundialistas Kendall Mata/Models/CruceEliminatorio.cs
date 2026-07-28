namespace SistemaQuinielasMundialistas.Models
{
    public class CruceEliminatorio
    {
        public int Numero { get; set; }
        public string Fase { get; set; } = string.Empty;
        public string EquipoLocal { get; set; } = string.Empty;
        public string EquipoVisitante { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
        public string Ganador { get; set; } = string.Empty;
    }
}
