using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Profile
{
    public partial class Resume : System.Web.UI.Page
    {
        protected int _UserID = 0;
        protected string _UserName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["Username"] != null)
                _UserName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);

            btnClosePlayerSkill.Attributes.Add("data-dismiss", "modal");
            btnCloseAffil.Attributes.Add("data-dismiss", "modal");
            btnCancelDeleteAffil.Attributes.Add("data-dismiss", "modal");
            btnCancelDeleteSkill.Attributes.Add("data-dismiss", "modal");

            if (!IsPostBack)
            {
                lblModalMessage.Text = "";
            }

            tbResumeComments.Attributes.Add("PlaceHolder", "Add any comments others may find useful.");
            tbLinkedInURL.Attributes.Add("PlaceHolder", "https://www.linkedin.com/...");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (!IsPostBack)
            {
            }

            Classes.cPlayer PlayerInfo = new cPlayer(_UserID, _UserName);
            hidPlayerProfileID.Value = PlayerInfo.PlayerProfileID.ToString();
            gvSkills.DataSource = PlayerInfo.PlayerSkills;
            gvSkills.DataBind();

            gvAffiliations.DataSource = PlayerInfo.PlayerAffiliations;
            gvAffiliations.DataBind();

            tbResumeComments.Text = PlayerInfo.ResumeComments;
            tbLinkedInURL.Text = PlayerInfo.LinkedInURL;
        }

        protected void btnSaveComments_Click(object sender, EventArgs e)
        {
            Classes.cPlayer PlayerInfo = new cPlayer(_UserID, _UserName);
            PlayerInfo.ResumeComments = tbResumeComments.Text;
            PlayerInfo.LinkedInURL = tbLinkedInURL.Text;
            PlayerInfo.Save();
            lblModalMessage.Text = "Your information has been saved.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalMessage();", true);
        }

        protected void btnSaveAffiliation_Click(object sender, EventArgs e)
        {
            int iPlayerAffilID;
            int iPlayerProfileID;
            if ((int.TryParse(hidPlayerAffilID.Value, out iPlayerAffilID)) &&
                (int.TryParse(hidPlayerProfileID.Value, out iPlayerProfileID)))
            {
                Classes.cPlayerAffiliation PlayerAffil = new cPlayerAffiliation(iPlayerAffilID, _UserName, _UserID);
                PlayerAffil.AffiliationName = tbAffiliationName.Text;
                PlayerAffil.AffiliationRole = tbAffiliationRole.Text;
                PlayerAffil.PlayerComments = tbAffiliationComments.Text;
                PlayerAffil.PlayerProfileID = iPlayerProfileID;
                PlayerAffil.Save(_UserName, _UserID);
            }
        }

        protected void btnDeleteAffil_Click(object sender, EventArgs e)
        {
            int iDeleteAffilID;
            if (int.TryParse(hidDeleteAffilID.Value, out iDeleteAffilID))
            {
                cPlayerAffiliation PlayerAffil = new cPlayerAffiliation(iDeleteAffilID, _UserName, _UserID);
                PlayerAffil.RecordStatus = RecordStatuses.Delete;
                PlayerAffil.Delete(_UserName, _UserID);
                lblModalMessage.Text = "Your affiliation has been deleted.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalMessage();", true);
            }
        }

        protected void btnSaveSkill_Click(object sender, EventArgs e)
        {
            int iPlayerSkillID;
            int iPlayerProfileID;
            if ((int.TryParse(hidPlayerResumeID.Value, out iPlayerSkillID)) &&
                (int.TryParse(hidPlayerProfileID.Value, out iPlayerProfileID)))
            {
                Classes.cPlayerSkill PlayerSkill = new cPlayerSkill(iPlayerSkillID, _UserName, _UserID);
                PlayerSkill.SkillLevel = ddlSkillLevel.SelectedValue;
                PlayerSkill.SkillName = tbSkillName.Text;
                PlayerSkill.PlayerComments = tbSkillComments.Text;
                PlayerSkill.PlayerProfileID = iPlayerProfileID;
                PlayerSkill.Save(_UserName, _UserID);
            }
        }

        protected void btnDeleteSkill_Click(object sender, EventArgs e)
        {
            int iDeleteSkillID;
            if (int.TryParse(hidDeleteSkillID.Value, out iDeleteSkillID))
            {
                cPlayerSkill PlayerSkill = new cPlayerSkill(iDeleteSkillID, _UserName, _UserID);
                PlayerSkill.RecordStatus = RecordStatuses.Delete;
                PlayerSkill.Delete(_UserName, _UserID);
                lblModalMessage.Text = "Your skill has been deleted.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalMessage();", true);
            }
        }
    }
}