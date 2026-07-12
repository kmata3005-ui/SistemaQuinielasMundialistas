namespace SistemaQuinielasMundialistas
{
    partial class FrmPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMenu = new Panel();
            btnEstadisticas = new Button();
            btnRanking = new Button();
            btnPartidos = new Button();
            btnQuinielas = new Button();
            btnUsuarios = new Button();
            panelContenido = new Panel();
            label1 = new Label();
            panelMenu.SuspendLayout();
            panelContenido.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = SystemColors.ControlDarkDark;
            panelMenu.Controls.Add(btnEstadisticas);
            panelMenu.Controls.Add(btnRanking);
            panelMenu.Controls.Add(btnPartidos);
            panelMenu.Controls.Add(btnQuinielas);
            panelMenu.Controls.Add(btnUsuarios);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(200, 450);
            panelMenu.TabIndex = 0;
            // 
            // btnEstadisticas
            // 
            btnEstadisticas.Location = new Point(34, 178);
            btnEstadisticas.Name = "btnEstadisticas";
            btnEstadisticas.Size = new Size(112, 34);
            btnEstadisticas.TabIndex = 5;
            btnEstadisticas.Text = "Estadísticas";
            btnEstadisticas.UseVisualStyleBackColor = true;
            btnEstadisticas.Click += btnEstadisticas_Click;
            // 
            // btnRanking
            // 
            btnRanking.Location = new Point(34, 138);
            btnRanking.Name = "btnRanking";
            btnRanking.Size = new Size(112, 34);
            btnRanking.TabIndex = 4;
            btnRanking.Text = "Ranking";
            btnRanking.UseVisualStyleBackColor = true;
            btnRanking.Click += btnRanking_Click;
            // 
            // btnPartidos
            // 
            btnPartidos.Location = new Point(34, 98);
            btnPartidos.Name = "btnPartidos";
            btnPartidos.Size = new Size(112, 34);
            btnPartidos.TabIndex = 3;
            btnPartidos.Text = "Partidos";
            btnPartidos.UseVisualStyleBackColor = true;
            btnPartidos.Click += btnPartidos_Click;
            // 
            // btnQuinielas
            // 
            btnQuinielas.Location = new Point(34, 58);
            btnQuinielas.Name = "btnQuinielas";
            btnQuinielas.Size = new Size(112, 34);
            btnQuinielas.TabIndex = 2;
            btnQuinielas.Text = " Quinielas";
            btnQuinielas.UseVisualStyleBackColor = true;
            btnQuinielas.Click += btnQuinielas_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.Location = new Point(3, 12);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(180, 40);
            btnUsuarios.TabIndex = 1;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.UseVisualStyleBackColor = true;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // panelContenido
            // 
            panelContenido.Controls.Add(label1);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(200, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(600, 450);
            panelContenido.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(212, 0);
            label1.Name = "label1";
            label1.Size = new Size(173, 25);
            label1.TabIndex = 0;
            label1.Text = "Módulo de Usuarios";
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            Name = "FrmPrincipal";
            Text = "Sistema de Quinielas Mundialistas";
            panelMenu.ResumeLayout(false);
            panelContenido.ResumeLayout(false);
            panelContenido.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Button btnUsuarios;
        private Button btnPartidos;
        private Button btnQuinielas;
        private Button btnEstadisticas;
        private Button btnRanking;
        private Panel panelContenido;
        private Label label1;
    }
}
