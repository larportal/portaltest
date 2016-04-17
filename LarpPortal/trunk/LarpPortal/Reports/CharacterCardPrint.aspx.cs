using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

namespace LarpPortal.Reports
{
    public partial class CharacterCardPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlEventLoad();
            }
        }

        protected void ddlEventLoad()
        {
            string stStoredProc = "uspGetCampaignEvents";
            string stCallingMethod = "CharacterCardPrint.aspx.ddlEventLoad";
            DataTable dtEvent = new DataTable();
            SortedList sParams = new SortedList();
            int CampaignID = 0;
            int.TryParse(Session["CampaignID"].ToString(), out CampaignID);
            sParams.Add("@CampaignID", CampaignID);
            sParams.Add("@StatusID", 50); // 50 = Scheduled
            sParams.Add("@EventLength", 50); // How many characters of Event Name/date to return with ellipsis
            dtEvent = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod);
            ddlEvent.ClearSelection();
            ddlEvent.DataTextField = "EventNameDate";
            ddlEvent.DataValueField = "EventID";
            ddlEvent.DataSource = dtEvent;
            ddlEvent.DataBind();
            ddlEvent.Items.Insert(0, new ListItem("Select Event - Date", "0"));
            ddlEvent.SelectedIndex = 0;
        }

        protected void FillGrid()
        {

        }

        protected void btnRunReport_Click(object sender, EventArgs e)
        {
            string URLPath = "../Character/eventcharcards?";
            int CampaignID = 0;
            int EventID = 0;
            int.TryParse(Session["CampaignID"].ToString(), out CampaignID);
            int.TryParse(ddlEvent.SelectedValue.ToString(), out EventID);
            if(rdoButtonCampaign.Checked)
            {
                URLPath = URLPath + "CampaignID=" + CampaignID;
                Response.Write("<script type='text/javascript'>window.open('" + URLPath + "','_blank');</script>");
            }
            if(rdoButtonEvent.Checked)
            {
                URLPath = URLPath + "EventID=" + EventID;
                Response.Write("<script type='text/javascript'>window.open('" + URLPath + "','_blank');</script>");
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        protected void btnExportCSV_Click(object sender, EventArgs e)
        {

        }

        protected void grpPrintBy_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoButtonEvent.Checked)
            {
                ddlEvent.Visible = true;
                if(ddlEvent.SelectedIndex > 0)
                {
                    btnRunReport.Visible = true;
                }
                else
                {
                    btnRunReport.Visible = false;
                }
            }
            else
            {
                btnRunReport.Visible = true;
                ddlEvent.Visible = false;
            }
        }

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEvent.SelectedIndex > 0)
                btnRunReport.Visible = true;
            else
                btnRunReport.Visible = false;
        }

    }
}