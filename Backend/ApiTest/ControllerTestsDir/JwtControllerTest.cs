using System.Collections.Generic;
using Application.Features.Jwt;
using Domain.Entities;
using FakeItEasy;
using MediatR;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace ApiTest.ControllerTestsDir
{
    public class JwtControllerTest
    {
        private readonly Mock<JwtController> controller;
        private readonly IMediator mediator;

        public JwtControllerTest()
        {
            mediator = A.Fake<IMediator>();
            controller = new Mock<JwtController>(mediator);
        }

        [Fact]
        public void GetToken()
        {
            TweetSerializer tweetSerializer = new TweetSerializer();
            tweetSerializer.userId = "@curcaandrei99";
            tweetSerializer.data = new List<MiniTweetDto>()
            {
                new MiniTweetDto()
                {
                    id = "1470666379263107072",
                    text = "I do not feel very good"
                },
                new MiniTweetDto()
                {
                    id = "1469987067379728389",
                    text = "i'm so sad today"
                }
            };
            controller.Object.GetToken(tweetSerializer);
            A.CallTo(() => mediator.Send(A<GetJwtQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}