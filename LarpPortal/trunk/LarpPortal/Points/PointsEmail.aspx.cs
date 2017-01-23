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

namespace LarpPortal.Points
{
    public partial class PointsEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CurrentCampaignID = Convert.ToInt32(Session["CampaignID"]);
                // ********** When we send, calling job is going to be PointsEmail which is entry in MDBSMTP table **********
                ddlCampaignLoad(CurrentCampaignID);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "Points";
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
                //ddlAttendanceLoad(hidUserName.Value, intCampaignID);
                //ddlCharacterLoad(hidUserName.Value, intCampaignID);
                //ddlEarnReasonLoad(hidUserName.Value, intCampaignID);
                //ddlEarnTypeLoad(hidUserName.Value, intCampaignID);
                //ddlPlayerLoad(hidUserName.Value, intCampaignID);
                FillGrid(hidUserName.Value, hidCampaignID.Value);
                //ddlCampaignPlayerLoad(hidUserName.Value, intCampaignID);
                //ddlAddCharacterLoad(hidUserName.Value, intCampaignID);
                //DropdownListDefaultColors();
            }
        }

        protected void ddlCampaignLoad(int CurrentCampaignID)
        {
            string stStoredProc = "uspGetCampaignsToSendPointsTo";
            string stCallingMethod = "PointsEmail.aspx.ddlCampaignLoad";
            DataTable dtDestinationCampaigns = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@FromCampaign", CurrentCampaignID);
            dtDestinationCampaigns = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", hidUserName.Value, stCallingMethod);
            ddlCampaign.DataTextField = "CampaignName";
            ddlCampaign.DataValueField = "CampaignID";
            ddlCampaign.DataSource = dtDestinationCampaigns;
            ddlCampaign.DataBind();
            ddlCampaign.Items.Insert(0, new ListItem("Select Campaign", "0"));
            ddlCampaign.SelectedIndex = 0;
        }

        protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Rick - 1/2/2017 - Need to modify this to fit this application
            string CampaignLPType;
            // If current campaign is picked then E3 otherwise E4
            if (ddlCampaign.SelectedValue == hidCampaignID.Value)
            {
                CampaignLPType = "S";
                Session["AddCPStep"] = "E3";
                hidLastAddCPStep.Value = "E3";
                //Load Event ddl
                //ddlSourceEventLoad();
                AddPanelVisibility();
                Session["AddCPStep"] = "F3";
                hidLastAddCPStep.Value = "F3";
                //ddlSelectCharacterOrBankF3Load();
            }
            else
            {
                Session["AddCPStep"] = "E4";
                hidLastAddCPStep.Value = "E4";
                hidInsertDestinationCampaign.Value = ddlCampaign.SelectedValue;
                //Load Event ddl
                //ddlSourceEventLoad();
                AddPanelVisibility();
                //Need to check whether it's a LARP Portal campaign or not before deciding between F4 and F5
                int iTemp = 0;
                Int32.TryParse(hidInsertDestinationCampaign.Value.ToString(), out iTemp);
                if (iTemp != 0)
                {
                    Classes.cCampaignBase cCampaign = new Classes.cCampaignBase(iTemp, hidUserName.Value, 0);
                    CampaignLPType = cCampaign.PortalAccessType;
                }
                else
                {
                    CampaignLPType = "B";
                }
                if (CampaignLPType == "S")
                {
                    Session["AddCPStep"] = "F4";
                    hidLastAddCPStep.Value = "F4";
                }
                else
                {
                    Session["AddCPStep"] = "F5";
                    hidLastAddCPStep.Value = "F5";
                }
            }
            hidInsertDestinationCampaignLPType.Value = CampaignLPType;
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
            dtOpportunities = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            gvPoints.DataSource = dtOpportunities;
            gvPoints.DataBind();
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
                HiddenField hidRole = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidRole");
                HiddenField hidNPCCampaignID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidNPCCampaignID");
                HiddenField hidRegistrationID = (HiddenField)gvPoints.Rows[e.RowIndex].FindControl("hidRegistrationID");
                Label lblEarnDesc = (Label)gvPoints.Rows[e.RowIndex].FindControl("lblEarnDescription");
                int intCmpPlyrID = 0;
                int intCharID = 0;
                int intEvntID = 0;
                int intCPOpp = 0;
                int intOppDefID = 0;
                int intRsnID = 0;
                int intAddID = 0;
                int intRole = 0;
                int intNPCCampaignID = 0;
                int intRegistrationID = 0;
                int intCampaignID = 0;
                int.TryParse(Session["CampaignID"].ToString(), out intCampaignID);
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
                if (int.TryParse(hidRole.Value.ToString(), out iTemp))
                    intRole = iTemp;
                if (int.TryParse(hidNPCCampaignID.Value.ToString(), out iTemp))
                    intNPCCampaignID = iTemp;
                if (int.TryParse(hidRegistrationID.Value.ToString(), out iTemp))
                    intRegistrationID = iTemp;
                string strComments = "";
                if (Session["EditMode"].ToString() == "Edit")
                {
                    GridViewRow row = gvPoints.Rows[index];
                    TextBox txtComments = row.FindControl("tbStaffComments") as TextBox;
                    strComments = txtComments.Text;
                    TextBox txtCP = row.FindControl("txtCPValue") as TextBox;
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
                Classes.cPoints Point = new Classes.cPoints();
                Point.UpdateCPOpportunity(UserID, intCPOpp, intCmpPlyrID, intCharID, intOppDefID, intEvntID,
                    strDesc, strOppNotes, strExURL, intRsnID, intAddID, CP, UserID,
                    DateTime.Now, UserID, strComments, intRole, intNPCCampaignID, intCampaignID);
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
            // Rick - 12/10/2016 - Hide button, show processing label, when done unhide button and rehide processing label

            foreach (GridViewRow gvrow in gvPoints.Rows)
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
                    HiddenField hidCPOpp = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidPointID");
                    HiddenField hidCmpPlyrID = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidCampaignPlayer");
                    HiddenField hidCharID = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidCharacterID");
                    HiddenField hidEvntID = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidEventID");
                    HiddenField hidOppDefID = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidCPOpportunityDefaultID");
                    HiddenField hidRsnID = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidReasonID");
                    HiddenField hidAddID = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidAddedByID");
                    HiddenField hidOppNotes = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidOpportunityNotes");
                    HiddenField hidExURL = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidExampleURL");
                    HiddenField hidRole = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidRole");
                    HiddenField hidNPCCampaignID = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidNPCCampaignID");
                    HiddenField hidRegistrationID = (HiddenField)gvPoints.Rows[gvrow.RowIndex].FindControl("hidRegistrationID");
                    Label lblEarnDesc = (Label)gvPoints.Rows[gvrow.RowIndex].FindControl("lblEarnDescription");
                    int intCmpPlyrID = 0;
                    int intCharID = 0;
                    int intEvntID = 0;
                    int intCPOpp = 0;
                    int intOppDefID = 0;
                    int intRsnID = 0;
                    int intAddID = 0;
                    int intRole = 0;
                    int intNPCCampaignID = 0;
                    int intRegistrationID = 0;
                    int intCampaignID = 0;
                    int.TryParse(Session["CampaignID"].ToString(), out intCampaignID);
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
                    if (int.TryParse(hidRole.Value.ToString(), out iTemp))
                        intRole = iTemp;
                    if (int.TryParse(hidNPCCampaignID.Value.ToString(), out iTemp))
                        intNPCCampaignID = iTemp;
                    if (int.TryParse(hidRegistrationID.Value.ToString(), out iTemp))
                        intRegistrationID = iTemp;
                    string strComments = "";
                    if (Session["EditMode"].ToString() == "Edit")
                    {
                        GridViewRow row = gvPoints.Rows[index];
                        TextBox txtComments = row.FindControl("tbStaffComments") as TextBox;
                        strComments = txtComments.Text;
                        TextBox txtCP = row.FindControl("txtCPValue") as TextBox;
                        if (double.TryParse(txtCP.Text.ToString(), out dblTemp))
                            CP = dblTemp;
                        Session["EditMode"] = "Assign";
                    }
                    else
                    {
                        Label lblCPValue = (Label)gvPoints.Rows[gvrow.RowIndex].FindControl("lblCPValue");
                        Label lblStaffComents = (Label)gvPoints.Rows[gvrow.RowIndex].FindControl("lblStaffComments");
                        if (double.TryParse(lblCPValue.Text, out dblTemp))
                            CP = dblTemp;
                        strComments = lblStaffComents.Text;

                    }
                    Classes.cPoints Point = new Classes.cPoints();
                    Point.UpdateCPOpportunity(UserID, intCPOpp, intCmpPlyrID, intCharID, intOppDefID, intEvntID,
                        strDesc, strOppNotes, strExURL, intRsnID, intAddID, CP, UserID,
                        DateTime.Now, UserID, strComments, intRole, intNPCCampaignID, intCampaignID);
                }
                catch (Exception ex)
                {
                    string l = ex.Message;
                }
            }
            gvPoints.EditIndex = -1;

            // Rick - 12/10/2016 Show processing complete pop-up and restore assign all button and label to default state
            string jsString = "alert('All points applied.');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    "MyApplication",
                    jsString,
                    true);
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

        protected void ResetHiddenValues()
        {
            int UserID = 0;
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out UserID);
            hidInsertCharacterID.Value = "0";
            hidInsertDescription.Value = "";
            hidInsertStatusID.Value = "21";
            hidInsertAddedByID.Value = UserID.ToString();
            hidInsertCPValue.Value = "0";
            hidInsertApprovedByID.Value = UserID.ToString();
            hidInsertReceiptDate.Value = DateTime.Now.ToString();
            hidInsertReceivedByID.Value = UserID.ToString();
            hidInsertCPAssignmentDate.Value = DateTime.Now.ToString();
            hidInsertStaffComments.Value = "";
        }

        protected void AddPanelVisibility()
        {
            // RP - 1/2/2017 - Don't know yet if we'll need something like this for PointsEmail.  I'll let me know.
            string FlowStep = "A";
            if (Session["AddCPStep"] != null)
            {
                FlowStep = Session["AddCPStep"].ToString();
            }
            switch (FlowStep)
            {
                case "A":

                    break;

                default:
                    //pnlAddSourceCampaign.Visible = false;
                    hidInsertCharacterID.Value = "0";
                    hidInsertCampaignCPOpportunityDefaultID.Value = "0";
                    hidInsertEventID.Value = "0";
                    hidInsertCampaignID.Value = "0";
                    hidInsertDescription.Value = "";
                    hidInsertOpportunityNotes.Value = "";
                    hidInsertExampleURL.Value = "";
                    hidInsertReasonID.Value = "0";
                    hidInsertStatusID.Value = "19";
                    hidInsertAddedByID.Value = "0";
                    hidInsertCPValue.Value = "0";
                    hidInsertApprovedByID.Value = "0";
                    hidInsertReceiptDate.Value = "";
                    hidInsertReceivedByID.Value = "0";
                    hidInsertCPAssignmentDate.Value = "Today";
                    hidInsertStaffComments.Value = "";
                    break;
            }
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {

        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {

        }

    }
}