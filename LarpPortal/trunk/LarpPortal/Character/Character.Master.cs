using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharacterMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Characters";

            if (!IsPostBack)
            {
                string PageName = Path.GetFileName(Request.PhysicalPath).ToUpper();
                if (PageName.Contains("CHARINFO"))
                    liInfo.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARITEMS"))
                    liItems.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARHISTORY"))
                    liHistory.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARSKILLS"))
                    liSkills.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARREQUESTS"))
                    liRequests.Attributes.Add("class", "active");
                //else if (PageName.Contains("CHARPOINTS"))
                //    liPoints.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARPLACES"))
                    liPlaces.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARREQUESTS"))
                    liPlaces.Attributes.Add("class", "active");

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

                    //if (ddlCharacterSelector.SelectedIndex == 0)
                    //{
                    //    ddlCharacterSelector.Items[0].Selected = true;
                    //    Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    //}
                }
                ddlCharacterSelector.Items.Add(new ListItem("Add a new character", "-1"));
                if (PageName.Contains("CHARADD"))
                {
                    ddlCharacterSelector.SelectedIndex = -1;
                    foreach (ListItem li in ddlCharacterSelector.Items)
                    {
                        if (li.Value == "-1")
                            li.Selected = true;
                        else
                            li.Selected = false;
                    }
                }
                else
                if (dtCharacters.Rows.Count == 0)
                    Response.Redirect("CharAdd.aspx");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            string s = width.Value;
            if (ddlCharacterSelector.SelectedValue != "")
            {
                string t = ddlCharacterSelector.SelectedValue;
            }
            string j = height.Value;
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
