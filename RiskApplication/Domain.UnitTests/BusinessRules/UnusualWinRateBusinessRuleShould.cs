using Domain.BusinessRules;
using Domain.Models;
using NUnit.Framework;

namespace Domain.UnitTests.BusinessRules
{
    [TestFixture]
    public class UnusualWinRateBusinessRuleShould
    {
        private UnusualWinRateBusinessRule _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new UnusualWinRateBusinessRule();
        }

        [Test]
        public void ReturnFalseWhenNullInput()
        {
            Assert.IsFalse(_sut.IsSatisfied(null));
        }

        [TestCase(100, 59, false)]
        [TestCase(100, 60, true)]
        [TestCase(100, 61, true)]
        public void ReturnExpectedResult(int totalBets, int totalWins, bool expected)
        {
            var result = _sut.IsSatisfied(new CustomerStatics {TotalBets = totalBets, TotalBetsWon = totalWins});
            Assert.AreEqual(result, expected);
        }
    }
}