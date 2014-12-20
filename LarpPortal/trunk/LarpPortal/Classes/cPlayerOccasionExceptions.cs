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
    public class cPlayerOccasionExceptions
    {

        private string _UserName = "";
        private Int32 _UserID = -1;
        private Int32 _PlayerOccasionExceptionID = -1;
        private Int32 _PlayerProfileID = -1;

        private Int32 _CampaignID = -1;
        private Int32 _OccasionID = -1;
        private Boolean _AttendPartial = false;
        private string _PlayerComments = "";
        private string _Comments = "";
        private DateTime _DateAdded;
        private DateTime _DateChanged;

        public Int32 PlayerOccasionExceptionID
        {
            get { return _PlayerOccasionExceptionID; }
        }
        public Int32 PlayerProfileID
        {
            get { return _PlayerProfileID; }
        }
        public Int32 CampaignID
        {
            get { return _CampaignID; }
            set { _CampaignID = value; }
        }
        public Int32 OccasionID
        {
            get { return _OccasionID; }
            set { _OccasionID = value; }
        }
        public Boolean AttendPartial
        {
            get { return _AttendPartial; }
            set { _AttendPartial = value; }
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



        private cPlayerOccasionExceptions()
        {

        }

        public cPlayerOccasionExceptions(Int32 intPlayerOcassionExceptionID, Int32 intPlayerProfileID, string strUserName, Int32 intUserID)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _PlayerOccasionExceptionID = intPlayerOcassionExceptionID;
            _PlayerProfileID = intPlayerProfileID;
            _UserName = strUserName;
            _UserID = intUserID;
            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@PlayerOccasionExceptionID", _PlayerOccasionExceptionID);
            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerOccasionExceptionByID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                      _PlayerProfileID = ldt.Rows[0]["PlayerProfileID"].ToString().ToInt32();
                      _CampaignID = ldt.Rows[0]["CampaignID"].ToString().ToInt32();
                      _OccasionID = ldt.Rows[0]["OccasionID"].ToString().ToInt32();
                      _AttendPartial = ldt.Rows[0]["AttendPartial"].ToString().ToBoolean();
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
                slParams.Add("@PlayerOccasionExceptionID", _PlayerOccasionExceptionID);
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                slParams.Add("@CampaignID", _CampaignID);
                slParams.Add("@OccasionID", _OccasionID);
                slParams.Add("@AttendPartial", _AttendPartial);
                slParams.Add("@PlayerComments", _PlayerComments);
                slParams.Add("@Comments", _Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("uspInsUpdPLPlayerOccasionExceptions", slParams, "LarpPoral", _UserName);
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