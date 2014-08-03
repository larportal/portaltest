using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace LARPortal.Classes
{
    public enum RecordStatuses
    {
        Active,
        Delete
    };

    class cCharacter
    {
        public cCharacter()
        {
            CharacterID = -1;      // -1 Means that no character has been loaded. Will be a new character.
            TotalCP = 0;
        }

        public override string ToString()
        {
            return FirstName + "Don't use this. Use something else.";
        }

        public int CharacterID { get; set; }
        public int CurrentUserID { get; set; }
        public int CharacterStatusID { get; set; }
        public int CharacterStatusDesc { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AKA { get; set; }
        public string Title { get; set; }
        public int CharacterType { get; set; }
        public int PlotLeadPerson { get; set; }
        public bool RulebookCharacter { get; set; }
        public string CharacterHistory { get; set; }
        public string DateOfBirth { get; set; }
        public string WhereFrom { get; set; }
        public string CurrentHome { get; set; }
        public string CardPrintName { get; set; }
        public string HousingListName { get; set; }
        public DateTime? StartDate { get; set; }
        public string CharacterEmail { get; set; }
        public double TotalCP { get; set; }
        public string CharacterPhoto { get; set; }
        public string Costuming { get; set; }
        public string Weapons { get; set; }
        public string Items { get; set; }
        public string Treasure { get; set; }
        public string Makeup { get; set; }
        public string PlayerComments { get; set; }
        public string Comments { get; set; }

        public List<cPlace> Places = new List<cPlace>();
        public List<cCharacterDeath> Deaths = new List<cCharacterDeath>();
        public List<cActor> Actors = new List<cActor>();
        public List<cRelationship> Relationships = new List<cRelationship>();
        public List<cPicture> Pictures = new List<cPicture>();
        public List<cCharacterSkill> CharacterSkills = new List<cCharacterSkill>();

        public cRace Race = new cRace();
        public cCharacterType CharType = new cCharacterType();
        public cCharacterStatus Status = new cCharacterStatus();

        public int LoadCharacter(int CharacterIDToLoad)
        {
            int iNumCharacterRecords = 0;
            using (SqlConnection connPortal = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
            {
                int iTemp;
                bool bTemp;
                DateTime dtTemp;
                double dTemp;

                connPortal.Open();

                using (SqlCommand cmdGetCharacterInfo = new SqlCommand("uspGetCharacterInfo", connPortal))
                {
                    cmdGetCharacterInfo.CommandType = CommandType.StoredProcedure;
                    cmdGetCharacterInfo.Parameters.AddWithValue("@CharacterID", CharacterIDToLoad);

                    SqlDataAdapter SDAGetCharacterInfo = new SqlDataAdapter(cmdGetCharacterInfo);
                    DataSet dsCharacterInfo = new DataSet();

                    SDAGetCharacterInfo.Fill(dsCharacterInfo);

                    dsCharacterInfo.Tables[0].TableName = "CHCharacters";

                    dsCharacterInfo.Tables[1].TableName = "CHCharacterActors";
                    dsCharacterInfo.Tables[2].TableName = "CHCharacterSkillSets";
                    dsCharacterInfo.Tables[3].TableName = "CHCharacterPlaces";
                    dsCharacterInfo.Tables[4].TableName = "CHCharacterStaffComments";
                    dsCharacterInfo.Tables[5].TableName = "CHCharacterRelationships";
                    dsCharacterInfo.Tables[6].TableName = "CHCharacterPictures";
                    dsCharacterInfo.Tables[7].TableName = "CHCharacterDeaths";

                    dsCharacterInfo.Tables[8].TableName = "CHCampaignRaces";
                    dsCharacterInfo.Tables[9].TableName = "CHCharacterTypes";
                    dsCharacterInfo.Tables[10].TableName = "MDBStatus";
                    dsCharacterInfo.Tables[11].TableName = "CurrentCharacterUser";
                    dsCharacterInfo.Tables[12].TableName = "PlotUser";
                    dsCharacterInfo.Tables[13].TableName = "CharacterSkills";

                    iNumCharacterRecords = dsCharacterInfo.Tables["CHCharacters"].Rows.Count;

                    foreach (DataRow dRow in dsCharacterInfo.Tables["CHCharacters"].Rows)
                    {
                        if (int.TryParse(dRow["CharacterID"].ToString(), out iTemp))
                            CharacterID = iTemp;

                        if (int.TryParse(dRow["CurrentUserID"].ToString(), out iTemp))
                            CurrentUserID = iTemp;

                        if (int.TryParse(dRow["CharacterStatus"].ToString(), out iTemp))
                            CharacterStatusID = iTemp;

                        FirstName = dRow["CharacterFirstName"].ToString();
                        MiddleName = dRow["CharacterMiddleName"].ToString();
                        LastName = dRow["CharacterLastName"].ToString();
                        AKA = dRow["CharacterAKA"].ToString();
                        Title = dRow["CharacterTitle"].ToString();

                        if (int.TryParse(dRow["CharacterType"].ToString(), out iTemp))
                            CharacterType = iTemp;
                        else
                            CharacterType = -1;

                        if (int.TryParse(dRow["PlotLeadPerson"].ToString(), out iTemp))
                            PlotLeadPerson = iTemp;
                        else
                            PlotLeadPerson = -1;

                        if (bool.TryParse(dRow["RulebookCharacter"].ToString(), out bTemp))
                            RulebookCharacter = bTemp;
                        else
                            RulebookCharacter = false;

                        CharacterHistory = dRow["CharacterHistory"].ToString();
                        DateOfBirth = dRow["DateOfBirth"].ToString();
                        WhereFrom = dRow["WhereFrom"].ToString();
                        CurrentHome = dRow["CurrentHome"].ToString();
                        CardPrintName = dRow["CardPrintName"].ToString();
                        HousingListName = dRow["HousingListName"].ToString();

                        if (DateTime.TryParse(dRow["StartDate"].ToString(), out dtTemp))
                            StartDate = dtTemp;
                        else
                            StartDate = null;

                        CharacterEmail = dRow["CharacterEmail"].ToString();

                        if (double.TryParse(dRow["TotalCP"].ToString(), out dTemp))
                            TotalCP = dTemp;
                        else
                            TotalCP = 0.0;

                        CharacterPhoto = dRow["CharacterPhoto"].ToString();
                        Costuming = dRow["Costuming"].ToString();

                        Weapons = dRow["Weapons"].ToString();
                        Items = dRow["Items"].ToString();
                        Treasure = dRow["Treasure"].ToString();
                        Makeup = dRow["Makeup"].ToString();
                        PlayerComments = dRow["PlayerComments"].ToString();
                        Comments = dRow["Comments"].ToString();

                    }

                    foreach (DataRow dPicture in dsCharacterInfo.Tables["CHCharacterPictures"].Rows)
                    {
                        cPicture NewPicture = new cPicture()
                        {
                            PictureID = -1,
                            PictureFileName = dPicture["PictureFileName"].ToString(),
                            RecordStatus = RecordStatuses.Active
                        };

                        if (int.TryParse(dPicture["CHCharacterPictureID"].ToString(), out iTemp))
                            NewPicture.PictureID = iTemp;
                    }

                    foreach (DataRow dPlaces in dsCharacterInfo.Tables["CHCharacterPlaces"].Rows)
                    {
                        cPlace NewPlace = new cPlace()
                        {
                            CampaignPlaceID = -1,

                            PlaceName = dPlaces["PlaceName"].ToString(),
                            Locale = dPlaces["Locale"].ToString(),
                            RulebookDescription = dPlaces["RulebookDescription"].ToString(),
                            StaffComments = dPlaces["StaffComments"].ToString(),
                            Comments = dPlaces["Comments"].ToString(),
                            RecordStatus = RecordStatuses.Active
                        };

                        if (int.TryParse(dPlaces["CampaignPlaceID"].ToString(), out iTemp))
                            NewPlace.CampaignPlaceID = iTemp;

                        if (int.TryParse(dPlaces["PlaceTypeID"].ToString(), out iTemp))
                            NewPlace.PlaceTypeID = iTemp;

                        if (int.TryParse(dPlaces["LocaleID"].ToString(), out iTemp))
                            NewPlace.LocaleID = iTemp;

                        if (int.TryParse(dPlaces["PlotLeadPerson"].ToString(), out iTemp))
                            NewPlace.PlotLeadPerson = iTemp;

                        Places.Add(NewPlace);
                    }

                    foreach (DataRow dDeaths in dsCharacterInfo.Tables["CHCharacterDeaths"].Rows)
                    {
                        cCharacterDeath NewDeath = new cCharacterDeath()
                        {
                            CharacterDeathID = -1,
                            CharacterID = CharacterIDToLoad,
                            StaffComments = dDeaths["StaffComments"].ToString(),
                            Comments = dDeaths["Comments"].ToString(),
                            RecordStatus = RecordStatuses.Active
                        };

                        if (bool.TryParse(dDeaths["DeathPermanent"].ToString(), out bTemp))
                            NewDeath.DeathPermanent = bTemp;

                        if (DateTime.TryParse(dDeaths["DeathDate"].ToString(), out dtTemp))
                            NewDeath.DeathDate = dtTemp;

                        Deaths.Add(NewDeath);
                    }

                    foreach (DataRow dActors in dsCharacterInfo.Tables["CHCharacterActors"].Rows)
                    {
                        cActor NewActor = new cActor()
                        {
                            CharacterActorID = -1,
                            UserID = -1,
                            StartDate = null,
                            EndDate = null,
                            StaffComments = dActors["StaffComments"].ToString(),
                            Comments = dActors["Comments"].ToString(),
                            RecordStatus = RecordStatuses.Active
                        };

                        if (int.TryParse(dActors["CharacterActorID"].ToString(), out iTemp))
                            NewActor.CharacterActorID = iTemp;

                        if (int.TryParse(dActors["UserID"].ToString(), out iTemp))
                            NewActor.UserID = iTemp;

                        if (DateTime.TryParse(dActors["StartDate"].ToString(), out dtTemp))
                            NewActor.StartDate = dtTemp;

                        if (DateTime.TryParse(dActors["EndDate"].ToString(), out dtTemp))
                            NewActor.EndDate = dtTemp;

                        Actors.Add(NewActor);
                    }

                    foreach (DataRow dRelationship in dsCharacterInfo.Tables["CHCharacterRelationships"].Rows)
                    {
                        cRelationship NewRelationship = new cRelationship()
                        {
                            CharacterRelationshipID = -1,
                            CharacterID = CharacterIDToLoad,
                            Name = dRelationship["Name"].ToString(),
                            RelationTypeID = -1,
                            RelationCharacterID = -1,
                            PlayerAssignedStatus = -1,
                            ListedInHistory = false,
                            RulebookCharacter = false,
                            RulebookCharacterID = -1,
                            PlayerComments = dRelationship["PlayerComments"].ToString(),
                            HideFromPC = false,
                            StaffAssignedRelationCharacterID = -1,
                            StaffAssignedStatus = -1,
                            StaffComments = dRelationship["StaffComments"].ToString(),
                            Comments = dRelationship["Comments"].ToString(),
                            RecordStatus = RecordStatuses.Active
                        };

                        if (int.TryParse(dRelationship["CharacterRelationshipID"].ToString(), out iTemp))
                            NewRelationship.CharacterRelationshipID = iTemp;

                        if (int.TryParse(dRelationship["RelationTypeID"].ToString(), out iTemp))
                            NewRelationship.RelationTypeID = iTemp;

                        if (int.TryParse(dRelationship["RelationCharacterID"].ToString(), out iTemp))
                            NewRelationship.PlayerAssignedStatus = iTemp;

                        if (int.TryParse(dRelationship["PlayerAssignedStatus"].ToString(), out iTemp))
                            NewRelationship.PlayerAssignedStatus = iTemp;

                        if (int.TryParse(dRelationship["RulebookCharacterID"].ToString(), out iTemp))
                            NewRelationship.RulebookCharacterID = iTemp;

                        if (int.TryParse(dRelationship["StaffAssignedRelationCharacterID"].ToString(), out iTemp))
                            NewRelationship.StaffAssignedRelationCharacterID = iTemp;

                        if (int.TryParse(dRelationship["StaffAssignedStatus"].ToString(), out iTemp))
                            NewRelationship.StaffAssignedStatus = iTemp;

                        if (bool.TryParse(dRelationship["ListedInHistory"].ToString(), out bTemp))
                            NewRelationship.ListedInHistory = bTemp;

                        if (bool.TryParse(dRelationship["RulebookCharacter"].ToString(), out bTemp))
                            NewRelationship.RulebookCharacter = bTemp;

                        if (bool.TryParse(dRelationship["HideFromPC"].ToString(), out bTemp))
                            NewRelationship.HideFromPC = bTemp;

                        Relationships.Add(NewRelationship);
                    }

                    foreach (DataRow dRace in dsCharacterInfo.Tables["CHCampaignRaces"].Rows)
                    {
                        if (int.TryParse(dRace["CampaignRaceID"].ToString(), out iTemp))
                            Race.CampaignRaceID = iTemp;
                        if (int.TryParse(dRace["GameSystemID"].ToString(), out iTemp))
                            Race.GameSystemID = iTemp;
                        if (int.TryParse(dRace["CampaignID"].ToString(), out iTemp))
                            Race.CampaignID = iTemp;
                        Race.RaceName = dRace["RaceName"].ToString();
                        Race.SubRace = dRace["SubRace"].ToString();
                        Race.Description = dRace["Description"].ToString();
                        Race.MakeupRequirements = dRace["MakeupRequirements"].ToString();
                        Race.Photo = dRace["Photo"].ToString();
                        Race.Comments = dRace["Comments"].ToString();
                    }

                    foreach (DataRow dCharType in dsCharacterInfo.Tables["CHCharacterTypes]"].Rows)
                    {
                        if (int.TryParse(dCharType["CharacterTypeID"].ToString(), out iTemp))
                            CharType.CharacterTypeID = iTemp;
                        CharType.Description = dCharType["Description"].ToString();
                        CharType.Comments = dCharType["Comments"].ToString();
                    }

                    foreach (DataRow dStatus in dsCharacterInfo.Tables["MDBStatus"].Rows)
                    {
                        if (int.TryParse(dStatus["StatusID"].ToString(), out iTemp))
                            Status.StatusID = iTemp;
                        Status.StatusType = dStatus["StatusType"].ToString();
                        Status.StatusName = dStatus["StatusName"].ToString();
                        Status.Comments = dStatus["Comments"].ToString();
                    }

                    foreach (DataRow dSkill in dsCharacterInfo.Tables["CharacterSkills"].Rows)
                    {
                        cCharacterSkill NewSkill = new cCharacterSkill()
                        {
                            CharacterSkillSetID = -1,
                            SkillSetName = dSkill["SkillSetName"].ToString(),
                            StatusName = dSkill["SkillName"].ToString(),
                            SkillSetTypeDescription = dSkill["SkillSetTypeDescription"].ToString(),
                            SkillName = dSkill["SkillName"].ToString(),
                            SkillShortDescription = dSkill["SkillShortDescription"].ToString(),
                            SkillLongDescription = dSkill["SkillLongDescription"].ToString(),
                            CampaignSkillsStandardComments = dSkill["CampaignSkillsStandardComments"].ToString(),
                            SkillTypeDescription = dSkill["SkillTypeDescription"].ToString(),
                            SkillTypeComments = dSkill["SkillTypeComments"].ToString()
                        };


                        if (int.TryParse(dSkill["CharacterID"].ToString(), out iTemp))
                            NewSkill.CharacterID = iTemp;

                        if (int.TryParse(dSkill["CharacterSkillSetStatusID"].ToString(), out iTemp))
                            NewSkill.CharacterSkillSetStatusID = iTemp;

                        if (int.TryParse(dSkill["StatusType"].ToString(), out iTemp))
                            NewSkill.StatusType = iTemp;

                        if (int.TryParse(dSkill["CharacterSkillSetTypeID"].ToString(), out iTemp))
                            NewSkill.CharacterSkillSetTypeID = iTemp;

                        if (int.TryParse(dSkill["CampaignSkillsStandardID"].ToString(), out iTemp))
                            NewSkill.CampaignSkillsStandardID = iTemp;

                        if (int.TryParse(dSkill["SkillTypeID"].ToString(), out iTemp))
                            NewSkill.SkillTypeID = iTemp;

                        if (int.TryParse(dSkill["SkillHeaderTypeID"].ToString(), out iTemp))
                            NewSkill.SkillHeaderTypeID = iTemp;

                        if (int.TryParse(dSkill["HeaderAssociation"].ToString(), out iTemp))
                            NewSkill.HeaderAssociation = iTemp;

                        if (int.TryParse(dSkill["SkillCostFixed"].ToString(), out iTemp))
                            NewSkill.SkillCostFixed = iTemp;

                        if (double.TryParse(dSkill["SkillCPCost"].ToString(), out dTemp))
                            NewSkill.SkillCPCost = dTemp;

                        if (double.TryParse(dSkill["CPCostPaid"].ToString(), out dTemp))
                            NewSkill.CPCostPaid = dTemp;

                        if (bool.TryParse(dSkill["CanBeUsedPassively"].ToString(), out bTemp))
                            NewSkill.CanBeUsedPassively = bTemp;

                        if (bool.TryParse(dSkill["AllowPassiveUse"].ToString(), out bTemp))
                            NewSkill.AllowPassiveUse = bTemp;

                        if (bool.TryParse(dSkill["OpenToAll"].ToString(), out bTemp))
                            NewSkill.OpenToAll = bTemp;

                        NewSkill.RecordStatus = RecordStatuses.Active;

                        CharacterSkills.Add(NewSkill);
                    }
                }
            }

            return iNumCharacterRecords;
        }

        public int SaveCharacter(string sUserUpdating)
        {
            using (SqlConnection connPortal = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
            {
                connPortal.Open();

                using (SqlCommand CmdCHCharacterInsUpd = new SqlCommand("uspInsUpdCHCharacters", connPortal))
                {
                    CmdCHCharacterInsUpd.CommandType = CommandType.StoredProcedure;
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@UserID", "");
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterID", CharacterID);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CurrentUserID", CurrentUserID);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterStatus", CharacterStatusID);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterFirstName", FirstName);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterMiddleName", MiddleName);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterLastName", LastName);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterAKA", AKA);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterTitle", Title);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterRace", Race.CampaignRaceID);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterType", CharacterType);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@PlitLeadPerson", PlotLeadPerson);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@RulebookCharacter", RulebookCharacter);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterHistory", CharacterHistory);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@WhereFrom", WhereFrom);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CurrentHome", CurrentHome);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CardPrintName", CardPrintName);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@HousingListName", HousingListName);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@StartDate", StartDate);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterEmail", CharacterEmail);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@TotalCP", TotalCP);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@CharacterPhoto", CharacterPhoto);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Costuming", Costuming);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Weapons", Weapons);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Items", Items);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Treasure", Treasure);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Makeup", Makeup);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@PlayerComments", PlayerComments);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Comments", Comments);

                    CmdCHCharacterInsUpd.ExecuteNonQuery();
                }

                foreach (cPlace Place in Places)
                {
                    Place.Save(sUserUpdating);
                }

                foreach (cCharacterDeath Death in Deaths)
                {
                    Death.Save(sUserUpdating, connPortal);
                }

                foreach (cActor Actor in Actors)
                {
                    Actor.Save(sUserUpdating, connPortal);
                }

                foreach (cRelationship Relationship in Relationships)
                {
                    Relationship.Save(sUserUpdating);
                }

                foreach (cPicture Picture in Pictures)
                {
                    if (Picture.RecordStatus == RecordStatuses.Delete)
                    {
                        //ToDo: Create delete routine for Picture.
                    }
                    else
                    {
                        //ToDo: Create insupd routine for Picture.
                    }
                }
            }

            return 0;
        }
    }
}


