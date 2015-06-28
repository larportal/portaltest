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

namespace LarpPortal
{
    public partial class MemberPointsView : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "PointsView";
            string uName = "";
            int uID = 0;
            if (Session["Username"] != null)
                uName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                uID = (Session["UserID"].ToString().ToInt32());
            //gvCP.AllowPaging = true;
            //gvCP.PageSize = 15;
            //gvCP.AllowSorting = true;
            //ViewState["SortExpression"] = "PlayerCPAuditID ASC";
            //BindGridView();
            BuildCPAuditTable(uID);
        }


        private void BuildCPAuditTable(int UserID)
        {
            int CampaignID = 0;
            int CharacterID = 0;
            int CPAuditRowCounter = 1;
            DateTime dtTemp;
            double dTemp = 0;
            string TransactionDate = "";
            string ReasonDescription = "";
            string AdditionalNotes = "";
            double CPAmount = 0;
            string Status = "Spent";
            if (Status != "Spent")
                Status = "Spent";
            string CPApprovedDate;
            string RecvFromCampaign = "";
            string OwningPlayer = "";
            string ReceivingCampaign = "";
            string Character = "";
            string ReceivingPlayer = "";
            string StatusName = "";
            string CampaignDDL = "";
            if(Session["CampaignName"] != null)
                CampaignDDL = Session["CampaignName"].ToString();
            string dq = "\"";
            string TableCode = "<table id=" + dq + "events-table" + dq + "class=" + dq + "table table-striped table-bordered table-hover table-condensed" + dq;
            TableCode = TableCode + " data-toggle=" + dq + "table" + dq + " data-height=" + dq + "250" + dq + " data-sort-name=" + dq;
            TableCode = TableCode + " date-earned" + dq + " data-sort-order=" + dq + " desc" + dq + " data-click-to-select=" + dq + "true" + dq;
            TableCode = TableCode + " data-select-item-name=" + dq + "total-point" + dq + "><tr><td>";
            Classes.cTransactions CPAudit = new Classes.cTransactions();
            DataTable dtCPAudit = new DataTable();
            dtCPAudit = CPAudit.GetCPAuditList(UserID, CampaignID, CharacterID);
            if (CPAudit.CPAuditCount == 0)
            {
                // Build table with no records
                lblCPAuditTableCode.Text = TableCode + "You have no CP transaction records</td></tr></table>";
            }
            else
            {
                foreach (DataRow dRow in dtCPAudit.Rows)
                {
                    if(CPAuditRowCounter == 1)
                    {
                        // Build the header row
                        TableCode = TableCode + "Earn Date</td><td>Type</td><td>Description</td><td>Points</td><td>Status</td><td>Spend Date</td>";
                        TableCode = TableCode + "<td>Earned At</td><td>Earned By</td><td>Spent At</td><td>Spent On</td><td>Transfer To</td><td>Approved</td></tr>";
                    }
                    // Build the detail row if the 'To' or 'From' campaign is the drop down list campaign
                    RecvFromCampaign = dRow["RecvFromCampaign"].ToString();
                    ReceivingCampaign = dRow["ReceivingCampaign"].ToString();
                    if (CampaignDDL == RecvFromCampaign || CampaignDDL == ReceivingCampaign)
                    {
                        if (DateTime.TryParse(dRow["TransactionDate"].ToString(), out dtTemp))
                            TransactionDate = string.Format("{0:MM/d/yyyy}", dtTemp);
                        else
                            TransactionDate = "";
                        TableCode = TableCode + "<td>" + TransactionDate + "</td>";
                        ReasonDescription = dRow["ReasonDescription"].ToString();
                        TableCode = TableCode + "<td>" + ReasonDescription +"</td>";
                        AdditionalNotes = dRow["AdditionalNotes"].ToString();
                        TableCode = TableCode + "<td>" + AdditionalNotes +"</td>";
                        if (double.TryParse(dRow["CPAmount"].ToString(), out dTemp))
                            CPAmount = dTemp;
                        else
                            CPAmount = 0;
                        TableCode = TableCode + "<td>" + CPAmount +"</td>";
                        StatusName = dRow["StatusName"].ToString();
                        TableCode = TableCode + "<td>" + StatusName +"</td>";
                        if (DateTime.TryParse(dRow["CPApprovedDate"].ToString(), out dtTemp))
                            CPApprovedDate = string.Format("{0:MM/d/yyyy}", dtTemp);
                        else
                            CPApprovedDate = "";
                        TableCode = TableCode + "<td>" + CPApprovedDate +"</td>";
                        TableCode = TableCode + "<td>" + RecvFromCampaign +"</td>";
                        OwningPlayer = dRow["OwningPlayer"].ToString();
                        TableCode = TableCode + "<td>" + OwningPlayer +"</td>";
                        TableCode = TableCode + "<td>" + ReceivingCampaign +"</td>";
                        Character = dRow["Character"].ToString();
                        TableCode = TableCode + "<td>" + Character +"</td>";
                        if (OwningPlayer == ReceivingPlayer)
                            ReceivingPlayer = "";
                        ReceivingPlayer = dRow["ReceivingPlayer"].ToString();
                        TableCode = TableCode + "<td>" + ReceivingPlayer +"</td>";
                        TableCode = TableCode + "<td>" + CPApprovedDate + "</td></tr>"; 
                    }
                    CPAuditRowCounter++;
                }
                // Build the table close
                lblCPAuditTableCode.Text = TableCode + "</table>";
            }
        }

        //private void BindGridView()
        //{
        //    Classes.cTransactions CPAudit = new Classes.cTransactions();
        //    int UserID = 2;
        //    int CampaignID = 0;
        //    int CharacterID = 0;
        //    DataSet dsCPAudit = new DataSet();
        //    dsCPAudit.Tables.Add(CPAudit.GetCPAuditList(UserID, CampaignID, CharacterID));
        //    DataView dvCPAudit = dsCPAudit.Tables["CPAudit"].DefaultView;
        //    gvCP.DataSource = dvCPAudit;
        //    gvCP.DataBind();
        //}

        //protected void gvCP_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void gvCP_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //}

        //protected void gvCP_Sorting(object sender, GridViewSortEventArgs e)
        //{

        //}
    }
}