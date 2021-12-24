using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InvestorsCRM.Models;
namespace InvestorsCRM.Models
{
    public class Master :Common
    {
       public string  CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string Designationame { get; set; }
        public string PlanName { get; set; }
        public string FK_ProjectID { get; set; }
        public string CreatedBy { get; set; }
        public string PK_ProjectID { get; set; }
        public  string PK_CompanyID { get; set; }
        public string EmailId { get; set; }
        public string Amount { get; set; }
        public List<Master> lstproject { get; set; }
        public List<Master> lstCompany { get; set; }
        public string DesignationName { get; set; }
        public string Percentage { get; set; }

        public DataSet InsertProject()
        {
            SqlParameter[] para ={new SqlParameter ("@ProjectName",ProjectName),
                                  new SqlParameter("@CreatedBy",CreatedBy),};
            DataSet ds = Connection.ExecuteQuery("SaveProject", para);                 //Connetion.ExecuteQuery();
            return ds;
        }
        public DataSet InsertCompany()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@CompanyName",CompanyName),
                 new SqlParameter("@CreatedBy",CreatedBy),
                 new SqlParameter("@FK_ProjectID",FK_ProjectID),

            };
            DataSet ds = Connection.ExecuteQuery("SaveCompany", para);
            return ds;
        }
        public DataSet GetProjectName()
        {
            SqlParameter [] para={
                  new SqlParameter("@FK_ProjectID",FK_ProjectID)
            };
            DataSet ds = Connection.ExecuteQuery("GetProjectList",para);
            return ds;
        }
        public DataSet UpdateCompanyName()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_CompanyID",PK_CompanyID),
                 new SqlParameter("@CompanyName",CompanyName),
                  new SqlParameter("@FK_ProjectID",FK_ProjectID),
                  new SqlParameter("@createdby",CreatedBy),

            };
            DataSet ds = Connection.ExecuteQuery("UpdateCompanyName", para);
            return ds;
        }
        public DataSet GetCompanyname()
        {
            SqlParameter[] para ={
                  new SqlParameter("@PK_CompanyID",PK_CompanyID)
            };
            DataSet ds = Connection.ExecuteQuery("GetCompanyName", para);
            return ds;
        }
        public DataSet updateprojectname()
        {
            SqlParameter[] para ={
                  new SqlParameter("@PK_ProjectID",PK_ProjectID),
                  new SqlParameter("@ProjectName",ProjectName),
                  new SqlParameter("@createdby",CreatedBy)

            };
            DataSet ds = Connection.ExecuteQuery("UpdateProjectName", para);
            return ds;
        }
        public DataSet InsertDesignation()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@DesignationName",DesignationName),
                new SqlParameter("@Percentage",Percentage),
                new SqlParameter("@CreatedBy",CreatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("SaveDesignation", para);
            return ds;
        }
    }
}