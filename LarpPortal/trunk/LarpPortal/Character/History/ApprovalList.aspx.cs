using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character.History
{
    public partial class ApprovalList : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["UpdateHistoryMessage"] != null)
            {
                string jsString = Session["UpdateHistoryMessage"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", jsString, true);
                Session.Remove("UpdateHistoryMessage");
            }
            ViewState["PELsDisplayed"] = BindData();
        }

        protected DataTable BindData()
        {
            SortedList sParams = new SortedList();
            DataTable dtCharHistory = new DataTable();

            int iSessionID = 0;
            int.TryParse(Session["CampaignID"].ToString(), out iSessionID);
            sParams.Add("@CampaignID", iSessionID);

            dtCharHistory = Classes.cUtilities.LoadDataTable("uspGetCampaignCharacterHistory", sParams, "LARPortal", Session["UserName"].ToString(), "PELApprovalList.Page_PreRender");

            string sSelectedChar = "";

            string sRowFilter = "";

            if (ddlCharacterName.SelectedIndex > 0)
            {
                if (ddlCharacterName.SelectedValue.Length > 0)
                {
                    sRowFilter += "(CharacterAKA = '" + ddlCharacterName.SelectedValue.Replace("'", "''") + "')";
                    sSelectedChar = ddlCharacterName.SelectedValue;
                }
                ddlStatus.SelectedIndex = 0;
            }
            else
            {
                switch (ddlStatus.SelectedValue)
                {
                    case "A":
                        sRowFilter = "(DateHistoryApproved is not null)";
                        break;

                    case "S":
                        sRowFilter = "(DateHistoryApproved is null) and (DateHistorySubmitted is not null)";
                        break;

                    default:
                        sRowFilter = "(DateHistorySubmitted is not null)";
                        break;
                }
            }


            if (dtCharHistory.Columns["HistoryStatus"] == null)
                dtCharHistory.Columns.Add("HistoryStatus", typeof(string));

            if (dtCharHistory.Columns["ShortHistory"] == null)
                dtCharHistory.Columns.Add("ShortHistory", typeof(string));

            foreach (DataRow dRow in dtCharHistory.Rows)
            {
                if (dRow["CharacterHistory"].ToString().Length > 100)
                    dRow["ShortHistory"] = dRow["CharacterHistory"].ToString().Substring(0, 95) + "...";
                else
                    dRow["ShortHistory"] = dRow["CharacterHistory"].ToString();
                if (dRow["DateHistoryApproved"] == DBNull.Value)
                    dRow["HistoryStatus"] = "Submitted";
                else
                    dRow["HistoryStatus"] = "Approved";
            }

            DataView dvPELs = new DataView(dtCharHistory, sRowFilter, "CharacterAKA", DataViewRowState.CurrentRows);
            gvHistoryList.DataSource = dvPELs;
            gvHistoryList.DataBind();

            if (!IsPostBack)
            {
                ddlCharacterName.DataSource = dvPELs;
                ddlCharacterName.DataTextField = "CharacterAKA";
                ddlCharacterName.DataValueField = "CharacterAKA";
                ddlCharacterName.DataBind();

                if (dvPELs.Count > 1)
                {
                    ListItem liNoFilter = new ListItem();
                    liNoFilter.Text = "No Filter";
                    liNoFilter.Value = "";
                    ddlCharacterName.Items.Insert(0, liNoFilter);
                }
            }
            return dtCharHistory;
        }

        protected void gvHistoryList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sCharacterID = e.CommandArgument.ToString();
            Response.Redirect("Approve.aspx?CharacterID=" + sCharacterID, false);
        }

        protected void btnApproveAll_Click(object sender, EventArgs e)
        {
            //DataTable dtPELs = ViewState["PELsDisplayed"] as DataTable;

            //DataView dvPELs = new DataView(dtPELs, "DateApproved is null", "", DataViewRowState.CurrentRows);
            //foreach (DataRowView dRow in dvPELs)
            //{
            //    int iRegistrationID;
            //    if (int.TryParse(dRow["RegistrationID"].ToString(), out iRegistrationID))
            //    {
            //        int iPELID;

            //        int.TryParse(dRow["PELID"].ToString(), out iPELID);

            //        SortedList sParams = new SortedList();
            //        sParams.Add("@UserID", Session["UserID"].ToString());
            //        sParams.Add("@PELID", iPELID);

            //        double dCPAwarded;
            //        if (double.TryParse(dRow["CPValue"].ToString(), out dCPAwarded))
            //            sParams.Add("@CPAwarded", dCPAwarded);
            //        sParams.Add("@DateApproved", DateTime.Now);

            //        Classes.cUtilities.PerformNonQuery("uspInsUpdCMPELs", sParams, "LARPortal", Session["UserName"].ToString());

            //        Classes.cPoints Points = new Classes.cPoints();
            //        int UserID = 0;
            //        int CampaignPlayerID = 0;
            //        int CharacterID = 0;
            //        int CampaignCPOpportunityDefaultID = 0;
            //        int EventID = 0;
            //        int ReasonID = 0;
            //        int CampaignID = 0;
            //        double CPAwarded = 0.0;
            //        DateTime dtDateSubmitted = DateTime.Now;

            //        int.TryParse(Session["UserID"].ToString(), out UserID);
            //        int.TryParse(dRow["CampaignPlayerID"].ToString(), out CampaignPlayerID);
            //        int.TryParse(dRow["CharacterID"].ToString(), out CharacterID);
            //        int.TryParse(dRow["CampaignCPOpportunityDefaultID"].ToString(), out CampaignCPOpportunityDefaultID);
            //        int.TryParse(dRow["ReasonID"].ToString(), out ReasonID);
            //        int.TryParse(dRow["CampaignID"].ToString(), out CampaignID);
            //        int.TryParse(dRow["EventID"].ToString(), out EventID);
            //        double.TryParse(dRow["CPValue"].ToString(), out CPAwarded);
            //        DateTime.TryParse(dRow["DateSubmitted"].ToString(), out dtDateSubmitted);

            //        Points.AssignPELPoints(UserID, CampaignPlayerID, CharacterID, CampaignCPOpportunityDefaultID, EventID, dRow["EventName"].ToString(), ReasonID, CampaignID, CPAwarded, dtDateSubmitted);
            //    }
            //}
        }
    }
}