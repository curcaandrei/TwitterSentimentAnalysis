using System.Collections;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T obj);
        void Update(T obj);
        
        void Delete(string id);

        public T SelectById(string id);

        public IEnumerable<T> SelectAll();
    }
}