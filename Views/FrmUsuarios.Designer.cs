namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmUsuarios
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
            txtNombre = new TextBox();
            txtCorreo = new TextBox();
            txtUsuario = new TextBox();
            txtContrasena = new TextBox();
            btnGuardar = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            dgvUsuarios = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(259, 9);
            label1.Name = "label1";
            label1.Size = new Size(206, 25);
            label1.TabIndex = 0;
            label1.Text = "MÓDULO DE USUARIOS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(157, 50);
            label2.Name = "label2";
            label2.Size = new Size(82, 25);
            label2.TabIndex = 1;
            label2.Text = "Nombre:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(157, 98);
            label3.Name = "label3";
            label3.Size = new Size(70, 25);
            label3.TabIndex = 2;
            label3.Text = "Correo:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(157, 150);
            label4.Name = "label4";
            label4.Size = new Size(76, 25);
            label4.TabIndex = 3;
            label4.Text = "Usuario:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(152, 197);
            label5.Name = "label5";
            label5.Size = new Size(105, 25);
            label5.TabIndex = 4;
            label5.Text = "Contraseña:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(259, 50);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(150, 31);
            txtNombre.TabIndex = 5;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(259, 98);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(150, 31);
            txtCorreo.TabIndex = 6;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(259, 150);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(150, 31);
            txtUsuario.TabIndex = 7;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(259, 197);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(150, 31);
            txtContrasena.TabIndex = 8;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(176, 234);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(112, 34);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(353, 234);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(112, 34);
            btnActualizar.TabIndex = 10;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(176, 279);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(112, 31);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(353, 276);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(112, 34);
            btnLimpiar.TabIndex = 12;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(-9, 316);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.RowHeadersWidth = 62;
            dgvUsuarios.Size = new Size(815, 225);
            dgvUsuarios.TabIndex = 13;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvUsuarios);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnActualizar);
            Controls.Add(btnGuardar);
            Controls.Add(txtContrasena);
            Controls.Add(txtUsuario);
            Controls.Add(txtCorreo);
            Controls.Add(txtNombre);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmUsuarios";
            Text = "Gestión de Usuarios";
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtNombre;
        private TextBox txtCorreo;
        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnGuardar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private DataGridView dgvUsuarios;
    }
}
