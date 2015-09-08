using System.Collections.Generic;
using System.Linq;
using Domain.FileReaders;
using Domain.Models;

namespace Domain.Services
{
    public class RiskAnalysisService : IRiskAnalysisService
    {
        private readonly ICsvFileReader<SettledBet> _settledBetCsvFileReader;
        private readonly ICsvFileReader<UnsettledBet> _unsettledBetCsvFileReader;
        private readonly IFilePathsProvider _filePathsProvider;

        public RiskAnalysisService(ICsvFileReader<UnsettledBet> unsettledBetCsvFileReader,
            ICsvFileReader<SettledBet> settledBetCsvFileReader, IFilePathsProvider filePathsProvider)
        {
            _unsettledBetCsvFileReader = unsettledBetCsvFileReader;
            _settledBetCsvFileReader = settledBetCsvFileReader;
            _filePathsProvider = filePathsProvider;
        }

        public IEnumerable<SettledBet> ReadAllSettledBets(int customer)
        {
            var filePath = _filePathsProvider.GetSettledBetsFilePath();
            return _settledBetCsvFileReader.ReadCsvFile(filePath).Where(p => p.Customer == customer);
        }

        public IEnumerable<UnsettledBet> ReadAllUnsettledBets()
        {
            var filePath = _filePathsProvider.GetUnsettledBetsFilePath();
            return _unsettledBetCsvFileReader.ReadCsvFile(filePath);
        }
        public IEnumerable<CustomerStatics> GetCustomerStatics()
        {
            var filePath = _filePathsProvider.GetSettledBetsFilePath();
            var settledBets = _settledBetCsvFileReader.ReadCsvFile(filePath);
            return settledBets
                .GroupBy(p => p.Customer)
                .Select(p => new CustomerStatics
                {
                    Customer = p.Key,
                    AverageStake = p.Average(q => q.Stake),
                    TotalBets = p.Count(),
                    TotalBetsWon = p.Count(q => q.Win > 0)
                });
        }
    }
}