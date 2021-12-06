using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;
using MongoDB.Bson;

namespace Application.Persistence
{
    public interface ITweetsRepository : IAsyncRepository<Tweet>
    {
        Task<TweetDTO> GetByIdAsync(ObjectId id);
    }
}