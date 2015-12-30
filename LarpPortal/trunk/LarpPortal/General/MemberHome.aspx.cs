using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace LarpPortal.General
{
    public partial class MemberHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Session["ActiveTopNav"] = "Home";
            //SortedList slParameters = new SortedList();
            //slParameters.Add("@intUserID", Session["UserID"].ToString());
            //DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
            //    "LARPortal", "Character", "MemberHome.Page_Load");
            //ddlCharacter.DataTextField = "CharacterAKA";
            //ddlCharacter.DataValueField = "CharacterID";
            //ddlCharacter.DataSource = dtCharacters;
            //ddlCharacter.DataBind();
            }
        }

        protected void ddlCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlCharacter.SelectedValue == "-1")
            //    Response.Redirect("CharAdd.aspx");

            ////if (Session["SelectedCharacter"].ToString() != ddlCharacter.SelectedValue)
            ////{
            //    Session["SelectedCharacter"] = ddlCharacter.SelectedValue;

            //    // Save the character so it will be the last logged in character.
            //    int iLoggedInChar = 0;
            //    if (int.TryParse(ddlCharacter.SelectedValue, out iLoggedInChar))
            //    {
            //        Classes.cUser UserInfo = new Classes.cUser(Session["UserName"].ToString(), "PasswordNotNeeded");
            //        UserInfo.LastLoggedInCharacter = iLoggedInChar;
            //        UserInfo.Save();
            //    }

            //    Response.Redirect("~/Character/CharSkills.aspx");
            //}
        }

        protected void btnGoToCampaign_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Character/CharSkills.aspx");
        }

        protected void btnGotoCharacterSkills_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Character/CharSkills.aspx");
        }

        protected void btnAddCampaign_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PublicCampaigns.aspx");
        }

        protected void btnRegisterForEvent_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Events/EventRegistration.aspx");
        }

        protected void btnWritePEL_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PELs/PELList.aspx");
        }

        protected void btnCheckPoints_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MemberPointsView.aspx");
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {

        }
    }
}