namespace SistemaQuinielasMundialistas.Models
{
    /// <summary>
    /// Clase base para las insignias del sistema.
    /// Las clases derivadas implementan su propia regla de evaluación,
    /// demostrando herencia y polimorfismo.
    /// </summary>
    public abstract class Insignia
    {
        public abstract string Nombre { get; }
        public abstract string Descripcion { get; }

        public abstract Usuario? ObtenerGanador(
            IReadOnlyCollection<Usuario> usuarios,
            IReadOnlyCollection<Pronostico> pronosticos);
    }
}
