using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character.Teams
{
    public partial class CreateTeam : System.Web.UI.Page
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
            tbNewTeamName.Attributes.Add("PlaceHolder", "Name of new team");
            oCharSelect.CharacterChanged += oCharSelect_CharacterChanged;
            btnCloseMessage.Attributes.Add("data-dismiss", "modal");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            oCharSelect.LoadInfo();

            if (!oCharSelect.CharacterID.HasValue)
                Response.Redirect("../CharInfo.aspx", true);

            if ((!IsPostBack) || (_Reload))
            {
                hidCampaignID.Value = oCharSelect.CharacterInfo.CampaignID.ToString();

                SortedList slParameters = new SortedList();
                slParameters.Add("@CampaignID", oCharSelect.CharacterInfo.CampaignID);

                DataTable dtTeams = Classes.cUtilities.LoadDataTable("uspGetTeamsByCampaignID", slParameters, "LARPortal", _UserName, lsRoutineName + ".GetTeamsByCampaignID");

                DataView dvTeams = new DataView(dtTeams, "", "TeamName", DataViewRowState.CurrentRows);
                gvList.DataSource = dvTeams;
                gvList.DataBind();
            }
        }

        private void SendSubmittedEmail(string sHistory, Classes.cCharacterHistory cHist)
        {
            try
            {
                if (hidNotificationEMail.Value.Length > 0)
                {
                    Classes.cUser User = new Classes.cUser(_UserName, "PasswordNotNeeded");
                    string sSubject = cHist.CampaignName + " character history from " + cHist.PlayerName + " - " + cHist.CharacterAKA;

                    string sBody = (string.IsNullOrEmpty(User.NickName) ? User.FirstName : User.NickName) +
                        " " + User.LastName + " has submitted a character history for " + cHist.CharacterAKA + ".<br><br>" +
                         sHistory;
                    Classes.cEmailMessageService cEMS = new Classes.cEmailMessageService();
                    cEMS.SendMail(sSubject, sBody, cHist.NotificationEMail, "", "", "CharacterHistory", _UserName);
                }
            }
            catch (Exception ex)
            {
                // Write the exception to error log and then throw it again...
                Classes.ErrorAtServer lobjError = new Classes.ErrorAtServer();
                lobjError.ProcessError(ex, "CharacterEdit.aspx.SendSubmittedEmail", "", Session.SessionID);
            }
        }

        protected void btnCloseMessage_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeMessage();", true);
        }

        protected void btnCreateTeam_Click(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            oCharSelect.LoadInfo();

            SortedList slParameters = new SortedList();
            slParameters.Add("@CampaignID", hidCampaignID.Value);
            DataTable dtTeams = Classes.cUtilities.LoadDataTable("uspGetTeamsByCampaignID", slParameters, "LARPortal", _UserName, lsRoutineName);

            DataView dvTeams = new DataView(dtTeams, "TeamName = '" + tbNewTeamName.Text.Replace("'", "''") + "'", "", DataViewRowState.CurrentRows);

            if (dvTeams.Count > 0)
            {
                lblAlreadyExists.Visible = true;
            }
            else
            {
                int CampaignID = 0;
                Int32.TryParse(hidCampaignID.Value, out CampaignID);
                slParameters = new SortedList();
                slParameters.Add("@UserID", _iUserID);
                slParameters.Add("@TeamID", -1);
                slParameters.Add("@TeamName", tbNewTeamName.Text);
                slParameters.Add("@TeamTypeID", 1);
                slParameters.Add("@CampaignID", CampaignID);
                slParameters.Add("@StatusID", 16);  // Active
                slParameters.Add("@CharacterID", oCharSelect.CharacterID.Value);
                Classes.cUtilities.PerformNonQuery("uspInsUpdCMTeams", slParameters, "LARPortal", _UserName);

                lblmodalMessage.Text = "The team " + tbNewTeamName.Text + " has been created.<br><br>" +
                    "You are the manager of the team.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
                tbNewTeamName.Text = "";
                _Reload = true;
            }
        }

        protected void oCharSelect_CharacterChanged(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (oCharSelect.CharacterInfo != null)
            {
                if (oCharSelect.CharacterID.HasValue)
                {
                    Classes.cUser UserInfo = new Classes.cUser(_UserName, "PasswordNotNeeded");
                    UserInfo.LastLoggedInCampaign = oCharSelect.CharacterInfo.CampaignID;
                    UserInfo.LastLoggedInCharacter = oCharSelect.CharacterID.Value;
                    UserInfo.LastLoggedInMyCharOrCamp = (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters ? "M" : "C");
                    UserInfo.Save();
                }
                _Reload = true;
            }
        }
    }
}