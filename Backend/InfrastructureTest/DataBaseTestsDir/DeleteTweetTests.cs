using Domain.Dtos;
using Domain.Entities;
using MongoDB.Bson;
using Xunit;

namespace InfrastructureTest.DataBaseTestsDir
{
    public class DeleteTweetTests : DataBaseTests
    {
        [Fact]
        public async void DeleteTweetTest()
        {
            await _repository.AddAsync(_tweet);
            TweetDto testTweet = await _repository.GetByIdAsync(_tweet.Id, true);
            Assert.NotNull(testTweet);
            _repository.DeleteAsync(_tweet.Id.ToString());
            TweetDto deletedTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.Equal("No date",deletedTweet.Date);
            Assert.Equal("No text",deletedTweet.Text);
            Assert.Equal("No id",deletedTweet.Id);
        }
        
        [Fact]
        public async void DeleteTweetIncorrectTest()
        {
            _tweet.Id = new ObjectId();
            TweetDto testTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.Equal("No date",testTweet.Date);
            Assert.Equal("No text",testTweet.Text);
            Assert.Equal("No id",testTweet.Id);

            _repository.DeleteAsync(_tweet.Id.ToString());
            TweetDto deletedTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.Equal("No date",deletedTweet.Date);
            Assert.Equal("No text",deletedTweet.Text);
            Assert.Equal("No id",deletedTweet.Id);
        }
    }
}