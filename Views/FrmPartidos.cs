using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Services;

namespace SistemaQuinielasMundialistas.Views
{
    public partial class FrmPartidos : Form
    {
        private readonly PartidoService partidoService = new PartidoService();
        private readonly PronosticoService pronosticoService = new PronosticoService();
        private readonly UsuarioService usuarioService = new UsuarioService();

        private readonly DateTimePicker dtpFechaSimulada = new();
        private readonly Label lblFechaSimulada = new();
        private readonly TextBox txtAnotadores = new();

        public FrmPartidos()
        {
            InitializeComponent();
            ConfigurarPanelFechaSimulada();
            ConfigurarCampoAnotadores();
            txtEstado.ReadOnly = true;
            txtEstado.Text = "Automático";
            MostrarPartidos();
        }

        private void ConfigurarPanelFechaSimulada()
        {
            const int altoPanel = 72;

            foreach (Control control in Controls.Cast<Control>().ToList())
            {
                control.Top += altoPanel;
            }

            ClientSize = new Size(ClientSize.Width, ClientSize.Height + altoPanel);

            Panel panel = new()
            {
                Dock = DockStyle.Top,
                Height = altoPanel,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label titulo = new()
            {
                AutoSize = true,
                Text = "Fecha y hora simulada:",
                Location = new Point(12, 11),
                Font = new Font(Font, FontStyle.Bold)
            };

            dtpFechaSimulada.Format = DateTimePickerFormat.Custom;
            dtpFechaSimulada.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFechaSimulada.ShowUpDown = true;
            dtpFechaSimulada.Width = 190;
            dtpFechaSimulada.Location = new Point(190, 7);
            dtpFechaSimulada.Value = AjustarRango(partidoService.ObtenerFechaSimulada());

            Button btnAplicar = CrearBoton("Aplicar", 390, 6, (_, _) => AplicarFechaSeleccionada());
            Button btnHora = CrearBoton("+1 hora", 490, 6, (_, _) => AvanzarHoras(1));
            Button btnDia = CrearBoton("+1 día", 590, 6, (_, _) => AvanzarDias(1));
            Button btnAhora = CrearBoton("Fecha real", 680, 6, (_, _) => RestablecerFechaReal());
            btnAhora.Width = 105;

            lblFechaSimulada.AutoSize = true;
            lblFechaSimulada.Location = new Point(12, 42);
            lblFechaSimulada.Text = ObtenerTextoFechaActual();

            panel.Controls.AddRange(new Control[]
            {
                titulo, dtpFechaSimulada, btnAplicar, btnHora, btnDia, btnAhora, lblFechaSimulada
            });

            Controls.Add(panel);
            panel.BringToFront();
        }

        private static Button CrearBoton(string texto, int x, int y, EventHandler evento)
        {
            Button boton = new()
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(90, 32)
            };
            boton.Click += evento;
            return boton;
        }


        private void ConfigurarCampoAnotadores()
        {
            Label lbl = new()
            {
                AutoSize = true,
                Text = "Anotadores:",
                Location = new Point(405, 121)
            };
            txtAnotadores.Multiline = true;
            txtAnotadores.ScrollBars = ScrollBars.Vertical;
            txtAnotadores.Location = new Point(539, 117);
            txtAnotadores.Size = new Size(245, 88);
            txtAnotadores.PlaceholderText = "Ej: Campbell 15', Ugalde 72'";
            Controls.Add(lbl);
            Controls.Add(txtAnotadores);
            txtAnotadores.BringToFront();
        }

        private void AplicarFechaSeleccionada()
        {
            partidoService.EstablecerFechaSimulada(dtpFechaSimulada.Value);
            SincronizarEstadosYPuntos();
        }

        private void AvanzarHoras(int horas)
        {
            partidoService.AvanzarHoras(horas);
            SincronizarEstadosYPuntos();
        }

        private void AvanzarDias(int dias)
        {
            partidoService.AvanzarDias(dias);
            SincronizarEstadosYPuntos();
        }

        private void RestablecerFechaReal()
        {
            partidoService.RestablecerFechaReal();
            SincronizarEstadosYPuntos();
        }

        private void SincronizarEstadosYPuntos()
        {
            partidoService.ActualizarEstadosAutomaticos();

            foreach (Partido partido in partidoService.ObtenerPartidos())
            {
                pronosticoService.RecalcularPronosticosDelPartido(partido);
            }

            usuarioService.RecalcularPuntosUsuarios(pronosticoService.ObtenerPronosticos());
            dtpFechaSimulada.Value = AjustarRango(partidoService.ObtenerFechaSimulada());
            lblFechaSimulada.Text = ObtenerTextoFechaActual();
            MostrarPartidos();
        }

        private string ObtenerTextoFechaActual() =>
            $"Reloj activo: {partidoService.ObtenerFechaSimulada():dd/MM/yyyy HH:mm} | " +
            "Duración simulada del partido: 2 horas";

        private DateTime AjustarRango(DateTime fecha)
        {
            if (fecha < dtpFechaSimulada.MinDate) return dtpFechaSimulada.MinDate;
            if (fecha > dtpFechaSimulada.MaxDate) return dtpFechaSimulada.MaxDate;
            return fecha;
        }

        private void MostrarPartidos()
        {
            partidoService.ActualizarEstadosAutomaticos();
            dgvPartidos.DataSource = null;
            dgvPartidos.DataSource = partidoService.ObtenerPartidos();
            dgvPartidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEquipoLocal.Text) ||
                string.IsNullOrWhiteSpace(txtEquipoVisitante.Text))
            {
                MessageBox.Show("Complete los equipos del partido.");
                return;
            }

            Partido partido = new()
            {
                EquipoLocal = txtEquipoLocal.Text.Trim(),
                EquipoVisitante = txtEquipoVisitante.Text.Trim(),
                FechaHora = dtpFecha.Value,
                GolesLocal = (int)nudGolesLocal.Value,
                GolesVisitante = (int)nudGolesVisitante.Value,
                Anotadores = txtAnotadores.Text.Trim()
            };

            try
            {
                partidoService.AgregarPartido(partido);
                MostrarPartidos();
                LimpiarCampos();
                MessageBox.Show("Partido guardado correctamente. El estado fue calculado automáticamente.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvPartidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Partido partidoSeleccionado =
                (Partido)dgvPartidos.Rows[e.RowIndex].DataBoundItem;

            txtEquipoLocal.Text = partidoSeleccionado.EquipoLocal;
            txtEquipoVisitante.Text = partidoSeleccionado.EquipoVisitante;
            dtpFecha.Value = partidoSeleccionado.FechaHora;
            txtEstado.Text = partidoSeleccionado.Estado;
            nudGolesLocal.Value = partidoSeleccionado.GolesLocal;
            nudGolesVisitante.Value = partidoSeleccionado.GolesVisitante;
            txtAnotadores.Text = partidoSeleccionado.Anotadores;
        }

        private void btnActualizarPartido_Click(object sender, EventArgs e)
        {
            if (dgvPartidos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un partido.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEquipoLocal.Text) ||
                string.IsNullOrWhiteSpace(txtEquipoVisitante.Text))
            {
                MessageBox.Show("Complete los equipos del partido.");
                return;
            }

            Partido partidoSeleccionado =
                (Partido)dgvPartidos.CurrentRow.DataBoundItem;

            Partido partidoActualizado = new()
            {
                Id = partidoSeleccionado.Id,
                EquipoLocal = txtEquipoLocal.Text.Trim(),
                EquipoVisitante = txtEquipoVisitante.Text.Trim(),
                FechaHora = dtpFecha.Value,
                GolesLocal = (int)nudGolesLocal.Value,
                GolesVisitante = (int)nudGolesVisitante.Value,
                Anotadores = txtAnotadores.Text.Trim(),
                Grupo = partidoSeleccionado.Grupo,
                Fase = partidoSeleccionado.Fase,
                NumeroCruce = partidoSeleccionado.NumeroCruce,
                FueAPenales = partidoSeleccionado.FueAPenales,
                GolesPenalesLocal = partidoSeleccionado.GolesPenalesLocal,
                GolesPenalesVisitante = partidoSeleccionado.GolesPenalesVisitante
            };

            try
            {
                partidoService.ActualizarPartido(partidoSeleccionado, partidoActualizado);
                pronosticoService.RecalcularPronosticosDelPartido(partidoSeleccionado);
                usuarioService.RecalcularPuntosUsuarios(pronosticoService.ObtenerPronosticos());
                MostrarPartidos();

                MessageBox.Show("Partido, estado automático, pronósticos y ranking actualizados correctamente.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminarPartido_Click(object sender, EventArgs e)
        {
            if (dgvPartidos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un partido.");
                return;
            }

            Partido partidoSeleccionado =
                (Partido)dgvPartidos.CurrentRow.DataBoundItem;

            partidoService.EliminarPartido(partidoSeleccionado);
            MostrarPartidos();
            MessageBox.Show("Partido eliminado correctamente.");
        }

        private void btnLimpiarPartido_Click(object sender, EventArgs e) => LimpiarCampos();

        private void LimpiarCampos()
        {
            txtEquipoLocal.Clear();
            txtEquipoVisitante.Clear();
            txtEstado.Text = "Automático";
            nudGolesLocal.Value = 0;
            nudGolesVisitante.Value = 0;
            txtAnotadores.Clear();
            dtpFecha.Value = AjustarRango(partidoService.ObtenerFechaSimulada());
            txtEquipoLocal.Focus();
        }

    }
}
