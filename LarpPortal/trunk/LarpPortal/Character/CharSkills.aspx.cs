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
        LogWriter oLog = new LogWriter();

        protected void Page_Load(object sender, EventArgs e)
        {
            oLog.AddLogMessage("Starting page load for CharSkills", "CharSkills.Page_Load", "", Session.SessionID);
            if (!IsPostBack)
            {
                ViewState["CurrentCharacter"] = "";

                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                oLog.AddLogMessage("Getting Character IDs By UserID", "CharSkills.Page_Load", "", Session.SessionID);
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", "CharacterMaster.Page_Load");
                oLog.AddLogMessage("Done Getting Character IDs By UserID", "CharSkills.Page_Load", "", Session.SessionID);
                ddlCharacterSelector.DataTextField = "CharacterAKA";
                ddlCharacterSelector.DataValueField = "CharacterID";
                ddlCharacterSelector.DataSource = dtCharacters;
                ddlCharacterSelector.DataBind();

                if (ddlCharacterSelector.Items.Count > 0)
                {
                    ddlCharacterSelector.ClearSelection();

                    if (Session["SelectedCharacter"] != null)
                    {
                        DataRow[] drValue = dtCharacters.Select("CharacterID = " + Session["SelectedCharacter"].ToString());
                        foreach (DataRow dRow in drValue)
                        {
                            DateTime DateChanged;
                            if (DateTime.TryParse(dRow["DateChanged"].ToString(), out DateChanged))
                                lblUpdateDate.Text = DateChanged.ToShortDateString();
                            else
                                lblUpdateDate.Text = "Unknown";
                            lblCampaign.Text = dRow["CampaignName"].ToString();
                        }
                        string sCurrentUser = Session["SelectedCharacter"].ToString();
                        foreach (ListItem liAvailableUser in ddlCharacterSelector.Items)
                        {
                            if (sCurrentUser == liAvailableUser.Value)
                                liAvailableUser.Selected = true;
                            else
                                liAvailableUser.Selected = false;
                        }
                    }
                    else
                    {
                        ddlCharacterSelector.Items[0].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    }

                    if (ddlCharacterSelector.SelectedIndex == 0)
                    {
                        ddlCharacterSelector.Items[0].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    }
                    ddlCharacterSelector.Items.Add(new ListItem("Add a new character", "-1"));
                }
                else
                    Response.Redirect("CharAdd.aspx");
            }
            oLog.AddLogMessage("Ending page load for CharSkills", "CharSkills.Page_Load", "", Session.SessionID);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            oLog.AddLogMessage("Done with PreRender", "CharSkills.Page_PreRender", "", Session.SessionID);
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            oLog.AddLogMessage("Closing the page", "CharSkills.Page_Unload", "", Session.SessionID);
        }

        protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCharacterSelector.SelectedValue == "-1")
                Response.Redirect("CharAdd.aspx");

            if (Session["SelectedCharacter"].ToString() != ddlCharacterSelector.SelectedValue)
            {
                Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                Response.Redirect("CharInfo.aspx");
            }
        }
    }
}
    