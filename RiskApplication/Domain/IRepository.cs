using System.Collections.Generic;
using Domain.Models;

namespace Domain
{
    public interface IRepository
    {
        IEnumerable<SettledBet> GetAllSettledBets();
        IEnumerable<UnsettledBet> GetAllUnsettledBets();
    }
}