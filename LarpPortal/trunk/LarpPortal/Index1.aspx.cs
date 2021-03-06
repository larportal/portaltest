﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;
using System.Net;
using System.Net.Mail;

namespace LarpPortal
{
    public partial class Index1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCampaigns();
            }
        }

        protected void LoadCampaigns()
        {
            ddlCampaign.Items.Clear();
            string stStoredProc = "uspGetCampaignsByName";
            string stCallingMethod = "Index1.aspx.LoadCampaigns";
            string strUserName = Session["Username"].ToString();

            DataTable dtCampaigns = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@GameSystemID", 0);
            sParams.Add("@EndDate", DateTime.Now.ToShortDateString());
            sParams.Add("@GameSystemFilter", 0);
            sParams.Add("@CampaignFilter", 0);
            sParams.Add("@GenreFilter", 0);
            sParams.Add("@StyleFilter", 0);
            sParams.Add("@TechLevelFilter", 0);
            sParams.Add("@SizeFilter", 0);

            dtCampaigns = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            ddlCampaign.DataTextField = "CampaignName";
            ddlCampaign.DataValueField = "CampaignID";
            ddlCampaign.DataSource = dtCampaigns;
            ddlCampaign.DataBind();
            ddlCampaign.Items.Insert(0, new ListItem("Select Campaign", "0"));
            ddlCampaign.SelectedIndex = 0;
        }

        protected void cblRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool b = cblRole.Items.Cast<ListItem>().Any(i => i.Selected);
            if (b == true)
            {
                hidRole.Value = "1";
                if (hidCampaign.Value == "1")
                    btnSave.Visible = true;
                else
                    btnSave.Visible = false;
            }
            else
            {
                hidRole.Value = "0";
                btnSave.Visible = false;
            }
        }

        protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCampaign.SelectedIndex == 0)
            {
                hidCampaign.Value = "0";
                btnSave.Visible = false;
            }
            else
            {
                hidCampaign.Value = "1";
                if (hidRole.Value == "1")
                    btnSave.Visible = true;
                else
                    btnSave.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int UserID = 0;
            int intCampaignID = 0; // lblCurrentCampaign.Text.ToString().ToInt32();
            int.TryParse(ddlCampaign.SelectedValue, out intCampaignID);
            if (Session["CampaignPlayerRoleID"] == null)
                Session["CampaignPlayerRoleID"] = 0;
            int CampaignPlayerRoleID = Session["CampaignPlayerRoleID"].ToString().ToInt32();
            string strUsername = "";
            string RequestEmail;
            if (Session["Username"] != null)
                strUsername = "";
            else
                strUsername = Session["Username"].ToString();
            if (Session["UserID"].ToString().ToInt32() != 0)
                UserID = Session["UserID"].ToString().ToInt32();
            Classes.cCampaignBase Campaign = new Classes.cCampaignBase(intCampaignID, strUsername, UserID);
            if (Campaign.JoinRequestEmail == "")
                RequestEmail = Campaign.InfoRequestEmail;
            else
                RequestEmail = Campaign.JoinRequestEmail;
            if (RequestEmail.Contains("@"))
            {
                // It has a "@".  Assume the email format is close enough, go on.
            }
            else
            {
                RequestEmail = "";
            }
            foreach (ListItem item in cblRole.Items)
            {
                if (item.Selected)
                {
                    switch (item.Value)
                    {
                        case "NPC":
                            // Permanent NPC needs approval
                            if (RequestEmail == "")
                                SignUpForSelectedRole(6, UserID, intCampaignID, 55);
                            else
                            {
                                SendApprovalEmail(intCampaignID, UserID, 6, RequestEmail);
                                SignUpForSelectedRole(10, UserID, intCampaignID, 56);
                            }
                            Response.Redirect("~/General/MemberHome.aspx");
                            break;
                        case "6False":
                            // Permanent NPC no approval
                            SignUpForSelectedRole(6, UserID, intCampaignID, 55);
                            break;
                        case "EventNPC":
                            // Event NPC needs approval
                            if (RequestEmail == "")
                                SignUpForSelectedRole(7, UserID, intCampaignID, 55);
                            else
                            {
                                SendApprovalEmail(intCampaignID, UserID, 7, RequestEmail);
                                SignUpForSelectedRole(10, UserID, intCampaignID, 56);
                            }
                            Response.Redirect("~/General/MemberHome.aspx");
                            break;
                        case "7False":
                            // Event NPC no approval
                            SignUpForSelectedRole(7, UserID, intCampaignID, 55);
                            break;
                        case "PC":
                            // PC needs approval
                            if (RequestEmail == "")
                                SignUpForSelectedRole(8, UserID, intCampaignID, 55);
                            else
                            {
                                SendApprovalEmail(intCampaignID, UserID, 8, RequestEmail);
                                SignUpForSelectedRole(8, UserID, intCampaignID, 56);
                            }
                            Response.Redirect("~/General/MemberHome.aspx");
                            break;
                        case "8False":
                            // PC no approval
                            SignUpForSelectedRole(8, UserID, intCampaignID, 55);
                            break;
                        case "10True":
                            // NPC needs approval
                            if (RequestEmail == "")
                                SignUpForSelectedRole(10, UserID, intCampaignID, 55);
                            else
                            {
                                SendApprovalEmail(intCampaignID, UserID, 10, RequestEmail);
                                SignUpForSelectedRole(10, UserID, intCampaignID, 56);
                            }
                            break;
                        case "10False":
                            // NPC no approval
                            SignUpForSelectedRole(10, UserID, intCampaignID, 55);
                            break;
                        default:
                            // Technically we shouldn't be able to get here so do nothing
                            Response.Redirect("~/General/MemberHome.aspx");
                            break;
                    }
                }
            }
            Response.Redirect("~/General/MemberHome.aspx");
        }

        protected void SignUpForSelectedRole(int RoleToSignUp, int UserID, int CampaignID, int StatusID)
        {
            int CampaignPlayerID = 0;
            string Username = "";
            if (Session["Username"] == null)
                Username = "";
            else
                Username = Session["Username"].ToString();
            Classes.cUserCampaign CampaignPlayer = new Classes.cUserCampaign();
            CampaignPlayer.Load(UserID, CampaignID);
            CampaignPlayerID = CampaignPlayer.CampaignPlayerID; // if this comes back empty (-1) make one
            if (CampaignPlayerID == -1)
            {
                CreatePlayerInCampaign(UserID, CampaignID);
                CampaignPlayer.Load(UserID, CampaignID);
                CampaignPlayerID = CampaignPlayer.CampaignPlayerID;
            }
            int RoleAlignment = 2;
            if (RoleToSignUp == 8)
                RoleAlignment = 1;
            Classes.cPlayerRole PlayerRole = new Classes.cPlayerRole();
            PlayerRole.CampaignPlayerRoleID = -1;
            PlayerRole.CampaignPlayerID = CampaignPlayerID;
            PlayerRole.RoleID = RoleToSignUp;
            PlayerRole.RoleAlignmentID = RoleAlignment;
            PlayerRole.Save(UserID);
            if (Username != "")
            {
                Classes.cUser LastLogged = new Classes.cUser(Username, "Password");
                string LastCampaign = LastLogged.LastLoggedInCampaign.ToString();
                if (LastCampaign == null || LastCampaign == "0")
                {
                    LastLogged.LastLoggedInCampaign = CampaignID;
                    LastLogged.Save();
                    Session["CampaignID"] = CampaignID;
                }

            }
        }

        protected void CreatePlayerInCampaign(int UserID, int CampaignID)
        {
            Classes.cUserCampaign UserCampaign = new Classes.cUserCampaign();
            UserCampaign.CampaignPlayerID = -1;
            UserCampaign.CampaignID = CampaignID;
            UserCampaign.Save(UserID);
        }

        protected void SendApprovalEmail(int CampaignID, int UserID, int Role, string RequestEmail)
        {
            string strFromUser = "playerservices";
            string strFromDomain = "larportal.com";
            string strFrom = strFromUser + "@" + strFromDomain;
            string strTo = RequestEmail;
            //string strSMTPPassword = "Piccolo1";
            string strSubject = "";
            string strBody = "";
            string CampaignName = "";
            string Username = "";
            if (Session["Username"] != null)
                Username = Session["Username"].ToString();
            string PlayerFirstName = "";  // Needs defining - Look up based on UserID
            string PlayerLastName = "";  // Needs defining
            string PlayerEmailAddress = "";  // Needs defining
            Classes.cCampaignBase Campaign = new Classes.cCampaignBase(CampaignID, "Username", UserID);
            CampaignName = Campaign.CampaignName;
            Classes.cUser UserInfo = new Classes.cUser(Username, "Password");
            PlayerFirstName = UserInfo.FirstName;
            PlayerLastName = UserInfo.LastName;
            PlayerEmailAddress = Session["MemberEmailAddress"].ToString();
            switch (Role)
            {
                case 6:
                    strSubject = "Request to be a Permanent NPC for " + CampaignName;
                    strBody = "A request to be a permanent NPC " + CampaignName + " has been received through LARP Portal.  The player information is below.<p></p><p></p>";
                    strBody = strBody + PlayerFirstName + " " + PlayerLastName + "<p></p>" + PlayerEmailAddress + "<p></p><p></p>";
                    strBody = strBody + "Please reply to the player's request within 48 hours.<p></p><p></p>";
                    strBody = strBody + "Thank you<p></p><p></p>LARP Portal staff";
                    break;
                case 7:
                    strSubject = "Request to be an Event NPC for " + CampaignName;
                    strBody = "A request to be an Evnet NPC " + CampaignName + " has been received through LARP Portal.  The player information is below.<p></p><p></p>";
                    strBody = strBody + PlayerFirstName + " " + PlayerLastName + "<p></p>" + PlayerEmailAddress + "<p></p><p></p>";
                    strBody = strBody + "Please reply to the player's request within 48 hours.<p></p><p></p>";
                    strBody = strBody + "Thank you<p></p><p></p>LARP Portal staff";
                    break;
                case 8:
                    strSubject = "Request to PC " + CampaignName;
                    strBody = "A request to PC " + CampaignName + " has been received through LARP Portal.  The player information is below.<p></p><p></p>";
                    strBody = strBody + PlayerFirstName + " " + PlayerLastName + "<p></p>" + PlayerEmailAddress + "<p></p><p></p>";
                    strBody = strBody + "Please reply to the player's request within 48 hours.<p></p><p></p>";
                    strBody = strBody + "Thank you<p></p><p></p>LARP Portal staff";
                    break;
                case 10:
                    strSubject = "Request to NPC " + CampaignName;
                    strBody = "A request to NPC " + CampaignName + " has been received through LARP Portal.  The player information is below.<p></p><p></p>";
                    strBody = strBody + PlayerFirstName + " " + PlayerLastName + "<p></p>" + PlayerEmailAddress + "<p></p><p></p>";
                    strBody = strBody + "Please reply to the player's request within 48 hours.<p></p><p></p>";
                    strBody = strBody + "Thank you<p></p><p></p>LARP Portal staff";
                    break;
                default:
                    break;
            }
            Classes.cEmailMessageService NotifyStaff = new Classes.cEmailMessageService();
            try
            {
                NotifyStaff.SendMail(strSubject, strBody, strTo, "", "", "PointsEmail", Username);
            }
            catch (Exception)
            {
                //lblEmailFailed.Text = "There was an issue. Please contact us at support@larportal.com for assistance.";
            }
            //MailMessage mail = new MailMessage(strFrom, strTo);
            //SmtpClient client = new SmtpClient("smtpout.secureserver.net", 80);
            //client.EnableSsl = false;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential(strFrom, strSMTPPassword);
            //client.Timeout = 10000;
            //NotifyStaff.SendMail()
            //mail.Subject = strSubject;
            //mail.Body = strBody;
            //mail.IsBodyHtml = true;
            //if (strSubject != "")
            //{
            //    try
            //    {
            //        client.Send(mail);
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}
        }

    }
}