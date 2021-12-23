using InvestorsCRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestorsCRM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin obj)
        {
            if (obj.LogingID == null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter LoginId";
                return RedirectToAction("Login");

            }
            if (obj.Psssword== null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter Password";
                return RedirectToAction("Login");
            }
            try
            {
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["FK_Usertype"].ToString() == "1")
                    {
                        ViewBag.errormsg = "";
                        Session["UserID"] = ds.Tables[0].Rows[0]["PK_AdminId"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginID"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Username"].ToString();
                        Session["FK_UserTypeID"] = ds.Tables[0].Rows[0]["FK_Usertype"].ToString();

                        return RedirectToAction("Index", "Master");

                    }

                    if (ds.Tables[0].Rows[0]["FK_Usertype"].ToString() != "1")
                    {
                        ViewBag.errormsg = "";
                        Session["UserID"] = ds.Tables[0].Rows[0]["PK_AdminId"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginID"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Username"].ToString();
                        Session["FK_UserTypeID"] = ds.Tables[0].Rows[0]["FK_Usertype"].ToString();

                        return RedirectToAction("Index", "Home");

                    }

                    else
                    {
                        ViewBag.errormsg = "";
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        return RedirectToAction("Login");

                    }

                }
                else
                {
                    ViewBag.errormsg = "";
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = ex.Message;
                return RedirectToAction("Login", "Home");

            }


          
        }

        public ActionResult ForgetPassword()
        {
           
            return View();
        }
    }
}