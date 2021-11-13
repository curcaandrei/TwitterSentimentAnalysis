using Domain.Entities;

namespace Application.Persistence
{
    public interface ITweetsRepository : IAsyncRepository<Tweet>
    {
        
    }
}