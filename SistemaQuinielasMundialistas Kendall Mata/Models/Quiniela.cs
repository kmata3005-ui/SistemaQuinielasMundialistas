namespace SistemaQuinielasMundialistas.Models
{
    public class Quiniela
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool EsPrivada { get; set; }
        public List<int> ParticipanteIds { get; set; } = new();
        public List<string> Timeline { get; set; } = new();
        public int CantidadParticipantes => ParticipanteIds?.Count ?? 0;
        public string Tipo => EsPrivada ? "Privada" : "Pública";
    }
}
