using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Repositories;

namespace SistemaQuinielasMundialistas.Services
{
    /// <summary>
    /// Administra el reloj simulado de la aplicación y lo conserva en JSON.
    /// </summary>
    public class FechaSimuladaService
    {
        private readonly IRepository<ConfiguracionSistema> repository =
            new JsonRepository<ConfiguracionSistema>("configuracion.json");

        private readonly ConfiguracionSistema configuracion;

        public FechaSimuladaService()
        {
            List<ConfiguracionSistema> configuraciones = repository.GetAll();
            configuracion = configuraciones.FirstOrDefault() ?? new ConfiguracionSistema();

            if (configuraciones.Count == 0)
            {
                Guardar();
            }
        }

        public DateTime ObtenerFecha() => configuracion.FechaSimulada;

        public void EstablecerFecha(DateTime fecha)
        {
            configuracion.FechaSimulada = fecha;
            Guardar();
        }

        public DateTime AvanzarHoras(int horas)
        {
            EstablecerFecha(configuracion.FechaSimulada.AddHours(horas));
            return configuracion.FechaSimulada;
        }

        public DateTime AvanzarDias(int dias)
        {
            EstablecerFecha(configuracion.FechaSimulada.AddDays(dias));
            return configuracion.FechaSimulada;
        }

        public void RestablecerAFechaReal() => EstablecerFecha(DateTime.Now);

        private void Guardar() => repository.SaveAll(new List<ConfiguracionSistema> { configuracion });
    }
}
