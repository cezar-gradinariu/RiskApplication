using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class CsvFileReader<T> : ICsvFileReader<T> where T : class
    {
        private readonly IFileSystem _fileSystem;

        public CsvFileReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IEnumerable<T> ReadCsvFile(string filePath)
        {
            var lines = _fileSystem.ReadAllLines(filePath);
            if (lines == null)
            {
                return new List<T>();
            }
            var valuedLines = lines.Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            if (valuedLines.Count() <= 1) //emtpy file or only header is present
            {
                return new List<T>();
            }

            return null;
        }
    }
}