using System.Drawing;
using System.Text.Json.Serialization;

namespace SistemaQuinielasMundialistas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string PaisPreferido { get; set; } = string.Empty;
        [JsonIgnore]
        public Image Bandera => SistemaQuinielasMundialistas.Utils.BanderaHelper.ObtenerImagen(PaisPreferido);
        public int Puntos { get; set; }
        public List<string> Insignias { get; set; } = new();
    }
}
