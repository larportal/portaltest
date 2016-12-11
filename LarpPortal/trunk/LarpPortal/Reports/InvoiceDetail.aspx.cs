using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Reports
{
    public partial class InvoiceDetail : System.Web.UI.Page
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
            //string stStoredProc = "uspGetPELAnswerSummary"; temporary comment to get over build errors on other routines
            //string stCallingMethod = "PELAnswerSummary.FillGrid"; temporary comment to get over build errors on other routines
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            //DataTable dtAnswers = new DataTable(); temporary comment to get over build errors on other routines
            //SortedList sParams = new SortedList(); temporary comment to get over build errors on other routines
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
            //sParams.Add("@QuestionID", QuestionID); temporary comment to get over build errors on other routines
            //sParams.Add("@EventID", EventID); temporary comment to get over build errors on other routines
            //sParams.Add("@Role", RoleID); temporary comment to get over build errors on other routines
            //dtAnswers = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod); temporary comment to get over build errors on other routines

            //gvAnswers.DataSource = dtAnswers; temporary comment to get over build errors on other routines
            gvAnswers.DataBind();
        }

        protected void ddlEventLoad()
        {
            //string stStoredProc = "uspGetCampaignEvents"; temporary comment to get over build errors on other routines
            //string stCallingMethod = "PELAnswerSummary.aspx.ddlSourceEventLoad"; temporary comment to get over build errors on other routines
            //DataTable dtEvent = new DataTable(); temporary comment to get over build errors on other routines
            //SortedList sParams = new SortedList(); temporary comment to get over build errors on other routines
            int CampaignID = 0;
            int.TryParse(Session["CampaignID"].ToString(), out CampaignID);
            //sParams.Add("@CampaignID", CampaignID); temporary comment to get over build errors on other routines
            //sParams.Add("@StatusID", 51); // 51 = Completed temporary comment to get over build errors on other routines
            //sParams.Add("@EventLength", 35); // How many characters of Event Name/date to return with ellipsis temporary comment to get over build errors on other routines
            //dtEvent = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod); temporary comment to get over build errors on other routines
            ddlEvent.ClearSelection();
            ddlRole.ClearSelection();
            lblRole.Visible = false;
            ddlRole.Visible = false;
            ddlQuestion.ClearSelection();
            ddlEvent.DataTextField = "EventNameDate";
            ddlEvent.DataValueField = "EventID";
            //ddlEvent.DataSource = dtEvent; temporary comment to get over build errors on other routines
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
            //string stStoredProc = "uspGetPELRolesForEvent"; temporary comment to get over build errors on other routines
            //string stCallingMethod = "PELAnswerSummary.aspx.ddlRoleLoad"; temporary comment to get over build errors on other routines
            //DataTable dtPELRoles = new DataTable(); temporary comment to get over build errors on other routines
            //SortedList sParams = new SortedList(); temporary comment to get over build errors on other routines
            int EventID = 0;
            int.TryParse(ddlEvent.SelectedValue.ToString(), out EventID);
            //sParams.Add("@EventID", EventID); temporary comment to get over build errors on other routines
            //dtPELRoles = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod);
            ddlRole.ClearSelection();
            ddlQuestion.ClearSelection();
            lblQuestion.Visible = false;
            ddlQuestion.Visible = false;
            ddlRole.DataTextField = "PlayerType";
            ddlRole.DataValueField = "RoleID";
            //ddlRole.DataSource = dtPELRoles; temporary comment to get over build errors on other routines
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
            //string stStoredProc = "uspGetPELQuestionsForEventRole"; temporary comment to get over build errors on other routines
            //string stCallingMethod = "PELAnswerSummary.aspx.ddlQuestionLoad"; temporary comment to get over build errors on other routines
            //DataTable dtQuestions = new DataTable(); temporary comment to get over build errors on other routines
            //SortedList sParams = new SortedList(); temporary comment to get over build errors on other routines
            int EventID = 0;
            int RoleID = 0;
            int.TryParse(ddlEvent.SelectedValue.ToString(), out EventID);
            int.TryParse(ddlRole.SelectedValue.ToString(), out RoleID);
            //sParams.Add("@EventID", EventID); temporary comment to get over build errors on other routines
            //sParams.Add("@RoleID", RoleID); temporary comment to get over build errors on other routines
            //dtQuestions = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod); temporary comment to get over build errors on other routines
            ddlQuestion.ClearSelection();
            ddlQuestion.DataTextField = "Question";
            ddlQuestion.DataValueField = "PELQuestionID";
            //ddlQuestion.DataSource = dtQuestions; temporary comment to get over build errors on other routines
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
            //HtmlForm form = new HtmlForm(); temporary comment to get over build errors on other routines
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "LARPCalendar.xls"));
            Response.ContentType = "application/ms-excel";
            //StringWriter sw = new StringWriter(); temporary comment to get over build errors on other routines
            //HtmlTextWriter hw = new HtmlTextWriter(sw); temporary comment to get over build errors on other routines
            gvAnswers.AllowPaging = false;
            //BindGridDetails(gvAnswers); temporary comment to get over build errors on other routines
            //form.Attributes["runat"] = "server"; temporary comment to get over build errors on other routines
            //form.Controls.Add(gvAnswers); temporary comment to get over build errors on other routines
            //this.Controls.Add(form); temporary comment to get over build errors on other routines
            //form.RenderControl(hw); temporary comment to get over build errors on other routines
            string style = @"<!--mce:2-->";
            Response.Write(style);
            //Response.Output.Write(sw.ToString()); temporary comment to get over build errors on other routines
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
            //StringBuilder sb = new StringBuilder(); temporary comment to get over build errors on other routines
            for (int k = 0; k < gvAnswers.Columns.Count; k++)
            {
                //add separator 
                //sb.Append(gvAnswers.Columns[k].HeaderText + ','); temporary comment to get over build errors on other routines
            }
            //append new line 
            //sb.Append("\r\n"); temporary comment to get over build errors on other routines
            for (int i = 0; i < gvAnswers.Rows.Count; i++)
            {
                for (int k = 0; k < gvAnswers.Columns.Count; k++)
                {
                    //add separator 
                    //sb.Append(gvAnswers.Rows[i].Cells[k].Text + ','); temporary comment to get over build errors on other routines
                }
                //append new line 
                //sb.Append("\r\n"); temporary comment to get over build errors on other routines
            }
            //Response.Output.Write(sb.ToString()); temporary comment to get over build errors on other routines
            Response.Flush();
            Response.End();
        }



    }
}