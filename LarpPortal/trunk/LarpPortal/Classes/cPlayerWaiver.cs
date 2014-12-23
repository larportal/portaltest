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
    public class cPlayerWaiver
    {

        private string _UserName = "";
        private Int32 _UserID = -1;
        private Int32 _PlayerWaiverID = -1;
        private Int32 _PlayerProfileID = -1;
        private Int32 _WaiverID = -1;
        private DateTime? _AcceptedDate;
        private string _WaiverImage;
        private DateTime? _DeclinedDate;
        private Int32 _DeclineApprovedBy;
        private string _PlayerNotes = "";
        private string _StaffNotes = "";
        private string _Comments = "";
        private DateTime _DateAdded;
        private DateTime _DateChanged;

        public Int32 PlayerWaiverID
        {
            get { return _PlayerWaiverID; }
        }
        public Int32 PlayerProfileID
        {
            get { return _PlayerProfileID; }
        }
        public Int32 WaiverID
        {
            get { return _WaiverID; }
            set { _WaiverID = value; }
        }
        public DateTime? AccpetedDate
        {
            get { return _AcceptedDate; }
            set { _AcceptedDate = value; }
        }
        public string WaiverImage
        {
            get { return _WaiverImage; }
            set { _WaiverImage = value; }
        }
        public DateTime? DeclinedDate
        {
            get { return _DeclinedDate; }
            set { _DeclinedDate = value; }
        }
        public Int32 DeclineApprovedBy
        {
            get { return _DeclineApprovedBy; }
            set { _DeclineApprovedBy = value; }
        }
        public string PlayerNotes
        {
            get { return _PlayerNotes; }
            set { _PlayerNotes = value; }
        }
        public string StaffNotes
        {
            get { return _StaffNotes; }
            set { _StaffNotes = value; }
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



        private cPlayerWaiver()
        {

        }

        public cPlayerWaiver(Int32 intPlayerWaiverID, Int32 intPlayerProfileID, string strUserName, Int32 intUserID)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _PlayerWaiverID = intPlayerWaiverID;
            _PlayerProfileID = intPlayerProfileID;
            _UserName = strUserName;
            _UserID = intUserID;
            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@PlayerWaiverID", _PlayerWaiverID);
            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerWaiverByID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                      _PlayerProfileID = ldt.Rows[0]["PlayerProfileID"].ToString().ToInt32();
                      _AcceptedDate = Convert.ToDateTime(ldt.Rows[0]["AcceptedDate"].ToString());
                      _WaiverImage = ldt.Rows[0]["WaiverImage"].ToString();
                      _DeclinedDate = Convert.ToDateTime(ldt.Rows[0]["DeclinedDate"].ToString());
                      _DeclineApprovedBy = ldt.Rows[0]["DeclineApprovedByID"].ToString().ToInt32();
                      _PlayerNotes = ldt.Rows[0]["PlayerNotes"].ToString();
                      _StaffNotes = ldt.Rows[0]["StaffNotes"].ToString();
                      _Comments = ldt.Rows[0]["Comments"].ToString();
                      _DateAdded = Convert.ToDateTime(ldt.Rows[0]["DateAdded"].ToString());
                      _DateChanged = Convert.ToDateTime(ldt.Rows[0]["DateChanged"].ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
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
                slParams.Add("@UserID", _UserID);
                slParams.Add("@PlayerWaiverID", _PlayerWaiverID);
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                slParams.Add("@WaiverID", _WaiverID);
                slParams.Add("@AcceptedDate", _AcceptedDate);
                slParams.Add("@WaiverImage", _WaiverImage);
                slParams.Add("@DeclinedDate", _DeclinedDate);
                slParams.Add("@DeclineApprovedByID", _DeclineApprovedBy);
                slParams.Add("@PlayerNotes", _PlayerNotes);
                slParams.Add("@StaffNotes", _StaffNotes);
                slParams.Add("@Comments", _Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("uspInsUpdPLPlayerWaivers", slParams, "LarpPoral", _UserName);
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