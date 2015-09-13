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
                //string strUserName = "NoUserName";
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
            sParams.Add("@EventLength", 28); // How many characters of Event Name/date to return with ellipsis
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

                    //string t = dRow["CardDisplayIncant"].ToString();
                    //if (dRow["DisplayDesc"].ToString().ToUpper().StartsWith("D"))
                    //{
                    //    CheckBox cbDisplayDesc = (CheckBox)e.Row.FindControl("cbDisplayDesc");
                    //    if (cbDisplayDesc != null)
                    //        cbDisplayDesc.Checked = true;
                    //}
                    //if (dRow["DisplayIncant"].ToString().ToUpper().StartsWith("D"))
                    //{
                    //    CheckBox cbDisplayIncant = (CheckBox)e.Row.FindControl("cbDisplayIncant");
                    //    if (cbDisplayIncant != null)
                    //        cbDisplayIncant.Checked = true;
                    //}
                }
            }
        }

        protected void gvPoints_RowEditing(object sender, GridViewEditEventArgs e)
        {
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
                    DateTime dTemp;
                    DateTime RecDate = DateTime.Now;
                    if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                        UserID = iTemp;
                    HiddenField hidCPOpp = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidPointID");
                    HiddenField hidCmpPlyrID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidCampaignPlayer");
                    HiddenField hidCharID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidCharacterID");
                    HiddenField hidEvntID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidEventID");
                    int intCmpPlyrID = 0;
                    int intCharID = 0;
                    int intEvntID = 0;
                    if (int.TryParse(hidCmpPlyrID.Value.ToString(), out iTemp))
                        intCmpPlyrID = iTemp;
                    if (int.TryParse(hidCharID.Value.ToString(), out iTemp))
                        intCharID = iTemp;
                    if (int.TryParse(hidEvntID.Value.ToString(), out iTemp))
                        intEvntID = iTemp;
                    GridViewRow row = gvPoints.Rows[index];
                    // Rick - RIGHT HERE - vvvvvvvvv THESE VALUES ARE WHAT'S ON THE PAGE vvvvvvvvv
                    TextBox txtComments =       row.FindControl("tbStaffComments") as TextBox;
                    TextBox txtCP =             row.FindControl("txtCPValue") as TextBox;
                    if (double.TryParse(txtCP.Text.ToString(), out dblTemp))
                        CP = dblTemp;
                    TextBox txtRcptDate =       row.FindControl("txtReceiptDate") as TextBox;
                    if (DateTime.TryParse(txtRcptDate.Text.ToString(), out dTemp))
                        RecDate = dTemp;
                    Classes.cPoints Point = new Classes.cPoints();
                    Point.AssignPELPoints(UserID, intCmpPlyrID, intCharID, 10, intEvntID, "Madrigal May 2015", 14, 33, CP, RecDate );  
                    //e.NewValues[""]  Finish this out with field names and pass to update routine
            //        CheckBox cbDisplayDesc = (CheckBox)gvSkills.Rows[e.RowIndex].FindControl("cbDisplayDesc");
            //        CheckBox cbDisplayIncant = (CheckBox)gvSkills.Rows[e.RowIndex].FindControl("cbDisplayIncant");
            //        TextBox tbPlayDesc = (TextBox)gvSkills.Rows[e.RowIndex].FindControl("tbPlayDesc");
            //        TextBox tbPlayIncant = (TextBox)gvSkills.Rows[e.RowIndex].FindControl("tbPlayIncant");
            //        HiddenField hidSkillID = (HiddenField)gvSkills.Rows[e.RowIndex].FindControl("hidSkillID");

            //        if (Session["SkillList"] != null)
            //        {
            //            DataTable dtSkills = Session["SkillList"] as DataTable;
            //            DataView dvRow = new DataView(dtSkills, "CharacterSkillsStandardID = " + hidSkillID.Value, "", DataViewRowState.CurrentRows);
            //            foreach (DataRowView dRow in dvRow)
            //            {
            //                if (cbDisplayDesc.Checked)
            //                    dRow["CardDisplayDescription"] = true;
            //                else
            //                    dRow["CardDisplayDescription"] = false;
            //                if (cbDisplayIncant.Checked)
            //                    dRow["CardDisplayIncant"] = true;
            //                else
            //                    dRow["CardDisplayIncant"] = false;
            //                dRow["PlayerDescription"] = tbPlayDesc.Text;
            //                dRow["PlayerIncant"] = tbPlayIncant.Text;
            //            }
            //            Session["SkillList"] = dtSkills;
            //        }
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
            double dblCPValue = 0;
            double dblTemp = 0;
            string strStaffComments = "";

            foreach (DictionaryEntry entry in e.NewValues)
            {
                e.NewValues[entry.Key] = Server.HtmlEncode(entry.Value.ToString());
            }
            //HiddenField hidCPValue = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("CPValue");
            strStaffComments = "stop";
        }

        protected void btnAssignAll_Click(object sender, EventArgs e)
        {
            gvPoints.EditIndex = -1;
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }

        protected void gvPoints_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            gvPoints.EditIndex = -1;
            FillGrid(hidUserName.Value, hidCampaignID.Value);
        }


    }
}