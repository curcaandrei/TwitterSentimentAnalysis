using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Application.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(ObjectId id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}