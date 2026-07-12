namespace SistemaQuinielasMundialistas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Correo { get; set; } = "";
        public string NombreUsuario { get; set; } = "";
        public string Contrasena { get; set; } = "";
        public string PaisPreferido { get; set; } = "";
        public int Puntos { get; set; }
    }
}