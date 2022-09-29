using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvestorsCRM.Models
{
    public class Setting
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string AddedBy { get; set; }

        public DataSet ChangePassword()
        {
            SqlParameter[] para = {new SqlParameter("@OldPassword",OldPassword),
                                   new SqlParameter("@NewPassword",NewPassword),
                                   new SqlParameter("@UpdatedBy",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("ChangePasswordUser", para);
            return ds;


        }
    }
}