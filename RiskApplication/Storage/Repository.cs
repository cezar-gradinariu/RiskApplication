using System.Collections.Generic;
using Domain;
using Domain.Models;
using Storage.FileReaders;

namespace Storage
{
    public class Repository : IRepository
    {
        private readonly IFilePathsProvider _filePathsProvider;
        private readonly ICsvFileReader<SettledBet> _settledBetCsvFileReader;
        private readonly ICsvFileReader<UnsettledBet> _unsettledBetCsvFileReader;

        public Repository(IFilePathsProvider filePathsProvider, ICsvFileReader<SettledBet> settledBetCsvFileReader,
            ICsvFileReader<UnsettledBet> unsettledBetCsvFileReader)
        {
            _filePathsProvider = filePathsProvider;
            _settledBetCsvFileReader = settledBetCsvFileReader;
            _unsettledBetCsvFileReader = unsettledBetCsvFileReader;
        }

        public IEnumerable<SettledBet> GetAllSettledBets()
        {
            var path = _filePathsProvider.GetSettledBetsFilePath();
            return _settledBetCsvFileReader.ReadCsvFile(path);
        }

        public IEnumerable<UnsettledBet> GetAllUnsettledBets()
        {
            var path = _filePathsProvider.GetUnsettledBetsFilePath();
            return _unsettledBetCsvFileReader.ReadCsvFile(path);
        }
    }
}