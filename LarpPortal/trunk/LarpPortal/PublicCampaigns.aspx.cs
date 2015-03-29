﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace LarpPortal
{
    public partial class PublicCampaigns1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Campaigns";
            Session["ActiveLeftNav"] = "PublicCampaigns";
            
            int UserID = 0;
            if(!IsPostBack)
            {
                Session["DefaultCampaignLogoPath"] = "img/logo/";
                Session["DefaultCampaignLogoImage"] = "http://placehold.it/820x130";
                pnlOverview.Visible = false;
                pnlSelectors.Visible = false;
                hplLinkToSite.Visible = false;
                imgCampaignImage.Visible = false;
                pnlSignUpForCampaign.Visible = false;
                if (Session["UserID"] == null)
                    UserID = 0;
                else
                    UserID = ((int) Session["UserID"]);
                // Populate ddlGameSystem
                ddlGameSystem.SelectedIndex = 0;
                ddlGameSystem.Items.Clear();
                //TODO-Rick-00 Create ReloadtvGameSystem and call it here
                ReloadtvGameSystem(UserID);
                // Populate ddlCampaign
                ddlCampaign.SelectedIndex = 0;
                ddlCampaign.Items.Clear();
                // Populate ddlGenre
                ddlGenre.SelectedIndex = 0;
                ddlGenre.Items.Clear();
                // Populate ddlStyle
                ddlStyle.SelectedIndex = 0;
                ddlStyle.Items.Clear();
                // Populate ddlTechLevel
                ddlTechLevel.SelectedIndex = 0;
                ddlTechLevel.Items.Clear();
                // Populate ddlSize
                ddlSize.SelectedIndex = 0;
                ddlSize.Items.Clear();
                // Populate ddlMileRadius
                ddlMileRadius.SelectedIndex = 0;
                ddlMileRadius.Items.Clear();
            }
        }

        protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make all tree views invisible and show the one that's poplulating
            tvGameSystem.Visible = false;
            tvCampaign.Visible = false;
            tvGenre.Visible = false;
            tvStyle.Visible = false;
            tvTechLevel.Visible = false;
            tvSize.Visible = false;
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            DateTime edt = DateTime.Now;
            string EndDate;
            if (chkEndedCampaigns.Checked  == true)
                EndDate = "";
            else
                EndDate = edt.ToShortDateString();
            Session["GameSystemFilter"] = 0;
            if (chkGameSystem.Checked == true)
                Session["GameSystemFilter"] = ddlGameSystem.SelectedValue.ToInt32();
            else
                Session["GameSystemFilter"] = 0;
            Session["CampaignFilter"] = 0;
            if (chkCampaign.Checked == true)
                Session["CampaignFilter"] = ddlCampaign.SelectedValue.ToInt32();
            else
                Session["CampaignFilter"] = 0;
            Session["GenreFilter"] = 0;
            if (chkGenre.Checked == true)
                Session["GenreFilter"] = ddlGenre.SelectedValue.ToInt32();
            else
                Session["GenreFilter"] = 0;
            Session["StyleFilter"] = 0;
            if (chkStyle.Checked == true)
                Session["StyleFilter"] = ddlStyle.SelectedValue.ToInt32();
            else
                Session["StyleFilter"] = 0;
            Session["TechLevelFilter"] = 0;
            if (chkTechLevel.Checked == true)
                Session["TechLevelFilter"] = ddlTechLevel.SelectedValue.ToInt32();
            else
                Session["TechLevelFilter"] = 0;
            Session["SizeFilter"] = 0;
            if (chkSize.Checked == true)
                Session["SizeFilter"] = ddlSize.SelectedValue.ToInt32();
            else
                Session["SizeFilter"] = 0;
            Session["ZipCodeFilter"] = "";
            Session["RadiusFilter"] = 0;
            if (chkZipCode.Checked == true)
            {
                Session["ZipCodeFilter"] = txtZipCode.Text;
                Session["RadiusFilter"] = ddlMileRadius.SelectedValue.ToInt32();
            }
            else
            {
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
            }

            switch (ddlOrderBy.Text)
            {
                case "Game System":
                    tvGameSystem.Visible = true;
                    break;
                case "Campaign":
                    tvCampaign.Visible = true;
                    break;
                case "Genre":
                    //TODO
                    tvGenre.Visible = true;
                    break;
                case "Style":
                    //TODO
                    tvStyle.Visible = true;
                    break;
                case "Tech Level":
                    //TODO
                    tvTechLevel.Visible = true;
                    break;
                case "Size":
                    //TODO
                    tvSize.Visible = true;
                    break;
                default:
                    break;
                    
            }
            ReloadActiveTreeView(UserID);
        }

        protected void MakeDetailsVisible(int URLVisible, int ImageVisible, int imgHeight, int imgWidth, int OverviewVisible, int SelectorsVisible, int CampaignID)
        {
            if (URLVisible == 1)
                hplLinkToSite.Visible = true;
            else
                hplLinkToSite.Visible = false;
            if (ImageVisible == 1)
            {
                // Max dimensions are 820 x 130
                if (imgHeight == 0)
                    imgHeight = 130;
                if (imgWidth == 0)
                    imgWidth = 820;
                decimal imgRatio;
                imgRatio = decimal.Divide(imgWidth, imgHeight);
                if (imgRatio == 0)
                    imgRatio = 1;

                int CalculatedHeight = Convert.ToInt32(Math.Round(820 / imgRatio,0));
                int CalculatedWidth = Convert.ToInt32(Math.Round(130 * imgRatio,0));

                if (imgRatio == 6.308m)
                {
                    imgCampaignImage.Height = 130;
                    imgCampaignImage.Width = 820;
                }
                if (imgRatio > 6.308m)
                {
                    imgCampaignImage.Height = CalculatedHeight;
                    imgCampaignImage.Width = 820;
                }
                if (imgRatio < 6.308m)
                {
                    imgCampaignImage.Height = 130;
                    imgCampaignImage.Width = CalculatedWidth;
                }
                imgCampaignImage.Visible = true;
            }
            else
                imgCampaignImage.Visible = false;
            if (OverviewVisible == 1)
                pnlOverview.Visible = true;
            else
                pnlOverview.Visible = false;
            if (SelectorsVisible == 1)
            {
                pnlSelectors.Visible = true;
                if ( ((int)Session["SecurityRole"]) == 10)
                    MakePanelSignUpVisible(CampaignID);
                else
                    pnlSignUpForCampaign.Visible = false;
            }
            else
            {
                pnlSelectors.Visible = false;
                pnlSignUpForCampaign.Visible = false;
            }
        }

        protected void MakePanelSignUpVisible(int CampaignID)
        {
            // Determine current roles for this campaign.
            int UserID = 0;
            string IsPC = "false";
            string IsNPC = "false";
            if (Session["UserID"].ToString().ToInt32() != 0)
                UserID = Session["UserID"].ToString().ToInt32();
            if (UserID > 0)
            {
                Classes.cPlayerRole Role = new Classes.cPlayerRole();
                Role.Load(UserID, 0, CampaignID);
                IsPC = Role.IsPC;
                IsNPC = Role.IsNPC;
                btnSignUp.Items.Clear();
                // None - Show all three choices
                if (IsPC == "false" && IsNPC == "false")
                {
                    btnSignUp.Items.Add(new ListItem("PC", "1"));
                    btnSignUp.Items.Add(new ListItem("NPC", "2"));
                    btnSignUp.Items.Add(new ListItem("Both", "3"));
                }
                // PC Only - Show NPC
                if (IsPC == "true" && IsNPC == "false")
                {
                    btnSignUp.Items.Add(new ListItem("NPC", "2"));
                }
                // NPC Only - Show PC
                if (IsPC == "false" && IsNPC == "true")
                {
                    btnSignUp.Items.Add(new ListItem("PC", "1"));
                }
                // Both - Don't show panel
                if (IsPC == "true"  && IsNPC == "true")
                {
                    pnlSignUpForCampaign.Visible = true;
                    btnSignUpForCampaign.Visible = false;
                }
                else
                {
                    pnlSignUpForCampaign.Visible = true;
                    btnSignUpForCampaign.Visible = true;
                }
                // Now get current roles.  If there are any start the label with "Current Roles:<br>"
                Classes.cPlayerRoles Roles = new Classes.cPlayerRoles();
                Roles.Load(UserID, 0, CampaignID, DateTime.Now); // Last parameter = 1 for Yes, Current roles only
                //Convert list to datatable
                listCurrentRoles.DataSource = Classes.cUtilities.CreateDataTable(Roles.lsPlayerRoles);
                listCurrentRoles.DataBind();
            }
        }

        protected void SetSiteLink(string strURL, string strGameName)
        {
            hplLinkToSite.NavigateUrl = strURL;
            hplLinkToSite.Text = "Visit " + strGameName + " home page.";

        }

        protected void SetSiteImage(string strImage)
        {
            string DefaultPath = "";
            string DefaultImage = "";
            if (Session["DefaultCampaignLogoImage"]  == null)
                DefaultImage = "";
            else
                DefaultImage = Session["DefaultCampaignLogoImage"].ToString();
            if (Session["DefaultCampaignLogoPath"]  == null)
                DefaultPath = "";
            else
                DefaultPath = Session["DefaultCampaignLogoPath"].ToString();
            if (strImage == "")
                imgCampaignImage.ImageUrl = DefaultImage;
            else
                imgCampaignImage.ImageUrl = DefaultPath + strImage;
        }

        protected void tvGameSystem_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strURL = "";
            string strImage = "";
            int intImageHeight = 0;
            int intImageWidth = 0;
            string strGameOrCampaignName = "";
            tvGameSystem.SelectedNode.Selected = true;
            string SelectedGorC = tvGameSystem.SelectedValue + "";
            string GorC = SelectedGorC.Substring(0, 1);
            string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
            int GameSystemID;
            int CampaignID;
            int intURLVisible = 1;
            int intImageVisible = 1;
            int intOverviewVisible = 1;
            int intSelectorsVisible = 1;
            int.TryParse(stGameSystemID, out GameSystemID);
            int.TryParse(tvGameSystem.SelectedValue, out CampaignID);
            if (GorC == "G") // Game System
            {
                intSelectorsVisible = 0;
                lblGorC1.Text = "Game System";
                lblGorC2.Text = "Game System Description";
                Classes.cGameSystem GS = new Classes.cGameSystem();
                GS.Load(GameSystemID, 0);
                strURL = GS.GameSystemURL;
                strImage = "";
                strGameOrCampaignName = GS.GameSystemName;
                lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
            }
            else  // Campaign
            {
                if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                {
                    intURLVisible = 0;
                    intImageVisible = 0;
                    intOverviewVisible = 0;
                    intSelectorsVisible = 0;
                }
                else
                {
                    lblGorC1.Text = "Campaign";
                    lblGorC2.Text = "Campaign Description";
                    Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                    strURL = Cam.URL;
                    strImage = Cam.Logo;
                    intImageHeight = Cam.LogoHeight;
                    intImageWidth = Cam.LogoWidth;
                    strGameOrCampaignName = Cam.CampaignName;
                    lblCampaignOverview.Text = Cam.WebPageDescription;
                    lblGameSystem1.Text = "Game System: ";
                    lblGameSystem2.Text = " " + Cam.GameSystemName;
                    lblGenre1.Text = "Genre: " ;
                    lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                    lblStyle1.Text = "Style: ";
                    lblStyle2.Text = " " + Cam.StyleDescription;
                    lblTechLevel1.Text = "Tech Level: ";
                    lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                    lblSize1.Text = "Size:";
                    lblSize2.Text = " " + Cam.CampaignSizeRange;    
                    //TODO-Rick-Add location info on the screen and logic it in
                }
            }
            SetSiteImage(strImage);
            if(strURL != null)
                SetSiteLink(strURL, strGameOrCampaignName);
            MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
        }

        protected void tvCampaign_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strURL = "";
            string strImage = "";
            int intImageHeight = 130;
            int intImageWidth = 820;
            string strGameOrCampaignName = "";
            tvCampaign.SelectedNode.Selected = true;
            string SelectedGorC = tvCampaign.SelectedValue + "";
            string GorC = SelectedGorC.Substring(0, 1);
            string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
            int GameSystemID;
            int CampaignID = tvCampaign.SelectedValue.ToString().ToInt32();
            int intURLVisible = 1;
            int intImageVisible = 1;
            int intOverviewVisible = 1;
            int intSelectorsVisible = 1;
            int.TryParse(stGameSystemID, out GameSystemID);
            if (GorC == "G") // Game System - We shouldn't have any of these in this treeview
            {
                intSelectorsVisible = 0;
                lblGorC1.Text = "Game System";
                lblGorC2.Text = "Game System Description";
                Classes.cGameSystem GS = new Classes.cGameSystem();
                GS.Load(GameSystemID, 0);
                strURL = GS.GameSystemURL;
                strImage = "";
                strGameOrCampaignName = GS.GameSystemName;
                lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
            }
            else  // Campaign
            {
                if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                {
                    intURLVisible = 0;
                    intImageVisible = 0;
                    intOverviewVisible = 0;
                    intSelectorsVisible = 0;
                }
                else   // for this treeview we really only want this line of logic
                {
                    lblGorC1.Text = "Campaign";
                    lblGorC2.Text = "Campaign Description";
                    Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                    strURL = Cam.URL;
                    strImage = Cam.Logo;
                    intImageWidth = Cam.LogoWidth;
                    intImageHeight = Cam.LogoHeight;
                    strGameOrCampaignName = Cam.CampaignName;
                    lblCampaignOverview.Text = Cam.WebPageDescription;
                    lblGameSystem1.Text = "Game System: ";
                    lblGameSystem2.Text = " " + Cam.GameSystemName;
                    lblGenre1.Text = "Genre: ";
                    lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                    lblStyle1.Text = "Style: ";
                    lblStyle2.Text = " " + Cam.StyleDescription;
                    lblTechLevel1.Text = "Tech Level: ";
                    lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                    lblSize1.Text = "Size:";
                    lblSize2.Text = " " + Cam.CampaignSizeRange;
                }
            }
            SetSiteImage(strImage);
            if (strURL != null)
                SetSiteLink(strURL, strGameOrCampaignName);
            MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
        }

        protected void tvGenre_SelectedNodeChanged(object sender, EventArgs e)
        {
            {
                string strURL = "";
                string strImage = "";
                int intImageHeight = 130;
                int intImageWidth = 820;
                string strGameOrCampaignName = "";
                tvGenre.SelectedNode.Selected = true;
                string SelectedGorC = tvGenre.SelectedValue + "";
                string GorC = SelectedGorC.Substring(0, 1);
                string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
                int GameSystemID;
                int CampaignID;
                int intURLVisible = 1;
                int intImageVisible = 1;
                int intOverviewVisible = 1;
                int intSelectorsVisible = 1;
                int.TryParse(stGameSystemID, out GameSystemID);
                int.TryParse(tvGenre.SelectedValue, out CampaignID);
                if (GorC == "G") // Game System
                {
                    intSelectorsVisible = 0;
                    intURLVisible = 0;
                    intImageVisible = 0;
                    lblGorC1.Text = "Genre";
                    lblGorC2.Text = "Genre Description";
                    Classes.cGameSystem GS = new Classes.cGameSystem();
                    GS.Load(GameSystemID, 0);
                    strURL = GS.GameSystemURL;
                    strImage = "";
                    strGameOrCampaignName = GS.GameSystemName;
                    lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
                }
                else  // Campaign
                {
                    if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                    {
                        intURLVisible = 0;
                        intImageVisible = 0;
                        intOverviewVisible = 0;
                        intSelectorsVisible = 0;
                    }
                    else
                    {
                        lblGorC1.Text = "Campaign";
                        lblGorC2.Text = "Campaign Description";
                        Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                        strURL = Cam.URL;
                        strImage = Cam.Logo;
                        intImageHeight = Cam.LogoHeight;
                        intImageWidth = Cam.LogoWidth;
                        strGameOrCampaignName = Cam.CampaignName;
                        lblCampaignOverview.Text = Cam.WebPageDescription;
                        lblGameSystem1.Text = "Game System: ";
                        lblGameSystem2.Text = " " + Cam.GameSystemName;
                        lblGenre1.Text = "Genre: ";
                        lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                        lblStyle1.Text = "Style: ";
                        lblStyle2.Text = " " + Cam.StyleDescription;
                        lblTechLevel1.Text = "Tech Level: ";
                        lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                        lblSize1.Text = "Size:";
                        lblSize2.Text = " " + Cam.CampaignSizeRange;
                    }
                }
                SetSiteImage(strImage);
                if (strURL != null)
                    SetSiteLink(strURL, strGameOrCampaignName);
                MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
            }
        }

        protected void tvStyle_SelectedNodeChanged(object sender, EventArgs e)
        {
            {
                string strURL = "";
                string strImage = "";
                int intImageHeight = 130;
                int intImageWidth = 820;
                string strGameOrCampaignName = "";
                tvStyle.SelectedNode.Selected = true;
                string SelectedGorC = tvStyle.SelectedValue + "";
                string GorC = SelectedGorC.Substring(0, 1);
                string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
                int GameSystemID;
                int CampaignID;
                int intURLVisible = 1;
                int intImageVisible = 1;
                int intOverviewVisible = 1;
                int intSelectorsVisible = 1;
                int.TryParse(stGameSystemID, out GameSystemID);
                int.TryParse(tvStyle.SelectedValue, out CampaignID);
                if (GorC == "G") // Game System
                {
                    intSelectorsVisible = 0;
                    intURLVisible = 0;
                    intImageVisible = 0;
                    lblGorC1.Text = "Style";
                    lblGorC2.Text = "Style Description";
                    Classes.cGameSystem GS = new Classes.cGameSystem();
                    GS.Load(GameSystemID, 0);
                    strURL = GS.GameSystemURL;
                    strImage = "";
                    strGameOrCampaignName = GS.GameSystemName;
                    lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
                }
                else  // Campaign
                {
                    if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                    {
                        intURLVisible = 0;
                        intImageVisible = 0;
                        intOverviewVisible = 0;
                        intSelectorsVisible = 0;
                    }
                    else
                    {
                        lblGorC1.Text = "Campaign";
                        lblGorC2.Text = "Campaign Description";
                        Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                        strURL = Cam.URL;
                        strImage = Cam.Logo;
                        intImageHeight = Cam.LogoHeight;
                        intImageWidth = Cam.LogoWidth;
                        strGameOrCampaignName = Cam.CampaignName;
                        lblCampaignOverview.Text = Cam.WebPageDescription;
                        lblGameSystem1.Text = "Game System: ";
                        lblGameSystem2.Text = " " + Cam.GameSystemName;
                        lblGenre1.Text = "Genre: ";
                        lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                        lblStyle1.Text = "Style: ";
                        lblStyle2.Text = " " + Cam.StyleDescription;
                        lblTechLevel1.Text = "Tech Level: ";
                        lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                        lblSize1.Text = "Size:";
                        lblSize2.Text = " " + Cam.CampaignSizeRange;
                    }
                }
                SetSiteImage(strImage);
                if (strURL != null)
                    SetSiteLink(strURL, strGameOrCampaignName);
                MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
            }
        }

        protected void tvTechLevel_SelectedNodeChanged(object sender, EventArgs e)
        {
            {
                string strURL = "";
                string strImage = "";
                int intImageHeight = 130;
                int intImageWidth = 820;
                string strGameOrCampaignName = "";
                tvTechLevel.SelectedNode.Selected = true;
                string SelectedGorC = tvTechLevel.SelectedValue + "";
                string GorC = SelectedGorC.Substring(0, 1);
                string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
                int GameSystemID;
                int CampaignID;
                int intURLVisible = 1;
                int intImageVisible = 1;
                int intOverviewVisible = 1;
                int intSelectorsVisible = 1;
                int.TryParse(stGameSystemID, out GameSystemID);
                int.TryParse(tvTechLevel.SelectedValue, out CampaignID);
                if (GorC == "G") // Game System
                {
                    intSelectorsVisible = 0;
                    intURLVisible = 0;
                    intImageVisible = 0;
                    lblGorC1.Text = "Genre";
                    lblGorC2.Text = "Genre Description";
                    Classes.cGameSystem GS = new Classes.cGameSystem();
                    GS.Load(GameSystemID, 0);
                    strURL = GS.GameSystemURL;
                    strImage = "";
                    strGameOrCampaignName = GS.GameSystemName;
                    lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
                }
                else  // Campaign
                {
                    if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                    {
                        intURLVisible = 0;
                        intImageVisible = 0;
                        intOverviewVisible = 0;
                        intSelectorsVisible = 0;
                    }
                    else
                    {
                        lblGorC1.Text = "Campaign";
                        lblGorC2.Text = "Campaign Description";
                        Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                        strURL = Cam.URL;
                        strImage = Cam.Logo;
                        intImageHeight = Cam.LogoHeight;
                        intImageWidth = Cam.LogoWidth;
                        strGameOrCampaignName = Cam.CampaignName;
                        lblCampaignOverview.Text = Cam.WebPageDescription;
                        lblGameSystem1.Text = "Game System: ";
                        lblGameSystem2.Text = " " + Cam.GameSystemName;
                        lblGenre1.Text = "Genre: ";
                        lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                        lblStyle1.Text = "Style: ";
                        lblStyle2.Text = " " + Cam.StyleDescription;
                        lblTechLevel1.Text = "Tech Level: ";
                        lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                        lblSize1.Text = "Size:";
                        lblSize2.Text = " " + Cam.CampaignSizeRange;
                        lblLocation2.Text = "<i>Location</i>";
                        lblEvent2.Text = "<i>Next Event Date</i>";
                    }
                }
                SetSiteImage(strImage);
                if (strURL != null)
                    SetSiteLink(strURL, strGameOrCampaignName);
                MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
            }
        }

        protected void tvSize_SelectedNodeChanged(object sender, EventArgs e)
        {
            {
                string strURL = "";
                string strImage = "";
                int intImageHeight = 130;
                int intImageWidth = 820;
                string strGameOrCampaignName = "";
                tvSize.SelectedNode.Selected = true;
                string SelectedGorC = tvSize.SelectedValue + "";
                string GorC = SelectedGorC.Substring(0, 1);
                string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
                int GameSystemID;
                int CampaignID;
                int intURLVisible = 1;
                int intImageVisible = 1;
                int intOverviewVisible = 1;
                int intSelectorsVisible = 1;
                int.TryParse(stGameSystemID, out GameSystemID);
                int.TryParse(tvSize.SelectedValue, out CampaignID);
                if (GorC == "G") // Game System
                {
                    intSelectorsVisible = 0;
                    intURLVisible = 0;
                    intImageVisible = 0;
                    lblGorC1.Text = "Genre";
                    lblGorC2.Text = "Genre Description";
                    Classes.cGameSystem GS = new Classes.cGameSystem();
                    GS.Load(GameSystemID, 0);
                    strURL = GS.GameSystemURL;
                    strImage = "";
                    strGameOrCampaignName = GS.GameSystemName;
                    lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
                }
                else  // Campaign
                {
                    if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                    {
                        intURLVisible = 0;
                        intImageVisible = 0;
                        intOverviewVisible = 0;
                        intSelectorsVisible = 0;
                    }
                    else
                    {
                        lblGorC1.Text = "Campaign";
                        lblGorC2.Text = "Campaign Description";
                        Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                        strURL = Cam.URL;
                        strImage = Cam.Logo;
                        intImageHeight = Cam.LogoHeight;
                        intImageWidth = Cam.LogoWidth;
                        strGameOrCampaignName = Cam.CampaignName;
                        lblCampaignOverview.Text = Cam.WebPageDescription;
                        lblGameSystem1.Text = "Game System: ";
                        lblGameSystem2.Text = " " + Cam.GameSystemName;
                        lblGenre1.Text = "Genre: ";
                        lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                        lblStyle1.Text = "Style: ";
                        lblStyle2.Text = " " + Cam.StyleDescription;
                        lblTechLevel1.Text = "Tech Level: ";
                        lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                        lblSize1.Text = "Size:";
                        lblSize2.Text = " " + Cam.CampaignSizeRange;
                    }
                }
                SetSiteImage(strImage);
                if (strURL != null)
                    SetSiteLink(strURL, strGameOrCampaignName);
                MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
            }
        }

        protected void chkGameSystem_CheckedChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            DateTime edt = DateTime.Now;
            if (chkEndedCampaigns.Checked == true)
                Session["EndDate"] = "";
            else
                Session["EndDate"] = edt.ToShortDateString();
            Session["GameSystemFilter"] = 0;
            if (chkGameSystem.Checked == false)
            {
                Session["GameSystemFilter"] = 0;
                ReloadActiveTreeView(UserID);
            }
            if (chkCampaign.Checked == true)
                Session["CampaignFilter"] = ddlCampaign.SelectedValue.ToInt32();
            else
                Session["CampaignFilter"] = 0;
            if (chkGenre.Checked == true)
                Session["GenreFilter"] = ddlGenre.SelectedValue.ToInt32();
            else
                Session["GenreFilter"] = 0;
            if (chkStyle.Checked == true)
                Session["StyleFilter"] = ddlStyle.SelectedValue.ToInt32();
            else
                Session["StyleFilter"] = 0;
            if (chkTechLevel.Checked == true)
                Session["TechLevelFilter"] = ddlTechLevel.SelectedValue.ToInt32();
            else
                Session["TechLevelFilter"] = 0;
            if (chkSize.Checked == true)
                Session["SizeFilter"] = ddlSize.SelectedValue.ToInt32();
            else
                Session["SizeFilter"] = 0;
            if (chkZipCode.Checked == true)
            {
                Session["ZipCodeFilter"] = txtZipCode.Text;
                Session["RadiusFilter"] = ddlMileRadius.SelectedValue.ToInt32();
            }
            else
            {
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
            }
            if (chkGameSystem.Checked == true)
            {
                LoadddlGameSystem(UserID);
                ddlGameSystem.Visible = true;
            }
            else
            {
                Session["GameSystemFilter"] = 0;
                ddlGameSystem.Visible = false;
            }
        }

        protected void ddlGameSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkGameSystem.Checked == true)
                Session["GameSystemFilter"] = ddlGameSystem.SelectedValue;
            else
                Session["GameSystemFilter"] = 0;
            ReloadActiveTreeView(UserID);
        }

        protected void chkCampaign_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["CampaignFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkCampaign.Checked == true)
            {
                ddlCampaign.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["SizeFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlCampaign(UserID);
            }
            else
            {
                ddlCampaign.Visible = false;
            }

        }

        protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkCampaign.Checked == true)
                Session["CampaignFilter"] = ddlGameSystem.SelectedValue;
            else
                Session["CampaignFilter"] = 0;
            ReloadActiveTreeView(UserID);
        }

        protected void chkGenre_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["GenreFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkGenre.Checked == true)
            {
                ddlGenre.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["SizeFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlGenre(UserID);
            }
            else
            {
                ddlGenre.Visible = false;
            }   
        }

        protected void ddlGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set Session["filterGenre"] to GenreID and reload tv
        }

        protected void chkStyle_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["StyleFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkStyle.Checked == true)
            {
                ddlStyle.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["SizeFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlStyle(UserID);
            }
            else
            {
                ddlStyle.Visible = false;
                //Session["filterStyle"] = 0;
            }    
        }

        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set Session["filterStyle"] to StyleID and reload tv
        }

        protected void chkTechLevel_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["TechLevelFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkTechLevel.Checked == true)
            {
                ddlTechLevel.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["SizeFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlTechLevel(UserID);
            }
            else
            {
                ddlTechLevel.Visible = false;
                //Session["filterTechLevel"] = 0;
            }     
        }

        protected void ddlTechLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set Session["filterTechLevel"] to TechLevelID and reload tv
        }

        protected void chkSize_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["SizeFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkSize.Checked == true)
            {
                ddlSize.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlSize(UserID);
            }
            else
            {
                ddlSize.Visible = false;
                //Session["filterSize"] = 0;
            }    
        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set Session["filterSize"] to SizeID and reload tv
        }

        protected void chkZipCode_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["RadiusFilter"] = 0;
            Session["ZipCode"] = "";
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkZipCode.Checked == true)
            {
                txtZipCode.Text = "";
                txtZipCode.Attributes.Add("Placeholder", "Enter your zip code");
                txtZipCode.Visible = true;
                ddlMileRadius.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["SizeFilter"] = 0;
                LoadddlMileRadius(UserID);
            }
            else
            {
                txtZipCode.Text = "";
                txtZipCode.Visible = false;
                ddlMileRadius.Visible = false;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                ReloadActiveTreeView(UserID);
            }
        }

        protected void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (txtZipCode.Text == "")
            {
                txtZipCode.Focus();
                Session["ZipCodeFilter"] = "";
            }
            else
            {
                Session["ZipCodeFilter"] = txtZipCode.Text;
                if (ddlMileRadius.SelectedValue.ToString() != "")
                    ReloadActiveTreeView(UserID);
                else
                    ddlMileRadius.Focus();
            }
        }

        protected void ddlMileRadius_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            Session["RadiusFilter"] = ddlMileRadius.SelectedValue;
            if (txtZipCode.Text != "")
                ReloadActiveTreeView(UserID);
            else
                txtZipCode.Focus();
        }

        protected void chkEndedCampaigns_CheckedChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            ReloadActiveTreeView(UserID);
        }

        protected void ReloadActiveTreeView(int UserID)
        {
            if (tvGameSystem.Visible == true)
                ReloadtvGameSystem(UserID);
            if (tvCampaign.Visible == true)
                ReloadtvCampaign(UserID);
            if (tvGenre.Visible == true)
                ReloadtvGenre(UserID);
            if (tvStyle.Visible == true)
                ReloadtvStyle(UserID);
            if (tvTechLevel.Visible == true)
                ReloadtvTechLevel(UserID);
            if (tvSize.Visible == true)
                ReloadtvSize(UserID);
        }

        protected void LoadddlGameSystem(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0; 
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection GameSystem = new Classes.cCampaignSelection();
            ddlGameSystem.DataSource = GameSystem.LoadGameSystems(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlGameSystem.DataTextField = "GameSystemName";
            ddlGameSystem.DataValueField = "GameSystemID";
            ddlGameSystem.DataBind();
            ddlGameSystem.Items.Insert(0, new ListItem("Select a Game System", ""));
        }

        protected void LoadddlCampaign(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Campaign = new Classes.cCampaignSelection();
            ddlCampaign.DataSource = Campaign.LoadCampaigns(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlCampaign.DataTextField = "CampaignName";
            ddlCampaign.DataValueField = "CampaignID";
            ddlCampaign.DataBind();
            ddlCampaign.Items.Insert(0, new ListItem("Select a Campaign", ""));
        }

        protected void LoadddlGenre(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Genre = new Classes.cCampaignSelection();
            ddlGenre.DataSource = Genre.LoadGenres(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlGenre.DataTextField = "GenreName";
            ddlGenre.DataValueField = "GenreID";
            ddlGenre.DataBind();
            ddlGenre.Items.Insert(0, new ListItem("Select a Genre", ""));
        }

        protected void LoadddlStyle(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Style = new Classes.cCampaignSelection();
            ddlStyle.DataSource = Style.LoadStyles(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlStyle.DataTextField = "StyleName";
            ddlStyle.DataValueField = "StyleID";
            ddlStyle.DataBind();
            ddlStyle.Items.Insert(0, new ListItem("Select a Style", ""));
        }

        protected void LoadddlTechLevel(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection TechLevel = new Classes.cCampaignSelection();
            ddlTechLevel.DataSource = TechLevel.LoadTechLevels(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlTechLevel.DataTextField = "TechLevelName";
            ddlTechLevel.DataValueField = "TechLevelID";
            ddlTechLevel.DataBind();
            ddlTechLevel.Items.Insert(0, new ListItem("Select a Tech Level", ""));
        }

        protected void LoadddlSize(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Size = new Classes.cCampaignSelection();
            ddlSize.DataSource = Size.LoadSizes(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlSize.DataTextField = "CampaignSizeRange";
            //ddlSize.DataValueField = "CampaignSizeID";
            ddlSize.DataBind();
            ddlSize.Items.Insert(0, new ListItem("Select a Size", ""));
        }

        protected void LoadddlMileRadius(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Radius = new Classes.cCampaignSelection();
            ddlMileRadius.DataSource = Radius.LoadRadius(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlMileRadius.DataTextField = "DistanceDescription";
            ddlMileRadius.DataValueField = "DistanceID";
            ddlMileRadius.DataBind();
            ddlMileRadius.Items.Insert(0, new ListItem("Select a Maximum Distance", ""));
        }

        protected void ReloadtvGameSystem(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string GameSystemName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode GameSystemNode;
            TreeNode CampaignNode;
            tvGameSystem.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection GameSystems = new Classes.cCampaignSelection();
            DataTable dtGameSystems = new DataTable();
            dtGameSystems = GameSystems.LoadGameSystems(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtGameSystems.Rows)
            {
                if (int.TryParse(dRow["GameSystemID"].ToString(), out iTemp))
                    GameSystemFilter = iTemp;
                GameSystemName = dRow["GameSystemName"].ToString();
                GameSystemNode = new TreeNode(GameSystemName, "G" + GameSystemFilter); // G will be assigned all Game Systems and C will be assigned to Campaigns
                GameSystemNode.Selected = false;
                GameSystemNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByGameSystem(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    GameSystemNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                GameSystemNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }    
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            GameSystemNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        GameSystemNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvGameSystem.Nodes.Add(GameSystemNode);
            }
        }

        protected void ReloadtvCampaign(int UserID)
        {
            int StatusID = 0;
            string CampaignName = "";
            int iTemp;
            TreeNode CampaignNode;
            tvCampaign.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
            DataTable dtCampaigns = new DataTable();
            dtCampaigns = Campaigns.LoadCampaigns(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            if (dtCampaigns.Rows.Count > 0)
            {
                foreach (DataRow dRow in dtCampaigns.Rows)
                {
                    if (int.TryParse(dRow["CampaignID"].ToString(), out iTemp))
                        CampaignFilter = iTemp;
                    if (int.TryParse(dRow["StatusID"].ToString(), out iTemp))
                        StatusID = iTemp;
                    CampaignName = dRow["CampaignName"].ToString();
                    if (StatusID != 4)
                    {
                        CampaignNode = new TreeNode(CampaignName, CampaignFilter.ToString());
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                        tvCampaign.Nodes.Add(CampaignNode);
                    }
                    else
                    {
                        if (chkEndedCampaigns.Checked == true)
                        {
                            CampaignName = CampaignName + " (Ended)";
                            CampaignNode = new TreeNode(CampaignName, CampaignFilter.ToString());
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                            tvCampaign.Nodes.Add(CampaignNode);
                        }
                    }
                }
            }
            else
            {
                CampaignNode = new TreeNode("No Campaigns Available", "0");
                CampaignNode.Selected = false;
                CampaignNode.NavigateUrl = "";
                tvCampaign.Nodes.Add(CampaignNode);
            }
        }

        protected void ReloadtvGenre(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string GenreName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode GenreNode;
            TreeNode CampaignNode;
            tvGenre.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Genres = new Classes.cCampaignSelection();
            DataTable dtGenres = new DataTable();
            dtGenres = Genres.LoadGenres(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtGenres.Rows)
            {
                if (int.TryParse(dRow["GenreID"].ToString(), out iTemp))
                    GenreFilter = iTemp;
                GenreName = dRow["GenreName"].ToString();
                GenreNode = new TreeNode(GenreName, "G" + GenreFilter); // G will be assigned all Genres and C will be assigned to Campaigns
                GenreNode.Selected = false;
                GenreNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByGenre(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    GenreNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                GenreNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }    
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            GenreNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        GenreNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvGenre.Nodes.Add(GenreNode);
            }
        }

        protected void ReloadtvStyle(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string StyleName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode StyleNode;
            TreeNode CampaignNode;
            tvStyle.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Styles = new Classes.cCampaignSelection();
            DataTable dtStyles = new DataTable();
            dtStyles = Styles.LoadStyles(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtStyles.Rows)
            {
                if (int.TryParse(dRow["StyleID"].ToString(), out iTemp))
                    StyleFilter = iTemp;
                StyleName = dRow["StyleName"].ToString();
                StyleNode = new TreeNode(StyleName, "G" + StyleFilter); // G will be assigned all Styles and C will be assigned to Campaigns
                StyleNode.Selected = false;
                StyleNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByStyle(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    StyleNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                StyleNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            StyleNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        StyleNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvStyle.Nodes.Add(StyleNode);
            }
        }

        protected void ReloadtvTechLevel(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string TechLevelName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode TechLevelNode;
            TreeNode CampaignNode;
            tvTechLevel.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection TechLevels = new Classes.cCampaignSelection();
            DataTable dtTechLevels = new DataTable();
            dtTechLevels = TechLevels.LoadTechLevels(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtTechLevels.Rows)
            {
                if (int.TryParse(dRow["TechLevelID"].ToString(), out iTemp))
                    TechLevelFilter = iTemp;
                TechLevelName = dRow["TechLevelName"].ToString();
                TechLevelNode = new TreeNode(TechLevelName, "G" + TechLevelFilter); // G will be assigned all Styles and C will be assigned to Campaigns
                TechLevelNode.Selected = false;
                TechLevelNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByTechLevel(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    TechLevelNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                TechLevelNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            TechLevelNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        TechLevelNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvTechLevel.Nodes.Add(TechLevelNode);
            }
        }

        protected void ReloadtvSize(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string SizeName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode SizeNode;
            TreeNode CampaignNode;
            tvSize.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Sizes = new Classes.cCampaignSelection();
            DataTable dtSizes = new DataTable();
            dtSizes = Sizes.LoadSizes(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtSizes.Rows)
            {
                if (int.TryParse(dRow["CampaignSizeID"].ToString(), out iTemp))
                    SizeFilter = iTemp;
                SizeName = dRow["CampaignSizeRange"].ToString();
                SizeNode = new TreeNode(SizeName, "G" + TechLevelFilter); // G will be assigned all Styles and C will be assigned to Campaigns
                SizeNode.Selected = false;
                SizeNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsBySize(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    SizeNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                SizeNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            SizeNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        SizeNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvSize.Nodes.Add(SizeNode);
            }
        }

        protected void btnSignUpForCampaign_Click(object sender, EventArgs e)
        {

        }
    }
}