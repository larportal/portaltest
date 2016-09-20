using System;
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
    public partial class JoinTeam : System.Web.UI.Page
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
            {
                ViewState["Reload"] = "Y";
                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
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
                    DataTable dtTeams = cUtilities.LoadDataTable("uspGetTeamsForCampaignAndCharacterInd", slParameters, "LARPortal", _UserName, lsRoutineName + ".CampaignAndCharacterInd");

                    Session["JoinTeamData"] = dtTeams;
                    BindData();
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

            DataTable dtTeams = Session["JoinTeamData"] as DataTable;
            DataRow[] dTeams = dtTeams.Select("TeamID = " + e.CommandArgument.ToString());

            if (dTeams.Length > 0)
            {
                DataRow dTeamRow = dTeams[0];

                switch (e.CommandName.ToUpper())
                {
                    case "JOINTEAM":
                        dTeamRow["Invited"] = "0";
                        dTeamRow["Approval"] = "0";
                        dTeamRow["Member"] = "0";
                        dTeamRow["Requested"] = "1";
                        break;

                    case "LEAVETEAM":
                        dTeamRow["Member"] = "0";
                        dTeamRow["Approval"] = "0";
                        dTeamRow["Invited"] = "0";
                        dTeamRow["Requested"] = "0";
                        break;

                    case "ACCEPTINVITE":
                        dTeamRow["Invited"] = "0";
                        dTeamRow["Member"] = "1";
                        dTeamRow["Requested"] = "0";
                        dTeamRow["Approval"] = "0";
                        break;

                    case "DECLINEINVITE":
                    case "CANCELREQUEST":
                        dTeamRow["Invited"] = "0";
                        dTeamRow["Member"] = "0";
                        dTeamRow["Approval"] = "0";
                        dTeamRow["Requested"] = "0";
                        break;
                }
                Session["JoinTeamData"] = dtTeams;
                BindData();
            }
        }

        public void BindData()
        {
            if (Session["JoinTeamData"] != null)
            {
                DataTable dtTeams = Session["JoinTeamData"] as DataTable;

                if (dtTeams.Columns["DisplayAccept"] == null)
                    dtTeams.Columns.Add("DisplayAccept", typeof(string));

                if (dtTeams.Columns["DisplayJoin"] == null)
                    dtTeams.Columns.Add("DisplayJoin", typeof(string));

                if (dtTeams.Columns["DisplayLeave"] == null)
                    dtTeams.Columns.Add("DisplayLeave", typeof(string));

                if (dtTeams.Columns["Message"] == null)
                    dtTeams.Columns.Add("Message", typeof(string));

                foreach (DataRow dRow in dtTeams.Rows)
                {
                    if ((dRow["Approval"].ToString() == "1") ||
                        (dRow["Member"].ToString() == "1"))
                    {
                        dRow["Message"] = "";
                        dRow["DisplayJoin"] = "0";
                        dRow["DisplayAccept"] = "0";
                        dRow["DisplayLeave"] = "1";
                    }
                    else if (dRow["Requested"].ToString() == "1")
                    {
                        dRow["Message"] = "Request Pending";
                        dRow["DisplayAccept"] = "0";
                        dRow["DisplayJoin"] = "0";
                        dRow["DisplayLeave"] = "0";
                    }
                    else if (dRow["Invited"].ToString() == "1")
                    {
                        dRow["Message"] = "Invited";
                        dRow["DisplayAccept"] = "1";
                        dRow["DisplayJoin"] = "0";
                        dRow["DisplayLeave"] = "0";
                    }
                    else
                    {
                        dRow["Message"] = "";
                        dRow["DisplayAccept"] = "0";
                        dRow["DisplayJoin"] = "1";
                        dRow["DisplayLeave"] = "0";
                    }
                }

                DataView dvMember = new DataView(dtTeams, "Approval = 1 or Member = 1 or Requested = 1", "TeamName", DataViewRowState.CurrentRows);
                gvMembers.DataSource = dvMember;
                gvMembers.DataBind();

                DataView dvAvailable = new DataView(dtTeams, "Approval = 0 and Member = 0 and Requested = 0", "TeamName", DataViewRowState.CurrentRows);
                gvAvailable.DataSource = dvAvailable;
                gvAvailable.DataBind();

                Session["JoinTeamData"] = dtTeams;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            DataTable dtStatuses = new DataTable();
            SortedList sParams = new SortedList();
            dtStatuses = cUtilities.LoadDataTable("select RoleID, RoleDescription, RoleTier from MDBRoles where RoleTier = 'Team' and DateDeleted is null", sParams, "LARPortal", _UserName,
                lsRoutineName, cUtilities.LoadDataTableCommandType.Text);

            int iRoleApprove = -1;
            int iRoleMember = -1;
            int iRoleInvite = -1;
            int iRoleRequested = -1;

            foreach (DataRow dRow in dtStatuses.Rows)
            {
                string RoleDesc = dRow["RoleDescription"].ToString().ToUpper();
                if (RoleDesc.Contains("APPROVAL"))
                    int.TryParse(dRow["RoleID"].ToString(), out iRoleApprove);
                else if (RoleDesc.Contains("REQUESTED"))
                    int.TryParse(dRow["RoleID"].ToString(), out iRoleRequested);
                else if (RoleDesc.Contains("INVITED"))
                    int.TryParse(dRow["RoleID"].ToString(), out iRoleInvite);
                else
                    int.TryParse(dRow["RoleID"].ToString(), out iRoleMember);
            }

            DataTable dtTeams = Session["JoinTeamData"] as DataTable;
            foreach (DataRow dRow in dtTeams.Rows)
            {
                if (dRow.RowState != DataRowState.Unchanged)
                {
                    string t = dRow["Approval"].ToString() + "/" + dRow["Member"].ToString() + "/" + dRow["Invited"].ToString() + "/" + dRow["Requested"].ToString();

                    int iTeamID = 0;
                    int iSelectedCharacter = 0;
                    if ((int.TryParse(dRow["TeamID"].ToString(), out iTeamID)) &&
                         (int.TryParse(Session["SelectedCharacter"].ToString(), out iSelectedCharacter)))
                    {
                        if (dRow["Approval"].ToString() == "1")
                        {
                            sParams = new SortedList();
                            sParams.Add("@TeamID", iTeamID);
                            sParams.Add("@CharacterID", iSelectedCharacter);
                            sParams.Add("@RoleID", iRoleApprove);
                            sParams.Add("@UserID", _iUserID);
                            sParams.Add("@TeamMemberID", -1);
                            cUtilities.PerformNonQuery("uspInsUpdCMTeamMembers", sParams, "LARPortal", _UserName);
                        }
                        else if (dRow["Member"].ToString() == "1")
                        {
                            sParams = new SortedList();
                            sParams.Add("@TeamID", iTeamID);
                            sParams.Add("@CharacterID", iSelectedCharacter);
                            sParams.Add("@RoleID", iRoleMember);
                            sParams.Add("@UserID", _iUserID);
                            sParams.Add("@TeamMemberID", -1);
                            cUtilities.PerformNonQuery("uspInsUpdCMTeamMembers", sParams, "LARPortal", _UserName);
                        }
                        else if (dRow["Requested"].ToString() == "1")
                        {
                            sParams = new SortedList();
                            sParams.Add("@TeamID", iTeamID);
                            sParams.Add("@CharacterID", iSelectedCharacter);
                            sParams.Add("@RoleID", iRoleRequested);
                            sParams.Add("@UserID", _iUserID);
                            sParams.Add("@TeamMemberID", -1);
                            cUtilities.PerformNonQuery("uspInsUpdCMTeamMembers", sParams, "LARPortal", _UserName);
                        }
                        else if (dRow["Invited"].ToString() == "1")
                        {
                            sParams = new SortedList();
                            sParams.Add("@TeamID", iTeamID);
                            sParams.Add("@CharacterID", iSelectedCharacter);
                            sParams.Add("@RoleID", iRoleInvite);
                            sParams.Add("@UserID", _iUserID);
                            sParams.Add("@TeamMemberID", -1);
                            cUtilities.PerformNonQuery("uspInsUpdCMTeamMembers", sParams, "LARPortal", _UserName);
                        }
                    }
                }
            }
        }

        //protected void btnCreateTeam_Click(object sender, EventArgs e)
        //{
        //    MethodBase lmth = MethodBase.GetCurrentMethod();
        //    string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

        //    SortedList slParameters = new SortedList();
        //    slParameters.Add("@CampaignID", hidCampaignID.Value);
        //    DataTable dtTeams = Classes.cUtilities.LoadDataTable("uspGetTeamsByCampaignID", slParameters, "LARPortal", _UserName, lsRoutineName);

        //    DataView dvTeams = new DataView(dtTeams, "TeamName = '" + tbNewTeamName.Text.Replace("'", "''") + "'", "", DataViewRowState.CurrentRows);

        //    if (dvTeams.Count > 0)
        //    {
        //        lblAlreadyExists.Visible = true;
        //    }
        //    else
        //    {
        //        slParameters = new SortedList();
        //        slParameters.Add("@UserID", _iUserID);
        //        slParameters.Add("@TeamID", "-1");
        //        slParameters.Add("@TeamName", tbNewTeamName.Text);
        //        slParameters.Add("@TeamTypeID", "1");
        //        slParameters.Add("@CampaignID", hidCampaignID.Value);
        //        Classes.cUtilities.PerformNonQuery("uspInsUpdCMTeams", slParameters, "LARPortal", _UserName);
        //        _Reload = true;
        //    }
        //}

        //private void BindData()
        //{
        //    DataTable dtTeams = Session["JoinTeamData"] as DataTable;

        //            DataView dvAvail = new DataView(dtTeams, "MemberOfTeam = 0", "TeamName", DataViewRowState.CurrentRows);
        //            gvAvailable.DataSource = dvAvail;
        //            gvAvailable.DataBind();

        //            DataView dvMember = new DataView(dtTeams, "MemberOfTeam = 1", "TeamName", DataViewRowState.CurrentRows);
        //            gvMembers.DataSource = dvMember;
        //            gvMembers.DataBind();

        //}

    }
}


