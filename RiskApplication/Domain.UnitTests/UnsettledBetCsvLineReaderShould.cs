using Domain.LineReaders;
using NUnit.Framework;

namespace Domain.UnitTests
{
    [TestFixture]
    public class UnsettledBetCsvLineReaderShould
    {
        [TestCase(null, Description = "On null line return null")]
        [TestCase("", Description = "On empty line return null")]
        [TestCase("  ", Description = "On whitespace line return null")]
        [TestCase("1,2,3", Description = "On less than 5 cells return null")]
        [TestCase("1,2,3,4,5,6", Description = "On more than 5 cells return null")]
        [TestCase("1,2,3,4,NaN", Description = "On non-integger value return null")]
        [TestCase("1,2,3,,5", Description = "When a cell is not valued return null")]
        public void ReturnNullOnInvalidLineInputs(string line)
        {
            var lineReader = new UnsettledBetCsvLineReader();
            var result = lineReader.ReadLine(line);
            Assert.IsNull(result);
        }

        [TestCase("1  , 2  ,3,  40 ,0  ", Description = "Should know how to read correctly when whitespaces are present")]
        [TestCase("1,2,3,40,0")]
        public void ReturnExpectedObject(string line)
        {
            var lineReader = new UnsettledBetCsvLineReader();
            var result = lineReader.ReadLine(line);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Customer);
            Assert.AreEqual(2, result.Event);
            Assert.AreEqual(3, result.Participant);
            Assert.AreEqual(40, result.Stake);
            Assert.AreEqual(0, result.ToWin);
        }
    }
}