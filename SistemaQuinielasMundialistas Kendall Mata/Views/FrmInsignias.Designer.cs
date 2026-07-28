namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmInsignias
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
            lblExplicacion = new Label();
            btnActualizar = new Button();
            dgvInsignias = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvInsignias).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(250, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(310, 38);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "MÓDULO DE INSIGNIAS";
            // 
            // lblExplicacion
            // 
            lblExplicacion.AutoSize = true;
            lblExplicacion.Location = new Point(145, 65);
            lblExplicacion.Name = "lblExplicacion";
            lblExplicacion.Size = new Size(540, 25);
            lblExplicacion.TabIndex = 1;
            lblExplicacion.Text = "Las insignias se calculan automáticamente con reglas polimórficas.";
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(285, 100);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(180, 38);
            btnActualizar.TabIndex = 2;
            btnActualizar.Text = "Actualizar insignias";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // dgvInsignias
            // 
            dgvInsignias.AllowUserToAddRows = false;
            dgvInsignias.AllowUserToDeleteRows = false;
            dgvInsignias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvInsignias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInsignias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInsignias.Location = new Point(25, 155);
            dgvInsignias.MultiSelect = false;
            dgvInsignias.Name = "dgvInsignias";
            dgvInsignias.ReadOnly = true;
            dgvInsignias.RowHeadersWidth = 62;
            dgvInsignias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInsignias.Size = new Size(760, 300);
            dgvInsignias.TabIndex = 3;
            // 
            // FrmInsignias
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 480);
            Controls.Add(dgvInsignias);
            Controls.Add(btnActualizar);
            Controls.Add(lblExplicacion);
            Controls.Add(lblTitulo);
            MinimumSize = new Size(760, 430);
            Name = "FrmInsignias";
            Text = "Insignias";
            ((System.ComponentModel.ISupportInitialize)dgvInsignias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblExplicacion;
        private Button btnActualizar;
        private DataGridView dgvInsignias;
    }
}
