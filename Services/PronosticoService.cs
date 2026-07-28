using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Repositories;

namespace SistemaQuinielasMundialistas.Services
{
    public class PronosticoService
    {
        private readonly IRepository<Pronostico> repository = new JsonRepository<Pronostico>("pronosticos.json");
        private readonly List<Pronostico> pronosticos;
        public PronosticoService() => pronosticos = repository.GetAll();
        public List<Pronostico> ObtenerPronosticos() => pronosticos;

        public void AgregarPronostico(Pronostico pronostico)
        {
            if (pronosticos.Any(p => p.PartidoId == pronostico.PartidoId &&
                p.NombreUsuario.Equals(pronostico.NombreUsuario, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Ese usuario ya registró un pronóstico para este partido.");
            pronostico.Id = pronosticos.Count == 0 ? 1 : pronosticos.Max(p => p.Id) + 1;
            pronosticos.Add(pronostico);
            Guardar();
        }
        public void EliminarPronostico(Pronostico pronostico) { pronosticos.Remove(pronostico); Guardar(); }
        public int CalcularPuntos(Pronostico p, Partido partido)
        {
            if (!PartidoService.NormalizarEstado(partido.Estado).Equals("Finalizado")) return 0;
            if (p.GolesLocalPronosticados == partido.GolesLocal && p.GolesVisitantePronosticados == partido.GolesVisitante) return 5;
            return CompararResultado(p.GolesLocalPronosticados, p.GolesVisitantePronosticados) ==
                   CompararResultado(partido.GolesLocal, partido.GolesVisitante) ? 2 : 0;
        }
        private static int CompararResultado(int local, int visita) => local.CompareTo(visita);
        public void RecalcularPronosticosDelPartido(Partido partido)
        {
            foreach (Pronostico p in pronosticos.Where(p => p.PartidoId == partido.Id))
                p.PuntosObtenidos = CalcularPuntos(p, partido);
            Guardar();
        }
        private void Guardar() => repository.SaveAll(pronosticos);
    }
}
