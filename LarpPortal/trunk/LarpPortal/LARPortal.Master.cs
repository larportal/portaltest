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
    public partial class LARPortal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Uncomment this if trying to run the page without going through the index.aspx page
            //Session["SecurityRole"] = 0;
            int i;
            int intSecurityRole;
            if (Session["LoginName"] == null)
            {
                Session["LoginName"] = "Guest";
            }

            lblLoginName.Text = "Welcome " + Session["LoginName"].ToString();
            if (int.TryParse(Session["SecurityRole"].ToString(), out i))
                intSecurityRole = i;
            else
            {
                intSecurityRole = 0;
            }
            LoadTopTab(intSecurityRole, 0); // Talk to Jeff, 2nd Parameter is unread message count - We have agreed on a session variable
            LoadPageFooter();
            if (Session["PageFooter"] == null)
            {
                Session["PageFooter"] = " ";
            }
            lblPageFooter.Text = Session["PageFooter"].ToString();
        }

        public void LoadTopTab(int SecurityRole, int UnreadCount)
        {
            int intTabsNeeded;
            string hrefline;
            string TabName;
            string ActiveState;
            string PageName;
            string TabClass;
            string TabIcon;
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
                hrefline = "<li" + ActiveState + "<a href=" + "\"" + PageName + "\"" + "\"><span class=\"" + "\"" +
                    TabClass + " " + TabIcon + "\"> </span>" + TabName + "</a></li>";
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
