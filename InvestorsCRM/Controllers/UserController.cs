using InvestorsCRM.Filter;
using InvestorsCRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestorsCRM.Controllers
{
    public class UserController : UserBaseController
    {
        // GET: User
        public ActionResult UserDashBoard(User obj)
        {
            //DataSet ds1 = obj.GetUserDashBoardDetails();
            //if (ds1 != null && ds1.Tables[0].Rows.Count > 0 && ds1.Tables.Count > 0)
            //{
            //    ViewBag.TotalTeam = ds1.Tables[0].Rows[0]["TotalTeam"].ToString();
            //    ViewBag.DirectInvestor = ds1.Tables[0].Rows[0]["DirectInvestor"].ToString();
            //    ViewBag.TeamInvestor = ds1.Tables[0].Rows[0]["TeamInvestor"].ToString();
            //    ViewBag.TotalInvestmentAmount = ds1.Tables[0].Rows[0]["TotalInvestmentAmount"].ToString();
            //    ViewBag.TotalReturn = ds1.Tables[0].Rows[0]["TotalReturn"].ToString();
            //    ViewBag.TotalPayout = ds1.Tables[0].Rows[0]["TotalPayout"].ToString();
            //}
            return View();
        }
        public ActionResult DirectUser()
        {
            User model = new User();
            List<User> lst = new List<User>();
            model.LoginID = Session["LoginId"].ToString();
            DataSet ds = model.GetDirectUser();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.SponsorName = r["FK_SponsorId"].ToString();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.FullName = r["InvestorName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Image = r["Agreement"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.EmailId = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.Address = r["Address"].ToString();
                    lst.Add(obj);
                }
                model.lstDirect = lst;
            }
            return View(model);
        }
        public ActionResult TeamUser()
        {
            User model = new User();
            List<User> lst = new List<User>();
            model.LoginID = Session["LoginId"].ToString();
            DataSet ds = model.GetTeamUser();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.SponsorName = r["FK_SponsorId"].ToString();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.FullName = r["InvestorName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Image = r["Agreement"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.EmailId = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.Address = r["Address"].ToString();
                    lst.Add(obj);
                }
                model.lstTeam = lst;
            }
            return View(model);
        }
        public ActionResult InvestorList()
        {
            User model = new User();
            List<User> lst = new List<User>();
            model.LoginID = Session["LoginId"].ToString();
            DataSet ds = model.GetInvestorDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.SponsorName = r["FK_SponsorId"].ToString();
                    //obj.FK_SponsorId = r["SponsorId"].ToString();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.FullName = r["InvestorName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Image = r["Agreement"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.EmailId = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    //obj.AdharNo = r["AdharNumber"].ToString();
                    //obj.BankAccount = r["MemberAccNo"].ToString();
                    //obj.BankName = r["MemberBankName"].ToString();
                    //obj.BranchName = r["MemberBranch"].ToString();
                    //obj.IFSCCode = r["IFSCCode"].ToString();
                    //obj.Pincode = r["PinCode"].ToString();
                    //obj.City = r["City"].ToString();
                    //obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    lst.Add(obj);
                }
                model.lstInvestor = lst;
            }
            return View(model);
        }
        

    }
}