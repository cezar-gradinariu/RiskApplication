namespace Storage
{
    public interface IFilePathsProvider
    {
        string GetSettledBetsFilePath();
        string GetUnsettledBetsFilePath();
    }
}