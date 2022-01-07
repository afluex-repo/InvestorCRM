using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvestorsCRM.Models;
using System.Data.SqlClient;
using System.Data;

namespace InvestorsCRM.Models
{
    public class Home:Common
    {
        public string EmailId { get; set; }
       public string NewPsssword { get; set; }
        public string AddedBy { get; set; }


        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginID",base.LoginID),
                                  new SqlParameter("@password",Password),};
            DataSet ds = Connection.ExecuteQuery("Login", para);                 //Connetion.ExecuteQuery();
            return ds;
        }
        public DataSet GetLoginDetails()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginID",base.LoginID),
                                  new SqlParameter("@EmailID",EmailId),};
            DataSet ds = Connection.ExecuteQuery("GetLoginDetails", para);                 //Connetion.ExecuteQuery();
            return ds;
        }
    }
}