using System;
using System.Web;
using InvestorsCRM.Filter;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;
using InvestorsCRM.Models;
using System.Data;

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
       public ActionResult InvestorRegistration(Admin model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("InvestorRegistration")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveRegistration(Admin model)
        {
            try
            {
                Random rnd = new Random();
                string GetPasword = rnd.Next(111111, 999999).ToString();
                model.Password = Crypto.Encrypt(GetPasword);
                model.CreatedBy = Session["UserID"].ToString();
                DataSet ds = model.Registration();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Registration saved successfully";
                        Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Password"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {

                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ConfirmRegistration", "Master");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
    }

    
}