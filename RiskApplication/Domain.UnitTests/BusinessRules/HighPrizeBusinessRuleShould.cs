using Domain.BusinessRules;
using Domain.Models;
using NUnit.Framework;

namespace Domain.UnitTests.BusinessRules
{
    [TestFixture]
    public class HighPrizeBusinessRuleShould
    {
        private HighPrizeBusinessRule _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new HighPrizeBusinessRule();
        }

        [Test]
        public void ReturnFalseWhenNullInput()
        {
            var result = _sut.IsSatisfied(null);
            Assert.IsFalse(result);
        }

        [TestCase(999, false)]
        [TestCase(1000, true)]
        [TestCase(1001, true)]
        public void ReturnExpectedResult(int prize, bool expected)
        {
            var bet = new UnsettledBet {ToWin = prize};
            var result = _sut.IsSatisfied(bet);
            Assert.AreEqual(expected, result);
        }
    }
}