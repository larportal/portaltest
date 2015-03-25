using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LarpPortal.Classes
{
    public class cCharacterSkill
    {
        public cCharacterSkill()
        {
            CharacterSkillSetID = -1;
            RecordStatus = RecordStatuses.Active;
        }

        public override string ToString()
        {
            return "ID: " + CharacterSkillSetID.ToString() +
                "   UserID: " + SkillSetName.ToString();
        }

        public int CharacterSkillsStandardID { get; set; }
        public int CharacterSkillSetID { get; set; }
        public int CharacterID { get; set; }
        public string SkillSetName { get; set; }
        public int CharacterSkillSetStatusID { get; set; }
        public int StatusType { get; set; }
        public string StatusName { get; set; }
        public int CharacterSkillSetTypeID { get; set; }
        public string SkillSetTypeDescription { get; set; }
        public int CampaignSkillsStandardID { get; set; }
        public double CPCostPaid { get; set; }
        public string SkillName { get; set; }
        public int SkillTypeID { get; set; }
        public int SkillHeaderTypeID { get; set; }
        public bool CanBeUsedPassively { get; set; }
        public int HeaderAssociation { get; set; }
        public int SkillCostFixed { get; set; }
        public double SkillCPCost { get; set; }
        public string SkillShortDescription { get; set; }
        public string SkillLongDescription { get; set; }
        public string CampaignSkillsStandardComments { get; set; }
        public string SkillTypeDescription { get; set; }
        public bool AllowPassiveUse { get; set; }
        public bool OpenToAll { get; set; }
        public string SkillTypeComments { get; set; }
        public RecordStatuses RecordStatus { get; set; }






        ///// <summary>
        ///// Delete the picture. Set the ID before doing this.
        ///// </summary>
        ///// <param name="sUserID">User ID of person deleting it.</param>
        public void Delete(string sUserID)
        {
            SortedList sParam = new SortedList();

            sParam.Add("@RecordID", CharacterSkillsStandardID);
            sParam.Add("@UserID", 1);
            DataTable dtDelChar = new DataTable();
            dtDelChar = cUtilities.LoadDataTable("uspDelCHCharacterSkillsStandard", sParam, "LARPortal", sUserID, "cCharacterSkills.Delete");
        }

        public void Save(string sUserID)
        {
            string t = "";
            if (CharacterSkillsStandardID == -1)
                t = "Now";
            SortedList sParam = new SortedList();

            sParam.Add("@CharacterSkillsStandardID", CharacterSkillsStandardID);
            sParam.Add("@CharacterSkillSetID", CharacterSkillSetID);
            sParam.Add("@CampaignSkillsStandardID", CampaignSkillsStandardID);
            sParam.Add("@DisplayOnCard", 1);
            sParam.Add("@CPCostPaid", 0);
            sParam.Add("@UserID", -1);

            cUtilities.PerformNonQuery("uspInsUpdCHCharacterSkillsStandard", sParam, "LARPortal", sUserID);
        }
    }
}