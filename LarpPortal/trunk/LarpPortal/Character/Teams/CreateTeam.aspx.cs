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
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["SelectedCharacter"] == null)
                Response.Redirect("../CharInfo.aspx", true);

            if ((!IsPostBack) || (_Reload))
            {
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

                    Classes.cCharacter cChar = new Classes.cCharacter();
                    cChar.LoadCharacter(iCharID);

                    hidCampaignID.Value = cChar.CampaignID.ToString();

                    slParameters = new SortedList();
                    slParameters.Add("@CampaignID", cChar.CampaignID);
                    DataTable dtTeams = Classes.cUtilities.LoadDataTable("uspGetTeamsByCampaignID", slParameters, "LARPortal", _UserName, lsRoutineName + ".GetTeamsByCampaignID");

                    DataView dvTeams = new DataView(dtTeams, "", "TeamName", DataViewRowState.CurrentRows);
                    gvList.DataSource = dvTeams;
                    gvList.DataBind();
                }
            }
        }

        //protected void ProcessButton(object sender, CommandEventArgs e)
        //{
        //    //int iUserID = 0;
        //    //if (Session["UserID"] != null)
        //    //    int.TryParse(Session["UserID"].ToString(), out iUserID);
        //    //iUserID = iUserID == 0 ? -1 : iUserID;

        //    //if (Session["SelectedCharacter"] != null)
        //    //{
        //    //    string sSelected = Session["SelectedCharacter"].ToString();
        //    //    int iCharID;
        //    //    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
        //    //    {
        //    //        Classes.cCharacterHistory cCharHist = new Classes.cCharacterHistory();
        //    //        cCharHist.Load(iCharID, iUserID);
        //    //        cCharHist.History = ckEditor.Text;
        //    //        if (e.CommandName.ToUpper() == "SUBMIT")
        //    //        {
        //    //            cCharHist.DateSubmitted = DateTime.Now;
        //    //            lblmodalMessage.Text = "The character history has been submitted.";
        //    //            SendSubmittedEmail(ckEditor.Text, cCharHist);
        //    //        }
        //    //        else
        //    //            lblmodalMessage.Text = "The character history has been saved.";

        //    //        cCharHist.Save(iCharID, iUserID, Session["UserName"].ToString());
        //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
        //    //        _Reload = true;
        //    //    }
        //    //}
        //}

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

        protected void btnCreateTeam_Click(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

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
                Classes.cUtilities.PerformNonQuery("uspInsUpdCMTeams", slParameters, "LARPortal", _UserName);
                _Reload = true;
            }
        }
    }
}