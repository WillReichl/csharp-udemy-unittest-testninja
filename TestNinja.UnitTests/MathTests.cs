using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        private Math _math;

        // SetUp -- called before each test
        [SetUp]
        public void SetUp()
        {
            _math = new Math(); // new instance before each test
        }

        // TearDown -- called after each test -- often used w/ integration tests, e.g. restore data in database to prior state

        [Test]
        [Ignore("Because I needed an example!")]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            var result = _math.Add(1, 2);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(1,2,2)]
        [TestCase(2,1,2)]
        [TestCase(1,1,1)]
        public void Max_WhenCalled_ReturnGreaterArgument (int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
