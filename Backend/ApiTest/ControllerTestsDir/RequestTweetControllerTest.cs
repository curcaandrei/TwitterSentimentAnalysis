using System.Collections.Generic;
using Application.Commands.RequestTweet;
using Application.Features.RequestTweet;
using Domain.Entities;
using FakeItEasy;
using MediatR;
using MongoDB.Bson;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace ApiTest.ControllerTestsDir
{
    public class RequestTweetControllerTest
    {
        private readonly Mock<RequestTweetController> controller;
        private readonly IMediator mediator;
        private readonly Tweet _tweet;
        
        public RequestTweetControllerTest()
        {
            mediator = A.Fake<IMediator>();
            controller = new Mock<RequestTweetController>(mediator);
            _tweet = new Tweet();
            _tweet.Id = new ObjectId("61a7aa54113c669a867f88e8");
            _tweet.Date = "01.12.2021";
            _tweet.User = "mihnea_ochesanu";
            _tweet.Text = "This is a text sample";
            _tweet.feels = new Dictionary<string, float> {{"sad", 100}};
        }
        
        [Fact]
        public async void GetAll()
        {
            await controller.Object.GetAll();
            A.CallTo(() => mediator.Send(A<GetAllRequestsQuery>._, default)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async void AddToRequests()
        {
            
            await controller.Object.AddToRequests(_tweet);
            A.CallTo(() => mediator.Send(A<RequestToAddTweetQuery>._, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void DeleteRequest()
        {
            controller.Object.DeleteRequest(_tweet.Id.ToString());
            A.CallTo(() => mediator.Send(A<DeleteTweetRequestCommand>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}