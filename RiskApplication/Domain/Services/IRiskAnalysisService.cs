using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
    public interface IRiskAnalysisService
    {
        IEnumerable<SettledBet> ReadAllSettledBets(int customer);
        IEnumerable<UnsettledBet> ReadAllUnsettledBets();
        IEnumerable<CustomerStatics> GetCustomerStatics();
    }
}