using System.Collections.Generic;
using System.Linq;

namespace BootcampDay3Generic.Repositories
{
    // Simple in-memory implementation of IRepository
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _items = new();
        private int _nextId = 1;

        public IEnumerable<T> GetAll() => _items;

        public T GetById(int id)
        {
            var prop = typeof(T).GetProperty("Id");
            return _items.FirstOrDefault(item => (int)prop.GetValue(item) == id);
        }

        public void Add(T entity)
        {
            var prop = typeof(T).GetProperty("Id");
            prop?.SetValue(entity, _nextId++);
            _items.Add(entity);
        }
    }
}
