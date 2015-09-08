namespace Domain
{
    public interface IFilePathsProvider
    {
        string GetSettledBetsFilePath();
        string GetUnsettledBetsFilePath();
    }
}