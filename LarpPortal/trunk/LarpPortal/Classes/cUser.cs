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
        private cEMail _PrimaryEmailAddress ;
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
        private Int32 _PrimaryAddressID = -1;
        private cAddress _PrimaryAddress;
        private List<cAddress> _UserAddresses;
        private Int32 _PrimaryPhoneNumberID = -1;
        private cPhone _PrimaryPhone;
        private List<cPhone> _UserPhones;
        private Int32 _DeliveryPreferenceID = -1;
        private string _DeliveryPreferenceString = "";
        private string _LastLoggedInLocation = "";
        private cUserCampaign _UserCampaign;
        private List<cUserCampaign> _UserCampaigns = new List<cUserCampaign>();
        private int _LastLoggedInCampaign = 0;
        private Int32 _XRefNumber = -1;
        private string _Comments = "";
        private DateTime _DateAdded;
        private DateTime _DateChanged;
        
        //private cSecurity _cUserSecurity;
        private cBank _UserCPBank;
        //private List<cNoifications> _UserNotifications;
        //private cCalandar _UserCalandar;
        private List<LarpPortal.Classes.cCharacter> _UserCharacters;


        public Int32 DeliveryPreferenceID
        {
            get { return _DeliveryPreferenceID; }
            set { _DeliveryPreferenceID = value; }
        }
        public String DeliveryPreferenceString
        {
            get { return _DeliveryPreferenceString;  }
        }
        public string LastLoggedInLocation
        {
            get { return _LastLoggedInLocation; }
            set { _LastLoggedInLocation = value; }
        }        
        public int LastLoggedInCampaign
        {
            get { return _LastLoggedInCampaign; }
            set { _LastLoggedInCampaign = value; }
        }
        public Int32 XRefNumber
        {
            get { return _XRefNumber; }
            set { _XRefNumber = value; }
        }
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value; }
        }
        public Int32 PrimaryEmailID
        {
            get { return _PrimaryEmailID; }
            set { _PrimaryEmailID = value; }
        }
        public cEMail PrimaryEmailAddress
        {
            get { return _PrimaryEmailAddress; }
            set { _PrimaryEmailAddress = value; }
        }
        public string LoginPassword
        {
            get { return _LoginPassword; }
            set { _LoginPassword = value; }
        }
        public Int32 SecurityRoleID
        {
            get { return _SecurityRoleID; }
            set { _SecurityRoleID = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }
        public string ForumUserName
        {
            get { return _ForumUserName; }
            set { _ForumUserName = value; }
        }
        public string AuthorName
        {
            get { return _AuthorName; }
            set { _AuthorName = value; }
        }
        public Int32 NotificationPreference
        {
            get { return _NotificationPreference; }
            set { _NotificationPreference = value; }
        }
        public string NotificationPreferenceString
        {
            get { return _NotificationPreferenceString; }
            set { _NotificationPreferenceString = value; }
        }
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        public DateTime DateAdded
        {
            get { return _DateAdded; }
            set { _DateAdded = value; }
        }
        public DateTime DateChanged
        {
            get { return _DateChanged; }
            set { _DateChanged = value; }
       }
        public Int32 PrimaryAddressID
        {
            get { return _PrimaryAddressID; }
            set { _PrimaryAddressID = value; }
        }
        public List<cEMail> UserEmails
        {
            get { return _UserEmails; }
            set { _UserEmails = value; }
        }
        public cAddress PrimaryAddress
        {
            get { return _PrimaryAddress; }
            set { _PrimaryAddress = value; }
        }
        public List<cAddress> UserAddresses
        {
            get { return _UserAddresses; }
            set { _UserAddresses = value; }
        }
        public Int32 PrimaryPhoneNumberID
        {
            get { return _PrimaryPhoneNumberID; }
            set { _PrimaryPhoneNumberID = value; }
        }
        public cPhone PrimaryPhone
        {
            get { return _PrimaryPhone; }
            set { _PrimaryPhone = value; }
        }
        public List<cPhone> UserPhones
        {
            get { return _UserPhones; }
            set { _UserPhones = value; }
        }
        public cBank UserCPBank
        {
            get { return _UserCPBank; }
            set { _UserCPBank = value; }
        }
        public List<cUserCampaign> UserCampaigns
        {
            get { return _UserCampaigns; }
            set { _UserCampaigns = value; }
        }
               
        private cUser()
        {

        }

        public cUser(string strLoginName, string strLoginPassword)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _LoginName = strLoginName;
            _LoginPassword = strLoginPassword;
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@LoginUserName", _LoginName);
            try
            {

                DataTable ldt = cUtilities.LoadDataTable("uspGetUserByLoginName", slParams, "LARPortal", _LoginName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _UserID = ldt.Rows[0]["UserID"].ToString().ToInt32();
		            _PrimaryEmailID = ldt.Rows[0]["EmailID"].ToString().ToInt32();
		            _SecurityRoleID = ldt.Rows[0]["SecurityRoleID"].ToString().ToInt32();
		            _FirstName = ldt.Rows[0]["FirstName"].ToString();
		            _MiddleName = ldt.Rows[0]["MiddleName"].ToString();
		            _LastName = ldt.Rows[0]["LastName"].ToString();
		            _NickName = ldt.Rows[0]["NickName"].ToString();
                    _PrimaryPhoneNumberID = ldt.Rows[0]["PrimaryPhoneID"].ToString().ToInt32();
                    _PrimaryAddressID = ldt.Rows[0]["PrimaryAddressID"].ToString().ToInt32();
		            _ForumUserName = ldt.Rows[0]["ForumUsername"].ToString();
		            _NotificationPreference = ldt.Rows[0]["NotificationPreferenceID"].ToString().ToInt32();
		            _DeliveryPreferenceID = ldt.Rows[0]["DeliveryPreferenceID"].ToString().ToInt32();
		            _LastLoggedInLocation = ldt.Rows[0]["LastLoggedInLocation"].ToString();
                    _LastLoggedInCampaign = ldt.Rows[0]["LastLoggedInCampaign"].ToString().ToInt32();
		            _XRefNumber = ldt.Rows[0]["XRefNumber"].ToString().ToInt32();
		            _DateAdded = Convert.ToDateTime(ldt.Rows[0]["DateAdded"].ToString());
                    _DateChanged = Convert.ToDateTime(ldt.Rows[0]["DateChanged"].ToString());
                }

                LoadAddresses();
                LoadPhones();
                LoadEmails();

            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _LoginName + lsRoutineName);
            }
        }

        private void LoadAddresses()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
           _PrimaryAddress = new cAddress(_PrimaryAddressID,_LoginName,_UserID);
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@intKeyID", _UserID);
                slParams.Add("@strKeyType", "cUser");
                DataTable ldt = cUtilities.LoadDataTable("uspGetAddressByKey", slParams, "LARPortal", _LoginName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach(DataRow ldr in ldt.Rows)
                    {
                        cAddress cAdd = new cAddress(ldr["AddressID"].ToString().Trim().ToInt32(), _LoginName, _UserID);
                        _UserAddresses.Add(cAdd);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _LoginName + lsRoutineName);
            }
        }

        private void LoadPhones()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _PrimaryPhone = new cPhone(_PrimaryPhoneNumberID, _UserID, _LoginName);
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@intKeyID", _UserID);
                slParams.Add("@strKeyType", "cUser");

                DataTable ldt = cUtilities.LoadDataTable("uspGetPhoneNumberByKeyInfo", slParams, "LARPortal", _LoginName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cPhone cPh = new cPhone(ldr["PhoneNumberID"].ToString().Trim().ToInt32(),  _UserID, _LoginName);
                        _UserPhones.Add(cPh);                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _LoginName + lsRoutineName);
            }
        }

        private void LoadEmails()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _PrimaryEmailAddress = new cEMail(_PrimaryEmailID, _LoginName, _UserID);
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@intKeyID", _UserID);
                slParams.Add("@strKeyType", "cUser");

                DataTable ldt = cUtilities.LoadDataTable("uspGetEmailsByKeyInfo", slParams, "LARPortal", _LoginName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    foreach (DataRow ldr in ldt.Rows)
                    {
                        cEMail cPh = new cEMail(ldr["EmailID"].ToString().Trim().ToInt32(),_LoginName,_UserID);
                        _UserEmails.Add(cPh);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _LoginName + lsRoutineName);
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
                slParams.Add("@UserID",_UserID);
                slParams.Add("@LoginUsername", _LoginName);
                slParams.Add("@EmailID", _PrimaryEmailID);
                slParams.Add("@SecurityRoleID",_SecurityRoleID);
                slParams.Add("@FirstName",_FirstName);
                slParams.Add("@MiddleName",_MiddleName);
                slParams.Add("@LastName",_LastName);
                slParams.Add("@Nickname",_NickName);
                slParams.Add("@PrimaryPhoneID",_PrimaryPhoneNumberID);
                slParams.Add("@PrimaryAddressID", _PrimaryAddressID);
                slParams.Add("@ForumUsername", _ForumUserName);
                slParams.Add("@NotificationPreferenceID", _NotificationPreference);
                slParams.Add("@DeliveryPreferenceID", _DeliveryPreferenceID);
                slParams.Add("@LastLoggedInLocation", _LastLoggedInLocation);
                slParams.Add("@LastLoggedInCampaign", _LastLoggedInCampaign);
                slParams.Add("@XRefNumber", _XRefNumber);
                slParams.Add("@Comments", _Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("uspInsUpdMDBUsers", slParams, "LARPortal", _LoginName);

                blnReturn = true;
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _LoginName + lsRoutineName);
                blnReturn = false;
            }

            return blnReturn;
        }

    }


}