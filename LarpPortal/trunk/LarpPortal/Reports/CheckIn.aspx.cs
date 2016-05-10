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
    public partial class CheckIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlEventLoad();
                FillGrid();
            }
        }

        protected void gvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void FillGrid()
        {
            //int iTemp = 0;
            int UserID = 0;
            string UserName;
            string stStoredProc = "uspRptCheckIn";
            string stCallingMethod = "CheckIn.aspx.cs.FillGrid";
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            DataTable dtCheckList = new DataTable();
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
            int EventID = 0;
            int.TryParse(ddlEvent.SelectedValue.ToString(), out EventID);
            sParams.Add("@EventID", EventID);
            dtCheckList = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);

            gvCheckList.DataSource = dtCheckList;
            gvCheckList.DataBind();
        }

        protected void ddlEventLoad()
        {
            string stStoredProc = "uspGetCampaignEvents";
            string stCallingMethod = "Checkin.aspx.ddlEventLoad";
            DataTable dtEvent = new DataTable();
            SortedList sParams = new SortedList();
            int CampaignID = 0;
            int.TryParse(Session["CampaignID"].ToString(), out CampaignID);
            sParams.Add("@CampaignID", CampaignID);
            sParams.Add("@StatusID", 50); // 50 = Scheduled
            sParams.Add("@EventLength", 35); // How many characters of Event Name/date to return with ellipsis
            dtEvent = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod);
            ddlEvent.ClearSelection();
            ddlEvent.DataTextField = "EventNameDate";
            ddlEvent.DataValueField = "EventID";
            ddlEvent.DataSource = dtEvent;
            ddlEvent.DataBind();
            ddlEvent.Items.Insert(0, new ListItem("Select Event - Date", "0"));
            ddlEvent.SelectedIndex = 0;
        }

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEventName.Text = ddlEvent.SelectedItem.ToString();
            btnRunReport.Visible = true;
        }

        protected void btnRunReport_Click(object sender, EventArgs e)
        {
            //btnExportCSV.Visible = true;
            btnExportExcel.Visible = true;
            FillGrid();
            pnlReportOutput.Visible = true;
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "EventCheckIn.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvCheckList.AllowPaging = false;
            //BindGridDetails(gvCheckList);
            form.Attributes["runat"] = "server";
            form.Controls.Add(gvCheckList);
            this.Controls.Add(form);
            form.RenderControl(hw);
            string style = @"<!--mce:2-->";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnExportCSV_Click(object sender, EventArgs e)
        {
            //BindGridDetails(gvCalendar);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=LARPCalendar.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            gvCheckList.AllowPaging = false;
            gvCheckList.DataBind();
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < gvCheckList.Columns.Count; k++)
            {
                //add separator 
                sb.Append(gvCheckList.Columns[k].HeaderText + ',');
            }
            //append new line 
            sb.Append("\r\n");
            for (int i = 0; i < gvCheckList.Rows.Count; i++)
            {
                for (int k = 0; k < gvCheckList.Columns.Count; k++)
                {
                    //add separator 
                    sb.Append(gvCheckList.Rows[i].Cells[k].Text + ',');
                }
                //append new line 
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

    }
}