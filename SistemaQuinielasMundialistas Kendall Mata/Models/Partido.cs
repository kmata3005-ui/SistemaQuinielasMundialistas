namespace SistemaQuinielasMundialistas.Models
{
    public class Partido
    {
        public int Id { get; set; }

        public string EquipoLocal { get; set; } = string.Empty;

        public string EquipoVisitante { get; set; } = string.Empty;

        public DateTime FechaHora { get; set; }

        public string Estado { get; set; } = "Próximo";

        // Vacío para amistosos o partidos eliminatorios. Ejemplo: "A".
        public string Grupo { get; set; } = string.Empty;

        // Fase del torneo: Grupos, Cuartos de final, Semifinal o Final.
        public string Fase { get; set; } = string.Empty;

        // Número interno del cruce dentro de la fase eliminatoria.
        public int NumeroCruce { get; set; }

        public int GolesLocal { get; set; }

        public int GolesVisitante { get; set; }

        // Se utiliza solamente en partidos eliminatorios que terminan empatados.
        public bool FueAPenales { get; set; }

        public int GolesPenalesLocal { get; set; }

        public int GolesPenalesVisitante { get; set; }

        public string Anotadores { get; set; } = string.Empty;
        public string NombrePartido
        {
            get
            {
                return $"{EquipoLocal} vs {EquipoVisitante}";
            }
        }
    }
}