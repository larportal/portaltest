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
        //private string _UserName = "";

        public Int32 CampaignPlayerID
        {
            get { return _CampaignPlayerID; }
            set { _CampaignPlayerID = value; }
        }
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public Int32 CampaignID
        {
            get { return _CampaignID; }
            set { _CampaignID = value; }
        }
        public string CampaignName
        {
            get { return _CampaignName; }
            set { _CampaignName = value; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        public Int32 LastLoggedInCampaign
        {
            get { return _LastLoggedInCampaign; }
            set { _LastLoggedInCampaign = value; }
        }
        public string Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }

        /// <summary>
        /// This will load the details of a particular user campaign
        /// Must pass a UserID
        /// CampaignIDToLoad to return a specific campaign
        /// </summary>
        public void Load(int UserID, int CampaignIDToLoad)
        {
            string stStoredProc = "uspGetMyCampaigns";
            string stCallingMethod = "cUserCampaign.Load";
            int iTemp;
            SortedList slParameters = new SortedList();
            slParameters.Add("@UserID", UserID);
            slParameters.Add("@CampaignID", CampaignIDToLoad);
            DataSet dsUserCampaign = new DataSet();
            dsUserCampaign = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsUserCampaign.Tables[0].TableName = "CMCampaignPlayers";

            foreach (DataRow dRow in dsUserCampaign.Tables["CMCampaignPlayers"].Rows)
            {
                if (int.TryParse(dRow["CampaignPlayerID"].ToString(), out iTemp))
                    CampaignPlayerID = iTemp;
                if (int.TryParse(dRow["UserID"].ToString(), out iTemp))
                    UserID = iTemp;
                if (int.TryParse(dRow["CampaignID"].ToString(), out iTemp))
                    CampaignID = iTemp;
                if (int.TryParse(dRow["LastLoggedInCampaign"].ToString(), out iTemp))
                    LastLoggedInCampaign = iTemp;
                FirstName = dRow["FirstName"].ToString();
                LastName = dRow["LastName"].ToString();
                CampaignName = dRow["CampaignName"].ToString();
                EmailAddress = dRow["EmailAddress"].ToString();
                Sort = dRow["Sort"].ToString();
            }
        }


        /// <summary>
        /// Save will handle both insert new record and update old record.
        /// Set CampaignPlayerID = -1 for insert and CampaignPlayerID = record number to update for existing record.
        /// </summary>
        public void Save(int UserID)
        {
            // I need to finish defining this one
            //string stStoredProc = "uspInsUpdCMCampaignPlayers";
            //string stCallingMethod = "cUserCampaign.Save";
            //SortedList slParameters = new SortedList();
            //slParameters.Add("@UserID", UserID);
            //slParameters.Add("@CampaignPlayerRoleID", CampaignPlayerRoleID);
            //slParameters.Add("@RoleAlignmentID", RoleAlignmentID);
            //slParameters.Add("@CPEarnedForRole", CPEarnedForRole);
            //slParameters.Add("@CPQuantityEarnedPerEvent", CPQuantityEarnedPerEvent);
            //if (@CampaignPlayerRoleID == -1) // Set fields that can only be set on insert of new record
            //{
            //    slParameters.Add("@CampaignPlayerID", CampaignPlayerID);
            //    slParameters.Add("@RoleID", RoleID);
            //    slParameters.Add("@RoleAlignmentID", RoleAlignmentID);
            //}
            //cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
        }

        public void Delete(int RecordID, int UserID)
        {
            string stStoredProc = "uspDelCMCampaignPlayers";
            SortedList slParameters = new SortedList();
            slParameters.Add("@RecordID", CampaignPlayerID);
            slParameters.Add("@UserID", UserID.ToString());
            cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
        }
    }
}