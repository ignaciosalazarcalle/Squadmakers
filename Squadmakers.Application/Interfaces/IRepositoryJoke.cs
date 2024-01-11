namespace Squadmakers.Application.Interfaces
{
    public interface IRepositoryJoke<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task Insert(T entity);
        void Update(T entity);
        Task<bool> Delete(Guid id);
        Task Save();
    }
}
