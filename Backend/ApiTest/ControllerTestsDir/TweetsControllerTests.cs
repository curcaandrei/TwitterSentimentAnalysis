using System.Collections.Generic;
using Application.Commands.CreateTweet;
using Application.Commands.DeleteTweet;
using Application.Commands.UpdateTweet;
using Application.Features.Tweets.GetAllTweets;
using Application.Features.Tweets.GetOneTweet;
using AutoMapper;
using MediatR;
using WebApi.Controllers;
using Xunit;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Moq;
using Persistence.MongoDb;
using Persistence.Repositories;

namespace ApiTest.ControllerTestsDir
{
    public class TweetsControllerTests
    {
        protected readonly Mock<MongoDbContext> _mockContext;
        protected readonly Mock<MongoSettings> _mongoSettings;
        protected readonly TweetRepository _repository;
        private readonly Mock<TweetsController> controller;
        private readonly IMediator mediator;

        public TweetsControllerTests()
        {
            _mongoSettings = new Mock<MongoSettings>("mongodb+srv://admin:admin@proiectdotnet.8hto9.mongodb.net/TwitterDB?retryWrites=true&w=majority","TwitterDB");
            IOptions<MongoSettings> options = Options.Create(_mongoSettings.Object);
            _mockContext = new Mock<MongoDbContext>(options);
            _repository = new TweetRepository(_mockContext.Object);
            mediator = A.Fake<IMediator>();
            controller = new Mock<TweetsController>(mediator);
            
        }

        [Fact]
        public async void GetOneTweetTest()
        {
            await controller.Object.GetOne("619b7c337c3468160b0021b7");
            A.CallTo(() => mediator.Send(A<GetOneTweetQuery>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetPageTest()
        {
            var list = await controller.Object.GetAll(1);
            A.CallTo(() => mediator.Send(A<GetTweetsQuery>._, default)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void CreateTweetTest()
        {
            var cmd = new CreateTweetCommand();
            cmd.Date = "";
            cmd.Feels = new Dictionary<string, float>();
            cmd.Text = "";
            cmd.User = "";
            await controller.Object.Create(cmd);
            cmd = new CreateTweetCommand("","","",new Dictionary<string, float>());
            await controller.Object.Create(cmd);
            A.CallTo(() => mediator.Send(A<CreateTweetCommand>._, default)).MustHaveHappenedTwiceExactly();
        }

        [Fact]
        public async void DeleteTweetTest()
        {
            var obj = await controller.Object.Create(new CreateTweetCommand());
            A.CallTo(() => mediator.Send(A<CreateTweetCommand>._, default)).MustHaveHappenedOnceExactly();
            var res = controller.Object.DeleteOne(obj);
            A.CallTo(() => mediator.Send(A<DeleteTweetCommand>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateTweetTest()
        {
            var obj = await controller.Object.Create(new CreateTweetCommand());
            A.CallTo(() => mediator.Send(A<CreateTweetCommand>._, default)).MustHaveHappenedOnceExactly();
            var res = controller.Object.UpdateOne(obj, new Dictionary<string, float>());
            A.CallTo(() => mediator.Send(A<UpdateTweetCommand>._, default)).MustHaveHappenedOnceExactly();

        }
    }
}