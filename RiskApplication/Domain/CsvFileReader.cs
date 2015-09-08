using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class CsvFileReader<T> : ICsvFileReader<T> where T : class
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICsvLineReader<T> _lineReader; 

        public CsvFileReader(IFileSystem fileSystem, ICsvLineReader<T> lineReader)
        {
            _fileSystem = fileSystem;
            _lineReader = lineReader;
        }

        public IEnumerable<T> ReadCsvFile(string filePath)
        {
            if (!_fileSystem.Exists(filePath))
            {
                return new List<T>();
            }
            var lines = _fileSystem.ReadAllLines(filePath);
            if (lines == null)
            {
                return new List<T>();
            }
            var valuedLines = lines.Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            if (valuedLines.Count() <= 1) //empty file or only header is present
            {
                return new List<T>();
            }
            return valuedLines.Skip(1) //skip header.
                .Select(_lineReader.ReadLine);
        }
    }
}