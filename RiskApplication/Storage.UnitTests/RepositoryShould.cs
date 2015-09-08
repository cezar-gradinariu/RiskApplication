using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Models;
using Moq;
using NUnit.Framework;
using Storage.FileReaders;

namespace Storage.UnitTests
{
    [TestFixture]
    public class RepositoryShould
    {
        private const string SettledBetsFilePath = "X:\\settled.csv";
        private const string UnsettledBetsFilePath = "X:\\unsettled.csv";
        private Mock<ICsvFileReader<SettledBet>> _settledBetCsvFileReader;
        private Mock<ICsvFileReader<UnsettledBet>> _unsettledBetCsvFileReader;
        private Mock<IFilePathsProvider> _filePathsProvider;
        private Repository _sut;

        [SetUp]
        public void Setup()
        {
            _settledBetCsvFileReader = new Mock<ICsvFileReader<SettledBet>>();
            _unsettledBetCsvFileReader = new Mock<ICsvFileReader<UnsettledBet>>();
            _filePathsProvider = new Mock<IFilePathsProvider>();
            _filePathsProvider.Setup(p => p.GetSettledBetsFilePath()).Returns(SettledBetsFilePath);
            _filePathsProvider.Setup(p => p.GetUnsettledBetsFilePath()).Returns(UnsettledBetsFilePath);

            _sut = new Repository(_filePathsProvider.Object, _settledBetCsvFileReader.Object,
                _unsettledBetCsvFileReader.Object);
        }

        [Test]
        public void ShouldReadSettledBets()
        {
            _settledBetCsvFileReader.Setup(p => p.ReadCsvFile(SettledBetsFilePath)).Returns(new List<SettledBet>
            {
                new SettledBet {Customer = 1, Stake = 20, Win = 50},
                new SettledBet {Customer = 2, Stake = 50, Win = 90}
            });
            var list = _sut.GetAllSettledBets();
            Assert.AreEqual(2, list.Count());
        }

        [Test]
        public void ShouldReadUnsettledBets()
        {
            _unsettledBetCsvFileReader.Setup(p => p.ReadCsvFile(UnsettledBetsFilePath)).Returns(new List<UnsettledBet>
            {
                new UnsettledBet{Customer = 1, Stake = 301, ToWin = 1500}, 
                new UnsettledBet{Customer = 2, Stake = 25, ToWin = 50}, 
                new UnsettledBet{Customer = 3, Stake = 40, ToWin = 60}, 
            });
            var list = _sut.GetAllUnsettledBets();
            Assert.AreEqual(3, list.Count());
        }
    }
}