using System;
using System.Web;
using InvestorsCRM.Filter;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;

namespace InvestorsCRM.Controllers
{
    public class AdminMasterController : BaseController
    {
        // GET: AdminMaster
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult UserRegistration()
        {
            return View();
        }
       [HttpPost]
       [ActionName("UserRegistration")]
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