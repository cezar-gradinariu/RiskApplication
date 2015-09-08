using System.Collections.Generic;

namespace Domain.FileReaders
{
    public interface ICsvFileReader<T> where T: class
    {
        IEnumerable<T> ReadCsvFile(string filePath);
    }
}