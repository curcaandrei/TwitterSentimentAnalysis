using System;
using Domain.Dtos;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
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
            TweetDTO testTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.NotNull(testTweet);
            Assert.Equal(testTweet.Date, _tweet.Date);
            Assert.Equal(testTweet.User, _tweet.User);
            Assert.Equal(testTweet.Text, _tweet.Text);
            foreach (var (key, value) in _tweet.feels)
            {
                Assert.Equal(testTweet.feels[key],value);
            }
            Dispose();
        }

        [Fact]
        public async void AddAsyncIncorrectTest()
        {
            _tweet.Id = new ObjectId();
            await _repository.AddAsync(_tweet);
            _tweet.Id = new ObjectId();

            TweetDTO testTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.Null(testTweet);
            Dispose();
        }

       
        
    }

}