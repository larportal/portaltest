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
    public class cPlayerLARPResume
    {

        private string _UserName = "";
        private Int32 _UserID = -1;
        private Int32 _PlayerLARPResumeID = -1;
        private Int32 _PlayerProfileID = -1;
        private string _GameSystem = "";
        private string _Campaign = "";
        private string _AuthorGM = "";
        private Int32 _Style = -1;
        private Int32 _Genre = -1;
        private Int32 _RoleID = -1;
        private DateTime _StartDate;
        private DateTime? _EndDate;
        private string _PlayerComments;
        private string _Comments = "";
        private DateTime _DateAdded;
        private DateTime _DateChanged;

        public Int32 PlayerLARPResumeID
        {
            get { return _PlayerLARPResumeID; }
        }
        public Int32 PlayerProfileID
        {
            get { return _PlayerProfileID; }
        }
        public string GameSystem
        {
            get { return _GameSystem; }
            set { _GameSystem = value; }
        }
        public string Campaign
        {   get { return _Campaign; }
            set { _Campaign = value; }
        }
        public string AuthorGM
        {
            get { return _AuthorGM; }
            set { _AuthorGM = value; }
        }
        public Int32 Styel
        {
            get { return _Style; }
            set { _Style = value; }
        }
        public Int32 Genre
        {
            get { return _Genre; }
            set { _Genre = value; }
        }
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        public string PlayerComments
        {
            get { return _PlayerComments; }
            set { _PlayerComments = value; }
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



        private cPlayerLARPResume()
        {

        }

        public cPlayerLARPResume(Int32 intPlayerLARPResumeID, Int32 intPlayerProfileID, string strUserName, Int32 intUserID)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _PlayerLARPResumeID = intPlayerLARPResumeID;
            _PlayerProfileID = intPlayerProfileID;
            _UserName = strUserName;
            _UserID = intUserID;
            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@PlayerLARPResumeID", _PlayerLARPResumeID);
            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerLARPResumeByID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                      _PlayerProfileID = ldt.Rows[0]["PlayerProfileID"].ToString().ToInt32();
                      _GameSystem =ldt.Rows[0]["GameSystem"].ToString();
                      _Campaign = ldt.Rows[0]["Campaign"].ToString();
                      _AuthorGM = ldt.Rows[0]["AuthorGM"].ToString();
                    _Style = ldt.Rows[0]["Style"].ToString().ToInt32();
                      _Genre = ldt.Rows[0]["Genre"].ToString().ToInt32();
                      _RoleID = ldt.Rows[0]["RoleID"].ToString().ToInt32();
                      _StartDate = Convert.ToDateTime(ldt.Rows[0]["StartDate"].ToString());
                      _EndDate = Convert.ToDateTime(ldt.Rows[0]["EndDate"].ToString());
                      _PlayerComments = ldt.Rows[0]["PlayerComments"].ToString();
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
                slParams.Add("@PlayerLARPResumeID", _PlayerLARPResumeID);
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                slParams.Add("@GameSystem", _GameSystem);
                slParams.Add("@Campaign", _Campaign);
                slParams.Add("@AuthorGM", _AuthorGM);
                slParams.Add("@Style", _Style);
                slParams.Add("@Genre", _Genre);
                slParams.Add("@RoleID", _RoleID);
                slParams.Add("@StartDate", _StartDate);
                slParams.Add("@EndDate", _EndDate);
                slParams.Add("@PlayerComments", _PlayerComments);
                slParams.Add("@Comments", _Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("uspInsUpdPLPlayerLARPResumes", slParams, "LarpPoral", _UserName);
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