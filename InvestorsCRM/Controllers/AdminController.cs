using System;
using System.Web;
using InvestorsCRM.Filter;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;

namespace InvestorsCRM.Controllers
{
    public class AdminController : BaseController
    {
        // GET: AdminMaster
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult Registration()
        {
            return View();
        }
       [HttpPost]
       [ActionName("Registration")]
       [OnAction(ButtonName ="btnsave")]
        public ActionResult SavrRegistration()
        { 

            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }

    
}