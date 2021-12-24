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
        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginID",LogingID),
                                  new SqlParameter("@password",Psssword),};
            DataSet ds = Connection.ExecuteQuery("Login", para);                 //Connetion.ExecuteQuery();
            return ds;
        }
    }
}