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
    class DemeritPointsCalculatorTests
    {
        private const int SpeedLimit = 65;
        private const int MaxSpeed = 300;
        private DemeritPointsCalculator _dpc;

        [SetUp]
        public void SetUp()
        {
            _dpc = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(0)]
        [TestCase(65)]
        public void CalculateDemeritPoints_BetweenSpeedLimitAndZero_NoPoints(int speed)
        {
            var result = _dpc.CalculateDemeritPoints(speed);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(MaxSpeed + 1)]
        public void CalculateDemeritPoints_OutOfRange_ThrowsOutOfRangeException(int speed)
        {
            Assert.That(() => _dpc.CalculateDemeritPoints(-1), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(70, 1)]
        [TestCase(100, 7)]
        [TestCase(66, 0)]
        public void CalculateDemeritPoints_GreaterThan65_ReturnsPoints(int speed, int expectedPoints)
        {
            var result = _dpc.CalculateDemeritPoints(speed);
            Assert.That(result, Is.EqualTo(expectedPoints));
        }


    }
}
