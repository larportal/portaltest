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
    public partial class PELAnswerSummary : System.Web.UI.Page
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

        protected void gvAnswers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void FillGrid()
        {
            //int iTemp = 0;
            int UserID = 0;
            string UserName;
            string stStoredProc = "uspGetPELAnswerSummary";
            string stCallingMethod = "PELAnswerSummary.FillGrid";
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            DataTable dtAnswers = new DataTable();
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
            int QuestionID = 0;
            int EventID = 0;
            int RoleID = 0;
            int.TryParse(ddlQuestion.SelectedValue.ToString(), out QuestionID);
            int.TryParse(ddlEvent.SelectedValue.ToString(), out EventID);
            int.TryParse(ddlRole.SelectedValue.ToString(), out RoleID);
            sParams.Add("@QuestionID", QuestionID);
            sParams.Add("@EventID", EventID);
            sParams.Add("@Role", RoleID);
            dtAnswers = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);

            gvAnswers.DataSource = dtAnswers;
            gvAnswers.DataBind();
        }

        protected void ddlEventLoad()
        {
            string stStoredProc = "uspGetCampaignEvents";
            string stCallingMethod = "PELAnswerSummary.aspx.ddlSourceEventLoad";
            DataTable dtEvent = new DataTable();
            SortedList sParams = new SortedList();
            int CampaignID = 0;
            int.TryParse(Session["CampaignID"].ToString(), out CampaignID);
            sParams.Add("@CampaignID", CampaignID);
            sParams.Add("@StatusID", 51); // 51 = Completed
            sParams.Add("@EventLength", 35); // How many characters of Event Name/date to return with ellipsis
            dtEvent = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod);
            ddlEvent.ClearSelection();
            ddlRole.ClearSelection();
            lblRole.Visible = false;
            ddlRole.Visible = false;
            ddlQuestion.ClearSelection();
            ddlEvent.DataTextField = "EventNameDate";
            ddlEvent.DataValueField = "EventID";
            ddlEvent.DataSource = dtEvent;
            ddlEvent.DataBind();
            ddlEvent.Items.Insert(0, new ListItem("Select Event - Date", "0"));
            ddlEvent.SelectedIndex = 0;
        }

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check what roles had PELs for that event and fill ddlRole
            ddlRoleLoad();            
        }

        protected void ddlRoleLoad()
        {
            string stStoredProc = "uspGetPELRolesForEvent";
            string stCallingMethod = "PELAnswerSummary.aspx.ddlRoleLoad";
            DataTable dtPELRoles = new DataTable();
            SortedList sParams = new SortedList();
            int EventID = 0;
            int.TryParse(ddlEvent.SelectedValue.ToString(), out EventID);
            sParams.Add("@EventID", EventID);
            dtPELRoles = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod);
            ddlRole.ClearSelection();
            ddlQuestion.ClearSelection();
            lblQuestion.Visible = false;
            ddlQuestion.Visible = false;
            ddlRole.DataTextField = "PlayerType";
            ddlRole.DataValueField = "RoleID";
            ddlRole.DataSource = dtPELRoles;
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("Select Player Type", "0"));
            ddlRole.SelectedIndex = 0;
            lblRole.Visible = true;
            ddlRole.Visible = true;
            btnRunReport.Visible = true;
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check what PELs had questions for that roll and fill ddlQuestion
            ddlQuestionLoad();  
        }

        protected void ddlQuestionLoad()
        {
            string stStoredProc = "uspGetPELQuestionsForEventRole";
            string stCallingMethod = "PELAnswerSummary.aspx.ddlQuestionLoad";
            DataTable dtQuestions = new DataTable();
            SortedList sParams = new SortedList();
            int EventID = 0;
            int RoleID = 0;
            int.TryParse(ddlEvent.SelectedValue.ToString(), out EventID);
            int.TryParse(ddlRole.SelectedValue.ToString(), out RoleID);
            sParams.Add("@EventID", EventID);
            sParams.Add("@RoleID", RoleID);
            dtQuestions = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod);
            ddlQuestion.ClearSelection();
            ddlQuestion.DataTextField = "Question";
            ddlQuestion.DataValueField = "PELQuestionID";
            ddlQuestion.DataSource = dtQuestions;
            ddlQuestion.DataBind();
            ddlQuestion.Items.Insert(0, new ListItem("Select Question", "0"));
            ddlQuestion.SelectedIndex = 0;
            ddlQuestion.Visible = true;
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
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "LARPCalendar.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvAnswers.AllowPaging = false;
            //BindGridDetails(gvAnswers);
            form.Attributes["runat"] = "server";
            form.Controls.Add(gvAnswers);
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
            gvAnswers.AllowPaging = false;
            gvAnswers.DataBind();
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < gvAnswers.Columns.Count; k++)
            {
                //add separator 
                sb.Append(gvAnswers.Columns[k].HeaderText + ',');
            }
            //append new line 
            sb.Append("\r\n");
            for (int i = 0; i < gvAnswers.Rows.Count; i++)
            {
                for (int k = 0; k < gvAnswers.Columns.Count; k++)
                {
                    //add separator 
                    sb.Append(gvAnswers.Rows[i].Cells[k].Text + ',');
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