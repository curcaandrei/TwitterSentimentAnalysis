using Application.Features.ExternalTwitterAPI.GetTweetFromURL;
using FakeItEasy;
using MediatR;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace ApiTest.ControllerTestsDir
{
    public class ExternalTweetsControllerTests
    {
        private readonly Mock<ExternalTwitterController> controller;
        private readonly IMediator mediator;

        public ExternalTweetsControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new Mock<ExternalTwitterController>(mediator);
        }

        [Fact]
        public async void GetExternalTweetTest()
        {
            await controller.Object.GetExternalTweetById("1445078208190291973");
            A.CallTo(() => mediator.Send(A<GetTweetFromUrlQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}