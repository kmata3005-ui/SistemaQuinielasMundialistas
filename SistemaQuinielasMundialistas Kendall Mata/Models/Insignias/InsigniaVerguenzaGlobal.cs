namespace SistemaQuinielasMundialistas.Models.Insignias
{
    public sealed class InsigniaVerguenzaGlobal : Insignia
    {
        public override string Nombre => "Último del ranking global";
        public override string Descripcion => "Insignia de la vergüenza para el usuario con menos puntos.";

        public override Usuario? ObtenerGanador(
            IReadOnlyCollection<Usuario> usuarios,
            IReadOnlyCollection<Pronostico> pronosticos)
        {
            if (usuarios.Count < 2)
                return null;

            return usuarios
                .OrderBy(u => u.Puntos)
                .ThenBy(u => u.NombreUsuario)
                .FirstOrDefault();
        }
    }
}
