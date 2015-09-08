using System.Collections.Generic;

namespace Storage.FileReaders
{
    public interface ICsvFileReader<T> where T: class
    {
        IEnumerable<T> ReadCsvFile(string filePath);
    }
}