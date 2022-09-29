﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvestorsCRM.Models
{
    public class User
    {
        public string LoginID { get; set; }
        public string Image { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string PanNo { get; set; }
        public string Amount { get; set; }
        public string Agreement { get; set; }
        public string Address { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string AddedBy { get; set; }
        public List<User> lstInvestor { get; set; }
        public List<User> lstDirect { get; set; }
        public List<User> lstTeam { get; set; }


        public DataSet GetInvestorDetails()
        {
            SqlParameter[] para =
            {
               new  SqlParameter ("@LoginId",LoginID)
            };
            DataSet ds = Connection.ExecuteQuery("GetInvestorDetails", para);
            return ds;
        }
        
        public DataSet GetUserDashBoardDetails()
        {
            DataSet ds = Connection.ExecuteQuery("GetUserDashBoardDetails");
            return ds;
        }


        public DataSet GetDirectUser()
        {
            SqlParameter[] para =
            {
               new  SqlParameter ("@LoginId",LoginID)
            };
            DataSet ds = Connection.ExecuteQuery("GetDirectUserList", para);
            return ds;
        }
        
        public DataSet GetTeamUser()
        {
            SqlParameter[] para =
            {
               new  SqlParameter ("@LoginId",LoginID)
            };
            DataSet ds = Connection.ExecuteQuery("GetTeamUserList", para);
            return ds;
        }
        

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