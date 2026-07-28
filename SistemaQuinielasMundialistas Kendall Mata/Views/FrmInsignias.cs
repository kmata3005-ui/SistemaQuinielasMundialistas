using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Services;

namespace SistemaQuinielasMundialistas.Views
{
    public sealed partial class FrmInsignias : Form
    {
        private readonly UsuarioService usuarioService = new();
        private readonly PronosticoService pronosticoService = new();
        private readonly InsigniaService insigniaService = new();

        public FrmInsignias()
        {
            InitializeComponent();
            CargarInsignias();
        }

        private void CargarInsignias()
        {
            List<Usuario> usuarios = usuarioService.ObtenerUsuarios();
            List<Pronostico> pronosticos = pronosticoService.ObtenerPronosticos();
            List<InsigniaResultado> resultados = insigniaService.EvaluarYAsignar(usuarios, pronosticos);

            usuarioService.GuardarEnJson();
            dgvInsignias.DataSource = null;
            dgvInsignias.DataSource = resultados;

            if (dgvInsignias.Columns[nameof(InsigniaResultado.Puntos)] != null)
                dgvInsignias.Columns[nameof(InsigniaResultado.Puntos)].HeaderText = "Puntos actuales";
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarInsignias();
        }
    }
}
