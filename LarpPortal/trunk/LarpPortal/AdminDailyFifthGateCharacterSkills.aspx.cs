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
    public partial class AdminDailyFifthGateCharacterSkills : System.Web.UI.Page
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
            int SkillTypeID2 = 0;
            string dq = "\"";
            string TableCode = "<table id=" + dq + "events-table" + dq + "class=" + dq + "table table-striped table-bordered table-hover table-condensed" + dq;
            TableCode = TableCode + " data-toggle=" + dq + "table" + dq + " data-height=" + dq + "250" + dq + " data-sort-name=" + dq;
            TableCode = TableCode + " date-earned" + dq + " data-sort-order=" + dq + " desc" + dq + " data-click-to-select=" + dq + "true" + dq;
            TableCode = TableCode + " data-select-item-name=" + dq + "total-point" + dq + "><tr><td>";
            Classes.cAdminViews Chars5G = new Classes.cAdminViews();
            DataTable dtChars5G = new DataTable();
            dtChars5G = Chars5G.FifthGateCharacterSkillList();
            if (Chars5G.CharacterSkillCount == 0)
            {
                // Build table with no records
                lblCharacterSkils.Text = TableCode + "There are no Fifth Gate character skills</td></tr></table>";
            }
            else
            {
                foreach (DataRow dRow in dtChars5G.Rows)
                {
                    if (CharacterSkillRowCounter == 1)
                    {
                        // Build the header row
                        TableCode = TableCode + "CharacterAKA</td><td>FirstName</td><td>LastName</td><td>World</td><td>DescriptorValue</td>";
                        TableCode = TableCode + "<td>SkillName</td><td>SkillTypeID</td><td>SkillTypeID</td></tr>";
                    }
                    CharacterAKA = dRow["CharacterAKA"].ToString();
                    TableCode = TableCode + "<td>" + CharacterAKA + "</td>";
                    FirstName = dRow["FirstName"].ToString();
                    TableCode = TableCode + "<td>" + FirstName + "</td>";
                    LastName = dRow["LastName"].ToString();
                    TableCode = TableCode + "<td>" + LastName + "</td>";
                    World = dRow["World"].ToString();
                    TableCode = TableCode + "<td>" + World + "</td>";
                    DescriptorValue = dRow["DescriptorValue"].ToString();
                    TableCode = TableCode + "<td>" + DescriptorValue + "</td>";
                    SkillName = dRow["SkillName"].ToString();
                    TableCode = TableCode + "<td>" + SkillName + "</td>";
                    if (int.TryParse(dRow["SkillTypeID"].ToString(), out iTemp))
                        SkillTypeID = iTemp;
                    TableCode = TableCode + "<td>" + SkillTypeID + "</td>";
                    if (int.TryParse(dRow["SkillTypeID2"].ToString(), out iTemp))
                        SkillTypeID2 = iTemp;
                    TableCode = TableCode + "<td>" + SkillTypeID2 + "</td></tr>";
                    CharacterSkillRowCounter++;
                }
                // Build the table close
                lblCharacterSkils.Text = TableCode + "</table>";
            }
        }
    }
}