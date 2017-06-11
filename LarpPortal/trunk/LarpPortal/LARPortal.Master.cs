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
            HttpContext context = HttpContext.Current;
            if (context.Request.Url.AbsoluteUri.ToUpper().Contains("BETA"))
//                ) || (context.Request.IsLocal))
            {
//                MainBody.Attributes.Add("bgcolor", "LightCyan");
                MainBody.Attributes.Add("style", "background-color: LightCyan");
            }
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
                    int ExclusionCount = 0;
                    if (Session["ExclusionCount"] == null)
                        Session["ExclusionCount"] = 0;
                    if (Session["PageName"] == null)
                    {
                        Session["PageName"] = PageName;
                    }
                    else
                    {
                        if(Session["PageName"] == PageName)
                        {
                            Int32.TryParse(Session["ExclusionCount"].ToString(), out ExclusionCount);
                        }
                        else
                        {
                            string lsRoutineName = "LARPortal.Master.PageNameContainCheck";
                            string stStoredProc = "uspCheckForExclusion";
                            string strUserName = Session["UserName"].ToString();
                            SortedList slParams = new SortedList();
                            slParams.Add("@CompareString", PageName);
                            slParams.Add("@ExclusionType", "LastLoggedInLocation");
                            DataTable dtExclusionCheck = cUtilities.LoadDataTable(stStoredProc, slParams, "LARPortal", strUserName, lsRoutineName);
                            foreach (DataRow dRow in dtExclusionCheck.Rows)
                            {
                                Int32.TryParse(dRow["Exclude"].ToString(), out ExclusionCount);
                            }
                        }
                    }
                    //if (PageName.Contains("Error") || PageName.Contains("WhatsNewDetail") || PageName.Contains("Reports/") || PageName.Contains("EventPayment") || PageName.Contains("PageUnderConstruction"))
                    if (ExclusionCount > 0)
                    {
                        // It met at least one exclusion criteria.  Do nothing.
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
                // Check page security
                // Check request.rawurl against cURLPermission
                // Go get all roles for that campaign and load them into a session variable
                int UserID = Convert.ToInt32(Session["UserID"]);
                int CampaignID = Convert.ToInt32(Session["CampaignID"]);
                Classes.cPlayerRoles Roles = new Classes.cPlayerRoles();
                Roles.Load(UserID, 0, CampaignID, DateTime.Today);
                Session["PlayerRoleString"] = Roles.PlayerRoleString;
                Classes.cURLPermission permissions = new Classes.cURLPermission();
                bool PagePermission = true;
                string DefaultUnauthorizedURL = "";
                permissions.GetURLPermissions(Request.RawUrl, UserID, Roles.PlayerRoleString);
                PagePermission = permissions._PagePermission;
                DefaultUnauthorizedURL = permissions._DefaultUnauthorizedURL;
                if (PagePermission == false)
                    Response.Redirect(DefaultUnauthorizedURL);
                // End permission check
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

        protected void Page_PreRender(object sender, EventArgs e)
        {

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
