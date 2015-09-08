using System.Web.Hosting;
using Domain;

namespace Portal
{
    public class FilePathsProvider : IFilePathsProvider
    {
        public string GetSettledBetsFilePath()
        {
            return HostingEnvironment.MapPath("~/app_data/Settled.csv");
        }

        public string GetUnsettledBetsFilePath()
        {
            return HostingEnvironment.MapPath("~/app_data/Unsettled.csv");
        }
    }
}