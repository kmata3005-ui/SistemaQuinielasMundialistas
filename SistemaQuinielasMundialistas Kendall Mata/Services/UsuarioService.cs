using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Repositories;

namespace SistemaQuinielasMundialistas.Services
{
    public class UsuarioService
    {
        private readonly IRepository<Usuario> repository = new JsonRepository<Usuario>("usuarios.json");
        private readonly List<Usuario> usuarios;

        public UsuarioService() => usuarios = repository.GetAll();

        public List<Usuario> ObtenerUsuarios() => usuarios;

        public void AgregarUsuario(Usuario usuario)
        {
            Validar(usuario, null);
            usuario.Id = usuarios.Count == 0 ? 1 : usuarios.Max(u => u.Id) + 1;
            usuarios.Add(usuario);
            GuardarEnJson();
        }

        public void EliminarUsuario(Usuario usuario)
        {
            usuarios.Remove(usuario);
            GuardarEnJson();
        }

        public void ActualizarUsuario(Usuario original, Usuario actualizado)
        {
            Validar(actualizado, original.Id);
            original.Nombre = actualizado.Nombre;
            original.Correo = actualizado.Correo;
            original.NombreUsuario = actualizado.NombreUsuario;
            original.Contrasena = actualizado.Contrasena;
            original.PaisPreferido = actualizado.PaisPreferido;
            GuardarEnJson();
        }

        private void Validar(Usuario usuario, int? idActual)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre) ||
                string.IsNullOrWhiteSpace(usuario.Correo) ||
                string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new ArgumentException("Nombre, correo y nombre de usuario son obligatorios.");

            if (!usuario.Correo.Contains('@'))
                throw new ArgumentException("El correo no tiene un formato válido.");

            if (usuarios.Any(u => u.Id != idActual &&
                u.NombreUsuario.Equals(usuario.NombreUsuario, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("El nombre de usuario ya existe.");
        }

        public void GuardarEnJson() => repository.SaveAll(usuarios);

        public void RecalcularPuntosUsuarios(List<Pronostico> pronosticos)
        {
            foreach (Usuario usuario in usuarios)
            {
                usuario.Puntos = pronosticos
                    .Where(p => p.NombreUsuario.Equals(usuario.NombreUsuario, StringComparison.OrdinalIgnoreCase))
                    .Sum(p => p.PuntosObtenidos);
            }
            var insigniaService = new InsigniaService();
            insigniaService.EvaluarYAsignar(usuarios, pronosticos);
            GuardarEnJson();
        }

        public Usuario? ObtenerTopScorer() => usuarios.OrderByDescending(u => u.Puntos).FirstOrDefault();
    }
}
