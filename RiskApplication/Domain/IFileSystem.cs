namespace Domain
{
    public interface IFileSystem
    {
        string[] ReadAllLines(string filePath);
    }
}