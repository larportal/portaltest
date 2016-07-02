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
using System.Net;
using System.Net.Mail;

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
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if(int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            Classes.cCampaignBase Campaigns = new Classes.cCampaignBase(CampaignID, UserName, UserID);
            
            lblCampaignName.Text = Campaigns.CampaignName;
            lblOwner.Text = Campaigns.PrimaryOwnerName;
            tbDateStarted.Text = string.Format("{0:MMM d, yyyy}", Campaigns.StartDate);
            tbExpectedEndDate.Text = string.Format("{0:MMM d, yyyy}", Campaigns.ProjectedEndDate);
            tbCampaignZip.Text = "";
            tbActualEndDate.Text = string.Format("{0:MMM d, yyyy}", Campaigns.ActualEndDate);
            tbAvgNoEvents.Text = "";
            tbEmergencyContact.Text = Campaigns.EmergencyEventContact;
            tbEmergencyPhone.Text = "";
        }

        protected void LoadGenres()
        {
            // To get a list of all genres available:
            // uspGetGenres
            // @GenreFilter = 0
            // To get all genres for a campaign:
            // uspGetCampaignGenresByCampaignID
            // @CampaignID = CampaignID 
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if(int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            Classes.cCampaignBase Genres = new Classes.cCampaignBase(CampaignID, UserName, UserID);
            Genres.GetGenres(UserName);
            lblGenres.Text = Genres.GenreList;
        }

        protected void LoadPeriods()
        {
            // To get a list of all periods available:
            // uspGetPeriods (no parameters necessary)
            // To get a list of periods for a campaign:
            // uspGetCampaignPeriodsByCampaignID
            // @CampaignID = CampaignID
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            Classes.cCampaignBase Genres = new Classes.cCampaignBase(CampaignID, UserName, UserID);
            Genres.GetGenres(UserName);
            lblGenres.Text = Genres.GenreList;
            lblPeriods.Text = "Medievel (600-1600),Renaissance (1300-1600)";
        }

        protected void LoadddlCampaignStatus()
        {
            // uspGetStatus
            // @StatusType = 'Campaign'
            // If 26 - Can't be changed by users
            //StatusID	StatusType	StatusName
            //1	        Campaign	Tentative
            //2	        Campaign	Scheduled
            //3	        Campaign	Active
            //4	        Campaign	Past
            //5	        Campaign	Complete
            //26	    Campaign	LARP Portal Hold

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
            pnlContact.Visible = false;
            pnlDemographics.Visible = false;
            pnlPolicy.Visible = false;
            pnlRequirements.Visible = false;
            pnlUserDefined.Visible = false;
            pnlWebPageDescription.Visible = false;

            switch (rdolSections.SelectedItem.Text)
            {
                case "Contact":
                    pnlContact.Visible = true;
                    break;

                case "Demographics":
                    pnlDemographics.Visible = true;
                    break;

                case "Policy":
                    pnlPolicy.Visible = true;
                    break;

                case "Requirements":
                    pnlRequirements.Visible = true;
                    break;

                case "Custom":
                    pnlUserDefined.Visible = true;
                    break;

                case "Web Page Description":
                    pnlWebPageDescription.Visible = true;
                    break;

            }
        }

    }
}