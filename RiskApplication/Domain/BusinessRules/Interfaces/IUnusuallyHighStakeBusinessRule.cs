﻿using Domain.Models;

namespace Domain.BusinessRules.Interfaces
{
    public interface IUnusuallyHighStakeBusinessRule : IBusinessRule<UnsettledBetWithStatistics>
    {
    }
}