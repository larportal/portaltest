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
    public class cSite
    {

        private Int32 _UserID = -1;
        private Int32 _SiteID = -1;
        private string _SiteName = "";
        private Int32 _AddressID = -1;
        private cAddress _SiteAddress;
        private Int32 _PhoneID = -1;
        private cPhone _SitePhone;
        private string _URL = "";
        private string _SiteMapURL = "";
        private Boolean _YearRound = false;
        private string _TimeRestrictions = "";
        private Boolean _EMTCertificationRequired = false;
        private Boolean _CookingCertifcationRequired = false;
        private Boolean _AdditionalWaiversRequired = false;
        private string _SiteNotes = "";
        private string _Comments = "";
        private DateTime _DateAdded = DateTime.Now;
        private DateTime _DateChanged = DateTime.Now;
        private DateTime? _DateDeleted = null;
        private string _UserName = "";

        public Int32 SiteID
        {
            get { return _SiteID; }
        }
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value;
            _SiteAddress = new cAddress(_AddressID, _UserName, _UserID);
            }
        }
        public cAddress SiteAddress
        {
            get { return _SiteAddress; }
            set { _SiteAddress = value; }
        }
        public Int32 PhoneID
        {
            get { return _PhoneID; }
            set { _PhoneID = value;
            _SitePhone = new cPhone(_PhoneID, _UserID, _UserName);
            }
        }
        public cPhone SitePhone
        {
            get { return _SitePhone; }
            set { _SitePhone = value; }
        }
        public string URL
        { 
            get { return _URL; }
            set { _URL = value; }
        }
        public string SiteMapURL
        {
            get { return _SiteMapURL; }
            set { _SiteMapURL = value; }
        }
        public Boolean YearRound
        {
            get { return _YearRound; }
            set { _YearRound = value; }
        }
        public string TimeRestrictions
        {
            get { return _TimeRestrictions; }
            set { _TimeRestrictions = value; }
        }
        public Boolean EMTCertificationRequired
        {
            get { return _EMTCertificationRequired; }
            set { _EMTCertificationRequired = value; }
        }
        public Boolean CookingCertificationRequired 
        {
            get { return _CookingCertifcationRequired; }
            set { _CookingCertifcationRequired = value; }
        }
        public Boolean AdditionalWaiversRequired
        {
            get { return _AdditionalWaiversRequired; }
            set { _AdditionalWaiversRequired = value; }
        }
        public string SiteNotes
        {
            get { return _SiteNotes; }
            set { _SiteNotes = value; }
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
        public DateTime? DateDeleted
        {
            get { return _DateDeleted; }
        }

        private cSite()
        {
            
        }

        public cSite(Int32 intSiteID, Int32 intUserID, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _UserID = intUserID;
            _UserName = strUserName;
            _SiteID = intSiteID;
            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@SiteID", intSiteID);
            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetSomeData", slParams, "LarpPortal", strUserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _SiteName = ldt.Rows[0]["SiteName"].ToString();
                    _AddressID = ldt.Rows[0]["AddressID"].ToString().Trim().ToInt32();
                    _SiteAddress = new cAddress(_AddressID, _UserName, _UserID);
                    _PhoneID = ldt.Rows[0]["PhoneID"].ToString().Trim().ToInt32();
                    _SitePhone = new cPhone(_PhoneID, _UserID, _UserName);
                    _URL = ldt.Rows[0]["URL"].ToString();
                    _SiteMapURL = ldt.Rows[0]["SiteMapURL"].ToString();
                    _YearRound = ldt.Rows[0]["YearRound"].ToString().ToBoolean();
                    _TimeRestrictions = ldt.Rows[0]["TimeRestrictions"].ToString();
                    _EMTCertificationRequired = ldt.Rows[0]["EMTCertificationRequired"].ToString().ToBoolean();
                    _CookingCertifcationRequired = ldt.Rows[0]["CookingCertificationRequired"].ToString().ToBoolean();
                    _AdditionalWaiversRequired = ldt.Rows[0]["AdditionalWaiversRequired"].ToString().ToBoolean();
                    _SiteNotes = ldt.Rows[0]["SiteNotes"].ToString();
                    _Comments = ldt.Rows[0]["Comments"].ToString();
                    _DateAdded = Convert.ToDateTime(ldt.Rows[0]["DateAdded"].ToString());
                    _DateChanged = Convert.ToDateTime(ldt.Rows[0]["DateChanged"].ToString());
                    if (ldt.Rows[0]["DateDeleted"] == null)
                    {
                        _DateDeleted = null;
                    }
                    else
                    {
                        _DateDeleted = Convert.ToDateTime(ldt.Rows[0]["DateDeleted"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }

        public Boolean SaveUpdate ()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            Boolean blnReturn = false;
            try
            {
                SortedList slParams = new SortedList();
                slParams.Add("@UserID", _UserID);
                slParams.Add("@SiteID", _SiteID);
                slParams.Add("@SiteName", _SiteName);
                slParams.Add("@AddressID", _AddressID);
                slParams.Add("@PhoneID", _PhoneID);
                slParams.Add("@URL", _URL);
                slParams.Add("@SiteMapURL", _SiteMapURL);
                slParams.Add("@YearRound", _YearRound);
                slParams.Add("@TimeRestrictions", _TimeRestrictions);
                slParams.Add("@EMTCertificationRequired", _EMTCertificationRequired);
                slParams.Add("@CookingCertificationRequired", _CookingCertifcationRequired);
                slParams.Add("@AdditionalWaiversRequired", _AdditionalWaiversRequired);
                slParams.Add("@SiteNotes", _SiteNotes);
                slParams.Add("@Comments", _Comments);
                cUtilities.PerformNonQueryBoolean("InsUpdSTSites", slParams, "LarpPortal", _UserName);
                _SitePhone.SaveUpdate();
                _SiteAddress.SaveUpdate(_UserID);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
            return blnReturn;
        }
       

    }


}