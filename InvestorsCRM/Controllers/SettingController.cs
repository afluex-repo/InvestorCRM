using InvestorsCRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestorsCRM.Controllers
{
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult ChangePasswordForUser()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ChangePasswordForUser")]
        public ActionResult ChangePasswordForUser(Setting model)
        {
            try
            {
                model.OldPassword = Crypto.Encrypt(model.OldPassword);
                model.NewPassword = Crypto.Encrypt(model.NewPassword);
                model.AddedBy = Session["PK_UserId"].ToString();
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Password changed  successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ChangePasswordForUser", "Setting");
        }

        public ActionResult ChangePasswordForInvestor()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ChangePasswordForInvestor")]
        public ActionResult ChangePasswordForInvestor(Setting model)
        {
            try
            {
                model.OldPassword = Crypto.Encrypt(model.OldPassword);
                model.NewPassword = Crypto.Encrypt(model.NewPassword);
                model.AddedBy = Session["PK_InvestorId"].ToString();
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Password changed  successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ChangePasswordForInvestor", "Setting");
        }

    }
}