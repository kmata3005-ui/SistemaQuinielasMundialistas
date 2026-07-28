using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Services;

namespace SistemaQuinielasMundialistas.Views
{
    public partial class FrmGrupos : Form
    {
        private readonly GrupoService grupoService = new GrupoService();

        public FrmGrupos()
        {
            InitializeComponent();
            ConfigurarTabla();
            CargarGrupos();
        }

        private void ConfigurarTabla()
        {
            dgvTabla.AutoGenerateColumns = false;
            dgvTabla.Columns.Clear();

            AgregarColumna("Posición", nameof(PosicionGrupo.Posicion), 55);
            AgregarColumnaImagen("Bandera", nameof(PosicionGrupo.Bandera), 60);
            AgregarColumna("Equipo", nameof(PosicionGrupo.Equipo), 160);
            AgregarColumna("PJ", nameof(PosicionGrupo.PartidosJugados), 50);
            AgregarColumna("PG", nameof(PosicionGrupo.PartidosGanados), 50);
            AgregarColumna("PE", nameof(PosicionGrupo.PartidosEmpatados), 50);
            AgregarColumna("PP", nameof(PosicionGrupo.PartidosPerdidos), 50);
            AgregarColumna("GF", nameof(PosicionGrupo.GolesFavor), 50);
            AgregarColumna("GC", nameof(PosicionGrupo.GolesContra), 50);
            AgregarColumna("DG", nameof(PosicionGrupo.DiferenciaGoles), 55);
            AgregarColumna("PTS", nameof(PosicionGrupo.Puntos), 55);
            AgregarColumna("Clasifica", nameof(PosicionGrupo.Clasificado), 75);
        }

        private void AgregarColumna(string encabezado, string propiedad, int ancho)
        {
            dgvTabla.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = encabezado,
                DataPropertyName = propiedad,
                FillWeight = ancho,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
        }

        private void AgregarColumnaImagen(string encabezado, string propiedad, int ancho)
        {
            dgvTabla.Columns.Add(new DataGridViewImageColumn
            {
                HeaderText = encabezado,
                DataPropertyName = propiedad,
                FillWeight = ancho,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            dgvTabla.RowTemplate.Height = 30;
        }

        private void CargarGrupos()
        {
            string? seleccion = cboGrupo.SelectedItem?.ToString();
            List<string> grupos = grupoService.ObtenerGrupos();
            cboGrupo.DataSource = grupos;

            if (!string.IsNullOrWhiteSpace(seleccion) && grupos.Contains(seleccion))
                cboGrupo.SelectedItem = seleccion;
            else if (grupos.Count > 0)
                cboGrupo.SelectedIndex = 0;
            else
                lblResumen.Text = "No existen partidos de fase de grupos.";
        }

        private void CargarTabla()
        {
            if (cboGrupo.SelectedItem is not string grupo)
                return;

            List<PosicionGrupo> tabla = grupoService.CalcularTabla(grupo);
            dgvTabla.DataSource = null;
            dgvTabla.DataSource = tabla;
            lblResumen.Text = $"Grupo {grupo} | Clasifican los primeros 2";

            foreach (DataGridViewRow row in dgvTabla.Rows)
            {
                row.DefaultCellStyle.Font = dgvTabla.Font;
                row.DefaultCellStyle.BackColor = Color.White;

                if (row.DataBoundItem is PosicionGrupo posicion && posicion.Clasificado)
                {
                    row.DefaultCellStyle.Font = new Font(dgvTabla.Font, FontStyle.Bold);
                    row.DefaultCellStyle.BackColor = Color.Honeydew;
                }
            }
        }

        private void cboGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTabla();
        }
    }
}
