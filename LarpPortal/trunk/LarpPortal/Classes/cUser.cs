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
    public class cUser
    {
        private Int32 _UserID = -1;
        private string _LoginName = "";
        private Int32 _PrimaryEmailID = -1;
        private string _PrimaryEmailAddress = "";
        private List<cEMail> _UserEmails;
        private string _LoginPassword = "";
        private Int32 _SecurityRoleID = -1;
        private string _FirstName = "";
        private string _LastName = "";
        private string _MiddleName = "";
        private string _NickName = "";
        private string _ForumUserName = "";
        private string _AuthorName = "";
        private Int32 _NotificationPreference = -1;
        private string _NotificationPreferenceString = "";
        private DateTime _DateOfBirth = DateTime.Now;
        private string _GenderStandared = "";
        private string _GenderOther = "";
        private string _EmergencyContactName = "";
        private string _EmergencyContactPhone = "";
        private Int32 _PrimaryAddressID = -1;
        private cAddress _PrimaryAddress;
        private List<cAddress> _UserAddresses;
        private Int32 _PrimaryPhoneNumberID = -1;
        private cPhone _PrimaryPhone;
        private List<cPhone> _UserPhones;
        private string _FoodAllergies = "";
        private Boolean _PrintAllergies = false;
        private string _MedicalConditions = "";
        private Boolean _PrintMedical = false;
        private string _Medications = "";
        private string _Limitations = "";
        private DateTime? _LimitationStartDate = null;
        private DateTime? _LimitationEndDate = null;
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
        //private cSecurity _cUserSecurity;
        //private cBank _cUserCPBank;
        //private List<cNoifications> _UserNotifications;
        //private cCalandar _UserCalandar;
        //private List<cCharacters> cUserCharacters;
        
        private cUser()
        {

        }

        public cUser(string strLoginName, string strLoginPassword)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _LoginName = strLoginName;
            _LoginPassword = strLoginPassword;


            // perform some sort of validation against some sort of a table I assume. 


            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@LoginName", _LoginName);
            slParams.Add("@LoginPassword", _LoginPassword);

            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetSomeData", slParams, "LarpPortal", _LoginName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    //_someValue = ldt.Rows[0]["SomeValue"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _LoginName + lsRoutineName);
            }
        }
       

    }


}