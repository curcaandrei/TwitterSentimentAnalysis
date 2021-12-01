using System.Collections.Generic;
using Application.Commands.CreateTweet;
using Application.Features.Tweets.GetOneTweet;
using AutoMapper;
using Domain.Entities;
using MediatR;
using WebApi.Controllers;
using Xunit;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
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

        // [Fact]
        // public async void CreateTweetTest()
        // {
        //     CreateTweetCommand createTweetCommand = new CreateTweetCommand();
        //     createTweetCommand.Date = "3454534";
        //     createTweetCommand.User = "dfgdfhdf";
        //     createTweetCommand.Text = "fasfasf";
        //     createTweetCommand.feels = new Dictionary<string, float> {{"sad", 100}};
        //
        //     await controller.Object.Create(createTweetCommand);
        //     // A.CallTo(() => mediator.Send(A<CreateTweetCommand>._, default)).MustHaveHappenedOnceExactly();
        //}
    }
}