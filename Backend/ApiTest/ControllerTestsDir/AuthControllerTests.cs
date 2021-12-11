using System.Formats.Asn1;
using Application.Features.ExternalTwitterAPI.LogInUser.GetTwitterAuth;
using Application.Features.ExternalTwitterAPI.LogInUser.ValidateAuth;
using FakeItEasy;
using MediatR;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace ApiTest.ControllerTestsDir
{
    public class AuthControllerTests
    {
        private readonly Mock<AuthenticationController> controller;
        private readonly IMediator mediator;

        public AuthControllerTests()
        {
            mediator = A.Fake<IMediator>();
            controller = new Mock<AuthenticationController>(mediator);
        }

        [Fact]
        public async void GetAuthLink()
        {
            await controller.Object.TwitterAuth();
            A.CallTo(() => mediator.Send(A<GetTwitterAuthQuery>._, default)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void ValidateAuth()
        {
            await controller.Object.ValidateTwitterAuth("test","test","test");
            A.CallTo(() => mediator.Send(A<ValidateAuthQuery>._, default)).MustHaveHappenedOnceExactly();
        }
    }
}