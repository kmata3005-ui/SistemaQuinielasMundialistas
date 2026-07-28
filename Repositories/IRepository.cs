namespace SistemaQuinielasMundialistas.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        void SaveAll(List<T> items);
    }
}
