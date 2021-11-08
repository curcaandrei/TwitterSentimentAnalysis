using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        
        Task<TEntity> UpdateAsync(TEntity entity);
        
        Task<TEntity> DeleteAsync(TEntity entity);
        
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        Task<TEntity> GetByIdAsync(Guid id);
    }
}