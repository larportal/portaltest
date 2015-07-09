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
    public partial class LARPing : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Define lblLeftNav to be the code for the menu depending on the page calling it
            string MenuChoice;
            if (Session["ActiveLeftNav"] != null)
            {
                MenuChoice = Session["ActiveLeftNav"].ToString();
                switch (MenuChoice)
                {
                    case "WhatIsLARPing":
                        break;
                    case "FAQ":
                        BuildFAQLeftNav();
                        break;
                    case "TBD1":
                        break;
                    case "TBD2":
                        break;
                    case "TBD3":
                        break;

                }
            }
            else
            {
                MenuChoice = Session["ActiveLeftNav"].ToString();
            }
        }

        private void BuildFAQLeftNav()
        {
            string dq = "\"";
            lblLeftNav.Text = "&nbsp;FAQs:<br /><br /><ul class = " + dq + "nav nav-pills" + dq + " " + dq + "panel-wrapper list-unstyled scroll-500" + dq + "><li>1</li><li>2</li></ul>";
            try
            {
                string LeftNavCode = "";

                string stUsername = "Public";
                if (Session["UserName"] != null)
                    stUsername = Session["UserName"].ToString();
                LeftNavCode = "&nbsp;FAQs:<br /><br /><ul class = " + dq + "nav nav-pills" + dq + " " + dq + "panel-wrapper list-unstyled scroll-500" + dq + ">";
                string stStoredProc = "uspGetFAQCategories";
                string stCallingMethod = "LARPing.master.cs.BuildFAQLeftNav";
                DataTable dtFAQCategories = new DataTable();
                SortedList sParams = new SortedList();
                dtFAQCategories = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", stUsername, stCallingMethod);
                if (dtFAQCategories.Rows.Count == 0)
                {
                    LeftNavCode = "<li></li>";
                }
                else
                {
                    foreach (DataRow dRow in dtFAQCategories.Rows)
                    {
                        LeftNavCode = LeftNavCode + "<li runat=" + dq + "server" + dq + " id=" + dq + "liCategory" + dRow["FAQCategoryID"].ToString() + dq + "><a href=" + dq +
                            "FAQ.aspx?CategoryID=" + dRow["FAQCategoryID"].ToString() + "&CategoryName=" + dRow["CategoryName"] + dq + ">" + dRow["CategoryName"].ToString() + "</a></li>";
                    }
                }
                LeftNavCode = LeftNavCode + "<ul>";
                if (LeftNavCode != null)
                    lblLeftNav.Text = LeftNavCode;
                else
                    lblLeftNav.Text = "";
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}