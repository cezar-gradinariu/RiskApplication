using Domain.Models;

namespace Domain.BusinessRules
{
    public class HighPrizeBusinessRule
    {
        public bool IsSatisfied(UnsettledBet unsettledBet)
        {
            return unsettledBet != null && unsettledBet.ToWin >= 1000;
        }
    }
}