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
    public class cUserCampaign
    {
        private Int32 _CampaignPlayerID = -1;
        private Int32 _UserID = -1;
        private string _FirstName = "";
        private string _LastName = "";
        private Int32 _CampaignID = -1;
        private string _CampaignName = "";
        private string _EmailAddress = "";
        private Int32 _LastLoggedInCampaign;
        private string _Sort = "";
        private string _UserName = "";

        public Int32 CampaignPlayerID
        {
            get{ return _CampaignPlayerID;}
        }
        public Int32 UserID
        {
            get{return _UserID;}
        }
        public string FirstName
        {
            get { return _FirstName; }
        }
        public string LastName
        {
            get { return _LastName; }
        }
        public Int32 CampaignID
        {
            get { return _CampaignID; }
        }
        public string CampaignName
        {
            get { return _CampaignName; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
        }
        public Int32 LastLoggedInCampaign
        {
            get { return _LastLoggedInCampaign; }
        }
        public string Sort
        {
            get { return _Sort; }
        }

        private cUserCampaign()
        {

        }

        public cUserCampaign(Int32 intUserID, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _UserID = intUserID;
            _UserName = strUserName;
            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@UserID", _UserID);
                DataTable ldt = cUtilities.LoadDataTable("uspGetMyCampaigns", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _CampaignPlayerID = ldt.Rows[0]["CampaignPlayerID"].ToString().Trim().ToInt32();
                    _UserID = ldt.Rows[0]["UserID"].ToString().Trim().ToInt32();
                    _CampaignID = ldt.Rows[0]["CampaignID"].ToString().Trim().ToInt32();
                    _LastLoggedInCampaign = ldt.Rows[0]["LastLoggedInCampaign"].ToString().Trim().ToInt32();
                    _FirstName = ldt.Rows[0]["FirstName"].ToString();
                    _LastName = ldt.Rows[0]["LastName"].ToString();
                    _CampaignName = ldt.Rows[0]["CampaignName"].ToString();
                    _EmailAddress = ldt.Rows[0]["EmailAddress"].ToString();
                    _Sort = ldt.Rows[0]["Sort"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }

    }


}