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
using System.Net.Mail;

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
        public string TestingResultsText { get; set; }
        public string ContactUsText { get; set; }
        public string TermsOfUseText { get; set; }
        public string LearnMoreText { get; set; }
        public List<cPageTab> lsPageTabs = new List<cPageTab>();
        public int PasswordValidation { get; set; }
        public int AcceptNewPassword { get; set; }
        public string PasswordParameterComments { get; set; }
        public int PasswordParameterValue { get; set; }
        public int PasswordSortOrder { get; set; }
        public string PasswordFailMessage { get; set; }
        public int LoginCount { get; set; }
        public string SecurityResetCode { get; set; }
        public DateTime SecurityLockoutDate { get; set; }
        public int UserSecurityID { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityQuestion3 { get; set; }
        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }

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
        /// This will get the text from the database for Testing Results
        /// </summary>
        public void getTestingResults()
        {
            string stStoredProc = "uspGetTestingResults";
            string stCallingMethod = "cLogin.getTestingResults";
            SortedList slParameters = new SortedList();
            DataSet dsTestingResults = new DataSet();
            dsTestingResults = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsTestingResults.Tables[0].TableName = "MDBParameters";
            foreach (DataRow dRow in dsTestingResults.Tables["MDBParameters"].Rows)
            {
                TestingResultsText = dRow["Comments"].ToString();
            }
        }

        /// <summary>
        /// This will get the text from the database for Contact Us
        /// </summary>
        public void getContactUs()
        {
            string stStoredProc = "uspGetContactUs";
            string stCallingMethod = "cLogin.getContactUs";
            SortedList slParameters = new SortedList();
            DataSet dsContactUs = new DataSet();
            dsContactUs = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsContactUs.Tables[0].TableName = "MDBParameters";
            foreach (DataRow dRow in dsContactUs.Tables["MDBParameters"].Rows)
            {
                ContactUsText = dRow["Comments"].ToString();
            }
        }


        /// <summary>
        /// This will get the text from the database for Terms Of Use
        /// </summary>
        public void getTermsOfUse()
        {
            string stStoredProc = "uspGetTermsOfUse";
            string stCallingMethod = "cLogin.getTermsOfUse";
            SortedList slParameters = new SortedList();
            DataSet dsTermsOfUse = new DataSet();
            dsTermsOfUse = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsTermsOfUse.Tables[0].TableName = "MDBParameters";
            foreach (DataRow dRow in dsTermsOfUse.Tables["MDBParameters"].Rows)
            {
                TermsOfUseText = dRow["Comments"].ToString();
            }
        }

        /// <summary>
        /// This will get the text from the database for Learn More About LARP Portal
        /// </summary>
        public void getLearnMore()
        {
            string stStoredProc = "uspGetLearnMore";
            string stCallingMethod = "cLogin.getLearnMore";
            SortedList slParameters = new SortedList();
            DataSet dsLearnMore = new DataSet();
            dsLearnMore = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsLearnMore.Tables[0].TableName = "MDBParameters";
            foreach (DataRow dRow in dsLearnMore.Tables["MDBParameters"].Rows)
            {
                LearnMoreText = dRow["Comments"].ToString();
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
        /// This will load the left bar navigation.
        /// </summary>
        public void LoadLeftNav(int CampaignID, string NavigationName)
        {
            int iTemp;
            TabCount = 0;
            string stStoredProc = "uspGetLeftNav";
            string stCallingMethod = "cLogin.LoadLeftNav";
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
            DateTime dtTemp;
            int iTemp;
            int UserID = 0; // Until the member login is verified, there is no UserID so we'll use the guest ID
            SortedList slParameters = new SortedList();
            slParameters.Add("@Username", Username);
            slParameters.Add("@Password", Password);
            DataSet dsMember = new DataSet();
            dsMember = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsMember.Tables[0].TableName = "MDBUsers";
            // TODO-Rick-1 Need to check for security lock in here too and pass it back to override login (number of logins, lock code, date)
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
                if (int.TryParse(dRow["LoginCount"].ToString(), out iTemp))
                    LoginCount = iTemp;
                if (int.TryParse(dRow["UserSecurityID"].ToString(), out iTemp))
                    UserSecurityID = iTemp;
                if (DateTime.TryParse(dRow["SecurityLockoutDate"].ToString(), out dtTemp))
                    SecurityLockoutDate = dtTemp;
                MemberID = UserID;
                Username = dRow["LoginUsername"].ToString();
                FirstName = dRow["FirstName"].ToString();
                LastName = dRow["LastName"].ToString();
                LastLoggedInLocation = dRow["LastLoggedInLocation"].ToString();
                SecurityResetCode = dRow["SecurityResetCode"].ToString();
                Email = dRow["EmailAddress"].ToString();
            }
        }

        /// <summary>
        /// This will check if a requested username already exists
        /// Must pass a username
        /// </summary>
        public void CheckForExistingUsername(string Username)
        {
            string stStoredProc = "uspValidateNonExistingUsername";
            string stCallingMethod = "cLogin.CheckForExistingUsername";
            int iTemp;
            int UserID = 0; // Until the member login is verified, there is no UserID so we'll use the guest ID
            SortedList slParameters = new SortedList();
            slParameters.Add("@Username", Username);
            DataSet dsMember = new DataSet();
            dsMember = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsMember.Tables[0].TableName = "MDBUsers";
            foreach (DataRow dRow in dsMember.Tables["MDBUsers"].Rows)
            {
                if (int.TryParse(dRow["UserID"].ToString(), out iTemp))
                    UserID = iTemp;
                MemberID = UserID;
                Username = dRow["LoginUsername"].ToString();
                FirstName = dRow["FirstName"].ToString();
                LastName = dRow["LastName"].ToString();
            }
        }

        /// <summary>
        /// This will validate that a user is who they say they are to reset their password
        /// Must pass a username, email address and last name
        /// </summary>
        public void ValidateUserForPasswordReset(string Username, string EmailAddress, string LastName)
        {
            string stStoredProc = "uspValidateUserForPasswordReset";
            string stCallingMethod = "cLogin.ValidateUserForPasswordReset";
            int iTemp;
            int UserID = 0; // Until the member login is verified, there is no UserID so we'll use the guest ID
            SortedList slParameters = new SortedList();
            slParameters.Add("@Username", Username);
            slParameters.Add("@EmailAddress", EmailAddress);
            slParameters.Add("@LastName", LastName);
            DataSet dsMember = new DataSet();
            dsMember = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsMember.Tables[0].TableName = "MDBUsers";
            foreach (DataRow dRow in dsMember.Tables["MDBUsers"].Rows)
            {
                if (int.TryParse(dRow["UserID"].ToString(), out iTemp))
                    MemberID = iTemp;
                if (int.TryParse(dRow["UserSecurityID"].ToString(), out iTemp))
                    UserSecurityID = iTemp;
                Username = dRow["LoginUsername"].ToString();
                FirstName = dRow["FirstName"].ToString();
                LastName = dRow["LastName"].ToString();
                Password = dRow["LogonPassword"].ToString();
                Email = dRow["EmailAddress"].ToString();
                SecurityQuestion1 = dRow["SecurityQuestion1"].ToString();
                SecurityQuestion2 = dRow["SecurityQuestion2"].ToString();
                SecurityQuestion3 = dRow["SecurityQuestion3"].ToString();
                SecurityAnswer1 = dRow["SecurityAnswer1"].ToString();
                SecurityAnswer2 = dRow["SecurityAnswer2"].ToString();
                SecurityAnswer3 = dRow["SecurityAnswer3"].ToString();
            }
        }

        /// <summary>
        /// This will find a username by email address
        /// Must pass an email address
        /// </summary>
        public void GetUsernameByEmail(string EmailAddress)
        {
            // Note to Rick, this is NOT a good stored proc to use as a template for other stored procs due to its minimalist nature
            string stStoredProc = "uspGetUsernameByEmail";
            string stCallingMethod = "cLogin.GetUsernameByEmail";
            Username = "";
            Email = "";
            SortedList slParameters = new SortedList();
            slParameters.Add("@EmailAddress", EmailAddress);
            DataSet dsMember = new DataSet();
            dsMember = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsMember.Tables[0].TableName = "MDBUsers";
            foreach (DataRow dRow in dsMember.Tables["MDBUsers"].Rows)
            {
                Username = dRow["LoginUsername"].ToString();
                Email = dRow["EmailAddress"].ToString();
                FirstName = dRow["FirstName"].ToString();
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
        /// This will validate that a new password meets required standards
        /// </summary>
        public void ValidateNewPassword(string NewPassword)
        {
            string stStoredProc = "uspValidatePasswordRequirements";
            string stCallingMethod = "cLogin.ValidateNewPassword";
            int iTemp;
            PasswordValidation = 0; // We'll assume it failed unless we set it otherwise
            PasswordFailMessage = "";
            SortedList slParameters = new SortedList();
            slParameters.Add("@NewPassword", NewPassword);
            DataSet dsValidation = new DataSet();
            dsValidation = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsValidation.Tables[0].TableName = "PasswordResults";
            foreach (DataRow dRow in dsValidation.Tables["PasswordResults"].Rows)
            {
                if (int.TryParse(dRow["AcceptNewPassword"].ToString(), out iTemp))
                    AcceptNewPassword = iTemp;
                if (int.TryParse(dRow["PasswordParameterValue"].ToString(), out iTemp))
                    PasswordParameterValue = iTemp;
                if (int.TryParse(dRow["PasswordSortOrder"].ToString(), out iTemp))
                    PasswordSortOrder = iTemp;
                PasswordParameterComments = dRow["PasswordParameterComments"].ToString();
                if (PasswordSortOrder == 0)
                {
                    if (AcceptNewPassword == 0) // Password failed
                    {
                        PasswordFailMessage = "Password failed for the following reasons:";
                    }
                    else
                    {
                        PasswordValidation = 1;
                    }
                }
                else
                {
                    if (AcceptNewPassword == 0) // Individual test failed - append error
                    {
                        if (PasswordFailMessage == "")
                        {
                            PasswordFailMessage = PasswordFailMessage + PasswordParameterComments + " " + PasswordParameterValue;
                        }
                        else
                        {
                            PasswordFailMessage = PasswordFailMessage + "<br>" + PasswordParameterComments + " " + PasswordParameterValue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ClearNewAccount will clear the new member account for use.
        /// Requires UserSecurityID of the record being cleared
        /// </summary>
        public void ClearNewAccount(int SecurityID, int UserID)
        {
            string stStoredProc = "uspInsUpdMDBUserSecurity";
            SortedList slParameters = new SortedList();
            slParameters.Add("@UserSecurityID", SecurityID);
            slParameters.Add("@SecurityResetCode", "");
            slParameters.Add("@UserID", UserID);
            cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
        }

        /// <summary>
        /// Will write out updated security questions and answers and set new password
        /// Requires all three questions, all three answers, new password and UserID
        /// </summary>
        public void UpdateQAandPassword(int SecurityID, int UserID, string Q1, string Q1U, string Q2, string Q2U, string Q3, string Q3U, string A1, string A1U, string A2, string A2U, string A3, string A3U, string NewPassword)
        {
            //TODO-Rick-000-Modify code below to make it work
            string stStoredProc = "uspInsUpdMDBUserSecurity";
            SortedList slParameters = new SortedList();
            slParameters.Add("@UserID", UserID);
            slParameters.Add("@UserSecurityID", SecurityID);
            if (Q1U == "1")
                slParameters.Add("@SecurityQuestion1", Q1);
            if (Q2U == "1")
                slParameters.Add("@SecurityQuestion2", Q2);
            if (Q3U == "1")
                slParameters.Add("@SecurityQuestion3", Q3);
            if (A1U == "1")
                slParameters.Add("@SecurityAnswer1", A1);
            if (A2U == "1")
                slParameters.Add("@SecurityAnswer2", A2);
            if (A3U == "1")
                slParameters.Add("@SecurityAnswer3", A3);
            slParameters.Add("@LogonPassword", NewPassword);
            cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
        }

        /// <summary>
        /// This will save a record of the login in the login audit table
        /// </summary>
        public void NightlyUpdates()
        {
            int UserID = 185;   // Owner@larportal.com because why not?
            string stStoredProc = "uspNightlyUpdates";
            SortedList slParameters = new SortedList();
            slParameters.Add("@UserID", UserID);
            cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
        }
    }
}


