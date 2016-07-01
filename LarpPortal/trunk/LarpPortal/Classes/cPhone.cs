﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;



namespace LarpPortal.Classes
{
    [Serializable]
    public class cPhoneType
    {
        public int PhoneTypeID { get; set; }
        public string PhoneType { get; set; }
    }

    [Serializable()]
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

        public bool IsPrimary { get; set; }

        public Int32 PhoneNumberID
        {
            get { return _PhoneNumberID; }
            set { _PhoneNumberID = value; }
        }
        public Int32 PhoneTypeID
        {
            get { return _PhoneTypeID; }
            set
            {
                _PhoneTypeID = value;
            }
        }

        public List<cPhoneType> PhoneTypes = new List<cPhoneType>();

        public string PhoneType
        {
            get
            {
                return PhoneTypes.Find(x => x.PhoneTypeID == PhoneTypeID).PhoneType;
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

        public cPhone()
        {
            PhoneNumberID = -1;

            SortedList sParams = new SortedList();

            DataTable dtPhoneTypes = cUtilities.LoadDataTable("uspGetPhoneTypes", sParams, "LARPortal", "", "cPhone.Creator");

            PhoneTypes = new List<cPhoneType>();
            if (new DataView(dtPhoneTypes, "PhoneTypeID = 0", "", DataViewRowState.CurrentRows).Count == 0)
            {
                cPhoneType NewPhoneNumber = new cPhoneType();
                NewPhoneNumber.PhoneTypeID = 0;
                NewPhoneNumber.PhoneType = "Choose...";
                PhoneTypes.Add(NewPhoneNumber);
            }

            foreach (DataRow dRow in dtPhoneTypes.Rows)
            {
                cPhoneType NewPhoneNumber = new cPhoneType();
                NewPhoneNumber.PhoneTypeID = dRow["PhoneTypeID"].ToString().ToInt32();
                NewPhoneNumber.PhoneType = dRow["PhoneType"].ToString();
                PhoneTypes.Add(NewPhoneNumber);
            }

            PhoneTypeID = 0;
        }

        public cPhone(Int32 intPhoneNumberID, Int32 intUserID, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _PhoneNumberID = intPhoneNumberID;
            _UserID = intUserID;
            _UserName = strUserName;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@intPhoneNumberID", _PhoneNumberID);
                DataSet lds = cUtilities.LoadDataSet("uspGetPhoneNumber", slParams, "LARPortal", _UserName, lsRoutineName);
                if (lds.Tables[0].Rows.Count > 0 )
                {
                    DataRow dRow = lds.Tables[0].Rows[0];
                    _PhoneTypeID = dRow["PhoneTypeID"].ToString().Trim().ToInt32();
                    if (dRow["PrimaryPhone"] != null) { IsPrimary = (bool)dRow["PrimaryPhone"]; }
                    
                    _PhoneTypeDescription = dRow["PhoneType"].ToString();
                    _IDD = dRow["IDD"].ToString();
                    _CountryCode = dRow["CountryCode"].ToString();
                    _AreaCode = dRow["AreaCode"].ToString();
                    _PhoneNumber = dRow["PhoneNumber"].ToString();
                    _Extension = dRow["Extension"].ToString();
                    _Comments = dRow["Comments"].ToString();
                }
                if (lds.Tables.Count > 1)
                {
                    PhoneTypes = new List<cPhoneType>();
                    foreach (DataRow dRow in lds.Tables[1].Rows)
                    {
                        cPhoneType NewPhoneNumber = new cPhoneType();
                        NewPhoneNumber.PhoneTypeID = dRow["PhoneTypeID"].ToString().ToInt32();
                        NewPhoneNumber.PhoneType = dRow["PhoneType"].ToString();
                        PhoneTypes.Add(NewPhoneNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }

        //private void GetPhoneTypeDescription()
        //{

        //    MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
        //    string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            
        //    try
        //    {
        //        SortedList slParams = new SortedList();
        //        slParams.Add("@PhoneTypeID", _PhoneTypeID);
        //        _PhoneTypeDescription = cUtilities.ReturnStringFromSQL("somestoredprocedure", "PhoneType", slParams, "LarpPortal", _UserName, lsRoutineName);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorAtServer lobjError = new ErrorAtServer();
        //        lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
                
        //    }            
        //}

        public static bool isValidPhoneNumber(string strPhone, int length)
        {
            ErrorDescription = string.Empty;

            if (string.IsNullOrWhiteSpace(strPhone))
            {
                ErrorDescription = "Phone Number cannont be empty";
                if (length == 3)
                    ErrorDescription = "Area Code cannot be empty";
                return false;
            }

            strPhone = strPhone.Trim();
            if (length == 10)
            {
                Regex phoneExp = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
                if (phoneExp.IsMatch(strPhone) == false)
                {
                    ErrorDescription = "Phone number must be a 10 digit number";
                    return false;
                }
                return true;
            }

            if (length == 7)
            {
                Regex phoneExp = new Regex(@"^([0-9]{3})[-. ]?([0-9]{4})$");
                if (phoneExp.IsMatch(strPhone) == false)
                {
                    ErrorDescription = "Phone number must be a 7 digit number";
                    return false;
                }
                return true;
            }
            //Make sure all values are digits
            if (strPhone.All(x => Char.IsDigit(x)) == false)
                return false;
            //This line is a substitute to remove any non-digits and only if we ever disable check above
            //string strPhone = string.Join(string.Empty, strPhone.Where(x => Char.IsDigit(x)).ToArray());

            //800s, 900, and zero digits on first position are not okay
            //if (strPhone.StartsWith("8") || strPhone.StartsWith("9") || strPhone.StartsWith("0"))
            //    return false;

            // Get all the digits from the string and make sure we have ten numeric value
            return (strPhone.Length == length);
        }

        public string strErrorDescription { get; private set; }

        public static string ErrorDescription { get; private set; }

        public bool isValidPhoneNumber()
        {            
            return isValidPhoneNumber(AreaCode + PhoneNumber, 10);
        }

        public bool IsValid()
        {
            strErrorDescription = string.Empty;

            if (isValidPhoneNumber(AreaCode, 3) == false)
            {
                strErrorDescription = (AreaCode + "") + " is not a valid Area Code must be a 3 digit number";
                return false;
            }

            if (isValidPhoneNumber(PhoneNumber, 7) == false)
            {
                strErrorDescription = (PhoneNumber + "") + " is not a valid Phone Number must be a 7 digit number";
                return false;
            }
            return true;
        }

        public Boolean SaveUpdate(int userID, bool delete = false)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            Boolean bUpdateComplete = false;
            Boolean blnReturn = false;
            try
            {
                SortedList slParams = new SortedList();
                if (delete)
                {
                    slParams.Add("@RecordID", PhoneNumberID);
                    slParams.Add("@UserID", userID);
                    bUpdateComplete = cUtilities.PerformNonQueryBoolean("uspDelMDBPhoneNumbers", slParams, "LARPortal", _UserName + string.Empty);
                }
                else
                {
                    slParams.Add("@UserID", userID);
                    slParams.Add("@PhoneNumberID", _PhoneNumberID);
                    slParams.Add("@KeyID", userID);
                    slParams.Add("@KeyType", "cUser"); //I did to hard code this because the get uses this value and there is no property to set for this                
                    slParams.Add("@PhoneTypeID", _PhoneTypeID);
                    slParams.Add("@PrimaryPhone", IsPrimary);
                    slParams.Add("@IDD", _IDD);
                    slParams.Add("@CountryCode", _CountryCode + string.Empty); //if null insert empty string
                    slParams.Add("@AreaCode", _AreaCode + string.Empty);
                    slParams.Add("@PhoneNumber", _PhoneNumber + string.Empty);
                    slParams.Add("@Extension", _Extension + string.Empty);
                    slParams.Add("@Comments", _Comments + string.Empty);
                    blnReturn = cUtilities.PerformNonQueryBoolean("uspInsUpdMDBPhoneNumbers", slParams, "LARPortal", _UserName);
                }
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