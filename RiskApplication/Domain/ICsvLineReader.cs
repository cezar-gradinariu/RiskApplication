namespace Domain
{
    public interface ICsvLineReader<T> where T : class
    {
        T ReadLine(string line);
    }
}