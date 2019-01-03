using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using TestNinja.UnitTests.Fakes;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            var service = new VideoService();
            service.FileReader = new FakeFileReader();
            var result = service.ReadVideoTitle(new);
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

    }
}
