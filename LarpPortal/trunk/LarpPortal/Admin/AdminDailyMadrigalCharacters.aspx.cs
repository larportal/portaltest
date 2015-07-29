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

namespace LarpPortal.Admin
{
    public partial class AdminDailyMadrigalCharacters : System.Web.UI.Page
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
            BuildCharacterTable();
        }

        private void BuildCharacterTable()
        {
            int CharacterRowCounter = 1;
            int iTemp = 0;
            double dTemp;
            string CharacterAKA = "";
            string TeamName = "";
            string CharacterFirstName = "";
            string CharacterMiddleName = "";
            string CharacterLastName = "";
            int PlotLeadPerson = 0;
            string FirstName = "";
            string LastName = "";
            string DateOfBirth = "";
            string WhereFrom = "";
            string CurrentHome = "";
            double TotalCP = 0;
            string PlayerComments = "";
            int CharacterSkillSetID = 0;
            string dq = "\"";
            string TableCode = "<table id=" + dq + "events-table" + dq + "class=" + dq + "table table-striped table-bordered table-hover table-condensed" + dq;
            TableCode = TableCode + " data-toggle=" + dq + "table" + dq + " data-height=" + dq + "250" + dq + " data-sort-name=" + dq;
            TableCode = TableCode + " date-earned" + dq + " data-sort-order=" + dq + " desc" + dq + " data-click-to-select=" + dq + "true" + dq;
            TableCode = TableCode + " data-select-item-name=" + dq + "total-point" + dq + "><tr><td>";
            Classes.cAdminViews CharsMad = new Classes.cAdminViews();
            DataTable dtCharsMad = new DataTable();
            dtCharsMad = CharsMad.MadrigalCharacterList();
            if (CharsMad.CharacterCount == 0)
            {
                // Build table with no records
                lblCharacters.Text = TableCode + "There are no Madrigal characters</td></tr></table>";
            }
            else
            {
                foreach (DataRow dRow in dtCharsMad.Rows)
                {
                    if (CharacterRowCounter == 1)
                    {
                        // Build the header row
                        TableCode = TableCode + "CharacterAKA</td><td>TeamName</td><td>CharacterFirstName</td><td>CharacterMiddleName</td><td>CharacterLastName</td>";
                        TableCode = TableCode + "<td>PlotLeadPerson</td><td>FirstName</td><td>LastName</td><td>DateOfBirth</td><td>Country</td>";
                        TableCode = TableCode + "<td>CurrentHome</td><td>TotalCP</td><td>PlayerComments</td><td>CharacterSkillSetID</td></tr>";
                    }
                    CharacterAKA = dRow["CharacterAKA"].ToString();
                    TableCode = TableCode + "<td>" + CharacterAKA + "</td>";
                    TeamName = dRow["TeamName"].ToString();
                    TableCode = TableCode + "<td>" + TeamName + "</td>";
                    CharacterFirstName = dRow["CharacterFirstName"].ToString();
                    TableCode = TableCode + "<td>" + CharacterFirstName + "</td>";
                    CharacterMiddleName = dRow["CharacterMiddleName"].ToString();
                    TableCode = TableCode + "<td>" + CharacterMiddleName + "</td>";
                    CharacterLastName = dRow["CharacterLastName"].ToString();
                    TableCode = TableCode + "<td>" + CharacterLastName + "</td>";
                    if (int.TryParse(dRow["PlotLeadPerson"].ToString(), out iTemp))
                        PlotLeadPerson = iTemp;
                    TableCode = TableCode + "<td>" + PlotLeadPerson + "</td>";
                    FirstName = dRow["FirstName"].ToString();
                    TableCode = TableCode + "<td>" + FirstName + "</td>";
                    LastName = dRow["LastName"].ToString();
                    TableCode = TableCode + "<td>" + LastName + "</td>";
                    DateOfBirth = dRow["DateOfBirth"].ToString();
                    TableCode = TableCode + "<td>" + DateOfBirth + "</td>";
                    WhereFrom = dRow["Country"].ToString();
                    TableCode = TableCode + "<td>" + WhereFrom + "</td>";
                    CurrentHome = dRow["CurrentHome"].ToString();
                    TableCode = TableCode + "<td>" + CurrentHome + "</td>";
                    if (double.TryParse(dRow["TotalCP"].ToString(), out dTemp))
                        TotalCP = dTemp;
                    TableCode = TableCode + "<td>" + TotalCP + "</td>";
                    PlayerComments = dRow["PlayerComments"].ToString();
                    TableCode = TableCode + "<td>" + PlayerComments + "</td>";
                    if (int.TryParse(dRow["CharacterSkillSetID"].ToString(), out iTemp))
                        CharacterSkillSetID = iTemp;
                    TableCode = TableCode + "<td>" + CharacterSkillSetID + "</td></tr>";
                    CharacterRowCounter++;
                }
                // Build the table close
                lblCharacters.Text = TableCode + "</table>";
            }
        }
    }
}