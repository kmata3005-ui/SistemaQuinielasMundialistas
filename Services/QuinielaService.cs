using SistemaQuinielasMundialistas.Models;
using System.Collections.Generic;

namespace SistemaQuinielasMundialistas.Services
{
    public class QuinielaService
    {
        private List<Quiniela> quinielas = new List<Quiniela>();

        public List<Quiniela> ObtenerQuinielas()
        {
            return quinielas;
        }

        public void AgregarQuiniela(Quiniela quiniela)
        {
            quiniela.Id = quinielas.Count + 1;
            quinielas.Add(quiniela);
        }

        public void EliminarQuiniela(Quiniela quiniela)
        {
            quinielas.Remove(quiniela);
        }

        public void ActualizarQuiniela(Quiniela original, Quiniela actualizada)
        {
            original.Nombre = actualizada.Nombre;
            original.EsPrivada = actualizada.EsPrivada;
        }
    }
}