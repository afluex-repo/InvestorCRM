using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvestorsCRM.Models
{
    public class Master
    {
       public string  CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string Designationame { get; set; }
        public string PlanName { get; set; }
        public string FK_ProjectID { get; set; }
        public string CreatedBy { get; set; }
        public string PK_ProjectID { get; set; }
        public List<Master> lstproject { get; set; }

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
        public DataSet updateprojectname()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_ProjectID",PK_ProjectID),
                 new SqlParameter("@ProjectName",ProjectName),
                  new SqlParameter("@createdby",CreatedBy),

            };
            DataSet ds = Connection.ExecuteQuery("UpdateProjectName", para);
            return ds;
        }
    }
}