using System;
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

namespace LarpPortal.Classes
{
    public class cCampaignSelection
    {
        public int GameSystemID { get; set; }
        public string GameSystemName { get; set; }
        public string GameSystemURL { get; set; }
        public string GameSystemWebPageDescription { get; set; }

        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public DateTime CampaignStartDate { get; set; }
        public DateTime CampaignProjectedEndDate { get; set; }
        public DateTime CampaignEndDate { get; set; }
        public int CampaignActualNumberOfEvents { get; set; }
        public int CampaignGameSystemID { get; set; }
        public string CampaignGameSystemName { get; set; }
        public int CampaignMarketingSizeID { get; set; }
        public string CampaignMarketingSize { get; set; }
        public int CampaignStyleID { get; set; }
        public string CampaignStyle { get; set; }
        public int CampaignAddressID { get; set; }
        public string CampaignCity { get; set; }
        public string CampaignState { get; set; }
        public string CampaignZipCode { get; set; }
        public string CampaignWebPageDescription { get; set; }
        public string CampaignURL { get; set; }
        public string CampaignLogo { get; set; }
        public double CampaignMembershipFee { get; set; }
        public string CampaignMembershipFeeFrequency { get; set; }
        //TODO-Rick-1 Define multiple selection items for campaigns (e.g. Tech Level, Genre, etc)
        //public List<cCampaignTechLevel> lsCampaignTechLevels = new list<cCampaignTechLevel>();
        //public List<cCampaignGenre> lsCampaignGenres = new list<cCampaignGenre>();
        //public List<cCampaignPeriod> lsCampaignPeriods = new list<cCampaignPeriod>();
        //public List<cCampaignWeapon> lsCampaignWeapons = new list<cCampaignWeapon>();

        public int GenreID { get; set; }
        public string GenreName { get; set; }

        public int StyleID { get; set; }
        public string StyleName { get; set; }

        public int TechLevelID { get; set; }
        public string TechLevelName { get; set; }

        public int SizeID { get; set; }
        public string SizeRange { get; set; }
        public int SizeSortOrder { get; set; }

        public string ZipCode { get; set; }


        public void GameSystems()
        {

        }

        public void Events()
        {

        }

        public void TechLevels()
        {

        }

        public void Sizes()
        {

        }

        public void Radius()
        {

        }

        public void Genres()
        {

        }

        public void Periods()
        {

        }

        public void Weapons()
        {

        }
    }

    
}