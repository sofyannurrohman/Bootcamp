using System.Collections.Generic;

namespace BootcampDay3Generic.Repositories
{
    // Generic repository interface
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
    }
}
