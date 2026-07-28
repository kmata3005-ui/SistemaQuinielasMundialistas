namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmInformacion
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblFechaSimulada;
        private Button btnActualizar;
        private TabControl tabInformacion;
        private TabPage tabHistorial;
        private TabPage tabUltimos;
        private TabPage tabProximos;
        private TabPage tabRankingPublico;
        private TabPage tabRankingPrivado;
        private ComboBox cboUsuario;
        private ComboBox cboQuiniela;
        private Label lblUsuario;
        private Label lblQuiniela;
        private Label lblRankingPrivado;
        private DataGridView dgvHistorial;
        private DataGridView dgvUltimos;
        private DataGridView dgvProximos;
        private DataGridView dgvRankingGlobal;
        private DataGridView dgvRankingPrivado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblFechaSimulada = new Label();
            btnActualizar = new Button();
            tabInformacion = new TabControl();
            tabHistorial = new TabPage();
            dgvHistorial = new DataGridView();
            cboUsuario = new ComboBox();
            lblUsuario = new Label();
            tabUltimos = new TabPage();
            dgvUltimos = new DataGridView();
            tabProximos = new TabPage();
            dgvProximos = new DataGridView();
            tabRankingPublico = new TabPage();
            dgvRankingGlobal = new DataGridView();
            tabRankingPrivado = new TabPage();
            dgvRankingPrivado = new DataGridView();
            lblRankingPrivado = new Label();
            cboQuiniela = new ComboBox();
            lblQuiniela = new Label();
            tabInformacion.SuspendLayout();
            tabHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            tabUltimos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUltimos).BeginInit();
            tabProximos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProximos).BeginInit();
            tabRankingPublico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRankingGlobal).BeginInit();
            tabRankingPrivado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRankingPrivado).BeginInit();
            SuspendLayout();
            // lblTitulo
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(24, 18);
            lblTitulo.Text = "INFORMACIÓN MUNDIALISTA";
            // lblFechaSimulada
            lblFechaSimulada.AutoSize = true;
            lblFechaSimulada.Location = new Point(27, 59);
            lblFechaSimulada.Text = "Fecha simulada:";
            // btnActualizar
            btnActualizar.Location = new Point(500, 49);
            btnActualizar.Size = new Size(150, 34);
            btnActualizar.Text = "Actualizar información";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // tabInformacion
            tabInformacion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabInformacion.Controls.Add(tabHistorial);
            tabInformacion.Controls.Add(tabUltimos);
            tabInformacion.Controls.Add(tabProximos);
            tabInformacion.Controls.Add(tabRankingPublico);
            tabInformacion.Controls.Add(tabRankingPrivado);
            tabInformacion.Location = new Point(20, 95);
            tabInformacion.Size = new Size(1080, 500);
            // historial
            tabHistorial.Text = "Historial de pronósticos";
            tabHistorial.Controls.Add(dgvHistorial);
            tabHistorial.Controls.Add(cboUsuario);
            tabHistorial.Controls.Add(lblUsuario);
            lblUsuario.AutoSize = true; lblUsuario.Location = new Point(18, 20); lblUsuario.Text = "Usuario:";
            cboUsuario.DropDownStyle = ComboBoxStyle.DropDownList; cboUsuario.Location = new Point(90, 16); cboUsuario.Size = new Size(230, 28); cboUsuario.SelectedIndexChanged += cboUsuario_SelectedIndexChanged;
            ConfigurarGrid(dgvHistorial, new Point(15, 58), new Size(1035, 395));
            // ultimos
            tabUltimos.Text = "Últimos 5 partidos";
            tabUltimos.Controls.Add(dgvUltimos);
            ConfigurarGrid(dgvUltimos, new Point(15, 18), new Size(1035, 435));
            // proximos
            tabProximos.Text = "Próximos 24 horas";
            tabProximos.Controls.Add(dgvProximos);
            ConfigurarGrid(dgvProximos, new Point(15, 18), new Size(1035, 435));
            // ranking publico
            tabRankingPublico.Text = "Ranking público";
            tabRankingPublico.Controls.Add(dgvRankingGlobal);
            ConfigurarGrid(dgvRankingGlobal, new Point(15, 18), new Size(1035, 435));
            // ranking privado
            tabRankingPrivado.Text = "Ranking privado";
            tabRankingPrivado.Controls.Add(dgvRankingPrivado);
            tabRankingPrivado.Controls.Add(lblRankingPrivado);
            tabRankingPrivado.Controls.Add(cboQuiniela);
            tabRankingPrivado.Controls.Add(lblQuiniela);
            lblQuiniela.AutoSize = true; lblQuiniela.Location = new Point(18, 20); lblQuiniela.Text = "Quiniela:";
            cboQuiniela.DropDownStyle = ComboBoxStyle.DropDownList; cboQuiniela.Location = new Point(90, 16); cboQuiniela.Size = new Size(260, 28); cboQuiniela.SelectedIndexChanged += cboQuiniela_SelectedIndexChanged;
            lblRankingPrivado.AutoSize = true; lblRankingPrivado.Font = new Font("Segoe UI", 9F, FontStyle.Bold); lblRankingPrivado.Location = new Point(380, 20); lblRankingPrivado.Text = "Ranking privado";
            ConfigurarGrid(dgvRankingPrivado, new Point(15, 58), new Size(1035, 395));
            // form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1120, 620);
            Controls.Add(tabInformacion);
            Controls.Add(btnActualizar);
            Controls.Add(lblFechaSimulada);
            Controls.Add(lblTitulo);
            Name = "FrmInformacion";
            Text = "Información";
            tabInformacion.ResumeLayout(false);
            tabHistorial.ResumeLayout(false); tabHistorial.PerformLayout(); ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            tabUltimos.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvUltimos).EndInit();
            tabProximos.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvProximos).EndInit();
            tabRankingPublico.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvRankingGlobal).EndInit();
            tabRankingPrivado.ResumeLayout(false); tabRankingPrivado.PerformLayout(); ((System.ComponentModel.ISupportInitialize)dgvRankingPrivado).EndInit();
            ResumeLayout(false); PerformLayout();
        }

        private static void ConfigurarGrid(DataGridView grid, Point location, Size size)
        {
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid.Location = location;
            grid.Size = size;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
