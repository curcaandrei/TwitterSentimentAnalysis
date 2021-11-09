using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITweetsRepository : IRepository<Tweet>
    {
        public Tweet SelectByText(string text);
    }
}