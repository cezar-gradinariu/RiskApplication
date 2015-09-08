using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Models;

namespace Portal.Controllers
{
    public class RiskController : Controller
    {
        public ActionResult Index()
        {
            return View(new List<CustomerStatics>());
        }

        [Route("/Risk/GetSettledBet/{id}")]
        public ActionResult GetSettledBet(int id)
        {
            return PartialView("SettledBets", new List<SettledBet>());
        }
    }
}