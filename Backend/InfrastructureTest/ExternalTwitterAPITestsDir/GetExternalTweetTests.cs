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
            _settings = new Mock<TwitterSettings>("DycmxCAZlwwx2b5eEflU5Sl1w",
                "jJr4emCb35iQPPB8WdTaJPsbfidZvCED6jpUxdQf3T4r6z5Qs0",
                "995542379410153472-8tuUoglasaaQw1O95njkv9b44E6pjy0", "U4vnZTfaUqoeztJaHJzZ6IYm94qt9Dand2S7Ew45VlpZa");
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
            Assert.Equal("Twitter",_tweet.User);
            Assert.Equal("hello literally everyone", _tweet.Text);
            Assert.Null(_tweet.feels);
        }
    }
}