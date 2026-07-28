using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Services;

namespace SistemaQuinielasMundialistas.Views
{
    public partial class FrmEstadisticas : Form
    {
        private readonly UsuarioService usuarioService = new();
        private readonly PartidoService partidoService = new();
        private readonly PronosticoService pronosticoService = new();
        private readonly EstadisticaService estadisticaService = new();
        private readonly DateTimePicker dtpDesde = new();
        private readonly DateTimePicker dtpHasta = new();
        private readonly Label lblAvanzadas = new();

        public FrmEstadisticas()
        {
            InitializeComponent();
            ConfigurarFiltros();
            CargarEstadisticas();
        }

        private void ConfigurarFiltros()
        {
            ClientSize = new Size(920, 590);
            Label desde = new() { Text = "Desde:", AutoSize = true, Location = new Point(55, 230) };
            Label hasta = new() { Text = "Hasta:", AutoSize = true, Location = new Point(390, 230) };
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(125, 225);
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(460, 225);

            var fechas = partidoService.ObtenerPartidos().Select(p => p.FechaHora).ToList();
            DateTime hoy = partidoService.ObtenerFechaSimulada();
            dtpDesde.Value = fechas.Count > 0 ? fechas.Min().Date : hoy.AddMonths(-1).Date;
            dtpHasta.Value = hoy.Date;

            Button btnFiltrar = new() { Text = "Aplicar filtro", Location = new Point(700, 223), Size = new Size(145, 34) };
            btnFiltrar.Click += (_, _) => CargarEstadisticas();

            lblAvanzadas.AutoSize = true;
            lblAvanzadas.Location = new Point(55, 285);
            lblAvanzadas.Font = new Font("Segoe UI", 10F);
            lblAvanzadas.MaximumSize = new Size(820, 0);

            Controls.AddRange(new Control[] { desde, dtpDesde, hasta, dtpHasta, btnFiltrar, lblAvanzadas });
        }

        private void CargarEstadisticas()
        {
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddTicks(-1);
            if (desde > hasta)
            {
                MessageBox.Show("La fecha inicial no puede ser posterior a la fecha final.");
                return;
            }

            var usuarios = usuarioService.ObtenerUsuarios();
            var partidos = partidoService.ObtenerPartidos().Where(p => p.FechaHora >= desde && p.FechaHora <= hasta).ToList();
            var ids = partidos.Select(p => p.Id).ToHashSet();
            var pronosticos = pronosticoService.ObtenerPronosticos()
                .Where(p => ids.Contains(p.PartidoId) || (p.FechaRegistro >= desde && p.FechaRegistro <= hasta))
                .ToList();

            lblTotalUsuarios.Text = "Total de usuarios: " + usuarios.Count;
            lblTotalPartidos.Text = "Partidos en el rango: " + partidos.Count;
            lblTotalPronosticos.Text = "Pronósticos en el rango: " + pronosticos.Count;

            Usuario? mejorUsuario = usuarios.OrderByDescending(u => u.Puntos).FirstOrDefault();
            lblMejorUsuario.Text = mejorUsuario == null
                ? "Mejor usuario: Sin datos"
                : $"Mejor usuario: {mejorUsuario.NombreUsuario} ({mejorUsuario.Puntos} pts)";

            lblAvanzadas.Text =
                $"ESTADÍSTICAS DEL {desde:dd/MM/yyyy} AL {hasta:dd/MM/yyyy}\n\n" +
                $"Equipo más apostado: {estadisticaService.EquipoMasApostado(pronosticos)}\n" +
                $"Resultado más repetido: {estadisticaService.ResultadoMasRepetido(pronosticos)}\n" +
                $"Partido con más pronósticos: {estadisticaService.PartidoConMasPronosticos(pronosticos)}\n" +
                $"Partido con más aciertos: {estadisticaService.PartidoConMasAciertos(pronosticos)}\n" +
                $"Usuario con más aciertos: {estadisticaService.UsuarioConMasAciertos(pronosticos)}\n" +
                $"Equipo sorpresa: {estadisticaService.EquipoSorpresa(partidos, pronosticos)}\n" +
                $"Promedio de goles: {estadisticaService.PromedioGoles(partidos):0.00}";
        }
    }
}
