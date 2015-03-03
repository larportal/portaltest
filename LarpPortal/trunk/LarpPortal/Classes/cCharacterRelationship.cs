using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LarpPortal.Classes
{
    class cRelationship
    {
        public cRelationship()
        {
            CharacterRelationshipID = -1;
            RecordStatus = RecordStatuses.Active;
        }

        public override string ToString()
        {
            return "ID: " + CharacterRelationshipID.ToString() +
                "   CharacterID: " + CharacterID.ToString() +
                "   RelationCharacterID: " + RelationCharacterID.ToString() +
                "   RelationTypeID: " + RelationTypeID.ToString();
        }

        public int? CharacterRelationshipID { get; set; }
        public int CharacterID { get; set; }
        public string Name { get; set; }
        public int RelationTypeID { get; set; }
        public int RelationCharacterID { get; set; }
        public string RelationDescription { get; set; }
        public int PlayerAssignedStatus { get; set; }
        public bool ListedInHistory { get; set; }
        public bool RulebookCharacter { get; set; }
        public int RulebookCharacterID { get; set; }
        public string PlayerComments { get; set; }
        public bool HideFromPC { get; set; }
        public int StaffAssignedRelationCharacterID { get; set; }
        public int StaffAssignedStatus { get; set; }
        public string StaffComments { get; set; }
        public string Comments { get; set; }
        public RecordStatuses RecordStatus { get; set; }

        /// <summary>
        /// Save a relationship record to the database. Use this if you don't already have a connection open.
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
        /// Save a relationship record to the database. Use this if you already have a connection open.
        /// </summary>
        /// <param name="sUserUpdating">Name of the user who is saving the record.</param>
        /// <param name="connPortal">Already OPEN connection to the database.</param>
        public void Save(string sUserUpdating, SqlConnection connPortal)
        {
            if (RecordStatus == RecordStatuses.Delete)
            {
                SqlCommand CmdDelCHCharacterDeath = new SqlCommand("uspDelCHCharacterRelationships", connPortal);
                CmdDelCHCharacterDeath.CommandType = CommandType.StoredProcedure;
                CmdDelCHCharacterDeath.Parameters.AddWithValue("@RecordID", CharacterRelationshipID);
                CmdDelCHCharacterDeath.Parameters.AddWithValue("@UserID", sUserUpdating);

                CmdDelCHCharacterDeath.ExecuteNonQuery();
            }
            else
            {
                SqlCommand CmdInsUpdCHCharacterDeath = new SqlCommand("uspInsUpdCHCharacterRelationships", connPortal);
                CmdInsUpdCHCharacterDeath.CommandType = CommandType.StoredProcedure;

                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@CharacterRelationshipID", CharacterRelationshipID);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@CharacterID", CharacterID);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@Name", Name);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@RelationTypeID", RelationTypeID);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@RelationCharacterID", RelationCharacterID);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@PlayerAssignedStatus", PlayerAssignedStatus);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@ListedInHistory", ListedInHistory);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@ReulbookCharacter", RulebookCharacter);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@RulebookCharacterID", RulebookCharacterID);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@PlayerComments", PlayerComments);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@HideFromPC", HideFromPC);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@StaffAssignedRelationCharacterID", StaffAssignedRelationCharacterID);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@StaffAssignedStatus", StaffAssignedStatus);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@StaffComments", StaffComments);
                CmdInsUpdCHCharacterDeath.Parameters.AddWithValue("@Comments", Comments);

                CmdInsUpdCHCharacterDeath.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Load a character relationship record. Make sure to set CharacterRelationshipID to the record to load.
        /// </summary>
        /// <param name="connPortal">SQL Connection to the portal.</param>
        public void Load(SqlConnection connPortal)
        {
            if (CharacterRelationshipID > 0)
            {
                SqlCommand CmdGetCharacterRelationshipRecord = new SqlCommand(
                    "select * from CHCharacterRelationship where CharacterRelationshipID = @CharacterRelationshipID", connPortal);
                CmdGetCharacterRelationshipRecord.Parameters.AddWithValue("@CharacterRelationshipID", CharacterRelationshipID);

                SqlDataAdapter SDAGetCharacterRelationshipRecord = new SqlDataAdapter(CmdGetCharacterRelationshipRecord);
                DataTable dtCharacterRelationshipRecord = new DataTable();

                SDAGetCharacterRelationshipRecord.Fill(dtCharacterRelationshipRecord);

                foreach (DataRow dRow in dtCharacterRelationshipRecord.Rows)
                {
                    StaffComments = dRow["StaffComments"].ToString();
                    Comments = dRow["Comments"].ToString();
                    Name = dRow["Name"].ToString();
                    PlayerComments = dRow["PlayerComments"].ToString();
                    RelationDescription = dRow["RelationDescription"].ToString();

                    int iTemp;
                    if (int.TryParse(dRow["CharacterID"].ToString(), out iTemp))
                        CharacterID = iTemp;

                    if (int.TryParse(dRow["RelationTypeID"].ToString(), out iTemp))
                        RelationTypeID = iTemp;

                    if (int.TryParse(dRow["RelationCharacterID"].ToString(), out iTemp))
                        RelationCharacterID = iTemp;

                    if (int.TryParse(dRow["PlayerAssignedStatus"].ToString(), out iTemp))
                        PlayerAssignedStatus = iTemp;

                    if (int.TryParse(dRow["RulebookCharacterID"].ToString(), out iTemp))
                        RulebookCharacterID = iTemp;

                    if (int.TryParse(dRow["StaffAssignedRelationCharacterID"].ToString(), out iTemp))
                        StaffAssignedRelationCharacterID = iTemp;

                    if (int.TryParse(dRow["StaffAssignedStatus"].ToString(), out iTemp))
                        StaffAssignedStatus = iTemp;

                    bool bTemp;
                    if (bool.TryParse(dRow["ListedInHistory"].ToString(), out bTemp))
                        ListedInHistory = bTemp;

                    if (bool.TryParse(dRow["RulebookCharacter"].ToString(), out bTemp))
                        RulebookCharacter = bTemp;

                    if (bool.TryParse(dRow["HideFromPC"].ToString(), out bTemp))
                        HideFromPC = bTemp;

                    RecordStatus = RecordStatuses.Active;
                }
            }
        }

        /// <summary>
        /// Load a character relationship record when you do NOT has an open connection. Make sure to set CharacterRelationshipID to the record to load.
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