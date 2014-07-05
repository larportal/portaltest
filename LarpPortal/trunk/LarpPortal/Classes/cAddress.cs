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
    public class cAddress
    {

        private Int32 _AddressID = -1;
        private string _Address1 = "";
        private string _Address2 = "";
        private string _City = "";
        private string _StateID = "";
        private string _PostalCode = "";
        private string _Country = "";
        private string _Comments = "";
        private DateTime _DateAdded = DateTime.Now;
        private DateTime _DateChanged = DateTime.Now;
        private DateTime? _DateDeleted = null;
        private string _UserName = "";
        private Int32 _UserID = -1;

        public Int32 AddressID
        {
            get { return _AddressID; }
        }
        public string Address1
        {   get {return _Address1;}
            set { _Address1 = value; }
        }
        public string Address2
        { get {return _Address2;}
          set {_Address2 = value;}
        }
        public string City
        { get {return _City;}
          set {_City = value;}
        }
        public string StateID
        { get{ return _StateID;}
          set {_StateID = value;}
        }
        public string PostalCode
        {
            get {return _PostalCode;}
            set {_PostalCode = value;}
        }
        public string Country
        {
            get {return _Country;}
            set {_Country = value;}
        }
        public string Comments
        {
            get {return _Comments;}
            set {_Comments = value;}
        }
        public DateTime DateAdded
        {
            get {return _DateAdded;}
        }
        public DateTime DateChanged
        {
            get {return _DateChanged;}
        }
        public DateTime? DateDeleted
        {
            get {return _DateDeleted;}
        }
        private cAddress()
        {

        }

        public cAddress(Int32 intID, string strUserName, Int32 intUserID)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _UserName = strUserName;
            _AddressID = intID;
            _UserID = intUserID;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@AddressID", _AddressID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetSomeData", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _Address1 = ldt.Rows[0]["Address1"].ToString();
                    _Address2 = ldt.Rows[0]["Address2"].ToString();
                    _City = ldt.Rows[0]["City"].ToString();
                    _StateID = ldt.Rows[0]["StateID"].ToString();
                    _Country = ldt.Rows[0]["Country"].ToString();
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
                slParams.Add("@UserID",_UserID);
                slParams.Add("@AddressID", _AddressID);
                slParams.Add("@Address1", _Address1);
                slParams.Add("@Address2", _Address2);
                slParams.Add("@City", _City);
                slParams.Add("@StateID", _StateID);
                slParams.Add("@PostalCode", _PostalCode);
                slParams.Add("@Country", _Country);
                slParams.Add("@Comments", _Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("InsUpdMDBAddresses",slParams,"LarpPortal",_UserName);
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