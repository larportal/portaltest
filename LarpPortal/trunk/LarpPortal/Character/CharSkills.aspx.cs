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

namespace LarpPortal.Character
{
    public partial class CharSkills : System.Web.UI.Page
    {
        public string PictureDirectory = "../Pictures";
        protected DataTable _dtSkills = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["CurrentCharacter"] = "";

                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", "CharacterMaster.Page_Load");
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
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //if (Session["SelectedCharacter"] != null)
            //{
            //    string sCurrent = ViewState["CurrentCharacter"].ToString();
            //    string sSelected = Session["SelectedCharacter"].ToString();
            //    if ((!IsPostBack) || (ViewState["CurrentCharacter"].ToString() != Session["SelectedCharacter"].ToString()))
            //    {
            //        int iCharID;
            //        if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
            //        {
            //            Classes.cCharacter cChar = new Classes.cCharacter();
            //            cChar.LoadCharacter(iCharID);

            //            lblHeader.Text = "Character Skills - " + cChar.AKA + " - " + cChar.CampaignName;
            //        }
            //    }
            //}
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
    