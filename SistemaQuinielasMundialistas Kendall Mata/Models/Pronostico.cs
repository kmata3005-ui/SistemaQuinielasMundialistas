using System;

namespace SistemaQuinielasMundialistas.Models
{
    public class Pronostico
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public int PartidoId { get; set; }

        public string EquipoLocal { get; set; } = string.Empty;

        public string EquipoVisitante { get; set; } = string.Empty;

        public int GolesLocalPronosticados { get; set; }

        public int GolesVisitantePronosticados { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int PuntosObtenidos { get; set; }
    }
}