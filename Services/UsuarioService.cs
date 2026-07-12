using SistemaQuinielasMundialistas.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace SistemaQuinielasMundialistas.Services
{
    public class UsuarioService
    {
        private List<Usuario> usuarios = new List<Usuario>();
        private string rutaArchivo = "usuarios.json";
        public UsuarioService()
        {
            CargarDesdeJson();
        }
        public List<Usuario> ObtenerUsuarios()
        {
            return usuarios;
        }
        public void AgregarUsuario(Usuario usuario)
        {
            usuario.Id = usuarios.Count + 1;

            usuarios.Add(usuario);
            GuardarEnJson();
        }

        public void EliminarUsuario(Usuario usuario)
        {
            usuarios.Remove(usuario);
            GuardarEnJson();
        }
        public void ActualizarUsuario(Usuario usuarioOriginal, Usuario usuarioActualizado)
        {
            usuarioOriginal.Nombre = usuarioActualizado.Nombre;
            usuarioOriginal.Correo = usuarioActualizado.Correo;
            usuarioOriginal.NombreUsuario = usuarioActualizado.NombreUsuario;
            usuarioOriginal.Contrasena = usuarioActualizado.Contrasena;

            GuardarEnJson();
        }
        public void GuardarEnJson()
        {
            string json = JsonSerializer.Serialize(usuarios,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(rutaArchivo, json);
        }

        public void CargarDesdeJson()
        {
            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);

                usuarios = JsonSerializer.Deserialize<List<Usuario>>(json)
                           ?? new List<Usuario>();
            }
        }
    }
    }