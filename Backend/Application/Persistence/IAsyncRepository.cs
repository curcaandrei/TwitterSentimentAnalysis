using System.Threading.Tasks;
using Domain.Dtos;
using MongoDB.Driver;

namespace Application.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<System.Collections.Generic.List<TweetDto>> ListAllAsync(int pageNr);

        Task<T> AddAsync(T entity);

        UpdateResult UpdateAsync(string id, System.Collections.Generic.Dictionary<string, float> feels);

        DeleteResult DeleteAsync(string id);
    }
}