using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestorsCRM.Controllers
{
    public class InvestorController : Controller
    {
        // GET: Investor
        public ActionResult Index()
        {
            return View();
        }
    }
}