using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Campaigns
{
    public partial class SetupContacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadlblCampaignName();
                LoadddlCampaignStatus();
                LoadddlGameSystem();
                LoadddlSite();
                LoadddlSize();
                LoadddlStyle();
                LoadddlTechLevel();
            }
        }

        protected void LoadlblCampaignName()
        {

            LoadSavedData();
            LoadGenres();
            LoadPeriods();
        }

        protected void LoadSavedData()
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            Classes.cCampaignBase Campaigns = new Classes.cCampaignBase(CampaignID, UserName, UserID);
        }

        protected void LoadGenres()
        {

        }

        protected void LoadPeriods()
        {

        }

        protected void LoadddlCampaignStatus()
        {

        }

        protected void LoadddlGameSystem()
        {

        }

        protected void LoadddlSite()
        {

        }

        protected void LoadddlSize()
        {

        }

        protected void LoadddlStyle()
        {

        }

        protected void LoadddlTechLevel()
        {

        }

        protected void LoadddlFrequency()
        {

        }

        protected void LoadddlWaiver()
        {

        }

        protected void ddlCampaignStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlGameSystem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPrimarySite_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTechLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlWaiver_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEditGenres_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditPeriods_Click(object sender, EventArgs e)
        {

        }

        protected void rdolSections_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {

        }

    }
}