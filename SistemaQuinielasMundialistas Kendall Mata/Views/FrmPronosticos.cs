using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Services;

namespace SistemaQuinielasMundialistas.Views
{
    public partial class FrmPronosticos : Form
    {
        private readonly PronosticoService pronosticoService =
    new PronosticoService();

        private readonly UsuarioService usuarioService =
            new UsuarioService();

        private readonly PartidoService partidoService =
            new PartidoService();
        public FrmPronosticos()
        {
            InitializeComponent();

            CargarUsuarios();
            CargarPartidos();
            MostrarPronosticos();
        }
        private void CargarUsuarios()
        {
            cmbUsuario.DataSource = usuarioService.ObtenerUsuarios();
            cmbUsuario.DisplayMember = "NombreUsuario";
        }

        private void CargarPartidos()
        {
            cmbPartido.DataSource = null;
            cmbPartido.DataSource = partidoService.ObtenerPartidos();
            cmbPartido.DisplayMember = "NombrePartido";
            cmbPartido.ValueMember = "Id";
        }

        private void btnGuardarPronostico_Click(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedItem is not Usuario usuario)
            {
                MessageBox.Show("Debes seleccionar un usuario.");
                return;
            }

            if (cmbPartido.SelectedItem is not Partido partido)
            {
                MessageBox.Show("Debes seleccionar un partido.");
                return;
            }

            if (!partidoService.AceptaPronosticos(partido))
            {
                MessageBox.Show($"No se aceptan pronósticos porque el partido está {partido.Estado.ToLowerInvariant()}. Fecha simulada: {partidoService.ObtenerFechaSimulada():dd/MM/yyyy HH:mm}.");
                return;
            }

            Pronostico nuevoPronostico = new Pronostico
            {
                NombreUsuario = usuario.NombreUsuario,
                PartidoId = partido.Id,
                EquipoLocal = partido.EquipoLocal,
                EquipoVisitante = partido.EquipoVisitante,
                GolesLocalPronosticados = (int)nudGolesLocal.Value,
                GolesVisitantePronosticados = (int)nudGolesVisitante.Value,
                FechaRegistro = partidoService.ObtenerFechaSimulada(),
                PuntosObtenidos = 0
            };
            nuevoPronostico.PuntosObtenidos =
                 pronosticoService.CalcularPuntos(nuevoPronostico, partido);
            try
            {
                pronosticoService.AgregarPronostico(nuevoPronostico);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MostrarPronosticos();

            nudGolesLocal.Value = 0;
            nudGolesVisitante.Value = 0;

            MessageBox.Show("Pronóstico guardado correctamente.");
        }
        private void MostrarPronosticos()
        {
            var lista = pronosticoService.ObtenerPronosticos()
                .Select(pronostico => new
                {
                    Usuario = pronostico.NombreUsuario,
                    Partido = $"{pronostico.EquipoLocal} vs {pronostico.EquipoVisitante}",
                    Pronostico =
                        $"{pronostico.GolesLocalPronosticados} - {pronostico.GolesVisitantePronosticados}",
                    Fecha = pronostico.FechaRegistro.ToString("dd/MM/yyyy HH:mm"),
                    Puntos = pronostico.PuntosObtenidos
                })
                .ToList();

            dgvListaPronosticos.DataSource = null;
            dgvListaPronosticos.DataSource = lista;

            dgvListaPronosticos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}

