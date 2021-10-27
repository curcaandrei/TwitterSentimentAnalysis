using NUnit.Framework;
using Proiect;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private Proiect.TestClass _testClass;

        [SetUp]
        public void Setup()
        {
            _testClass = new TestClass();
        }

        [Test]
        public void Test1()
        {
            int expectedValue = _testClass.Test();

            Assert.AreEqual(1, expectedValue);
        }
    }
}