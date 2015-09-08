using Domain.BusinessRules.Interfaces;
using Domain.Models;

namespace Domain.BusinessRules
{
    public class HighPrizeBusinessRule :IHighPrizeBusinessRule
    {
        public bool IsSatisfied(UnsettledBet unsettledBet)
        {
            return unsettledBet != null && unsettledBet.ToWin >= 1000;
        }
    }
}