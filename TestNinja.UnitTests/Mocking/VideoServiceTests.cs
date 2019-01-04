using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
//using TestNinja.UnitTests.Fakes;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _mockFileReader;
        private Mock<IVideoRepository> _mockVideoRepository;

        [SetUp]
        public void SetUp()
        {
            _mockFileReader = new Mock<IFileReader>();
            _mockVideoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_mockFileReader.Object, _mockVideoRepository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            _mockFileReader.Setup(mfr => mfr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_OneUnprocesssedVideo_ReturnsSingleVal ()
        {
            _mockVideoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video> {
                new Video { Id = 1 }
            }); // empty list

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1"));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_ThreeUnprocesssedVideos_ReturnsCsvString ()
        {
            _mockVideoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video> {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            }); // empty list

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsEmptyString ()
        {
            _mockVideoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>()); // empty list

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

    }
}
