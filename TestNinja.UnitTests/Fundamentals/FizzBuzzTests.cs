using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    class FizzBuzzTests
    {
        [Test]
        [TestCase(0, "FizzBuzz")]
        [TestCase(1, "1")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        public void GetOutput_WhenCalled_GetsExpectedResult(int num, string expectedResult)
        {
            var result = FizzBuzz.GetOutput(num);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
