using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Moq;
using Persistence.MongoDb;
using Persistence.Repositories;

namespace InfrastructureTest.DataBaseTestsDir
{
    public class DataBaseTests :IDisposable 
    {
        protected readonly Mock<MongoDbContext> _mockContext;
        protected readonly Mock<MongoSettings> _mongoSettings;
        protected readonly Tweet _tweet;
        protected readonly TweetRepository _repository;
        public DataBaseTests()
        {
            _mongoSettings = new Mock<MongoSettings>("mongodb+srv://admin:admin@proiectdotnet.8hto9.mongodb.net/TwitterDB?retryWrites=true&w=majority","TwitterDB");
            IOptions<MongoSettings> options = Options.Create(_mongoSettings.Object);
            _mockContext = new Mock<MongoDbContext>(options);
            _repository = new TweetRepository(_mockContext.Object);
            _tweet = new Tweet();
            _tweet.Id = new ObjectId("61a7aa54113c669a867f88e8");
            _tweet.Date = "01.12.2021";
            _tweet.User = "mihnea_ochesanu";
            _tweet.Text = "This is a text sample";
            _tweet.feels = new Dictionary<string, float> {{"sad", 100}};
        }
        public void Dispose()
        {
            _repository.DeleteAsync(_tweet.Id.ToString());
            GC.SuppressFinalize(this);
        }
    }
}