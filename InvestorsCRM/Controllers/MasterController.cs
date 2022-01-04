using System;
using System.Collections.Generic;
using InvestorsCRM.Filter;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvestorsCRM.Models;
using System.Data;

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
                      //  obj1.ProjectName = ds1.Tables[0].Rows[0]["ProjectName"].ToString();
                      //  obj1.FK_ProjectID = ds1.Tables[0].Rows[0]["PK_ProjectID"].ToString();
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
    }
}