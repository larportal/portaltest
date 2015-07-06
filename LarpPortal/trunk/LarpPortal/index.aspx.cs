using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;
using System.Net;
using System.Net.Mail;

namespace LarpPortal
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // Destroys everything in the session which is essentially what logging out does.
                Session.Clear();
                // TODO-Rick-2 Make the next 7 things visible for real release
                int HidePublicAccess = 0;  // 1 (think of 1 as true) will hide the public access
                if (HidePublicAccess == 1)
                {
                    txtNewUsername.Visible = false;
                    txtFirstName.Visible = false;
                    txtLastName.Visible = false;
                    txtEmail.Visible = false;
                    txtPasswordNew.Visible = false;
                    txtPasswordNewRetype.Visible = false;
                    GuestLogin.Text = "";
                    //LearnMore.Text = "";
                    lblPasswordReqs.Text = "";
                }
                else
                {
                    GuestLogin.Text = "<a id=" + "\"" + "lnkGuestLogin" + "\"" + " href=" + "\"" + "PublicCampaigns.aspx" + "\"" + ">Enter LARP Portal as a guest</a>";
                    //lblPasswordReqs.Text = "<a id=" + "\"" + "PasswordReqs" + "\"" + " href=" + "\"" + "PasswordRequirements.aspx" + "\"" + " target=" + "\"" + "_blank" + "\"" + "><span class=" + "\"" + "glyphicon glyphicon-question-sign" + "\"" + "></span></a>";
                    lblPasswordReqs.Text = "<span class=" + "\"" + "glyphicon glyphicon-question-sign" + "\"" + "></span>";
                }
                chkTermsOfUse.Visible = false;
                btnValidateAccount.Visible = false;
                txtSecurityResetCode.Visible = false;
                lblSecurityResetCode.Visible = false;
                lblSignUpErrors.Visible = false; 
                Session["LoginName"] = "Guest";                   // Until login changes it
                Session["UserID"] = 0;                            // Until login changes it
                Session["SecurityRole"] = 0;                      // Until login changes it
                Session["WebPage"] = "~/MemberDemographics.aspx"; // Until login changes it
                lblInvalidLogin.Visible = false;
                lblInvalidActivationKey.Visible = false;
                lblInvalidLogin2.Visible = false;
                string SiteOpsMode;                
                Classes.cLogin OpsMode = new Classes.cLogin();
                OpsMode.SetSiteOperationalMode();
                SiteOpsMode = OpsMode.SiteOperationalMode;
                Session["OperationalMode"] = SiteOpsMode;
                ForgotPassword.Text = "<a id=" + "\"" + "lnkForgotPassword" + "\"" + " href=" + "\"" + "ForgotPassword.aspx" + "\"" + " target=" + "\"" + "_blank" + "\"" + ">Forgot password?</a>";
                // Get OS and browser settings and save them to session variables
                HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
                string UserAgent = HttpContext.Current.Request.UserAgent;
                Session["IPAddress"] = HttpContext.Current.Request.UserHostAddress;
                Session["Browser"] = bc.Browser;
                Session["BrowserVersion"] = bc.Version;
                Session["Platform"] = bc.Platform;
                Session["OSVersion"] = Request.UserAgent;
                // Check for browser.  If not Chrome pop message
                if (bc.Browser != "Chrome")
                {
                    string jsString = "alert('LARP Portal is optimized for Chrome.  You may experience issues with other browsers.');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                            "MyApplication",
                            jsString,
                            true);
                }

                //
            }
            txtName.Visible = false;
            txtLastLocation.Visible = false;
            txtLastCharacter.Visible = false;
            txtLastCampaign.Visible = false;
            txtUserID.Visible = false;
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
            if (Page.IsValid)
            {
                Classes.cLogin Login = new Classes.cLogin();
                Login.Load(txtUserName.Text, txtPassword.Text);
                if (Login.MemberID == 0) // Invalid user, fall straight to fail logic
                {
                    Session["SecurityRole"] = 0;
                    lblInvalidLogin.Visible = true;
                    lblInvalidLogin2.Visible = true;
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
                            Session["SavePassword"] = txtPassword.Text;
                            txtSecurityResetCode.Visible = true;
                            btnValidateAccount.Visible = true;
                            btnLogin.Visible = false;
                            txtSecurityResetCode.Focus();
                            txtPassword.Text = Session["SavePassword"].ToString();
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
                lblInvalidActivationKey.Visible = true;
                txtSecurityResetCode.Text = "";
                txtSecurityResetCode.Focus();
            }
        }

        protected void txtSecurityResetCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void MemberLogin(string AttemptedUsername, string AttemptedPassword)
        {
            Classes.cLogin Login = new Classes.cLogin();
            string PasswordToUse = txtPassword.Text;
            if (PasswordToUse == "")
                PasswordToUse = AttemptedPassword;
            Login.Load(txtUserName.Text, PasswordToUse);
            int intUserID;
            string WhereAreYouGoing;
            Session["MemberEmailAddress"] = Login.Email;
            Session["SecurityRole"] = Login.SecurityRoleID;
            txtName.Text = Login.FirstName + " " + Login.LastName;
            txtLastLocation.Text = Login.LastLoggedInLocation;
            txtLastCharacter.Text = Login.LastLoggedInCharacter.ToString();
            txtLastCampaign.Text = Login.LastLoggedInCampaign.ToString();
            txtUserID.Text = Login.MemberID.ToString();
            intUserID = Login.MemberID;
            Session["LoginName"] = Login.FirstName;
            Session["Username"] = Session["AttemptedUsername"];
            Session["LoginPassword"] = Session["AttemptedPassword"];
            Session["UserID"] = Login.MemberID;
            if (txtLastCharacter.Text != "0")
                Session["SelectedCharacter"] = txtLastCharacter.Text;
            if (txtLastCampaign.Text != "0")
                Session["CampaignID"] = txtLastCampaign.Text;
            // Write login entry to UserLoginAudit table
            string txtIPAddress = "";
            string txtBrowser = "";
            string txtBrowserVersion = "";
            string txtPlatform = "";
            string txtOSVersion = "";
            if (Session["IPAddress"] != null)
                txtIPAddress = Session["IPAddress"].ToString();
            if (Session["Browser"] != null)
                txtBrowser = Session["Browser"].ToString();
            if (Session["BrowserVersion"] != null)
                txtBrowserVersion = Session["BrowserVersion"].ToString();
            if (Session["Platform"] != null)
                txtPlatform = Session["Platform"].ToString();
            if (Session["OSVersion"] != null)
                txtOSVersion = Session["OSVersion"].ToString();
            Login.LoginAudit(Login.MemberID, txtUserName.Text,txtPassword.Text, txtIPAddress, txtBrowser, txtBrowserVersion, txtPlatform, txtOSVersion);
            Session["WebPage"] = Login.LastLoggedInLocation;
            // Go to the default or last page visited
            if(Session["WebPage"] == null)
            {
                Session["WebPage"] = "~/MemberDemographics.aspx";
            }
            else
            {
                if (txtLastLocation.Text == "")
                    txtLastLocation.Text = "MemberDemographics.aspx";
                string FirstChar = txtLastLocation.Text.Substring(1, 1);
                int LocationLength = txtLastLocation.Text.Length;
                if (FirstChar == "/")
                {
                    LocationLength = LocationLength - 1;
                    txtLastLocation.Text = txtLastLocation.Text.Substring(2, LocationLength);
                }
                Session["WebPage"] = "~/" + txtLastLocation.Text;
            }
            WhereAreYouGoing = Session["WebPage"].ToString();
            Response.Redirect(Session["WebPage"].ToString());
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Session["AttemptedPassword"] == null)
                txtPasswordNew.Text = "";
            else
            {
                txtPasswordNew.Text = Session["AttemptedPassword"].ToString();
                txtPasswordNew.Attributes.Add("value", txtPasswordNew.Text);
            }
            if (Session["AttemptedPasswordRetype"] == null)
                txtPasswordNewRetype.Text = "";
            else
            {
                txtPasswordNewRetype.Text = Session["AttemptedPasswordRetype"].ToString();
                txtPasswordNewRetype.Attributes.Add("value",txtPasswordNewRetype.Text);
            }
            if (Page.IsValid)
            {
                lblSignUpErrors.Text = "";
                // 1 - No duplicate usernames allowed
                Classes.cLogin Login = new Classes.cLogin();
                Login.CheckForExistingUsername(txtNewUsername.Text);
                if (Login.MemberID != 0)  // UserID is taken
                {
                    lblSignUpErrors.Text = "This username is already in use.  Please select a different one.";
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
                // 4 - New request - If the email address is already on file, warn them and suggest they go to the Forgot Username / Password section
                Classes.cLogin ExistingEmailAddress = new Classes.cLogin();
                ExistingEmailAddress.GetUsernameByEmail(txtEmail.Text);
                if(ExistingEmailAddress.Username != "")
                    if (lblSignUpErrors.Text != "")
                    {
                        lblSignUpErrors.Text = lblSignUpErrors.Text + "<p></p>This email address is already associated with an account.  If you've forgotten your username or password, please use the link above.";
                    }
                    else
                    {
                        lblSignUpErrors.Text = "This email address is already associated with an account.  If you've forgotten your username or password, please use the link above.";
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
                    Classes.cLogin Activation = new Classes.cLogin();
                    Activation.Load(txtNewUsername.Text, txtPasswordNew.Text);
                    string ActivationKey = "";
                    ActivationKey = Activation.SecurityResetCode;
                    GenerateWelcomeEmail(txtFirstName.Text, txtLastName.Text, txtNewUsername.Text, txtEmail.Text, ActivationKey);
                    // TODO-Rick-0c Redirect to page that will tell user to go look in their email for login directions - Done but 0b isn't so leaving placeholder for now
                    //Response.Redirect("~/NewUserLoginDirections.aspx", "_blank");
                    Response.Write("<script>");
                    Response.Write("window.open('NewUserLoginDirections.aspx','_blank')");
                    Response.Write("</script>");
                    // TODO-Rick-0d Oh yeah, create page NewUserLoginDirections.aspx before directing them there.
                    // TODO-Rick-0e Account for versioning of 'terms of use' and keeping track of date/time and which version user agreed to
                }
            }
            else
            {
                // TODO-Rick-3 On create user if something totally unexpected is wrong put up a message
            }
        }

        protected void GenerateWelcomeEmail(string FirstName, string LastName, string Username, string strTo, string ActivationKey)
        {
            string strBody;
            string strFromUser = "support";
            string strFromDomain = "larportal.com";
            string strFrom = strFromUser + "@" + strFromDomain;
            string strSMTPPassword = "Piccolo1";
            string strSubject = "Your LARP Portal Activation Key";
            strBody = "Hi " + FirstName + "<p></p>Welcome to LARP Portal.  The activation key for your new account is " + ActivationKey + ".  To activate your ";
            strBody = strBody + "account return to www.larportal.com.  Enter your username and password into the Member Login section and click the Login ";
            strBody = strBody + "button.  When the site prompts you for your activation key, enter it and click the Login button again.<p></p>If you have ";
            strBody = strBody + "any questions please email us at support@larportal.com.";
            MailMessage mail = new MailMessage(strFrom, strTo);
            SmtpClient client = new SmtpClient("smtpout.secureserver.net", 80);
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(strFrom, strSMTPPassword);
            client.Timeout = 10000;
            mail.Subject = strSubject;
            mail.Body = strBody;
            mail.IsBodyHtml = true;

            try
            {
                client.Send(mail);
            }
            catch (Exception)
            {
                lblEmailFailed.Text = "There was an issue. Please contact us at support@larportal.com for assistance.";
                lblEmailFailed.Visible = true;
            }
        }

        protected void chkTermsOfUse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTermsOfUse.Checked == true)
            {
                btnSignUp.Visible = true;
                btnSignUp.Focus();
            }
            else
            {
                btnSignUp.Visible = false;
            }
        }

        protected void txtPasswordNewRetype_TextChanged(object sender, EventArgs e)
        {
            Session["AttemptedPasswordRetype"] = txtPasswordNewRetype.Text;
            chkTermsOfUse.Focus();
            if (Session["AttemptedPassword"] == null)
                txtPasswordNew.Text = "";
            else
            {
                txtPasswordNew.Text = Session["AttemptedPassword"].ToString();
                txtPasswordNew.Attributes.Add("value", txtPasswordNew.Text);
            }
            if (Session["AttemptedPasswordRetype"] == null)
                txtPasswordNewRetype.Text = "";
            else
            {
                txtPasswordNewRetype.Text = Session["AttemptedPasswordRetype"].ToString();
                txtPasswordNewRetype.Attributes.Add("value", txtPasswordNewRetype.Text);
            }
        }

        protected void txtPasswordNew_TextChanged(object sender, EventArgs e)
        {
            Session["AttemptedPassword"] = txtPasswordNew.Text;
            txtPasswordNewRetype.Focus();
            if (Session["AttemptedPassword"] == null)
                txtPasswordNew.Text = "";
            else
            {
                txtPasswordNew.Text = Session["AttemptedPassword"].ToString();
                txtPasswordNew.Attributes.Add("value", txtPasswordNew.Text);
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if(txtEmail.Text.Length > 0)
                chkTermsOfUse.Visible = true;
            txtPasswordNew.Focus();
        }
    }
}