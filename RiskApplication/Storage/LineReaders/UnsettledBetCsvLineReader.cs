using System;
using System.Linq;
using Domain.Models;
using Storage.Extensions;

namespace Storage.LineReaders
{
    public class UnsettledBetCsvLineReader : ICsvLineReader<UnsettledBet>
    {
        public UnsettledBet ReadLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }
            var tokens = line.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Count() != 5)
            {
                return null;
            }
            var values = tokens.Select(p => p.ToNullableInt()).ToList();
            if (values.Any(p => p == null))
            {
                return null;
            }
            return new UnsettledBet
            {
                Customer = values[0].Value,
                Event = values[1].Value,
                Participant = values[2].Value,
                Stake = values[3].Value,
                ToWin = values[4].Value
            };
        }
    }
}