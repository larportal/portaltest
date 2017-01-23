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
    public partial class MemberProfile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Profile";
            string hrefline;
            string ActiveState;
            string PageName;
            string LineText;
            int liLinesNeeded = 7;
            liLinesNeeded = 2;
            DataTable LeftNavTable = new DataTable();
            LeftNavTable.Columns.Add("href_li");
            ActiveState = ">";
            PageName = "~/DESTINATIONPAGE.aspx";
            LineText = "";
            for (int i = 0; i <= liLinesNeeded; i++)
            {
                //build on case of i
                switch (i)
                {
                    case 0:
                        if (Session["ActiveLeftNav"].ToString() == "Demographics")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "MemberDemographics.aspx";
                        LineText = "Demographics";
                        break;

                    case 1:
                        if (Session["ActiveLeftNav"].ToString() == "Security")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "MemberSecurity.aspx";
                        LineText = "Security";
                        break;

                    //case 2:
                    //    if (Session["ActiveLeftNav"].ToString() == "PlayerResume")
                    //    {
                    //        ActiveState = " class=\"active\">";
                    //    }
                    //    else
                    //    {
                    //        ActiveState = ">";
                    //    }
                    //    //PageName = "PlayerResume.aspx";
                    //    PageName = "PageUnderConstruction.aspx";
                    //    LineText = "Player Resume";
                    //    break;

                    //case 3:
                    //    if (Session["ActiveLeftNav"].ToString() == "LARPResume")
                    //    {
                    //        ActiveState = " class=\"active\">";
                    //    }
                    //    else
                    //    {
                    //        ActiveState = ">";
                    //    }
                    //    //PageName = "LARPResume.aspx";
                    //    PageName = "PageUnderConstruction.aspx";
                    //    LineText = "LARP Resume";
                    //    break;

                    //case 4:
                    //    if (Session["ActiveLeftNav"].ToString() == "Medical")
                    //    {
                    //        ActiveState = " class=\"active\">";
                    //    }
                    //    else
                    //    {
                    //        ActiveState = ">";
                    //    }
                    //    //PageName = "Medical.aspx";
                    //    PageName = "PageUnderConstruction.aspx";
                    //    LineText = "Medical";
                    //    break;

                    //case 5:
                    //    if (Session["ActiveLeftNav"].ToString() == "Waivers")
                    //    {    
                    //        ActiveState = " class=\"active\">";
                    //    }
                    //    else
                    //    {
                    //        ActiveState = ">";
                    //    }
                    //    //PageName = "Waivers.aspx";
                    //    PageName = "PageUnderConstruction.aspx";
                    //    LineText = "Waivers & Consent";
                    //    break;

                    //case 6:
                    //    if (Session["ActiveLeftNav"].ToString() == "Preferences")
                    //    {
                    //        ActiveState = " class=\"active\">";
                    //    }
                    //    else
                    //    {
                    //        ActiveState = ">";
                    //    }
                    //    //PageName = "Preferences.aspx";
                    //    PageName = "PageUnderConstruction.aspx";
                    //    LineText = "Player Preferences";
                    //    break;

                    //case 7:
                    //    if (Session["ActiveLeftNav"].ToString() == "Inventory")
                    //    {
                    //        ActiveState = " class=\"active\">";
                    //    }
                    //    else
                    //    {
                    //        ActiveState = ">";
                    //    }
                    //    //PageName = "Inventory.aspx";
                    //    PageName = "PageUnderConstruction.aspx";
                    //    LineText = "Player Inventory";
                    //    break;

                }
                hrefline = "<li" + ActiveState + "<a href=" + "\"" + PageName + "\"" + " data-toggle=" + "\"" + "pill" + "\"" + ">" + LineText + "</a></li>";
                DataRow LeftNavRow = LeftNavTable.NewRow();
                LeftNavRow["href_li"] = hrefline;
                LeftNavTable.Rows.Add(LeftNavRow);
                menu_ul_memberprofile.DataSource = LeftNavTable;
                menu_ul_memberprofile.DataBind();
            }
        }
    }
}