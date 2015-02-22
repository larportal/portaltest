using System;
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
                if (Session["UserID"] == null)
                    UserID = 0;
                else
                    UserID = ((int) Session["UserID"]);
                Session["filterGameSystem"] = 0;
                Session["filterCampaign"] = 0;
                Session["filterGenre"] = 0;
                Session["filterStyle"] = 0;
                Session["filterTechLevel"] = 0;
                Session["filterSize"] = 0;
                Session["filterMileRadius"] = 0;
                Session["filterCampaignEnded"] = 0;
                lblTestText.Text = "Nothing has changed the test text in this box yet.";
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
            lblTestText.Text = "We just passed through ddlOrderBy SelectedIndexChange.";
            switch (ddlOrderBy.Text)
            {
                case "Game System":
                    tvGameSystem.Visible = true;
                    LoadddlGameSystem(UserID);
                    break;
                case "Campaign":
                    tvCampaign.Visible = true;
                    LoadddlCampaign();
                    break;
                case "Genre":
                    tvGenre.Visible = true;
                    LoadddlGenre();
                    break;
                case "Style":
                    tvStyle.Visible = true;
                    LoadddlStyle();
                    break;
                case "Tech Level":
                    tvTechLevel.Visible = true;
                    LoadddlTechLevel();
                    break;
                case "Size":
                    tvSize.Visible = true;
                    LoadddlSize();
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
                pnlSelectors.Visible = true;
            else
                pnlSelectors.Visible = false;
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
            lblTestText.Text = "We just passed through tvGameSystem SelectedNodeChange.";
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
            // TODO-Rick-001 Define game or campaign URL and name and put it in the call
            if(strURL != null)
                SetSiteLink(strURL, strGameOrCampaignName);
            MakeDetailsVisible(intURLVisible, intImageVisible, intOverviewVisible, intSelectorsVisible);
        }

        protected void tvCampaign_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through tvCampaign SelectedNodeChange.";
        }

        protected void tvGenre_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through tvGenre SelectedNodeChange.";
        }

        protected void tvStyle_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through tvStyle SelectedNodeChange.";
        }

        protected void tvTechLevel_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through tvTechLevel SelectedNodeChange.";
        }

        protected void tvSize_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through tvSize SelectedNodeChange.";
        }

        protected void chkGameSystem_CheckedChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            lblTestText.Text = "We just passed through chkGameSystem CheckedChange.";
            if (chkGameSystem.Checked == true)
            {
                ddlGameSystem.Visible = true;
                LoadddlGameSystem(UserID);
            }
            else
            {
                ddlGameSystem.Visible = false;
                Session["filterGameSystem"] = 0;
            }
                
        }

        protected void ddlGameSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through ddlGameSystem SelectedIndexChange.";
            // Set Session["filterGameSystem"] to GameSystemID and reload tv

        }

        protected void chkCampaign_CheckedChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through chkCampaign CheckedChange.";
            if (chkCampaign.Checked == true)
            {
                ddlCampaign.Visible = true;
                LoadddlCampaign();
            }
            else
            {
                ddlCampaign.Visible = false;
                Session["filterCampaign"] = 0;
            }
                
        }

        protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through ddlCampaign SelectedIndexChange.";
            // Set Session["filterCampaign"] to CampaignID and reload tv
        }

        protected void chkGenre_CheckedChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through chkGenre CheckedChange.";
            if (chkGenre.Checked == true)
            {
                ddlGenre.Visible = true;
                LoadddlGenre();
            }
            else
            {
                ddlGenre.Visible = false;
                Session["filterGenre"] = 0;
            }   
        }

        protected void ddlGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through ddlGenre SelectedIndexChange.";
            // Set Session["filterGenre"] to GenreID and reload tv
        }

        protected void chkStyle_CheckedChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through chkStyle CheckedChange.";
            if (chkStyle.Checked == true)
            {
                ddlStyle.Visible = true;
                LoadddlStyle();
            }
            else
            {
                ddlStyle.Visible = false;
                Session["filterStyle"] = 0;
            }    
        }

        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through ddlStyle SelectedIndexChange.";
            // Set Session["filterStyle"] to StyleID and reload tv
        }

        protected void chkTechLevel_CheckedChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through chkTechLevel CheckedChange.";
            if (chkTechLevel.Checked == true)
            {
                ddlTechLevel.Visible = true;
                LoadddlTechLevel();
            }
            else
            {
                ddlTechLevel.Visible = false;
                Session["filterTechLevel"] = 0;
            }     
        }

        protected void ddlTechLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through ddlTechLevel SelectedIndexChange.";
            // Set Session["filterTechLevel"] to TechLevelID and reload tv
        }

        protected void chkSize_CheckedChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through chkSize CheckedChange.";
            if (chkSize.Checked == true)
            {
                ddlSize.Visible = true;
                LoadddlSize();
            }
            else
            {
                ddlSize.Visible = false;
                Session["filterSize"] = 0;
            }    
        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through ddlSize SelectedIndexChange.";
            // Set Session["filterSize"] to SizeID and reload tv
        }

        protected void chkZipCode_CheckedChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through chkZipZode CheckedChange.";
            if (chkZipCode.Checked == true)
            {
                txtZipCode.Visible = true;
                ddlMileRadius.Visible = true;
                LoadddlMileRadius();
            }
            else
            {
                txtZipCode.Text = "";
                txtZipCode.Visible = false;
                ddlMileRadius.Visible = false;
                Session["filterMileRadius"] = 0;
            }
        }

        protected void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through txtZipCode TextChange.";
        }

        protected void ddlMileRadius_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through ddlMileRadius SelectedIndexChange.";
            // Set Session["filterMileRadius"] to MileRadiusID and reload tv
        }

        protected void chkEndedCampaigns_CheckedChanged(object sender, EventArgs e)
        {
            lblTestText.Text = "We just passed through chkEndedCampaigns CheckedChange.";
            if (chkEndedCampaigns.Checked == true)
            {
                Session["filterCampaignEnded"] = 1;
            }
            else
            {
                Session["filterCampaignEnded"] = 0;
            }
        }

        protected void LoadddlGameSystem(int UserID)
        {
            lblTestText.Text = lblTestText.Text + " ddlGameSystem count = 0.";
            Classes.cGameSystems GameSystem = new Classes.cGameSystems();
            GameSystem.LoadGameSystemsByName(UserID);
        }

        protected void LoadddlCampaign()
        {
            lblTestText.Text = lblTestText.Text + " ddlCampaign count = 0.";
        }

        protected void LoadddlGenre()
        {
            lblTestText.Text = lblTestText.Text + " ddlGenre count = 0.";
        }

        protected void LoadddlStyle()
        {
            lblTestText.Text = lblTestText.Text + " ddlStyle count = 0.";
        }

        protected void LoadddlTechLevel()
        {
            lblTestText.Text = lblTestText.Text + " ddlTechLevel count = 0.";
        }

        protected void LoadddlSize()
        {
            lblTestText.Text = lblTestText.Text + " ddlSize count = 0.";
        }

        protected void LoadddlMileRadius()
        {
            lblTestText.Text = lblTestText.Text + " ddlRadius count = 0.";
        }

        protected void ReloadtvGameSystem(int UserID)
        {
            // TODO-Rick-01 Check for filters, call class, pass variables, load tree view GameSystem
            
            int GameSystemID = 0;
            int CampaignID = 0;
            int StatusID = 0;
            string GameSystemName = "";
            string CampaignName = "";
            int iTemp;
            TreeNode GameSystemNode;
            TreeNode CampaignNode;
            tvGameSystem.Nodes.Clear();

            Classes.cGameSystems GameSystems = new Classes.cGameSystems();
            DataTable dtGameSystems = new DataTable();
            dtGameSystems = GameSystems.LoadGameSystemsByName(UserID);
            // Loop through all the Game System rows of dtGameSystems
            foreach (DataRow dRow in dtGameSystems.Rows)
            {
                if (int.TryParse(dRow["GameSystemID"].ToString(), out iTemp))
                    GameSystemID = iTemp;
                GameSystemName = dRow["GameSystemName"].ToString();
                GameSystemNode = new TreeNode(GameSystemName, "G" + GameSystemID); // G will be assigned all Game Systems and C will be assigned to Campaigns
                GameSystemNode.Selected = false;
                GameSystemNode.NavigateUrl = "";

                string EndDate;
                DateTime edt = DateTime.Now;
                if (chkEndedCampaigns.Checked)
                    EndDate = "";
                else
                    EndDate = edt.ToShortDateString();
                Classes.cGameSystems Campaigns = new Classes.cGameSystems();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByGameSystem(GameSystemID, EndDate, UserID);
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

        protected void ReloadtvCampaign()
        {
            // TODO-Rick-02 Check for filters, call class, pass variables, load tree view Campaign
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