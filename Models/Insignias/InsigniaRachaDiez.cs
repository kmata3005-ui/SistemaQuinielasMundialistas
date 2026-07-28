namespace SistemaQuinielasMundialistas.Models.Insignias
{
    public sealed class InsigniaRachaDiez : Insignia
    {
        public override string Nombre => "Racha de 10 aciertos";
        public override string Descripcion => "Usuario que alcanzó una racha de diez o más pronósticos acertados.";

        public override Usuario? ObtenerGanador(
            IReadOnlyCollection<Usuario> usuarios,
            IReadOnlyCollection<Pronostico> pronosticos)
        {
            string? mejorUsuario = null;
            int mejorRacha = 0;

            foreach (var grupo in pronosticos.GroupBy(
                p => p.NombreUsuario,
                StringComparer.OrdinalIgnoreCase))
            {
                int rachaActual = 0;
                int rachaMaxima = 0;

                foreach (Pronostico pronostico in grupo.OrderBy(p => p.FechaRegistro))
                {
                    rachaActual = pronostico.PuntosObtenidos > 0 ? rachaActual + 1 : 0;
                    rachaMaxima = Math.Max(rachaMaxima, rachaActual);
                }

                if (rachaMaxima > mejorRacha)
                {
                    mejorRacha = rachaMaxima;
                    mejorUsuario = grupo.Key;
                }
            }

            if (mejorRacha < 10 || string.IsNullOrWhiteSpace(mejorUsuario))
                return null;

            return usuarios.FirstOrDefault(u =>
                u.NombreUsuario.Equals(mejorUsuario, StringComparison.OrdinalIgnoreCase));
        }
    }
}
