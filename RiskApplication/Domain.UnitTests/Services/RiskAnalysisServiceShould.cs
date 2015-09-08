using System.Collections.Generic;
using System.Linq;
using Domain.FileReaders;
using Domain.Models;
using Domain.Services;
using Moq;
using NUnit.Framework;

namespace Domain.UnitTests.Services
{
    [TestFixture]
    public class RiskAnalysisServiceShould
    {
        private const string SettledBetsFilePath = "X:\\settled.csv";
        private const string UnsettledBetsFilePath = "X:\\unsettled.csv";
        private Mock<ICsvFileReader<SettledBet>> _settledBetCsvFileReader;
        private Mock<ICsvFileReader<UnsettledBet>> _unsettledBetCsvFileReader;
        private Mock<IFilePathsProvider> _filePathsProvider;
        private RiskAnalysisService _sut;

        [SetUp]
        public void Setup()
        {
            _settledBetCsvFileReader = new Mock<ICsvFileReader<SettledBet>>();
            _unsettledBetCsvFileReader = new Mock<ICsvFileReader<UnsettledBet>>();
            _filePathsProvider = new Mock<IFilePathsProvider>();
            _filePathsProvider.Setup(p => p.GetSettledBetsFilePath()).Returns(SettledBetsFilePath);
            _filePathsProvider.Setup(p => p.GetUnsettledBetsFilePath()).Returns(UnsettledBetsFilePath);

            _sut = new RiskAnalysisService(_unsettledBetCsvFileReader.Object, _settledBetCsvFileReader.Object,
                _filePathsProvider.Object);
        }

        [Test]
        public void ReturnClientStatisticsAsExpected()
        {
            _settledBetCsvFileReader.Setup(p => p.ReadCsvFile(SettledBetsFilePath)).Returns(new List<SettledBet>
            {
                new SettledBet{Customer = 1, Stake = 20, Win = 50},
                new SettledBet{Customer = 1, Stake = 40, Win = 0},
                new SettledBet{Customer = 1, Stake = 35, Win = 0},
                new SettledBet{Customer = 1, Stake = 25, Win = 40},

                new SettledBet{Customer = 2, Stake = 100, Win = 125},
                new SettledBet{Customer = 2, Stake = 50, Win = 90}
            });

            var result = _sut.GetCustomerStatics().ToList();
            Assert.AreEqual(2, result.Count());
            var first = result.First(c => c.Customer == 1);
            Assert.AreEqual(30, first.AverageStake);
            Assert.AreEqual(4, first.TotalBets);
            Assert.AreEqual(2, first.TotalBetsWon);
            Assert.AreEqual(50, first.WinningPercentage);
            var second = result.First(c => c.Customer == 2);
            Assert.AreEqual(75, second.AverageStake);
            Assert.AreEqual(2, second.TotalBets);
            Assert.AreEqual(2, second.TotalBetsWon);
            Assert.AreEqual(100, second.WinningPercentage);
        }

        [Test]
        public void ReturnSettledBetsForASpecificCustomer()
        {
            _settledBetCsvFileReader.Setup(p => p.ReadCsvFile(SettledBetsFilePath)).Returns(new List<SettledBet>
            {
                new SettledBet{Customer = 1, Stake = 20, Win = 50},
                new SettledBet{Customer = 1, Stake = 40, Win = 0},
                new SettledBet{Customer = 1, Stake = 35, Win = 0},
                new SettledBet{Customer = 1, Stake = 25, Win = 40},

                new SettledBet{Customer = 2, Stake = 100, Win = 125},
                new SettledBet{Customer = 2, Stake = 50, Win = 90}
            });

            var result = _sut.ReadAllSettledBets(1).ToList();
            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(1, result[0].Customer);
        }
    }
}