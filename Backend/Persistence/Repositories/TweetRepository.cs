using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Persistence;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.Repositories
{
    public class TweetRepository : BaseRepository<Tweet>, ITweetsRepository
    {
        private readonly IMongoCollection<Tweet> _collection;
        
        public TweetRepository(IMongoDbContext dbContext) : base(dbContext)
        {
            _collection = dbContext.GetCollection<Tweet>(typeof(Tweet).Name);
        }
        
        public Task<TweetDto> GetByIdAsync(ObjectId id, bool unitTest = false)
        {
            var res = _collection.FindAsync(x => x.Id == id).Result.FirstOrDefaultAsync();
            TweetDto dto = new TweetDto();
            if (res.Result != null)
            {
                dto.Id = res.Result.Id.ToString();
                if (!unitTest)
                {
                    try
                    {
                        if (dto.Feels.Count != 2)
                        {
                            dto.Feels = PredictSentiment(dto.Text).Result;
                        }
                    }
                    catch(NullReferenceException e)
                    {
                        dto.Feels = PredictSentiment(dto.Text).Result;
                    }
                }
                dto.Text = res.Result.Text;
                dto.Username = res.Result.Username;
                dto.Date = res.Result.Date;
                dto.User = res.Result.User;
                return Task.FromResult(dto);
            }
            return Task.FromResult<TweetDto>(dto);
        }
    }
}