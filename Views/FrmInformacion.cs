using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Services;

namespace SistemaQuinielasMundialistas.Views
{
    public partial class FrmInformacion : Form
    {
        private readonly UsuarioService usuarioService = new();
        private readonly QuinielaService quinielaService = new();
        private readonly PronosticoService pronosticoService = new();
        private readonly PartidoService partidoService = new();
        private bool cargandoFiltros;

        public FrmInformacion()
        {
            InitializeComponent();
            CargarFiltros();
            ActualizarTodo();
        }

        private void CargarFiltros()
        {
            cargandoFiltros = true;
            try
            {
            cboUsuario.DataSource = usuarioService.ObtenerUsuarios()
                .OrderBy(u => u.NombreUsuario)
                .ToList();
            cboUsuario.DisplayMember = nameof(Usuario.NombreUsuario);

            cboQuiniela.DataSource = quinielaService.ObtenerQuinielas()
                .OrderBy(q => q.Nombre)
                .ToList();
            cboQuiniela.DisplayMember = nameof(Quiniela.Nombre);
            }
            finally
            {
                cargandoFiltros = false;
            }
        }

        private void ActualizarTodo()
        {
            MostrarHistorial();
            MostrarUltimosCinco();
            MostrarProximos24Horas();
            MostrarRankingGlobal();
            MostrarRankingPrivado();
            ConfigurarColumnasBandera();
            lblFechaSimulada.Text = $"Fecha simulada: {partidoService.ObtenerFechaSimulada():dd/MM/yyyy HH:mm}";
        }

        private void MostrarHistorial()
        {
            if (cboUsuario.SelectedItem is not Usuario usuario)
            {
                dgvHistorial.DataSource = null;
                return;
            }

            dgvHistorial.DataSource = pronosticoService.ObtenerPronosticos()
                .Where(p => p.NombreUsuario.Equals(usuario.NombreUsuario, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(p => p.FechaRegistro)
                .Select(p => new
                {
                    Fecha = p.FechaRegistro.ToString("dd/MM/yyyy HH:mm"),
                    Partido = $"{p.EquipoLocal} vs {p.EquipoVisitante}",
                    Pronostico = $"{p.GolesLocalPronosticados} - {p.GolesVisitantePronosticados}",
                    Puntos = p.PuntosObtenidos
                })
                .ToList();
        }

        private void MostrarUltimosCinco()
        {
            partidoService.ActualizarEstadosAutomaticos();
            DateTime ahora = partidoService.ObtenerFechaSimulada();

            dgvUltimos.DataSource = partidoService.ObtenerPartidos()
                .Where(p => PartidoService.NormalizarEstado(p.Estado) == "Finalizado" && p.FechaHora <= ahora)
                .OrderByDescending(p => p.FechaHora)
                .Take(5)
                .Select(p => new
                {
                    Fecha = p.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                    Partido = p.NombrePartido,
                    Resultado = $"{p.GolesLocal} - {p.GolesVisitante}",
                    p.Fase,
                    p.Grupo,
                    p.Anotadores
                })
                .ToList();
        }

        private void MostrarProximos24Horas()
        {
            partidoService.ActualizarEstadosAutomaticos();
            DateTime desde = partidoService.ObtenerFechaSimulada();
            DateTime hasta = desde.AddHours(24);

            dgvProximos.DataSource = partidoService.ObtenerPartidos()
                .Where(p => p.FechaHora >= desde && p.FechaHora <= hasta)
                .OrderBy(p => p.FechaHora)
                .Select(p => new
                {
                    Fecha = p.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                    Partido = p.NombrePartido,
                    p.Estado,
                    p.Fase,
                    p.Grupo
                })
                .ToList();
        }

        private void MostrarRankingGlobal()
        {
            dgvRankingGlobal.DataSource = usuarioService.ObtenerUsuarios()
                .OrderByDescending(u => u.Puntos)
                .ThenBy(u => u.NombreUsuario)
                .Select((u, indice) => new
                {
                    Posicion = indice + 1,
                    Usuario = u.NombreUsuario,
                    Bandera = u.Bandera,
                    Pais = u.PaisPreferido,
                    u.Puntos
                })
                .ToList();
        }

        private void MostrarRankingPrivado()
        {
            if (cboQuiniela.SelectedItem is not Quiniela quiniela)
            {
                dgvRankingPrivado.DataSource = null;
                return;
            }

            HashSet<int> participantes = (quiniela.ParticipanteIds ?? new List<int>()).ToHashSet();
            dgvRankingPrivado.DataSource = usuarioService.ObtenerUsuarios()
                .Where(u => participantes.Contains(u.Id))
                .OrderByDescending(u => u.Puntos)
                .ThenBy(u => u.NombreUsuario)
                .Select((u, indice) => new
                {
                    Posicion = indice + 1,
                    Usuario = u.NombreUsuario,
                    Bandera = u.Bandera,
                    Pais = u.PaisPreferido,
                    u.Puntos,
                    Estado = indice == 0 ? "Líder privado" : string.Empty
                })
                .ToList();

            lblRankingPrivado.Text = $"Ranking privado: {quiniela.Nombre} | {quiniela.CantidadParticipantes} integrantes";
        }

        private void ConfigurarColumnasBandera()
        {
            ConfigurarColumnaBandera(dgvRankingGlobal);
            ConfigurarColumnaBandera(dgvRankingPrivado);
        }

        private static void ConfigurarColumnaBandera(DataGridView tabla)
        {
            // La columna se genera automáticamente al asignar el DataSource.
            // Solo configuramos el modo de imagen; no modificamos Width/Height aquí
            // porque WinForms puede lanzar NullReferenceException mientras termina
            // de crear internamente las bandas del DataGridView.
            if (tabla == null || tabla.IsDisposed || tabla.Columns.Count == 0)
                return;

            DataGridViewColumn? columnaBase = tabla.Columns
                .Cast<DataGridViewColumn>()
                .FirstOrDefault(c => string.Equals(c.Name, "Bandera", StringComparison.OrdinalIgnoreCase));

            if (columnaBase is DataGridViewImageColumn columnaImagen)
            {
                columnaImagen.ImageLayout = DataGridViewImageCellLayout.Zoom;
                columnaImagen.DefaultCellStyle.NullValue = null;
            }
        }

        private void cboUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargandoFiltros) MostrarHistorial();
        }

        private void cboQuiniela_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargandoFiltros) MostrarRankingPrivado();
        }
        private void btnActualizar_Click(object sender, EventArgs e) => ActualizarTodo();
    }
}
