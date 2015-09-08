using Domain;
using Domain.BusinessRules;
using Domain.BusinessRules.Interfaces;
using Domain.Models;
using Domain.Services;
using Ninject.Modules;
using Storage;
using Storage.FileReaders;
using Storage.LineReaders;
using Storage.SystemWrappers;

namespace Portal
{
    public class PortalNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICsvFileReader<SettledBet>>().To<CsvFileReader<SettledBet>>();
            Bind<ICsvFileReader<UnsettledBet>>().To<CsvFileReader<UnsettledBet>>();
            Bind<ICsvLineReader<SettledBet>>().To<SettledBetCsvLineReader>();
            Bind<ICsvLineReader<UnsettledBet>>().To<UnsettledBetCsvLineReader>();
            Bind<IFileSystem>().To<FileSystem>();
            Bind<IHighPrizeBusinessRule>().To<HighPrizeBusinessRule>();
            Bind<IUnusualStakeBusinessRule>().To<UnusualStakeBusinessRule>();
            Bind<IUnusuallyHighStakeBusinessRule>().To<UnusuallyHighStakeBusinessRule>();
            Bind<IUnusualWinRateBusinessRule>().To<UnusualWinRateBusinessRule>();
            Bind<IRiskAnalysisService>().To<RiskAnalysisService>();
            Bind<IFilePathsProvider>().To<FilePathsProvider>();
            Bind<IRepository>().To<Repository>();
        }
    }
}