﻿using System;
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
    public partial class CampaignPlayers : System.Web.UI.Page
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

        protected void gvCampaignPlayers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void FillGrid()
        {
            //int iTemp = 0;
            int UserID = 0;
            string UserName;
            int CampaignID = 0;
            string stStoredProc = "uspRptCampaignPlayers";
            string stCallingMethod = "CampaignPlayers.FillGrid";
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            DataTable dtCampaignPlayers = new DataTable();
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
            if (Session["CampaignID"].ToString() != null)
            {
                int.TryParse(Session["CampaignID"].ToString(), out CampaignID);
            }

            sParams.Add("@CampaignID", CampaignID);
            dtCampaignPlayers = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            gvCampaignPlayers.DataSource = dtCampaignPlayers;
            gvCampaignPlayers.DataBind();
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
            gvCampaignPlayers.AllowPaging = false;
            //BindGridDetails(gvCalendar);
            form.Attributes["runat"] = "server";
            form.Controls.Add(gvCampaignPlayers);
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
            gvCampaignPlayers.AllowPaging = false;
            gvCampaignPlayers.DataBind();
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < gvCampaignPlayers.Columns.Count; k++)
            {
                //add separator 
                sb.Append(gvCampaignPlayers.Columns[k].HeaderText + ',');
            }
            //append new line 
            sb.Append("\r\n");
            for (int i = 0; i < gvCampaignPlayers.Rows.Count; i++)
            {
                for (int k = 0; k < gvCampaignPlayers.Columns.Count; k++)
                {
                    //add separator 
                    sb.Append(gvCampaignPlayers.Rows[i].Cells[k].Text + ',');
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