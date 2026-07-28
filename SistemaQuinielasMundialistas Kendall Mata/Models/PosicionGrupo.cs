using System.Drawing;
using System.Text.Json.Serialization;

namespace SistemaQuinielasMundialistas.Models
{
    public class PosicionGrupo
    {
        public int Posicion { get; set; }
        public string Grupo { get; set; } = string.Empty;
        public string Equipo { get; set; } = string.Empty;
        [JsonIgnore]
        public Image Bandera => SistemaQuinielasMundialistas.Utils.BanderaHelper.ObtenerImagen(Equipo);
        public int PartidosJugados { get; set; }
        public int PartidosGanados { get; set; }
        public int PartidosEmpatados { get; set; }
        public int PartidosPerdidos { get; set; }
        public int GolesFavor { get; set; }
        public int GolesContra { get; set; }
        public int DiferenciaGoles => GolesFavor - GolesContra;
        public int Puntos { get; set; }
        public bool Clasificado => Posicion is 1 or 2;
    }
}
