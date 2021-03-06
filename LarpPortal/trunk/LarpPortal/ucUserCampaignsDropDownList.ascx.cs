﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace LarpPortal
{
    public partial class ucUserCampaignsDropDownList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlUserCampaigns.SelectedIndex = 0;
                ddlUserCampaigns.Items.Clear();
                string uName = "";
                int uID = 0;
                if (Session["Username"] != null)
                    uName = Session["Username"].ToString();
                if (Session["UserID"] != null)
                    uID = (Session["UserID"].ToString().ToInt32());
                Classes.cUserCampaigns CampaignChoices = new Classes.cUserCampaigns();
                CampaignChoices.Load(uID);
                if (CampaignChoices.CountOfUserCampaigns == 0)
                    Response.Redirect("~/NoCurrentCampaignAssociations.aspx");
                ddlUserCampaigns.DataTextField = "CampaignName";
                ddlUserCampaigns.DataValueField = "CampaignID";
                ddlUserCampaigns.DataSource = CampaignChoices.lsUserCampaigns;
                ddlUserCampaigns.DataBind();

                if (Session["CampaignName"].ToString() != ddlUserCampaigns.SelectedItem.Text.ToString())
                {
                    // Define Player roles here
                    Session["CampaignName"] = ddlUserCampaigns.SelectedItem.Text.ToString();
                    Classes.cPlayerRoles Roles = new Classes.cPlayerRoles();
                    Roles.Load(uID, 0, ddlUserCampaigns.SelectedItem.Value.ToInt32(), DateTime.Today);
                    Session["PlayerRoleString"] = Roles.PlayerRoleString;
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }

        protected void ddlReorderList()
        {

        }

        protected void ddlUserCampaigns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserCampaigns.SelectedValue == "-1")
            {
                Response.Redirect("~/PublicCampaigns.aspx");
            }
            int intUserID;
            string SelectedText;
            string SelectedValue;
            SelectedText = ddlUserCampaigns.SelectedItem.Text.ToString();
            SelectedValue = ddlUserCampaigns.SelectedItem.Value.ToString();
            if (Session["UserID"] == null)
            {
                intUserID = -1;    // In theory we can't actually get here so we should just go back to login
                Response.Redirect("~/index.aspx");
            }
            else
            {
                intUserID = Session["UserID"].ToString().ToInt32();
            }
            Classes.cUser User = new Classes.cUser(Session["Username"].ToString(), "PasswordNotNeeded");
            User.UserID = intUserID;
            User.LastLoggedInCampaign = ddlUserCampaigns.SelectedItem.Value.ToInt32();
            Session["CampaignID"] = ddlUserCampaigns.SelectedItem.Value.ToInt32();
            Session["CampaignName"] = ddlUserCampaigns.SelectedItem.Text.ToString();
            User.SetCharacterForCampaignUser(intUserID, ddlUserCampaigns.SelectedItem.Value.ToInt32());
            Session["SelectedCharacter"] = User.LastLoggedInCharacter;
            User.LastLoggedInMyCharOrCamp = "M";    // 5/27/2017-RPierce - If switching campaign list, assume switching to my characters on character tab
            User.Save();
            // 5/27/2018 - RPierce - Remove Campaign Character session variables
            if (Session["CharacterCampaignCharID"] != null)
                Session.Remove("CharacterCampaignCharID");
            if (Session["CharacterSelectCampaign"] != null)
                Session.Remove("CharacterSelectCampaign");
            if (Session["CharacterSelectGroup"] != null)
                Session.Remove("CharacterSelectGroup");
            if (Session["CharacterSelectID"] != null)
                Session.Remove("CharacterSelectID");
            if (Session["CampaignsToEdit"] != null)
                Session.Remove("CampaignsToEdit");
            if (Session["MyCharacters"] != null)
                Session.Remove("MyCharacters");
            // Go get all roles for that campaign and load them into a session variable
            Classes.cPlayerRoles Roles = new Classes.cPlayerRoles();
            Roles.Load(intUserID, 0, ddlUserCampaigns.SelectedItem.Value.ToInt32(), DateTime.Today);
            Session["PlayerRoleString"] = Roles.PlayerRoleString;
            Classes.cURLPermission permissions = new Classes.cURLPermission();
            bool PagePermission = true;
            string DefaultUnauthorizedURL = "";
            permissions.GetURLPermissions(Request.RawUrl, intUserID, Roles.PlayerRoleString);
            PagePermission = permissions._PagePermission;
            DefaultUnauthorizedURL = permissions._DefaultUnauthorizedURL;
            string ReportCheck = Request.RawUrl.Substring(0, 8);
            if (PagePermission == true)
                if (ReportCheck == "/Reports")
                {
                    Response.Redirect("/Reports/ReportsList.aspx");
                }
                else
                {
                    Response.Redirect(Request.RawUrl);
                }

            else
                Response.Redirect(DefaultUnauthorizedURL);
        }
    }
}