using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;


//   Jbradshaw  6/19/2016  Changed over to new style of definitions for variables. No need for local variables anymore.
//                         Made changes required for user profiles.

namespace LarpPortal.Classes
{
    public class cPlayer
    {
        private Int32 _PictureID = -1;      // Only used internally to get the info.
        public Int32 UserID { get; set; }
        public string UserName { get; set; }
        public Int32 PlayerProfileID { get; set; }
        public String AuthorName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GenderStandared { get; set; }
        public string GenderOther { get; set; }
        public string EmergencyContactName { get; set; }
        public Int32 EmergencyContactPhoneID { get; set; }
        public string EmergencyContactPhone { get; set; }
        public Int32 MaxNumberOfEventsPerYear { get; set; }
        public string CPPreferenceDefault { get; set; }
        public Int32 CPDestinationDefault { get; set; }
        public Int32 PhotoPreference { get; set; }
        public string PhotoPreferenceDescription { get; set; }
        public cPicture Picture { get; set; }
        public string UserPhoto { get; set; }
        public Boolean SearchableProfile { get; set; }
        public string BackGroundKnowledge { get; set; }
        public string Comments { get; set; }
        /// <summary>
        /// 0 to 100 max out at 100.
        /// </summary>
        private Int32 _RolePlayPercentage { get; set; }  // = 0; // 
        /// <summary>
        /// 0 to 100 max out at 100.
        /// </summary>
        private Int32 _CombatPercentage { get; set; }
        /// <summary>
        /// In days.
        /// </summary>
        public Int32 WriteUpLeadTimeNeeded { get; set; }
        /// <summary>
        /// In pages.
        /// </summary>
        public Int32 WriteUpLengthPreference { get; set; }
        public List<cPlayerInventory> PlayerInventoryItems = new List<cPlayerInventory>();
        public List<cPlayerLARPResume> PlayerLARPResumes = new List<cPlayerLARPResume>();
        public List<cPlayerOccasionExceptions> PlayerOccasionExceptions = new List<cPlayerOccasionExceptions>();
        public List<cPlayerSkill> PlayerSkills = new List<cPlayerSkill>();
        public List<cPlayerWaiver> PlayerWaivers = new List<cPlayerWaiver>();
        public DateTime DateAdded;
        public DateTime DateChanged;

        public Int32 RolePlayPercentage
        {
            get { return _RolePlayPercentage; }
            set
            {
                if (value > -1 && value < 101)
                {
                    if (value + _CombatPercentage != 100)
                    {
                        _RolePlayPercentage = value;
                        _CombatPercentage = 100 - _RolePlayPercentage;
                    }
                    else
                    { _RolePlayPercentage = value; }
                }
                else
                { _RolePlayPercentage = 0; }
            }
        }
        public Int32 CombatPercentage
        {
            get { return _CombatPercentage; }
            set
            {
                if (value > -1 && value < 101)
                {
                    if (value + _RolePlayPercentage != 100)
                    {
                        _CombatPercentage = value;
                        _RolePlayPercentage = 100 - _CombatPercentage;
                    }
                    else
                    { _CombatPercentage = value; }
                }
                else
                {
                    _CombatPercentage = 0;
                }
            }
        }

        private cPlayer()
        {
        }

        public cPlayer(Int32 intUserId, string strUserName)
        {
            Picture = new cPicture();
            UserID = -1;
            PlayerProfileID = -1;
            AuthorName = "";
            DateOfBirth = DateTime.Now;
            GenderStandared = "";
            GenderOther = "";
            EmergencyContactName = "";
            EmergencyContactPhoneID = -1;
            EmergencyContactPhone = "";
            MaxNumberOfEventsPerYear = 0;
            CPPreferenceDefault = "";
            CPDestinationDefault = -1;
            PhotoPreference = -1;
            PhotoPreferenceDescription = "";
            UserPhoto = "";
            SearchableProfile = false;
            _RolePlayPercentage = 0;
            _CombatPercentage = 0;
            WriteUpLeadTimeNeeded = 0;
            WriteUpLengthPreference = 0;
            BackGroundKnowledge = "";
            Comments = "";
            DateAdded = DateTime.Now;
            DateChanged = DateTime.Now;

            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            UserID = intUserId;
            UserName = strUserName;
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@UserID", UserID);
            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerProfileByUserID", slParams, "LARPortal", strUserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    PlayerProfileID = ldt.Rows[0]["PlayerProfileID"].ToString().ToInt32();
                    AuthorName = ldt.Rows[0]["AuthorName"].ToString();
                    DateOfBirth = Convert.ToDateTime(ldt.Rows[0]["DateOfBirth"].ToString());
                    GenderStandared = ldt.Rows[0]["GenderStandard"].ToString();
                    GenderOther = ldt.Rows[0]["GenderOther"].ToString();
                    EmergencyContactName = ldt.Rows[0]["EmergencyContactName"].ToString();
                    EmergencyContactPhone = ldt.Rows[0]["EmergencyContactPhone"].ToString();
                    MaxNumberOfEventsPerYear = ldt.Rows[0]["MaxNumberEventsPerYear"].ToString().ToInt32();
                    CPPreferenceDefault = ldt.Rows[0]["CPPreferenceDefault"].ToString();
                    CPDestinationDefault = ldt.Rows[0]["CPDestinationDefault"].ToString().ToInt32();
                    PhotoPreference = ldt.Rows[0]["PhotoPreference"].ToString().ToInt32();
                    UserPhoto = ldt.Rows[0]["UserPhoto"].ToString();
                    SearchableProfile = ldt.Rows[0]["SearchableProfile"].ToString().ToBoolean();
                    RolePlayPercentage = ldt.Rows[0]["RoleplayPercentage"].ToString().ToInt32();
                    CombatPercentage = ldt.Rows[0]["CombatPercentage"].ToString().ToInt32();
                    WriteUpLeadTimeNeeded = ldt.Rows[0]["WriteUpLeadTimeNeeded"].ToString().ToInt32();
                    WriteUpLengthPreference = ldt.Rows[0]["WriteUpLengthPreference"].ToString().ToInt32();
                    BackGroundKnowledge = ldt.Rows[0]["BackgroundKnowledge"].ToString();
                    Comments = ldt.Rows[0]["Comments"].ToString();
                    DateAdded = Convert.ToDateTime(ldt.Rows[0]["DateAdded"].ToString());
                    DateChanged = Convert.ToDateTime(ldt.Rows[0]["DateChanged"].ToString());

                    // Cleaning up the Phone Number.
                    EmergencyContactPhone = EmergencyContactPhone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "");
                }
                LoadInventory();
                LoadLARPResumes();
                LoadOccasionExceptions();
                LoadSkills();
                LoadWaivers();
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName + lsRoutineName);
            }
        }

        public Boolean Save()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            Boolean blnReturn = false;
            try
            {
                SortedList slParams = new SortedList();
                slParams.Add("@PlayerProfileID", PlayerProfileID);
                slParams.Add("@UserID", UserID);
                slParams.Add("@AuthorName", AuthorName);
                slParams.Add("@DateOfBirth", DateOfBirth);
                slParams.Add("@GenderStandard", GenderStandared);
                slParams.Add("@GenderOther", GenderOther);
                slParams.Add("@EmergencyContactName", EmergencyContactName);
                slParams.Add("@EmergencyContactPhone", EmergencyContactPhone);
                slParams.Add("@MaxNumberEventsPerYear", MaxNumberOfEventsPerYear);
                slParams.Add("@CPPreferenceDefault", CPPreferenceDefault);
                slParams.Add("@CPDestinationDefault", CPDestinationDefault);
                slParams.Add("@PhotoPreference", PhotoPreference);
                slParams.Add("@UserPhoto", UserPhoto);
                slParams.Add("@SearchableProfile", SearchableProfile);
                slParams.Add("@RoleplayPercentage", _RolePlayPercentage);
                slParams.Add("@CombatPercentage", _CombatPercentage);
                slParams.Add("@WriteUpLeadTimeNeeded", WriteUpLeadTimeNeeded);
                slParams.Add("@WriteUpLengthPreference", WriteUpLengthPreference);
                slParams.Add("@BackgroundKnowledge", BackGroundKnowledge);
                slParams.Add("@Comments", Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("uspInsUpdPLPlayerProfiles", slParams, "LARPortal", UserName);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, UserName + lsRoutineName);
                blnReturn = false;
            }
            return blnReturn;

        }

        private void LoadInventory()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@PlayerProfileID", PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerInventoryByPlayerProfileID", slParams, "LARPortal", UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerInventory cAdd = new cPlayerInventory(ldr["PlayerInventoryID"].ToString().Trim().ToInt32(), PlayerProfileID, UserName, UserID);
                        PlayerInventoryItems.Add(cAdd);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, UserName + lsRoutineName);
            }
        }

        private void LoadLARPResumes()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@PlayerProfileID", PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerLarpResumesByPlayerProfileID", slParams, "LARPortal", UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerLARPResume cAdd = new cPlayerLARPResume(ldr["PlayerLARPResumeID"].ToString().Trim().ToInt32(), PlayerProfileID, UserName, UserID);
                        PlayerLARPResumes.Add(cAdd);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, UserName + lsRoutineName);
            }
        }

        private void LoadOccasionExceptions()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@PlayerProfileID", PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerOccasionExceptionsByPlayerProfileID", slParams, "LARPortal", UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerOccasionExceptions cAdd = new cPlayerOccasionExceptions(ldr["PlayerOccasionExceptionID"].ToString().Trim().ToInt32(), PlayerProfileID, UserName, UserID);
                        PlayerOccasionExceptions.Add(cAdd);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, UserName + lsRoutineName);
            }
        }

        private void LoadSkills()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@PlayerProfileID", PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerSkillsByPlayerProfileID", slParams, "LARPortal", UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerSkill cAdd = new cPlayerSkill(ldr["PlayerSkillID"].ToString().Trim().ToInt32(), PlayerProfileID, UserName, UserID);
                        PlayerSkills.Add(cAdd);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, UserName + lsRoutineName);
            }
        }

        private void LoadWaivers()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@PlayerProfileID", PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerWaiversByPlayerProfileID", slParams, "LARPortal", UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerWaiver cAdd = new cPlayerWaiver(ldr["PlayerWaiverID"].ToString().Trim().ToInt32(), PlayerProfileID, UserName, UserID);
                        PlayerWaivers.Add(cAdd);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, UserName + lsRoutineName);
            }
        }
    }
}