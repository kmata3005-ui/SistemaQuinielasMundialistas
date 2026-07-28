namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmCruces
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblExplicacion;
        private Label lblResumen;
        private Button btnGenerarActualizar;
        private DataGridView dgvCruces;
        private Label lblPenalesLocal;
        private NumericUpDown nudPenalesLocal;
        private Label lblPenalesVisitante;
        private NumericUpDown nudPenalesVisitante;
        private Button btnDefinirPenales;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblExplicacion = new Label();
            lblResumen = new Label();
            btnGenerarActualizar = new Button();
            dgvCruces = new DataGridView();
            lblPenalesLocal = new Label();
            nudPenalesLocal = new NumericUpDown();
            lblPenalesVisitante = new Label();
            nudPenalesVisitante = new NumericUpDown();
            btnDefinirPenales = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCruces).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPenalesLocal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPenalesVisitante).BeginInit();
            SuspendLayout();
            // lblTitulo
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(24, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(291, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "CRUCES AUTOMÁTICOS";
            // lblExplicacion
            lblExplicacion.AutoSize = true;
            lblExplicacion.Location = new Point(26, 64);
            lblExplicacion.Name = "lblExplicacion";
            lblExplicacion.Size = new Size(747, 25);
            lblExplicacion.TabIndex = 1;
            lblExplicacion.Text = "Los clasificados generan cuartos de final; los ganadores avanzan a semifinales y final.";
            // btnGenerarActualizar
            btnGenerarActualizar.Location = new Point(26, 103);
            btnGenerarActualizar.Name = "btnGenerarActualizar";
            btnGenerarActualizar.Size = new Size(211, 38);
            btnGenerarActualizar.TabIndex = 2;
            btnGenerarActualizar.Text = "Generar / actualizar cruces";
            btnGenerarActualizar.UseVisualStyleBackColor = true;
            btnGenerarActualizar.Click += btnGenerarActualizar_Click;
            // lblResumen
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResumen.Location = new Point(257, 111);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(0, 20);
            lblResumen.TabIndex = 3;
            // dgvCruces
            dgvCruces.AllowUserToAddRows = false;
            dgvCruces.AllowUserToDeleteRows = false;
            dgvCruces.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCruces.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCruces.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCruces.Location = new Point(26, 207);
            dgvCruces.MultiSelect = false;
            dgvCruces.Name = "dgvCruces";
            dgvCruces.ReadOnly = true;
            dgvCruces.RowHeadersWidth = 51;
            dgvCruces.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCruces.Size = new Size(1040, 395);
            dgvCruces.TabIndex = 4;
            // lblPenalesLocal
            lblPenalesLocal.AutoSize = true;
            lblPenalesLocal.Location = new Point(26, 163);
            lblPenalesLocal.Name = "lblPenalesLocal";
            lblPenalesLocal.Size = new Size(119, 25);
            lblPenalesLocal.TabIndex = 5;
            lblPenalesLocal.Text = "Penales local:";
            // nudPenalesLocal
            nudPenalesLocal.Location = new Point(151, 160);
            nudPenalesLocal.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            nudPenalesLocal.Name = "nudPenalesLocal";
            nudPenalesLocal.Size = new Size(70, 31);
            nudPenalesLocal.TabIndex = 6;
            nudPenalesLocal.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // lblPenalesVisitante
            lblPenalesVisitante.AutoSize = true;
            lblPenalesVisitante.Location = new Point(239, 163);
            lblPenalesVisitante.Name = "lblPenalesVisitante";
            lblPenalesVisitante.Size = new Size(147, 25);
            lblPenalesVisitante.TabIndex = 7;
            lblPenalesVisitante.Text = "Penales visitante:";
            // nudPenalesVisitante
            nudPenalesVisitante.Location = new Point(392, 160);
            nudPenalesVisitante.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            nudPenalesVisitante.Name = "nudPenalesVisitante";
            nudPenalesVisitante.Size = new Size(70, 31);
            nudPenalesVisitante.TabIndex = 8;
            nudPenalesVisitante.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // btnDefinirPenales
            btnDefinirPenales.Location = new Point(482, 157);
            btnDefinirPenales.Name = "btnDefinirPenales";
            btnDefinirPenales.Size = new Size(220, 38);
            btnDefinirPenales.TabIndex = 9;
            btnDefinirPenales.Text = "Definir ganador por penales";
            btnDefinirPenales.UseVisualStyleBackColor = true;
            btnDefinirPenales.Click += btnDefinirPenales_Click;
            // FrmCruces
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1092, 627);
            Controls.Add(btnDefinirPenales);
            Controls.Add(nudPenalesVisitante);
            Controls.Add(lblPenalesVisitante);
            Controls.Add(nudPenalesLocal);
            Controls.Add(lblPenalesLocal);
            Controls.Add(dgvCruces);
            Controls.Add(lblResumen);
            Controls.Add(btnGenerarActualizar);
            Controls.Add(lblExplicacion);
            Controls.Add(lblTitulo);
            Name = "FrmCruces";
            Text = "Cruces automáticos";
            ((System.ComponentModel.ISupportInitialize)dgvCruces).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPenalesLocal).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPenalesVisitante).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
