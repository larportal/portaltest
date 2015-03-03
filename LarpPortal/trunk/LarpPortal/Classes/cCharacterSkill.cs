using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    }
}