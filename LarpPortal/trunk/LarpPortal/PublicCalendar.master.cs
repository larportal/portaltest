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
    public partial class PublicCalendar : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Calendar";
            string hrefline;
            string ActiveState;
            string PageName;
            string LineText;
            int liLinesNeeded = 0; //REPLACE WITH NUMBER OF MENU ITEMS NEEDED
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
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "WhatIsLARPing.aspx";
                        LineText = "What is LARPing?";
                        break;
                    case 1:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing2")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "WhatIsLARPing2.aspx";
                        LineText = "Menu Item TBD 2";
                        break;
                    case 2:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing3")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "WhatIsLARPing3.aspx";
                        LineText = "Menu Item TBD 3";
                        break;

                    case 3:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing4")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "WhatIsLARPing4.aspx";
                        LineText = "Menu Item TBD 4";
                        break;
                    case 4:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing5")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "WhatIsLARPing5.aspx";
                        LineText = "Menu Item TBD 5";
                        break;
                    case 5:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing6")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "WhatIsLARPing6.aspx";
                        LineText = "Menu Item TBD 6";
                        break;
                    case 6:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing7")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "WhatIsLARPing7.aspx";
                        LineText = "Menu Item TBD 7";
                        break;
                    case 7:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "WhatIsLARPing8.aspx";
                        LineText = "Menu Item TBD 8";
                        break;
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