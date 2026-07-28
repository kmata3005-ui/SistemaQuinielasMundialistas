using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Repositories;

namespace SistemaQuinielasMundialistas.Services
{
    public class QuinielaService
    {
        private readonly IRepository<Quiniela> repository = new JsonRepository<Quiniela>("quinielas.json");
        private readonly List<Quiniela> quinielas;
        public QuinielaService() => quinielas = repository.GetAll();
        public List<Quiniela> ObtenerQuinielas() => quinielas;
        public void AgregarQuiniela(Quiniela quiniela)
        {
            Validar(quiniela);
            quiniela.Id = quinielas.Count == 0 ? 1 : quinielas.Max(q => q.Id) + 1;
            quinielas.Add(quiniela); Guardar();
        }
        public void EliminarQuiniela(Quiniela quiniela) { quinielas.Remove(quiniela); Guardar(); }
        public void ActualizarQuiniela(Quiniela original, Quiniela actualizada)
        {
            Validar(actualizada);
            original.Nombre = actualizada.Nombre;
            original.Descripcion = actualizada.Descripcion;
            original.EsPrivada = actualizada.EsPrivada;
            Guardar();
        }
        public void AgregarParticipante(Quiniela quiniela, Usuario usuario)
        {
            if (!quiniela.ParticipanteIds.Contains(usuario.Id)) quiniela.ParticipanteIds.Add(usuario.Id);
            quiniela.Timeline.Add($"{DateTime.Now:dd/MM/yyyy HH:mm}: {usuario.NombreUsuario} se unió a la quiniela.");
            Guardar();
        }
        private static void Validar(Quiniela q)
        {
            if (string.IsNullOrWhiteSpace(q.Nombre)) throw new ArgumentException("El nombre de la quiniela es obligatorio.");
        }
        private void Guardar() => repository.SaveAll(quinielas);
    }
}
