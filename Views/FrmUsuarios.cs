using SistemaQuinielasMundialistas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistemaQuinielasMundialistas.Views
{
    public partial class FrmUsuarios : Form
    {
        private Services.UsuarioService usuarioService =
            new Services.UsuarioService();
        public FrmUsuarios()
        {
            InitializeComponent();
            MostrarUsuarios();
        }
        private void MostrarUsuarios()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarioService.ObtenerUsuarios();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();

            usuario.Nombre = txtNombre.Text;
            usuario.Correo = txtCorreo.Text;
            usuario.NombreUsuario = txtUsuario.Text;
            usuario.Contrasena = txtContrasena.Text;

            usuarioService.AgregarUsuario(usuario);
            MostrarUsuarios();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para eliminar.");
                return;
            }

            Usuario usuarioSeleccionado =
                (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            usuarioService.EliminarUsuario(usuarioSeleccionado);

            MostrarUsuarios();

            txtNombre.Clear();
            txtCorreo.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();

            txtNombre.Focus();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();

            txtNombre.Focus();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                return;
            }

            Usuario usuarioSeleccionado =
                (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            txtNombre.Text = usuarioSeleccionado.Nombre;
            txtCorreo.Text = usuarioSeleccionado.Correo;
            txtUsuario.Text = usuarioSeleccionado.NombreUsuario;
            txtContrasena.Text = usuarioSeleccionado.Contrasena;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para actualizar.");
                return;
            }

            Usuario usuarioSeleccionado =
                dgvUsuarios.CurrentRow.DataBoundItem as Usuario;

            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("No se pudo seleccionar el usuario.");
                return;
            }

            Usuario usuarioActualizado = new Usuario();

            usuarioActualizado.Nombre = txtNombre.Text;
            usuarioActualizado.Correo = txtCorreo.Text;
            usuarioActualizado.NombreUsuario = txtUsuario.Text;
            usuarioActualizado.Contrasena = txtContrasena.Text;

            usuarioService.ActualizarUsuario(usuarioSeleccionado, usuarioActualizado);

            MostrarUsuarios();

            MessageBox.Show("Usuario actualizado correctamente.");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}