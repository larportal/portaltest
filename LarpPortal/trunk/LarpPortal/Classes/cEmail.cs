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
    public class cEMail
    {

        private Int32 _EMailID = -1;
        private string _EMailAddress = "";
        private string _Comments = "";
        private DateTime _DateAdded = DateTime.Now;
        private DateTime _DateChanged = DateTime.Now;
        private DateTime? _DateDeleted = null;
        private string _UserName = "";
        private Int32 _UserID = -1;

        // ID for the EMail Address
        public Int32 EMailID
        {
            get { return _EMailID; }
            set { _EMailID = value; }
        }
        
        //Email Address
        public string EmailAddress
        {
            get { return _EMailAddress; }
            set { _EMailAddress = value; }
        }
        // Comments about the email address
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        //Date Email Address Added
        public DateTime DateAdded
        {
            get { return _DateAdded; }
            //set { _DateAdded = value; }
        }
        //Date Email Address Changed
        public DateTime DateChanged
        {
            get { return _DateChanged; }
            //set { _DateChanged = value; }
        }
        // Date Email Address Flagged as Deleted, null vlaue if not deleted
        public DateTime? DateDeleted
        {
            get { return _DateDeleted; }
            //set { _DateDeleted = value; }
        }

        private cEMail()
        {

        }

        // Passing an ID of -1 should produce the devault values, and on update should generate a new record. 
        public cEMail(Int32 intEMailID, string strUserName, Int32 intUserID)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _UserName = strUserName;
            _UserID = intUserID;
            _EMailID = intEMailID;
            try
            {
                SortedList slParams = new SortedList();
                slParams.Add("@intEMailID", intEMailID);
                
                // need to put the correct stored procedure in to the class
                DataTable ldt = cUtilities.LoadDataTable("uspGetSomeData", slParams, "DefaultSQLConnection", strUserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _EMailID = ldt.Rows[0]["EMailID"].ToString().ToInt32();
                    _EMailAddress = ldt.Rows[0]["EMailAddress"].ToString().Trim();
                    _Comments = ldt.Rows[0]["Comments"].ToString().Trim();
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

        public Boolean SaveUpdate()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            Boolean blnReturn = false;
            try
            {
                SortedList slParams = new SortedList();
                slParams.Add("@UserID", _UserID);
                slParams.Add("@EMailID", _EMailID);
                slParams.Add("@EmailAddress", _EMailAddress);
                slParams.Add("@Comments", _Comments);
                //slParams.Add("@DateAdded", _DateAdded);
                //slParams.Add("@DateChanged", _DateChanged);
                //slParams.Add("@DateDeleted", _DateDeleted);
                blnReturn = cUtilities.PerformNonQueryBoolean("InsUpdMDBEmails", slParams, "LARPortal", _UserName);
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