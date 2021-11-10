using System.Collections;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IRepository<T>
    {
        void CreateOrUpdate(T obj);

        void Delete(string id);

        public T SelectById(string id);

        public List<T> SelectAll();
    }
}