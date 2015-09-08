using System.Collections.Generic;

namespace Domain
{
    public interface ICsvFileReader<T> where T: class
    {
        IEnumerable<T> ReadCsvFile(string filePath);
    }
}