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
    public class cPlayerSkill
    {

        private string _UserName = "";
        private Int32 _UserID = -1;
        private Int32 _PlayerSkillID = -1;
        private Int32 _PlayerProfileID = -1;
        private string _SkillName = "";
        private string _SkillLevel = "";
        private string _PlayerComments = "";
        private string _Comments = "";
        private DateTime _DateAdded;
        private DateTime _DateChanged;

        public Int32 PlayerSkillID
        {
            get { return _PlayerSkillID; }
        }
        public Int32 PlayerProfileID
        {
            get { return _PlayerProfileID; }
        }
        public string SkillName
        {
            get { return _SkillName; }
            set { _SkillName = value; }
        }
        public string SkillLevel
        {
            get { return _SkillLevel; }
            set { _SkillLevel = value; }
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



        private cPlayerSkill()
        {

        }

        public cPlayerSkill(Int32 intPlayerSkillID, Int32 intPlayerProfileID, string strUserName, Int32 intUserID)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _PlayerSkillID = intPlayerSkillID;
            _PlayerProfileID = intPlayerProfileID;
            _UserName = strUserName;
            _UserID = intUserID;
            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@PlayerSkillID", _PlayerSkillID);
            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerSkilssByID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                      _PlayerProfileID = ldt.Rows[0]["PlayerProfileID"].ToString().ToInt32();
                      _SkillName = ldt.Rows[0]["SkillName"].ToString();
                      _SkillLevel = ldt.Rows[0]["SkillLevel"].ToString();
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
                slParams.Add("@PlayerSkillID", _PlayerSkillID);
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                slParams.Add("@SkillName", _SkillName);
                slParams.Add("@SkillLevel", _SkillLevel);
                slParams.Add("@PlayerComments", _PlayerComments);
                slParams.Add("@Comments", _Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("uspInsUpdPLPlayerSkills", slParams, "LarpPoral", _UserName);
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