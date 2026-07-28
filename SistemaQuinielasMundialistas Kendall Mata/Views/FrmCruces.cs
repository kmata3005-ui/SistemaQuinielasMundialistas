using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Services;

namespace SistemaQuinielasMundialistas.Views
{
    public partial class FrmCruces : Form
    {
        private readonly CruceService cruceService = new CruceService();

        public FrmCruces()
        {
            InitializeComponent();
            CargarCruces();
        }

        private void btnGenerarActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                cruceService.ActualizarCruces();
                CargarCruces();
                MessageBox.Show(
                    "Cruces actualizados. Las siguientes rondas se generan cuando todos los partidos de la ronda anterior están finalizados y tienen ganador.",
                    "Cruces automáticos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudieron actualizar los cruces", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDefinirPenales_Click(object sender, EventArgs e)
        {
            if (dgvCruces.CurrentRow?.DataBoundItem is not CruceEliminatorio cruce)
            {
                MessageBox.Show("Seleccione un cruce.", "Penales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                cruceService.DefinirGanadorPorPenales(
                    cruce.Fase,
                    cruce.Numero,
                    (int)nudPenalesLocal.Value,
                    (int)nudPenalesVisitante.Value);

                CargarCruces();
                MessageBox.Show(
                    "Ganador por penales registrado. Si la ronda quedó completa, la siguiente fase se generó automáticamente.",
                    "Penales registrados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudieron registrar los penales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarCruces()
        {
            try
            {
                var cruces = cruceService.ObtenerCruces();
                dgvCruces.AutoGenerateColumns = true;
                dgvCruces.DataSource = null;
                dgvCruces.DataSource = cruces;
                lblResumen.Text = cruces.Count == 0
                    ? "Finalice la fase de grupos para generar los cuartos de final."
                    : $"Cruces generados: {cruces.Count}. Edite resultados desde el módulo Partidos.";
            }
            catch (Exception ex)
            {
                lblResumen.Text = ex.Message;
            }
        }
    }
}
