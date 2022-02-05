using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InvestorsCRM.Models;
using System.Web.Mvc;

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
        public List<Master> lstDesignation { get; set; }
        public List<Master> lstPlan { get; set; }
        public List<Master> lstRegistation { get; set; }
        public List<Master> lstInvestor { get; set; }
        public List<Master> lstInvestment { get; set; }
        public string DesignationName { get; set; }
        public string Percentage { get; set; }
        public string PK_DesignationID { get; set; }
        public string PK_PlanID { get; set; }
        public string[] FK_ProjectIDTO { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string ConfirmPassword { get; set; }
       // public DataTable DtCompanyDetail { get; set;}
        public List<Master> lstCompanyproject { get; set; }
        public List<ListProject> listProject { get; set; }
        public DataTable dtCompanyDetails { get; set; }
        public string SponsorName { get; set; }
  
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
        public string  Address { get; set; }
        public string TransPassword { get; set; }
        public string FullName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PK_InvestorID { get; set; }
        public string InvestorId { get; set; }
        public string Investorname { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
 public  string InvestmentDate { get; set; }
        public string EncryptKey { get; set; }
        public List<SelectListItem> ddlProject { get; set; }
        
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
            DataSet ds = Connection.ExecuteQuery("Registration", para);
            return ds;
        }

        public DataSet GetRegistartionDeatils()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginID),
                 new SqlParameter("@SponsorLoginId", FK_SponsorId),
                  new SqlParameter("@FromDate", FromDate),
                   new SqlParameter("@ToDate", ToDate)

            };
            DataSet ds = Connection.ExecuteQuery("GetRegistartionDeatils",para);
            return ds;
        }

        public DataSet GetStateCity()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", Pincode) };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;
        }
        public DataSet InsertProject()
        {
            SqlParameter[] para ={new SqlParameter ("@ProjectName",ProjectName),
                                  new SqlParameter("@CreatedBy",CreatedBy),};
            DataSet ds = Connection.ExecuteQuery("SaveProject", para);                 //Connetion.ExecuteQuery();
            return ds;
        }
        public DataSet GetSponsorName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForInvestor", para);
            return ds;
        }
        public DataSet GetNameByLoginID()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }
        public DataSet InsertCompany()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@CompanyName",CompanyName),
                 new SqlParameter("@AddedBy",CreatedBy),
                   new SqlParameter("@DtCompanyDetail",dtCompanyDetails)

            };
            DataSet ds = Connection.ExecuteQuery("SaveCompany", para);
            return ds;
        }
        public DataSet UpdateCompanyProject()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_CompanyID",PK_CompanyID),
                 new SqlParameter("@AddedBy",CreatedBy),
                   new SqlParameter("@DtCompanyDetail",dtCompanyDetails)

            };
            DataSet ds = Connection.ExecuteQuery("updateCompanyProject", para);
            return ds;
        }
        public DataSet GetProjectName()
        {
            SqlParameter [] para={
                 // new SqlParameter("@FK_ProjectID",FK_ProjectID)
            };
            DataSet ds = Connection.ExecuteQuery("ProjectForCompany", para);
            return ds;
        }
        public DataSet GetProjectList()
        {
            SqlParameter[] para ={
                 new SqlParameter("@FK_ProjectID",FK_ProjectID)
            };
            DataSet ds = Connection.ExecuteQuery("GetProjectList", para);
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
            DataSet ds = Connection.ExecuteQuery("CompanyName", para);
            return ds;
        }

        public DataSet GetCompany()
        {
            //SqlParameter[] para ={
            //      new SqlParameter("@PK_CompanyID",PK_CompanyID)
            //};
            DataSet ds = Connection.ExecuteQuery("CompanyName");
            return ds;
        }



        public DataSet DeleteCompanytName()
        {
            SqlParameter[] para ={
                  new SqlParameter("@PK_CompanyID ",PK_CompanyID ),
                   new SqlParameter("@deletedby",CreatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteCompanyName", para);
            return ds;
        }
        public DataSet DeleteCompanytProject()
        {
            SqlParameter[] para ={
                  new SqlParameter("@FK_ProjectID",FK_ProjectID ),
                   new SqlParameter("@deletedby",CreatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteCompanyProject", para);
            return ds;
        }
        public DataSet GetCompanyProjectName()
        {
            SqlParameter[] para ={
                  new SqlParameter("@PK_CompanyID ",PK_CompanyID )
            };
            DataSet ds = Connection.ExecuteQuery("GetCompanyName", para);
            return ds;
        }

        public DataSet GetCompanyProjectbyID()
        {
            SqlParameter[] para ={
                  new SqlParameter("FK_CompanyId ",PK_CompanyID )
            };
            DataSet ds = Connection.ExecuteQuery("GetCompanyProjectname", para);
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

        public DataSet GetDasignationList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_DesignationID",PK_DesignationID)

            };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList", para);
            return ds;
        }

        public class ListProject
        {
            public string ddlprojectname { get; set; }
        }
        public DataSet DeleteDasignationList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_DesignationID",PK_DesignationID),
                   new SqlParameter("@DeletedBy",CreatedBy)


            };
            DataSet ds = Connection.ExecuteQuery("DeleteDesignationList", para);
            return ds;
        }

        public DataSet SavePlan()
        {
            SqlParameter[] para={
                new SqlParameter("@PlanName ",PlanName),
                 new SqlParameter("@CreatedBy ",CreatedBy)

            };
            DataSet ds = Connection.ExecuteQuery("SavePlan", para);
            return ds;
        }
        public DataSet GetPlan()
        {
            SqlParameter[] para ={
                new SqlParameter("@PK_PlanID ",PK_PlanID)
               

            };
            DataSet ds = Connection.ExecuteQuery("GetPlan", para);
            return ds;
        }
        public DataSet GetPlanName()
        {
            SqlParameter[] para ={
                new SqlParameter("@PK_PlanID ",PK_PlanID)


            };
            DataSet ds = Connection.ExecuteQuery("GetPlan", para);
            return ds;
        }
        public DataSet UpdatePlan()
        {
            SqlParameter[] para =
            {
               new  SqlParameter ("@Pk_PlanID",PK_PlanID),
               new  SqlParameter ("@PlanName",PlanName),
               new SqlParameter("@CreatedBy",CreatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("UpdatePlan", para);
            return ds;
        }
        public DataSet ChnagePassword()
        {
            SqlParameter[] para =
            {
               new  SqlParameter ("@Password",NewPassword),
            
               new SqlParameter("@PK_AdminId",CreatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("ChangePassword", para);
            return ds;
        }

        public DataSet InvestorRegistration()
        {
            SqlParameter[] para ={

                                 new SqlParameter("@FK_SponsorId",FK_SponsorId),
                                 //  new SqlParameter("@FK_UserID",UserID),
                                   new SqlParameter("@CreatedBy",CreatedBy),
                                   new SqlParameter("@name",Username),
                                    new SqlParameter("@Password",Password),
                                     new SqlParameter("@Amount",Amount),
                                     new SqlParameter("@Pk_PlanID",PK_PlanID),
                                      new SqlParameter("@agreementImage",Image),
                                      new SqlParameter("@PanNo",PanNo),
                                     new SqlParameter("@Mobile",Mobile),
                                     new SqlParameter("@Email",EmailId),
                                     new SqlParameter("@AdharNo",AdharNo),
                                     new SqlParameter("@FK_ProjectID",FK_ProjectID),
                                     new SqlParameter("@FK_CompanyID",PK_CompanyID),
                                      new SqlParameter("@FatherName",FatherName),
                                      new SqlParameter("@FirstName",FirstName),
                                      new SqlParameter("@LastName",LastName),
                                      new SqlParameter("@BankName",BankName),
                                     new SqlParameter("@IFSCCode",IFSCCode),
                                     new SqlParameter("@Address",Address),
                                     new SqlParameter("@BankAccount",BankAccount),
                                     new SqlParameter("@Pincode",Pincode),
                                     new SqlParameter("@City",City),
                                      new SqlParameter("@State",State)

            };
            DataSet ds = Connection.ExecuteQuery("InvestmentForm", para);                 //Connetion.ExecuteQuery();
            return ds;
        }
        public DataSet InvestmentForm()
        {
            SqlParameter[] para ={

                                 new SqlParameter("@InvestmentDate",InvestmentDate),
                                    new SqlParameter("@FK_UserID",UserID),
                                   new SqlParameter("@FK_SponsorID",FK_SponsorId),
                                    new SqlParameter("@Amount",Amount),
                                     new SqlParameter("@FK_PlanID",PK_PlanID),
                                     new SqlParameter("@FK_ProjectID",FK_ProjectID),
                                      new SqlParameter("@Agreement",Image),
                                      new SqlParameter("@FK_CompanyID",PK_CompanyID),
                                     new SqlParameter("@CreatedBy",UserID),
                                     new SqlParameter("@PaymentMode",Fk_Paymentid),
                                     new SqlParameter("@TransactionNo",TransactionNo),
                                     new SqlParameter("@TransactionDate",TransactionDate),
                                     new SqlParameter("@BankName",BankName),
                                      new SqlParameter("@BranchName",BranchName),
            };
            DataSet ds = Connection.ExecuteQuery("SaveInvestment", para);   
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


        public DataSet InvestmentList()
        {
            SqlParameter[] para =
            {
               new  SqlParameter ("@PK_InvestorID",PK_InvestorID),
                new  SqlParameter ("@FromDate",FromDate),
                 new  SqlParameter ("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetInvestmentDetails", para);
            return ds;
        }

        public DataSet GetSponsorForInvestmentid()
        {
            SqlParameter[] para =
           {
               new  SqlParameter ("@LoginID",UserID)
              
            };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForInvestmentid", para);
            return ds;
        }
        public DataSet UPDATEInvestment()
        {
            SqlParameter[] para =
          {
               new  SqlParameter ("@FK_UserID",UserID),
               new  SqlParameter ("@PK_InvestorID",PK_InvestorID),
               new  SqlParameter ("@Amount ",Amount),
              new  SqlParameter ("@Agreement",Image),
               new  SqlParameter ("@Updatedby",CreatedBy),
             new  SqlParameter ("@PaymentMode",Fk_Paymentid),
               new  SqlParameter ("@TransactionNo",TransactionNo),
                 new  SqlParameter ("@TransactionDate",TransactionDate),
             new  SqlParameter ("@BankName",BankName),
               new  SqlParameter ("@BranchName",BranchName),
               new SqlParameter("@InvestmentDate",InvestmentDate),
               new SqlParameter("@FK_ProjectID",FK_ProjectID),
               new SqlParameter("@FK_CompanyID",PK_CompanyID),
                   new SqlParameter("@FK_PlanID",PK_PlanID)

            };
            DataSet ds = Connection.ExecuteQuery("UPDATEInvestment", para);
            return ds;
        }
    }
}