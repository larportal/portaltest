using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace LarpPortal
{
    public partial class AdminViewLogins : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SecurityRole"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            Session["ActiveLeftNav"] = "N/A";
            string uName = "";
            int uID = 0;
            if (Session["Username"] != null)
                uName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                uID = (Session["UserID"].ToString().ToInt32());
            BuildLoginTable();
        }

        private void BuildLoginTable()
        {
            int LoginAuditRowCounter = 1;
            int iTemp = 0;
            DateTime dtTemp;
            int UserID = 0;
            string LoginEST;
            string Player = "";
            string UsernameUsed = "";
            string PasswordUsed = "";
            string Browser = "";
            string BrowserVersion = "";
            string OS = "";
            string OSVersion = "";
            string IPAddress = "";
            string dq = "\"";
            string TableCode = "<table id=" + dq + "events-table" + dq + "class=" + dq + "table table-striped table-bordered table-hover table-condensed" + dq;
            TableCode = TableCode + " data-toggle=" + dq + "table" + dq + " data-height=" + dq + "250" + dq + " data-sort-name=" + dq;
            TableCode = TableCode + " date-earned" + dq + " data-sort-order=" + dq + " desc" + dq + " data-click-to-select=" + dq + "true" + dq;
            TableCode = TableCode + " data-select-item-name=" + dq + "total-point" + dq + "><tr><td>";
            Classes.cAdminViews AViews = new Classes.cAdminViews();
            DataTable dtLoginAudit = new DataTable();
            dtLoginAudit = AViews.GetLoginAudit();
            if (AViews.LoginAuditCount == 0)
            {
                // Build table with no records
                lblViewLogins.Text = TableCode + "There are no user login records within the past 30 days</td></tr></table>";
            }
            else
            {
                foreach (DataRow dRow in dtLoginAudit.Rows)
                {
                    if (LoginAuditRowCounter == 1)
                    {
                        // Build the header row
                        TableCode = TableCode + "UserID</td><td>Login (EST)</td><td>Player</td><td>Username</td><td>Password</td><td>Browser</td>";
                        TableCode = TableCode + "<td>Version</td><td>OS</td><td>Version</td><td>IP Address</td></tr>";
                    }
                    // Build the detail row if the 'To' or 'From' campaign is the drop down list campaign
                    //RecvFromCampaign = dRow["RecvFromCampaign"].ToString();
                    //ReceivingCampaign = dRow["ReceivingCampaign"].ToString();
                    if (int.TryParse(dRow["UserID"].ToString(), out iTemp))
                        UserID = iTemp;
                    TableCode = TableCode + "<td>" + UserID + "</td>";
                    if (DateTime.TryParse(dRow["LoginEST"].ToString(), out dtTemp))
                        LoginEST = string.Format("{0:MM/d/yyyy hh:mm:ss}", dtTemp);
                    else
                        LoginEST = "";
                    TableCode = TableCode + "<td>" + LoginEST + "</td>";
                    Player = dRow["Player"].ToString();
                    TableCode = TableCode + "<td>" + Player + "</td>";
                    UsernameUsed = dRow["UsernameUsed"].ToString();
                    TableCode = TableCode + "<td>" + UsernameUsed + "</td>";
                    PasswordUsed = dRow["PasswordUsed"].ToString();
                    TableCode = TableCode + "<td>" + PasswordUsed + "</td>";
                    Browser = dRow["Browser"].ToString();
                    TableCode = TableCode + "<td>" + Browser + "</td>";
                    BrowserVersion = dRow["BrowserVersion"].ToString();
                    TableCode = TableCode + "<td>" + BrowserVersion + "</td>";
                    OS = dRow["OS"].ToString();
                    TableCode = TableCode + "<td>" + OS + "</td>";
                    OSVersion = dRow["OSVersion"].ToString();
                    TableCode = TableCode + "<td>" + OSVersion + "</td>";
                    IPAddress = dRow["IPAddress"].ToString();
                    TableCode = TableCode + "<td>" + IPAddress + "</td></tr>";
                    LoginAuditRowCounter++;
                }
                // Build the table close
                lblViewLogins.Text = TableCode + "</table>";
            }
        }
    }
}