using Domain;
using Domain.BusinessRules;
using Domain.BusinessRules.Interfaces;
using Domain.FileReaders;
using Domain.LineReaders;
using Domain.Models;
using Domain.Services;
using Domain.SystemWrappers;
using Ninject.Modules;

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
        }
    }
}