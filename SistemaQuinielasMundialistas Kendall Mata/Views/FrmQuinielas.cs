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
    public partial class FrmQuinielas : Form
    {
        private QuinielaService quinielaService = new QuinielaService();

        public FrmQuinielas()
        {
            InitializeComponent();
            MostrarQuinielas();
        }
        private void MostrarQuinielas()
        {
            dgvQuinielas.DataSource = null;
            dgvQuinielas.DataSource =
                quinielaService.ObtenerQuinielas();
        }

        private void btnGuardarQuiniela_Click(object sender, EventArgs e)
        {
            Quiniela quiniela = new Quiniela
            {
                Nombre = txtNombreQuiniela.Text,
                Descripcion = txtDescripcion.Text
            };

            quinielaService.AgregarQuiniela(quiniela);

            MostrarQuinielas();

            txtNombreQuiniela.Clear();
            txtDescripcion.Clear();

            MessageBox.Show("Quiniela guardada correctamente.");
        }

        private void btnLimpiarQuiniela_Click(object sender, EventArgs e)
        {
            txtNombreQuiniela.Clear();
            txtDescripcion.Clear();
            txtNombreQuiniela.Focus();
        }

        private void btnEliminarQuiniela_Click(object sender, EventArgs e)
        {
            if (dgvQuinielas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una quiniela.");
                return;
            }

            Quiniela quinielaSeleccionada =
                (Quiniela)dgvQuinielas.CurrentRow.DataBoundItem;

            quinielaService.EliminarQuiniela(quinielaSeleccionada);

            MostrarQuinielas();

            MessageBox.Show("Quiniela eliminada correctamente.");
        }

        private void dgvQuinielas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvQuinielas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Quiniela quinielaSeleccionada =
                (Quiniela)dgvQuinielas.Rows[e.RowIndex].DataBoundItem;

            txtNombreQuiniela.Text = quinielaSeleccionada.Nombre;
            txtDescripcion.Text = quinielaSeleccionada.Descripcion;
        }
        private void btnActualizarQuiniela_Click(object sender, EventArgs e)
        {
            if (dgvQuinielas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una quiniela.");
                return;
            }

            Quiniela quinielaOriginal =
                (Quiniela)dgvQuinielas.CurrentRow.DataBoundItem;

            Quiniela quinielaActualizada = new Quiniela
            {
                Nombre = txtNombreQuiniela.Text,
                Descripcion = txtDescripcion.Text,
                EsPrivada = quinielaOriginal.EsPrivada
            };

            quinielaService.ActualizarQuiniela(
                quinielaOriginal,
                quinielaActualizada);
            MostrarQuinielas();

            MessageBox.Show("Quiniela actualizada correctamente.");
        }
    }
    }

