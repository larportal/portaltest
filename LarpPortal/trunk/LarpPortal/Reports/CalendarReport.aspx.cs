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

namespace LarpPortal.Reports
{
    public partial class CalendarReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }

        protected void FillGrid()
        {
            int iTemp = 0;
            int intCampaignID = 0;
            int UserID = 0;
            int CampaignSorter = 0;
            string UserName;
            string stStoredProc = "uspGetEventCalendar";
            string stCallingMethod = "CalendarReport.FillGrid";
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            int CampaignID = 0;
            DataTable dtCalendar = new DataTable();
            SortedList sParams = new SortedList();
            if (Session["UserID"].ToString() != null)
            {
                int.TryParse(Session["UserID"].ToString(), out UserID);
            }
            if (Session["UserName"] != null)
            {
                UserName = Session["UserName"].ToString();
            }
            else
            {
                UserName = "Reporting";
            }
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
            {
                intCampaignID = iTemp;
            }

            switch (ddlEventDateRange.SelectedValue)
            {
                case "1":
                    StartDate = DateTime.Today;
                    EndDate = DateTime.Today;
                    EndDate = EndDate.AddYears(10); // Look out 10 years
                    break;

                case "2":
                    StartDate = DateTime.Today;
                    EndDate = DateTime.Today;
                    EndDate = EndDate.AddMonths(6); // Look out 6 months
                    break;

                case "3":
                    StartDate = DateTime.Today;
                    EndDate = DateTime.Today;
                    StartDate = StartDate.AddMonths(-3); // Look back 3 months
                    break;

                case "4":
                    StartDate = DateTime.Today;
                    StartDate = StartDate.AddMonths(-6); // Look back 6 months
                    EndDate = DateTime.Today;
                    break;

                case "5":
                    StartDate = DateTime.Today;
                    EndDate = DateTime.Today;
                    StartDate = StartDate.AddYears(-1); // Look back 12 months
                    break;

                case "6":
                    StartDate = DateTime.Today;
                    EndDate = DateTime.Today;
                    StartDate = StartDate.AddYears(-100); // All historical
                    break;

                default:
                    StartDate = DateTime.Today;
                    EndDate = DateTime.Today;
                    EndDate = EndDate.AddYears(10); // Look out 10 years
                    break;
            }

            switch (ddlCampaignChoice.SelectedValue)
            {
                case "1":
                    CampaignID = 0; // All MY campaigns
                    break;

                case "2":
                    if (Session["CampaignID"].ToString() != null)
                    {
                        int.TryParse((Session["CampaignID"].ToString()), out CampaignID);   // Selected campaign
                    }
                    break;

                case "3":
                    int.TryParse((Session["CampaignID"].ToString()), out CampaignID);; // Campaigns in the selected game system
                    break;

                case "4":
                    CampaignID = -1; // ALL LARP Portal campaigns
                    break;

                default:
                    CampaignID = 0;
                    break;
            }

            switch (ddlOrderBy.SelectedValue)
            {
                case "1":

                    break;

                case "2":

                    break;

                case "3":

                    break;

                default:

                    break;
            }

            if (Session["CampaignID"].ToString() != null)
            {
                int.TryParse((Session["CampaignID"].ToString()), out CampaignID);   // Selected campaign
            }
            int.TryParse((ddlOrderBy.SelectedValue.ToString()), out CampaignSorter);
            sParams.Add("@UserID", UserID);
            sParams.Add("@OrderBy", ddlOrderBy.SelectedValue);
            sParams.Add("@CampaignID", CampaignID);
            sParams.Add("@CampaignChoice", ddlCampaignChoice.SelectedValue);
            sParams.Add("@StartDate", StartDate);
            sParams.Add("@EndDate", EndDate);
            sParams.Add("CampaignSorter", CampaignSorter);
            dtCalendar = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            gvCalendar.DataSource = dtCalendar;
            gvCalendar.DataBind();
        }

        protected void ddlEventDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCampaignChoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnRunReport_Click(object sender, EventArgs e)
        {
            btnExportCSV.Visible = true;
            btnExportExcel.Visible = true;
            FillGrid();
            pnlReportOutput.Visible = true;
        }

        protected void gvCalendar_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        protected void btnExportCSV_Click(object sender, EventArgs e)
        {

        }
    }
}