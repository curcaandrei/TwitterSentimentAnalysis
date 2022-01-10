using System.Threading.Tasks;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using MongoDB.Driver;

namespace Application.Persistence
{
    public interface IRequestTweetRepository
    {
        Task<System.Collections.Generic.List<TweetDto>> ListAllAsync();
        public Task<Tweet> AddAsync(Tweet entity);
        
        public DeleteResult DeleteAsync(string id);

        public void RetrainAlgorithm(string id);
    }
}