namespace SistemaQuinielasMundialistas.Models.Insignias
{
    public sealed class InsigniaLiderGlobal : Insignia
    {
        public override string Nombre => "Líder global";
        public override string Descripcion => "Usuario con mayor cantidad de puntos en el ranking global.";

        public override Usuario? ObtenerGanador(
            IReadOnlyCollection<Usuario> usuarios,
            IReadOnlyCollection<Pronostico> pronosticos)
        {
            return usuarios
                .OrderByDescending(u => u.Puntos)
                .ThenBy(u => u.NombreUsuario)
                .FirstOrDefault();
        }
    }
}
