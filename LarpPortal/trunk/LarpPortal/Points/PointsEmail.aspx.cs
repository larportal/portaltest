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
                //pnlPreviewEmail.Visible = false;
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
                FillGrid(hidUserName.Value, hidCampaignID.Value);
            }
        }

        protected void ddlCampaignLoad(int CurrentCampaignID)
        {
            string stStoredProc = "uspGetCampaignsToSendPointsTo";
            string stCallingMethod = "PointsEmail.aspx.ddlCampaignLoad";
            DataTable dtDestinationCampaigns = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@FromCampaignID", CurrentCampaignID);
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
            lblTest.Text = "ddlCampaign_SelectedIndexChanged";
            pnlAwaitingSending.Visible = false;
            pnlPreviewEmail.Visible = true;
            btnPreview.Visible = true;
            btnPreview2.Visible = true;
            string CampaignLPType;
            if (ddlCampaign.SelectedValue == hidCampaignID.Value)
            {
                CampaignLPType = "S";
            }
            else
            {
                int iTemp = 0;
                //Int32.TryParse(hidInsertDestinationCampaign.Value.ToString(), out iTemp);
                if (iTemp != 0)
                {
                    Classes.cCampaignBase cCampaign = new Classes.cCampaignBase(iTemp, hidUserName.Value, 0);
                    CampaignLPType = cCampaign.PortalAccessType;
                }
                else
                {
                    CampaignLPType = "B";
                }
            }
            //hidInsertDestinationCampaignLPType.Value = CampaignLPType;
        }

        private void FillGrid(string strUserName, string strCampaignID)
        {
            int iTemp = 0;
            int intFromCampaignID = 0;
            int intToCampaignID = 0;
            if (int.TryParse(strCampaignID, out iTemp))
                intFromCampaignID = iTemp;
            if (int.TryParse(ddlCampaign.SelectedValue.ToString(), out iTemp))
                intToCampaignID = iTemp;
            string stStoredProc = "uspGetCampaignPointOpportunitiesToSendOut";
            string stCallingMethod = "PointsEmail.aspx.FillGrid";
            DataTable dtOpportunities = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@FromCampaignID", intFromCampaignID);
            sParams.Add("@ToCampaignID", intToCampaignID);
            dtOpportunities = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            gvPoints.DataSource = dtOpportunities;
            gvPoints.DataBind();
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


        protected void btnPreview_Click(object sender, EventArgs e)
        {
            pnlAwaitingSending.Visible = false;
            pnlPreviewEmail.Visible = true;
            lblTest.Text = "btnPreview_Click";

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
                    break;
            }
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            // Rick 2/5/2017 - Delete function
        }

        protected void btnPreview2_Click(object sender, EventArgs e)
        {
            pnlAwaitingSending.Visible = false;
            pnlPreviewEmail.Visible = true;
            lblTest.Text = "btnPreview2_Click";
        }

    }
}