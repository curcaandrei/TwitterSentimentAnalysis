using Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;
using Persistence.TwitterExternalAPI;
using Xunit;

namespace InfrastructureTest.ExternalTwitterAPITestsDir
{
    public class GetExternalTweetTests : BaseTests
    {
        [Fact]
        public async void GetExternalTweetTest()
        {
            _tweet = await _externalTwitterRepository.Object.GetTweetById("1445078208190291973");
            Assert.NotNull(_tweet);
            Assert.Equal("Twitter",_tweet.User);
            Assert.Equal("hello literally everyone", _tweet.Text);
            Assert.Null(_tweet.feels);
        }
    }
}