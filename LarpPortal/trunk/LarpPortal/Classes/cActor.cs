using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LARPortal.Classes
{
    public class cActor
    {
        public cActor()
        {
            CharacterActorID = -1;
            RecordStatus = RecordStatuses.Active;
        }

        public override string ToString()
        {
            return "ID: " + CharacterActorID.ToString() +
                "   UserID: " + UserID.ToString() +
                "   StartDate: " + (StartDate.HasValue ? StartDate.Value.ToString() : "Not set") +
                "   EndDate: " + (EndDate.HasValue ? EndDate.Value.ToString() : "Not set");
        }

        public int? CharacterActorID { get; set; }
        public int CharacterID { get; set; }
        public int UserID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StaffComments { get; set; }
        public string Comments { get; set; }
        public RecordStatuses RecordStatus { get; set; }

        /// <summary>
        /// Save an actor record to the database. Use this if you don't already have a connection open.
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
        /// Save an actor record to the database. Use this if you have a connection open.
        /// </summary>
        /// <param name="sUserUpdating">Name of the user who is saving the record.</param>
        /// <param name="connPortal">Already OPEN connection to the database.</param>
        public void Save(string sUserUpdating, SqlConnection connPortal)
        {
            if (RecordStatus == RecordStatuses.Delete)
            {
                SqlCommand CmdDelCHCharacterActors = new SqlCommand("uspDelCHCharacterActors", connPortal);
                CmdDelCHCharacterActors.CommandType = CommandType.StoredProcedure;
                CmdDelCHCharacterActors.Parameters.AddWithValue("@RecordID", CharacterActorID);
                CmdDelCHCharacterActors.Parameters.AddWithValue("@UserID", sUserUpdating);

                CmdDelCHCharacterActors.ExecuteNonQuery();
            }
            else
            {
                SqlCommand CmdInsUpdCHCharacterActors = new SqlCommand("uspInsUpdCHCharacterActors", connPortal);
                CmdInsUpdCHCharacterActors.CommandType = CommandType.StoredProcedure;
                CmdInsUpdCHCharacterActors.Parameters.AddWithValue("@CharacterActorID", CharacterActorID);
                CmdInsUpdCHCharacterActors.Parameters.AddWithValue("@CharacterID", CharacterID);
                CmdInsUpdCHCharacterActors.Parameters.AddWithValue("@UserID", UserID);
                CmdInsUpdCHCharacterActors.Parameters.AddWithValue("@StartDate", StartDate);
                CmdInsUpdCHCharacterActors.Parameters.AddWithValue("@EndDate", EndDate);
                CmdInsUpdCHCharacterActors.Parameters.AddWithValue("@StaffComments", StaffComments);
                CmdInsUpdCHCharacterActors.Parameters.AddWithValue("@Comments", Comments);

                CmdInsUpdCHCharacterActors.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Load a actor record. Make sure to set CharacterActorID to the record to load. Use this when you have a connection.
        /// </summary>
        /// <param name="connPortal">SQL Connection to the portal.</param>
        public void Load(SqlConnection connPortal)
        {
            if (CharacterActorID > 0)
            {
                SqlCommand CmdGetActorRecord = new SqlCommand("select * from CHCharacterActors where CharacterActorID = @CharacterActorID", connPortal);
                CmdGetActorRecord.Parameters.AddWithValue("@CharacterActorID", CharacterActorID);

                SqlDataAdapter SDAGetActorRecord = new SqlDataAdapter(CmdGetActorRecord);
                DataTable dtActorRecord = new DataTable();

                SDAGetActorRecord.Fill(dtActorRecord);

                foreach (DataRow dRow in dtActorRecord.Rows)
                {
                    StaffComments = dRow["StaffComments"].ToString();
                    Comments = dRow["Comments"].ToString();

                    int iTemp;
                    if (int.TryParse(dRow["CharacterID"].ToString(), out iTemp))
                        CharacterID = iTemp;

                    DateTime dtTemp;
                    if (DateTime.TryParse(dRow["StartDate"].ToString(), out dtTemp))
                        StartDate = dtTemp;

                    if (DateTime.TryParse(dRow["EndDate"].ToString(), out dtTemp))
                        EndDate = dtTemp;
                }
            }
        }

        /// <summary>
        /// Load a actor record. Make sure to set CharacterActorID to the record to load. Use this when you don't have a connection.
        /// </summary>
        public void Load()
        {
            using (SqlConnection connPortal = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
            {
                connPortal.Open();
                Load(connPortal);
            }
        }
    }
}