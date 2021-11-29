using System.Threading.Tasks;
using Domain.Entities;
using MongoDB.Bson;

namespace Application.Persistence
{
    public interface ITweetsRepository : IAsyncRepository<Tweet>
    {
        Task<Tweet> GetByIdAsync(ObjectId id);
    }
}