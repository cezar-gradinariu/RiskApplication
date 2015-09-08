using System.Linq;
using Moq;
using NUnit.Framework;

namespace Domain.UnitTests
{
    [TestFixture]
    public class CsvFileReaderShould
    {
        private const string FilePath = "X:\\settledBets.csv";
        private Mock<IFileSystem> _fileSystem;
        private Mock<ICsvLineReader<SettledBet>> _lineReader;
        private CsvFileReader<SettledBet> _sut;
            
        [SetUp]
        public void Setup()
        {
            _fileSystem = new Mock<IFileSystem>();
            _lineReader = new Mock<ICsvLineReader<SettledBet>>();
            _sut = new CsvFileReader<SettledBet>(_fileSystem.Object, _lineReader.Object);
        }

        [Test]
        public void ReturnEmptyListIfFileDoesNotExist()
        {
            //-->file does not exist
            _fileSystem.Setup(p => p.Exists(FilePath)).Returns(false);
            //-->valid settings
            _fileSystem.Setup(p => p.ReadAllLines(FilePath)).Returns(new[]
            {
                "Customer,Event,Participant,Stake,Win",
                "1,2,3,400, 0"
            });
            _lineReader.Setup(p => p.ReadLine(It.IsAny<string>())).Returns(new SettledBet());
            //ACT
            var result = _sut.ReadCsvFile(FilePath);
            //ASSERT
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ReturnEmptyListIfTheFileIsEmtpy()
        {
            _fileSystem.Setup(p => p.Exists(FilePath)).Returns(true);
            _lineReader.Setup(p => p.ReadLine(It.IsAny<string>())).Returns(new SettledBet());
            _fileSystem.Setup(p => p.ReadAllLines(FilePath)).Returns(new string[] { });
            var result = _sut.ReadCsvFile(FilePath);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ReturnEmptyListIfTheFileHasOnlyTheHeader()
        {

            _fileSystem.Setup(p => p.Exists(FilePath)).Returns(true);
            _lineReader.Setup(p => p.ReadLine(It.IsAny<string>())).Returns(new SettledBet());
            _fileSystem.Setup(p => p.ReadAllLines(FilePath))
                .Returns(new[] { "Customer,Event,Participant,Stake,Win" });
            var result = _sut.ReadCsvFile(FilePath);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ReturnEmptyListIfTheFileHasOnlyTheHeaderAndEmptyLines()
        {
            _fileSystem.Setup(p => p.Exists(FilePath)).Returns(true);
            _lineReader.Setup(p => p.ReadLine(It.IsAny<string>())).Returns(new SettledBet());
            _fileSystem.Setup(p => p.ReadAllLines(FilePath))
                .Returns(new[] { "Customer,Event,Participant,Stake,Win" , "", "     "});
            var result = _sut.ReadCsvFile(FilePath);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ReturnAListOf1ObjectWhenAValidLineIsSentThrough()
        {
            _fileSystem.Setup(p => p.Exists(FilePath)).Returns(true);
            _fileSystem.Setup(p => p.ReadAllLines(FilePath)).Returns(new[]
            {
                "Customer,Event,Participant,Stake,Win",
                "1,2,3,400, 0",
                ""
            });
            _lineReader.Setup(p => p.ReadLine(It.IsAny<string>())).Returns(new SettledBet());

            var result = _sut.ReadCsvFile(FilePath);
            Assert.AreEqual(1, result.Count());
        }
    }
}