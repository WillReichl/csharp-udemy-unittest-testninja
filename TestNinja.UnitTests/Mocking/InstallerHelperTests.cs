using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownlaoder> _mockFileDownloader;
        private InstallerHelper _installerHelper;

        // File found, return true
        // File not found, expect WebException

        [SetUp]
        public void SetUp()
        {
            _mockFileDownloader = new Mock<IFileDownlaoder>();
            _installerHelper = new InstallerHelper(_mockFileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            _mockFileDownloader.Setup(mfd => 
                mfd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new WebException());
            var result = _installerHelper.DownloadInstaller(customerName: "x", installerName: "y");
            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadSucceeds_ReturnsTrue()
        {
            var result = _installerHelper.DownloadInstaller(customerName: "x", installerName: "y");
            Assert.That(result, Is.True);
        }
    }
}
