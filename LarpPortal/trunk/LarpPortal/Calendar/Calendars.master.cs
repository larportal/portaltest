using LarpPortal.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Calendars
{
    public partial class Calendars : System.Web.UI.MasterPage
    {
        public event EventHandler CampaignChanged;
        public int _UserID;
        public string _UserName;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Calendar";
            if (Session["CalendarCampaignID"] == null)
                Session["CalendarCampaignID"] = "-1";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            string PageName = Path.GetFileName(Request.PhysicalPath).ToUpper();
            PageName = Request.Url.AbsoluteUri.ToUpper();

            if (!IsPostBack)
            {
                if (PageName.Contains("CALENDARRPT"))
                    liCalendarReport.Attributes.Add("class", "active");
                if (PageName.Contains("CAMPAIGNCALENDAR"))
                    liCampCalendar.Attributes.Add("class", "active");

                SortedList sParams = new SortedList();
                DataTable dtCampaigns = Classes.cUtilities.LoadDataTable("uspGetActiveCampaigns", sParams, "LARPortal", _UserName, lsRoutineName);
                DataView dvCampaigns = new DataView(dtCampaigns, "", "CampaignName", DataViewRowState.CurrentRows);

                ddlCampList.DataSource = dvCampaigns;
                ddlCampList.DataTextField = "CampaignName";
                ddlCampList.DataValueField = "CampaignID";
                ddlCampList.DataBind();

                ddlCampList.Items.Insert(0, new ListItem("My Campaigns", "-1"));
                ddlCampList.Items.Insert(0, new ListItem("All Campaigns", "-2"));

                ddlCampList.ClearSelection();
                ddlCampList.Items[1].Selected = true;
                ddlCampList_SelectedIndexChanged(null, null);
            }

            if (Session["CalendarCampaignID"] == null)
                Session["CalendarCampaignID"] = "-1";

            try
            {
                ddlCampList.SelectedValue = Session["CalendarCampaignID"].ToString();
                if (ddlCampList.SelectedIndex < 0)
                    ddlCampList.SelectedValue = "-1";
            }
            catch
            {

            }
        }

        protected void ddlCampList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CalendarCampaignID"] = ddlCampList.SelectedValue;

            if (this.CampaignChanged != null)
                this.CampaignChanged(this, e);
        }
    }
}