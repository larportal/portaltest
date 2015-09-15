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
                Session["EditMode"] = "Assign";
                int iTemp = 0;
                int intCampaignID = 0;
                if (Session["UserName"] != null)
                    hidUserName.Value = Session["UserName"].ToString();
                if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                { 
                    hidCampaignID.Value = iTemp.ToString();
                    intCampaignID = iTemp;
                }
                ddlAttendanceLoad(hidUserName.Value, intCampaignID);
                ddlCharacterLoad(hidUserName.Value, intCampaignID);
                ddlEarnReasonLoad(hidUserName.Value, intCampaignID);
                ddlEarnTypeLoad(hidUserName.Value, intCampaignID);
                ddlPlayerLoad(hidUserName.Value, intCampaignID);
                ddlCampaignPlayerLoad(hidUserName.Value, intCampaignID);
                FillGrid(hidUserName.Value, hidCampaignID.Value);
            }
        }

        private void FillGrid(string strUserName, string strCampaignID)
        {
            int iTemp = 0;
            int intCampaignID = 0;
            if (int.TryParse(strCampaignID, out iTemp))
                intCampaignID = iTemp;
            string stStoredProc = "uspGetCampaignPointOpportunities";
            string stCallingMethod = "PointsAssign.aspx.FillGrid";
            DataTable dtOpportunities = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@CampaignID", intCampaignID);
            sParams.Add("@StatusID", 19);   // 19 = Claimed
            sParams.Add("@EventID", ddlAttendance.SelectedValue);
            sParams.Add("@CampaignCPOpportunityDefaultID", ddlEarnReason.SelectedValue);
            sParams.Add("@UserID", ddlPlayer.SelectedValue);
            sParams.Add("@CharacterID", ddlCharacters.SelectedValue);
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
            sParams.Add("@EventLength", 35); // How many characters of Event Name/date to return with ellipsis
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
            sParams.Add("@CharacterDisplayNameLength", 25); // How much of the character name do you want to return
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

        protected void ddlAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void ddlEarnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void ddlEarnReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void ddlPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void ddlCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void gvPoints_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPoints.EditIndex = -1;
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void gvPoints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dRow = e.Row.DataItem as DataRowView;
                }
            }
        }

        protected void gvPoints_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["EditMode"] = "Edit";
            gvPoints.EditIndex = e.NewEditIndex;
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void gvPoints_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
                try
                {
                    int index = gvPoints.EditIndex;
                    int iTemp;
                    int UserID = 0;
                    double dblTemp = 0;
                    double CP = 0;
                    if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                        UserID = iTemp;
                    HiddenField hidCPOpp = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidPointID");
                    HiddenField hidCmpPlyrID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidCampaignPlayer");
                    HiddenField hidCharID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidCharacterID");
                    HiddenField hidEvntID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidEventID");
                    HiddenField hidOppDefID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidCPOpportunityDefaultID");
                    HiddenField hidRsnID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidReasonID");
                    HiddenField hidAddID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidAddedByID");
                    HiddenField hidOppNotes = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidOpportunityNotes");
                    HiddenField hidExURL = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidExampleURL");
                    Label lblEarnDesc = (Label)gvPoints.Rows[e.RowIndex].FindControl("lblEarnDescription");
                    int intCmpPlyrID = 0;
                    int intCharID = 0;
                    int intEvntID = 0;
                    int intCPOpp = 0;
                    int intOppDefID = 0;
                    int intRsnID = 0;
                    int intAddID = 0;
                    string strOppNotes = hidOppNotes.Value.ToString();
                    string strExURL = hidExURL.Value.ToString();
                    string strDesc = lblEarnDesc.Text;
                    if (int.TryParse(hidCmpPlyrID.Value.ToString(), out iTemp))
                        intCmpPlyrID = iTemp;
                    if (int.TryParse(hidCharID.Value.ToString(), out iTemp))
                        intCharID = iTemp;
                    if (int.TryParse(hidEvntID.Value.ToString(), out iTemp))
                        intEvntID = iTemp;
                    if (int.TryParse(hidCPOpp.Value.ToString(), out iTemp))
                        intCPOpp = iTemp;
                    if (int.TryParse(hidOppDefID.Value.ToString(), out iTemp))
                        intOppDefID = iTemp;
                    if (int.TryParse(hidRsnID.Value.ToString(), out iTemp))
                        intRsnID = iTemp;
                    if (int.TryParse(hidAddID.Value.ToString(), out iTemp))
                        intAddID = iTemp;
                    string strComments = "";
                    if (Session["EditMode"].ToString() == "Edit")
                    {
                        GridViewRow row = gvPoints.Rows[index];
                        TextBox txtComments =       row.FindControl("tbStaffComments") as TextBox;
                        strComments = txtComments.Text;
                        TextBox txtCP =             row.FindControl("txtCPValue") as TextBox;     
                        if (double.TryParse(txtCP.Text.ToString(), out dblTemp))
                            CP = dblTemp;
                        Session["EditMode"] = "Assign";
                    }
                    else
                    {
                        Label lblCPValue = (Label)gvPoints.Rows[e.RowIndex].FindControl("lblCPValue");
                        Label lblStaffComents = (Label)gvPoints.Rows[e.RowIndex].FindControl("lblStaffComments");
                        if (double.TryParse(lblCPValue.Text, out dblTemp))
                            CP = dblTemp;
                        strComments = lblStaffComents.Text;

                    }
                    //TextBox txtRcptDate =       row.FindControl("txtReceiptDate") as TextBox;
                    //if (DateTime.TryParse(txtRcptDate.Text.ToString(), out dTemp))
                    //    RecDate = dTemp;
                    Classes.cPoints Point = new Classes.cPoints();
                    Point.UpdateCPOpportunity(UserID, intCPOpp, intCmpPlyrID, intCharID, intOppDefID, intEvntID,
                        strDesc, strOppNotes, strExURL, intRsnID, intAddID, CP, UserID, 
                        DateTime.Now, UserID, strComments);
                    //Point.AssignPELPoints(UserID, intCmpPlyrID, intCharID, 10, intEvntID, "Madrigal May 2015", 14, 33, CP, RecDate );
                }
                catch (Exception ex)
                {
                    string l = ex.Message;
                }
                gvPoints.EditIndex = -1;
                FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void gvPoints_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            foreach (DictionaryEntry entry in e.NewValues)
            {
                e.NewValues[entry.Key] = Server.HtmlEncode(entry.Value.ToString());
            }
        }

        protected void btnAssignAll_Click(object sender, EventArgs e)
        {
            gvPoints.EditIndex = -1;
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void gvPoints_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            HiddenField hidCPOpp = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidPointID");
            int iTemp = 0;
            int intCPOpp = 0;
            int UserID = 0;
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(hidCPOpp.Value.ToString(), out iTemp))
                intCPOpp = iTemp;
            Classes.cPoints Points = new Classes.cPoints();
            Points.DeleteCPOpportunity(UserID, intCPOpp);
            gvPoints.EditIndex = -1;
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void ddlCampaignPlayerLoad(string strUserName, int intCampaignID)
        {
            ddlCampaignPlayer.Items.Clear();
            string stStoredProc = "uspGetCampaignPlayers";
            string stCallingMethod = "PointsAssign.aspx.ddlCampaignPlayerLoad";
            DataTable dtPlayers = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@CampaignID", intCampaignID);
            dtPlayers = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            ddlPlayer.DataTextField = "PlayerName";
            ddlPlayer.DataValueField = "CampaignPlayerID";
            ddlPlayer.DataSource = dtPlayers;
            ddlPlayer.DataBind();
            ddlPlayer.Items.Insert(0, new ListItem("Select Player", "0"));
            ddlPlayer.SelectedIndex = 0;
        }

        protected void ddlCampaignPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}