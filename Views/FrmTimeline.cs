using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Services;

namespace SistemaQuinielasMundialistas.Views
{
    public partial class FrmTimeline : Form
    {
        private readonly TimelineService timelineService = new();
        private readonly QuinielaService quinielaService = new();

        public FrmTimeline()
        {
            InitializeComponent();
            CargarQuinielas();
            MostrarEventos();
        }

        private void CargarQuinielas()
        {
            List<QuinielaFiltro> opciones = new()
            {
                new QuinielaFiltro { Id = null, Nombre = "Todas las quinielas" }
            };

            opciones.AddRange(
                quinielaService.ObtenerQuinielas()
                    .Select(q => new QuinielaFiltro { Id = q.Id, Nombre = q.Nombre }));

            cboQuiniela.DataSource = opciones;
            cboQuiniela.DisplayMember = nameof(QuinielaFiltro.Nombre);
            cboQuiniela.ValueMember = nameof(QuinielaFiltro.Id);
        }

        private void MostrarEventos()
        {
            int? quinielaId = (cboQuiniela.SelectedItem as QuinielaFiltro)?.Id;
            List<TimelineEvento> eventos = timelineService.ObtenerEventosPorQuiniela(quinielaId);

            dgvTimeline.DataSource = null;
            dgvTimeline.DataSource = eventos.Select(e => new
            {
                Fecha = e.Fecha.ToString("dd/MM/yyyy HH:mm"),
                e.Quiniela,
                e.Tipo,
                e.Mensaje
            }).ToList();
        }

        private void cboQuiniela_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarEventos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            MostrarEventos();
        }

        private sealed class QuinielaFiltro
        {
            public int? Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
        }
    }
}
