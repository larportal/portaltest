using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Campaigns
{
    public partial class SetupContacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSavedData();
            }
        }

        protected void LoadSavedData()
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            Classes.cCampaignBase Campaigns = new Classes.cCampaignBase(CampaignID, UserName, UserID);
            tbCampaignURL.Text = Campaigns.URL;
            tbCharacterGeneratorURL.Text = Campaigns.CharacterGeneratorURL;
            tbCharacterHistoryEmail.Text = Campaigns.CharacterHistoryNotificationEmail;
            chkCharacterHistoryEmail.Checked = Campaigns.ShowCharacterHistoryEmail;
            tbCharacterHistoryURL.Text = Campaigns.CharacterHistoryURL;
            tbCharacterNotificationEmail.Text = Campaigns.CharacterNotificationEMail;
            chkCharacterNotificationEmail.Checked = Campaigns.ShowCharacterNotificationEmail;
            tbCPEmail.Text = Campaigns.CPNotificationEmail;
            chkCPEmail.Checked = Campaigns.ShowCPNotificationEmail;
            tbInfoRequestEmail.Text = Campaigns.InfoRequestEmail;
            chkInfoRequestEmail.Checked = Campaigns.ShowCampaignInfoEmail;
            tbInfoSkillEmail.Text = Campaigns.InfoSkillEMail;
            chkInfoSkillEmail.Checked = Campaigns.ShowInfoSkillEmail;
            tbInfoSkillURL.Text = Campaigns.InfoSkillURL;
            tbJoinRequestEmail.Text = Campaigns.JoinRequestEmail;
            chkJoinRequestEmail.Checked = Campaigns.ShowJoinRequestEmail;
            tbJoinURL.Text = Campaigns.JoinURL;
            tbPELNotificationEmail.Text = Campaigns.PELNotificationEMail;
            chkPELNotificationEmail.Checked = Campaigns.ShowPELNotificationEmail;
            tbPELSubmissionURL.Text = Campaigns.PELSubmissionURL;
            tbProductionSkillEmail.Text = Campaigns.ProductionSkillEMail;
            chkProductionSkillEmail.Checked = Campaigns.ShowProductionSkillEmail;
            tbProductionSkillURL.Text = Campaigns.ProductionSkillURL;
            tbRegistrationNotificationEmail.Text = Campaigns.RegistrationNotificationEmail;
            chkREgistrationNotificationEmail.Checked = Campaigns.ShowRegistrationNotificationEmail;
            tbRegistrationURL.Text = Campaigns.RegistrationURL;
            tbRulesURL.Text = Campaigns.RulesURL;

        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            Classes.cCampaignBase Campaigns = new Classes.cCampaignBase(CampaignID, UserName, UserID);
            Campaigns.URL = tbCampaignURL.Text;
            Campaigns.CharacterGeneratorURL = tbCharacterGeneratorURL.Text;
            Campaigns.CharacterHistoryNotificationEmail = tbCharacterHistoryEmail.Text;
            Campaigns.ShowCharacterHistoryEmail = chkCharacterHistoryEmail.Checked;
            Campaigns.CharacterHistoryURL = tbCharacterHistoryURL.Text;
            Campaigns.CharacterNotificationEMail = tbCharacterNotificationEmail.Text;
            Campaigns.ShowCharacterNotificationEmail = chkCharacterNotificationEmail.Checked;
            Campaigns.CPNotificationEmail = tbCPEmail.Text;
            Campaigns.ShowCPNotificationEmail = chkCPEmail.Checked;
            Campaigns.InfoRequestEmail = tbInfoRequestEmail.Text;
            Campaigns.ShowCampaignInfoEmail = chkInfoRequestEmail.Checked;
            Campaigns.InfoSkillEMail = tbInfoSkillEmail.Text;
            Campaigns.ShowInfoSkillEmail = chkInfoSkillEmail.Checked;
            Campaigns.InfoSkillURL = tbInfoSkillURL.Text;
            Campaigns.JoinRequestEmail = tbJoinRequestEmail.Text;
            Campaigns.ShowJoinRequestEmail = chkJoinRequestEmail.Checked;
            Campaigns.JoinURL = tbJoinURL.Text;
            Campaigns.PELNotificationEMail = tbPELNotificationEmail.Text;
            Campaigns.ShowPELNotificationEmail = chkPELNotificationEmail.Checked;
            Campaigns.PELSubmissionURL = tbPELSubmissionURL.Text;
            Campaigns.ProductionSkillEMail = tbProductionSkillEmail.Text;
            Campaigns.ShowProductionSkillEmail = chkProductionSkillEmail.Checked;
            Campaigns.ProductionSkillURL = tbProductionSkillURL.Text;
            Campaigns.RegistrationNotificationEmail = tbRegistrationNotificationEmail.Text;
            Campaigns.ShowRegistrationNotificationEmail = chkREgistrationNotificationEmail.Checked;
            Campaigns.RegistrationURL = tbRegistrationURL.Text;
            Campaigns.RulesURL = tbRulesURL.Text;
            Campaigns.Save();
            string jsString = "alert('Campaign contact changes have been saved.');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    "MyApplication",
                    jsString,
                    true);
        }

    }
}