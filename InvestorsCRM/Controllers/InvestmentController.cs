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
            
            List<Master> lst = new List<Master>();
            Master model = new Master();

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.InvestmentList();
            if(ds!=null && ds.Tables[0].Rows.Count>0 && ds.Tables.Count>0 )
            {
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    Master obj =new  Master();
                    obj.PK_InvestorID = r["PK_InvestorID"].ToString();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.FullName = r["InvestorName"].ToString();
                    obj.FK_SponsorId = r["SponsorLoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.TransactionNo  = r["TransactionNo"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Fk_Paymentid = r["PK_paymentID"].ToString();
                    obj.ProjectName = r["ProjectName"].ToString();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.PlanName = r["PlanName"].ToString();
                    obj.Image = r["Agreement"].ToString();
                    obj.InvestmentDate = r["InvestmentDate"].ToString();
                    obj.UserID = Crypto.Encrypt(r["PK_UserId"].ToString());
                    lst.Add(obj);
                }
                model.lstInvestment = lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult InvestmentList(Master model)
        {
            List<Master> lst = new List<Master>();
          
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.InvestmentList();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_InvestorID = r["PK_InvestorID"].ToString();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.FullName = r["InvestorName"].ToString();
                    obj.FK_SponsorId = r["SponsorLoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.TransactionNo = r["TransactionNo"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Fk_Paymentid = r["PK_paymentID"].ToString();
                    obj.ProjectName = r["ProjectName"].ToString();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.PlanName = r["PlanName"].ToString();
                    obj.Image = r["Agreement"].ToString();
                    obj.InvestmentDate = r["InvestmentDate"].ToString();
                    obj.UserID = Crypto.Encrypt(r["PK_UserId"].ToString());

                    lst.Add(obj);
                }
                model.lstInvestment = lst;
            }
            return View(model);
        }
        public ActionResult InvestmentForm(string Id)
        {
            //List<SelectListItem> ddlprojectname = new List<SelectListItem>();

            Master obj = new Master();
            if(Id != null)
            {
                obj.UserID = Crypto.Decrypt(Id);
                DataSet dsid = obj.GetSponsorForInvestmentid();
                if (dsid != null && dsid.Tables[0].Rows.Count > 0 && dsid.Tables.Count > 0)
                {
                    obj.InvestorId = dsid.Tables[0].Rows[0]["LoginID"].ToString();
                    obj.SponsorName = dsid.Tables[0].Rows[0]["SponsorName"].ToString();
                    obj.UserID = dsid.Tables[0].Rows[0]["PK_UserID"].ToString();
                    obj.LoginID = dsid.Tables[0].Rows[0]["SponsorLoginId"].ToString();
                    obj.Investorname = dsid.Tables[0].Rows[0]["Name"].ToString();
                    obj.PK_CompanyID= dsid.Tables[0].Rows[0]["FK_CompanyID"].ToString();
                    obj.PK_PlanID= dsid.Tables[0].Rows[0]["FK_PlanID"].ToString();
                    obj.Amount = dsid.Tables[0].Rows[0]["Amount"].ToString();
                    obj.Fk_Paymentid = dsid.Tables[0].Rows[0]["PaymentMode"].ToString();
                    obj.TransactionNo= dsid.Tables[0].Rows[0]["TransactionNo"].ToString();
                    obj.TransactionDate = dsid.Tables[0].Rows[0]["TransactionDate"].ToString();
                    obj.BankName = dsid.Tables[0].Rows[0]["BankName"].ToString();
                    obj.BranchName = dsid.Tables[0].Rows[0]["BranchName"].ToString();
                    obj.InvestmentDate = dsid.Tables[0].Rows[0]["InvestmentDate"].ToString();
                    obj.FK_ProjectID = dsid.Tables[0].Rows[0]["FK_ProjectID"].ToString();
                    obj.PK_InvestorID = dsid.Tables[0].Rows[0]["PK_InvestorID"].ToString();
                    obj.Image=dsid.Tables[0].Rows[0]["Agreement"].ToString();
                }
                //if (dsid != null && obj.PK_CompanyID == dsid.Tables[0].Rows[0]["FK_CompanyID"].ToString())
                //{

                //    List<SelectListItem> FK_ProjectID = new List<SelectListItem>();
                //    obj.PK_CompanyID = dsid.Tables[0].Rows[0]["FK_CompanyID"].ToString();
                //    DataSet dsblock = obj.GetCompanyProjectbyID();

                //    #region ddlProject
                //    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                //    {
                     
                //        foreach (DataRow dr in dsblock.Tables[0].Rows)
                //        {
                //            //ddlprojectname.Add(new SelectListItem { Text = dr["ProjectName"].ToString(), Value = dr["FK_ProjectID"].ToString() });
                //            int count3 = 0;
                //            if (count3 == 0)
                //            {
                //               // ddlprojectname.Add(new SelectListItem { Value = "0", Text = "Select Project" });
                //            }
                //            FK_ProjectID.Add(new SelectListItem { Text = dr["ProjectName"].ToString(), Value = dr["FK_ProjectID"].ToString() });
                //            count3 = count3 + 1;
                //        }

                //    }

                //    obj.ddlprojectname = FK_ProjectID;
                   

                //}
            }

            List<SelectListItem> ddlprojectname = new List<SelectListItem>();
            ddlprojectname.Add(new SelectListItem { Text = "Select", Value = "0" });
            ViewBag.ddlprojectname = ddlprojectname;
           
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
                    TempData["Error"] = "Investment Save SuccessFully";
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
        public ActionResult GetProjectName(string PK_CompanyID)
        {
            List<SelectListItem> ddlProject = new List<SelectListItem>();
            Master obj = new Master();
            obj.PK_CompanyID = PK_CompanyID;
            DataSet dsblock = obj.GetCompanyProjectbyID();

           
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    ddlProject.Add(new SelectListItem { Text = dr["ProjectName"].ToString(), Value = dr["FK_ProjectID"].ToString() });
                }

            }

            obj.ddlProject = ddlProject;
       

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSponsorName(string InvestorId)
        {
            try
            {
                Master model = new Master();
                model.LoginID = (InvestorId);
               // model.LoginID = InvestorId;

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
        [HttpPost]
        [ActionName("InvestmentForm")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateInvestmentList(Master model, HttpPostedFileBase PostedFile)
        {
            model.CreatedBy =  Session["UserID"].ToString();
            if (PostedFile != null)
            {
                model.Image = "../AgreementUploadFile/" + Guid.NewGuid() + Path.GetExtension(PostedFile.FileName);
                PostedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
            }
            model.InvestmentDate = string.IsNullOrEmpty(model.InvestmentDate) ? null : Common.ConvertToSystemDate(model.InvestmentDate, "dd/MM/yyyy");
            model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");
            DataSet Ds = model.UPDATEInvestment();
            if (Ds != null && Ds.Tables[0].Rows.Count > 0 && Ds.Tables.Count > 0 && Ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
            {
                TempData["update"] = "Investment Update SuccessFully";
            }
            else
            {
                TempData["update"] = Ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
          
            return RedirectToAction("InvestmentList", "Investment");
        }
    }
}