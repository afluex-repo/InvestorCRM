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
       public ActionResult InvestorRegistration(string ID)
        {
            Admin obj = new Admin();
            if(ID!=null)
            {
                obj.LoginID = (ID);
                DataSet ds = obj.GetInvestorDetails();
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                  
                    obj.LoginID = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    obj.Username = ds.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                    obj.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                    obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    obj.Image = ds.Tables[0].Rows[0]["Agreement"].ToString();
                    obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
                    obj.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    obj.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    obj.BankAccount = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    obj.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                    obj.BranchName = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                    obj.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                    obj.Pincode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                    obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                    obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    obj.UserID= ds.Tables[0].Rows[0]["PK_UserId"].ToString();

                }
            }
            return View(obj);
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

        [HttpPost]
        [ActionName("InvestorRegistration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateRegistration(Admin model)
        {
            try
            {
               
                model.CreatedBy = Session["UserID"].ToString();
                DataSet ds = model.UpdateRegistration();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Registration saved successfully";
                       
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
            return RedirectToAction("InvestorList", "Master");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }

    
}