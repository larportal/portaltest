using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace LarpPortal
{
    public partial class SilverfireRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            DateTime dtRegistrationStart = new DateTime(2015, 7, 30, 20, 0, 0);
            DateTime dtRegistrationEnd = new DateTime(2015, 9, 4, 20, 0, 0);

            if (DateTime.Now < dtRegistrationStart)
            {
                mvDisplay.SetActiveView(vwNotOpenYet);
                return;
            }
            else if (DateTime.Now > dtRegistrationEnd)
            {
                mvDisplay.SetActiveView(vwRegistrationClosed);
                return;
            }

            // If we get this far, it means we are during the correct time frame.

            SortedList sParam = new SortedList();
            sParam.Add("@UserID", Session["UserID"].ToString());

            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            DataSet dsUser = Classes.cUtilities.LoadDataSet("uspGetSilverfireCharacter", sParam, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

            mvPlayerInfo.SetActiveView(vwNoPlayer);
            mvDisplay.SetActiveView(vwRegistrationOpen);

            foreach (DataRow dRow in dsUser.Tables[0].Rows)
            {
                mvPlayerInfo.SetActiveView(vwPlayerInfo);
                if (dRow["NickName"].ToString().Trim().Length > 0)
                    lblPlayerName.Text = dRow["NickName"].ToString().Trim() + " ( " + dRow["FirstName"].ToString().Trim() + " " + dRow["LastName"].ToString().Trim() + " )";
                else
                    lblPlayerName.Text = dRow["FirstName"].ToString().Trim() + " " + dRow["LastName"].ToString().Trim();

                lblPlayerEmail.Text = dRow["EmailAddress"].ToString();

                lblCharacterAKA.Text = dRow["CharacterAKA"].ToString();

                ddlPaymentType.SelectedIndex = -1;

                if (dRow["EventPaymentTypeID"] != DBNull.Value)
                {
                    lblAlreadyRegistered.Visible = true;
                    btnRegister.Visible = false;

                    foreach (ListItem li in ddlPaymentType.Items)
                        if (dRow["EventPaymentTypeID"].ToString() == li.Value)
                            li.Selected = true;
                }
                else
                {
                    lblAlreadyRegistered.Visible = false;
                    btnRegister.Visible = true;
                }

                hidCharacterID.Value = dRow["CharacterID"].ToString();

                tbComment.Text = dRow["CommentToStaff"].ToString();
                hidEventID.Value = dRow["EventID"].ToString();

                if (dsUser.Tables[1].Rows.Count > 0)
                {
                    ddlTeams.DataTextField = "TeamName";
                    ddlTeams.DataValueField = "TeamID";
                    ddlTeams.DataSource = dsUser.Tables[1];
                    ddlTeams.DataBind();
                    ddlTeams.Items.Insert(0, new ListItem("No team selected", "0"));
                    ddlTeams.Visible = true;
                    lblNoTeamMember.Visible = false;
                    hidTeamMember.Value = "1";
                    ddlTeams.SelectedIndex = -1;
                    if (dRow["TeamID"] != DBNull.Value)
                        foreach (ListItem li in ddlTeams.Items)
                            if (li.Value == dRow["TeamID"].ToString())
                                li.Selected = true;
                    if (ddlTeams.SelectedIndex == -1)
                        ddlTeams.SelectedIndex = 0;
                }
                else
                {
                    ddlTeams.Visible = false;
                    lblNoTeamMember.Visible = true;
                    hidTeamMember.Value = "0";
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            SortedList sParam = new SortedList();
            sParam.Add("@RegistrationID", "-1");
            sParam.Add("@UserID", Session["UserID"].ToString());
            sParam.Add("@EventID", hidEventID.Value);
            sParam.Add("@RoleAlignmentID", "1");
            sParam.Add("@CharacterID", hidCharacterID.Value);
            sParam.Add("@DateRegistered", DateTime.Now);
            sParam.Add("@EventPaymentTypeID", ddlPaymentType.SelectedValue);
            sParam.Add("@PlayerCommentsToStaff", tbComment.Text.Trim());
            sParam.Add("@RegistrationStatus", 44);
            if (hidTeamMember.Value == "1")
                if (ddlTeams.SelectedIndex != 0)
                    sParam.Add("@TeamID", ddlTeams.SelectedValue);

            try
            {
                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
                DataTable dtUser = Classes.cUtilities.LoadDataTable("uspInsUpdCMRegistrations", sParam, "LARPortal", Session["UserName"].ToString(), lsRoutineName);
                mvPlayerInfo.SetActiveView(vwRegistered);
                string jsString = "alert('Character " + lblCharacterAKA.Text + " has been registered.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
                NotifyOfNewRegistration();
            }
            catch
            {
                mvPlayerInfo.SetActiveView(vwError);
            }
        }

        protected void NotifyOfNewRegistration()
        {
            string strBody;
            //string strFromUser = "support";
            //string strFromDomain = "larportal.com";
            //string strFrom = strFromUser + "@" + strFromDomain;
            //string strSMTPPassword = "Piccolo1";
            string strSubject = "New Silverfire event registration - " + lblPlayerName.Text;
            string strTeam = "";
            if (ddlTeams.SelectedIndex >= 0)
                strTeam = ddlTeams.SelectedItem.Text;
            strBody = lblPlayerName.Text + " has just registered for the upcoming Silverfire event.  <br>Email: " + lblPlayerEmail.Text + "<br>Character: " + lblCharacterAKA.Text + "<br>Team: ";
            strBody = strBody + strTeam + "<br>Payment Method: " + ddlPaymentType.SelectedItem.Text + "<br>Player Comments: " + tbComment.Text;
            //string EmailAddress = "fifthgategm@gmail.com";
            //MailMessage mail = new MailMessage(strFrom, EmailAddress);
            //mail.Bcc.Add("support@larportal.com");
            //SmtpClient client = new SmtpClient("smtpout.secureserver.net", 80);
            //client.EnableSsl = false;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential(strFrom, strSMTPPassword);
            //client.Timeout = 10000;
            //mail.Subject = strSubject;
            //mail.Body = strBody;
            //mail.IsBodyHtml = true;

            Classes.cEmailMessageService RegistrationEmail = new Classes.cEmailMessageService();

            try
            {
                //client.Send(mail);
                RegistrationEmail.SendMail(strSubject, strBody, "fifthgategm@gmail.com", lblPlayerEmail.Text, "support@larportal.com");
            }
            catch (Exception)
            {
                //lblUsernameISEmail.Text = "There was an issue. Please contact us at support@larportal.com for assistance.";
                //lblUsernameISEmail.Visible = true;
            }
        }

    }
}