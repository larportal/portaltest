using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace LarpPortal.Classes
{
    public class cLogin
    {
        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public int MemberID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewUsersName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmailID { get; set; }
        public string Email { get; set; }
        public int SecurityRoleID { get; set; }
        public string LastLoggedInLocation { get; set; }
        public int LastLoggedInCampaign { get; set; }
        public string SiteOperationalMode { get; set; }
        public string SiteFooter { get; set; }
        public int SecurityRoleTabID { get; set; }
        public string SecurityRoleName { get; set; }
        public string CallsPageName { get; set; }
        public string TabName { get; set; }
        public int SortOrder { get; set; }
        public int TabCount { get; set; }
        public string TabClass { get; set; }
        public string TabIcon { get; set; }
        public string WhatIsLARPingText { get; set; }
        public string AboutUsText { get; set; }
        public List<cPageTab> lsPageTabs = new List<cPageTab>();

        /// <summary>
        /// This will check the parameters table of the site to see if the site is in test mode or production mode
        /// Nothing needs to be passed.  The stored proc will return the mode and set the site to that mode.
        /// Don't fuck with it unless you know what you're doing.
        /// </summary>
        public void SetSiteOperationalMode()
        {
            string stStoredProc = "uspGetSiteOperationalMode";
            string stCallingMethod = "cLogin.SetSiteOperationalMode";
            SortedList slParameters = new SortedList();
            DataSet dsMode = new DataSet();
            dsMode = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsMode.Tables[0].TableName = "MDBParameters";
            foreach (DataRow dRow in dsMode.Tables["MDBParameters"].Rows)
            {
                SiteOperationalMode = dRow["OperationalMode"].ToString();
            }
        }

        /// <summary>
        /// This will get the site footer from the database
        /// </summary>
        public void SetPageFooter()
        {
            string stStoredProc = "uspGetPageFooter";
            string stCallingMethod = "cLogin.SetPageFooter";
            SortedList slParameters = new SortedList();
            DataSet dsFooter = new DataSet();
            dsFooter = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsFooter.Tables[0].TableName = "MDBParameters";
            foreach (DataRow dRow in dsFooter.Tables["MDBParameters"].Rows)
            {
                SiteFooter = dRow["ParameterValue"].ToString();
            }

        }

        /// <summary>
        /// This will get the text from the database for What Is LARPing?
        /// </summary>
        public void getWhatIsLARPing()
        {
            string stStoredProc = "uspGetWhatIsLARPing";
            string stCallingMethod = "cLogin.WhatIsLARPing";
            SortedList slParameters = new SortedList();
            DataSet dsWhatsLARPing = new DataSet();
            dsWhatsLARPing = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsWhatsLARPing.Tables[0].TableName = "MDBParameters";
            foreach (DataRow dRow in dsWhatsLARPing.Tables["MDBParameters"].Rows)
            {
                WhatIsLARPingText = dRow["Comments"].ToString();
            }
        }

        /// <summary>
        /// This will get the text from the database for About Us
        /// </summary>
        public void getAboutUs()
        {
            string stStoredProc = "uspGetAboutUs";
            string stCallingMethod = "cLogin.getAboutUs";
            SortedList slParameters = new SortedList();
            DataSet dsAboutUs = new DataSet();
            dsAboutUs = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsAboutUs.Tables[0].TableName = "MDBParameters";
            foreach (DataRow dRow in dsAboutUs.Tables["MDBParameters"].Rows)
            {
                AboutUsText = dRow["Comments"].ToString();
            }
        }

        /// <summary>
        /// This will load the tabs associated with a given security role.
        /// Requires a security roleID
        /// </summary>
        public void LoadTabsBySecurityRole(int SecurityRoleID)
        {
            int iTemp;
            TabCount = 0;
            string stStoredProc = "uspGetSecurityRoleTabs";
            string stCallingMethod = "cLogin.LoadTabsBySecurityRole";
            SortedList slParameters = new SortedList();
            slParameters.Add("@SecurityRoleID", SecurityRoleID);
            DataSet dsTabs = new DataSet();
            dsTabs = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsTabs.Tables[0].TableName = "MDBSecurityRoleTabs";
            foreach (DataRow dRow in dsTabs.Tables["MDBSecurityRoleTabs"].Rows)
            {
                TabCount = TabCount + 1;
                if (int.TryParse(dRow["SecurityRoleTabID"].ToString(), out iTemp))
                    SecurityRoleTabID = iTemp;
                if (int.TryParse(dRow["SecurityRoleID"].ToString(), out iTemp))
                    SecurityRoleID = iTemp;
                if (int.TryParse(dRow["SortOrder"].ToString(), out iTemp))
                    SortOrder = iTemp;
                SecurityRoleName = dRow["SecurityRoleName"].ToString();
                CallsPageName = dRow["CallsPageName"].ToString();
                TabName = dRow["TabName"].ToString();
                TabClass = dRow["TabClass"].ToString();
                TabIcon = dRow["TabIcon"].ToString();
                cPageTab PageTab = new cPageTab();
                PageTab.Load(SecurityRoleTabID);
                lsPageTabs.Add(PageTab);
            }
        }

        /// <summary>
        /// This will load the login information about a member
        /// Must pass a username and password
        /// </summary>
        public void Load(string Username, string Password)
        {
            string stStoredProc = "uspValidateMemberLogin";
            string stCallingMethod = "cLogin.Load";
            int iTemp;
            int UserID = 0; // Until the member login is verified, there is no UserID so we'll use the guest ID
            //bool bTemp;
            //DateTime dtTemp;
            //double dTemp;
            SortedList slParameters = new SortedList();
            slParameters.Add("@Username", Username);
            slParameters.Add("@Password", Password);
            DataSet dsMember = new DataSet();
            dsMember = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsMember.Tables[0].TableName = "MDBUsers";

            foreach (DataRow dRow in dsMember.Tables["MDBUsers"].Rows)
            {
                if (int.TryParse(dRow["UserID"].ToString(), out iTemp))
                    UserID = iTemp;
                if (int.TryParse(dRow["EmailID"].ToString(), out iTemp))
                    EmailID = iTemp;
                if (int.TryParse(dRow["SecurityRoleID"].ToString(), out iTemp))
                    SecurityRoleID = iTemp;
                if (int.TryParse(dRow["LastLoggedInCampaign"].ToString(), out iTemp))
                    LastLoggedInCampaign = iTemp;
                MemberID = UserID;
                Username = dRow["LoginUsername"].ToString();
                FirstName = dRow["FirstName"].ToString();
                LastName = dRow["LastName"].ToString();
                LastLoggedInLocation = dRow["LastLoggedInLocation"].ToString();
            }
        }

        /// <summary>
        /// This will save a record of the login in the login audit table
        /// </summary>
        public void LoginFail(string Username, string Password)
        {
            string stStoredProc = "uspInsUpdMDBUserLoginAudit";
            SortedList slParameters = new SortedList();
            slParameters.Add("@UserLoginAuditID", -1);
            slParameters.Add("@UserID", -1);
            slParameters.Add("@Username", Username);
            slParameters.Add("@Password", Password);
            cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
        }

        /// <summary>
        /// This will save a record of the login in the login audit table
        /// </summary>
        public void LoginAudit(int UserID, string Username, string Password)
        {
            string stStoredProc = "uspWriteLoginAudit";
            SortedList slParameters = new SortedList();
            if (Email == null)
                Email = "";
            slParameters.Add("@UserLoginAuditID", -1);
            slParameters.Add("@Email", Email);
            slParameters.Add("@UserID", UserID);
            slParameters.Add("@Username", Username);
            slParameters.Add("@Password", Password);
            cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
        }

        /// <summary>
        /// Save will handle insert new member record.
        /// Set CampaignPlayerRoleID = -1 for insert.
        /// </summary>
        //public void Save(int UserID)
        //{
        //    // 
        //    string stStoredProc = "uspInsUpdCMCampaignPlayerRoles";
        //    //string stCallingMethod = "cGameSystem.Save";
        //    SortedList slParameters = new SortedList();
        //    slParameters.Add("@UserID", UserID);
        //    slParameters.Add("@CampaignPlayerRoleID", CampaignPlayerRoleID);
        //    slParameters.Add("@RoleAlignmentID", RoleAlignmentID);
        //    slParameters.Add("@CPEarnedForRole", CPEarnedForRole);
        //    slParameters.Add("@CPQuantityEarnedPerEvent", CPQuantityEarnedPerEvent);
        //    if (@CampaignPlayerRoleID==-1) // Set fields that can only be set on insert of new record
        //    {
        //        slParameters.Add("@CampaignPlayerID", CampaignPlayerID);
        //        slParameters.Add("@RoleID", RoleID);
        //        slParameters.Add("@RoleAlignmentID", RoleAlignmentID);
        //    }
        //    cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
        //}

    }
}


