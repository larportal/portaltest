using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;

namespace LarpPortal.Campaigns
{
    public partial class CampaignSetup : System.Web.UI.Page
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
            //placeholders for now

            LoadSavedData();
            LoadGenres();
            LoadPeriods(); 
        }

        protected void LoadSavedData()
        {
            lblCampaignName.Text = "Fifth Gate";
            lblOwner.Text = "James Marston";
            tbDateStarted.Text = "01-Apr-2015";
            tbExpectedEndDate.Text = "30-Nov-2017";
            tbCampaignZip.Text = "03077";
            tbActualEndDate.Text = " ";
            tbAvgNoEvents.Text = "3";
            tbEmergencyContact.Text = "Robin Veniga";
            tbEmergencyPhone.Text = "603-123-4567";
        }

        protected void LoadGenres()
        {
            lblGenres.Text = "Heroic,High Fantasy,Post Apocalyptic,Steampunk,Survival";
        }

        protected void LoadPeriods()
        {
            lblPeriods.Text = "Medievel (600-1600),Renaissance (1300-1600)";
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

    }
}