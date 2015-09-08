using System.Collections.Generic;
using System.Linq;
using Domain.BusinessRules.Interfaces;
using Domain.Models;

namespace Domain.Services
{
    public class RiskAnalysisService : IRiskAnalysisService
    {
        private readonly IRepository _repository;

        private readonly IHighPrizeBusinessRule _highPrizeBusinessRule;
        private readonly IUnusualStakeBusinessRule _unusualStakeBusinessRule;
        private readonly IUnusuallyHighStakeBusinessRule _unusuallyHighStakeBusinessRule;
        private readonly IUnusualWinRateBusinessRule _unusualWinRateBusinessRule;

        public RiskAnalysisService(IHighPrizeBusinessRule highPrizeBusinessRule,
            IUnusualStakeBusinessRule unusualStakeBusinessRule,
            IUnusuallyHighStakeBusinessRule unusuallyHighStakeBusinessRule,
            IUnusualWinRateBusinessRule unusualWinRateBusinessRule, IRepository repository)
        {
            _highPrizeBusinessRule = highPrizeBusinessRule;
            _unusualStakeBusinessRule = unusualStakeBusinessRule;
            _unusuallyHighStakeBusinessRule = unusuallyHighStakeBusinessRule;
            _unusualWinRateBusinessRule = unusualWinRateBusinessRule;
            _repository = repository;
        }

        public IEnumerable<SettledBet> ReadAllSettledBets(int customer)
        {
            return _repository.GetAllSettledBets().Where(p => p.Customer == customer);
        }

        public IEnumerable<UnsettledBet> ReadAllUnsettledBets()
        {
            return _repository.GetAllUnsettledBets();
        }
        public IEnumerable<CustomerStatics> GetCustomerStatics()
        {
            var settledBets = _repository.GetAllSettledBets();
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