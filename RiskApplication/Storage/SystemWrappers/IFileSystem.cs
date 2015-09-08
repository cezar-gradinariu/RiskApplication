namespace Storage.SystemWrappers
{
    public interface IFileSystem
    {
        string[] ReadAllLines(string filePath);
        bool Exists(string filePath);
    }
}