using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        [Test]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            var math = new Math();

            var result = math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_WhenFirstArgumentGreater_ReturnFirstArgument()
        {
            // Arrange
            var math = new Math();

            // Act
            var result = math.Max(2, 1);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_WhenSecondArgumentGreater_ReturnSecondArgument()
        {
            // Arrange
            var math = new Math();

            // Act
            var result = math.Max(1, 2);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_WhenArgsAreEqual_ReturnFirstArgument()
        {
            // Arrange
            var math = new Math();

            // Act
            var result = math.Max(1, 1);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }
    }
}
