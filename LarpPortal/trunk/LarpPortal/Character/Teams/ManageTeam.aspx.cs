﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Character.Teams
{
    public partial class ManageTeam : System.Web.UI.Page
    {
        bool _Reload = false;

        string _UserName = "";
        int _iUserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _iUserID);
            if (_iUserID == 0)
                _iUserID = -1;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["SelectedCharacter"] == null)
                Response.Redirect("../CharInfo.aspx", true);

            if ((ViewState["Reload"] == null) ||
                (_Reload))
            //            if ((!IsPostBack) || (_Reload))
            {
                ViewState["Reload"] = "Y";
                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                DataTable dtCharacters = cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", lsRoutineName + ".GetCharacterIDsByUserID");
                ddlCharacterSelector.DataTextField = "CharacterAKA";
                ddlCharacterSelector.DataValueField = "CharacterID";
                ddlCharacterSelector.DataSource = dtCharacters;
                ddlCharacterSelector.DataBind();

                int iCharID;
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    ddlCharacterSelector.ClearSelection();
                    foreach (ListItem li in ddlCharacterSelector.Items)
                    {
                        if (li.Value == iCharID.ToString())
                        {
                            ddlCharacterSelector.ClearSelection();
                            li.Selected = true;
                        }
                    }

                    slParameters = new SortedList();
                    slParameters.Add("@CharacterID", iCharID.ToString());
                    DataTable dtTeamList = cUtilities.LoadDataTable("uspGetTeamMembers", slParameters, "LARPortal", _UserName, lsRoutineName + ".GetTeams");

                    DataTable dtTeams = new DataView(dtTeamList, "Approval", "TeamName", DataViewRowState.CurrentRows).ToTable();
                    ddlTeams.DataSource = dtTeams;
                    ddlTeams.DataTextField = "TeamName";
                    ddlTeams.DataValueField = "TeamID";
                    ddlTeams.DataBind();

                    if (dtTeams.Rows.Count > 0)
                    {
                        ddlTeams.ClearSelection();
                        ddlTeams.Items[0].Selected = true;
                        hidTeamID.Value = ddlTeams.SelectedValue;
                        if (dtTeams.Rows.Count == 1)
                        {
                            lblTeamName.Text = ddlTeams.SelectedItem.Text;
                            lblTeamName.Visible = true;
                            ddlTeams.Visible = false;
                        }
                    }
                    else
                    {
                        Response.Redirect("/Character/Teams/JoinTeam.aspx", true);
                    }

                    if (ddlTeams.SelectedValue != null)
                    {
                        slParameters = new SortedList();
                        slParameters.Add("@TeamID", ddlTeams.SelectedValue);
                        DataTable dtTeamMembers = cUtilities.LoadDataTable("uspGetTeamMembers", slParameters, "LARPortal", _UserName, lsRoutineName + ".GetMembers");
                        Session["TeamMembers"] = dtTeamMembers;

                        BindData();
                    }
                }
            }
        }

        private void SendSubmittedEmail(string sHistory, Classes.cCharacterHistory cHist)
        {
            try
            {
                if (hidNotificationEMail.Value.Length > 0)
                {
                    Classes.cUser User = new Classes.cUser(Session["UserName"].ToString(), "PasswordNotNeeded");
                    string sSubject = cHist.CampaignName + " character history from " + cHist.PlayerName + " - " + cHist.CharacterAKA;

                    string sBody = (string.IsNullOrEmpty(User.NickName) ? User.FirstName : User.NickName) +
                        " " + User.LastName + " has submitted a character history for " + cHist.CharacterAKA + ".<br><br>" +
                         sHistory;
                    Classes.cEmailMessageService cEMS = new Classes.cEmailMessageService();
                    cEMS.SendMail(sSubject, sBody, cHist.NotificationEMail, "", "", "CharacterHistory", Session["Username"].ToString());
                }
            }
            catch (Exception ex)
            {
                // Write the exception to error log and then throw it again...
                Classes.ErrorAtServer lobjError = new Classes.ErrorAtServer();
                lobjError.ProcessError(ex, "CharacterEdit.aspx.SendSubmittedEmail", "", Session.SessionID);
            }
        }

        protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCharacterSelector.SelectedValue == "-1")
                Response.Redirect("../CharAdd.aspx");

            if (Session["SelectedCharacter"].ToString() != ddlCharacterSelector.SelectedValue)
            {
                Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                _Reload = true;
            }
        }

        protected void btnCloseMessage_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeMessage();", true);
        }

        protected void gvAvailable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string t = e.CommandArgument.ToString();
            string j = e.CommandName;

            DataTable dtTeamMembers = Session["TeamMembers"] as DataTable;
            DataView dv = new DataView(dtTeamMembers, "CharacterID = " + e.CommandArgument.ToString(), "CharacterLastName", DataViewRowState.CurrentRows);

            lblChangesNotSaved.Visible = true;
            lblChangesNotSaved2.Visible = true;
            switch (e.CommandName.ToUpper())
            {
                case "INVITECHAR":
                    if (dv.Count > 0)
                    {
                        dv[0]["Invited"] = true;
                        dv[0]["Message"] = "Invited";
                        dv[0]["UpdateRecord"] = true;
                    }
                    break;

                case "APPROVECHAR":
                case "ACCEPTCHAR":
                    if (dv.Count > 0)
                    {
                        dv[0]["Member"] = true;
                        dv[0]["Invited"] = false;
                        dv[0]["Message"] = "Approved";
                        dv[0]["UpdateRecord"] = true;
                    }
                    break;

                case "DENYCHAR":
                case "REVOKECHAR":
                    if (dv.Count > 0)
                    {
                        dv[0]["Member"] = false;
                        dv[0]["Approval"] = false;
                        dv[0]["Requested"] = false;
                        dv[0]["Invited"] = false;
                        dv[0]["DisplayInvite"] = "0";
                        dv[0]["DisplayApprove"] = "0";
                        dv[0]["DisplayRevoke"] = "0";
                        dv[0]["DisplayAccept"] = "0";
                        dv[0]["Message"] = "Removed";
                        dv[0]["UpdateRecord"] = true;
                    }
                    break;
            }
            Session["TeamMembers"] = dtTeamMembers;
            BindData();
        }


        public void BindData()
        {
            if (Session["TeamMembers"] != null)
            {
                DataTable dtTeamMembers = Session["TeamMembers"] as DataTable;

                if (dtTeamMembers.Columns["DisplayInvite"] == null)
                    dtTeamMembers.Columns.Add("DisplayInvite", typeof(string));
                if (dtTeamMembers.Columns["DisplayApprove"] == null)
                    dtTeamMembers.Columns.Add("DisplayApprove", typeof(string));
                if (dtTeamMembers.Columns["DisplayAccept"] == null)
                    dtTeamMembers.Columns.Add("DisplayAccept", typeof(string));
                if (dtTeamMembers.Columns["DisplayRevoke"] == null)
                    dtTeamMembers.Columns.Add("DisplayRevoke", typeof(string));
                if (dtTeamMembers.Columns["Message"] == null)
                    dtTeamMembers.Columns.Add("Message", typeof(string));

                if (dtTeamMembers.Columns["UpdateRecord"] == null)
                {
                    DataColumn dValueChanged = new DataColumn("UpdateRecord", typeof(Boolean));
                    dValueChanged.DefaultValue = false;
                    dtTeamMembers.Columns.Add(dValueChanged);
                }

                foreach (DataRow dRow in dtTeamMembers.Rows)
                {
                    if (((bool)dRow["Approval"]) ||
                        ((bool)dRow["Member"]))
                    {
                        dRow["DisplayInvite"] = "0";
                        dRow["DisplayApprove"] = "0";
                        dRow["DisplayRevoke"] = "1";
                        dRow["DisplayAccept"] = "0";
                    }
                    else if ((bool)dRow["Requested"])
                    {
                        dRow["DisplayInvite"] = "0";
                        dRow["DisplayApprove"] = "1";
                        dRow["DisplayAccept"] = "1";
                        dRow["DisplayRevoke"] = "0";
                    }
                    else if ((bool)dRow["Invited"])
                    {
                        dRow["DisplayApprove"] = "0";
                        dRow["DisplayInvite"] = "0";
                        dRow["DisplayAccept"] = "0";
                        dRow["DisplayRevoke"] = "1";
                    }
                    else
                    {
                        dRow["DisplayApprove"] = "0";
                        dRow["DisplayInvite"] = "1";
                        dRow["DisplayAccept"] = "0";
                        dRow["DisplayRevoke"] = "0";
                    }
                }

                DataView dvMember = new DataView(dtTeamMembers, "Approval or Member or Invited", "CharacterAKA", DataViewRowState.CurrentRows);
                gvMembers.DataSource = dvMember;
                gvMembers.DataBind();

                DataView dvAvailable = new DataView(dtTeamMembers, "not Approval and not Member and not Invited", "CharacterAKA", DataViewRowState.CurrentRows);
                gvAvailable.DataSource = dvAvailable;
                gvAvailable.DataBind();

                Session["TeamMembers"] = dtTeamMembers;
            }
        }

        protected void chkBoxApprover_CheckedChanged(object sender, EventArgs e)
        {
            lblChangesNotSaved.Visible = true;
            lblChangesNotSaved2.Visible = true;

            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox chkBoxApprover = (CheckBox)gvMembers.Rows[index].FindControl("chkBoxApprover");
            HiddenField hidCharacterID = (HiddenField)gvMembers.Rows[index].FindControl("hidCharacterID");

            if ((chkBoxApprover != null) &&
                (hidCharacterID != null) &&
                (Session["TeamMembers"] != null))
            {
                DataTable dtTeamMembers = Session["TeamMembers"] as DataTable;
                DataRow[] dChar = dtTeamMembers.Select("CharacterID = " + hidCharacterID.Value);

                foreach ( DataRow dRow in dChar)
                {
                    dRow["Approval"] = chkBoxApprover.Checked;
                    dRow["UpdateRecord"] = true;
                }
                Session["TeamMembers"] = dtTeamMembers;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["TeamMembers"] != null)
            {
                int Approval = -1;
                int Member = -1;
                int Invited = -1;
                int Requested = -1;

                DataTable dtTeamMembers = Session["TeamMembers"] as DataTable;

                SortedList sParams = new SortedList();
                DataTable dtStatus = cUtilities.LoadDataTable("select RoleID, RoleDescription from MDBRoles where RoleTier='Team'", sParams, "LARPortal",
                    _UserName, lsRoutineName, cUtilities.LoadDataTableCommandType.Text);

                foreach (DataRow dStatus in dtStatus.Rows)
                {
                    string sRoles = dStatus["RoleDescription"].ToString().ToUpper();
                    if (sRoles.Contains("APPROVAL"))
                        int.TryParse(dStatus["RoleID"].ToString(), out Approval);
                    else if (sRoles.Contains("REQUESTED"))
                        int.TryParse(dStatus["RoleID"].ToString(), out Requested);
                    else if (sRoles.Contains("INVITED"))
                        int.TryParse(dStatus["RoleID"].ToString(), out Invited);
                    else if (sRoles == "TEAM MEMBER")
                        int.TryParse(dStatus["RoleID"].ToString(), out Member);
                }

                DataView dv = new DataView(dtTeamMembers, "UpdateRecord", "", DataViewRowState.CurrentRows);

                foreach (DataRowView dRow in dv)
                {
                    string sProcedureName = "uspInsUpdCMTeamMembers";

                    int CharID = -1;
                    int TeamID = -1;
                    int.TryParse(dRow["CharacterID"].ToString(), out CharID);
                    int.TryParse(dRow["TeamID"].ToString(), out TeamID);
                    sParams = new SortedList();
                    sParams.Add("@UserID", _iUserID);
                    sParams.Add("@TeamID", TeamID);
                    sParams.Add("@CharacterID", CharID);

                    if ((bool)dRow["Approval"])
                        sParams.Add("@RoleID", Approval);
                    else if ((bool)dRow["Member"])
                        sParams.Add("@RoleID", Member);
                    else if ((bool)dRow["Requested"])
                        sParams.Add("@RoleID", Requested);
                    else if ((bool)dRow["Invited"])
                        sParams.Add("@RoleID", Invited);
                    else
                        sProcedureName = "uspDelCMTeamMembers";

                    cUtilities.PerformNonQuery(sProcedureName, sParams, "LARPortal", _UserName);
                }
            }
            lblmodalMessage.Text = "Changes have been saved.";
            lblChangesNotSaved.Visible = false;
            lblChangesNotSaved2.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
        }
    }
}

