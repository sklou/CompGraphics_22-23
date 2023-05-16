using NUnit.Framework;
using Moq;
using System.IO;
using laba2;

namespace TestsConvertor
{
    public class ImageReaderTests
    {
        private Mock<IImageReader> _mockImageReader;
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _mockImageReader = new Mock<IImageReader>();
            _filePath = "path/to/test/image.ppm";
        }

        [Test]
        public void CanRead_ShouldReturnTrue_WhenImageIsSupported()
        {
            _mockImageReader.Setup(m => m.CanRead(_filePath)).Returns(true);

            bool result = _mockImageReader.Object.CanRead(_filePath);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanRead_ShouldReturnFalse_WhenImageIsNotSupported()
        {
            _mockImageReader.Setup(m => m.CanRead(_filePath)).Returns(false);

            bool result = _mockImageReader.Object.CanRead(_filePath);

            Assert.IsFalse(result);
        }

        [Test]
        public void ReadImage_ShouldNotThrowException_WhenImageIsSupported()
        {
            _mockImageReader.Setup(m => m.CanRead(_filePath)).Returns(true);

            Assert.DoesNotThrow(() => _mockImageReader.Object.ReadImage(_filePath));
        }
    }
}
