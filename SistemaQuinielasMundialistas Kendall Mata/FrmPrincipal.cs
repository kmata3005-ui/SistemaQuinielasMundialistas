using SistemaQuinielasMundialistas.Views;

namespace SistemaQuinielasMundialistas
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            this.Text = "Sistema de Quinielas Mundialistas";
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmUsuarios frm = new Views.FrmUsuarios();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }
        private void btnQuinielas_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmQuinielas frm = new Views.FrmQuinielas();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }

        private void btnPartidos_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmPartidos frm = new Views.FrmPartidos();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmRanking frm = new Views.FrmRanking();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmEstadisticas frm = new Views.FrmEstadisticas();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }

        private void btnPronosticos_Click(object sender, EventArgs e)
        {
            FrmPronosticos frm = new FrmPronosticos();
            frm.ShowDialog();
        }

        private void btnInsignias_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmInsignias frm = new Views.FrmInsignias();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }
        private void btnTimeline_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmTimeline frm = new Views.FrmTimeline();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }

        private void btnGrupos_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmGrupos frm = new Views.FrmGrupos();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }

        private void btnCruces_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmCruces frm = new Views.FrmCruces();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }

        private void btnInformacion_Click(object sender, EventArgs e)
        {
            panelContenido.Controls.Clear();

            Views.FrmInformacion frm = new Views.FrmInformacion();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frm);
            frm.Show();
        }

    }
}