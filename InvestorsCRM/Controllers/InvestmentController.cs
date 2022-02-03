using InvestorsCRM.Filter;
using InvestorsCRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestorsCRM.Controllers
{
    public class InvestmentController : BaseController
    {
        // GET: Investment
        public ActionResult InvestmentList()
        {
            return View();
        }
        public ActionResult InvestmentForm()
        {


            #region ddlprojectname

            Master obj = new Master();

            List<SelectListItem> ddlprojectname = new List<SelectListItem>();
            ddlprojectname.Add(new SelectListItem { Text = "Select", Value = "0" });
            ViewBag.ddlprojectname = ddlprojectname;
            #endregion
            #region plan
            int count3 = 0;

            List<SelectListItem> ddlplan = new List<SelectListItem>();
            DataSet dPlan = obj.GetPlanName();
            if (dPlan != null && dPlan.Tables.Count > 0 && dPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dPlan.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlplan.Add(new SelectListItem { Value = "0", Text = "Select Plan" });
                    }
                    ddlplan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PlanID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlplan = ddlplan;

            #endregion
            #region ddlCompanyname
            int count2 = 0;

            List<SelectListItem> ddlcompanyname = new List<SelectListItem>();
            DataSet ds11 = obj.GetCompany();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlcompanyname.Add(new SelectListItem { Value = "0", Text = "Select" });
                    }
                    ddlcompanyname.Add(new SelectListItem { Text = r["CompanyName"].ToString(), Value = r["PK_CompanyID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlcompanyname = ddlcompanyname;
            #endregion
            #region getComapny name

            //DataSet dsa = obj.GetNameByLoginID();
            //if (dsa != null && dsa.Tables[0].Rows.Count > 0)
            //{
            //  //  obj.Username = dsa.Tables[0].Rows[0]["Name"].ToString();
            //    obj.LoginID = dsa.Tables[0].Rows[0]["LoginID"].ToString();
            //    //obj.AssociateImage = dsa.Tables[0].Rows[0]["profilepic"].ToString();
            //    //obj.FK_SponsorId= dsa.Tables[0].Rows[0]["PK_UserID"].ToString();


            //}
            //else
            //{
            //    obj.SponsorName = "";

            //}
            #endregion
            #region PaymentMode
            Common com = new Common();
            List<SelectListItem> ddlPayment = new List<SelectListItem>();
            DataSet ds = com.PaymentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int paycount = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (paycount == 0)
                    {
                        ddlPayment.Add(new SelectListItem { Text = "Select Payment", Value = "0" });
                    }
                    ddlPayment.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    paycount++;
                }
            }

            ViewBag.ddlPayment = ddlPayment;

            #endregion
            return View(obj);
        }

        [HttpPost]
        [ActionName("InvestmentForm")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult InvestmentForm(Master model, HttpPostedFileBase PostedFile)
        {
            try
            {
                Random rnd = new Random();
                int ctrPasword = rnd.Next(111111, 999999);
                model.Password = Crypto.Encrypt(ctrPasword.ToString());
                model.CreatedBy = Session["UserID"].ToString();

                if (PostedFile != null)
                {
                    model.Image = "../AgreementUploadFile/" + Guid.NewGuid() + Path.GetExtension(PostedFile.FileName);
                    PostedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");
                DataSet Ds = model.InvestmentForm();
                if (Ds != null && Ds.Tables[0].Rows.Count > 0 && Ds.Tables.Count > 0 && Ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    TempData["Error"] = "Investor Registration Save SuccessFully";
                }
                else
                {
                    TempData["Error"] = Ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("InvestmentForm", "Investment");
        }

        public ActionResult GetSponsorName(string InvestorId)
        {
            try
            {
                Master model = new Master();
                model.LoginID = InvestorId;

                #region GetSiteRate
                DataSet dsSponsorName = model.GetSponsorName();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.InvestorId = dsSponsorName.Tables[0].Rows[0]["LoginID"].ToString();
                    model.PanNo = dsSponsorName.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.Pincode = dsSponsorName.Tables[0].Rows[0]["PinCode"].ToString();
                    model.State = dsSponsorName.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsSponsorName.Tables[0].Rows[0]["City"].ToString();
                    model.Mobile = dsSponsorName.Tables[0].Rows[0]["Mobile"].ToString();
                    model.EmailId = dsSponsorName.Tables[0].Rows[0]["Email"].ToString();
                    model.SponsorName = dsSponsorName.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.FK_SponsorId = dsSponsorName.Tables[0].Rows[0]["Fk_sponsorid"].ToString();
                    model.LoginID= dsSponsorName.Tables[0].Rows[0]["SponsorLoginId"].ToString();
                    model.Investorname = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.SponsorName = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


    }
}