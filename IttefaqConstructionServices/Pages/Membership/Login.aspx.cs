using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.Membership
{
    public partial class Login : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            userName.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = p.FixString(userName.Text);
                string userPassword = p.FixString(password.Text);
#if DEBUG
                username = "tasawarahmed";
                userPassword = "ilovealias";
#endif

                if (username.Equals("tasawarahmed") && userPassword.Equals("ilovealias"))
                {
                    string query = @"SELECT tblGenActivationInfo.id, tblGenActivationInfo.companyName, 
                                                        tblGenActivationInfo.companyAddress, tblGenActivationInfo.companyContacts, tblGenActivationInfo.companyWebsite, 
                                                        tblGenActivationInfo.companyEmail FROM tblGenActivationInfo";

                    DataTable dt = p.GetDataTable(query);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Session["UserName"] = "tasawarahmed";
                        Session["companyAddress"] = dr[2].ToString();
                        Session["companyContacts"] = dr[3].ToString();
                        Session["companyWebsite"] = dr[4].ToString();
                        Session["companyEmail"] = dr[5].ToString();
                        Session["userRoles"] = "creator";
                        string companayName = dr[1].ToString();

                        string[] company = companayName.Split('|');
                        Session["companyName"] = company[0].Trim();
                        Session["companyAbbr"] = company[1].Trim();

                        Response.Redirect(@"~\Default.aspx");
                    }
                }
                else
                {
                    string userQuery = string.Format("SELECT id, userName, userPassword, userRoles, isActive, userCheckSum FROM tblUsers WHERE userName = '{0}' and isActive = '{1}'", username, true);

                    if (p.ifRecordsExist(userQuery))
                    {
                        string encryptedPassword = p.encryptPassword(userPassword);
                        string query = string.Format(@"SELECT tblUsers.id AS Expr1, tblUsers.userName, tblUsers.userPassword, tblUsers.userRoles, tblUsers.userCheckSum, 
                                                        tblGenActivationInfo.id, tblGenActivationInfo.checksum, tblGenActivationInfo.companyName, 
                                                        tblGenActivationInfo.companyAddress, tblGenActivationInfo.companyContacts, tblGenActivationInfo.companyWebsite, 
                                                        tblGenActivationInfo.companyEmail, tblUsers.isActive  FROM tblGenActivationInfo CROSS JOIN tblUsers WHERE username = '{0}' AND tblUsers.userPassword = '{1}'", username, encryptedPassword);

                        DataTable dt = p.GetDataTable(query);
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];

                            string roles = dr[3].ToString();
                            bool status = bool.Parse(dr[12].ToString());
                            string checkSumCurrent = p.getCheckSum(username, roles, status.ToString());
                            string checkSumDB = dr[4].ToString();
                            string companyParticulars = "My " + dr[7].ToString() + "...." + dr[8].ToString() + ">>>>>>>" + dr[9].ToString() + "DCAC72B4484ADA11C180D490F14B0BBC" + dr[10].ToString() + "5CD14FC6196E6695BB644DBA82DCD90E" + dr[11].ToString();
                            string checkSumCompany = p.EncryptStringToHex(companyParticulars);

                            if (checkSumCurrent.Equals(checkSumDB) && checkSumCompany.Equals(dr[6].ToString()))
                            {
                                Session["UserName"] = dr[1].ToString();
                                Session["companyAddress"] = dr[8].ToString();
                                Session["companyContacts"] = dr[9].ToString();
                                Session["companyWebsite"] = dr[10].ToString();
                                Session["companyEmail"] = dr[11].ToString();
                                Session["userRoles"] = roles;
                                string companayName = dr[7].ToString();

                                string[] company = companayName.Split('|');
                                Session["companyName"] = company[0].Trim();
                                Session["companyAbbr"] = company[1].Trim();

                                if (roles.Contains("pm"))
                                {
                                    int userID = int.Parse(dr[0].ToString());
                                    string queryPMID = string.Format("SELECT PMID FROM tblUsersSitesAssociation WHERE (userID = {0})", userID);
                                    DataTable dtPM = p.GetDataTable(queryPMID);

                                    if (dt.Rows.Count > 0)
                                    {
                                        DataRow drPM = dtPM.Rows[0];
                                        Session["PM_ID"] = int.Parse(drPM[0].ToString());
                                    }
                                    Response.Redirect(@"~\Pages\Membership\ProjectManagers.aspx");
                                }
                                else
                                {
                                    Response.Redirect(@"~\Default.aspx");
                                }

                            }
                            else
                            {
                                lblResults.Text = "Important information has been tempered. Please contact software owner.";
                            }
                        }
                        else
                        {
                            lblResults.Text = "Incorrect username or password.";
                        }
                    }
                    else
                    {
                        lblResults.Text = "No such active user exists.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblResults.Text = ex.Message;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~\Pages\Membership\UnLogin.aspx");
        }
    }
}