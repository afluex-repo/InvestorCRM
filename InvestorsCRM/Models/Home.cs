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
       // public string LogingID { get; set; }
        
        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginID",base.LogingID),
                                  new SqlParameter("@password",Psssword),};
            DataSet ds = Connection.ExecuteQuery("Login", para);                 //Connetion.ExecuteQuery();
            return ds;
        }
    }
}