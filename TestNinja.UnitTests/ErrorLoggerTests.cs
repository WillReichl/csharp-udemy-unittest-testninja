using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class ErrorLoggerTests
    {
        private ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty ()
        {
            _logger.Log("a");

            Assert.That(_logger.LastError, Is.EqualTo("a"));               
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowsNullException(string error)
        {
            Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);

            // This test actually doesn't compile. I don't want to spend any more time figuring out why.
            //Assert.That(() => _logger.Log(error), Throws.Exception.TypeOf<ArgumentNullException>);
        }
    }
}
