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
            DataSet ds = model.GetProjectName();
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
                        obj1.ProjectName = ds1.Tables[0].Rows[0]["ProjectName"].ToString();
                        obj1.FK_ProjectID = ds1.Tables[0].Rows[0]["PK_ProjectID"].ToString();
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
            List<SelectListItem> ddlprojectname = new List<SelectListItem>();
            DataSet ds = obj.GetProjectName();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlprojectname.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlprojectname.Add(new SelectListItem { Text = r["ProjectName"].ToString(), Value = r["PK_ProjectID"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlprojectname = ddlprojectname;
            #endregion

         

            return View(obj1);
        }

        [HttpPost]
        [ActionName("CompanyMaster")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult CompanyMaster(Master obj)
        {
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
            if(ds!=null && ds.Tables[0].Rows.Count>0)
            {
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.PK_CompanyID = r["PK_CompanyID"].ToString();
                    obj.ProjectName = r["ProjectName"].ToString();
                    obj.PK_ProjectID = r["PK_ProjectID"].ToString();
                    lst.Add(obj);
                }
                model.lstproject = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("CompanyMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateCompany( string PK_CompanyID, string CompanyName, string FK_ProjectID)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.PK_CompanyID = PK_CompanyID;
                obj.CompanyName = CompanyName;
                obj.FK_ProjectID = FK_ProjectID;
                obj.CreatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateCompanyName();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "CompanyList";
                        Controller = "Master";

                        TempData["Updated"] = "Company is Successfully Updated!";
                    }
                    else
                    {
                        Session["CompanyName"] = CompanyName;
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
        public ActionResult DesignationMaster()
        {
            return View();
        }
        [HttpPost]
        [ActionName("DesignationMaster")]
        [OnAction(ButtonName ="btnSave")]
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
            catch (Exception  ex)
            {
                TempData["Error"] = ex.Message;
               // throw ex;
            }
            
            return RedirectToAction("DesignationMaster","Master");
        }
        public ActionResult PLanMaster()
        {
            return View();
        }
       
    }
}