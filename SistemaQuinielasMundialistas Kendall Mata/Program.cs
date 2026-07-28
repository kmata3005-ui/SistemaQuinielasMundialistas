namespace SistemaQuinielasMundialistas
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Services.SeedDataService.InicializarSiEsNecesario();

            var partidoService = new Services.PartidoService();
            var pronosticoService = new Services.PronosticoService();
            partidoService.ActualizarEstadosAutomaticos();

            foreach (Models.Partido partido in partidoService.ObtenerPartidos())
            {
                pronosticoService.RecalcularPronosticosDelPartido(partido);
            }

            new Services.UsuarioService().RecalcularPuntosUsuarios(
                pronosticoService.ObtenerPronosticos());

            Application.Run(new FrmPrincipal());
        }
    }
}
