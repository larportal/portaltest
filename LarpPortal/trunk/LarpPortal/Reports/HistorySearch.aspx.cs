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
    public partial class HistorySearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void gvHistorySearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void FillGrid()
        {
            //int iTemp = 0;
            int CampaignID = 0;
            string Keyword1;
            Keyword1 = "xxxxxx";
            if (txtKeyword.Text != null)
                Keyword1 = txtKeyword.Text;
            int UserID = 0;
            string UserName;
            string stStoredProc = "uspGetHistoriesWithKeywords";
            string stCallingMethod = "HistorySearch.FillGrid";
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            DataTable dtHistorySearch = new DataTable();
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
            int.TryParse(Session["CampaignID"].ToString(), out CampaignID);
            sParams.Add("@CampaignID", CampaignID);
            sParams.Add("@Keyword1", Keyword1);
            sParams.Add("@PadLength", 60);
            dtHistorySearch = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            gvHistorySearch.DataSource = dtHistorySearch;
            gvHistorySearch.DataBind();
        }

        protected void btnRunReport_Click(object sender, EventArgs e)
        {
            btnExportExcel.Visible = true;
            FillGrid();
            pnlReportOutput.Visible = true;
            Session["Keyword"] = txtKeyword.Text;
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "LARPCalendar.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvHistorySearch.AllowPaging = false;
            form.Attributes["runat"] = "server";
            form.Controls.Add(gvHistorySearch);
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
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=LARPCalendar.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            gvHistorySearch.AllowPaging = false;
            gvHistorySearch.DataBind();
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < gvHistorySearch.Columns.Count; k++)
            {
                //add separator 
                sb.Append(gvHistorySearch.Columns[k].HeaderText + ',');
            }
            //append new line 
            sb.Append("\r\n");
            for (int i = 0; i < gvHistorySearch.Rows.Count; i++)
            {
                for (int k = 0; k < gvHistorySearch.Columns.Count; k++)
                {
                    //add separator 
                    sb.Append(gvHistorySearch.Rows[i].Cells[k].Text + ',');
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