namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmPartidos
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtEquipoLocal = new TextBox();
            txtEquipoVisitante = new TextBox();
            txtEstado = new TextBox();
            dtpFecha = new DateTimePicker();
            btnGuardarPartido = new Button();
            btnActualizarPartido = new Button();
            btnLimpiarPartido = new Button();
            btnEliminarPartido = new Button();
            dgvPartidos = new DataGridView();
            label6 = new Label();
            label7 = new Label();
            nudGolesVisitante = new NumericUpDown();
            nudGolesLocal = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudGolesVisitante).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudGolesLocal).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(289, 0);
            label1.Name = "label1";
            label1.Size = new Size(202, 25);
            label1.TabIndex = 0;
            label1.Text = "MÓDULO DE PARTIDOS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 34);
            label2.Name = "label2";
            label2.Size = new Size(117, 25);
            label2.TabIndex = 1;
            label2.Text = "Equipo Local:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 67);
            label3.Name = "label3";
            label3.Size = new Size(144, 25);
            label3.TabIndex = 2;
            label3.Text = "Equipo Visitante:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 156);
            label4.Name = "label4";
            label4.Size = new Size(61, 25);
            label4.TabIndex = 3;
            label4.Text = "Fecha:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 115);
            label5.Name = "label5";
            label5.Size = new Size(70, 25);
            label5.TabIndex = 4;
            label5.Text = "Estado:";
            // 
            // txtEquipoLocal
            // 
            txtEquipoLocal.Location = new Point(145, 30);
            txtEquipoLocal.Name = "txtEquipoLocal";
            txtEquipoLocal.Size = new Size(150, 31);
            txtEquipoLocal.TabIndex = 5;
            // 
            // txtEquipoVisitante
            // 
            txtEquipoVisitante.Location = new Point(145, 74);
            txtEquipoVisitante.Name = "txtEquipoVisitante";
            txtEquipoVisitante.Size = new Size(150, 31);
            txtEquipoVisitante.TabIndex = 6;
            // 
            // txtEstado
            // 
            txtEstado.Location = new Point(145, 115);
            txtEstado.Name = "txtEstado";
            txtEstado.Size = new Size(150, 31);
            txtEstado.TabIndex = 7;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(69, 152);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(300, 31);
            dtpFecha.TabIndex = 8;
            // 
            // btnGuardarPartido
            // 
            btnGuardarPartido.Location = new Point(125, 206);
            btnGuardarPartido.Name = "btnGuardarPartido";
            btnGuardarPartido.Size = new Size(112, 34);
            btnGuardarPartido.TabIndex = 9;
            btnGuardarPartido.Text = "Guardar";
            btnGuardarPartido.UseVisualStyleBackColor = true;
            btnGuardarPartido.Click += button1_Click;
            // 
            // btnActualizarPartido
            // 
            btnActualizarPartido.Location = new Point(379, 206);
            btnActualizarPartido.Name = "btnActualizarPartido";
            btnActualizarPartido.Size = new Size(112, 34);
            btnActualizarPartido.TabIndex = 10;
            btnActualizarPartido.Text = "Actualizar";
            btnActualizarPartido.UseVisualStyleBackColor = true;
            btnActualizarPartido.Click += btnActualizarPartido_Click;
            // 
            // btnLimpiarPartido
            // 
            btnLimpiarPartido.Location = new Point(379, 246);
            btnLimpiarPartido.Name = "btnLimpiarPartido";
            btnLimpiarPartido.Size = new Size(112, 34);
            btnLimpiarPartido.TabIndex = 11;
            btnLimpiarPartido.Text = "Limpiar";
            btnLimpiarPartido.UseVisualStyleBackColor = true;
            btnLimpiarPartido.Click += btnLimpiarPartido_Click;
            // 
            // btnEliminarPartido
            // 
            btnEliminarPartido.Location = new Point(125, 246);
            btnEliminarPartido.Name = "btnEliminarPartido";
            btnEliminarPartido.Size = new Size(112, 34);
            btnEliminarPartido.TabIndex = 12;
            btnEliminarPartido.Text = "Eliminar";
            btnEliminarPartido.UseVisualStyleBackColor = true;
            btnEliminarPartido.Click += btnEliminarPartido_Click;
            // 
            // dgvPartidos
            // 
            dgvPartidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPartidos.Location = new Point(-12, 286);
            dgvPartidos.Name = "dgvPartidos";
            dgvPartidos.RowHeadersWidth = 62;
            dgvPartidos.Size = new Size(823, 235);
            dgvPartidos.TabIndex = 13;
            dgvPartidos.CellClick += dgvPartidos_CellClick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(431, 142);
            label6.Name = "label6";
            label6.Size = new Size(105, 25);
            label6.TabIndex = 14;
            label6.Text = "Goles Local:";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(420, 175);
            label7.Name = "label7";
            label7.Size = new Size(132, 25);
            label7.TabIndex = 15;
            label7.Text = "Goles Visitante:";
            // 
            // nudGolesVisitante
            // 
            nudGolesVisitante.Location = new Point(548, 173);
            nudGolesVisitante.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            nudGolesVisitante.Name = "nudGolesVisitante";
            nudGolesVisitante.Size = new Size(180, 31);
            nudGolesVisitante.TabIndex = 16;
            // 
            // nudGolesLocal
            // 
            nudGolesLocal.Location = new Point(548, 140);
            nudGolesLocal.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            nudGolesLocal.Name = "nudGolesLocal";
            nudGolesLocal.Size = new Size(180, 31);
            nudGolesLocal.TabIndex = 17;
            // 
            // FrmPartidos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(nudGolesLocal);
            Controls.Add(nudGolesVisitante);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(dgvPartidos);
            Controls.Add(btnEliminarPartido);
            Controls.Add(btnLimpiarPartido);
            Controls.Add(btnActualizarPartido);
            Controls.Add(btnGuardarPartido);
            Controls.Add(dtpFecha);
            Controls.Add(txtEstado);
            Controls.Add(txtEquipoVisitante);
            Controls.Add(txtEquipoLocal);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmPartidos";
            Text = "FrmPartidos";
            ((System.ComponentModel.ISupportInitialize)dgvPartidos).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudGolesVisitante).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudGolesLocal).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtEquipoLocal;
        private TextBox txtEquipoVisitante;
        private TextBox txtEstado;
        private DateTimePicker dtpFecha;
        private Button btnGuardarPartido;
        private Button btnActualizarPartido;
        private Button btnLimpiarPartido;
        private Button btnEliminarPartido;
        private DataGridView dgvPartidos;
        private Label label6;
        private Label label7;
        private NumericUpDown nudGolesVisitante;
        private NumericUpDown nudGolesLocal;
    }
}