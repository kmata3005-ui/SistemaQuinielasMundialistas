namespace SistemaQuinielasMundialistas.Models
{
    public class TimelineEvento
    {
        public int Id { get; set; }
        public int? QuinielaId { get; set; }
        public string Quiniela { get; set; } = "General";
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = "Información";
        public string Mensaje { get; set; } = string.Empty;
    }
}
