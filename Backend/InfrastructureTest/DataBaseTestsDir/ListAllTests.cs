using Domain.Dtos;
using Xunit;

namespace InfrastructureTest.DataBaseTestsDir
{
    public class ListAllTests : DataBaseTests
    {
        [Fact]
        public async void ListPage()
        {
            var list = await _repository.ListAllAsync(1);
            Assert.NotEmpty(list);
        }
    }
}