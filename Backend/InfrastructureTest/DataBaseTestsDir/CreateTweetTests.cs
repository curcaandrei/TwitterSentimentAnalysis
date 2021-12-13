using Domain.Dtos;
using MongoDB.Bson;
using Xunit;

namespace InfrastructureTest.DataBaseTestsDir
{

    public class TweetTest : DataBaseTests
    {
        
        [Fact]
        public async void AddAsyncTest()
        {
            _tweet.Id = new ObjectId();
            await _repository.AddAsync(_tweet);
            TweetDto testTweet = await _repository.GetByIdAsync(_tweet.Id, true);
            Assert.NotNull(testTweet);
            Assert.Equal(testTweet.Date, _tweet.Date);
            Assert.Equal(testTweet.User, _tweet.User);
            Assert.Equal(testTweet.Text, _tweet.Text);
            Dispose();
        }

        [Fact]
        public async void AddAsyncIncorrectTest()
        {
            _tweet.Id = new ObjectId();
            await _repository.AddAsync(_tweet);
            _tweet.Id = new ObjectId();

            TweetDto testTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.Equal("No date",testTweet.Date);
            Assert.Equal("No text",testTweet.Text);
            Assert.Equal("No id",testTweet.Id);
            Dispose();
        }

       
        
    }

}