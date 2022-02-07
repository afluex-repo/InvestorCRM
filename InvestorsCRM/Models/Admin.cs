using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InvestorsCRM.Models
{
    public class Admin :Common
    {
        public string UserID { get; set; }
        public string AssociateImage { get; set; }
        public string Result { get; set; }
        public string ddlprojectname { get; set; }
        public string Agreement { get; set; }
        public string Image { get; set; }
        public string FK_SponsorId { get; set; }
        public string AdharNo { get; set; }
        public string Mobile { get; set; }
        public string PanNo { get; set; }
        public string FirstName { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string TransPassword { get; set; }
        public string FullName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string EmailId { get; set; }
        public string CreatedBy { get; set; }
        public DataSet Registration()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@FirstName",FirstName),
                                     new SqlParameter("@LastName",LastName),
                                     new SqlParameter("@MobileNo",Mobile),
                                      new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@PanNo",PanNo),
                                     new SqlParameter("@AddharNo",AdharNo),
                                     new SqlParameter("@AccoutnNo",BankAccount),
                                     new SqlParameter("@BankName",BankName),
                                     new SqlParameter("@BranchName",BranchName),
                                     new SqlParameter("@IFSC",IFSCCode),
                                      new SqlParameter("@PinCode",Pincode),
                                      new SqlParameter("@City",City),
                                      new SqlParameter("@State",State),
                                     new SqlParameter("@Address",Address),
                                     new SqlParameter("@Password",Password),
                                        new SqlParameter("@TransPassword",Password),
                                     new SqlParameter("@AddedBy",CreatedBy),
                                     new SqlParameter("@FK_SponsorId",FK_SponsorId)
            };
            DataSet ds = Connection.ExecuteQuery("InvestorRegistration", para);
            return ds;
        }

        public DataSet GetInvestorDetails()
        {
            SqlParameter[] para =
            {
               new  SqlParameter ("@LoginId",LoginID)
            };
            DataSet ds = Connection.ExecuteQuery("GetInvestorDetails", para);
            return ds;
        }

        public DataSet UpdateRegistration()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@FirstName",FirstName),
                                     new SqlParameter("@LastName",LastName),
                                     new SqlParameter("@MobileNo",Mobile),
                                      new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@PanNo",PanNo),
                                     new SqlParameter("@AddharNo",AdharNo),
                                     new SqlParameter("@AccoutnNo",BankAccount),
                                     new SqlParameter("@BankName",BankName),
                                     new SqlParameter("@BranchName",BranchName),
                                     new SqlParameter("@IFSC",IFSCCode),
                                      new SqlParameter("@PinCode",Pincode),
                                      new SqlParameter("@City",City),
                                      new SqlParameter("@State",State),
                                     new SqlParameter("@Address",Address),
                                     new SqlParameter("@AddedBy",CreatedBy),
                                      new SqlParameter("@Pk_UserId",UserID),

            };
            DataSet ds = Connection.ExecuteQuery("UpdateInvestorRegistration", para);
            return ds;
        }

    }
}