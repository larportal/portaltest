using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;
using System.Data;
namespace LarpPortal.Classes
{
    [Serializable()]
    public class cAddress
    {
        string strUserName = "";
        /// <summary>
        /// Table: MDBAddresses Field: AddressID  Notes: This field identifies the address record
        /// </summary>
        private int _IntAddressID = -1;
        public int IntAddressID
        {
            get { return _IntAddressID; }
            set
            {
                if (value.GetType() == typeof(int))
                {
                    _IntAddressID = value;
                }
                else
                {
                    // TODO add error handling for invalid types passed to class and security issues IE injection attempts inserted into variables
                }


            }
        }
        /// <summary>
        /// Table: MDBAddresses	Field: Address1 Notes: This field indicates address 1
        /// </summary>
        private string _StrAddress1 = ""; //Table MDBAddresses
        public string StrAddress1
        {
            get { return _StrAddress1; }
            set { _StrAddress1 = value; }
        }
        /// <summary>
        /// Table: MDBAddresses	Field: Address2 Notes: This field indicates address 2
        /// </summary>
        private string _StrAddress2 = ""; //Table MDBAddresses
        public string StrAddress2
        {
            get { return _StrAddress2; }
            set { _StrAddress2 = value; }
        }
        /// <summary>
        /// Table: MDBAddresses	Field: City Notes: This Field Indicates City
        /// </summary>
        private string _StrCity = ""; //Table MDBAddresses
        public string StrCity
        {
            get { return _StrCity; }
            set { _StrCity = value; }
        }
        /// <summary>
        /// Table: MDBAddreses Field: StateID Notes: This Field Indicates State/Province
        /// </summary>
        private string _StrStateID = ""; //Table MDBAddresses
        public string StrStateID
        {
            get { return _StrStateID; }
            set { _StrStateID = value; }
        }
        /// <summary>
        /// Table MDBAddresses Field: PostalCode Notes: This Field Incidactes Zip code / postal code for international
        /// </summary>
        private string _StrPostalCode = ""; //Table MDBAddresses
        public string StrPostalCode
        {
            get { return _StrPostalCode; }
            set { _StrPostalCode = value; }
        }
        /// <summary>
        /// Table MDBAddresses Field: Country Notes: This field indicates country
        /// </summary>
        private string _StrCountry = ""; //Table MDBAddresses
        public string StrCountry
        {
            get { return _StrCountry; }
            set { _StrCountry = value; }
        }
        /// <summary>
        /// Table MDBAddressed Field: Comments.
        /// </summary>
        private string _StrComments = "";
        public string StrComments
        {
            get { return _StrComments; }
            set { _StrComments = value; }
        }
        /// <summary>
        /// Table: MDBAddresses Field: DateAdded Notes: Date Added
        /// </summary>
        private DateTime _DateAdded = DateTime.Now;
        public DateTime DateAdded
        {
            get { return _DateAdded; }
            set
            {
                if (value.GetType() == typeof(DateTime))
                {
                    _DateAdded = value;
                }
                else
                {
                    // TODO add error handling for invalid types passed to class and security issues IE injection attempts inserted into variables
                }
            }
        }
        /// <summary>
        /// Table: MDBAddresses Field: DateChanged Notes: Date Changed
        /// </summary>
        private DateTime _DateChanged = DateTime.Now;
        public DateTime DateChanged
        {
            get { return _DateChanged; }
            set
            {
                if (value.GetType() == typeof(DateTime))
                {
                    _DateChanged = value;
                }
                else
                {
                    // TODO add error handling for invalid types passed to class and security issues IE injection attempts inserted into variables
                }
            }
        }
        /// <summary>
        /// Table: MDBAddresses Field: DateDeleted Notes: Date Deleted
        /// </summary>
        private DateTime _DateDeleted = DateTime.Now;
        public DateTime DateDeleted
        {
            get { return _DateDeleted; }
            set
            {
                if (value.GetType() == typeof(DateTime))
                {
                    _DateDeleted = value;
                }
                else
                {
                    // TODO add error handling for invalid types passed to class and security issues IE injection attempts inserted into variables
                }
            }
        }

        /// <summary>
        /// Table MDBStates Field StateName Notes: Name of the state translated from StateID field in MDBAddresses
        /// </summary>
        private string _StateName = ""; //Table MDBStates
        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }

        private string _StrGoogleString;
        public string GoogleString
        {
            get { return _StrGoogleString; }

        }
        /// <summary>
        ///  Auto Generated link to location in google maps.  Only generated if keytype is not player
        /// </summary>
        private string _StrGoogleLink = "";
        public string StrGoogleLink
        {
            get { return "http://maps.google.com/preview?q=" + _StrGoogleString; }
        }
        /// <summary>
        /// Creates Empty caddress class object
        /// </summary>
        public cAddress()
        {

        }
        /// <summary>
        /// Creates a caddress class object from data stored in the database with the Address ID provided
        /// </summary>
        /// <param name="intAddressID">ID for Address</param>
        /// <param name="userName">User ID</param>
        public cAddress(int intAddressID, string strUserNameParam, int intuserID)
        {
            strUserName = strUserNameParam;
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;


            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@Parameter1", intAddressID);
            slParams.Add("@Parameter2", intuserID);

            try
            {
                // SELECT AddressID, KeyID, AddressTypeID, KeyType, Address1, Address2, City, StateID, PostalCode, Country, MDBStates.StateName FROM MDBAddresses LEFT JOIN MDBStates ON MDBStates.StateID = MDBAddresses.StateID AND AddressID = @param
                DataTable ldt = cUtilities.LoadDataTable("uspGetAddress", slParams, "DefaultSQLConnection", strUserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {

                    _IntAddressID = intAddressID;//Table MDBAddresses
                    if (ldt.Rows[0]["Address1"] != null) { _StrAddress1 = ldt.Rows[0]["Address1"].ToString(); } //Table MDBAddresses
                    if (ldt.Rows[0]["Address2"] != null) { _StrAddress2 = ldt.Rows[0]["Address2"].ToString(); } //Table MDBAddresses
                    if (ldt.Rows[0]["City"] != null) { _StrCity = ldt.Rows[0]["City"].ToString(); } //Table MDBAddresses
                    if (ldt.Rows[0]["StateID"] != null) { _StrStateID = ldt.Rows[0]["StateID"].ToString(); }//Table MDBAddresses
                    if (ldt.Rows[0]["PostalCode"] != null) { _StrPostalCode = ldt.Rows[0]["PostalCode"].ToString(); } //Table MDBAddresses
                    if (ldt.Rows[0]["Country"] != null) { _StrCountry = ldt.Rows[0]["Country"].ToString(); } //Table MDBAddresses
                    if (ldt.Rows[0]["MDBStatesStateName"] != null) { _StateName = ldt.Rows[0]["MDBStatesStateName"].ToString(); } //Table Table MDBStates
                    if (ldt.Rows[0]["Comments"] != null) { _StrComments = ldt.Rows[0]["Comments"].ToString(); }
                    _DateAdded = (DateTime)ldt.Rows[0]["DateAdded"];
                    _DateChanged = (DateTime)ldt.Rows[0]["DateChanged"];
                    _DateDeleted = (DateTime)ldt.Rows[0]["DateDeleted"];
                    _StrGoogleString = _StrAddress1 + "+" + _StrAddress2 + "+" + _StrCity + "+" + _StrStateID + "+" + _StrPostalCode;
                }

            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName + lsRoutineName);
            }

        }
        /// <summary>
        /// Deletes, Updates, or Creates Address in the database
        /// </summary>
        /// <param name="option">String argument to tell the function what to do. u for Update, d for Delete, i for insert, defaults to update</param>
        /// <returns>True on success</returns>
        public Boolean SaveUpdate(int userID, bool delete = false)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            Boolean bUpdateComplete = false;
            SortedList slParams = new SortedList();
            try
            {
                if(delete)
                {
                    slParams.Add("@Paramater1", userID);
                    slParams.Add("@Paramater2", _IntAddressID);
                    bUpdateComplete = cUtilities.PerformNonQueryBoolean("upsDelMDBAddresses", slParams, "DefaultSQLConnection", strUserName);
                }
                else
                {
                    slParams.Add("@Paramater1", userID);
                    slParams.Add("@Paramater2", _IntAddressID);
                    slParams.Add("@Paramater3", _StrAddress1);
                    slParams.Add("@Paramater4", _StrAddress2);
                    slParams.Add("@Paramater5", _StrCity);
                    slParams.Add("@Paramater6", _StrStateID);
                    slParams.Add("@Paramater7", _StrPostalCode);
                    slParams.Add("@Paramater8", _StrCountry);
                    slParams.Add("@Parameter9", _StrComments);
                    bUpdateComplete = cUtilities.PerformNonQueryBoolean("uspInsMDBAddresses", slParams, "DefaultSQLConnection", strUserName);
                    
                }

            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName + lsRoutineName);
            }

            return bUpdateComplete;

        }

    }
}