using Domain.BusinessRules.Interfaces;
using Domain.Models;

namespace Domain.BusinessRules
{
    public class UnusualStakeBusinessRule : IUnusualStakeBusinessRule
    {
        public bool IsSatisfied(UnsettledBetWithStatistics source)
        {
            if (source == null || source.UnsettledBet == null || source.CustomerStatics == null)
            {
                return false;
            }
            return source.CustomerStatics.AverageStake*10 <= source.UnsettledBet.Stake;
        }
    }
}