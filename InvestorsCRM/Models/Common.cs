using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvestorsCRM.Models
{
    public class Common
    {
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string PanCard { get; set; }
        public string AdharCard { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Fk_Paymentid { get; set; }
        public DataSet PaymentList()
        {
            SqlParameter[] para =
            {
                  new SqlParameter("@FK_paymentID", Fk_Paymentid),
            };
            DataSet ds = Connection.ExecuteQuery("GetPaymentModeList", para);
            return ds;
        }
    }
}