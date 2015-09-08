using Domain.BusinessRules;
using Domain.Models;
using NUnit.Framework;

namespace Domain.UnitTests.BusinessRules
{
    [TestFixture]
    public class UnusualStakeBusinessRuleShould
    {
        private UnusualStakeBusinessRule _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new UnusualStakeBusinessRule();
        }

        [Test]
        public void ReturnFalseWhenNullInput()
        {
            var result = _sut.IsSatisfied(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnFalseWhenNullStatistics()
        {
            var result =
                _sut.IsSatisfied(new UnsettledBetWithStatistics
                {
                    CustomerStatics = null,
                    UnsettledBet = new UnsettledBet()
                });
            Assert.IsFalse(result);
        }
        [Test]
        public void ReturnFalseWhenNullUnsettledBet()
        {
            var result =
                _sut.IsSatisfied(new UnsettledBetWithStatistics
                {
                    CustomerStatics = new CustomerStatics(),
                    UnsettledBet = null
                });
            Assert.IsFalse(result);
        }

        [TestCase(10, 99, false)]
        [TestCase(10, 100, true)]
        [TestCase(10, 101, true)]
        public void ReturnExpectedResult(int averageStake, int currentStake, bool expected)
        {
            var bet = new UnsettledBet { Stake = currentStake };
            var statistics = new CustomerStatics { AverageStake = averageStake };
            var result = _sut.IsSatisfied(new UnsettledBetWithStatistics
            {
                CustomerStatics = statistics,
                UnsettledBet = bet
            });
            Assert.AreEqual(expected, result);
        }

    }
}