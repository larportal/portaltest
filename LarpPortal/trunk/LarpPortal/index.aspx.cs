using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;

namespace LarpPortal
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // TODO-Rick-2 Make the next 7 things visible for real release
                txtNewUsername.Visible = false;
                txtFirstName.Visible = false;
                txtLastName.Visible = false;
                txtEmail.Visible = false;
                txtPasswordNew.Visible = false;
                txtPasswordNewRetype.Visible = false;
                chkTermsOfUse.Visible = false;
                // Uncomment the next two lines for release
                // GuestLogin.Text = "<a id=" + "\"" + "lnkGuestLogin" + "\"" + " href=" + "\"" + "WhatIsLARPing.aspx" + "\"" + ">Log into LARP Portal as a guest</a>";
                // lblPasswordReqs.Text = "<a id=" + "\"" + "PasswordReqs" + "\"" + " href=" + "\"" + "PasswordRequirements.aspx" + "\"" + " target=" + "\"" + "_blank" + "\"" + "><span class=" + "\"" + "glyphicon glyphicon-question-sign" + "\"" + "></span></a>";
                // 
                btnValidateAccount.Visible = false;
                txtSecurityResetCode.Visible = false;
                lblSecurityResetCode.Visible = false;
                lblSignUpErrors.Visible = false; 
                Session["LoginName"] = "Guest";                // Until login changes it
                Session["UserID"] = 0;                         // Until login changes it
                Session["SecurityRole"] = 0;                   // Until login changes it
                Session["WebPage"] = "~/publicCampaigns.aspx"; // Until login changes it
                lblInvalidLogin.Visible = false;
                string SiteOpsMode;                
                Classes.cLogin OpsMode = new Classes.cLogin();
                OpsMode.SetSiteOperationalMode();
                SiteOpsMode = OpsMode.SiteOperationalMode;
                Session["OperationalMode"] = SiteOpsMode;
                if (SiteOpsMode == "Test")
                {
                    txtLastLocation.Attributes.Add("Placeholder", "Last Location in the Portal");
                    txtName.Attributes.Add("Placeholder", "Member Name");
                    txtUserID.Attributes.Add("Placeholder", "UserID");
                    txtName.Visible = true;
                    txtLastLocation.Visible = true;
                    txtUserID.Visible = true;
                    GuestLogin.Text = "<a id=" + "\"" + "lnkGuestLogin" + "\"" + " href=" + "\"" + "WhatIsLARPing.aspx" + "\"" + ">Log into LARP Portal as a guest</a>";
                    lblPasswordReqs.Text = "<a id=" + "\"" + "PasswordReqs" + "\"" + " href=" + "\"" + "PasswordRequirements.aspx" + "\"" + " target=" + "\"" + "_blank" + "\"" + "><span class=" + "\"" + "glyphicon glyphicon-question-sign" + "\"" + "></span></a>";
                }
            }
            txtName.Visible = false;
            txtLastLocation.Visible = false;
            txtUserID.Visible = false;
            //TODO-Rick-3 Define password requirement ToolTip programatically instead of hard coded
            lblPasswordReqs.ToolTip = "LARP Portal login passwords must be at least 7 characters long and contain at least " +  
                "1 uppercase letter, 1 lowercse letter, 1 number and 1 special character";
            if (!IsPostBack)
            {
                txtUserName.Attributes.Add("Placeholder", "Username");
                txtUserName.Focus();
                txtPassword.Attributes.Add("Placeholder", "Password");
                txtEmail.Attributes.Add("Placeholder", "Email");
                txtNewUsername.Attributes.Add("Placeholder", "Username");
                txtFirstName.Attributes.Add("Placeholder", "First Name");
                txtLastName.Attributes.Add("Placeholder", "Last Name");
                txtPasswordNew.Attributes.Add("Placeholder", "Password");
                txtPasswordNewRetype.Attributes.Add("Placeholder", "Retype Password");
                btnSignUp.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session["AttemptedPassword"] = txtPassword.Text;
            Session["AttemptedUsername"] = txtUserName.Text;
            // TODO-Rick-4 Remove "Signup" cheat on go live - It won't matter if we don't, but clean code and all
            if (txtUserName.Text == "Signup")
            {
                if (txtPassword.Text == "Signup")
                {
                    txtNewUsername.Visible = true;
                    txtFirstName.Visible = true;
                    txtLastName.Visible = true;
                    txtEmail.Visible = true;
                    txtPasswordNew.Visible = true;
                    txtPasswordNewRetype.Visible = true;
                    chkTermsOfUse.Visible = true;
                    GuestLogin.Text = "<a id=" + "\"" + "lnkGuestLogin" + "\"" + " href=" + "\"" + "WhatIsLARPing.aspx" + "\"" + ">Log into LARP Portal as a guest</a>";
                    lblPasswordReqs.Text = "<a id=" + "\"" + "PasswordReqs" + "\"" + " href=" + "\"" + "PasswordRequirements.aspx" + "\"" + " target=" + "\"" + "_blank" + "\"" + "><span class=" + "\"" + "glyphicon glyphicon-question-sign" + "\"" + "></span></a>";
                    txtNewUsername.Focus();
                }
            }
            if (Page.IsValid)
            {
                Classes.cLogin Login = new Classes.cLogin();
                Login.Load(txtUserName.Text, txtPassword.Text);
                if (Login.MemberID == 0) // Invalid user, fall straight to fail logic
                {
                    Session["SecurityRole"] = 0;
                    lblInvalidLogin.Visible = true;
                    Login.LoginFail(txtUserName.Text, txtPassword.Text);
                }
                else // Valid member. Is there a lock?
                {
                    if (Login.SecurityResetCode != "")
                    {
                        if (Login.LoginCount == 0) // New user.  First time activation.
                        {
                            lblSecurityResetCode.Text = "Account activation code";
                            lblSecurityResetCode.ToolTip = "This code can be found in the welcome email that was sent when you registered for a LARP Portal account.";
                            lblSecurityResetCode.Visible = true;
                            txtSecurityResetCode.Visible = true;
                            btnValidateAccount.Visible = true;
                            btnLogin.Visible = false;
                            txtSecurityResetCode.Focus();
                        }
                        else // Existing user with a bigger problem
                        {
                            // TODO-Rick-3 Define how to handle user account locks on attempted login
                        }
                    }
                    else  // Valid member.  Login.
                    {
                        MemberLogin(Session["AttemptedUsername"].ToString(), Session["AttemptedPassword"].ToString());
                    }
                }
            }
        }

        protected void btnValidateAccount_Click(object sender, EventArgs e)
        {
            // Check that username,password,lock code are all matched.  Clear the lock code.  Follow the login logic.
            Classes.cLogin Login = new Classes.cLogin();
            Login.Load(Session["AttemptedUsername"].ToString(), Session["AttemptedPassword"].ToString());
            if (txtSecurityResetCode.Text == Login.SecurityResetCode)
            {
                Login.SecurityResetCode = "";
                Login.ClearNewAccount(Login.UserSecurityID, Login.MemberID);
                MemberLogin(Session["AttemptedUsername"].ToString(), Session["AttemptedPassword"].ToString());
            }
            else
            {
                // TODO-Rick-2 The user screwed up the new account validation code (SecurityResetCode) - Need handling
            }
        }

        protected void MemberLogin(string AttemptedUsername, string AttemptedPassword)
        {
            Classes.cLogin Login = new Classes.cLogin();
            Login.Load(txtUserName.Text, txtPassword.Text);
            int intUserID;
            string WhereAreYouGoing;
            Session["SecurityRole"] = Login.SecurityRoleID;
            txtName.Text = Login.FirstName + " " + Login.LastName;
            txtLastLocation.Text = Login.LastLoggedInLocation;
            txtUserID.Text = Login.MemberID.ToString();
            intUserID = Login.MemberID;
            Session["LoginName"] = Login.FirstName;
            Session["Username"] = Session["AttemptedUsername"];
            Session["LoginPassword"] = Session["AttemptedPassword"];
            Session["UserID"] = Login.MemberID;
            // Write login entry to UserLoginAudit table
            Login.LoginAudit(Login.MemberID, txtUserName.Text,txtPassword.Text);
            // Go to the default or last page visited
            if(Session["WebPage"] == null)
            {
                Session["WebPage"] = "~/MemberDemographics.aspx";
            }
            else
            {
                if (txtLastLocation.Text == "")
                    txtLastLocation.Text = "Preferences.aspx";
                Session["WebPage"] = "~/" + txtLastLocation.Text;
            }
            WhereAreYouGoing = Session["WebPage"].ToString();
            Response.Redirect(Session["WebPage"].ToString());
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblSignUpErrors.Text = "";
                // 1 - No duplicate usernames allowed
                Classes.cLogin Login = new Classes.cLogin();
                Login.CheckForExistingUsername(txtNewUsername.Text);
                if (Login.MemberID != 0)  // UserID is taken
                {
                    lblSignUpErrors.Text = "Please select a different username.";
                }
                // 2 - Password must meet parameter standards
                int ValidPassword;
                Classes.cLogin PasswordValidate = new Classes.cLogin();
                PasswordValidate.ValidateNewPassword(txtPasswordNew.Text);
                ValidPassword = PasswordValidate.PasswordValidation;
                if (ValidPassword == 0)
                {
                    if (lblSignUpErrors.Text != "")
                    {
                        lblSignUpErrors.Text = lblSignUpErrors.Text + "<p></p>" + PasswordValidate.PasswordFailMessage + ".";
                    }
                    else
                    {
                        lblSignUpErrors.Text = PasswordValidate.PasswordFailMessage + ".";
                    }
                }
                // 3 - Both passwords must be the same
                if (txtPasswordNew.Text != txtPasswordNewRetype.Text)
                    //set an error message
                {
                    if (lblSignUpErrors.Text != "")
                    {
                        lblSignUpErrors.Text = lblSignUpErrors.Text + "<p></p>Passwords don't match.  Please re-enter.";
                    }
                    else
                    {
                        lblSignUpErrors.Text = "Passwords don't match.  Please re-enter.";
                    }
                    txtPasswordNew.Text = "";
                    txtPasswordNewRetype.Text = "";
                }
                // If there were errors, display them and return to form
                if (lblSignUpErrors.Text != "")
                {
                    lblSignUpErrors.Visible = true;
                    txtNewUsername.Focus();
                }
                else
                {
                    // Everything is ok.  Create the record.  If successful, go to the member demographics screen.
                    Classes.cUser NewUser = new Classes.cUser(txtNewUsername.Text, txtPasswordNew.Text);
                    NewUser.FirstName = txtFirstName.Text;
                    NewUser.LastName = txtLastName.Text;
                    NewUser.LoginPassword = txtPasswordNew.Text;
                    NewUser.LoginEmail = txtEmail.Text;
                    NewUser.LoginName = txtNewUsername.Text;
                    NewUser.Save();
                    // TODO-Rick-0b Generate email to user with login directions - Need class from Jeff
                    
                    // TODO-Rick-0c Redirect to page that will tell user to go look in their email for login directions - Done but 0b isn't so leaving placeholder for now
                    Response.Redirect("~/NewUserLoginDirections.aspx");
                    // TODO-Rick-0d Oh yeah, create page NewUserLoginDirections.aspx before directing them there.
                    // TODO-Rick-0e Account for versioning of 'terms of use' and keeping track of date/time and which version user agreed to
                     
                }
            }
            else
            {
                // TODO-Rick-3 On create user if something totally unexpected is wrong put up a message
            }
        }

        protected void chkTermsOfUse_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTermsOfUse.Checked)
            {
                btnSignUp.Visible = true;
                btnSignUp.Focus();
            }
            else
            {
                btnSignUp.Visible = false;
            }
        }
    }
}