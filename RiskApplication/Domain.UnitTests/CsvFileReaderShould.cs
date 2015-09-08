using System.Linq;
using Moq;
using NUnit.Framework;

namespace Domain.UnitTests
{
    [TestFixture]
    public class CsvFileReaderShould
    {
        private Mock<IFileSystem> _fileSystem;
        private CsvFileReader<SettledBet> _sut;
            
        [SetUp]
        public void Setup()
        {
            _fileSystem = new Mock<IFileSystem>();
            _sut = new CsvFileReader<SettledBet>(_fileSystem.Object);
        }

        [Test]
        public void ReturnEmptyListIfTheFileIsEmtpy()
        {
            _fileSystem.Setup(p => p.ReadAllLines(It.IsAny<string>())).Returns(new string[]{});
            var result = _sut.ReadCsvFile(It.IsAny<string>());
            Assert.AreEqual(0, result.Count());
        }


        [Test]
        public void ReturnEmptyListIfTheFileHasOnlyTheHeader()
        {
            _fileSystem.Setup(p => p.ReadAllLines(It.IsAny<string>()))
                .Returns(new[] { "Customer,Event,Participant,Stake,Win" });
            var result = _sut.ReadCsvFile(It.IsAny<string>());
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ReturnEmptyListIfTheFileHasOnlyTheHeaderAndEmptyLines()
        {
            _fileSystem.Setup(p => p.ReadAllLines(It.IsAny<string>()))
                .Returns(new[] { "Customer,Event,Participant,Stake,Win" , "", "     "});
            var result = _sut.ReadCsvFile(It.IsAny<string>());
            Assert.AreEqual(0, result.Count());
        }
    }
}