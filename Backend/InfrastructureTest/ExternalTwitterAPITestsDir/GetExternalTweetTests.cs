using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;
using Persistence.TwitterExternalAPI;
using Xunit;

namespace InfrastructureTest.ExternalTwitterAPITestsDir
{
    public class GetExternalTweetTests
    {
        protected readonly ExternalTwitterRepository _externalTwitterRepository;
        protected Tweet _tweet;
        private readonly Mock<TwitterHelper> _twitterHelper;
        private readonly Mock<TwitterSettings> _settings;

        public GetExternalTweetTests()
        {
            _settings = new Mock<TwitterSettings>("yhJAct7wkrUjZqH29G2JnaNXp",
                "VIGJH866VEcowNcK1VxzsdJXJ01JJFLohGrNXS8mSHwNvLR2g7",
                "995542379410153472-dKr2vqGzIvUviUoQwJre0ddfy0uS5In", "JU8O787mlpPXyAoWnJWUZsJmFVaIyBaFAnmEEGe3UJ3NP");
            IOptions<TwitterSettings> options = Options.Create(_settings.Object);
            _twitterHelper = new Mock<TwitterHelper>(options);
            _externalTwitterRepository = new ExternalTwitterRepository(_twitterHelper.Object);
            _tweet = new Tweet();
        }

        [Fact]
        public async void GetExternalTweetTest()
        {
            _tweet = await _externalTwitterRepository.GetTweetById("1445078208190291973");
            Assert.NotNull(_tweet);
            Assert.True(_tweet.Date is "04/10/2021 17:27:47 +00:00" or 
                "10/04/2021 17:27:47 +00:00" or 
                "10/4/2021 5:27:47 PM +00:00" or 
                "4/10/2021 5:27:47 PM +00:00");
            Assert.Equal("Services",_tweet.User);
            Assert.Equal("hello literally everyone", _tweet.Text);
            Assert.Null(_tweet.feels);
        }
    }
}