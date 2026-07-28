namespace SistemaQuinielasMundialistas.Models
{
    public sealed class InsigniaResultado
    {
        public string Insignia { get; init; } = string.Empty;
        public string Descripcion { get; init; } = string.Empty;
        public string Usuario { get; init; } = "Sin ganador todavía";
        public int Puntos { get; init; }
    }
}
