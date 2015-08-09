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

namespace LarpPortal.Reports.FifthGate
{
    public partial class SF2Registrations : System.Web.UI.Page
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
            BuildRegistrationTable();
        }

        private void BuildRegistrationTable()
        {
            int RegistrationRowCounter = 1;
            int iTemp = 0;
            int RegistrationID = 0;
            string EventName = "";
            string DateRegistered = "";
            string StatusName = "";
            string CharacterAKA = "";
            string OrderName = "";
            string TeamName = "";
            string FirstName = "";
            string LastName = "";
            string Description = "";
            string EmailAddress = "";
            string PlayerCommentsToStaff = "";
            string dq = "\"";
            string TableCode = "<table id=" + dq + "events-table" + dq + "class=" + dq + "table table-striped table-bordered table-hover table-condensed" + dq;
            TableCode = TableCode + " data-toggle=" + dq + "table" + dq + " data-height=" + dq + "250" + dq + " data-sort-name=" + dq;
            TableCode = TableCode + " date-earned" + dq + " data-sort-order=" + dq + " desc" + dq + " data-click-to-select=" + dq + "true" + dq;
            TableCode = TableCode + " data-select-item-name=" + dq + "total-point" + dq + "><tr><td>";
            Classes.cAdminViews Reg5G = new Classes.cAdminViews();
            DataTable dtReg5G = new DataTable();
            dtReg5G = Reg5G.FifthGateRegistrations();
            if (Reg5G.RegCount == 0)
            {
                // Build table with no records
                lblCharacters.Text = TableCode + "There are no Fifth Gate registrations</td></tr></table>";
            }
            else
            {
                foreach (DataRow dRow in dtReg5G.Rows)
                {
                    if (RegistrationRowCounter == 1)
                    {
                        // Build the header row
                        TableCode = TableCode + "Registration ID</td><td>Event Name</td><td>Date Registered</td><td>Status</td><td>Character AKA</td><td>Order</td>";
                        TableCode = TableCode + "<td>Team Name</td><td>First Name</td><td>Last Name</td><td>Payment</td><td>Email Address</td>";
                        TableCode = TableCode + "<td>Player Comments To Staff</td></tr>";
                    }
                    if (int.TryParse(dRow["RegistrationID"].ToString(), out iTemp))
                        RegistrationID = iTemp;
                    TableCode = TableCode + "<td>" + RegistrationID + "</td>";
                    EventName = dRow["EventName"].ToString();
                    TableCode = TableCode + "<td>" + EventName + "</td>";
                    DateRegistered = dRow["DateRegistered"].ToString();
                    TableCode = TableCode + "<td>" + DateRegistered + "</td>";
                    StatusName = dRow["StatusName"].ToString();
                    TableCode = TableCode + "<td>" + StatusName + "</td>";
                    CharacterAKA = dRow["CharacterAKA"].ToString();
                    TableCode = TableCode + "<td>" + CharacterAKA + "</td>";
                    OrderName = dRow["OrderName"].ToString();
                    TableCode = TableCode + "<td>" + OrderName + "</td>";
                    TeamName = dRow["TeamName"].ToString();
                    TableCode = TableCode + "<td>" + TeamName + "</td>";
                    FirstName = dRow["FirstName"].ToString();
                    TableCode = TableCode + "<td>" + FirstName + "</td>";
                    LastName = dRow["LastName"].ToString();
                    TableCode = TableCode + "<td>" + LastName + "</td>";
                    Description = dRow["Description"].ToString();
                    TableCode = TableCode + "<td>" + Description + "</td>";
                    EmailAddress = dRow["EmailAddress"].ToString();
                    TableCode = TableCode + "<td>" + EmailAddress + "</td>";
                    PlayerCommentsToStaff = dRow["PlayerCommentsToStaff"].ToString();
                    TableCode = TableCode + "<td>" + PlayerCommentsToStaff + "</td></tr>";
                    RegistrationRowCounter++;
                }
                // Build the table close
                lblCharacters.Text = TableCode + "</table>";
            }
        }
    }
}