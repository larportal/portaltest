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
    public partial class AdminDailyMadrigalCharacterSkills : System.Web.UI.Page
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
            BuildCharacterSkillTable();
        }

        private void BuildCharacterSkillTable()
        {
            int CharacterSkillRowCounter = 1;
            int iTemp = 0;
            string CharacterAKA = "";
            string FirstName = "";
            string LastName = "";
            string World = "";
            string DescriptorValue = "";
            string SkillName = "";
            int SkillTypeID = 0;
            int SkillID = 0;
            string OrderOrigin = "";
            string AttributeDesc = "";
            string TeamName = "";
            string dq = "\"";
            string TableCode = "<table id=" + dq + "events-table" + dq + "class=" + dq + "table table-striped table-bordered table-hover table-condensed" + dq;
            TableCode = TableCode + " data-toggle=" + dq + "table" + dq + " data-height=" + dq + "250" + dq + " data-sort-name=" + dq;
            TableCode = TableCode + " date-earned" + dq + " data-sort-order=" + dq + " desc" + dq + " data-click-to-select=" + dq + "true" + dq;
            TableCode = TableCode + " data-select-item-name=" + dq + "total-point" + dq + "><tr><td>";
            Classes.cAdminViews CharsMad = new Classes.cAdminViews();
            DataTable dtCharsMad = new DataTable();
            dtCharsMad = CharsMad.MadrigalCharacterSkillList();
            if (CharsMad.CharacterSkillCount == 0)
            {
                // Build table with no records
                lblCharacterSkils.Text = TableCode + "There are no Madrigal character skills</td></tr></table>";
            }
            else
            {
                foreach (DataRow dRow in dtCharsMad.Rows)
                {
                    if (CharacterSkillRowCounter == 1)
                    {
                        // Build the header row
                        TableCode = TableCode + "CharacterAKA</td><td>FirstName</td><td>LastName</td><td>World</td><td>Team</td><td>SkillTypeID</td>";
                        TableCode = TableCode + "<td>Order Origin</td><td>Skill</td><td>Attribute</td><td>DescriptorValue</td><td>SkillID</td></tr>";
                    }
                    CharacterAKA = dRow["CharacterAKA"].ToString();
                    TableCode = TableCode + "<td>" + CharacterAKA + "</td>";
                    FirstName = dRow["FirstName"].ToString();
                    TableCode = TableCode + "<td>" + FirstName + "</td>";
                    LastName = dRow["LastName"].ToString();
                    TableCode = TableCode + "<td>" + LastName + "</td>";
                    World = dRow["World"].ToString();
                    TableCode = TableCode + "<td>" + World + "</td>";
                    TeamName = dRow["Team"].ToString();
                    TableCode = TableCode + "<td>" + TeamName + "</td>";
                    if (int.TryParse(dRow["SkillTypeID"].ToString(), out iTemp))
                        SkillTypeID = iTemp;
                    TableCode = TableCode + "<td>" + SkillTypeID + "</td>";
                    OrderOrigin = dRow["OrderOrigin"].ToString();
                    TableCode = TableCode + "<td>" + OrderOrigin + "</td>";
                    SkillName = dRow["SkillName"].ToString();
                    TableCode = TableCode + "<td>" + SkillName + "</td>";
                    AttributeDesc = dRow["AttributeDesc"].ToString();
                    TableCode = TableCode + "<td>" + AttributeDesc + "</td>";
                    DescriptorValue = dRow["DescriptorValue"].ToString();
                    TableCode = TableCode + "<td>" + DescriptorValue + "</td>";
                    if (int.TryParse(dRow["SkillID"].ToString(), out iTemp))
                        SkillID = iTemp;
                    TableCode = TableCode + "<td>" + SkillID + "</td></tr>";
                    CharacterSkillRowCounter++;
                }
                // Build the table close
                lblCharacterSkils.Text = TableCode + "</table>";
            }
        }
    }
}