using Domain.BusinessRules.Interfaces;
using Domain.Models;

namespace Domain.BusinessRules
{
    public class UnusualWinRateBusinessRule : IUnusualWinRateBusinessRule
    {
        public bool IsSatisfied(CustomerStatics statistics)
        {
            return statistics != null && statistics.WinningPercentage >= 60;
        }
    }
}