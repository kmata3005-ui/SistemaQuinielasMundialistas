namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmEstadisticas
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
            lblTotalUsuarios = new Label();
            lblTotalPartidos = new Label();
            lblTotalPronosticos = new Label();
            lblMejorUsuario = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(224, 18);
            label1.Name = "label1";
            label1.Size = new Size(234, 25);
            label1.TabIndex = 0;
            label1.Text = "MÓDULO DE ESTADÍSTICAS";
            // 
            // lblTotalUsuarios
            // 
            lblTotalUsuarios.AutoSize = true;
            lblTotalUsuarios.Location = new Point(111, 66);
            lblTotalUsuarios.Name = "lblTotalUsuarios";
            lblTotalUsuarios.Size = new Size(149, 25);
            lblTotalUsuarios.TabIndex = 1;
            lblTotalUsuarios.Text = "Total de usuarios:";
            // 
            // lblTotalPartidos
            // 
            lblTotalPartidos.AutoSize = true;
            lblTotalPartidos.Location = new Point(111, 108);
            lblTotalPartidos.Name = "lblTotalPartidos";
            lblTotalPartidos.Size = new Size(149, 25);
            lblTotalPartidos.TabIndex = 2;
            lblTotalPartidos.Text = "Total de partidos:";
            // 
            // lblTotalPronosticos
            // 
            lblTotalPronosticos.AutoSize = true;
            lblTotalPronosticos.Location = new Point(111, 146);
            lblTotalPronosticos.Name = "lblTotalPronosticos";
            lblTotalPronosticos.Size = new Size(177, 25);
            lblTotalPronosticos.TabIndex = 3;
            lblTotalPronosticos.Text = "Total de pronósticos:";
            // 
            // lblMejorUsuario
            // 
            lblMejorUsuario.AutoSize = true;
            lblMejorUsuario.Location = new Point(111, 183);
            lblMejorUsuario.Name = "lblMejorUsuario";
            lblMejorUsuario.Size = new Size(125, 25);
            lblMejorUsuario.TabIndex = 4;
            lblMejorUsuario.Text = "Mejor usuario:";
            // 
            // FrmEstadisticas
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblMejorUsuario);
            Controls.Add(lblTotalPronosticos);
            Controls.Add(lblTotalPartidos);
            Controls.Add(lblTotalUsuarios);
            Controls.Add(label1);
            Name = "FrmEstadisticas";
            Text = "FrmEstadisticas";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblTotalUsuarios;
        private Label lblTotalPartidos;
        private Label lblTotalPronosticos;
        private Label lblMejorUsuario;
    }
}