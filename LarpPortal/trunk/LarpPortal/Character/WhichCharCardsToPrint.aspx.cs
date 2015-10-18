using System;
using System.Collections;
using System.Data;

namespace LarpPortal.Character
{
    public partial class WhichCharCardsToPrint : System.Web.UI.Page
    {
        public string PictureDirectory = "../Pictures";

        protected void Page_Load(object sender, EventArgs e)
        {
            ddlCharacterSelector.Attributes.Add("onChange", "return HideTextBox(this);");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CampaignID"] != null)
                {
                    hidCampaignID.Value = Session["CampaignID"].ToString();

                    SortedList sParams = new SortedList();
                    sParams.Add("@CampaignID", Session["CampaignID"].ToString());

                    DataTable dtEvents = Classes.cUtilities.LoadDataTable("uspGetCampaignEvents", sParams, "LARPortal", Session["UserName"].ToString(), "WhichCharCardsToPrint.Page_PreRender.GetEvents");
                    ddlEvents.DataSource = dtEvents;
                    ddlEvents.DataTextField = "EventNameDate";
                    ddlEvents.DataValueField = "EventID";
                    ddlEvents.DataBind();

                    DataTable dtCharacters = Classes.cUtilities.LoadDataTable("uspGetCampaignCharacters", sParams, "LARPortal", Session["UserName"].ToString(), "WhichCharCardsToPrint.Page_PreRender.GetChars");
                    ddlCharacters.DataSource = dtCharacters;
                    ddlCharacters.DataTextField = "CharacterName";
                    ddlCharacters.DataValueField = "CharacterID";
                    ddlCharacters.DataBind();
                }
            }
        }
    }
}
