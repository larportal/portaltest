using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharHistory : System.Web.UI.Page
    {
        private string _UserName = "";
        private int _UserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
            oCharSelect.CharacterChanged += oCharSelect_CharacterChanged;

            if (!IsPostBack)
            {
            }

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (!IsPostBack)
            {
                if (oCharSelect.CharacterID.HasValue)
                {
                    tbHistory.Text = oCharSelect.CharacterInfo.CharacterHistory;        // cChar.CharacterHistory;
                    lblHistory.Text = oCharSelect.CharacterInfo.CharacterHistory.Replace(Environment.NewLine, "<br>");

                    if ((oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters) &&
                        (oCharSelect.CharacterInfo.CharacterType != 1))
                    {
                        mvButtons.Visible = false;
                        lblHistory.Visible = true;
                        tbHistory.Visible = false;
                    }
                    else
                    {
                        if (oCharSelect.CharacterInfo.DateHistoryApproved != null)
                        {
                            mvButtons.SetActiveView(vwAlreadyApproved);
                            tbHistory.Visible = false;
                            lblHistory.Visible = true;
                        }
                        else if (oCharSelect.CharacterInfo.DateHistorySubmitted != null)
                        {
                            mvButtons.SetActiveView(vwAlreadySubmitted);
                            tbHistory.Visible = false;
                            lblHistory.Visible = true;
                        }
                        else
                        {
                            mvButtons.SetActiveView(vwButtons);
                            tbHistory.Visible = true;
                            lblHistory.Visible = false;
                        }
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (oCharSelect.CharacterID.HasValue)
            {
                oCharSelect.CharacterInfo.CharacterHistory = tbHistory.Text;
                oCharSelect.CharacterInfo.SaveCharacter(_UserName, _UserID);

                string jsString = "alert('Character " + oCharSelect.CharacterInfo.AKA +     // cChar.AKA + 
                    " has been saved.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
                CharacterHistoryEmailNotificaiton();
            }
        }

        protected void CharacterHistoryEmailNotificaiton()
        {
            string strUsername = Session["Username"].ToString();
            string strTo = "";
            string strCampaignName = oCharSelect.CharacterInfo.CampaignName;        // lblCampaign.Text;
            string strPlayerName = "";
            string strPlayerEmail = "";

            if (Session["CharacterHistoryEmail"] != null)
                strTo = Session["CharacterHistoryEmail"].ToString();
            if (Session["PlayerName"] != null)
                strPlayerName = Session["PlayerName"].ToString();
            if (Session["PlayerEmail"] != null)
                strPlayerEmail = Session["PlayerEmail"].ToString();
            string strSubject = "Character History submitted - Character: " + oCharSelect.CharacterInfo.AKA;
            string strBody = "A new character history has been submitted for campaign " + oCharSelect.CharacterInfo.CampaignName +
                "<br><br>";
            strBody = strBody + "Player: " + strPlayerName + "<br>";
            strBody = strBody + "Player Email:" + strPlayerEmail + "<br>";
            strBody = strBody + "Character:" + oCharSelect.CharacterInfo.CampaignName + "<br><br>";
            strBody = strBody + "History Text:<br>" + tbHistory.Text;
            strBody = strBody.Replace(System.Environment.NewLine, "<br />");

            Classes.cSendNotifications Notifications = new Classes.cSendNotifications();
            Classes.cEmailMessageService SubmitCharacterHistory = new Classes.cEmailMessageService();
            try
            {
                SubmitCharacterHistory.SendMail(strSubject, strBody, strTo, strPlayerEmail, "", "CharacterHistory", _UserName);
            }
            catch (Exception)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CharHistory.aspx", true);
        }

        protected void btnSubmitOrSave_Command(object sender, CommandEventArgs e)
        {
            if (oCharSelect.CharacterID.HasValue)
            {
                oCharSelect.CharacterInfo.CharacterHistory = tbHistory.Text;
//                oCharSelect.CharacterInfo.Comments = tbHistory.Text;
                if (e.CommandName == "SUBMIT")
                {
                    oCharSelect.CharacterInfo.DateHistorySubmitted = DateTime.Now;
                    lblCharHistoryMessage.Text = "The character history for " + oCharSelect.CharacterInfo.AKA +
                        " has been submitted for staff approval.";
                }
                else
                    lblCharHistoryMessage.Text = "The character history for " + oCharSelect.CharacterInfo.AKA +
                        " has been saved.";

                oCharSelect.CharacterInfo.SaveCharacter(_UserName, _UserID);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
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
            }
        }
    }
}