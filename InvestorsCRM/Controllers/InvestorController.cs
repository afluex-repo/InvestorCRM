using InvestorsCRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestorsCRM.Controllers
{
    public class InvestorController : InvestorBaseController
    {
        // GET: Investor
        public ActionResult InvestorDashBoard()
        {
            return View();
        }
        public ActionResult MyInvestment()
        {
            return View();
        }
        
    }
}