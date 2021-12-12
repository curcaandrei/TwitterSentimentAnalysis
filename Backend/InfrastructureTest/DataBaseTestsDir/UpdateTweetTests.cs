using System.Collections.Generic;
using Domain.Dtos;
using Domain.Entities;
using MongoDB.Bson;
using Xunit;

namespace InfrastructureTest.DataBaseTestsDir
{
    public class UpdateTweetTests : DataBaseTests
    {
        
        [Fact]
        public async void UpdateTweetTest()
        {
            _tweet.Id = new ObjectId();
            await _repository.AddAsync(_tweet);
            _repository.UpdateAsync(_tweet.Id.ToString(), new Dictionary<string, float> {{"happy", 100}});
            _tweet.feels = new Dictionary<string, float> {{"happy", 100}};
            TweetDto testTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.NotNull(testTweet.Feels);
            Dispose();
        }

        [Fact]
        public async void UpdateTweetIncorrectTest()
        {
            _tweet.Id = new ObjectId();
            await _repository.AddAsync(_tweet);
            _repository.UpdateAsync(_tweet.Id.ToString(), new Dictionary<string, float> {{"happy", 100}});
            TweetDto testTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.NotNull(testTweet);
            foreach (var (key, value) in _tweet.feels)
            {
                if (!_tweet.feels.ContainsKey(key))
                {
                    Assert.NotEqual(testTweet.Feels[key],value);
                }
                else
                {
                    Assert.True(_tweet.feels.ContainsKey(key));
                }
                
            }
            Dispose();
        }
    }
}