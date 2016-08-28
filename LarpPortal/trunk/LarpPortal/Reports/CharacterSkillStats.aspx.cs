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
    public partial class CharacterSkillStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlEventLoad();
                Session["ReportName"] = "";
                //FillGrid();
            }
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
            if (ddlEvent.SelectedIndex == 0)
            {
                rdoSkillCount.Visible = false;
                rdoSkillDetail.Visible = false;
                rdoSkillTypeCount.Visible = false;
                rdoSkillTypeDetail.Visible = false;
                btnRunReport.Visible = false;
            }
            else
            {
                rdoSkillCount.Visible = true;
                rdoSkillDetail.Visible = true;
                rdoSkillTypeCount.Visible = true;
                rdoSkillTypeDetail.Visible = true;
                btnRunReport.Visible = true;
            }
        }

        protected void FillGrid()
        {
            int UserID = 0;
            string UserName;
            string stStoredProc = "";
            string stCallingMethod = "";
            DataTable dtSkillStats = new DataTable();
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
            int CampaignID = 0;
            int.TryParse(ddlEvent.SelectedValue.ToString(), out EventID);
            int.TryParse(Session["CampaignID"].ToString(), out CampaignID);
            sParams.Add("@EventID", EventID);
            sParams.Add("@CampaignID", CampaignID);

            if (rdoSkillCount.Checked)
            {
                Session["ReportName"] = "Skill Count";
                pnlReportOutputSkillCount.Visible = true;
                pnlReportOutputSkillDetail.Visible = false;
                pnlReportOutputSkillTypeCount.Visible = false;
                pnlReportOutputSkillTypeDetail.Visible = false;
                stStoredProc = "uspRptCharacterEventSkillCount";
                stCallingMethod = "CharacterSkillStats.aspx.FillGrid.SkillCount";
                dtSkillStats = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
                gvSkillCount.DataSource = dtSkillStats;
                gvSkillCount.DataBind();
            }

            if (rdoSkillDetail.Checked)
            {
                Session["ReportName"] = "Skill Detail";
                pnlReportOutputSkillCount.Visible = false;
                pnlReportOutputSkillDetail.Visible = true;
                pnlReportOutputSkillTypeCount.Visible = false;
                pnlReportOutputSkillTypeDetail.Visible = false;
                stStoredProc = "uspRptCharacterEventSkillDetail";
                stCallingMethod = "CharacterSkillStats.aspx.FillGrid.SkillDetail";
                dtSkillStats = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
                gvSkillDetail.DataSource = dtSkillStats;
                gvSkillDetail.DataBind();
            }

            if (rdoSkillTypeCount.Checked)
            {
                Session["ReportName"] = "Skill Type Count";
                pnlReportOutputSkillCount.Visible = false;
                pnlReportOutputSkillDetail.Visible = false;
                pnlReportOutputSkillTypeCount.Visible = true;
                pnlReportOutputSkillTypeDetail.Visible = false;
                stStoredProc = "uspRptCharacterEventSkillTypeCount";
                stCallingMethod = "CharacterSkillStats.aspx.FillGrid.SkillTypeCount";
                dtSkillStats = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
                gvSkillTypeCount.DataSource = dtSkillStats;
                gvSkillTypeCount.DataBind();
            }

            if (rdoSkillTypeDetail.Checked)
            {
                Session["ReportName"] = "Skill Type Detail";
                pnlReportOutputSkillCount.Visible = false;
                pnlReportOutputSkillDetail.Visible = false;
                pnlReportOutputSkillTypeCount.Visible = false;
                pnlReportOutputSkillTypeDetail.Visible = true;
                stStoredProc = "uspRptCharacterEventSkillTypeDetail";
                stCallingMethod = "CharacterSkillStats.aspx.FillGrid.SkillTypeDetail";
                dtSkillStats = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
                gvSkillTypeDetail.DataSource = dtSkillStats;
                gvSkillTypeDetail.DataBind();
            }
        }

        protected void grpPrintBy_CheckedChanged(object sender, EventArgs e)
        {
            btnRunReport.Visible = true;
            pnlReportOutputSkillCount.Visible = false;
            pnlReportOutputSkillDetail.Visible = false;
            pnlReportOutputSkillTypeCount.Visible = false;
            pnlReportOutputSkillTypeDetail.Visible = false;
            btnExportExcel.Visible = false;
            FillGrid();
        }

        protected void btnRunReport_Click(object sender, EventArgs e)
        {
            string ReportName;
            if (Session["ReportName"] != null)
                ReportName = Session["ReportName"].ToString();
            else
                ReportName = "";
            //btnExportCSV.Visible = true;
            btnExportExcel.Visible = true;
            FillGrid();
            switch (ReportName)
            {
                case "Skill Count":
                    pnlReportOutputSkillCount.Visible = true;
                    break;

                case "Skill Detail":
                    pnlReportOutputSkillDetail.Visible = true;
                    break;

                case "Skill Type Count":
                    pnlReportOutputSkillTypeCount.Visible = true;
                    break;

                case "Skill Type Detail":
                    pnlReportOutputSkillTypeDetail.Visible = true;
                    break;

                default:

                    break;
            }

        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string ReportName;
            if (Session["ReportName"] != null)
                ReportName = Session["ReportName"].ToString();
            else
                ReportName = "";
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", ReportName + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //BindGridDetails(gvCheckList);
            form.Attributes["runat"] = "server";
            switch (ReportName)
            {
                case "Skill Count":
                    gvSkillCount.AllowPaging = false;
                    form.Controls.Add(gvSkillCount);
                    break;

                case "Skill Detail":
                    gvSkillDetail.AllowPaging = false;
                    form.Controls.Add(gvSkillDetail);
                    break;

                case "Skill Type Count":
                    gvSkillTypeCount.AllowPaging = false;
                    form.Controls.Add(gvSkillTypeCount);
                    break;

                case "Skill Type Detail":
                    gvSkillTypeDetail.AllowPaging = false;
                    form.Controls.Add(gvSkillTypeDetail);
                    break;

                default:

                    break;

            }

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
            gvSkillCount.AllowPaging = false;
            gvSkillCount.DataBind();
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < gvSkillCount.Columns.Count; k++)
            {
                //add separator 
                sb.Append(gvSkillCount.Columns[k].HeaderText + ',');
            }
            //append new line 
            sb.Append("\r\n");
            for (int i = 0; i < gvSkillCount.Rows.Count; i++)
            {
                for (int k = 0; k < gvSkillCount.Columns.Count; k++)
                {
                    //add separator 
                    sb.Append(gvSkillCount.Rows[i].Cells[k].Text + ',');
                }
                //append new line 
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void gvSkillCount_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvSkillDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvSkillTypeCount_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvSkillTypeDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}