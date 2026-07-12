namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmQuinielas
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
            txtNombreQuiniela = new TextBox();
            txtDescripcion = new TextBox();
            btnGuardarQuiniela = new Button();
            btnActualizarQuiniela = new Button();
            btnEliminarQuiniela = new Button();
            btnLimpiarQuiniela = new Button();
            dgvQuinielas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvQuinielas).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(280, 9);
            label1.Name = "label1";
            label1.Size = new Size(209, 25);
            label1.TabIndex = 0;
            label1.Text = "MÓDULO DE QUINIELAS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(220, 43);
            label2.Name = "label2";
            label2.Size = new Size(151, 25);
            label2.TabIndex = 1;
            label2.Text = "Nombre Quiniela:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(234, 93);
            label3.Name = "label3";
            label3.Size = new Size(108, 25);
            label3.TabIndex = 2;
            label3.Text = "Descripción:";
            // 
            // txtNombreQuiniela
            // 
            txtNombreQuiniela.Location = new Point(372, 46);
            txtNombreQuiniela.Name = "txtNombreQuiniela";
            txtNombreQuiniela.Size = new Size(150, 31);
            txtNombreQuiniela.TabIndex = 3;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(372, 90);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(150, 31);
            txtDescripcion.TabIndex = 4;
            // 
            // btnGuardarQuiniela
            // 
            btnGuardarQuiniela.Location = new Point(234, 145);
            btnGuardarQuiniela.Name = "btnGuardarQuiniela";
            btnGuardarQuiniela.Size = new Size(112, 34);
            btnGuardarQuiniela.TabIndex = 5;
            btnGuardarQuiniela.Text = "Guardar";
            btnGuardarQuiniela.UseVisualStyleBackColor = true;
            btnGuardarQuiniela.Click += btnGuardarQuiniela_Click;
            // 
            // btnActualizarQuiniela
            // 
            btnActualizarQuiniela.Location = new Point(410, 145);
            btnActualizarQuiniela.Name = "btnActualizarQuiniela";
            btnActualizarQuiniela.Size = new Size(112, 34);
            btnActualizarQuiniela.TabIndex = 6;
            btnActualizarQuiniela.Text = "Actualizar";
            btnActualizarQuiniela.UseVisualStyleBackColor = true;
            btnActualizarQuiniela.Click += btnActualizarQuiniela_Click;
            // 
            // btnEliminarQuiniela
            // 
            btnEliminarQuiniela.Location = new Point(234, 202);
            btnEliminarQuiniela.Name = "btnEliminarQuiniela";
            btnEliminarQuiniela.Size = new Size(112, 34);
            btnEliminarQuiniela.TabIndex = 7;
            btnEliminarQuiniela.Text = "Eliminar";
            btnEliminarQuiniela.UseVisualStyleBackColor = true;
            btnEliminarQuiniela.Click += btnEliminarQuiniela_Click;
            // 
            // btnLimpiarQuiniela
            // 
            btnLimpiarQuiniela.Location = new Point(410, 202);
            btnLimpiarQuiniela.Name = "btnLimpiarQuiniela";
            btnLimpiarQuiniela.Size = new Size(112, 34);
            btnLimpiarQuiniela.TabIndex = 8;
            btnLimpiarQuiniela.Text = "Limpiar";
            btnLimpiarQuiniela.UseVisualStyleBackColor = true;
            btnLimpiarQuiniela.Click += btnLimpiarQuiniela_Click;
            // 
            // dgvQuinielas
            // 
            dgvQuinielas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvQuinielas.Location = new Point(-14, 242);
            dgvQuinielas.Name = "dgvQuinielas";
            dgvQuinielas.RowHeadersWidth = 62;
            dgvQuinielas.Size = new Size(832, 264);
            dgvQuinielas.TabIndex = 9;
            dgvQuinielas.CellClick += dgvQuinielas_CellClick;
            dgvQuinielas.CellContentClick += dgvQuinielas_CellContentClick;
            // 
            // FrmQuinielas
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvQuinielas);
            Controls.Add(btnLimpiarQuiniela);
            Controls.Add(btnEliminarQuiniela);
            Controls.Add(btnActualizarQuiniela);
            Controls.Add(btnGuardarQuiniela);
            Controls.Add(txtDescripcion);
            Controls.Add(txtNombreQuiniela);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmQuinielas";
            Text = "Módulo de Quinielas";
            ((System.ComponentModel.ISupportInitialize)dgvQuinielas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtNombreQuiniela;
        private TextBox txtDescripcion;
        private Button btnGuardarQuiniela;
        private Button btnActualizarQuiniela;
        private Button btnEliminarQuiniela;
        private Button btnLimpiarQuiniela;
        private DataGridView dgvQuinielas;
    }
}