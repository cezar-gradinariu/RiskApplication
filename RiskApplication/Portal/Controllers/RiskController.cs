using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Domain.Services;
using Portal.Models;

namespace Portal.Controllers
{
    public class RiskController : Controller
    {
        private readonly IRiskAnalysisService _riskAnalysisService;

        public RiskController(IRiskAnalysisService riskAnalysisService)
        {
            _riskAnalysisService = riskAnalysisService;
        }

        public ActionResult Index()
        {
            var betsAndRisks = _riskAnalysisService.GetUnsettledBetsWithRiskAnalysis();
            var list = Mapper.Map<IEnumerable<UnsettledBetAndRiskViewModel>>(betsAndRisks);
            return View(list);
        }
        public ActionResult History()
        {
            var list = _riskAnalysisService.GetCustomerStatics();
            return View(list);
        }

        [Route("/Risk/GetSettledBet/{id}")]
        public ActionResult GetSettledBet(int id)
        {
            var list = _riskAnalysisService.ReadAllSettledBets(id);
            return PartialView("SettledBets", list);
        }
    }
}