namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmPronosticos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            cmbUsuario = new ComboBox();
            label2 = new Label();
            cmbPartido = new ComboBox();
            label3 = new Label();
            nudGolesLocal = new NumericUpDown();
            label4 = new Label();
            nudGolesVisitante = new NumericUpDown();
            btnGuardarPronostico = new Button();
            dgvListaPronosticos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)nudGolesLocal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudGolesVisitante).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvListaPronosticos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 9);
            label1.Name = "label1";
            label1.Size = new Size(76, 25);
            label1.TabIndex = 0;
            label1.Text = "Usuario:";
            // 
            // cmbUsuario
            // 
            cmbUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUsuario.FormattingEnabled = true;
            cmbUsuario.Location = new Point(117, 12);
            cmbUsuario.Name = "cmbUsuario";
            cmbUsuario.Size = new Size(182, 33);
            cmbUsuario.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 58);
            label2.Name = "label2";
            label2.Size = new Size(72, 25);
            label2.TabIndex = 2;
            label2.Text = "Partido:";
            // 
            // cmbPartido
            // 
            cmbPartido.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPartido.FormattingEnabled = true;
            cmbPartido.Location = new Point(117, 58);
            cmbPartido.Name = "cmbPartido";
            cmbPartido.Size = new Size(182, 33);
            cmbPartido.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 117);
            label3.Name = "label3";
            label3.Size = new Size(101, 25);
            label3.TabIndex = 4;
            label3.Text = "Goles local:";
            // 
            // nudGolesLocal
            // 
            nudGolesLocal.Location = new Point(140, 117);
            nudGolesLocal.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudGolesLocal.Name = "nudGolesLocal";
            nudGolesLocal.Size = new Size(180, 31);
            nudGolesLocal.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 175);
            label4.Name = "label4";
            label4.Size = new Size(130, 25);
            label4.TabIndex = 6;
            label4.Text = "Goles visitante:";
            // 
            // nudGolesVisitante
            // 
            nudGolesVisitante.Location = new Point(140, 173);
            nudGolesVisitante.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudGolesVisitante.Name = "nudGolesVisitante";
            nudGolesVisitante.Size = new Size(180, 31);
            nudGolesVisitante.TabIndex = 7;
            // 
            // btnGuardarPronostico
            // 
            btnGuardarPronostico.Location = new Point(297, 216);
            btnGuardarPronostico.Name = "btnGuardarPronostico";
            btnGuardarPronostico.Size = new Size(180, 50);
            btnGuardarPronostico.TabIndex = 8;
            btnGuardarPronostico.Text = "Guardar Pronóstico";
            btnGuardarPronostico.UseVisualStyleBackColor = true;
            btnGuardarPronostico.Click += btnGuardarPronostico_Click;
            // 
            // dgvListaPronosticos
            // 
            dgvListaPronosticos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListaPronosticos.Location = new Point(-15, 272);
            dgvListaPronosticos.Name = "dgvListaPronosticos";
            dgvListaPronosticos.RowHeadersWidth = 62;
            dgvListaPronosticos.Size = new Size(824, 229);
            dgvListaPronosticos.TabIndex = 9;
            // 
            // FrmPronosticos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvListaPronosticos);
            Controls.Add(btnGuardarPronostico);
            Controls.Add(nudGolesVisitante);
            Controls.Add(label4);
            Controls.Add(nudGolesLocal);
            Controls.Add(label3);
            Controls.Add(cmbPartido);
            Controls.Add(label2);
            Controls.Add(cmbUsuario);
            Controls.Add(label1);
            Name = "FrmPronosticos";
            Text = "FrmPronosticos";
            ((System.ComponentModel.ISupportInitialize)nudGolesLocal).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudGolesVisitante).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvListaPronosticos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbUsuario;
        private Label label2;
        private ComboBox cmbPartido;
        private Label label3;
        private NumericUpDown nudGolesLocal;
        private Label label4;
        private NumericUpDown nudGolesVisitante;
        private Button btnGuardarPronostico;
        private DataGridView dgvListaPronosticos;
    }
}