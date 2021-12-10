using Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;
using Persistence.TwitterExternalAPI;

namespace InfrastructureTest.ExternalTwitterAPITestsDir
{
    public class BaseTests
    {
        protected readonly Mock<ExternalTwitterRepository> _externalTwitterRepository;
        protected Tweet _tweet;
        protected readonly Mock<TwitterHelper> _twitterHelper;
        protected readonly Mock<TwitterSettings> _settings;

        public BaseTests()
        {
            _settings = new Mock<TwitterSettings>("DycmxCAZlwwx2b5eEflU5Sl1w",
                "jJr4emCb35iQPPB8WdTaJPsbfidZvCED6jpUxdQf3T4r6z5Qs0",
                "995542379410153472-8tuUoglasaaQw1O95njkv9b44E6pjy0", "U4vnZTfaUqoeztJaHJzZ6IYm94qt9Dand2S7Ew45VlpZa");
            IOptions<TwitterSettings> options = Options.Create(_settings.Object);
            _twitterHelper = new Mock<TwitterHelper>(options);
            _externalTwitterRepository = new Mock<ExternalTwitterRepository>(_twitterHelper.Object);
            _tweet = new Tweet();
        }
    }
}