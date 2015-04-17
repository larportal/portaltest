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
    public class cPhone
    {

        private Int32 _UserID = -1;
        private Int32 _PhoneNumberID = -1;
        private Int32 _PhoneTypeID = -1;
        private string _PhoneTypeDescription = "";
        private string _IDD = "";
        private string _CountryCode = "";
        private string _AreaCode = "";
        private string _PhoneNumber = "";
        private string _Extension = "";
        private string _Comments = "";
        private string _UserName = "";

        public Int32 PhoneNumberID
        {
            get { return _PhoneNumberID; }
        }
        public Int32 PhoneTypeID
        {
            get { return _PhoneTypeID; }
            set
            {
                _PhoneTypeID = value;
                GetPhoneTypeDescription();
            }
                 
        }
        public string IDD
        {
            get { return _IDD; }
            set { _IDD = value; }
        }
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        public string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }

        private cPhone()
        {

        }

        public cPhone(Int32 intPhoneNumberID, Int32 intUserID, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _UserID = intUserID;
            _UserName = strUserName;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@intPhoneNumberID", _PhoneNumberID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetPhoneNumber", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _PhoneTypeID = ldt.Rows[0]["PhoneTypeID"].ToString().Trim().ToInt32();
                    //GetPhoneTypeDescription();
                    _PhoneTypeDescription = ldt.Rows[0]["PhoneType"].ToString();
                    _IDD = ldt.Rows[0]["IDD"].ToString();
                    _CountryCode = ldt.Rows[0]["CountryCode"].ToString();
                    _AreaCode = ldt.Rows[0]["AreaCode"].ToString();
                    _PhoneNumber = ldt.Rows[0]["PhoneNumber"].ToString();
                    _Extension = ldt.Rows[0]["Extension"].ToString();
                    _Comments = ldt.Rows[0]["Comments"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }

        private void GetPhoneTypeDescription()
        {

            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            
            try
            {
                SortedList slParams = new SortedList();
                slParams.Add("@PhoneTypeID", _PhoneTypeID);
                _PhoneTypeDescription = cUtilities.ReturnStringFromSQL("somestoredprocedure", "PhoneType", slParams, "LarpPortal", _UserName, lsRoutineName);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
                
            }

            
        }

        public static bool isValidPhoneNumber(string strPhone)
        {
            if (string.IsNullOrWhiteSpace(strPhone))
                return false;

            strPhone = strPhone.Trim();
            //Make sure all values are digits
            if (strPhone.All(x => Char.IsDigit(x)) == false)
                return false;
            //This line is a substitute to remove any non-digits and only if we ever disable check above
            //string strPhone = string.Join(string.Empty, strPhone.Where(x => Char.IsDigit(x)).ToArray());

            //800s, 900, and zero digits on first position are not okay
            if (strPhone.StartsWith("8") || strPhone.StartsWith("9") || strPhone.StartsWith("0"))
                return false;

            // Get all the digits from the string and make sure we have ten numeric value
            return (strPhone.Length == 10);
        }

        public bool isValidPhoneNumber()
        {
            return isValidPhoneNumber(AreaCode + PhoneNumber);
        }

        public Boolean SaveUpdate()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            Boolean blnReturn = false;
            try
            {
                SortedList slParams = new SortedList();
                slParams.Add("@UserID", _UserID);
                slParams.Add("@PhoneNumberID", _PhoneNumberID);
                slParams.Add("@PhoneTypeID", _PhoneTypeID);
                slParams.Add("@IDD", _IDD);
                slParams.Add("@CountryCode", _CountryCode);
                slParams.Add("@AreaCode", _AreaCode);
                slParams.Add("@PhoneNumber", _PhoneNumber);
                slParams.Add("@Extension", _Extension);
                slParams.Add("@Comments", _Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("InsUpdMDBPhoneNumbers", slParams, "LarpPortal", _UserName);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
                blnReturn = false;
            }
            return blnReturn;
        }
       

    }


}