using System.Linq;
using System.Security.Cryptography;
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

    [TestFixture]
    public class SettledBetCsvLineReaderShould
    {
        [TestCase(null, Description = "On null line return null")]
        [TestCase("", Description = "On empty line return null")]
        [TestCase("  ", Description = "On whitespace line return null")]
        [TestCase("1,2,3", Description = "On less than 5 cells return null")]
        [TestCase("1,2,3,4,5,6", Description = "On more than 5 cells return null")]
        [TestCase("1,2,3,4,NaN", Description = "On non-integger value return null")]
        [TestCase("1,2,3,,5", Description = "When a cell is not valued return null")]
        public void ReturnNullOnInvalidLineInputs(string line)
        {
            var settledBetCsvLineReader = new SettledBetCsvLineReader();
            var result = settledBetCsvLineReader.ReadLine(line);
            Assert.IsNull(result);
        }

        [TestCase("1  , 2  ,3,  40 ,0  ", Description = "Should know how to read correctly when whitespaces are present")]
        [TestCase("1,2,3,40,0")]
        public void ReturnExpectedObject(string line)
        {
            var settledBetCsvLineReader = new SettledBetCsvLineReader();
            var result = settledBetCsvLineReader.ReadLine(line);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Customer);
            Assert.AreEqual(2, result.Event);
            Assert.AreEqual(3, result.Participant);
            Assert.AreEqual(40, result.Stake);
            Assert.AreEqual(0, result.Win);
        }
    }
}