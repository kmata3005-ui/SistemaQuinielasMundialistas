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
    }
}