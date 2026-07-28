namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmGrupos
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
            lblGrupo = new Label();
            cboGrupo = new ComboBox();
            btnActualizar = new Button();
            lblResumen = new Label();
            dgvTabla = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvTabla).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(413, 38);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "TABLA DE POSICIONES POR GRUPOS";
            // 
            // lblGrupo
            // 
            lblGrupo.AutoSize = true;
            lblGrupo.Location = new Point(28, 75);
            lblGrupo.Name = "lblGrupo";
            lblGrupo.Size = new Size(66, 25);
            lblGrupo.TabIndex = 1;
            lblGrupo.Text = "Grupo:";
            // 
            // cboGrupo
            // 
            cboGrupo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGrupo.FormattingEnabled = true;
            cboGrupo.Location = new Point(100, 70);
            cboGrupo.Name = "cboGrupo";
            cboGrupo.Size = new Size(110, 33);
            cboGrupo.TabIndex = 2;
            cboGrupo.SelectedIndexChanged += cboGrupo_SelectedIndexChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(225, 68);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(145, 36);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar tabla";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResumen.Location = new Point(390, 75);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(0, 25);
            lblResumen.TabIndex = 4;
            // 
            // dgvTabla
            // 
            dgvTabla.AllowUserToAddRows = false;
            dgvTabla.AllowUserToDeleteRows = false;
            dgvTabla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTabla.BackgroundColor = SystemColors.ControlDark;
            dgvTabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTabla.Location = new Point(25, 120);
            dgvTabla.MultiSelect = false;
            dgvTabla.Name = "dgvTabla";
            dgvTabla.ReadOnly = true;
            dgvTabla.RowHeadersVisible = false;
            dgvTabla.RowHeadersWidth = 62;
            dgvTabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTabla.Size = new Size(1000, 430);
            dgvTabla.TabIndex = 5;
            // 
            // FrmGrupos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1050, 575);
            Controls.Add(dgvTabla);
            Controls.Add(lblResumen);
            Controls.Add(btnActualizar);
            Controls.Add(cboGrupo);
            Controls.Add(lblGrupo);
            Controls.Add(lblTitulo);
            MinimumSize = new Size(760, 450);
            Name = "FrmGrupos";
            Text = "Tabla de posiciones por grupos";
            ((System.ComponentModel.ISupportInitialize)dgvTabla).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblGrupo;
        private ComboBox cboGrupo;
        private Button btnActualizar;
        private Label lblResumen;
        private DataGridView dgvTabla;
    }
}
