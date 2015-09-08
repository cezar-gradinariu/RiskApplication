using System.Collections.Generic;
using System.Linq;
using Domain.BusinessRules.Interfaces;
using Domain.FileReaders;
using Domain.Models;

namespace Domain.Services
{
    public class RiskAnalysisService : IRiskAnalysisService
    {
        private readonly ICsvFileReader<SettledBet> _settledBetCsvFileReader;
        private readonly ICsvFileReader<UnsettledBet> _unsettledBetCsvFileReader;
        private readonly IFilePathsProvider _filePathsProvider;

        private readonly IHighPrizeBusinessRule _highPrizeBusinessRule;
        private readonly IUnusualStakeBusinessRule _unusualStakeBusinessRule;
        private readonly IUnusuallyHighStakeBusinessRule _unusuallyHighStakeBusinessRule;
        private readonly IUnusualWinRateBusinessRule _unusualWinRateBusinessRule;

        public RiskAnalysisService(ICsvFileReader<UnsettledBet> unsettledBetCsvFileReader,
            ICsvFileReader<SettledBet> settledBetCsvFileReader, IFilePathsProvider filePathsProvider, IHighPrizeBusinessRule highPrizeBusinessRule, IUnusualStakeBusinessRule unusualStakeBusinessRule, IUnusuallyHighStakeBusinessRule unusuallyHighStakeBusinessRule, IUnusualWinRateBusinessRule unusualWinRateBusinessRule)
        {
            _unsettledBetCsvFileReader = unsettledBetCsvFileReader;
            _settledBetCsvFileReader = settledBetCsvFileReader;
            _filePathsProvider = filePathsProvider;
            _highPrizeBusinessRule = highPrizeBusinessRule;
            _unusualStakeBusinessRule = unusualStakeBusinessRule;
            _unusuallyHighStakeBusinessRule = unusuallyHighStakeBusinessRule;
            _unusualWinRateBusinessRule = unusualWinRateBusinessRule;
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

        private IEnumerable<UnsettledBetWithStatistics> GetUnsettledBetsWithStatistics()
        {
            var statistics = GetCustomerStatics();
            var unsettledBets = ReadAllUnsettledBets();

            var query = from bet in unsettledBets
                join s in statistics on bet.Customer equals s.Customer into gj
                from g in gj.DefaultIfEmpty()
                select new UnsettledBetWithStatistics
                {
                    CustomerStatics = g,
                    UnsettledBet = bet
                };
            return query.ToList();
        }

        public IEnumerable<UnsettledBetWithRiskAnalysis> GetUnsettledBetsWithRiskAnalysis()
        {
            return GetUnsettledBetsWithStatistics()
                .Select(p => new UnsettledBetWithRiskAnalysis
                {
                    WinningPercentage = p.CustomerStatics == null ? 0 : p.CustomerStatics.WinningPercentage,
                    UnsettledBet = p.UnsettledBet,
                    RiskAnalysis = new RiskAnalysis
                    {
                        IsHighPrize = _highPrizeBusinessRule.IsSatisfied(p.UnsettledBet),
                        IsUnusuallyHighStake = _unusuallyHighStakeBusinessRule.IsSatisfied(p),
                        HasUnusualWinRate = _unusualWinRateBusinessRule.IsSatisfied(p.CustomerStatics),
                        IsHighStake = _unusualStakeBusinessRule.IsSatisfied(p)
                    }
                });
        }
    }
}