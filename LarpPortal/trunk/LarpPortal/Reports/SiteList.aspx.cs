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
    public partial class SiteList1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int UserID = 0;
                string UserName;
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
                    UserName = "SiteList";
                }
                ddlStateLoad(UserName);
                ddlCountryLoad(UserName);
                FillGrid();
            }
        }

        protected void gvSites_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlStateLoad(string UserName)
        {
            string stStoredProc = "uspGetSiteStates";
            string stCallingMethod = "SiteList.ddlStateLoad";
            DataTable dtState = new DataTable();
            SortedList sParams = new SortedList();
            dtState = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            ddlState.DataTextField = "State";
            ddlState.DataValueField = "RowID";
            ddlState.DataSource = dtState;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("All", "0"));
            ddlState.SelectedIndex = 0;
        }

        protected void ddlCountryLoad(string UserName)
        {
            string stStoredProc = "uspGetSiteCountries";
            string stCallingMethod = "SiteList.ddlCountryLoad";
            DataTable dtCountry = new DataTable();
            SortedList sParams = new SortedList();
            dtCountry = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            ddlCountry.DataTextField = "Country";
            ddlCountry.DataValueField = "RowID";
            ddlCountry.DataSource = dtCountry;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("All", "0"));
            ddlCountry.SelectedIndex = 0;
        }

        protected void FillGrid()
        {
            int UserID = 0;
            string UserName;
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
                UserName = "SiteList";
            }
            string stStoredProc = "uspGetSiteList";
            string stCallingMethod = "SiteList.FillGrid";
            DataTable dtSites = new DataTable();
            SortedList sParams = new SortedList();
            string sState = ddlState.SelectedItem.Text;
            string sCountry = ddlCountry.SelectedItem.Text;
            if (sState == "All")
                sState = "0";
            if (sCountry == "All")
                sCountry = "0";
            sParams.Add("@State", sState);
            sParams.Add("@Country", sCountry);
            dtSites = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            gvSites.DataSource = dtSites;
            gvSites.DataBind();
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
            gvSites.AllowPaging = false;
            //BindGridDetails(gvCalendar);
            form.Attributes["runat"] = "server";
            form.Controls.Add(gvSites);
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
            gvSites.AllowPaging = false;
            gvSites.DataBind();
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < gvSites.Columns.Count; k++)
            {
                //add separator 
                sb.Append(gvSites.Columns[k].HeaderText + ',');
            }
            //append new line 
            sb.Append("\r\n");
            for (int i = 0; i < gvSites.Rows.Count; i++)
            {
                for (int k = 0; k < gvSites.Columns.Count; k++)
                {
                    //add separator 
                    sb.Append(gvSites.Rows[i].Cells[k].Text + ',');
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