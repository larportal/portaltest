using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace LarpPortal.Classes
{
    public class cEmailMessageService   // This is far from working.
    {
        private MailMessage mail { get; set; }

        public void EmailMessageService()
        {
            mail = new MailMessage() { From = new MailAddress("support@larportal.com") };
        }

        public void SendMail(string subject, string body)
        {
            if (mail == null)
                throw new Exception("No mail message to send");

            mail.Subject = subject;
            mail.Body = body;
            SmtpClient emailClient = new SmtpClient("smtpout.secureserver.net");
            emailClient.UseDefaultCredentials = false;
            emailClient.EnableSsl = true;
            emailClient.Credentials = new System.Net.NetworkCredential("support@larportal.com", "Piccolo1");
            emailClient.Send(mail);
        }

//----------->	I would add a trim to the email address and also check to make sure the address is not blank.
        public void SetMailTo(string[] toEmails)
        {
            foreach (string e in toEmails)
                mail.To.Add(new MailAddress(e));
        }

        public void SetMailCc(string[] toEmails)
        {
            foreach (string e in toEmails)
                mail.CC.Add(new MailAddress(e));
        }

        public void TheseSettingsWorkForSendingAndNeedToBeIncorporatedInTheClassRippedOffFromIndexASPX()
        {
            string strTo;
            string strBody;
            string strFromUser = "support";
            string strFromDomain = "larportal.com";
            string strFrom = strFromUser + "@" + strFromDomain;
            string strSMTPPassword = "Piccolo1";
            string strSubject = "Subject Text";
            strBody = "Body Text";
            strTo = "To address(es) defined in code";
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
                //This was the catch I used in the new member login.  It needs something way better but I was in a hurry to get the programming done. - Rick
                //lblEmailFailed.Text = "There was an issue. Please contact us at support@larportal.com for assistance.";
                //lblEmailFailed.Visible = true;
            }
        }
    }
}