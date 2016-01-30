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
                //FillGrid();
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
            sParams.Add("@StatusID", 51); // 51 = Completed
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

        //protected void gvCalendar_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //}

        protected void FillGrid()
        {
            //int iTemp = 0;
            //int UserID = 0;
            //string UserName;
            //string stStoredProc = "{StoredProcName}";
            //string stCallingMethod = "{aspxName}.FillGrid";
            //DateTime StartDate = DateTime.Today;
            //DateTime EndDate = DateTime.Today;
            //DataTable dtCalendar = new DataTable();
            //SortedList sParams = new SortedList();
            //if (Session["UserID"].ToString() != null)
            //{
            //    int.TryParse(Session["UserID"].ToString(), out UserID);
            //}
            //if (Session["UserName"] != null)
            //{
            //    UserName = Session["UserName"].ToString();
            //}
            //else
            //{
            //    UserName = "Reporting";
            //}

            //sParams.Add("@UserID", UserID);
            //sParams.Add("@Parameter2", 0);      // etc
            //dtCalendar = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            //gvCalendar.DataSource = dtCalendar;
            //gvCalendar.DataBind();
        }

        protected void btnRunReport_Click(object sender, EventArgs e)
        {
            //btnExportCSV.Visible = true;
            //btnExportExcel.Visible = true;
            //FillGrid();
            //pnlReportOutput.Visible = true;
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
            //HtmlForm form = new HtmlForm();
            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "LARPCalendar.xls"));
            //Response.ContentType = "application/ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //gvCalendar.AllowPaging = false;
            ////BindGridDetails(gvCalendar);
            //form.Attributes["runat"] = "server";
            //form.Controls.Add(gvCalendar);
            //this.Controls.Add(form);
            //form.RenderControl(hw);
            //string style = @"<!--mce:2-->";
            //Response.Write(style);
            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();
        }

        protected void btnExportCSV_Click(object sender, EventArgs e)
        {
            //BindGridDetails(gvCalendar);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=LARPCalendar.csv");
            //Response.Charset = "";
            //Response.ContentType = "application/text";
            //gvCalendar.AllowPaging = false;
            //gvCalendar.DataBind();
            //StringBuilder sb = new StringBuilder();
            //for (int k = 0; k < gvCalendar.Columns.Count; k++)
            //{
            //    //add separator 
            //    sb.Append(gvCalendar.Columns[k].HeaderText + ',');
            //}
            ////append new line 
            //sb.Append("\r\n");
            //for (int i = 0; i < gvCalendar.Rows.Count; i++)
            //{
            //    for (int k = 0; k < gvCalendar.Columns.Count; k++)
            //    {
            //        //add separator 
            //        sb.Append(gvCalendar.Rows[i].Cells[k].Text + ',');
            //    }
            //    //append new line 
            //    sb.Append("\r\n");
            //}
            //Response.Output.Write(sb.ToString());
            //Response.Flush();
            //Response.End();
        }

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEvent.SelectedIndex > 0)
                btnRunReport.Visible = true;
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

    }
}