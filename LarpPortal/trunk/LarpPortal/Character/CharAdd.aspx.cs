using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

            ddlUserCampaigns.DataTextField = "CampaignName";
            ddlUserCampaigns.DataValueField = "CampaignID";
            ddlUserCampaigns.DataSource = CampaignChoices.lsUserCampaigns;
            ddlUserCampaigns.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            /// Todo JLB - Add creating the character.
        }
    }
}