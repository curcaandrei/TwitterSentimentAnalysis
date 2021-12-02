﻿using Domain.Entities;
using MongoDB.Bson;
using Xunit;

namespace InfrastructureTest.DataBaseTestsDir
{
    public class DeleteTweetTests : DataBaseTests
    {
        [Fact]
        public async void DeleteTweetTest()
        {
            _tweet.Id = new ObjectId();
            await _repository.AddAsync(_tweet);
            Tweet testTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.NotNull(testTweet);
            _repository.DeleteAsync(_tweet.Id.ToString());
            Tweet deletedTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.Null(deletedTweet);
        }
        
        [Fact]
        public async void DeleteTweetIncorrectTest()
        {
            _tweet.Id = new ObjectId();
            Tweet testTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.Null(testTweet);
            _repository.DeleteAsync(_tweet.Id.ToString());
            Tweet deletedTweet = await _repository.GetByIdAsync(_tweet.Id);
            Assert.Null(deletedTweet);
        }
    }
}