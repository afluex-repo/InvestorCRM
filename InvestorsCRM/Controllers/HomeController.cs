using InvestorsCRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using InvestorsCRM.Filter;

namespace InvestorsCRM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Home obj)
        {
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
                        //Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                        return RedirectToAction("Index", "Master");
                    }

                    if (ds.Tables[0].Rows[0]["FK_Usertype"].ToString() != "1")
                    {
                        ViewBag.errormsg = "";
                        Session["UserID"] = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginID"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Username"].ToString();
                        Session["FK_UserTypeID"] = ds.Tables[0].Rows[0]["FK_Usertype"].ToString();
                      //  Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
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
        [HttpPost]
        [ActionName("ForgetPassword")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult ForgetPassword(Home model)
        {

            Random rnd = new Random();
            string ctrpass = rnd.Next(111111, 999999).ToString();
            model.NewPsssword = Crypto.Encrypt(ctrpass);
           
            DataSet ds = model.GetLoginDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                if (model.EmailId != null)
                {
                    string mailbody = "";
                    try
                    {
                        mailbody = "Dear  " + ds.Tables[0].Rows[0]["Username"].ToString() + "<br/>  <b>Login ID</b> :  " + model.LoginID + "<br/> <b>Passoword</b>  : " + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());


                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("developer5.afluex@gmail.com", "Afluex@123")

                        };

                        using (var message = new MailMessage("developer5.afluex@gmail.com", model.EmailId)
                        {
                            IsBodyHtml = true,
                            Subject = "Forget Password",
                            Body = mailbody
                        })
                            smtp.Send(message);
                        ViewBag.errormsg = "";
                        TempData["Error"] = "Sent SMS Successfully!";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.errormsg = "";
                        TempData["Error"] = ex.Message;
                    }

                }
            }
            else
            {
                ViewBag.errormsg = "";
                TempData["Error"] = "Invalied  EmailID !";
            }
            return RedirectToAction ("ForgetPassword", "Home");
        }
    }
}