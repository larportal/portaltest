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
    public partial class SetupDemographics : System.Web.UI.Page
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
            lblCampaignName.Text = Campaigns.CampaignName;
            lblLARPPortalType.Text = Campaigns.PortalAccessDescription;
            lblOwner.Text = Campaigns.PrimaryOwnerName;
            tbDateStarted.Text = string.Format("{0:MMM d, yyyy}", Campaigns.StartDate);
            tbExpectedEndDate.Text = string.Format("{0:MMM d, yyyy}", Campaigns.ProjectedEndDate);
            tbCampaignZip.Text = Campaigns.PrimarySiteZipCode;
            tbActualEndDate.Text = string.Format("{0:MMM d, yyyy}", Campaigns.ActualEndDate);
            tbAvgNoEvents.Text = "";
            tbEmergencyContact.Text = Campaigns.EmergencyEventContact;
            tbEmergencyPhone.Text = "";
            LoadddlGameSystem(Campaigns.GameSystemID);
            LoadddlCampaignStatus(Campaigns.StatusID);
            LoadddlSite();
        }

        protected void LoadGenres()
        {
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
        }

        protected void LoadPeriods()
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            Classes.cCampaignBase Periods = new Classes.cCampaignBase(CampaignID, UserName, UserID);
            Periods.GetPeriods(UserName);
            lblPeriods.Text = Periods.PeriodList;
        }

        protected void LoadddlCampaignStatus(int CurrentStatusID)
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            string stStoredProc = "uspGetStatus";
            SortedList sParams = new SortedList();
            sParams.Add("@StatusType", "Campaign");
            string stCallingMethod = "SetupDemographics.LoadddlCampaignStatus";
            DataTable dtCampaignStatus = new DataTable();
            dtCampaignStatus = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            ddlCampaignStatus.DataTextField = "StatusName";
            ddlCampaignStatus.DataValueField = "StatusID";
            ddlCampaignStatus.DataSource = dtCampaignStatus;
            ddlCampaignStatus.DataBind();
            ddlCampaignStatus.SelectedValue = CurrentStatusID.ToString();
            if(CurrentStatusID == 26)
            {
                ddlCampaignStatus.Enabled = false;
            }
        }

        protected void LoadddlGameSystem(int CurrentGameSystemID)
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            string stStoredProc = "uspGetGameSystems";
            SortedList sParams = new SortedList();
            string stCallingMethod = "SetupDemographics.LoadddlGameSystems";
            DataTable dtGameSystems = new DataTable();
            dtGameSystems = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            ddlGameSystem.DataTextField = "GameSystemName";
            ddlGameSystem.DataValueField = "GameSystemID";
            ddlGameSystem.DataSource = dtGameSystems;
            ddlGameSystem.DataBind();
            ddlGameSystem.SelectedValue = CurrentGameSystemID.ToString();
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

            ;// tbCampaignZip.Text = "updated value from site lookup";
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


        protected void btnSaveChanges_Click(object sender, EventArgs e)
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
            //Campaigns.fieldname = form.fieldname.value OR form.ddlfieldname.selectedvalue.toint32
            Campaigns.GameSystemID = ddlGameSystem.SelectedValue.ToInt32();
            Campaigns.StatusID = ddlCampaignStatus.SelectedValue.ToInt32();
            //Campaigns.StartDate = tbDateStarted.
            Campaigns.PrimarySiteZipCode = tbCampaignZip.Text;
            //Campaigns.ProjectedEndDate = tbExpectedEndDate.
            //Campaigns.PrimarySite = 
            //Campaigns.ActualEndDate = tbActualEndDate.
            //Campaigns.ProjectedNumberOfEvents = tbAvgNoEvents.Text.ToInt32(); Which one, this one or the one below?  Definitely missing something.
            Campaigns.ProjectedNumberOfEvents = tbProjTotalNumEvents.Text.ToInt32();
            Campaigns.EmergencyEventContact = tbEmergencyContact.Text;
            //Campaigns.phone = tbEmergencyPhone.Text;
            Campaigns.StyleID = ddlStyle.SelectedValue.ToInt32();
            Campaigns.TechLevelID = ddlTechLevel.SelectedValue.ToInt32();
            //Campaigns.Save();
            //string jsString = "alert('Campaign demographic changes have been saved.');";
            string jsString = "alert('Campaign demographic changes are currently disabled.');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    "MyApplication",
                    jsString,
                    true);
        }

        protected void gvSites_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvSites_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvSites_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvSites_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvSites_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnEditSite_Click(object sender, EventArgs e)
        {
            pnlSites.Visible = true;
            btnSaveSites.Visible = true;
        }

        protected void btnSaveSites_Click(object sender, EventArgs e)
        {

            string jsString = "alert('Site changes have been saved.');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    "MyApplication",
                    jsString,
                    true);
            pnlSites.Visible = false;
            btnSaveSites.Visible = false;
        }

    }
}