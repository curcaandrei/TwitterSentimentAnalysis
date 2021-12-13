using Xunit;

namespace InfrastructureTest.ExternalTwitterAPITestsDir
{
    public class ValidateAuthTests : BaseTests
    {
        [Fact]
        public void ValidateAuthTestLinkUsed()
        {
            var res = _externalTwitterRepository.Object.ValidateAuth(
                "https://api.twitter.com/oauth/authorize?oauth_token=PsuqzQAAAAABWPzyAAABfaR8ULM");
            Assert.NotNull(res);
        }
        
    }
}