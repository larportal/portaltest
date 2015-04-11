using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LarpPortal.Classes
{
    public class cPlace
    {
        public cPlace()
        {
            CampaignPlaceID = -1;
            RecordStatus = RecordStatuses.Active;
        }

        public override string ToString()
        {
            return "ID: " + CampaignPlaceID.ToString() + "   Name: " + PlaceName;
        }

        public int CampaignPlaceID { get; set; }
        public int PlaceID { get; set; }
        public int PlaceTypeID { get; set; }
        public string PlaceName { get; set; }
        public int LocaleID { get; set; }
        public string Locale { get; set; }
        public string RulebookDescription { get; set; }
        public int PlotLeadPerson { get; set; }
        public string StaffComments { get; set; }
        public string Comments { get; set; }
        public RecordStatuses RecordStatus { get; set; }

        /// <summary>
        /// Save a place record to the database. Use this if you don't already have a connection open.
        /// </summary>
        /// <param name="sUserUpdating">Name of the user who is saving the record.</param>
        public void Save(string sUserUpdating)
        {
            using (SqlConnection connPortal = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
            {
                connPortal.Open();
                Save(sUserUpdating, connPortal);
            }
        }

        /// <summary>
        /// Save a place record to the database. Use this if you have a connection open.
        /// </summary>
        /// <param name="sUserUpdating">Name of the user who is saving the record.</param>
        /// <param name="connPortal">Already OPEN connection to the database.</param>
        public void Save(string sUserUpdating, SqlConnection connPortal)
        {
            if (RecordStatus == RecordStatuses.Delete)
            {
                SqlCommand CmdDelCHCampaignPlaces = new SqlCommand("uspDelCHCampaignPlaces", connPortal);
                CmdDelCHCampaignPlaces.CommandType = CommandType.StoredProcedure;
                CmdDelCHCampaignPlaces.Parameters.AddWithValue("@RecordID", CampaignPlaceID);
                CmdDelCHCampaignPlaces.Parameters.AddWithValue("@UserID", sUserUpdating);

                CmdDelCHCampaignPlaces.ExecuteNonQuery();
            }
            else
            {
                SqlCommand CmdInsUpdCMCampaignPlaces = new SqlCommand("uspInsUpdCHCampaignPlaces", connPortal);
                CmdInsUpdCMCampaignPlaces.CommandType = CommandType.StoredProcedure;
                CmdInsUpdCMCampaignPlaces.Parameters.AddWithValue("@CharacterPlaceIDID", CampaignPlaceID);
                CmdInsUpdCMCampaignPlaces.Parameters.AddWithValue("@PlaceID", PlaceID);
                CmdInsUpdCMCampaignPlaces.Parameters.AddWithValue("@PlaceName", PlaceName);
                CmdInsUpdCMCampaignPlaces.Parameters.AddWithValue("@StaffComments", StaffComments);
                CmdInsUpdCMCampaignPlaces.Parameters.AddWithValue("@PlayerComments", Comments);
                CmdInsUpdCMCampaignPlaces.Parameters.AddWithValue("@UserID", sUserUpdating);

                CmdInsUpdCMCampaignPlaces.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Load a campaign place record. Make sure to set CampaignPlaceID to the record to load.  Use this if you don't have a connection open.
        /// </summary>
        public void Load()
        {
            using (SqlConnection connPortal = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
            {
                connPortal.Open();
                Load(connPortal);
            }
        }

        /// <summary>
        /// Load a campaign place record. Make sure to set CampaignPlaceID to the record to load.  Use this if you have a connection open.
        /// </summary>
        /// <param name="connPortal">SQL Connection to the portal.</param>
        public void Load(SqlConnection connPortal)
        {
            if (CampaignPlaceID > 0)
            {
                SqlCommand CmdGetCampaignPlaceRecord = new SqlCommand("select * from CHCampaignPlaces where CampaignPlacesID = @CampaignPlaceID", connPortal);
                CmdGetCampaignPlaceRecord.Parameters.AddWithValue("@CampaignPlaceID", CampaignPlaceID);

                SqlDataAdapter SDAGetCampaignPlaceRecord = new SqlDataAdapter(CmdGetCampaignPlaceRecord);
                DataTable dtCampaignPlaceRecord = new DataTable();

                SDAGetCampaignPlaceRecord.Fill(dtCampaignPlaceRecord);

                foreach (DataRow dRow in dtCampaignPlaceRecord.Rows)
                {
                    Comments = dRow["Comments"].ToString();
                    StaffComments = dRow["StaffComments"].ToString();
                    PlaceName = dRow["PlaceName"].ToString();
                    Locale = dRow["Locale"].ToString();
                    RulebookDescription = dRow["RulebookDescription"].ToString();

                    int iTemp;
                    //if (int.TryParse(dRow["PlaceTypeID"].ToString(), out iTemp))
                    //    PlaceTypeID = iTemp;

                    if (int.TryParse(dRow["LocaleID"].ToString(), out iTemp))
                        LocaleID = iTemp;

                    if (int.TryParse(dRow["PlotLeadPerson"].ToString(), out iTemp))
                        PlotLeadPerson = iTemp;

                    RecordStatus = RecordStatuses.Active;
                }
            }
        }
    }
}