using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace LarpPortal
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ToggleFieldsVisibility();
                txtUsername.Focus();
            }
        }

        protected void ToggleFieldsVisibility()
        {
            pnlAnswerQuestion.Visible = false;
            pnlFinalStep.Visible = false;
            pnlSetPasswords.Visible = false;
            pnlSetQuestion.Visible = false;
            pnlVariables.Visible = false;   // Only using it to store invisible variables on the page
            lblInvalidCombination.Visible = false;
            btnInvalidCombination.Visible = false;
            lblWantQ2.Visible = false;
            btnWantQ2Yes.Visible = false;
            lblWantQ3.Visible = false;
            btnWantQ2No.Visible = false;
            btnWantQ3Yess.Visible = false;
            btnWantQ3No.Visible = false;
            lblSecurityQ2.Visible = false;
            txtSecurityQ2.Visible = false;
            lblSecurityA2.Visible = false;
            txtSecurityA2.Visible = false;
            lblSecurityQ3.Visible = false;
            txtSecurityQ3.Visible = false;
            lblSecurityA3.Visible = false;
            txtsecurityA3.Visible = false;
            lblAskQuestion2.Visible = false;
            txtAnswerQuestion2.Visible = false;
            lblAskQuestion3.Visible = false;
            txtAnswerQuestion3.Visible = false;
            lblAnsweredQuestion1Wrong.Visible = false;
            lblAnsweredQuestion2Wrong.Visible = false;
            lblAnsweredQuestion3Wrong.Visible = false;
            lblResetPassword.Visible = false;
            btnResetPassword.Visible = false;
            //lblDone.Visible = false;
            //btnDone.Visible = false;
        }

        protected void SetQandA(string Qu1, string Qu2, string Qu3, string An1, string An2, string An3)
        {
            Q1.Text = Qu1;
            Q1Update.Text = "1";
            Q2.Text = Qu2;
            Q2Update.Text = "1";
            Q3.Text = Qu3;
            Q3Update.Text = "1";
            A1.Text = An1;
            A1Update.Text = "1";
            A2.Text = An2;
            A2Update.Text = "1";
            A3.Text = An3;
            A3Update.Text = "1";
        }

        protected void btnForgotUsername_Click(object sender, EventArgs e)
        {
            //TODO-Rick-1 Use the email address to go get the username and email it to them.  If that email address has multiple usernames, send them all of them.
            if (txtEmailAddress.Text == "")
            {
                lblUsernameISEmail.Text = "Fill in the email address and click the 'Forgot Username' button again.";
            }
            else
            {
                Classes.cLogin ValidUser = new Classes.cLogin();
                ValidUser.GetUsernameByEmail(txtEmailAddress.Text);
                if (ValidUser.Email == "")
                {
                    lblUsernameISEmail.Text = "This email address is not associated with a LARP Portal account.  Please click 'Sign Up' to create an account.";
                }
                else
                {
                    if (ValidUser.Email == ValidUser.Username)
                    {
                        lblUsernameISEmail.Text = "Your email address is your username.  We recommend you change your username after logging in.";
                        txtUsername.Text = ValidUser.Username;
                    }
                    else
                    {
                        //TODO-Rick-1 Send an email to the user with just their username.
                        ForgotUsername(txtEmailAddress.Text);
                        lblUsernameISEmail.Text = "An email has been sent to this email address with your username.  Use that username to fill out this form and complete the process.";
                    }
                }
            }

            lblUsernameISEmail.Visible = true;
        }

        protected void ForgotUsername(string EmailAddress)
        {
            string strBody;
            string FirstName = "";
            string LastName = "";
            string LoginUsername = "";
            Classes.cLogin Username = new Classes.cLogin();
            Username.GetUsernameByEmail(EmailAddress);
            FirstName = Username.FirstName;
            LastName = Username.LastName;
            LoginUsername = Username.Username;
            string strFromUser = "support";
            string strFromDomain = "larportal.com";
            string strFrom = strFromUser + "@" + strFromDomain;
            string strSMTPPassword = "Piccolo1";
            string strSubject = "Your LARP Portal Username";
            strBody = "Hi " + FirstName + ",<p></p>Your LARP Portal username is " + LoginUsername + ".  If you need further assistance please contact us ";
            strBody = strBody + "contact us via email at support@larportal.com.";
            MailMessage mail = new MailMessage(strFrom, EmailAddress);
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
                lblUsernameISEmail.Text = "There was an issue. Please contact us at support@larportal.com for assistance.";
                lblUsernameISEmail.Visible = true;
            }
        }

        protected void btnGetPassword_Click(object sender, EventArgs e)
        {
            //TODO-Rick-1 Validate the username, email, last name combination.
            Classes.cLogin ValidUser = new Classes.cLogin();
            ValidUser.ValidateUserForPasswordReset(txtUsername.Text, txtEmailAddress.Text, txtLastName.Text);
            if (ValidUser.MemberID == 0)
            {
                //If it's not valid flash a message with a clear button and tell them to try again.
                lblInvalidCombination.Visible = true;
                btnInvalidCombination.Visible = true;
            }
            else  //If it's valid check for security questions.
            {
                Session["UserID"] = ValidUser.MemberID;
                UserSecurityID.Text = ValidUser.UserSecurityID.ToString();
                string An1 = ValidUser.SecurityAnswer1;
                string An2 = ValidUser.SecurityAnswer2;
                string An3 = ValidUser.SecurityAnswer3;
                string Qu1 = ValidUser.SecurityQuestion1;
                string Qu2 = ValidUser.SecurityQuestion2;
                string Qu3 = ValidUser.SecurityQuestion3;
                SetQandA(Qu1, Qu2, Qu3, An1, An2, An3);
                if (ValidUser.SecurityQuestion1 == "")  //If no security questions, make 'add question panel visible'.
                {
                    pnlSetQuestion.Visible = true;
                    pnlIDYourself.Visible = false;
                    txtSecurityQ1.Focus();
                }
                else //If at least one question, make the 'answer question panel visible.
                {
                    lblAskQuestion1.Text = "Security Question 1: " + Q1.Text;
                    lblAskQuestion2.Text = "Security Question 2: " + Q2.Text;
                    lblAskQuestion3.Text = "Security Question 3: " + Q3.Text;
                    pnlAnswerQuestion.Visible = true;
                    pnlIDYourself.Visible = false;
                    txtAnswerQuestion1.Focus();
                }
            } 
        }

        protected void btnInvalidCombination_Click(object sender, EventArgs e)
        {
            lblInvalidCombination.Visible = false;
            btnInvalidCombination.Visible = false;
            txtUsername.Focus();
        }

        protected void btnWantQ2Yes_Click(object sender, EventArgs e)
        {
            lblSecurityQ2.Visible = true;
            txtSecurityQ2.Visible = true;
            lblSecurityA2.Visible = true;
            txtSecurityA2.Visible = true;
            txtSecurityQ2.Focus();
        }

        protected void btnWantQ2No_Click(object sender, EventArgs e)
        {
            lblSecurityQ2.Visible = false;
            txtSecurityQ2.Text = "";
            txtSecurityA2.Visible = false;
            txtSecurityA2.Text = "";
            lblSecurityQ3.Visible = false;
            txtSecurityQ3.Text = "";
            txtsecurityA3.Visible = false;
            txtsecurityA3.Text = "";
            lblResetPassword.Visible = true;
            btnResetPassword.Visible = true;
            btnResetPassword.Focus();
        }

        protected void btnWantQ3Yes_Click(object sender, EventArgs e)
        {
            lblSecurityQ3.Visible = true;
            txtSecurityQ3.Visible = true;
            lblSecurityA3.Visible = true;
            txtsecurityA3.Visible = true;
            txtSecurityQ3.Focus();
        }

        protected void btnWantQ3No_Click(object sender, EventArgs e)
        {
            lblSecurityQ3.Visible = false;
            txtSecurityQ3.Text = "";
            txtsecurityA3.Visible = false;
            txtsecurityA3.Text = "";
            lblResetPassword.Visible = true;
            btnResetPassword.Visible = true;
            btnResetPassword.Focus();
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string An1 = txtsecurityA1.Text;
            string An2 = txtSecurityA2.Text;
            string An3 = txtsecurityA3.Text;
            string Qu1 = txtSecurityQ1.Text;
            string Qu2 = txtSecurityQ2.Text;
            string Qu3 = txtSecurityQ3.Text;
            SetQandA(Qu1, Qu2, Qu3, An1, An2, An3);
            pnlSetPasswords.Visible = true;
            pnlSetQuestion.Visible = false;
            txtNewPassword.Focus();
        }

        protected void txtAnswerQuestion1_TextChanged(object sender, EventArgs e)
        {
            //TODO-Rick-0 Validate Answer - 3 strikes you're out, lock account, send unlock key via email
            if (txtAnswerQuestion1.Text != A1.Text)
            {
                lblAnsweredQuestion1Wrong.Text = "You answered the question incorrectly.  Please try again.";
                txtAnswerQuestion1.Text = "";
                txtAnswerQuestion1.Focus();
            }
            {
                if (Q2.Text != "")
                {
                    lblAskQuestion2.Visible = true;
                    txtAnswerQuestion2.Visible = true;
                    txtAnswerQuestion2.Focus();
                }
                else
                {
                    MoveToFinalStep();
                }
            }
        }

        protected void txtAnswerQuestion2_TextChanged(object sender, EventArgs e)
        {
            //TODO-Rick-0 Validate Answer - 3 strikes you're out, lock account, send unlock key via email
           if(txtAnswerQuestion2.Text != A2.Text)
           {
               lblAnsweredQuestion2Wrong.Text = "You answered the question incorrectly.  Please try again.";
               txtAnswerQuestion2.Text = "";
               txtAnswerQuestion2.Focus();
           }
            if (Q3.Text != "")
            {
                lblAskQuestion3.Visible = true;
                txtAnswerQuestion3.Visible = true;
                txtAnswerQuestion3.Focus();
            }
            {
                MoveToFinalStep();
            }
        }

        protected void txtAnswerQuestion3_TextChanged(object sender, EventArgs e)
        {
            //TODO-Rick-0 Validate Answer - 3 strikes you're out, lock account, send unlock key via email
            if (txtAnswerQuestion3.Text != A3.Text)
            {
                lblAnsweredQuestion3Wrong.Text = "you answered the question incorrectly.  Please try again.";
                txtAnswerQuestion3.Text = "";
                txtAnswerQuestion3.Focus();
            }
            else
            {
                MoveToFinalStep();
            }
        }

        protected void MoveToFinalStep()
        {
            pnlSetPasswords.Visible = true;
            pnlAnswerQuestion.Visible = false;
            lblPasswordErrors.Visible = false;
            txtNewPassword.Focus();
        }

        protected void btnSubmitPasswordChange_Click(object sender, EventArgs e)
        {
            int ValidPassword;
            Classes.cLogin PasswordValidate = new Classes.cLogin();
            PasswordValidate.ValidateNewPassword(txtNewPassword.Text);
            ValidPassword = PasswordValidate.PasswordValidation;
            if (ValidPassword == 0)
            {
                if (lblPasswordErrors.Text != "")
                {
                    lblPasswordErrors.Text = lblPasswordErrors.Text + "<p></p>" + PasswordValidate.PasswordFailMessage + ".";
                }
                else
                {
                    lblPasswordErrors.Text = PasswordValidate.PasswordFailMessage + ".";
                }
                txtNewPassword.Text = "";
                txtNewPasswordRetype.Text = "";
                lblPasswordErrors.Visible = true;
                txtNewPassword.Focus();
            }
            else
            {
                int UserID = ((int)Session["UserID"]);
                int iTemp;
                int intUserSecurityID = 0;
                if (int.TryParse(UserSecurityID.Text, out iTemp))
                    intUserSecurityID = iTemp;
                Classes.cLogin UpdateSecurity = new Classes.cLogin();
                UpdateSecurity.UpdateQAandPassword(intUserSecurityID, UserID, Q1.Text, Q1Update.Text, Q2.Text, Q2Update.Text, Q3.Text, Q3Update.Text, A1.Text, A1Update.Text, A2.Text, A2Update.Text, A3.Text, A3Update.Text, txtNewPassword.Text);
                pnlSetPasswords.Visible = false;
                pnlFinalStep.Visible = true;
            }
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            lblUsernameISEmail.Visible = false;
            txtLastName.Focus();
        }

        protected void txtEmailAddress_TextChanged(object sender, EventArgs e)
        {
            lblUsernameISEmail.Visible = false;
            txtUsername.Focus();
        }

        protected void txtLastName_TextChanged(object sender, EventArgs e)
        {
            lblUsernameISEmail.Visible = false;
            btnGetPassword.Focus();
        }

        protected void txtsecurityA1_TextChanged(object sender, EventArgs e)
        {
            lblWantQ2.Visible = true;
            btnWantQ2Yes.Visible = true;
            btnWantQ2No.Visible = true;
        }

        protected void txtSecurityA2_TextChanged(object sender, EventArgs e)
        {
            lblWantQ3.Visible = true;
            btnWantQ3Yess.Visible = true;
            btnWantQ3No.Visible = true;
        }

        protected void txtsecurityA3_TextChanged(object sender, EventArgs e)
        {
            lblResetPassword.Visible = true;
            btnResetPassword.Visible = true;
        }

        protected void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            lblPasswordErrors.Visible = false;
            txtNewPasswordRetype.Focus();
        }

        protected void txtNewPasswordRetype_TextChanged(object sender, EventArgs e)
        {
            btnDone.Focus();
        }
    }
}