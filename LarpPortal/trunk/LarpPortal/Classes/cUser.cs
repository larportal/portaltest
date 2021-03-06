﻿using System;
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
        private cEMail _PrimaryEmailAddress = new cEMail();
        private List<cEMail> _UserEmails = new List<cEMail>();
        private string _LoginEmail = "";
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
        private cAddress _PrimaryAddress = new cAddress();
        private List<cAddress> _UserAddresses = new List<cAddress>();
        private Int32 _PrimaryPhoneNumberID = -1;
        private cPhone _PrimaryPhone = new cPhone();
        private List<cPhone> _UserPhones = new List<cPhone>();
        private Int32 _DeliveryPreferenceID = -1;
        private string _DeliveryPreferenceString = "";
        private string _LastLoggedInLocation = "";
        //private cUserCampaign _UserCampaign;
        private List<cUserCampaign> _UserCampaigns = new List<cUserCampaign>();
        private int _LastLoggedInCampaign = 0;
        private int _LastLoggedInCharacter = 0;
        private string _LastLoggedInMyCharOrCamp = "";
        private Int32 _XRefNumber = -1;
        private string _Comments = "";
        private DateTime _DateAdded;
        private DateTime _DateChanged;

        
        //private cSecurity _cUserSecurity;
        private cBank _UserCPBank = new cBank();
        //private List<cNoifications> _UserNotifications;
        //private cCalandar _UserCalandar;
        //private List<LarpPortal.Classes.cCharacter> _UserCharacters;


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
        public int LastLoggedInCharacter            // JLB 7/11/2015 Added to save the last character that was saved.
        {
            get { return _LastLoggedInCharacter; }
            set { _LastLoggedInCharacter = value; }
        }

        public string LastLoggedInMyCharOrCamp      // RGP - 5/27/2017
        {
            get { return _LastLoggedInMyCharOrCamp; }
            set { _LastLoggedInMyCharOrCamp = value; }
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
        public string LoginEmail
        {
            get { return _LoginEmail; }
            set { _LoginEmail = value; }
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
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            _LoginName = strLoginName;
            _LoginPassword = strLoginPassword;
            SortedList slParams = new SortedList();
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

                    _LastLoggedInCharacter = ldt.Rows[0]["LastLoggedInCharacter"].ToString().ToInt32();
                    _LastLoggedInMyCharOrCamp = ldt.Rows[0]["LastLoggedInMyCharOrCamp"].ToString();

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
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

           _PrimaryAddress = new cAddress(_PrimaryAddressID,_LoginName,_UserID);
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@intKeyID", _UserID);
                slParams.Add("@strKeyType", "cUser");
                DataTable ldt = cUtilities.LoadDataTable("uspGetAddressByKey", slParams, "LARPortal", _LoginName, lsRoutineName);
                _UserAddresses = new List<cAddress>();
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
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            try
            {
                _PrimaryPhone = new cPhone(_PrimaryPhoneNumberID, _UserID, _LoginName);
                _UserPhones = new List<cPhone>();

                SortedList slParams = new SortedList();
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
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            try
            {
                _PrimaryEmailAddress = new cEMail(_PrimaryEmailID, _LoginName, _UserID);
                _UserEmails = new List<cEMail>();

                SortedList slParams = new SortedList();
                slParams.Add("@intKeyID", _UserID);
                slParams.Add("@strKeyType", "MDBUsers");

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

        public void SetCampaignForCharacter(int UserID, int CharacterID)
        {
            int iTemp = 0;
            string stStoredProc = "uspGetCampaignForCharacter";
            string stCallingMethod = "cUser.SetCampaignsForCharacter";
            SortedList slParams = new SortedList();
            DataTable dtCampaign = new DataTable();
            slParams.Add("@CharacterID", CharacterID);
            dtCampaign = cUtilities.LoadDataTable(stStoredProc, slParams, "LARPortal", UserID.ToString(), stCallingMethod);
            foreach (DataRow drow in dtCampaign.Rows)
            {
                if (int.TryParse(drow["CampaignID"].ToString(), out iTemp))
                {
                    if (iTemp != 0)
                        _LastLoggedInCampaign = iTemp;
                }
            }
        }

        public void SetCharacterForCampaignUser(int UserID, int CampaignID)
        {
            int iTemp = 0;
            string stStoredProc = "uspGetCharacterForCampaignUser";
            string stCallingMethod = "cUser.SetCharacterForCampaignUser";
            SortedList slParams = new SortedList();
            DataTable dtCharacters = new DataTable();
            slParams.Add("@UserID", UserID);
            slParams.Add("@CampaignID", CampaignID);
            dtCharacters = cUtilities.LoadDataTable(stStoredProc, slParams, "LARPortal", UserID.ToString(), stCallingMethod);
            foreach (DataRow drow in dtCharacters.Rows)
            {
                if (int.TryParse(drow["CharacterID"].ToString(), out iTemp))
                {
                    if (iTemp != 0)
                    {
                        _LastLoggedInCharacter = iTemp;
                    }
                }
            }
        }
       
        public Boolean Save()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            Boolean blnReturn = false;
            try
            {
                string stStoredProc = "uspInsUpdMDBUsers";
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
                if (_LastLoggedInLocation == null)
                    _LastLoggedInLocation = "MemberDemographics.aspx";
                slParams.Add("@LastLoggedInLocation", _LastLoggedInLocation);
                //if (_LastLoggedInCampaign == null)
                //    _LastLoggedInCampaign = 0;
                slParams.Add("@LastLoggedInCampaign", _LastLoggedInCampaign);
                slParams.Add("@LastLoggedInCharacter", _LastLoggedInCharacter);     //JLB 07/11/2015 Save the last character selected.
                slParams.Add("@LastLoggedInMyCharOrCamp", _LastLoggedInMyCharOrCamp);   //RGP 5/27/2017
                slParams.Add("@XRefNumber", _XRefNumber);
                slParams.Add("@Comments", _Comments);
                slParams.Add("@LogonPassword", _LoginPassword);
                if (_LoginEmail == null)
                    _LoginEmail = "";
                slParams.Add("@EmailAddress", _LoginEmail);
                blnReturn = cUtilities.PerformNonQueryBoolean(stStoredProc, slParams, "LARPortal", _LoginName);
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