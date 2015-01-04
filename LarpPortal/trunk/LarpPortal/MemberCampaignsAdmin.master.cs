﻿using System;
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
    public partial class MemberCampaignsAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Campaigns";
            // Load My Campaigns selection

            string hrefline;
            string ActiveState;
            string PageName;
            string LineText;
            int liLinesNeeded = 21; //REPLACE WITH NUMBER OF MENU ITEMS NEEDED 21 when done (22 but 0 base and all that)
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
                        if (Session["ActiveLeftNav"].ToString() == "CampaignInfoSetup")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "CampaignInfoSetup.aspx";
                        LineText = "Campaign Info";
                        break;
                    case 1:
                        if (Session["ActiveLeftNav"].ToString() == "SetupInfo")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "CampaignSetupInfo.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign Info";
                        break;
                    case 2:
                        if (Session["ActiveLeftNav"].ToString() == "SetupPeople")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "CampaignSetupPeople.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign People";
                        break;

                    case 3:
                        if (Session["ActiveLeftNav"].ToString() == "SetupPeople")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "CampaignSetupPlaces.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign Places";
                        break;
                    case 4:
                        if (Session["ActiveLeftNav"].ToString() == "SetupRules")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "CampaignSetupRules.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign Rules";
                        break;
                    case 5:
                        if (Session["ActiveLeftNav"].ToString() == "SetupRulesIndex")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "CampaignSetupRulesIndex.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign Rules Index";
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
                        //PageName = "WhatIsLARPing7.aspx";
                        PageName = "PageUnderConstruction.aspx";
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
                        //PageName = "WhatIsLARPing8.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 8";
                        break;
                    case 8:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing8.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 9";
                        break;
                    case 9:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing8.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 10";
                        break;
                    case 10:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing8.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 11";
                        break;
                    case 11:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing8.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 12";
                        break;
                    case 12:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing2")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing2.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 13";
                        break;
                    case 13:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing3")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing3.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 14";
                        break;

                    case 14:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing4")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing4.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 15";
                        break;
                    case 15:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing5")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing5.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 16";
                        break;
                    case 16:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing6")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing6.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 17";
                        break;
                    case 17:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing7")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing7.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 18";
                        break;
                    case 18:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing8.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 19";
                        break;
                    case 19:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing8.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 20";
                        break;
                    case 20:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing8.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 21";
                        break;
                    case 21:
                        if (Session["ActiveLeftNav"].ToString() == "WhatIsLARPing8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //PageName = "WhatIsLARPing8.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Menu Item TBD 22";
                        break;
                }
                hrefline = "<li" + ActiveState + "<a href=" + "\"" + PageName + "\"" + " data-toggle=" + "\"" + "pill" + "\"" + ">" + LineText + "</a></li>";
                DataRow LeftNavRow = LeftNavTable.NewRow();
                LeftNavRow["href_li"] = hrefline;
                LeftNavTable.Rows.Add(LeftNavRow);
                menu_ul_membercampaignsadmin.DataSource = LeftNavTable;
                menu_ul_membercampaignsadmin.DataBind();
            }
        }
    }
}