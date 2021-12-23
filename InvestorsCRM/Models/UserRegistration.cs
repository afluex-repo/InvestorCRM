using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvestorsCRM.Models
{
    public class UserLogin
    {

        public string LogingID { get; set; }
        public string Psssword { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }



        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginID",LogingID),
                                  new SqlParameter("@password",Psssword),};
            DataSet ds = Connection.ExecuteQuery("Login", para);                 //Connetion.ExecuteQuery();
            return ds;
        }
    }
}