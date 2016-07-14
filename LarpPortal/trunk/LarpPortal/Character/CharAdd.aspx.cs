using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                tbCharacterName.Attributes.Add("PlaceHolder", "The nickname for the character.");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ddlUserCampaigns.SelectedIndex = 0;
            ddlUserCampaigns.Items.Clear();
            string uName = "";
            int uID = 0;
            if (Session["Username"] != null)
                uName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                Int32.TryParse(Session["UserID"].ToString(), out uID);

            Classes.cUserCampaigns CampaignChoices = new Classes.cUserCampaigns();
            CampaignChoices.Load(uID);

            if (CampaignChoices.CountOfUserCampaigns == 0)
            {
                mvCharacterCreate.SetActiveView(vwNoCampaigns);
            }
            else
            {
                ddlUserCampaigns.DataTextField = "CampaignName";
                ddlUserCampaigns.DataValueField = "CampaignID";
                ddlUserCampaigns.DataSource = CampaignChoices.lsUserCampaigns;
                ddlUserCampaigns.DataBind();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (tbCharacterName.Text.Trim().Length > 0)
            {
                SortedList sParam = new SortedList();
                sParam.Add("@CampaignID", ddlUserCampaigns.SelectedValue);
                sParam.Add("@CharacterAKA", tbCharacterName.Text.Trim());
                sParam.Add("@UserID", Session["UserID"].ToString());
                DataTable dtCharInfo = new DataTable();
                dtCharInfo = Classes.cUtilities.LoadDataTable("prCreateNewCharacter", sParam, "LARPortal", Session["LoginName"].ToString(), lsRoutineName);

                if (dtCharInfo.Rows.Count > 0)
                {
                    string sCharId = dtCharInfo.Rows[0]["CharacterID"].ToString();
                    Session["SelectedCharacter"] = sCharId;
                    Response.Redirect("CharInfo.aspx");
                }
                else
                    btnSave.Text = "Problem Saving the character....";
            }
            else
            {
                lblmodalError.Text = "You must enter the character nick name to save the character.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openError();", true);
            }
        }

        protected void btnCloseError_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeError();", true);
        }
    }
}