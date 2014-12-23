using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;


namespace LarpPortal.Classes
{
    public class cPlayer
    {
        private Int32 _UserID = -1;
        private string _UserName = "";
        private Int32 _PlayerProfileID = -1;
        private String _AuthorName = "";
        private DateTime _DateOfBirth = DateTime.Now;
        private string _GenderStandared = "";
        private string _GenderOther = "";
        private string _EmergencyContactName = "";
        private Int32 _EmergencyContactPhoneID = -1;
        private cPhone _EmergencyContactPhone ;
        private Int32 _MaxNumberOfEventsPerYear = 0;
        private string _CPPreferenceDefault = "";
        private Int32 _CPDestinationDefault = -1;
        private Int32 _PhotoPreference = -1;
        private string _PhotoPreferenceDescription = "";
        private string _UserPhoto = "";       
        private Boolean _SearchableProfile = false;
        private Int32 _RolePlayPercentage = 0; // 0 to 100 max out at 100
        private Int32 _CombatPercentage = 0; // 0 to 100 max out at 100 ..  
        private Int32 _WriteUpLeadTimeNeeded = 0; // in days
        private Int32 _WriteUpLengthPreference = 0; // in pages
        private string _BackGroundKnowledge = "";
        private string _Comments = "";
        private DateTime  _DateAdded;
        private DateTime  _DateChanged;
        private List<cPlayerInventory> _PlayerInventoryItems;
        private List<cPlayerLARPResume> _PlayerLARPResumes;
        private List<cPlayerOccasionExceptions> _PlayerOccasionExceptions;
        private List<cPlayerSkill> _PlayerSkills;
        private List<cPlayerWaiver> _PlayerWaivers;


        public Int32 UserID
        {   get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
        }
        public Int32 PlayerProfileID
       {
           get { return _PlayerProfileID; }
           set { _PlayerProfileID = value; }
       }
        public string AuthorName
        {
            get { return _AuthorName; }
            set { _AuthorName = value; }
        }
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        public string GenderStandared
        {
            get { return _GenderStandared; }
            set { _GenderStandared = value; }
        }
        public string GenderOther
        {
            get { return _GenderOther; }
            set { _GenderOther = value; }
        }
        public string EmergencyContactName
        {
            get { return _EmergencyContactName; }
            set { _EmergencyContactName = value; }
        }
        public Int32 EmergencyContactPhoneID
        {
            get { return _EmergencyContactPhoneID; }
            set { _EmergencyContactPhoneID = value; }
        }
        public cPhone EmergencyContactPhone
        {
            get { return _EmergencyContactPhone; }
            set { _EmergencyContactPhone = value; }
        }
        public string CPPreferenceDefault
        {
            get { return _CPPreferenceDefault; }
            set { _CPPreferenceDefault = value; }
        }
        public Int32 CPDestinationDefault
        {
            get { return _CPDestinationDefault; }
            set { _CPDestinationDefault = value; }
        }
        public Int32 PhotoPreference
        {
            get { return _PhotoPreference; }
            set { _PhotoPreference = value; }
        }
        public string PhotoPreferenceDescription
        {
            get { return _PhotoPreferenceDescription; }
            set { _PhotoPreferenceDescription = value; }
        }
        public string UserPhoto
        {
            get { return _UserPhoto; }
            set { _UserPhoto = value; }
        }
        public Boolean SearchableProfile
        {
            get { return _SearchableProfile; }
            set { _SearchableProfile = value; }
        }
        public Int32 RolePlayPercentage
        {
            get { return _RolePlayPercentage; }
            set { 
                    if (value >-1 && value <101)
                    { 
                        if (value + _CombatPercentage != 100)
                        {   _RolePlayPercentage = value; 
                            _CombatPercentage = 100 - _RolePlayPercentage; }
                        else
                        {   _RolePlayPercentage = value; }
                    }
                    else
                    { _RolePlayPercentage = 0; }
                }
        }
        public Int32 CombatPercentage
        {
            get { return _CombatPercentage; }
            set {
                    if (value > -1 && value < 101)
                    {
                        if (value + _RolePlayPercentage != 100)
                        { _CombatPercentage = value;
                         _RolePlayPercentage = 100 - _CombatPercentage; }
                        else
                        { _CombatPercentage = value; }
                    }
                    else
                    {
                        _CombatPercentage = 0;
                    }
                }
        }
        public Int32 WriteUpLeadTimeNeeded
        {
            get { return _WriteUpLeadTimeNeeded; }
            set { _WriteUpLeadTimeNeeded = value; }
        }
        public Int32 WriteUpLengthPreference
        {
            get { return _WriteUpLengthPreference; }
            set { _WriteUpLengthPreference = value; }
        }
        public string BackGroundKnowledge
        {
            get { return _BackGroundKnowledge; }
            set { _BackGroundKnowledge = value; }
        }
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        public DateTime DateAdded
        {
            get { return _DateAdded; }
        }
        public DateTime DateChanged
        {
            get { return _DateChanged; }
        }
        public List<cPlayerInventory> PlayerInventoryItems
        {
            get { return _PlayerInventoryItems;}
            set {_PlayerInventoryItems = value;}
        }
        public List<cPlayerLARPResume> PlayerLARPResumes
        {
            get { return _PlayerLARPResumes; }
            set { _PlayerLARPResumes = value; }
        }
        public List<cPlayerOccasionExceptions> PlayerOccasionExceptions
        {
            get { return _PlayerOccasionExceptions; }
            set { _PlayerOccasionExceptions = value; }
        }
        public List<cPlayerSkill> PlayerSkills
        {
            get { return _PlayerSkills; }
            set { _PlayerSkills = value; }
        }
        public List<cPlayerWaiver> PlayerWaivers
        {
            get { return _PlayerWaivers; }
            set { _PlayerWaivers = value; }
        }



        private cPlayer()
        {

        }

        public cPlayer(Int32 intUserId, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _UserID = intUserId;
            _UserName = strUserName;
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@UserID", _UserID);
            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerProfileByUserID", slParams, "LarpPortal", strUserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _PlayerProfileID = ldt.Rows[0][PlayerProfileID].ToString().ToInt32();
                    _AuthorName = ldt.Rows[0]["AuthorName"].ToString();
                    _DateOfBirth = Convert.ToDateTime(ldt.Rows[0]["DateOfBirth"].ToString());
                    _GenderStandared = ldt.Rows[0]["GenderStandard"].ToString();
                    _GenderOther = ldt.Rows[0]["GenderOther"].ToString();
                    _EmergencyContactName = ldt.Rows[0]["EmergencyContactName"].ToString();
                    _EmergencyContactPhoneID = ldt.Rows[0]["EmergencyContactPhone"].ToString().ToInt32();
                    _EmergencyContactPhone = new cPhone(_EmergencyContactPhoneID,_UserID,strUserName);
                    _MaxNumberOfEventsPerYear = ldt.Rows[0]["MaxNumberEventsPerYear"].ToString().ToInt32();
                    _CPPreferenceDefault = ldt.Rows[0]["CPPreferenceDefault"].ToString();
                    _CPDestinationDefault = ldt.Rows[0]["CPDestinationDefault"].ToString().ToInt32();
                    _PhotoPreference = ldt.Rows[0]["PhotoPreference"].ToString().ToInt32();
                    _UserPhoto = ldt.Rows[0]["UserPhoto"].ToString();
                    _SearchableProfile = ldt.Rows[0]["SearchableProfile"].ToString().ToBoolean();
                    _RolePlayPercentage = ldt.Rows[0]["RoleplayPercentage"].ToString().ToInt32();
                    _CombatPercentage = ldt.Rows[0]["CombatPercentage"].ToString().ToInt32();
                    _WriteUpLeadTimeNeeded = ldt.Rows[0]["WriteUpLeadTimeNeeded"].ToString().ToInt32();
                    _WriteUpLengthPreference = ldt.Rows[0]["WriteUpLengthPreference"].ToString().ToInt32();
                    _BackGroundKnowledge = ldt.Rows[0]["BackgroundKnowledge"].ToString();
                    _Comments = ldt.Rows[0]["Comments"].ToString();
                    _DateAdded = Convert.ToDateTime(ldt.Rows[0]["DateAdded"].ToString());
                    _DateChanged = Convert.ToDateTime(ldt.Rows[0]["DateChanged"].ToString());
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
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                slParams.Add("@UserID", _UserID);
                slParams.Add("@AuthorName", _AuthorName);
                slParams.Add("@DateOfBirth", _DateOfBirth);
                slParams.Add("@GenderStandard", _GenderStandared);
                slParams.Add("@GenderOther", _GenderOther);
                slParams.Add("@EmergencyContactName", _EmergencyContactName);
                slParams.Add("@EmergencyContactPhone", _EmergencyContactPhoneID);
                slParams.Add("@MaxNumberEventsPerYear", _MaxNumberOfEventsPerYear);
                slParams.Add("@CPPreferenceDefault", _CPPreferenceDefault);
                slParams.Add("@CPDestinationDefault", _CPDestinationDefault);
                slParams.Add("@PhotoPreference", _PhotoPreference);
                slParams.Add("@UserPhoto", _UserPhoto);
                slParams.Add("@SearchableProfile", _SearchableProfile);
                slParams.Add("@RoleplayPercentage", _RolePlayPercentage);
                slParams.Add("@CombatPercentage", _CombatPercentage);
                slParams.Add("@WriteUpLeadTimeNeeded", _WriteUpLeadTimeNeeded);
                slParams.Add("@WriteUpLengthPreference", _WriteUpLengthPreference);
                slParams.Add("@BackgroundKnowledge", _BackGroundKnowledge);
                slParams.Add("@Comments", _Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("uspInsUpdPLPlayerProfiles", slParams, "LarpPortal", _UserName);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
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
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerInventoryByPlayerProfileID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerInventory cAdd = new cPlayerInventory(ldr["PlayerInventoryID"].ToString().Trim().ToInt32(), _PlayerProfileID, _UserName, _UserID);
                        _PlayerInventoryItems.Add(cAdd);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }
        private void LoadLARPResumes()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerLarpResumesByPlayerProfileID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerLARPResume cAdd = new cPlayerLARPResume(ldr["PlayerLARPResumeID"].ToString().Trim().ToInt32(), _PlayerProfileID, _UserName, _UserID);
                        _PlayerLARPResumes.Add(cAdd);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }
        private void LoadOccasionExceptions()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerOccasionExceptionsByPlayerProfileID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerOccasionExceptions cAdd = new cPlayerOccasionExceptions(ldr["PlayerOccasionExceptionID"].ToString().Trim().ToInt32(), _PlayerProfileID, _UserName, _UserID);
                        _PlayerOccasionExceptions.Add(cAdd);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }
        private void LoadSkills()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerSkillsByPlayerProfileID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerSkill cAdd = new cPlayerSkill(ldr["PlayerSkillID"].ToString().Trim().ToInt32(), _PlayerProfileID, _UserName, _UserID);
                        _PlayerSkills.Add(cAdd);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }
        private void LoadWaivers()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerWaiversByPlayerProfileID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPlayerWaiver cAdd = new cPlayerWaiver(ldr["PlayerWaiverID"].ToString().Trim().ToInt32(), _PlayerProfileID, _UserName, _UserID);
                        _PlayerWaivers.Add(cAdd);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }




    }


}