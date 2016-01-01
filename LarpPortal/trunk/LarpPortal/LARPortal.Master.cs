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
using System.IO;

namespace LarpPortal
{
    public partial class LARPortal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["CurrentPagePermission"].ToString() == "False")
                {
                    if (Session["AlternatePageURL"] != null)
                    {
                        string AltURL = Session["AlternatePageURL"].ToString();
                        Session["CurrentPagePermission"] = "True";
                        Session["LastLoggedInLocation"] = AltURL;
                        //Response.Redirect(AltURL);    Rick - 10/11/2015 - Remove comment when security works
                    } 
                }
                string PageName = Request.Url.AbsolutePath + ".aspx";
                Session["LastLoggedInLocation"] = PageName;
                if (Session["LastWrittenLoggedInLocation"] != null && Session["LastWrittenLoggedInLocation"] == Session["LastLoggedInLocation"])
                {
                    // Do nothing
                }
                else
                {
                    if(PageName.Contains("Error") || PageName.Contains("WhatsNewDetail") || PageName.Contains("Reports/") )
                    {
                        // Do nothing
                    }
                    else
                    {
                        // Set them equal and write to MDBUser LastLoggedInLocation
                        Session["LastWrittenLoggedInLocation"] = Session["LastLoggedInLocation"];
                        Classes.cLogin LastLoggedIn = new Classes.cLogin();
                        if(Session["UserID"] != null)
                        {
                            int intUserID = Session["UserID"].ToString().ToInt32();
                            LastLoggedIn.LogLastPage(intUserID, PageName);
                        }
                    }  
                }
            }
            // Uncomment this if trying to run the page without going through the index.aspx page
            //Session["SecurityRole"] = 0;
            int i;
            int intSecurityRole;
            if (Session["CampaignName"] == null)
            {
                Session["CampaignName"] = "";
            }
            //lblCampaignName.Text =  Session["CampaignName"].ToString();
            if (Session["LoginName"] == null)
            {
                Session["LoginName"] = "Guest";
            }
            //lblLoginName.Text = "Welcome " + Session["LoginName"].ToString();
            if (int.TryParse(Session["SecurityRole"].ToString(), out i))
                intSecurityRole = i;
            else
            {
                intSecurityRole = 0;
            }
            LoadMainLinks();
            LoadTopTab(intSecurityRole, 0); //TODO-Rick-2 Change second variable to a session variable as defined by Jeff's communications section
            LoadPageFooter();
            if (Session["PageFooter"] == null)
            {
                Session["PageFooter"] = " ";
            }
            lblPageFooter.Text = Session["PageFooter"].ToString();
        }

        public void LoadMainLinks()
        {
            // Load the main links at the top right of the master page.  These are the same for everyone, except for home which will be member only
            int intTabsNeeded;
            int SecurityRole = 1;
            string hrefline;
            string TabName;
            string PageName;
            string TabClass;
            string TabIcon;
            int UserID = 0;
            //UserID = int.TryParse(Session["UserID"].ToString(), out iTemp);
            UserID = ((int?)Session["UserID"] ?? 0);
            Classes.cLogin RoleTabs = new Classes.cLogin();
            RoleTabs.LoadTabsBySecurityRole(SecurityRole);
            intTabsNeeded = RoleTabs.TabCount;
            DataTable TopTabTable = new DataTable();
            TopTabTable.Columns.Add("href_main");
            for (int i = 0; i <= intTabsNeeded; i++)
            {
                if (i < intTabsNeeded)
                {
                    PageName = RoleTabs.lsPageTabs[i].CallsPageName.ToString();
                    TabClass = RoleTabs.lsPageTabs[i].TabClass.ToString();
                    TabIcon = RoleTabs.lsPageTabs[i].TabIcon.ToString();
                    TabName = RoleTabs.lsPageTabs[i].TabName.ToString();
                    switch (TabName)
                    {
                        case "Home":
                            if(UserID == 0)
                            {
                                hrefline = "skip";
                            }
                            else
                            {
                                hrefline = "<li><a href=" + "\"" + PageName + "\"" + ">" + TabName + "</a></li>";
                            }
                            break;
                        default:
                            hrefline = "<li><a href=" + "\"" + PageName + "\"" + ">" + TabName + "</a></li>";
                            break;
                    }
                }
                else
                {
                    hrefline = "<li><b>Welcome " + Session["LoginName"].ToString() + "</b></li>";
                }  
                DataRow TopTabRow = TopTabTable.NewRow();
                TopTabRow["href_main"] = hrefline;
                if(hrefline != "skip")
                {
                    TopTabTable.Rows.Add(TopTabRow);
                }
                    
            }
            menu_ul_main.DataSource = TopTabTable;
            menu_ul_main.DataBind();
        }

        public void LoadTopTab(int SecurityRole, int UnreadCount)
        {
            // Load the user based security tabs on the master page.  These change based on user security levels.
            int intTabsNeeded;
            string hrefline;
            string TabName;
            string ActiveState;
            string PageName;
            string TabClass;
            string TabIcon;
            int UseTabIcons = 0;   // 0 = Leave tab icons off ; 1 = Include tab icons
            Classes.cLogin RoleTabs = new Classes.cLogin();
            RoleTabs.LoadTabsBySecurityRole(SecurityRole);
            intTabsNeeded = RoleTabs.TabCount - 1;
            DataTable TopTabTable = new DataTable();
            TopTabTable.Columns.Add("href_li");
            TopTabTable.Columns.Add("DisplayText");
            for (int i = 0; i <= intTabsNeeded; i++)
            {
                PageName = RoleTabs.lsPageTabs[i].CallsPageName.ToString();
                TabClass = RoleTabs.lsPageTabs[i].TabClass.ToString();
                TabIcon = RoleTabs.lsPageTabs[i].TabIcon.ToString();
                TabName = RoleTabs.lsPageTabs[i].TabName.ToString();
                if (Session["ActiveTopNav"] == null)
                    Session["ActiveTopNav"] = "Campaigns";
                if (Session["ActiveTopNav"].ToString() == TabName)
                    ActiveState = " class=" + "\"" + "active" + "\"" + ">";
                else
                    ActiveState = ">";
                if(RoleTabs.lsPageTabs[i].TabAlert.ToString() == " ")
                {
                    TabName = " " + TabName;
                }
                else
                {
                    TabName = " " + TabName + RoleTabs.lsPageTabs[i].TabAlert.ToString() + UnreadCount;
                }
                if(UseTabIcons == 0)
                {
                    //Without icons
                    hrefline = "<li" + ActiveState + "<a href=" + "\"" + PageName + "\"" + "\"><span class=\"" + "\"" +
                        TabClass + " " + TabIcon + "\"> </span>" + TabName + "</a></li>";
                }
                else
                {
                    //With icons
                    hrefline = "<li" + ActiveState + "<a href=" + "\"" + PageName + "\"" + "\"><span class=\"" +
                        TabClass + " " + TabIcon + "\"> </span>" + TabName + "</a></li>";
                }

                DataRow TopTabRow = TopTabTable.NewRow();
                TopTabRow["href_li"] = hrefline;
                TopTabRow["DisplayText"] = TabName;
                TopTabTable.Rows.Add(TopTabRow);
            }
            menu_ul_1.DataSource = TopTabTable;
            menu_ul_1.DataBind();
        }

        public void LoadPageFooter()
        {
            string SiteFoot = " ";
            Classes.cLogin SiteFooter = new Classes.cLogin();
            SiteFooter.SetPageFooter();
            SiteFoot = SiteFooter.SiteFooter;
            Session["PageFooter"] = SiteFoot;
        }

    }
}
