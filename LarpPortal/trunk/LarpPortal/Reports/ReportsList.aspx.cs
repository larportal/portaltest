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

namespace LarpPortal.Reports
{
    public partial class ReportsList : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "Reports";
            string uName = "";
            int uID = 0;
            if (Session["Username"] != null)
                uName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                uID = (Session["UserID"].ToString().ToInt32());
            BuildReportTable(uID);
        }

        private void BuildReportTable(int UserID)
        {
            int CampaignID = 0;
            string stStoredProc = "uspGetReports";
            string stCallingMethod = "ReportsList.aspx.BuildReportTable";
            if (Session["CampaignID"] != null)
                CampaignID = (Session["CampaignID"].ToString().ToInt32());
            string CampaignDDL = "";
            if (Session["CampaignName"] != null)
                CampaignDDL = Session["CampaignName"].ToString();
            DataTable dtReports = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@UserID", UserID);
            sParams.Add("@CampaignID", CampaignID);
            dtReports = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", "Reporting", stCallingMethod);
            DataView dvReports = new DataView(dtReports, "", "", DataViewRowState.CurrentRows);
            gvReportsList.DataSource = dvReports;
            gvReportsList.DataBind();

        }
    }
}
