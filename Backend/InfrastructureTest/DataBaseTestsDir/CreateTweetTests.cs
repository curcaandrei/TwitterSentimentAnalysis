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
            TweetDto testTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.NotNull(testTweet);
            Assert.Equal(testTweet.Date, _tweet.Date);
            Assert.Equal(testTweet.User, _tweet.User);
            Assert.Equal(testTweet.Text, _tweet.Text);
            foreach (var (key, value) in _tweet.feels)
            {
                Assert.Equal(testTweet.Feels[key],value);
            }
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