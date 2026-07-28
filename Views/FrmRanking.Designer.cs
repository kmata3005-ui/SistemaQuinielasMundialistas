namespace SistemaQuinielasMundialistas.Views
{
    partial class FrmRanking
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
            dgvRanking = new DataGridView();
            btnActualizarRanking = new Button();
            lblTopScorer = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvRanking).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(282, -3);
            label1.Name = "label1";
            label1.Size = new Size(196, 25);
            label1.TabIndex = 0;
            label1.Text = "MÓDULO DE RANKING";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(108, 9);
            label2.Name = "label2";
            label2.Size = new Size(156, 25);
            label2.TabIndex = 1;
            label2.Text = "RANKING GLOBAL";
            // 
            // dgvRanking
            // 
            dgvRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRanking.Location = new Point(-9, 147);
            dgvRanking.Name = "dgvRanking";
            dgvRanking.RowHeadersWidth = 62;
            dgvRanking.Size = new Size(889, 303);
            dgvRanking.TabIndex = 2;
            // 
            // btnActualizarRanking
            // 
            btnActualizarRanking.AutoSize = true;
            btnActualizarRanking.Location = new Point(108, 87);
            btnActualizarRanking.Name = "btnActualizarRanking";
            btnActualizarRanking.Size = new Size(167, 35);
            btnActualizarRanking.TabIndex = 3;
            btnActualizarRanking.Text = "Actualizar Ranking";
            btnActualizarRanking.UseVisualStyleBackColor = true;
            btnActualizarRanking.Click += btnActualizarRanking_Click;
            // 
            // lblTopScorer
            // 
            lblTopScorer.AutoSize = true;
            lblTopScorer.Location = new Point(89, 46);
            lblTopScorer.Name = "lblTopScorer";
            lblTopScorer.Size = new Size(214, 25);
            lblTopScorer.TabIndex = 4;
            lblTopScorer.Text = "🏆 Líder actual: Sin datos";
            // 
            // FrmRanking
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTopScorer);
            Controls.Add(btnActualizarRanking);
            Controls.Add(dgvRanking);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmRanking";
            Text = "FrmRanking";
            ((System.ComponentModel.ISupportInitialize)dgvRanking).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private DataGridView dgvRanking;
        private Button btnActualizarRanking;
        private Label lblTopScorer;
    }
}