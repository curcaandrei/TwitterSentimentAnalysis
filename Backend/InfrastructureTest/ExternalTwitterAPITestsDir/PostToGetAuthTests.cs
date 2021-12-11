using Xunit;

namespace InfrastructureTest.ExternalTwitterAPITestsDir
{
    public class PostToGetAuthTests : BaseTests
    {
        [Fact]
        public void PostToGetAuthTest()
        {
            var link = _externalTwitterRepository.Object.PostToGetAuth();
            Assert.NotNull(link);
        }
    }
}