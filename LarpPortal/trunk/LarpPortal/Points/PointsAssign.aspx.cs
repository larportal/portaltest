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

namespace LarpPortal.Points
{
    public partial class PointsAssign : System.Web.UI.Page
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
                string strUserName = "NoUserName";
                int iTemp = 0;
                int intCampaignID = 0;
                if (Session["UserName"] != null)
                    strUserName = Session["UserName"].ToString();
                if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                    intCampaignID = iTemp;
                ddlAttendanceLoad(strUserName, intCampaignID);
                ddlCharacterLoad(strUserName, intCampaignID);
                ddlEarnReasonLoad(strUserName, intCampaignID);
                ddlEarnTypeLoad(strUserName, intCampaignID);
                ddlPlayerLoad(strUserName, intCampaignID);
                FillGrid(strUserName, intCampaignID);
            }

        }

        private void FillGrid(string strUserName, int intCampaignID)
        {
            string stStoredProc = "uspGetCampaignPointOpportunities";
            string stCallingMethod = "PointsAssign.aspx.FillGrid";
            DataTable dtOpportunities = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@CampaignID", intCampaignID);
            sParams.Add("@StatusID", 19);   // 19 = Claimed
            dtOpportunities = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            gvPoints.DataSource = dtOpportunities;
            gvPoints.DataBind();
        }

        private void ddlAttendanceLoad(string strUserName, int intCampaignID)
        {
            ddlAttendance.Items.Clear();
            string stStoredProc = "uspGetCampaignEvents";
            string stCallingMethod = "PointsAssign.aspx.ddlAttendanceLoad";
            DataTable dtAttendance = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@CampaignID", intCampaignID);
            sParams.Add("@StatusID", 51); // 51 = Completed
            dtAttendance = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            ddlAttendance.DataTextField = "EventNameDate";
            ddlAttendance.DataValueField = "EventID";
            ddlAttendance.DataSource = dtAttendance;
            ddlAttendance.DataBind();
            ddlAttendance.Items.Insert(0,new ListItem("Select Event - Date", "0"));
            ddlAttendance.SelectedIndex = 0;
        }

        private void ddlCharacterLoad(string strUserName, int intCampaignID)
        {        
            ddlCharacters.Items.Clear();
            string stStoredProc = "uspGetCampaignCharacters";
            string stCallingMethod = "PointsAssign.aspx.ddlCharacterLoad";
            DataTable dtCharacters = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@CampaignID", intCampaignID);
            dtCharacters = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            ddlCharacters.DataTextField = "CharacterName";
            ddlCharacters.DataValueField = "CharacterID";
            ddlCharacters.DataSource = dtCharacters;
            ddlCharacters.DataBind();
            ddlCharacters.Items.Insert(0, new ListItem("Select Character", "0"));
            ddlCharacters.SelectedIndex = 0;
        }

        private void ddlEarnReasonLoad(string strUserName, int intCampaignID)
        {
            ddlEarnReason.Items.Clear();
            string stStoredProc = "uspGetCampaignCPOpportunities";
            string stCallingMethod = "PointsAssign.aspx.ddlEarnReasonLoad";
            DataTable dtEarnReasons = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@CampaignID", intCampaignID);
            dtEarnReasons = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            ddlEarnReason.DataTextField = "Description";
            ddlEarnReason.DataValueField = "CampaignCPOpportunityDefaultID";
            ddlEarnReason.DataSource = dtEarnReasons;
            ddlEarnReason.DataBind();
            ddlEarnReason.Items.Insert(0, new ListItem("Select Earn Reason", "0"));
            ddlEarnReason.SelectedIndex = 0;
        }

        private void ddlEarnTypeLoad(string strUserName, int intCampaignID)
        {
            ddlEarnType.Items.Clear();
            string stStoredProc = "uspGetCPOpportunityTypes";
            string stCallingMethod = "PointsAssign.aspx.ddlEarnTypeLoad";
            DataTable dtEarnTypes = new DataTable();
            SortedList sParams = new SortedList();
            dtEarnTypes = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            ddlEarnType.DataTextField = "OpportunityTypeName";
            ddlEarnType.DataValueField = "CampaignCPOpportunityTypeID";
            ddlEarnType.DataSource = dtEarnTypes;
            ddlEarnType.DataBind();
            ddlEarnType.Items.Insert(0, new ListItem("Select Earn Type", "0"));
            ddlEarnType.SelectedIndex = 0;
        }

        private void ddlPlayerLoad(string strUserName, int intCampaignID)
        {
            ddlPlayer.Items.Clear();
            string stStoredProc = "uspGetCampaignPlayers";
            string stCallingMethod = "PointsAssign.aspx.ddlPlayerLoad";
            DataTable dtPlayers = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@CampaignID", intCampaignID);
            dtPlayers = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            ddlPlayer.DataTextField = "PlayerName";
            ddlPlayer.DataValueField = "UserID";
            ddlPlayer.DataSource = dtPlayers;
            ddlPlayer.DataBind();
            ddlPlayer.Items.Insert(0, new ListItem("Select Player", "0"));
            ddlPlayer.SelectedIndex = 0;
        }

        protected void gvPoints_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvPoints_RowUpdating(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvPoints_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvPoints_RowEditing(object sender, GridViewRowEventArgs e)
        {

        }

    }
}