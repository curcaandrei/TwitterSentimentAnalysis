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
            _settings = new Mock<TwitterSettings>("IAup8uCkBVBvNNlETc6oHcdPT",
                "gSs0B9xiI8zIABz728ugOYlaKWBlWi9mL5tRvD51hof7yG8AEz",
                "995542379410153472-AUx78yrjMn2bqzkJR2sqWgfUBhzIBeH", "RcxyF6vQVEFMAjZXAd6rlQWrxDjNGUAah50CdnxVxdBDt");
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
            Assert.Equal("Twitter",_tweet.User);
            Assert.Equal("hello literally everyone", _tweet.Text);
            Assert.Null(_tweet.feels);
        }
    }
}