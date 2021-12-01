using System.Transactions;
using Domain.Entities;
using NUnit.Framework;
using Persistence.Repositories;

namespace InfrastructureTests
{
    [TestFixture]
    public class TweetTest
    {
        private TransactionScope _transactionScope;
        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();
            
        }

        [Test]
        public void UpdateTweetTest()
        {
            
        }
    }
}