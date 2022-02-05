using System;
using System.Collections.Generic;
using InvestorsCRM.Filter;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvestorsCRM.Models;
using System.Data;
using System.IO;

namespace InvestorsCRM.Controllers
{
    public class MasterController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProjectMaster(string FK_ProjectID)
        {
            Master obj = new Master();
            try
            {
                if (FK_ProjectID != null)
                {
                    obj.FK_ProjectID = FK_ProjectID;
                    DataSet ds = obj.GetProjectName();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                        obj.FK_ProjectID = ds.Tables[0].Rows[0]["PK_ProjectID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(obj);
        }
        [HttpPost]
        [ActionName("ProjectMaster")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult ProjectMaster(Master obj)
        {
            obj.CreatedBy = Session["UserID"].ToString();
            DataSet ds = obj.InsertProject();
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Error"] = "Project Name is Successfully Added";
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
            return RedirectToAction("ProjectMaster", "Master");
        }
        public ActionResult Projectlist()
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();
            DataSet ds = model.GetProjectList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProjectName = r["ProjectName"].ToString();
                    obj.FK_ProjectID = r["PK_ProjectID"].ToString();

                    lst1.Add(obj);
                }
                model.lstproject = lst1;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ProjectMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult updateProject(string FK_ProjectID, string ProjectName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.PK_ProjectID = FK_ProjectID;
                obj.ProjectName = ProjectName;
                obj.CreatedBy = Session["UserID"].ToString();
                DataSet ds = obj.updateprojectname();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "Projectlist";
                        Controller = "Master";

                        TempData["Delete"] = "Project is Successfully Updated!";
                    }
                    else
                    {
                        Session["PK_ProjectID"] = FK_ProjectID;
                        FormName = "ProjectMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult CompanyMaster(string PK_CompanyID)
        {
            Master obj1 = new Master();
            try
            {
                if (PK_CompanyID != null)
                {
                    obj1.PK_CompanyID = PK_CompanyID;
                    DataSet ds1 = obj1.GetCompanyname();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        obj1.CompanyName = ds1.Tables[0].Rows[0]["CompanyName"].ToString();
                        obj1.PK_CompanyID = ds1.Tables[0].Rows[0]["PK_CompanyID"].ToString();
              
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            #region ddlprojectname

            Master obj = new Master();
            int count1 = 0;
            List<SelectListItem> FK_ProjectID = new List<SelectListItem>();
            DataSet ds = obj.GetProjectName();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        FK_ProjectID.Add(new SelectListItem { Value = "0" });
                    }
                    FK_ProjectID.Add(new SelectListItem { Text = r["ProjectName"].ToString(), Value = r["PK_ProjectID"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.FK_ProjectID = FK_ProjectID;
            #endregion

            

            return View(obj1);
        }

        [HttpPost]
        [ActionName("CompanyMaster")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult CompanyMaster(Master obj)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Fk_ProjectId", typeof(string));
            if (obj.FK_ProjectIDTO != null)
            {
                string[] i1;
                i1 = obj.FK_ProjectIDTO;
                for (int i = 0; i < i1.Length; i++)
                {
                    /*string s = i1[i]; *//*Inside string type s variable should contain items values */
                    string Fk_Siteid = i1[i];
                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();
                    dr["Fk_ProjectId"] = Fk_Siteid;
                    dt.Rows.Add(dr);
                }
            }
            obj.dtCompanyDetails = dt;
            obj.CreatedBy = Session["UserID"].ToString();
            DataSet ds = obj.InsertCompany();
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Error"] = "Company Name is Successfully Added";
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


            return RedirectToAction("CompanyMaster", "Master");
        }
        public ActionResult CompanyList()
        {
            List<Master> lst = new List<Master>();
            Master model = new Master();
            DataSet ds = model.GetCompanyname();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.PK_CompanyID = r["PK_CompanyID"].ToString();
                    lst.Add(obj);
                }
                model.lstproject = lst;
            }
            return View(model);
        }
        public ActionResult DeleteCompany(string PK_CompanyID)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.PK_CompanyID = PK_CompanyID;
                obj.CreatedBy = Session["UserID"].ToString();
                DataSet ds = obj.DeleteCompanytName();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "CompanyList";
                        Controller = "Master";

                        TempData["Error"] = "Company is Deleted!";
                    }
                    else
                    {
                       
                        FormName = "CompanyMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult DesignationMaster(string PK_DesignationID)
        {
            Master obj = new Master();
            if (PK_DesignationID != null)
            {
                obj.PK_DesignationID = PK_DesignationID;
                DataSet ds = obj.GetDasignationList();
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    obj.Percentage = ds.Tables[0].Rows[0]["Percentage"].ToString();
                    obj.Designationame = ds.Tables[0].Rows[0]["DesignationName"].ToString();
                    obj.PK_DesignationID = ds.Tables[0].Rows[0]["PK_DesignationID"].ToString();
                }
            }
            return View(obj);
        }
        [HttpPost]
        [ActionName("DesignationMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult DesignationMaster(Master Obj)
        {
            Obj.CreatedBy = Session["UserID"].ToString();
            DataSet ds = Obj.InsertDesignation();
            try
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Error"] = "Designation Name is Successfully Added";
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
                // throw ex;
            }

            return RedirectToAction("DesignationMaster", "Master");
        }
        public ActionResult DesignationList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetDasignationList();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Designationame = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.PK_DesignationID = r["PK_DesignationID"].ToString();
                    lst.Add(obj);
                }
                model.lstDesignation = lst;
            }
            return View(model);
        }

        public ActionResult DesignationDelete(string PK_DesignationID)
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            model.PK_DesignationID = PK_DesignationID;
            model.CreatedBy = Session["UserID"].ToString();
            DataSet ds = model.DeleteDasignationList();
            try
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Error"] = "Designation Name is Successfully Deleted";
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

            return RedirectToAction("DesignationList", "Master");
        }
        public ActionResult PLanMaster(string PK_PlanID)
        {
            Master obj = new Master();
            try
            {
                obj.PK_PlanID = PK_PlanID;
                DataSet ds = obj.GetPlan();
                if (PK_PlanID != null)
                {

                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                    {
                        obj.PK_PlanID = ds.Tables[0].Rows[0]["PK_PlanID"].ToString();
                        obj.PlanName = ds.Tables[0].Rows[0]["PlanName"].ToString();
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

            return View(obj);
        }
        [HttpPost]
        [ActionName("PLanMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult PLanMaster(Master obj)
        {

            try
            {
                obj.CreatedBy = Session["UserID"].ToString();
                DataSet ds = obj.SavePlan();
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Error"] = "Plan Name Has Been Added SuccessFully";
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

            return RedirectToAction("PLanMaster", "Master");
        }
        public ActionResult PlanList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet Ds = model.GetPlan();
            try
            {
                if (Ds != null && Ds.Tables[0].Rows.Count > 0 && Ds.Tables.Count > 0)
                {

                    foreach (DataRow r in Ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.PlanName = r["PlanName"].ToString();
                        obj.PK_PlanID = r["PK_PlanID"].ToString();
                        lst.Add(obj);
                    }
                    model.lstPlan = lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }
        [ActionName("PLanMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePlan(string PK_PlanID, string PlanName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.PK_PlanID = PK_PlanID;
                obj.PlanName = PlanName;
                obj.CreatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdatePlan();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "PlanList";
                        Controller = "Master";

                        TempData["Updated"] = "Plan is Successfully Updated!";
                    }
                    else
                    {

                        FormName = "PlanMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult GetCompanyProject(string PK_CompanyID)
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            model.PK_CompanyID = PK_CompanyID;
            DataSet ds = model.GetCompanyProjectName();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProjectName = r["ProjectName"].ToString();
                    obj.FK_ProjectID = r["PK_ProjectID"].ToString();
                    obj.PK_CompanyID = r["PK_CompanyID"].ToString();
                    lst.Add(obj);
                }
                model.lstCompanyproject = lst;
            }
            return View(model);
        
        }

        public ActionResult DeleteCompanyProject(string FK_ProjectID)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.FK_ProjectID = FK_ProjectID;
                obj.CreatedBy = Session["UserID"].ToString();
                DataSet ds = obj.DeleteCompanytProject();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "CompanyList";
                        Controller = "Master";

                        TempData["Error"] = "Project is Deleted!";
                    }
                    else
                    {

                        FormName = "CompanyList";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }
        [ActionName("CompanyMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult updatecompanyProject(string PK_CompanyID, Master obj)
        {
             
            DataTable dt = new DataTable();
            dt.Columns.Add("Fk_ProjectId", typeof(string));
            if (obj.FK_ProjectIDTO != null)
            {
                string[] i1;
                i1 = obj.FK_ProjectIDTO;
                for (int i = 0; i < i1.Length; i++)
                {
                    /*string s = i1[i]; *//*Inside string type s variable should contain items values */
                    string Fk_Siteid = i1[i];
                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();
                    dr["Fk_ProjectId"] = Fk_Siteid;
                    dt.Rows.Add(dr);
                }
            }
            obj.dtCompanyDetails = dt;
            obj.CreatedBy = Session["UserID"].ToString();
            DataSet ds = obj.UpdateCompanyProject();
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Error"] = "Company Name is Updated Successfully";
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


            return RedirectToAction("CompanyMaster", "Master");
        }

        public ActionResult Registration(Master model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("Registration")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveRegistration(Master model)
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
       
        public ActionResult RegistrationList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetRegistartionDeatils();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.Password =Crypto.Decrypt(r["Password"].ToString());
                    obj.FullName = r["FullName"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.EmailId = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.AdharNo = r["AdharNumber"].ToString();
                    obj.BankAccount = r["MemberAccNo"].ToString();
                    obj.BankName = r["MemberBankName"].ToString();
                    obj.BranchName = r["MemberBranch"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.Pincode = r["PinCode"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    lst.Add(obj);
                }
                model.lstRegistation = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("RegistrationList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult RegistrationList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetRegistartionDeatils();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.FullName = r["FullName"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.EmailId = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.AdharNo = r["AdharNumber"].ToString();
                    obj.BankAccount = r["MemberAccNo"].ToString();
                    obj.BankName = r["MemberBankName"].ToString();
                    obj.BranchName = r["MemberBranch"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.Pincode = r["PinCode"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    lst.Add(obj);
                }
                model.lstRegistation = lst;
            }
            return View(model);
        }




        public ActionResult ConfirmRegistration()
        {
            return View();
        }
        

        public ActionResult GetLoginName(string LoginID)
        {
            try
            { 
            Master obj = new Master();
            obj.LoginID = LoginID;
            DataSet ds = obj.GetNameByLoginID();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                    obj.Username = ds.Tables[0].Rows[0]["Name"].ToString();
                    obj.FK_SponsorId = ds.Tables[0].Rows[0]["PK_UserID"].ToString();
                    obj.AssociateImage = ds.Tables[0].Rows[0]["profilepic"].ToString();
                    obj.Result = "yes";
            }
            else
            {
                    obj.SponsorName = "";
                    obj.Result = "no";
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                Master model = new Master();
                model.Pincode = Pincode;

                #region GetStateCity
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State = model.City = "";
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

        public ActionResult GetProjectName(string PK_CompanyID)
        {
            List<SelectListItem> ddlProject = new List<SelectListItem>();
            Master obj = new Master();
            obj.PK_CompanyID = PK_CompanyID;
            DataSet dsblock = obj.GetCompanyProjectbyID();

            #region ddlProject
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    ddlProject.Add(new SelectListItem { Text = dr["ProjectName"].ToString(), Value = dr["FK_ProjectID"].ToString() });
                }

            }

            obj.ddlProject = ddlProject;
            #endregion

            return Json(obj, JsonRequestBehavior.AllowGet);
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

       

      
        public ActionResult InvestorList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetInvestorDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.FK_SponsorId = r["FK_SponsorId"].ToString();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.FullName = r["InvestorName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Image = r["Agreement"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.EmailId = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.AdharNo = r["AdharNumber"].ToString();
                    obj.BankAccount = r["MemberAccNo"].ToString();
                    obj.BankName = r["MemberBankName"].ToString();
                    obj.BranchName = r["MemberBranch"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.Pincode = r["PinCode"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    lst.Add(obj);
                }
                model.lstInvestor = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("InvestorList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult InvestorList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetInvestorDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.FK_SponsorId = r["FK_SponsorId"].ToString();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.FullName = r["InvestorName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Image = r["Agreement"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.EmailId = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.AdharNo = r["AdharNumber"].ToString();
                    obj.BankAccount = r["MemberAccNo"].ToString();
                    obj.BankName = r["MemberBankName"].ToString();
                    obj.BranchName = r["MemberBranch"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.Pincode = r["PinCode"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    lst.Add(obj);
                }
                model.lstInvestor = lst;
            }
            return View(model);
        }


    }
}