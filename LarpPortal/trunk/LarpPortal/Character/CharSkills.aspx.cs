using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Character
{
    public partial class CharSkills : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["CurrentCharacter"] = "";
            }
            oCharSelect.CharacterChanged += oCharSelect_CharacterChanged;
            btnCloseMessage.Attributes.Add("data-dismiss", "modal");
            btnCloseError.Attributes.Add("data-dismiss", "modal");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();
            if (oCharSelect.CharacterID.HasValue)
                Session["CharSkillCharacterID"] = oCharSelect.CharacterID.Value;

            if (!IsPostBack)
                oCharSelect_CharacterChanged(null, null);

            btnSave.Enabled = true;
            btnSave.Style["background-color"] = null;
            btnSave.CssClass = "StandardButton";

            if (Session["CharSkillReadOnly"] != null)
                if (Session["CharSkillReadOnly"].ToString() == "Y")
                {
                    btnSave.Enabled = false;
                    btnSave.CssClass = "btn-default";
                    btnSave.Style["background-color"] = "grey";
                }

            if (Session["SkillSavedMessage"] != null)
            {
                lblmodalMessage.Text = Session["SkillSavedMessage"].ToString();
                Session.Remove("SkillSavedMessage");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
            }
        }

        protected void oCharSelect_CharacterChanged(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (oCharSelect.CharacterID.HasValue)
            {
                Session["CharSkillCharacterID"] = oCharSelect.CharacterID.Value;
                Session["ReloadCharacter"] = "Y";
                if ((oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters) &&
                    (oCharSelect.CharacterInfo.CharacterType != 1))
                {
                    divExcl.Visible = false;
                    Session["CharSkillReadOnly"] = "Y";
                    btnSave.Enabled = false;
                    btnSave.CssClass = "btn-default";
                    btnSave.Style["background-color"] = "grey";
                    lblSpacer.Text = "<br>";
                }
                else
                {
                    divExcl.Visible = true;
                    Session["CharSkillReadOnly"] = "N";
                    btnSave.Enabled = true;
                    btnSave.Style["background-color"] = null;
                    btnSave.CssClass = "StandardButton";
                    lblSpacer.Text = "<br><br>";
                }

                Classes.cUser UserInfo = new Classes.cUser(Session["UserName"].ToString(), "PasswordNotNeeded");
                UserInfo.LastLoggedInCampaign = oCharSelect.CharacterInfo.CampaignID;
                UserInfo.LastLoggedInCharacter = oCharSelect.CharacterID.Value;
                UserInfo.LastLoggedInMyCharOrCamp = (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters ? "M" : "C");
                UserInfo.Save();
            }
        }

        protected void cbxShowExclusions_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShowExclusions.Checked)
                Session["SkillShowExclusions"] = "Y";
            else
                if (Session["SkillShowExclusions"] != null)
                    Session.Remove("SkillShowExclusions");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Session["CharSaveSkills"] = "Y";
        }
    }
}
