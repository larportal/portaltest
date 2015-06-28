using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace LarpPortal
{
    public partial class PublicCampaigns1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Campaigns";
            Session["ActiveLeftNav"] = "PublicCampaigns";
            
            int UserID = 0;
            if(!IsPostBack)
            {
                Session["DefaultCampaignLogoPath"] = "img/logo/";
                Session["DefaultCampaignLogoImage"] = "http://placehold.it/820x130";
                pnlOverview.Visible = false;
                pnlSelectors.Visible = false;
                pnlImageURL.Visible = false;
                hplLinkToSite.Visible = false;
                //imgCampaignImage.Visible = false;
                pnlSignUpForCampaign.Visible = false;
                if (Session["UserID"] == null)
                    UserID = 0;
                else
                    UserID = ((int) Session["UserID"]);
                // Populate ddlGameSystem
                ddlGameSystem.SelectedIndex = 0;
                ddlGameSystem.Items.Clear();
                //TODO-Rick-00 Create ReloadtvGameSystem and call it here
                ReloadtvGameSystem(UserID);
                // Populate ddlCampaign
                ddlCampaign.SelectedIndex = 0;
                ddlCampaign.Items.Clear();
                // Populate ddlGenre
                ddlGenre.SelectedIndex = 0;
                ddlGenre.Items.Clear();
                // Populate ddlStyle
                ddlStyle.SelectedIndex = 0;
                ddlStyle.Items.Clear();
                // Populate ddlTechLevel
                ddlTechLevel.SelectedIndex = 0;
                ddlTechLevel.Items.Clear();
                // Populate ddlSize
                ddlSize.SelectedIndex = 0;
                ddlSize.Items.Clear();
                // Populate ddlMileRadius
                ddlMileRadius.SelectedIndex = 0;
                ddlMileRadius.Items.Clear();
            }
        }

        protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make all tree views invisible and show the one that's poplulating
            tvGameSystem.Visible = false;
            tvCampaign.Visible = false;
            tvGenre.Visible = false;
            tvStyle.Visible = false;
            tvTechLevel.Visible = false;
            tvSize.Visible = false;
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            DateTime edt = DateTime.Now;
            string EndDate;
            if (chkEndedCampaigns.Checked  == true)
                EndDate = "";
            else
                EndDate = edt.ToShortDateString();
            Session["GameSystemFilter"] = 0;
            if (chkGameSystem.Checked == true)
                Session["GameSystemFilter"] = ddlGameSystem.SelectedValue.ToInt32();
            else
                Session["GameSystemFilter"] = 0;
            Session["CampaignFilter"] = 0;
            if (chkCampaign.Checked == true)
                Session["CampaignFilter"] = ddlCampaign.SelectedValue.ToInt32();
            else
                Session["CampaignFilter"] = 0;
            Session["GenreFilter"] = 0;
            if (chkGenre.Checked == true)
                Session["GenreFilter"] = ddlGenre.SelectedValue.ToInt32();
            else
                Session["GenreFilter"] = 0;
            Session["StyleFilter"] = 0;
            if (chkStyle.Checked == true)
                Session["StyleFilter"] = ddlStyle.SelectedValue.ToInt32();
            else
                Session["StyleFilter"] = 0;
            Session["TechLevelFilter"] = 0;
            if (chkTechLevel.Checked == true)
                Session["TechLevelFilter"] = ddlTechLevel.SelectedValue.ToInt32();
            else
                Session["TechLevelFilter"] = 0;
            Session["SizeFilter"] = 0;
            if (chkSize.Checked == true)
                Session["SizeFilter"] = ddlSize.SelectedValue.ToInt32();
            else
                Session["SizeFilter"] = 0;
            Session["ZipCodeFilter"] = "";
            Session["RadiusFilter"] = 0;
            if (chkZipCode.Checked == true)
            {
                Session["ZipCodeFilter"] = txtZipCode.Text;
                Session["RadiusFilter"] = ddlMileRadius.SelectedValue.ToInt32();
            }
            else
            {
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
            }

            pnlImageURL.Visible = false;
            pnlOverview.Visible = false;
            pnlSelectors.Visible = false;
            pnlSignUpForCampaign.Visible = false;

            switch (ddlOrderBy.Text)
            {
                case "Game System":
                    lblCampaignSearchBy.Text = " by Game System";
                    tvGameSystem.Visible = true;
                    break;
                case "Campaign":
                    lblCampaignSearchBy.Text = " by Campaign";
                    tvCampaign.Visible = true;
                    break;
                case "Genre":
                    lblCampaignSearchBy.Text = " by Genre";
                    tvGenre.Visible = true;
                    break;
                case "Style":
                    lblCampaignSearchBy.Text = " by Style";
                    tvStyle.Visible = true;
                    break;
                case "Tech Level":
                    lblCampaignSearchBy.Text = " by Tech Level";
                    tvTechLevel.Visible = true;
                    break;
                case "Size":
                    lblCampaignSearchBy.Text = " by Size";
                    tvSize.Visible = true;
                    break;
                default:
                    break;
                    
            }
            ReloadActiveTreeView(UserID);
        }

        protected void MakeDetailsVisible(int URLVisible, int ImageVisible, int imgHeight, int imgWidth, int OverviewVisible, int SelectorsVisible, int CampaignID)
        {
            if (URLVisible == 1)
                hplLinkToSite.Visible = true;
            else
                hplLinkToSite.Visible = false;
            if (ImageVisible == 1)
            {
                // Max dimensions are 820 x 130
                if (imgHeight == 0)
                    imgHeight = 130;
                if (imgWidth == 0)
                    imgWidth = 820;
                decimal imgRatio;
                imgRatio = decimal.Divide(imgWidth, imgHeight);
                if (imgRatio == 0)
                    imgRatio = 1;

                int CalculatedHeight = Convert.ToInt32(Math.Round(820 / imgRatio, 0));
                int CalculatedWidth = Convert.ToInt32(Math.Round(130 * imgRatio, 0));

                if (imgRatio == 6.308m)
                {
                    imgCampaignImage.Height = 130;
                    imgCampaignImage.Width = 820;
                }
                if (imgRatio > 6.308m)
                {
                    imgCampaignImage.Height = CalculatedHeight;
                    imgCampaignImage.Width = 820;
                }
                if (imgRatio < 6.308m)
                {
                    imgCampaignImage.Height = 130;
                    imgCampaignImage.Width = CalculatedWidth;
                }
                pnlImageURL.Visible = true;
                imgCampaignImage.Visible = true;
            }
            else
                //pnlImageURL.Visible = false;
                imgCampaignImage.Visible = false;
            if (OverviewVisible == 1)
                pnlOverview.Visible = true;
            else
                pnlOverview.Visible = false;
            if (SelectorsVisible == 1)
            {
                pnlSelectors.Visible = true;
                if ( ((int)Session["SecurityRole"]) == 10)
                    MakePanelSignUpVisible(CampaignID);
                else
                    pnlSignUpForCampaign.Visible = false;
            }
            else
            {
                pnlSelectors.Visible = false;
                pnlSignUpForCampaign.Visible = false;
            }
        }

        protected void MakePanelSignUpVisible(int CampaignID)
        {
            // Determine current roles for this campaign.
            int UserID = 0;
            int iTemp = 0;
            int RoleID = 0;
            Boolean bTemp = false;
            Boolean AutoApprove = false;
            string RoleDescription = "";
            string IsPC = "false";
            string IsNPC = "false";
            lblSignUpMessage.Visible = false;
            lblCurrentCampaign.Text = CampaignID.ToString();
            if (Session["UserID"].ToString().ToInt32() != 0)
                UserID = Session["UserID"].ToString().ToInt32();
            if (UserID > 0)
            {
                Classes.cPlayerRole Role = new Classes.cPlayerRole();
                Role.Load(UserID, 0, CampaignID);
                //Possible options:
                int RoleID6 = 0;    //6 Permanent NPC
                int RoleID7 = 0;    //7 Event NPC
                int RoleID8 = 0;    //8 PC
                int RoleID10 = 0;   //10 NPC
                int RoleCounter = 0; //Count the number of available roles for choosing "Both or All"
                string RoleDescription6 = "";
                string RoleDescription7 = "";
                string RoleDescription8 = "";
                string RoleDescription10 = "";
                Boolean AutoApprove6 = false;
                Boolean AutoApprove7 = false;
                Boolean AutoApprove8 = false;
                Boolean AutoApprove10 = false;
                Classes.cCampaignBase CampaignRoles = new Classes.cCampaignBase(CampaignID, "UserName", UserID);
                DataTable dtAvailRoles = new DataTable();
                dtAvailRoles = CampaignRoles.GetCampaignRequestableRoles(CampaignID, UserID);
                foreach (DataRow dRow in dtAvailRoles.Rows)
                {
                    RoleCounter = 0;
                    if (int.TryParse(dRow["RoleID"].ToString(), out iTemp))
                        RoleID = iTemp;
                    if (Boolean.TryParse(dRow["AutoApprove"].ToString(), out bTemp))
                        AutoApprove = bTemp;
                    RoleDescription = dRow["RoleDescription"].ToString();
                    switch (RoleID)
                    {
                        case 6:
                            RoleID6 = 6;
                            RoleDescription6 = "Permanent NPC";
                            AutoApprove6 = AutoApprove;
                            break;
                        case 7:
                            RoleID7 = 7;
                            RoleDescription7 = "Event NPC";
                            AutoApprove7 = AutoApprove;
                            break;
                        case 8:
                            RoleID8 = 8;
                            RoleDescription8 = RoleDescription;
                            AutoApprove8 = AutoApprove;
                            break;
                        case 10:
                            RoleID10 = 10;
                            RoleDescription10 = RoleDescription;
                            AutoApprove10 = AutoApprove;
                            break;
                        default:
                            //TODO - Get rid of these two lines once 6 and 7 are really defined.  This will shut up the warnings
                            RoleDescription6 = RoleDescription7;
                            RoleDescription7 = RoleDescription6;
                            break;
                    }
                }
                IsPC = Role.IsPC;
                IsNPC = Role.IsNPC;
                //TODO-Rick-9-Clean-up crap code - all instances of btnSignup; changed to chkSignup
                //btnSignUp.Items.Clear();
                chkSignUp.Items.Clear();
                // None - Show all three choices if applicable
                if (IsPC == "false" && IsNPC == "false")
                {
                    if (RoleID8 != 0)
                    {
                        //btnSignUp.Items.Add(new ListItem("PC", "8"));
                        chkSignUp.Items.Add(new ListItem("PC", "8" + AutoApprove8.ToString()));
                        RoleCounter++;
                    }
                    if (RoleID10 != 0)
                    {
                        //btnSignUp.Items.Add(new ListItem("NPC", "10"));
                        chkSignUp.Items.Add(new ListItem("NPC", "10" + AutoApprove10.ToString()));
                        RoleCounter++;
                    }
                    if (RoleID7 != 0)
                    {
                        //btnSignUp.Items.Add(new ListItem("Event NPC", "7"));
                        chkSignUp.Items.Add(new ListItem("Event NPC", "7" + AutoApprove7.ToString()));
                        RoleCounter++;
                    }
                    if (RoleID6 != 0)
                    {
                        //btnSignUp.Items.Add(new ListItem("Permanent NPC", "6"));
                        chkSignUp.Items.Add(new ListItem("Permanent NPC", "6" + AutoApprove6.ToString()));
                        RoleCounter++;
                    }
                    //TODO-Rick-9-Clean-up crap code
                    //if (RoleCounter == 2)
                    //{
                    //    //btnSignUp.Items.Add(new ListItem("Both", "2"));
                    //    chkSignUp.Items.Add(new ListItem("Both", "2"));
                    //}
                    //if (RoleCounter > 2)
                    //{
                    //    //btnSignUp.Items.Add(new ListItem("All", "2"));
                    //    chkSignUp.Items.Add(new ListItem("All", "2"));
                    //}                   
                }
                // PC Only - Show NPC
                if (IsPC == "true" && IsNPC == "false")
                {
                    if (RoleID10 != 0)
                    {
                        //btnSignUp.Items.Add(new ListItem("NPC", "10"));
                        chkSignUp.Items.Add(new ListItem("NPC", "10" + AutoApprove10.ToString()));
                        RoleCounter++;
                    }
                    if (RoleID7 != 0)
                    {
                        //btnSignUp.Items.Add(new ListItem("Event NPC", "7"));
                        chkSignUp.Items.Add(new ListItem("Event NPC", "7" + AutoApprove7.ToString()));
                        RoleCounter++;
                    }
                    if (RoleID6 != 0)
                    {
                        //btnSignUp.Items.Add(new ListItem("Permanent NPC", "6"));
                        chkSignUp.Items.Add(new ListItem("Permanent NPC", "6" + AutoApprove6.ToString()));
                        RoleCounter++;
                    }
                    //TODO-Rick-9-Clean-up crap code
                    //if (RoleCounter == 2)
                    //{
                    //    //btnSignUp.Items.Add(new ListItem("Both", "2"));
                    //    chkSignUp.Items.Add(new ListItem("Both", "2"));
                    //}
                    //if (RoleCounter > 2)
                    //{
                    //    //btnSignUp.Items.Add(new ListItem("All", "2"));
                    //    chkSignUp.Items.Add(new ListItem("All", "2"));
                    //} 
                }
                // NPC Only - Show PC
                if (IsPC == "false" && IsNPC == "true")
                {
                    if (RoleID8 != 0)
                    {
                        //btnSignUp.Items.Add(new ListItem("PC", "8"));
                        chkSignUp.Items.Add(new ListItem("PC", "8" + AutoApprove8.ToString()));
                        RoleCounter++;
                    }
                }
                // Both - Don't show panel
                if (IsPC == "true"  && IsNPC == "true")
                {
                    pnlSignUpForCampaign.Visible = true;
                    btnSignUpForCampaign.Visible = false;
                }
                else
                {
                    pnlSignUpForCampaign.Visible = true;
                    btnSignUpForCampaign.Visible = true;
                }
                // Now get current roles.  If there are any start the label with "Current Roles:<br>"
                Classes.cPlayerRoles Roles = new Classes.cPlayerRoles();
                Roles.Load(UserID, 0, CampaignID, DateTime.Now); // Last parameter = 1 for Yes, Current roles only
                //Convert list to datatable
                listCurrentRoles.DataSource = Classes.cUtilities.CreateDataTable(Roles.lsPlayerRoles);
                listCurrentRoles.DataBind();
            }
        }

        protected void btnSignUpForCampaign_Click(object sender, EventArgs e)
        {
            int UserID = 0;
            int intCampaignID = lblCurrentCampaign.Text.ToString().ToInt32();
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
            foreach (ListItem item in chkSignUp.Items)
            {
                if(item.Selected)
                {
                    switch (item.Value)
                    {
                        case "6True":
                            // Permanent NPC needs approval
                            if(RequestEmail == "")
                                SignUpForSelectedRole(6, UserID, intCampaignID, 55);
                            else
                            {
                                SendApprovalEmail(intCampaignID, UserID, 6, RequestEmail);
                                SignUpForSelectedRole(10, UserID, intCampaignID, 56);
                            }
                            break;
                        case "6False":
                            // Permanent NPC no approval
                            SignUpForSelectedRole(6, UserID, intCampaignID, 55);
                            break;
                        case "7True":
                            // Event NPC needs approval
                            if(RequestEmail == "")
                                SignUpForSelectedRole(7, UserID, intCampaignID, 55);
                            else
                            {
                                SendApprovalEmail(intCampaignID, UserID, 7, RequestEmail);
                                SignUpForSelectedRole(10, UserID, intCampaignID, 56);
                            }
                            break;
                        case "7False":
                            // Event NPC no approval
                            SignUpForSelectedRole(7, UserID, intCampaignID, 55);
                            break;
                        case "8True":
                            // PC needs approval
                            if(RequestEmail == "")
                                SignUpForSelectedRole(8, UserID, intCampaignID, 55);
                            else
                            {
                                SendApprovalEmail(intCampaignID, UserID, 8, RequestEmail);
                                SignUpForSelectedRole(10, UserID, intCampaignID, 56);
                            }
                            break;
                        case "8False":
                            // PC no approval
                            SignUpForSelectedRole(8, UserID, intCampaignID, 55);
                            break;
                        case "10True":
                            // NPC needs approval
                            if(RequestEmail == "")
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
                            break;
                    }
                }
            }
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
            if(CampaignPlayerID == -1)
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
                }

            }

            btnSignUpForCampaign.Visible = false;
            lblSignUpMessage.Text = "Request submitted. Choose more campaigns or <a id=\"" + "lnkReturnToMember " + "\"href=\"" + "CampaignInfo.aspx\"" + ">return to the member section.</a>";
            lblSignUpMessage.Visible = true;
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
            string strSMTPPassword = "Piccolo1";
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
            MailMessage mail = new MailMessage(strFrom, strTo);
            SmtpClient client = new SmtpClient("smtpout.secureserver.net", 80);
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(strFrom, strSMTPPassword);
            client.Timeout = 10000;
            mail.Subject = strSubject;
            mail.Body = strBody;
            mail.IsBodyHtml = true;
            if (strSubject != "")
            {
                try
                {
                    client.Send(mail);
                }
                catch (Exception)
                {

                }
            }
        }

        protected void SetSiteLink(string strURL, string strGameName)
        {
            hplLinkToSite.NavigateUrl = strURL;
            hplLinkToSite.Text = "Visit " + strGameName + " home page.";

        }

        protected void SetSiteImage(string strImage)
        {
            string DefaultPath = "";
            string DefaultImage = "";
            if (Session["DefaultCampaignLogoImage"]  == null)
                DefaultImage = "";
            else
                DefaultImage = Session["DefaultCampaignLogoImage"].ToString();
            if (Session["DefaultCampaignLogoPath"]  == null)
                DefaultPath = "";
            else
                DefaultPath = Session["DefaultCampaignLogoPath"].ToString();
            if (strImage == "")
                imgCampaignImage.ImageUrl = DefaultImage;
            else
                imgCampaignImage.ImageUrl = DefaultPath + strImage;
        }

        protected void tvGameSystem_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strURL = "";
            string strImage = "";
            int intImageHeight = 0;
            int intImageWidth = 0;
            string strGameOrCampaignName = "";
            tvGameSystem.SelectedNode.Selected = true;
            string SelectedGorC = tvGameSystem.SelectedValue + "";
            string GorC = SelectedGorC.Substring(0, 1);
            string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
            int GameSystemID;
            int CampaignID;
            int intURLVisible = 1;
            int intImageVisible = 1;
            int intOverviewVisible = 1;
            int intSelectorsVisible = 1;
            int.TryParse(stGameSystemID, out GameSystemID);
            int.TryParse(tvGameSystem.SelectedValue, out CampaignID);
            if (GorC == "G") // Game System
            {
                intSelectorsVisible = 0;
                lblGorC1.Text = "Game System";
                lblGorC2.Text = "Game System Description";
                Classes.cGameSystem GS = new Classes.cGameSystem();
                GS.Load(GameSystemID, 0);
                strURL = GS.GameSystemURL;
                strImage = GS.GameSystemLogo;
                intImageHeight = GS.GameSystemLogoHeight;
                intImageWidth = GS.GameSystemLogoWidth;
                strGameOrCampaignName = GS.GameSystemName;
                lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
            }
            else  // Campaign
            {
                if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                {
                    intURLVisible = 0;
                    intImageVisible = 0;
                    intOverviewVisible = 0;
                    intSelectorsVisible = 0;
                }
                else
                {
                    lblGorC1.Text = "Campaign";
                    lblGorC2.Text = "Campaign Description";
                    Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                    strURL = Cam.URL;
                    strImage = Cam.Logo;
                    intImageHeight = Cam.LogoHeight;
                    intImageWidth = Cam.LogoWidth;
                    strGameOrCampaignName = Cam.CampaignName;
                    lblCampaignOverview.Text = Cam.WebPageDescription;
                    lblGameSystem1.Text = "Game System: ";
                    lblGameSystem2.Text = " " + Cam.GameSystemName;
                    lblGenre1.Text = "Genre: " ;
                    lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                    lblStyle1.Text = "Style: ";
                    lblStyle2.Text = " " + Cam.StyleDescription;
                    lblTechLevel1.Text = "Tech Level: ";
                    lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                    lblSize1.Text = "Size:";
                    lblSize2.Text = " " + Cam.CampaignSizeRange;
                    lblLocation2.Text = Cam.MarketingLocation;
                    lblEvent2.Text = string.Format("{0:MMM d, yyyy}", Cam.NextEventDate);
                };
            }
            SetSiteImage(strImage);
            if(strURL != null)
                SetSiteLink(strURL, strGameOrCampaignName);
            MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
        }

        protected void tvCampaign_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strURL = "";
            string strImage = "";
            int intImageHeight = 130;
            int intImageWidth = 820;
            string strGameOrCampaignName = "";
            tvCampaign.SelectedNode.Selected = true;
            string SelectedGorC = tvCampaign.SelectedValue + "";
            string GorC = SelectedGorC.Substring(0, 1);
            string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
            int GameSystemID;
            int CampaignID = tvCampaign.SelectedValue.ToString().ToInt32();
            int intURLVisible = 1;
            int intImageVisible = 1;
            int intOverviewVisible = 1;
            int intSelectorsVisible = 1;
            int.TryParse(stGameSystemID, out GameSystemID);
            if (GorC == "G") // Game System - We shouldn't have any of these in this treeview
            {
                intSelectorsVisible = 0;
                lblGorC1.Text = "Game System";
                lblGorC2.Text = "Game System Description";
                Classes.cGameSystem GS = new Classes.cGameSystem();
                GS.Load(GameSystemID, 0);
                strURL = GS.GameSystemURL;
                strImage = "";
                strGameOrCampaignName = GS.GameSystemName;
                lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
            }
            else  // Campaign
            {
                if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                {
                    intURLVisible = 0;
                    intImageVisible = 0;
                    intOverviewVisible = 0;
                    intSelectorsVisible = 0;
                }
                else   // for this treeview we really only want this line of logic
                {
                    lblGorC1.Text = "Campaign";
                    lblGorC2.Text = "Campaign Description";
                    Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                    strURL = Cam.URL;
                    strImage = Cam.Logo;
                    intImageWidth = Cam.LogoWidth;
                    intImageHeight = Cam.LogoHeight;
                    strGameOrCampaignName = Cam.CampaignName;
                    lblCampaignOverview.Text = Cam.WebPageDescription;
                    lblGameSystem1.Text = "Game System: ";
                    lblGameSystem2.Text = " " + Cam.GameSystemName;
                    lblGenre1.Text = "Genre: ";
                    lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                    lblStyle1.Text = "Style: ";
                    lblStyle2.Text = " " + Cam.StyleDescription;
                    lblTechLevel1.Text = "Tech Level: ";
                    lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                    lblSize1.Text = "Size:";
                    lblSize2.Text = " " + Cam.CampaignSizeRange;
                    lblLocation2.Text = Cam.MarketingLocation;
                    lblEvent2.Text = string.Format("{0:MMM d, yyyy}", Cam.NextEventDate);
                }
            }
            SetSiteImage(strImage);
            if (strURL != null)
                SetSiteLink(strURL, strGameOrCampaignName);
            MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
        }

        protected void tvGenre_SelectedNodeChanged(object sender, EventArgs e)
        {
            {
                string strURL = "";
                string strImage = "";
                int intImageHeight = 130;
                int intImageWidth = 820;
                string strGameOrCampaignName = "";
                tvGenre.SelectedNode.Selected = true;
                string SelectedGorC = tvGenre.SelectedValue + "";
                string GorC = SelectedGorC.Substring(0, 1);
                string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
                int GameSystemID;
                int CampaignID;
                int intURLVisible = 1;
                int intImageVisible = 1;
                int intOverviewVisible = 1;
                int intSelectorsVisible = 1;
                int.TryParse(stGameSystemID, out GameSystemID);
                int.TryParse(tvGenre.SelectedValue, out CampaignID);
                if (GorC == "G") // Game System
                {
                    intSelectorsVisible = 0;
                    intURLVisible = 0;
                    intImageVisible = 0;
                    lblGorC1.Text = "Genre";
                    lblGorC2.Text = "Genre Description";
                    Classes.cGameSystem GS = new Classes.cGameSystem();
                    GS.Load(GameSystemID, 0);
                    strURL = GS.GameSystemURL;
                    strImage = "";
                    strGameOrCampaignName = GS.GameSystemName;
                    lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
                }
                else  // Campaign
                {
                    if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                    {
                        intURLVisible = 0;
                        intImageVisible = 0;
                        intOverviewVisible = 0;
                        intSelectorsVisible = 0;
                    }
                    else
                    {
                        lblGorC1.Text = "Campaign";
                        lblGorC2.Text = "Campaign Description";
                        Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                        strURL = Cam.URL;
                        strImage = Cam.Logo;
                        intImageHeight = Cam.LogoHeight;
                        intImageWidth = Cam.LogoWidth;
                        strGameOrCampaignName = Cam.CampaignName;
                        lblCampaignOverview.Text = Cam.WebPageDescription;
                        lblGameSystem1.Text = "Game System: ";
                        lblGameSystem2.Text = " " + Cam.GameSystemName;
                        lblGenre1.Text = "Genre: ";
                        lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                        lblStyle1.Text = "Style: ";
                        lblStyle2.Text = " " + Cam.StyleDescription;
                        lblTechLevel1.Text = "Tech Level: ";
                        lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                        lblSize1.Text = "Size:";
                        lblSize2.Text = " " + Cam.CampaignSizeRange;
                        lblLocation2.Text = Cam.MarketingLocation;
                        lblEvent2.Text = string.Format("{0:MMM d, yyyy}", Cam.NextEventDate);
                    }
                }
                SetSiteImage(strImage);
                if (strURL != null)
                    SetSiteLink(strURL, strGameOrCampaignName);
                MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
            }
        }

        protected void tvStyle_SelectedNodeChanged(object sender, EventArgs e)
        {
            {
                string strURL = "";
                string strImage = "";
                int intImageHeight = 130;
                int intImageWidth = 820;
                string strGameOrCampaignName = "";
                tvStyle.SelectedNode.Selected = true;
                string SelectedGorC = tvStyle.SelectedValue + "";
                string GorC = SelectedGorC.Substring(0, 1);
                string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
                int GameSystemID;
                int CampaignID;
                int intURLVisible = 1;
                int intImageVisible = 1;
                int intOverviewVisible = 1;
                int intSelectorsVisible = 1;
                int.TryParse(stGameSystemID, out GameSystemID);
                int.TryParse(tvStyle.SelectedValue, out CampaignID);
                if (GorC == "G") // Game System
                {
                    intSelectorsVisible = 0;
                    intURLVisible = 0;
                    intImageVisible = 0;
                    lblGorC1.Text = "Style";
                    lblGorC2.Text = "Style Description";
                    Classes.cGameSystem GS = new Classes.cGameSystem();
                    GS.Load(GameSystemID, 0);
                    strURL = GS.GameSystemURL;
                    strImage = "";
                    strGameOrCampaignName = GS.GameSystemName;
                    lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
                }
                else  // Campaign
                {
                    if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                    {
                        intURLVisible = 0;
                        intImageVisible = 0;
                        intOverviewVisible = 0;
                        intSelectorsVisible = 0;
                    }
                    else
                    {
                        lblGorC1.Text = "Campaign";
                        lblGorC2.Text = "Campaign Description";
                        Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                        strURL = Cam.URL;
                        strImage = Cam.Logo;
                        intImageHeight = Cam.LogoHeight;
                        intImageWidth = Cam.LogoWidth;
                        strGameOrCampaignName = Cam.CampaignName;
                        lblCampaignOverview.Text = Cam.WebPageDescription;
                        lblGameSystem1.Text = "Game System: ";
                        lblGameSystem2.Text = " " + Cam.GameSystemName;
                        lblGenre1.Text = "Genre: ";
                        lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                        lblStyle1.Text = "Style: ";
                        lblStyle2.Text = " " + Cam.StyleDescription;
                        lblTechLevel1.Text = "Tech Level: ";
                        lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;  //TODO-Rick-00 Fix this too
                        lblSize1.Text = "Size:";
                        lblSize2.Text = " " + Cam.CampaignSizeRange;
                        lblLocation2.Text = Cam.MarketingLocation;
                        lblEvent2.Text = string.Format("{0:MMM d, yyyy}", Cam.NextEventDate);
                    }
                }
                SetSiteImage(strImage);
                if (strURL != null)
                    SetSiteLink(strURL, strGameOrCampaignName);
                MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
            }
        }

        protected void tvTechLevel_SelectedNodeChanged(object sender, EventArgs e)
        {
            {
                string strURL = "";
                string strImage = "";
                int intImageHeight = 130;
                int intImageWidth = 820;
                string strGameOrCampaignName = "";
                tvTechLevel.SelectedNode.Selected = true;
                string SelectedGorC = tvTechLevel.SelectedValue + "";
                string GorC = SelectedGorC.Substring(0, 1);
                string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
                int GameSystemID;
                int CampaignID;
                int intURLVisible = 1;
                int intImageVisible = 1;
                int intOverviewVisible = 1;
                int intSelectorsVisible = 1;
                int.TryParse(stGameSystemID, out GameSystemID);
                int.TryParse(tvTechLevel.SelectedValue, out CampaignID);
                if (GorC == "G") // Game System
                {
                    intSelectorsVisible = 0;
                    intURLVisible = 0;
                    intImageVisible = 0;
                    lblGorC1.Text = "Genre";
                    lblGorC2.Text = "Genre Description";
                    Classes.cGameSystem GS = new Classes.cGameSystem();
                    GS.Load(GameSystemID, 0);
                    strURL = GS.GameSystemURL;
                    strImage = "";
                    strGameOrCampaignName = GS.GameSystemName;
                    lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
                }
                else  // Campaign
                {
                    if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                    {
                        intURLVisible = 0;
                        intImageVisible = 0;
                        intOverviewVisible = 0;
                        intSelectorsVisible = 0;
                    }
                    else
                    {
                        lblGorC1.Text = "Campaign";
                        lblGorC2.Text = "Campaign Description";
                        Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                        strURL = Cam.URL;
                        strImage = Cam.Logo;
                        intImageHeight = Cam.LogoHeight;
                        intImageWidth = Cam.LogoWidth;
                        strGameOrCampaignName = Cam.CampaignName;
                        lblCampaignOverview.Text = Cam.WebPageDescription;
                        lblGameSystem1.Text = "Game System: ";
                        lblGameSystem2.Text = " " + Cam.GameSystemName;
                        lblGenre1.Text = "Genre: ";
                        lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                        lblStyle1.Text = "Style: ";
                        lblStyle2.Text = " " + Cam.StyleDescription;
                        lblTechLevel1.Text = "Tech Level: ";
                        lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;
                        lblSize1.Text = "Size:";
                        lblSize2.Text = " " + Cam.CampaignSizeRange;
                        lblLocation2.Text = Cam.MarketingLocation;
                        lblEvent2.Text = string.Format("{0:MMM d, yyyy}", Cam.NextEventDate);
                    }
                }
                SetSiteImage(strImage);
                if (strURL != null)
                    SetSiteLink(strURL, strGameOrCampaignName);
                MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
            }
        }

        protected void tvSize_SelectedNodeChanged(object sender, EventArgs e)
        {
            {
                string strURL = "";
                string strImage = "";
                int intImageHeight = 130;
                int intImageWidth = 820;
                string strGameOrCampaignName = "";
                tvSize.SelectedNode.Selected = true;
                string SelectedGorC = tvSize.SelectedValue + "";
                string GorC = SelectedGorC.Substring(0, 1);
                string stGameSystemID = SelectedGorC.Substring(1, SelectedGorC.Length - 1);
                int GameSystemID;
                int CampaignID;
                int intURLVisible = 1;
                int intImageVisible = 1;
                int intOverviewVisible = 1;
                int intSelectorsVisible = 1;
                int.TryParse(stGameSystemID, out GameSystemID);
                int.TryParse(tvSize.SelectedValue, out CampaignID);
                if (GorC == "G") // Game System
                {
                    intSelectorsVisible = 0;
                    intURLVisible = 0;
                    intImageVisible = 0;
                    lblGorC1.Text = "Genre";
                    lblGorC2.Text = "Genre Description";
                    Classes.cGameSystem GS = new Classes.cGameSystem();
                    GS.Load(GameSystemID, 0);
                    strURL = GS.GameSystemURL;
                    strImage = "";
                    strGameOrCampaignName = GS.GameSystemName;
                    lblCampaignOverview.Text = GS.GameSystemWebPageDescription;
                }
                else  // Campaign
                {
                    if (CampaignID < 1) // Placeholder for Game Systems with no campaigns
                    {
                        intURLVisible = 0;
                        intImageVisible = 0;
                        intOverviewVisible = 0;
                        intSelectorsVisible = 0;
                    }
                    else
                    {
                        lblGorC1.Text = "Campaign";
                        lblGorC2.Text = "Campaign Description";
                        Classes.cCampaignBase Cam = new Classes.cCampaignBase(CampaignID, "public", 0);
                        strURL = Cam.URL;
                        strImage = Cam.Logo;
                        intImageHeight = Cam.LogoHeight;
                        intImageWidth = Cam.LogoWidth;
                        strGameOrCampaignName = Cam.CampaignName;
                        lblCampaignOverview.Text = Cam.WebPageDescription;
                        lblGameSystem1.Text = "Game System: ";
                        lblGameSystem2.Text = " " + Cam.GameSystemName;
                        lblGenre1.Text = "Genre: ";
                        lblGenre2.Text = " " + Cam.GenreList; //TODO-Rick-00 Fix this
                        lblStyle1.Text = "Style: ";
                        lblStyle2.Text = " " + Cam.StyleDescription;
                        lblTechLevel1.Text = "Tech Level: ";
                        lblTechLevel2.Text = " " + Cam.TechLevelName + Cam.TechLevelList;
                        lblSize1.Text = "Size:";
                        lblSize2.Text = " " + Cam.CampaignSizeRange;
                        lblLocation2.Text = Cam.MarketingLocation;
                        lblEvent2.Text = string.Format("{0:MMM d, yyyy}", Cam.NextEventDate);
                    }
                }
                SetSiteImage(strImage);
                if (strURL != null)
                    SetSiteLink(strURL, strGameOrCampaignName);
                MakeDetailsVisible(intURLVisible, intImageVisible, intImageHeight, intImageWidth, intOverviewVisible, intSelectorsVisible, CampaignID);
            }
        }

        protected void chkGameSystem_CheckedChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            DateTime edt = DateTime.Now;
            if (chkEndedCampaigns.Checked == true)
                Session["EndDate"] = "";
            else
                Session["EndDate"] = edt.ToShortDateString();
            Session["GameSystemFilter"] = 0;
            if (chkGameSystem.Checked == false)
            {
                Session["GameSystemFilter"] = 0;
                ReloadActiveTreeView(UserID);
            }
            if (chkCampaign.Checked == true)
                Session["CampaignFilter"] = ddlCampaign.SelectedValue.ToInt32();
            else
                Session["CampaignFilter"] = 0;
            if (chkGenre.Checked == true)
                Session["GenreFilter"] = ddlGenre.SelectedValue.ToInt32();
            else
                Session["GenreFilter"] = 0;
            if (chkStyle.Checked == true)
                Session["StyleFilter"] = ddlStyle.SelectedValue.ToInt32();
            else
                Session["StyleFilter"] = 0;
            if (chkTechLevel.Checked == true)
                Session["TechLevelFilter"] = ddlTechLevel.SelectedValue.ToInt32();
            else
                Session["TechLevelFilter"] = 0;
            if (chkSize.Checked == true)
                Session["SizeFilter"] = ddlSize.SelectedValue.ToInt32();
            else
                Session["SizeFilter"] = 0;
            if (chkZipCode.Checked == true)
            {
                Session["ZipCodeFilter"] = txtZipCode.Text;
                Session["RadiusFilter"] = ddlMileRadius.SelectedValue.ToInt32();
            }
            else
            {
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
            }
            if (chkGameSystem.Checked == true)
            {
                LoadddlGameSystem(UserID);
                ddlGameSystem.Visible = true;
            }
            else
            {
                Session["GameSystemFilter"] = 0;
                ddlGameSystem.Visible = false;
            }
        }

        protected void ddlGameSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkGameSystem.Checked == true)
                Session["GameSystemFilter"] = ddlGameSystem.SelectedValue;
            else
                Session["GameSystemFilter"] = 0;
            ReloadActiveTreeView(UserID);
        }

        protected void chkCampaign_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["CampaignFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkCampaign.Checked == true)
            {
                ddlCampaign.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["SizeFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlCampaign(UserID);
            }
            else
            {
                ddlCampaign.Visible = false;
            }

        }

        protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkCampaign.Checked == true)
                Session["CampaignFilter"] = ddlGameSystem.SelectedValue;
            else
                Session["CampaignFilter"] = 0;
            ReloadActiveTreeView(UserID);
        }

        protected void chkGenre_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["GenreFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkGenre.Checked == true)
            {
                ddlGenre.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["SizeFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlGenre(UserID);
            }
            else
            {
                ddlGenre.Visible = false;
            }   
        }

        protected void ddlGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set Session["filterGenre"] to GenreID and reload tv
        }

        protected void chkStyle_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["StyleFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkStyle.Checked == true)
            {
                ddlStyle.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["SizeFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlStyle(UserID);
            }
            else
            {
                ddlStyle.Visible = false;
                //Session["filterStyle"] = 0;
            }    
        }

        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set Session["filterStyle"] to StyleID and reload tv
        }

        protected void chkTechLevel_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["TechLevelFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkTechLevel.Checked == true)
            {
                ddlTechLevel.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["SizeFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlTechLevel(UserID);
            }
            else
            {
                ddlTechLevel.Visible = false;
                //Session["filterTechLevel"] = 0;
            }     
        }

        protected void ddlTechLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set Session["filterTechLevel"] to TechLevelID and reload tv
        }

        protected void chkSize_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["SizeFilter"] = 0;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkSize.Checked == true)
            {
                ddlSize.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                LoadddlSize(UserID);
            }
            else
            {
                ddlSize.Visible = false;
                //Session["filterSize"] = 0;
            }    
        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set Session["filterSize"] to SizeID and reload tv
        }

        protected void chkZipCode_CheckedChanged(object sender, EventArgs e)
        {
            int UserID = 0;
            Session["RadiusFilter"] = 0;
            Session["ZipCode"] = "";
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (chkZipCode.Checked == true)
            {
                txtZipCode.Text = "";
                txtZipCode.Attributes.Add("Placeholder", "Enter your zip code");
                txtZipCode.Visible = true;
                ddlMileRadius.Visible = true;
                Session["EndDate"] = "";
                Session["GameSystemFilter"] = 0;
                Session["CampaignFilter"] = 0;
                Session["GenreFilter"] = 0;
                Session["StyleFilter"] = 0;
                Session["TechLevelFilter"] = 0;
                Session["SizeFilter"] = 0;
                LoadddlMileRadius(UserID);
            }
            else
            {
                txtZipCode.Text = "";
                txtZipCode.Visible = false;
                ddlMileRadius.Visible = false;
                Session["ZipCodeFilter"] = "";
                Session["RadiusFilter"] = 0;
                ReloadActiveTreeView(UserID);
            }
        }

        protected void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            if (txtZipCode.Text == "")
            {
                txtZipCode.Focus();
                Session["ZipCodeFilter"] = "";
            }
            else
            {
                Session["ZipCodeFilter"] = txtZipCode.Text;
                if (ddlMileRadius.SelectedValue.ToString() != "")
                    ReloadActiveTreeView(UserID);
                else
                    ddlMileRadius.Focus();
            }
        }

        protected void ddlMileRadius_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            Session["RadiusFilter"] = ddlMileRadius.SelectedValue;
            if (txtZipCode.Text != "")
                ReloadActiveTreeView(UserID);
            else
                txtZipCode.Focus();
        }

        protected void chkEndedCampaigns_CheckedChanged(object sender, EventArgs e)
        {
            int UserID;
            if (Session["UserID"] == null)
                UserID = 0;
            else
                UserID = ((int)Session["UserID"]);
            ReloadActiveTreeView(UserID);
        }

        protected void ReloadActiveTreeView(int UserID)
        {
            if (tvGameSystem.Visible == true)
                ReloadtvGameSystem(UserID);
            if (tvCampaign.Visible == true)
                ReloadtvCampaign(UserID);
            if (tvGenre.Visible == true)
                ReloadtvGenre(UserID);
            if (tvStyle.Visible == true)
                ReloadtvStyle(UserID);
            if (tvTechLevel.Visible == true)
                ReloadtvTechLevel(UserID);
            if (tvSize.Visible == true)
                ReloadtvSize(UserID);
        }

        protected void LoadddlGameSystem(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0; 
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection GameSystem = new Classes.cCampaignSelection();
            ddlGameSystem.DataSource = GameSystem.LoadGameSystems(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlGameSystem.DataTextField = "GameSystemName";
            ddlGameSystem.DataValueField = "GameSystemID";
            ddlGameSystem.DataBind();
            ddlGameSystem.Items.Insert(0, new ListItem("Select a Game System", ""));
        }

        protected void LoadddlCampaign(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Campaign = new Classes.cCampaignSelection();
            ddlCampaign.DataSource = Campaign.LoadCampaigns(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlCampaign.DataTextField = "CampaignName";
            ddlCampaign.DataValueField = "CampaignID";
            ddlCampaign.DataBind();
            ddlCampaign.Items.Insert(0, new ListItem("Select a Campaign", ""));
        }

        protected void LoadddlGenre(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Genre = new Classes.cCampaignSelection();
            ddlGenre.DataSource = Genre.LoadGenres(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlGenre.DataTextField = "GenreName";
            ddlGenre.DataValueField = "GenreID";
            ddlGenre.DataBind();
            ddlGenre.Items.Insert(0, new ListItem("Select a Genre", ""));
        }

        protected void LoadddlStyle(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Style = new Classes.cCampaignSelection();
            ddlStyle.DataSource = Style.LoadStyles(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlStyle.DataTextField = "StyleName";
            ddlStyle.DataValueField = "StyleID";
            ddlStyle.DataBind();
            ddlStyle.Items.Insert(0, new ListItem("Select a Style", ""));
        }

        protected void LoadddlTechLevel(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection TechLevel = new Classes.cCampaignSelection();
            ddlTechLevel.DataSource = TechLevel.LoadTechLevels(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlTechLevel.DataTextField = "TechLevelName";
            ddlTechLevel.DataValueField = "TechLevelID";
            ddlTechLevel.DataBind();
            ddlTechLevel.Items.Insert(0, new ListItem("Select a Tech Level", ""));
        }

        protected void LoadddlSize(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Size = new Classes.cCampaignSelection();
            ddlSize.DataSource = Size.LoadSizes(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlSize.DataTextField = "CampaignSizeRange";
            //ddlSize.DataValueField = "CampaignSizeID";
            ddlSize.DataBind();
            ddlSize.Items.Insert(0, new ListItem("Select a Size", ""));
        }

        protected void LoadddlMileRadius(int UserID)
        {
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Radius = new Classes.cCampaignSelection();
            ddlMileRadius.DataSource = Radius.LoadRadius(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            ddlMileRadius.DataTextField = "DistanceDescription";
            ddlMileRadius.DataValueField = "DistanceID";
            ddlMileRadius.DataBind();
            ddlMileRadius.Items.Insert(0, new ListItem("Select a Maximum Distance", ""));
        }

        protected void ReloadtvGameSystem(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string GameSystemName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode GameSystemNode;
            TreeNode CampaignNode;
            tvGameSystem.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection GameSystems = new Classes.cCampaignSelection();
            DataTable dtGameSystems = new DataTable();
            dtGameSystems = GameSystems.LoadGameSystems(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtGameSystems.Rows)
            {
                if (int.TryParse(dRow["GameSystemID"].ToString(), out iTemp))
                    GameSystemFilter = iTemp;
                GameSystemName = dRow["GameSystemName"].ToString();
                GameSystemNode = new TreeNode(GameSystemName, "G" + GameSystemFilter); // G will be assigned all Game Systems and C will be assigned to Campaigns
                GameSystemNode.Selected = false;
                GameSystemNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByGameSystem(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    GameSystemNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                GameSystemNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }    
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            GameSystemNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        GameSystemNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvGameSystem.Nodes.Add(GameSystemNode);
            }
        }

        protected void ReloadtvCampaign(int UserID)
        {
            int StatusID = 0;
            string CampaignName = "";
            int iTemp;
            TreeNode CampaignNode;
            tvCampaign.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
            DataTable dtCampaigns = new DataTable();
            dtCampaigns = Campaigns.LoadCampaigns(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            if (dtCampaigns.Rows.Count > 0)
            {
                foreach (DataRow dRow in dtCampaigns.Rows)
                {
                    if (int.TryParse(dRow["CampaignID"].ToString(), out iTemp))
                        CampaignFilter = iTemp;
                    if (int.TryParse(dRow["StatusID"].ToString(), out iTemp))
                        StatusID = iTemp;
                    CampaignName = dRow["CampaignName"].ToString();
                    if (StatusID != 4)
                    {
                        CampaignNode = new TreeNode(CampaignName, CampaignFilter.ToString());
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                        tvCampaign.Nodes.Add(CampaignNode);
                    }
                    else
                    {
                        if (chkEndedCampaigns.Checked == true)
                        {
                            CampaignName = CampaignName + " (Ended)";
                            CampaignNode = new TreeNode(CampaignName, CampaignFilter.ToString());
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                            tvCampaign.Nodes.Add(CampaignNode);
                        }
                    }
                }
            }
            else
            {
                CampaignNode = new TreeNode("No Campaigns Available", "0");
                CampaignNode.Selected = false;
                CampaignNode.NavigateUrl = "";
                tvCampaign.Nodes.Add(CampaignNode);
            }
        }

        protected void ReloadtvGenre(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string GenreName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode GenreNode;
            TreeNode CampaignNode;
            tvGenre.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Genres = new Classes.cCampaignSelection();
            DataTable dtGenres = new DataTable();
            dtGenres = Genres.LoadGenres(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtGenres.Rows)
            {
                if (int.TryParse(dRow["GenreID"].ToString(), out iTemp))
                    GenreFilter = iTemp;
                GenreName = dRow["GenreName"].ToString();
                GenreNode = new TreeNode(GenreName, "G" + GenreFilter); // G will be assigned all Genres and C will be assigned to Campaigns
                GenreNode.Selected = false;
                GenreNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByGenre(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    GenreNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                GenreNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }    
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            GenreNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        GenreNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvGenre.Nodes.Add(GenreNode);
            }
        }

        protected void ReloadtvStyle(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string StyleName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode StyleNode;
            TreeNode CampaignNode;
            tvStyle.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Styles = new Classes.cCampaignSelection();
            DataTable dtStyles = new DataTable();
            dtStyles = Styles.LoadStyles(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtStyles.Rows)
            {
                if (int.TryParse(dRow["StyleID"].ToString(), out iTemp))
                    StyleFilter = iTemp;
                StyleName = dRow["StyleName"].ToString();
                StyleNode = new TreeNode(StyleName, "G" + StyleFilter); // G will be assigned all Styles and C will be assigned to Campaigns
                StyleNode.Selected = false;
                StyleNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByStyle(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    StyleNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                StyleNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            StyleNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        StyleNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvStyle.Nodes.Add(StyleNode);
            }
        }

        protected void ReloadtvTechLevel(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string TechLevelName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode TechLevelNode;
            TreeNode CampaignNode;
            tvTechLevel.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection TechLevels = new Classes.cCampaignSelection();
            DataTable dtTechLevels = new DataTable();
            dtTechLevels = TechLevels.LoadTechLevels(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtTechLevels.Rows)
            {
                if (int.TryParse(dRow["TechLevelID"].ToString(), out iTemp))
                    TechLevelFilter = iTemp;
                TechLevelName = dRow["TechLevelName"].ToString();
                TechLevelNode = new TreeNode(TechLevelName, "G" + TechLevelFilter); // G will be assigned all Styles and C will be assigned to Campaigns
                TechLevelNode.Selected = false;
                TechLevelNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsByTechLevel(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    TechLevelNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                TechLevelNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            TechLevelNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        TechLevelNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvTechLevel.Nodes.Add(TechLevelNode);
            }
        }

        protected void ReloadtvSize(int UserID)
        {
            int CampaignID = 0;
            int StatusID = 0;
            string SizeName = "";
            string CampaignName = "";
            int iTemp;
            int NumberOfCampaignsInThisSystem = 0;
            TreeNode SizeNode;
            TreeNode CampaignNode;
            tvSize.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate;
            int GameSystemFilter;
            int CampaignFilter;
            int GenreFilter;
            int StyleFilter;
            int TechLevelFilter;
            int SizeFilter;
            string ZipCodeFilter;
            int RadiusFilter;
            if (Session["EndDate"] != null)
                EndDate = Session["EndDate"].ToString();
            else
                EndDate = "";
            if (Session["GameSystemFilter"] != null)
                GameSystemFilter = Session["GameSystemFilter"].ToString().ToInt32();
            else
                GameSystemFilter = 0;
            if (Session["CampaignFilter"] != null)
                CampaignFilter = Session["CampaignFilter"].ToString().ToInt32();
            else
                CampaignFilter = 0;
            if (Session["GenreFilter"] != null)
                GenreFilter = Session["GenreFilter"].ToString().ToInt32();
            else
                GenreFilter = 0;
            if (Session["StyleFilter"] != null)
                StyleFilter = Session["StyleFilter"].ToString().ToInt32();
            else
                StyleFilter = 0;
            if (Session["TechLevelFilter"] != null)
                TechLevelFilter = Session["TechLevelFilter"].ToString().ToInt32();
            else
                TechLevelFilter = 0;
            if (Session["SizeFilter"] != null)
                SizeFilter = Session["SizeFilter"].ToString().ToInt32();
            else
                SizeFilter = 0;
            if (Session["ZipCodeFilter"] != null)
                ZipCodeFilter = Session["ZipCodeFilter"].ToString();
            else
                ZipCodeFilter = "";
            if (Session["RadiusFilter"] != null)
                RadiusFilter = Session["RadiusFilter"].ToString().ToInt32();
            else
                RadiusFilter = 0;
            // End Session Variable Definition for calling class
            Classes.cCampaignSelection Sizes = new Classes.cCampaignSelection();
            DataTable dtSizes = new DataTable();
            dtSizes = Sizes.LoadSizes(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            foreach (DataRow dRow in dtSizes.Rows)
            {
                if (int.TryParse(dRow["CampaignSizeID"].ToString(), out iTemp))
                    SizeFilter = iTemp;
                SizeName = dRow["CampaignSizeRange"].ToString();
                SizeNode = new TreeNode(SizeName, "G" + TechLevelFilter); // G will be assigned all Styles and C will be assigned to Campaigns
                SizeNode.Selected = false;
                SizeNode.NavigateUrl = "";
                Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
                DataTable dtCampaigns = new DataTable();
                dtCampaigns = Campaigns.CampaignsBySize(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
                // Loop through all the Campaign rows of dtCampaigns
                NumberOfCampaignsInThisSystem = 0;
                if (dtCampaigns.Rows.Count == 0)
                {
                    // Add an "empty" node to indicate no campaigns (and line up the tree)
                    CampaignID = 0;
                    StatusID = 0;
                    CampaignName = "No Campaigns";
                    CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                    SizeNode.ChildNodes.Add(CampaignNode);
                    CampaignNode.Selected = false;
                    CampaignNode.NavigateUrl = "";
                }
                else
                {
                    foreach (DataRow cRow in dtCampaigns.Rows)
                    {
                        if (int.TryParse(cRow["CampaignID"].ToString(), out iTemp))
                            CampaignID = iTemp;
                        if (int.TryParse(cRow["StatusID"].ToString(), out iTemp))
                            StatusID = iTemp;
                        CampaignName = cRow["CampaignName"].ToString();
                        if (StatusID == 4)   // Past/Ended
                        {
                            CampaignName = CampaignName + " (Ended)";
                            if (chkEndedCampaigns.Checked == true)
                            {
                                CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                                SizeNode.ChildNodes.Add(CampaignNode);
                                NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                                CampaignNode.Selected = false;
                                CampaignNode.NavigateUrl = "";
                            }
                        }
                        else
                        {
                            CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                            SizeNode.ChildNodes.Add(CampaignNode);
                            NumberOfCampaignsInThisSystem = NumberOfCampaignsInThisSystem + 1;
                            CampaignNode.Selected = false;
                            CampaignNode.NavigateUrl = "";
                        }
                    }
                    if (NumberOfCampaignsInThisSystem == 0)
                    {
                        // Add an "empty" node to indicate no campaigns (and line up the tree)
                        CampaignID = 0;
                        StatusID = 0;
                        CampaignName = "No Campaigns";
                        CampaignNode = new TreeNode(CampaignName, CampaignID.ToString());
                        SizeNode.ChildNodes.Add(CampaignNode);
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                    }
                }
                tvSize.Nodes.Add(SizeNode);
            }
        }
    }
}