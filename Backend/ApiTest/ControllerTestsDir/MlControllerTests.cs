using Application.Features.Tweets.GetOneTweet;
using Application.Features.Tweets.PredictTweetSentiment;
using FakeItEasy;
using MediatR;
using Microsoft.Extensions.Options;
using Moq;
using Persistence.MongoDb;
using Persistence.Repositories;
using WebApi.Controllers;
using Xunit;

namespace ApiTest.ControllerTestsDir
{
    public class MlControllerTests
    {
        protected readonly Mock<MongoDbContext> _mockContext;
        protected readonly Mock<MongoSettings> _mongoSettings;
        protected readonly TweetRepository _repository;
        private readonly Mock<PredictMlController> controller;
        private readonly IMediator mediator;

        public MlControllerTests()
        {
            _mongoSettings = new Mock<MongoSettings>("mongodb+srv://admin:admin@proiectdotnet.8hto9.mongodb.net/TwitterDB?retryWrites=true&w=majority","TwitterDB");
            IOptions<MongoSettings> options = Options.Create(_mongoSettings.Object);
            _mockContext = new Mock<MongoDbContext>(options);
            _repository = new TweetRepository(_mockContext.Object);
            mediator = A.Fake<IMediator>();
            controller = new Mock<PredictMlController>(mediator);
        }
        
        [Fact]
        public async void PredictText()
        {
            await controller.Object.Analysis("I'm sad today");
            A.CallTo(() => mediator.Send(A<PredictTweetSentimentQuery>._, default)).MustHaveHappenedOnceExactly();
        }


    }
}