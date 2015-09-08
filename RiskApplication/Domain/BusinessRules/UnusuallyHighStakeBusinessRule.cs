using Domain.BusinessRules.Interfaces;
using Domain.Models;

namespace Domain.BusinessRules
{
    public class UnusuallyHighStakeBusinessRule : IUnusuallyHighStakeBusinessRule
    {
        public bool IsSatisfied(UnsettledBetWithStatistics source)
        {
            if (source == null || source.UnsettledBet == null || source.CustomerStatics == null)
            {
                return false;
            }
            return source.CustomerStatics.AverageStake * 30 <= source.UnsettledBet.Stake;
        }
    }
}