using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LARPortal.Classes
{
    class cPicture
    {
        public cPicture()
        {
            RecordStatus = RecordStatuses.Active;
        }

        public override string ToString()
        {
            return "ID: " + PictureID.ToString() + "  " + PictureFileName;
        }

        public int PictureID;
        public string PictureFileName;
        public RecordStatuses RecordStatus { get; set; }


        /// <summary>
        /// Save an picture record to the database. Use this if you don't already have a connection open.
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
        /// Save an picture record to the database. Use this if you have a connection open.
        /// </summary>
        /// <param name="sUserUpdating">Name of the user who is saving the record.</param>
        /// <param name="connPortal">Already OPEN connection to the database.</param>
        public void Save(string sUserUpdating, SqlConnection connPortal)
        {
            if (RecordStatus == RecordStatuses.Delete)
            {
                SqlCommand CmdDelCHCharacterPicture = new SqlCommand("uspDelCHCharacterPictures", connPortal);
                CmdDelCHCharacterPicture.CommandType = CommandType.StoredProcedure;
                CmdDelCHCharacterPicture.Parameters.AddWithValue("@RecordID", PictureID);
                CmdDelCHCharacterPicture.Parameters.AddWithValue("@UserID", sUserUpdating);

                CmdDelCHCharacterPicture.ExecuteNonQuery();
            }
            else
            {
                SqlCommand CmdInsUpdCHCharacterPicture = new SqlCommand("uspInsUpdCHCharacterPictures", connPortal);
                CmdInsUpdCHCharacterPicture.CommandType = CommandType.StoredProcedure;
                CmdInsUpdCHCharacterPicture.Parameters.AddWithValue("@CharacterPictureID", PictureID);
                CmdInsUpdCHCharacterPicture.Parameters.AddWithValue("@PictureFileName", PictureFileName);

                CmdInsUpdCHCharacterPicture.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Load a picture record. Make sure to set CharacterActorID to the record to load. Use this when you have a connection.
        /// </summary>
        /// <param name="connPortal">SQL Connection to the portal.</param>
        public void Load(SqlConnection connPortal)
        {
            if (PictureID > 0)
            {
                SqlCommand CmdGetPictureRecord = new SqlCommand("select * from CHCharacterPictures where CHCharacterPictureID = @CHCharacterPictureID", connPortal);
                CmdGetPictureRecord.Parameters.AddWithValue("@CHCharacterPictureID", PictureID);

                SqlDataAdapter SDAGetPictureRecord = new SqlDataAdapter(CmdGetPictureRecord);
                DataTable dtPictureRecord = new DataTable();

                SDAGetPictureRecord.Fill(dtPictureRecord);

                foreach (DataRow dRow in dtPictureRecord.Rows)
                {
                    PictureFileName = dRow["PictureFileName"].ToString();
                }
            }
        }

        /// <summary>
        /// Load a picture record. Make sure to set CharacterActorID to the record to load. Use this when you don't have a connection.
        /// </summary>
        public void Load()
        {
            using (SqlConnection connPortal = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
            {
                connPortal.Open();
                Load(connPortal);
            }
        }

        public void CreateNewPictureRecord()
        {
            using (SqlConnection connPortal = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
            {
                connPortal.Open();
                SqlCommand CmduspCHCharacterPicturesNewPicture = new SqlCommand("uspCHCharacterPicturesNewPicture", connPortal);
                CmduspCHCharacterPicturesNewPicture.CommandType = CommandType.StoredProcedure;

//                CmduspCHCharacterPicturesNewPicture.Parameters.AddWithValue("@CharacterID", CharacterID);

                PictureID = (int)CmduspCHCharacterPicturesNewPicture.ExecuteScalar();
            }
        }
    }
}