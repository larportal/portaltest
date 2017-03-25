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

namespace LarpPortal.CalendarRpt
{
    public partial class CalendarRpt : System.Web.UI.Page
    {
        protected int _UserID = 0;
        protected string _UserName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"].ToString() != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
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
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

//            int iTemp = 0;
//            int intCampaignID = 0;
//            int UserID = 0;
//            int CampaignSorter = 0;
//            string stStoredProc = "uspGetEventCalendar";
//            string stCallingMethod = "CalendarReport.FillGrid";
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today;
//            int CampaignID = 0;
            DataTable dtCalendar = new DataTable();
            SortedList sParams = new SortedList();

            //if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
            //{
            //    intCampaignID = iTemp;
            //}

            StartDate = DateTime.Today;
            EndDate = DateTime.Today;

            switch (ddlEventDateRange.SelectedValue)
            {
                case "1":
                    EndDate = EndDate.AddYears(10); // Look out 10 years
                    break;

                case "2":
                    EndDate = EndDate.AddMonths(6); // Look out 6 months
                    break;

                case "3":
                    StartDate = StartDate.AddMonths(-3); // Look back 3 months
                    break;

                case "4":
                    StartDate = StartDate.AddMonths(-6); // Look back 6 months
                    break;

                case "5":
                    StartDate = StartDate.AddYears(-1); // Look back 12 months
                    break;

                case "6":
                    StartDate = StartDate.AddYears(-100); // All historical
                    break;

                default:
                    EndDate = EndDate.AddYears(10); // Look out 10 years
                    break;
            }

            //switch (ddlCampaignChoice.SelectedValue)
            //{
            //    case "1":
            //        CampaignID = 0; // All MY campaigns
            //        break;

            //    case "2":
            //        if (Session["CampaignID"].ToString() != null)
            //        {
            //            int.TryParse((Session["CampaignID"].ToString()), out CampaignID);   // Selected campaign
            //        }
            //        break;

            //    case "3":
            //        int.TryParse((Session["CampaignID"].ToString()), out CampaignID); ; // Campaigns in the selected game system
            //        break;

            //    case "4":
            //        CampaignID = -1; // ALL LARP Portal campaigns
            //        break;

            //    default:
            //        CampaignID = 0;
            //        break;
            //}

            if (Session["CalendarCampaignID"] == null)
                Session["CalendarCampaignID"] = "-1";

            int CalCampID;
            if (!int.TryParse(Session["CalendarCampaignID"].ToString(), out CalCampID))
                CalCampID = -1;

            //if (CalCampID == -1)
            //    CampaignID = 0;
            //else if (CalCampID == -2)
            //    CampaignID = -1;
            //else
            //    CampaignID = CalCampID;

            //switch (ddlOrderBy.SelectedValue)
            //{
            //    case "1":

            //        break;

            //    case "2":

            //        break;

            //    case "3":

            //        break;

            //    default:

            //        break;
            //}

            //if (Session["CampaignID"].ToString() != null)
            //{
            //    int.TryParse((Session["CampaignID"].ToString()), out CampaignID);   // Selected campaign
            //}
            //int.TryParse((ddlOrderBy.SelectedValue.ToString()), out CampaignSorter);
            //sParams.Add("@UserID", UserID);
            //sParams.Add("@OrderBy", ddlOrderBy.SelectedValue);
            //sParams.Add("@CampaignID", CampaignID); // Which campaign is picked in the drop down list
            //sParams.Add("@CampaignChoice", ddlCampaignChoice.SelectedValue);    // 1 All my campaigns / 2 Selected campaign / 3 Selected game system / 4 All
            //sParams.Add("@StartDate", StartDate);
            //sParams.Add("@EndDate", EndDate);
            //sParams.Add("@CampaignSorter", CampaignSorter);

            sParams = new SortedList();
            sParams.Add("@UserID", _UserID);
            sParams.Add("@CampaignID", CalCampID);
            sParams.Add("@StartDate", StartDate);
            sParams.Add("@EndDate", EndDate);

            dtCalendar = Classes.cUtilities.LoadDataTable("uspGetEventCalendarDates", sParams, "LARPortal", _UserName, lsRoutineName);
            DataView dvCalendar = new DataView(dtCalendar, "", ddlOrderBy.SelectedValue, DataViewRowState.CurrentRows);

//            dtCalendar = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            gvCalendar.DataSource = dvCalendar;
            gvCalendar.DataBind();
        }

        //protected void ddlEventDateRange_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void ddlCampaignChoice_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        protected void btnRunReport_Click(object sender, EventArgs e)
        {
            //btnExportCSV.Visible = true;
            btnExportExcel.Visible = true;
            FillGrid();
            pnlReportOutput.Visible = true;
        }

        //protected void gvCalendar_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //}

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
            gvCalendar.AllowPaging = false;
            //BindGridDetails(gvCalendar);
            form.Attributes["runat"] = "server";
            form.Controls.Add(gvCalendar);
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
            gvCalendar.AllowPaging = false;
            gvCalendar.DataBind();
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < gvCalendar.Columns.Count; k++)
            {
                //add separator 
                sb.Append(gvCalendar.Columns[k].HeaderText + ',');
            }
            //append new line 
            sb.Append("\r\n");
            for (int i = 0; i < gvCalendar.Rows.Count; i++)
            {
                for (int k = 0; k < gvCalendar.Columns.Count; k++)
                {
                    //add separator 
                    sb.Append(gvCalendar.Rows[i].Cells[k].Text + ',');
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






// Below is the original code for the page.
//        <%--        <asp:Panel ID="pnlParameters" runat="server" Visible="true">--%>
//        <div class="row col-sm-12">
//            <%--                <div class="col-sm-5">
//                    <div class="col-sm-12 form-group">--%>
//            <label for="ddlEventDateRange" class="col-sm-5 control-label">Date Range</label>
//            <asp:DropDownList ID="ddlEventDateRange" runat="server">
//                <asp:ListItem Value="1" Text="All scheduled"></asp:ListItem>
//                <%--                            <asp:ListItem Value="2" Text="Next 6 months"></asp:ListItem>
//                            <asp:ListItem Value="3" Text="Last 3 months"></asp:ListItem>
//                            <asp:ListItem Value="4" Text="Last 6 months"></asp:ListItem>
//                            <asp:ListItem Value="5" Text="Last 12 months"></asp:ListItem>--%>
//                <asp:ListItem Value="6" Text="All historical"></asp:ListItem>
//            </asp:DropDownList>
//            <%--                    </div>
//                    <div class="col-sm-12 form-group">

//                    </div>
//                    <div class="col-sm-12 form-group">--%>
//            <label for="ddlOrderBy" class="col-sm-5 control-label">Sort By</label>
//            <asp:DropDownList ID="ddlOrderBy" runat="server">
//                <asp:ListItem Value="StartDate" Text="Event Date (ascending)"></asp:ListItem>
//                <%--<asp:ListItem Value="2" Text="Event Date (descending)"></asp:ListItem>--%>
//                <asp:ListItem Value="CampaignName, StartDate" Text="Campaign, Event Date"></asp:ListItem>
//            </asp:DropDownList>
//            <%--                    </div>
//                </div>
//                <div class="col-sm-6">
//                    <div class="col-sm-12 form-group">
//                        <label for="ddlCampaignChoice" class="col-sm-5 control-label">Campaigns</label>
//                        <asp:DropDownList ID="ddlCampaignChoice" runat="server" AutoPostBack="true">
//                            <asp:ListItem Value="1" Text="My campaigns"></asp:ListItem>
//                            <asp:ListItem Value="2" Text="Selected campaign"></asp:ListItem>
//                            <%--<asp:ListItem Value="3" Text="Include campaigns in the selected game system"></asp:ListItem>--%>
//            <%--                            <asp:ListItem Value="4" Text="All campaigns"></asp:ListItem>
//                        </asp:DropDownList>
//                    </div>
//                    <div class="col-sm-12 form-group">

//                    </div>
//                </div>--%>

//            <%--                <div class="col-sm-1">
//                    <div class="row">
//                        <div style="padding-right: 20px; padding-top: 2px">--%>
//            <asp:Button ID="btnRunReport" runat="server" CssClass="StandardButton" Width="125px" Text="Run Report" OnClick="btnRunReport_Click" />
//            <%--                        </div>
//                    </div>
//                    <div class="row">
//                        <div style="padding-right: 20px; padding-top: 2px">  --%>
//                            <asp:Button ID="btnExportExcel" runat="server" CssClass="StandardButton" Width="125px" Text="Excel" OnClick="btnExportExcel_Click" Visible="false" />
//<%--                        </div>
//                    </div>--%>
//            <%--                    <div class="row">
//                        <div style="padding-right: 20px; padding-top: 2px">
//                            <asp:Button ID="btnExportCSV" runat="server" CssClass="StandardButton" Width="77px"  Text="CSV" OnClick="btnExportCSV_Click" Visible="false" />
//                        </div>
//                    </div>--%>
//        </div>
//        <%--            </div>
//        </asp:Panel>--%>
