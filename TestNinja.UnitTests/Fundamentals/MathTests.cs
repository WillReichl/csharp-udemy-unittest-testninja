using NUnit.Framework;
using System.Linq;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
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
        [TestCase(1, 2, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            // General --> Specific
            Assert.That(result, Is.Not.Empty);

            Assert.That(result.Count(), Is.EqualTo(3));

            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));

            // This test renders all tests above duplicative, but left them in for additional examples
            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            // Other useful collection tests
            Assert.That(result, Is.Ordered); // result should be sorted
            Assert.That(result, Is.Unique); // result should have no dupes
        }
    }
}
