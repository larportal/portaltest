using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LarpPortal.Classes
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
            ProfilePicture = new cPicture();
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
        public string Accessories { get; set; }
        public string Items { get; set; }
        public string Treasure { get; set; }
        public string Makeup { get; set; }
        public string PlayerComments { get; set; }
        public string Comments { get; set; }
        public int ProfilePictureID { get; set; }
        public cPicture ProfilePicture { get; set; }
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public int CharacterSkillSetID { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }

        public List<cPlace> Places = new List<cPlace>();
        public List<cCharacterDeath> Deaths = new List<cCharacterDeath>();
        public List<cActor> Actors = new List<cActor>();
        public List<cRelationship> Relationships = new List<cRelationship>();
        public List<cPicture> Pictures = new List<cPicture>();
        public List<cCharacterSkill> CharacterSkills = new List<cCharacterSkill>();
        public List<cDescriptor> Descriptors = new List<cDescriptor>();
        public List<cCharacterItems> CharacterItems = new List<cCharacterItems>();

        public cRace Race = new cRace();
        public cCharacterType CharType = new cCharacterType();
        public cCharacterStatus Status = new cCharacterStatus();

        public int LoadCharacter(int CharacterIDToLoad)
        {
            int iNumCharacterRecords = 0;

            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            SortedList sParam = new SortedList();
            sParam.Add("@CharacterID", CharacterIDToLoad);

            DataSet dsCharacterInfo = new DataSet();
            dsCharacterInfo = cUtilities.LoadDataSet("uspGetCharacterInfo", sParam, "LARPortal", "GeneralUser", lsRoutineName);

            int iTemp;
            bool bTemp;
            DateTime dtTemp;
            double dTemp;

            Dictionary<int, string> TableInfo = new Dictionary<int, string>();

            for (int i = 0; i < dsCharacterInfo.Tables.Count; i++)
                if (dsCharacterInfo.Tables[i].Rows.Count > 0)
                {
                    if (dsCharacterInfo.Tables[i].Columns["TableName"] != null)
                        TableInfo.Add(i, dsCharacterInfo.Tables[i].Rows[0]["TableName"].ToString());
                    else
                        TableInfo.Add(i, "Unknown");
                }
                else
                    TableInfo.Add(i, "No Rows");

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
            dsCharacterInfo.Tables[14].TableName = "Descriptors";
            dsCharacterInfo.Tables[15].TableName = "CharacterItems";
            dsCharacterInfo.Tables[16].TableName = "ProfilePicture";

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
                Accessories = dRow["Accessories"].ToString();
                Items = dRow["Items"].ToString();
                Treasure = dRow["Treasure"].ToString();
                Makeup = dRow["Makeup"].ToString();
                PlayerComments = dRow["PlayerComments"].ToString();
                Comments = dRow["Comments"].ToString();

                if (int.TryParse(dRow["CampaignID"].ToString(), out iTemp))
                    CampaignID = iTemp;
                CampaignName = dRow["CampaignName"].ToString();

                if (int.TryParse(dRow["CharacterSkillSetID"].ToString(), out iTemp))
                    CharacterSkillSetID = iTemp;

                if (int.TryParse(dRow["TeamID"].ToString(), out iTemp))
                {
                    TeamID = iTemp;
                    TeamName = dRow["TeamName"].ToString();
                }
                else
                    TeamName = "";
            }

            foreach (DataRow dItems in dsCharacterInfo.Tables["CharacterItems"].Rows)
            {
                int CharacterItemID;
                int CharacterID;
                int? ItemPictureID;

                if ((int.TryParse(dItems["CharacterItemID"].ToString(), out CharacterItemID)) &&
                     (int.TryParse(dItems["CharacterID"].ToString(), out CharacterID)))
                {
                    ItemPictureID = null;
                    if (!dItems.IsNull("ItemPictureID"))
                    {
                        if (int.TryParse(dItems["ItemPictureID"].ToString(), out iTemp))
                            ItemPictureID = iTemp;
                    }
                    cCharacterItems NewItem = new cCharacterItems(CharacterItemID, CharacterID, dItems["ItemDescription"].ToString(), ItemPictureID);
                    CharacterItems.Add(NewItem);
                }
            }

            foreach (DataRow dPicture in dsCharacterInfo.Tables["CHCharacterPictures"].Rows)
            {
                cPicture NewPicture = new cPicture()
                {
                    PictureID = -1,
                    PictureFileName = dPicture["PictureFileName"].ToString(),
                    CreatedBy = dPicture["CreatedBy"].ToString(),
                    RecordStatus = RecordStatuses.Active
                };

                string sPictureType = dPicture["PictureType"].ToString();
                NewPicture.PictureType = (cPicture.PictureTypes)Enum.Parse(typeof(cPicture.PictureTypes), sPictureType);

                if (int.TryParse(dPicture["MDBPictureID"].ToString(), out iTemp))
                    NewPicture.PictureID = iTemp;

                if (int.TryParse(dPicture["CharacterID"].ToString(), out iTemp))
                    NewPicture.CharacterID = iTemp;

                Pictures.Add(NewPicture);
            }

            foreach (DataRow dPlaces in dsCharacterInfo.Tables["CHCharacterPlaces"].Rows)
            {
                cPlace NewPlace = new cPlace()
                {
                    CampaignPlaceID = -1,

                    PlaceName = dPlaces["PlaceName"].ToString(),
                    Locale = dPlaces["Locale"].ToString(),
                    //                            RulebookDescription = dPlaces["RulebookDescription"].ToString(),
                    StaffComments = dPlaces["StaffComments"].ToString(),
                    Comments = dPlaces["PlayerComments"].ToString(),
                    RecordStatus = RecordStatuses.Active
                };

                //TODO: What are these next fields?
                if (int.TryParse(dPlaces["CharacterPlaceID"].ToString(), out iTemp))
                    NewPlace.CampaignPlaceID = iTemp;

                if (int.TryParse(dPlaces["PlaceID"].ToString(), out iTemp))
                    NewPlace.PlaceID = iTemp;

                if (int.TryParse(dPlaces["PlaceTypeID"].ToString(), out iTemp))
                    NewPlace.PlaceTypeID = iTemp;

                if (int.TryParse(dPlaces["LocaleID"].ToString(), out iTemp))
                    NewPlace.LocaleID = iTemp;

                //if (int.TryParse(dPlaces["PlotLeadPerson"].ToString(), out iTemp))
                //    NewPlace.PlotLeadPerson = iTemp;

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
                    RelationDescription = dRelationship["RelationDescription"].ToString(),
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
                //       StaffAssignedRelationCharacterID
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

            foreach (DataRow dCharType in dsCharacterInfo.Tables["CHCharacterTypes"].Rows)
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

                if (int.TryParse(dSkill["CharacterSkillsStandardID"].ToString(), out iTemp))
                    NewSkill.CharacterSkillsStandardID = iTemp;

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

                if (int.TryParse(dSkill["CharacterSkillSetID"].ToString(), out iTemp))
                    NewSkill.CharacterSkillSetID = iTemp;

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

            foreach (DataRow dRow in dsCharacterInfo.Tables["Descriptors"].Rows)
            {
                cDescriptor NewDesc = new cDescriptor();
                NewDesc.SkillSetName = dRow["SkillSetName"].ToString();
                NewDesc.DescriptorValue = dRow["DescriptorValue"].ToString();
                NewDesc.CharacterDescriptor = dRow["CharacterDescriptor"].ToString();
                NewDesc.PlayerComments = dRow["PlayerComments"].ToString();

                int iValue;

                if (int.TryParse(dRow["CharacterSkillSetID"].ToString(), out iValue))
                    NewDesc.CharacterSkillSetID = iValue;
                else
                    NewDesc.CharacterSkillSetID = 0;

                if (int.TryParse(dRow["CharacterAttributesBasicID"].ToString(), out iValue))
                    NewDesc.CharacterAttributesBasicID = iValue;
                else
                    NewDesc.CharacterAttributesBasicID = 0;

                if (int.TryParse(dRow["CampaignAttributeStandardID"].ToString(), out iValue))
                    NewDesc.CampaignAttributeStandardID = iValue;
                else
                    NewDesc.CampaignAttributeStandardID = 0;

                if (int.TryParse(dRow["CampaignAttributeDescriptorID"].ToString(), out iValue))
                    NewDesc.CampaignAttributeDescriptorID = iValue;
                else
                    NewDesc.CampaignAttributeDescriptorID = 0;

                Descriptors.Add(NewDesc);
            }

            ProfilePicture = null;

            foreach (DataRow dRow in dsCharacterInfo.Tables["ProfilePicture"].Rows)
            {
                if (int.TryParse(dRow["MDBPictureID"].ToString(), out iTemp))
                {
                    ProfilePicture = new cPicture();
                    ProfilePicture.PictureID = iTemp;
                    ProfilePictureID = iTemp;
                    ProfilePicture.PictureFileName = dRow["PictureFileName"].ToString();
                    ProfilePicture.PictureType = cPicture.PictureTypes.Profile;
                    ProfilePicture.RecordStatus = RecordStatuses.Active;
                }
            }

            return iNumCharacterRecords;
        }

        public string SaveCharacter(string sUserUpdating, int iUserID)
        {
            string Timing;

            Timing = "Save character start: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");

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
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@PlotLeadPerson", PlotLeadPerson);
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
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Accessories", Accessories);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Items", Items);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Treasure", Treasure);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Makeup", Makeup);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@PlayerComments", PlayerComments);
                    CmdCHCharacterInsUpd.Parameters.AddWithValue("@Comments", Comments);

                    CmdCHCharacterInsUpd.ExecuteNonQuery();
                }

                Timing += ", character record update done: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");

                foreach (cPicture Picture in Pictures)
                {
                    Picture.CharacterID = CharacterID;
                    if (Picture.RecordStatus == RecordStatuses.Delete)
                        Picture.Delete(sUserUpdating);
                    else
                        Picture.Save(sUserUpdating);
                }

                if (ProfilePicture != null)
                    if (ProfilePicture.RecordStatus == RecordStatuses.Delete)
                        ProfilePicture.Delete(sUserUpdating);
                    else
                        ProfilePicture.Save(sUserUpdating);


                foreach (cCharacterSkill Skill in CharacterSkills)
                {
                    if (Skill.RecordStatus == RecordStatuses.Delete)
                        Skill.Delete(sUserUpdating);
                    else
                        Skill.Save(sUserUpdating);
                }

                Timing += ", skills update done: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");

                foreach (cDescriptor Desc in Descriptors)
                {
                    Desc.CharacterSkillSetID = CharacterSkillSetID;
                    if (Desc.RecordStatus == RecordStatuses.Delete)
                        Desc.Delete(sUserUpdating, iUserID);
                    else
                        Desc.Save(sUserUpdating, iUserID);
                }

                foreach (cPlace Place in Places)
                {
                    Place.CharacterID = CharacterID;
                    if (Place.RecordStatus == RecordStatuses.Delete)
                    {
                     //   Place.Delete(sUserUpdating);
                    }
                    else
                        Place.Save(sUserUpdating);
                }

                //              foreach (cCharacterDeath Death in Deaths)
                //              {
                //   //               Death.Save(sUserUpdating, connPortal);
                //              }

                //              foreach (cActor Actor in Actors)
                //              {
                // //                 Actor.Save(sUserUpdating, connPortal);
                //              }

                //              foreach (cRelationship Relationship in Relationships)
                //              {
                ////                  Relationship.Save(sUserUpdating);
                //              }

            }

            Timing += ", character save done: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");

            return Timing;
        }
    }
}


