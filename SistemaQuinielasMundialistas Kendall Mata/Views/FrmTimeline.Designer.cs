namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmTimeline
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblFiltro = new Label();
            cboQuiniela = new ComboBox();
            btnActualizar = new Button();
            dgvTimeline = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvTimeline).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(421, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Timeline de notificaciones";
            // 
            // lblFiltro
            // 
            lblFiltro.AutoSize = true;
            lblFiltro.Location = new Point(22, 75);
            lblFiltro.Name = "lblFiltro";
            lblFiltro.Size = new Size(83, 25);
            lblFiltro.TabIndex = 1;
            lblFiltro.Text = "Quiniela:";
            // 
            // cboQuiniela
            // 
            cboQuiniela.DropDownStyle = ComboBoxStyle.DropDownList;
            cboQuiniela.FormattingEnabled = true;
            cboQuiniela.Location = new Point(110, 70);
            cboQuiniela.Name = "cboQuiniela";
            cboQuiniela.Size = new Size(270, 33);
            cboQuiniela.TabIndex = 2;
            cboQuiniela.SelectedIndexChanged += cboQuiniela_SelectedIndexChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(400, 68);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(115, 36);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // dgvTimeline
            // 
            dgvTimeline.AllowUserToAddRows = false;
            dgvTimeline.AllowUserToDeleteRows = false;
            dgvTimeline.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTimeline.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTimeline.BackgroundColor = Color.White;
            dgvTimeline.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTimeline.Location = new Point(20, 125);
            dgvTimeline.MultiSelect = false;
            dgvTimeline.Name = "dgvTimeline";
            dgvTimeline.ReadOnly = true;
            dgvTimeline.RowHeadersVisible = false;
            dgvTimeline.RowHeadersWidth = 62;
            dgvTimeline.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTimeline.Size = new Size(720, 350);
            dgvTimeline.TabIndex = 4;
            // 
            // FrmTimeline
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(765, 500);
            Controls.Add(dgvTimeline);
            Controls.Add(btnActualizar);
            Controls.Add(cboQuiniela);
            Controls.Add(lblFiltro);
            Controls.Add(lblTitulo);
            MinimumSize = new Size(620, 480);
            Name = "FrmTimeline";
            Padding = new Padding(18);
            Text = "Timeline de Quinielas";
            ((System.ComponentModel.ISupportInitialize)dgvTimeline).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblFiltro;
        private ComboBox cboQuiniela;
        private Button btnActualizar;
        private DataGridView dgvTimeline;
    }
}
