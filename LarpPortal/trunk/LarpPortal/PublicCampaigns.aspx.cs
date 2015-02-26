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
                pnlOverview.Visible = false;
                pnlSelectors.Visible = false;
                hplLinkToSite.Visible = false;
                imgCampaignImage.Visible = false;
                pnlSignUpForCampaign.Visible = false;
                if (Session["UserID"] == null)
                    UserID = 0;
                else
                    UserID = ((int) Session["UserID"]);
                //lblTestText.Text = "Nothing has changed the test text in this box yet.";
                // Populate ddlGameSystem
                ddlGameSystem.SelectedIndex = 0;
                ddlGameSystem.Items.Clear();
                //TODO-Rick-00 Create ReloadtvGameSystem and call it here
                ReloadtvGameSystem(UserID, "", 0, 0, 0, 0, 0, 0, "", 0);
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
            int GameSystemFilter = 0;
            if (chkGameSystem.Checked == true)
                GameSystemFilter = ddlGameSystem.SelectedValue.ToInt32();
            else
                GameSystemFilter = 0;
            int CampaignFilter = 0;
            if (chkCampaign.Checked == true)
                CampaignFilter = ddlCampaign.SelectedValue.ToInt32();
            else
                CampaignFilter = 0;
            int GenreFilter = 0;
            if (chkGenre.Checked == true)
                GenreFilter = ddlGenre.SelectedValue.ToInt32();
            else
                GenreFilter = 0;
            int StyleFilter = 0;
            if (chkStyle.Checked == true)
                StyleFilter = ddlStyle.SelectedValue.ToInt32();
            else
                StyleFilter = 0;
            int TechLevelFilter = 0;
            if (chkTechLevel.Checked == true)
                TechLevelFilter = ddlTechLevel.SelectedValue.ToInt32();
            else
                TechLevelFilter = 0;
            int SizeFilter = 0;
            if (chkSize.Checked == true)
                SizeFilter = ddlSize.SelectedValue.ToInt32();
            else
                SizeFilter = 0;
            string ZipCode = "";
            int RadiusFilter = 0;
            if (chkZipCode.Checked == true)
            {
                ZipCode = txtZipCode.Text;
                RadiusFilter = ddlMileRadius.SelectedValue.ToInt32();
            }
            else
            {
                ZipCode = "";
                RadiusFilter = 0;
            }

            //lblTestText.Text = "We just passed through ddlOrderBy SelectedIndexChange.";
            switch (ddlOrderBy.Text)
            {
                case "Game System":
                    tvGameSystem.Visible = true;
                    LoadddlGameSystem(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
                    break;
                case "Campaign":
                    tvCampaign.Visible = true;
                    LoadddlCampaign(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
                    break;
                case "Genre":
                    //TODO
                    tvGenre.Visible = true;
                    LoadddlGenre(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
                    break;
                case "Style":
                    //TODO
                    tvStyle.Visible = true;
                    LoadddlStyle(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
                    break;
                case "Tech Level":
                    //TODO
                    tvTechLevel.Visible = true;
                    LoadddlTechLevel(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
                    break;
                case "Size":
                    //TODO
                    tvSize.Visible = true;
                    LoadddlSize(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
                    break;
                default:
                    break;
            }
        }

        protected void MakeDetailsVisible(int URLVisible, int ImageVisible, int OverviewVisible, int SelectorsVisible)
        {
            if (URLVisible == 1)
                hplLinkToSite.Visible = true;
            else
                hplLinkToSite.Visible = false;
            if (ImageVisible == 1)
                imgCampaignImage.Visible = true;
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
                    pnlSignUpForCampaign.Visible = true;
                else
                    pnlSignUpForCampaign.Visible = false;
            }
            else
            {
                pnlSelectors.Visible = false;
                pnlSignUpForCampaign.Visible = false;
            }
        }

        protected void SetSiteLink(string strURL, string strGameName)
        {
            hplLinkToSite.NavigateUrl = strURL;
            hplLinkToSite.Text = "Visit " + strGameName + " home page.";
        }

        protected void tvGameSystem_SelectedNodeChanged(object sender, EventArgs e)
        {
            // TODO-Rick-00 Make crap happen in the right hand side of the screen
            string strURL = "";
            string strGameOrCampaignName = "";
            //lblTestText.Text = "We just passed through tvGameSystem SelectedNodeChange.";
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
                }
            }
            if(strURL != null)
                SetSiteLink(strURL, strGameOrCampaignName);
            MakeDetailsVisible(intURLVisible, intImageVisible, intOverviewVisible, intSelectorsVisible);
        }

        protected void tvCampaign_SelectedNodeChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through tvCampaign SelectedNodeChange.";
        }

        protected void tvGenre_SelectedNodeChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through tvGenre SelectedNodeChange.";
        }

        protected void tvStyle_SelectedNodeChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through tvStyle SelectedNodeChange.";
        }

        protected void tvTechLevel_SelectedNodeChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through tvTechLevel SelectedNodeChange.";
        }

        protected void tvSize_SelectedNodeChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through tvSize SelectedNodeChange.";
        }

        protected void chkGameSystem_CheckedChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            DateTime edt = DateTime.Now;
            string EndDate;
            if (chkEndedCampaigns.Checked == true)
                EndDate = "";
            else
                EndDate = edt.ToShortDateString();
            int GameSystemFilter = 0;
            //if (chkGameSystem.Checked == true)
            //    GameSystemFilter = ddlGameSystem.SelectedValue.ToInt32();
            //else
            //    GameSystemFilter = 0;
            int CampaignFilter = 0;
            if (chkCampaign.Checked == true)
                CampaignFilter = ddlCampaign.SelectedValue.ToInt32();
            else
                CampaignFilter = 0;
            int GenreFilter = 0;
            if (chkGenre.Checked == true)
                GenreFilter = ddlGenre.SelectedValue.ToInt32();
            else
                GenreFilter = 0;
            int StyleFilter = 0;
            if (chkStyle.Checked == true)
                StyleFilter = ddlStyle.SelectedValue.ToInt32();
            else
                StyleFilter = 0;
            int TechLevelFilter = 0;
            if (chkTechLevel.Checked == true)
                TechLevelFilter = ddlTechLevel.SelectedValue.ToInt32();
            else
                TechLevelFilter = 0;
            int SizeFilter = 0;
            if (chkSize.Checked == true)
                SizeFilter = ddlSize.SelectedValue.ToInt32();
            else
                SizeFilter = 0;
            string ZipCode = "";
            int RadiusFilter = 0;
            if (chkZipCode.Checked == true)
            {
                ZipCode = txtZipCode.Text;
                RadiusFilter = ddlMileRadius.SelectedValue.ToInt32();
            }
            else
            {
                ZipCode = "";
                RadiusFilter = 0;
            }
            //lblTestText.Text = "We just passed through chkGameSystem CheckedChange.";
            if (chkGameSystem.Checked == true)
            {
                LoadddlGameSystem(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
                ddlGameSystem.Visible = true;
            }
            else
            {
                ddlGameSystem.Visible = false;
            }
        }

        protected void ddlGameSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through ddlGameSystem SelectedIndexChange.";
            // Set Session["filterGameSystem"] to GameSystemID and reload tv

        }

        protected void chkCampaign_CheckedChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through chkCampaign CheckedChange.";
            int UserID = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkCampaign.Checked == true)
            {
                ddlCampaign.Visible = true;
                string EndDate = "";
                int GameSystemFilter = 0;
                int CampaignFilter = 0;
                int GenreFilter = 0;
                int StyleFilter = 0;
                int TechLevelFilter = 0;
                int SizeFilter = 0;
                string ZipCode = "";
                int RadiusFilter = 0;
                LoadddlCampaign(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            }
            else
            {
                ddlCampaign.Visible = false;
            }
        }

        protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through ddlCampaign SelectedIndexChange.";
            // Set Session["filterCampaign"] to CampaignID and reload tv
        }

        protected void chkGenre_CheckedChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through chkGenre CheckedChange.";
            int UserID = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkGenre.Checked == true)
            {
                ddlGenre.Visible = true;
                string EndDate = "";
                int GameSystemFilter = 0;
                int CampaignFilter = 0;
                int GenreFilter = 0;
                int StyleFilter = 0;
                int TechLevelFilter = 0;
                int SizeFilter = 0;
                string ZipCode = "";
                int RadiusFilter = 0;
                LoadddlGenre(UserID, EndDate,GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            }
            else
            {
                ddlGenre.Visible = false;
            }   
        }

        protected void ddlGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through ddlGenre SelectedIndexChange.";
            // Set Session["filterGenre"] to GenreID and reload tv
        }

        protected void chkStyle_CheckedChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through chkStyle CheckedChange.";
            int UserID = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkStyle.Checked == true)
            {
                ddlStyle.Visible = true;
                string EndDate = "";
                int GameSystemFilter = 0;
                int CampaignFilter = 0;
                int GenreFilter = 0;
                int StyleFilter = 0;
                int TechLevelFilter = 0;
                int SizeFilter = 0;
                string ZipCode = "";
                int RadiusFilter = 0;
                LoadddlStyle(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            }
            else
            {
                ddlStyle.Visible = false;
                //Session["filterStyle"] = 0;
            }    
        }

        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through ddlStyle SelectedIndexChange.";
            // Set Session["filterStyle"] to StyleID and reload tv
        }

        protected void chkTechLevel_CheckedChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through chkTechLevel CheckedChange.";
            int UserID = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkTechLevel.Checked == true)
            {
                ddlTechLevel.Visible = true;
                string EndDate = "";
                int GameSystemFilter = 0;
                int CampaignFilter = 0;
                int GenreFilter = 0;
                int StyleFilter = 0;
                int TechLevelFilter = 0;
                int SizeFilter = 0;
                string ZipCode = "";
                int RadiusFilter = 0;
                LoadddlTechLevel(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            }
            else
            {
                ddlTechLevel.Visible = false;
                //Session["filterTechLevel"] = 0;
            }     
        }

        protected void ddlTechLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through ddlTechLevel SelectedIndexChange.";
            // Set Session["filterTechLevel"] to TechLevelID and reload tv
        }

        protected void chkSize_CheckedChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through chkSize CheckedChange.";
            int UserID = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkSize.Checked == true)
            {
                ddlSize.Visible = true;
                string EndDate = "";
                int GameSystemFilter = 0;
                int CampaignFilter = 0;
                int GenreFilter = 0;
                int StyleFilter = 0;
                int TechLevelFilter = 0;
                int SizeFilter = 0;
                string ZipCode = "";
                int RadiusFilter = 0;
                LoadddlSize(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            }
            else
            {
                ddlSize.Visible = false;
                //Session["filterSize"] = 0;
            }    
        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through ddlSize SelectedIndexChange.";
            // Set Session["filterSize"] to SizeID and reload tv
        }

        protected void chkZipCode_CheckedChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through chkZipZode CheckedChange.";
            int UserID = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkZipCode.Checked == true)
            {
                txtZipCode.Visible = true;
                ddlMileRadius.Visible = true;
                string EndDate = "";
                int GameSystemFilter = 0;
                int CampaignFilter = 0;
                int GenreFilter = 0;
                int StyleFilter = 0;
                int TechLevelFilter = 0;
                int SizeFilter = 0;
                string ZipCode = "";
                int RadiusFilter = 0;
                LoadddlMileRadius(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            }
            else
            {
                txtZipCode.Text = "";
                txtZipCode.Visible = false;
                ddlMileRadius.Visible = false;
            }
        }

        protected void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through txtZipCode TextChange.";
        }

        protected void ddlMileRadius_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through ddlMileRadius SelectedIndexChange.";
            // Set Session["filterMileRadius"] to MileRadiusID and reload tv
        }

        protected void chkEndedCampaigns_CheckedChanged(object sender, EventArgs e)
        {
            //lblTestText.Text = "We just passed through chkEndedCampaigns CheckedChange.";

        }

        protected void LoadddlGameSystem(int UserID, string EndDate, int GameSystemFilter, int CampaignFilter, int GenreFilter, int StyleFilter, int TechLevelFilter, int SizeFilter, string ZipCode, int RadiusFilter)
        {
            //lblTestText.Text = lblTestText.Text + " ddlGameSystem count = 0.";
            Classes.cCampaignSelection GameSystem = new Classes.cCampaignSelection();
            ddlGameSystem.DataSource = GameSystem.LoadGameSystems(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            ddlGameSystem.DataTextField = "GameSystemName";
            ddlGameSystem.DataValueField = "GameSystemID";
            ddlGameSystem.DataBind();
        }

        protected void LoadddlCampaign(int UserID, string EndDate, int GameSystemFilter, int CampaignFilter, int GenreFilter, int StyleFilter, int TechLevelFilter, int SizeFilter, string ZipCode, int RadiusFilter)
        {
            //lblTestText.Text = lblTestText.Text + " ddlCampaign count = 0.";
            Classes.cCampaignSelection Campaign = new Classes.cCampaignSelection();
            //Campaign.LoadCampaigns(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            ddlCampaign.DataSource = Campaign.LoadCampaigns(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            ddlCampaign.DataTextField = "CampaignName";
            ddlCampaign.DataValueField = "CampaignID";
            ddlCampaign.DataBind();
        }

        protected void LoadddlGenre(int UserID, string EndDate, int GameSystemFilter, int CampaignFilter, int GenreFilter, int StyleFilter, int TechLevelFilter, int SizeFilter, string ZipCode, int RadiusFilter)
        {
            //lblTestText.Text = lblTestText.Text + " ddlGenre count = 0.";
            Classes.cCampaignSelection Genre = new Classes.cCampaignSelection();
            ddlGenre.DataSource = Genre.LoadGenres(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            ddlGenre.DataTextField = "GenreName";
            ddlGenre.DataValueField = "GenreID";
            ddlGenre.DataBind();
        }

        protected void LoadddlStyle(int UserID, string EndDate, int GameSystemFilter, int CampaignFilter, int GenreFilter, int StyleFilter, int TechLevelFilter, int SizeFilter, string ZipCode, int RadiusFilter)
        {
            //lblTestText.Text = lblTestText.Text + " ddlStyle count = 0.";
            Classes.cCampaignSelection Style = new Classes.cCampaignSelection();
            ddlStyle.DataSource = Style.LoadStyles(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            ddlStyle.DataTextField = "StyleName";
            ddlStyle.DataValueField = "StyleID";
            ddlStyle.DataBind();
        }

        protected void LoadddlTechLevel(int UserID, string EndDate, int GameSystemFilter, int CampaignFilter, int GenreFilter, int StyleFilter, int TechLevelFilter, int SizeFilter, string ZipCode, int RadiusFilter)
        {
            //lblTestText.Text = lblTestText.Text + " ddlTechLevel count = 0.";
            Classes.cCampaignSelection TechLevel = new Classes.cCampaignSelection();
            ddlTechLevel.DataSource = TechLevel.LoadTechLevels(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            ddlTechLevel.DataTextField = "TechLevelName";
            ddlTechLevel.DataValueField = "TechLevelID";
            ddlTechLevel.DataBind();
        }

        protected void LoadddlSize(int UserID, string EndDate, int GameSystemFilter, int CampaignFilter, int GenreFilter, int StyleFilter, int TechLevelFilter, int SizeFilter, string ZipCode, int RadiusFilter)
        {
            //lblTestText.Text = lblTestText.Text + " ddlSize count = 0.";
            Classes.cCampaignSelection Size = new Classes.cCampaignSelection();
            ddlSize.DataSource = Size.LoadSizes(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            ddlSize.DataTextField = "CampaignSizeRange";
            //ddlSize.DataValueField = "CampaignSizeID";
            ddlSize.DataBind();
        }

        protected void LoadddlMileRadius(int UserID, string EndDate, int GameSystemFilter, int CampaignFilter, int GenreFilter, int StyleFilter, int TechLevelFilter, int SizeFilter, string ZipCode, int RadiusFilter)
        {
            //lblTestText.Text = lblTestText.Text + " ddlRadius count = 0.";
            Classes.cCampaignSelection Radius = new Classes.cCampaignSelection();
            ddlMileRadius.DataSource = Radius.LoadRadius(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            ddlMileRadius.DataTextField = "DistanceDescription";
            ddlMileRadius.DataValueField = "DistanceID";
            ddlMileRadius.DataBind();
        }

        protected void ReloadtvGameSystem(int UserID, string EndDate,int GameSystemFilter, int CampaignFilter, int GenreFilter, int StyleFilter, int TechLevelFilter, int SizeFilter, string ZipCode, int RadiusFilter)
        {
            int GameSystemID = 0;
            int CampaignID = 0;
            int StatusID = 0;
            string GameSystemName = "";
            string CampaignName = "";
            int iTemp;
            TreeNode GameSystemNode;
            TreeNode CampaignNode;
            tvGameSystem.Nodes.Clear();
            Classes.cCampaignSelection GameSystems = new Classes.cCampaignSelection();
            DataTable dtGameSystems = new DataTable();
            dtGameSystems = GameSystems.LoadGameSystems(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            foreach (DataRow dRow in dtGameSystems.Rows)
            {
                if (int.TryParse(dRow["GameSystemID"].ToString(), out iTemp))
                    GameSystemID = iTemp;
                GameSystemName = dRow["GameSystemName"].ToString();
                GameSystemNode = new TreeNode(GameSystemName, "G" + GameSystemID); // G will be assigned all Game Systems and C will be assigned to Campaigns
                GameSystemNode.Selected = false;
                GameSystemNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByGameSystem(GameSystemID, EndDate, UserID, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
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
                foreach (DataRow cRow in dtCampaigns.Rows)
                {
                    if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                        CampaignID = iTemp;
                    if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                        StatusID = iTemp;
                    CampaignName = cRow["CampaignName"].ToString();
                    if (StatusID == 4)   // Past/Ended
                        CampaignName = CampaignName + " (Ended)";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    GameSystemNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                tvGameSystem.Nodes.Add(GameSystemNode);
            }
        }

        protected void ReloadtvCampaign(int UserID, string EndDate, int GameSystemFilter, int CampaignFilter, int GenreFilter, int StyleFilter, int TechLevelFilter, int SizeFilter, string ZipCode, int RadiusFilter)
        {
            //TODO-00000-This is copied from tvGameSystem - Hack it to campaigns
            int GameSystemID = 0;
            int CampaignID = 0;
            int StatusID = 0;
            string GameSystemName = "";
            string CampaignName = "";
            int iTemp;
            TreeNode GameSystemNode;
            TreeNode CampaignNode;
            tvGameSystem.Nodes.Clear();
            Classes.cCampaignSelection GameSystems = new Classes.cCampaignSelection();
            DataTable dtGameSystems = new DataTable();
            dtGameSystems = GameSystems.LoadGameSystems(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
            foreach (DataRow dRow in dtGameSystems.Rows)
            {
                if (int.TryParse(dRow["GameSystemID"].ToString(), out iTemp))
                    GameSystemID = iTemp;
                GameSystemName = dRow["GameSystemName"].ToString();
                GameSystemNode = new TreeNode(GameSystemName, "G" + GameSystemID); // G will be assigned all Game Systems and C will be assigned to Campaigns
                GameSystemNode.Selected = false;
                GameSystemNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByGameSystem(GameSystemID, EndDate, UserID, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCode, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
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
                foreach (DataRow cRow in dtCampaigns.Rows)
                {
                    if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                        CampaignID = iTemp;
                    if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                        StatusID = iTemp;
                    CampaignName = cRow["CampaignName"].ToString();
                    if (StatusID == 4)   // Past/Ended
                        CampaignName = CampaignName + " (Ended)";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    GameSystemNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                tvGameSystem.Nodes.Add(GameSystemNode);
            }
        }

        protected void ReloadtvGenre()
        {  
               // TODO-Rick-03 Check for filters, call class, pass variables, load tree view Genre
        }

        protected void ReloadtvStyle()
        {
            // TODO-Rick-04 Check for filters, call class, pass variables, load tree view Style
        }

        protected void ReloadtvTechLevel()
        {
            // TODO-Rick-05 Check for filters, call class, pass variables, load tree view Tech Level
        }

        protected void ReloadtvSize()
        {
            // TODO-Rick-06 Check for filters, call class, pass variables, load tree view Size
        }

    }
}