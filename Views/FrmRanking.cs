using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SistemaQuinielasMundialistas.Services;
using System.Linq;

namespace SistemaQuinielasMundialistas.Views
{
    public partial class FrmRanking : Form
    {
        private readonly UsuarioService usuarioService = new UsuarioService();
        public FrmRanking()
        {
            InitializeComponent();
        }

        private void btnActualizarRanking_Click(object sender, EventArgs e)
        {
            var ranking = usuarioService.ObtenerUsuarios()
                .OrderByDescending(usuario => usuario.Puntos)
                .Select((usuario, indice) => new
                {
                    Posicion = indice + 1,
                    Usuario = usuario.NombreUsuario,
                    Nombre = usuario.Nombre,
                    Pais = usuario.PaisPreferido,
                    Puntos = usuario.Puntos
                })
                .ToList();

            dgvRanking.DataSource = null;
            dgvRanking.DataSource = ranking;

            dgvRanking.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            MostrarTopScorer();
        }
        private void MostrarTopScorer()
        {
            var lider = usuarioService.ObtenerTopScorer();

            if (lider != null)
            {
                lblTopScorer.Text =
                    $"🏆 Líder: {lider.NombreUsuario} - {lider.Puntos} puntos";
            }
            else
            {
                lblTopScorer.Text = "🏆 Líder actual: Sin datos";
            }
        }
    }
    }

