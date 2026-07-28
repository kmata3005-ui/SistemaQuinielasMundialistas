namespace SistemaQuinielasMundialistas.Models.Insignias
{
    public sealed class InsigniaReyEmpates : Insignia
    {
        public override string Nombre => "Rey de los empates";
        public override string Descripcion => "Usuario con más pronósticos acertados de empate.";

        public override Usuario? ObtenerGanador(
            IReadOnlyCollection<Usuario> usuarios,
            IReadOnlyCollection<Pronostico> pronosticos)
        {
            var ganador = pronosticos
                .Where(p => p.GolesLocalPronosticados == p.GolesVisitantePronosticados && p.PuntosObtenidos > 0)
                .GroupBy(p => p.NombreUsuario, StringComparer.OrdinalIgnoreCase)
                .Select(g => new { NombreUsuario = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .ThenBy(x => x.NombreUsuario)
                .FirstOrDefault();

            return ganador == null
                ? null
                : usuarios.FirstOrDefault(u =>
                    u.NombreUsuario.Equals(ganador.NombreUsuario, StringComparison.OrdinalIgnoreCase));
        }
    }
}
