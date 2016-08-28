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
            hidCampaignZip.Value = Campaigns.PrimarySiteZipCode;
            tbActualEndDate.Text = string.Format("{0:MMM d, yyyy}", Campaigns.ActualEndDate);
            tbAvgNoEvents.Text = "";
            tbEmergencyContact.Text = Campaigns.EmergencyEventContact;
            tbEmergencyPhone.Text = "";
            LoadddlGameSystem(Campaigns.GameSystemID);
            LoadddlCampaignStatus(Campaigns.StatusID);
            LoadddlSite(Campaigns.CampaignAddressID);
            LoadddlSize(Campaigns.MarketingCampaignSize);
            LoadddlStyle(Campaigns.StyleID);
            LoadddlTechLevel(Campaigns.TechLevelID);
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

        protected void LoadddlSite(int CurrentSiteID)
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            string stStoredProc = "uspGetSites";
            SortedList sParams = new SortedList();
            string stCallingMethod = "SetupDemographics.LoadddlSite";
            DataTable dtSites = new DataTable();
            dtSites = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            dtSites.DefaultView.Sort = "SiteStateName asc";
            ddlPrimarySite.DataTextField = "SiteStateName";
            ddlPrimarySite.DataValueField = "SiteID";
            ddlPrimarySite.DataSource = dtSites;
            ddlPrimarySite.DataBind();
            ddlPrimarySite.SelectedValue = CurrentSiteID.ToString();
        }

        protected void LoadddlSize(int CurrentSize)
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            string stStoredProc = "uspGetSizes";
            SortedList sParams = new SortedList();
            sParams.Add("@SizeFilter", 0);
            string stCallingMethod = "SetupDemographics.LoadddlSize";
            DataTable dtSizes = new DataTable();
            dtSizes = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            ddlSize.DataTextField = "CampaignSizeRange";
            ddlSize.DataValueField = "CampaignSizeID";
            ddlSize.DataSource = dtSizes;
            ddlSize.DataBind();
            ddlSize.SelectedValue = CurrentSize.ToString();
        }

        protected void LoadddlStyle(int CurrentStyle)
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            string stStoredProc = "uspGetStyles";
            SortedList sParams = new SortedList();
            sParams.Add("@StyleFilter", 0);
            string stCallingMethod = "SetupDemographics.LoadddlStyle";
            DataTable dtStyles = new DataTable();
            dtStyles = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            ddlStyle.DataTextField = "StyleName";
            ddlStyle.DataValueField = "StyleID";
            ddlStyle.DataSource = dtStyles;
            ddlStyle.DataBind();
            ddlStyle.SelectedValue = CurrentStyle.ToString();
        }

        protected void LoadddlTechLevel(int CurrentTech)
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            string stStoredProc = "uspGetTechLevels";
            SortedList sParams = new SortedList();
            sParams.Add("@TechLevelFilter", 0);
            string stCallingMethod = "SetupDemographics.LoadddlTechLevel";
            DataTable dtTech = new DataTable();
            dtTech = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            ddlTechLevel.DataTextField = "TechLevelName";
            ddlTechLevel.DataValueField = "TechLevelID";
            ddlTechLevel.DataSource = dtTech;
            ddlTechLevel.DataBind();
            ddlTechLevel.SelectedValue = CurrentTech.ToString();
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
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            string stStoredProc = "uspGetSTSitesByID";
            string stCallingMethod = "SetupDemographics.ddlPrimarySite_SelectedIndexChanged";
            SortedList sParams = new SortedList();
            sParams.Add("@intSiteID", ddlPrimarySite.SelectedValue);
            DataTable dtSite = new DataTable();
            dtSite = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", UserName, stCallingMethod);
            hidCampaignZip.Value = dtSite.Rows[0]["PostalCode"].ToString().Trim();

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
            //Campaigns.StartDate = tbDateStarted.Text.ToString().to
            Campaigns.StartDate = DateTime.Parse(tbDateStarted.Text);


            Campaigns.PrimarySiteZipCode = hidCampaignZip.Value.ToString();
            //Campaigns.ProjectedEndDate = tbExpectedEndDate.
            //Campaigns.PrimarySite = 
            //Campaigns.ActualEndDate = tbActualEndDate.
            //Campaigns.ProjectedNumberOfEvents = tbAvgNoEvents.Text.ToInt32(); Which one, this one or the one below?  Definitely missing something.
            int ProjTotalNumEvents = 0;
            if (int.TryParse(tbProjTotalNumEvents.Text, out iTemp))
                ProjTotalNumEvents = iTemp;
            Campaigns.ProjectedNumberOfEvents = ProjTotalNumEvents;
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

        protected void btnSaveGenres_Click(object sender, EventArgs e)
        {

        }

        protected void btnSavePeriods_Click(object sender, EventArgs e)
        {

        }

    }
}