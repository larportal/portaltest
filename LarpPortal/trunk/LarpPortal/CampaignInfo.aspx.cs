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

namespace LarpPortal
{
    public partial class CampaignInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ActiveLeftNav"] = "CampaignInfoSetup";
                if (Session["CampaignName"] != null)
                    lblTopCampaignName.Text = Session["CampaignName"].ToString();
                else
                    lblTopCampaignName.Text = "";
            }
            int CampaignID;
            string UserName;
            int UserID;
            if (Session["Username"] != null)
                UserName = Session["Username"].ToString();
            else
            {
                UserName = "";
                Response.Redirect("~/index.aspx");
            }
            if (Session["CampaignID"] == null)
            {
                Classes.cUser User = new Classes.cUser(UserName, "PasswordNotNeeded");
                CampaignID = User.LastLoggedInCampaign;
            }
            else
            {
                int.TryParse(Session["CampaignID"].ToString(), out CampaignID);
            }
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out UserID);
            //UserID = ((int)Session["UserID"]);
            else
            {
                UserID = 0;
            }
            Classes.cCampaignBase CampaignBase = new Classes.cCampaignBase(CampaignID, UserName, UserID);
            txtCampaignName.Text = CampaignBase.CampaignName;
            DateTime? dtStartDate = CampaignBase.StartDate;
            txtStarted.Text = string.Format("{0:MMM d, yyyy}", dtStartDate);
            DateTime? dtExpEndDate = CampaignBase.ProjectedEndDate;
            txtExpectedEnd.Text = string.Format("{0:MMM d, yyyy}", dtExpEndDate);
            txtNumEvents.Text = CampaignBase.ActualNumberOfEvents.ToString();
            txtGameSystem.Text = CampaignBase.GameSystemName;
            txtGenres.Text = CampaignBase.GenreList;
            txtStyle.Text = CampaignBase.StyleDescription;
            txtTechLevel.Text = CampaignBase.TechLevelName;
            txtSize.Text = CampaignBase.CampaignSizeRange;
            txtAvgNumEvents.Text = CampaignBase.ProjectedNumberOfEvents.ToString();
            string c1 = CampaignBase.URL;    //CampaignGMURL
            string c2 = "";
            string c4 = "";
            string c6 = "";
            string c8 = "";
            string c10 = "";
            string c12 = "";
            string c14 = "";
            string c16 = "";
            string c18 = "";
            if (CampaignBase.ShowCampaignInfoEmail == true)
            {
                c2 = CampaignBase.InfoRequestEmail;    //CampgaignGMEmail
            }
            string c3 = CampaignBase.CharacterGeneratorURL;   //CharacterURL
            if (CampaignBase.ShowCharacterNotificationEmail == true)
            {
                c4 = CampaignBase.CharacterNotificationEMail;    //CharacterEmail
            }
            string c5 = CampaignBase.CharacterHistoryURL;   //CharHistoryURl
            if (CampaignBase.ShowCharacterHistoryEmail == true)
            {
                c6 = CampaignBase.CharacterHistoryNotificationEmail;   //CharHistoryEmail
            }
            string c7 = CampaignBase.InfoSkillURL;   //InfoSkillsURL
            if (CampaignBase.ShowInfoSkillEmail == true)
            {
                c8 = CampaignBase.InfoSkillEMail;   //InfoSkillsEmail
            }
            string c9 = CampaignBase.ProductionSkillURL; //ProdSkillsURL
            if (CampaignBase.ShowProductionSkillEmail == true)
            {
                c10 = CampaignBase.ProductionSkillEMail; //ProdSkillsEmail
            }
            string c11 = CampaignBase.PELSubmissionURL;  //PELURL
            if (CampaignBase.ShowPELNotificationEmail == true)
            {
                c12 = CampaignBase.PELNotificationEMail;    //PELEmail
            }
            string c13 = "";
            if (CampaignBase.ShowCPNotificationEmail == true)
            {
                c14 = CampaignBase.CPNotificationEmail;
            }
            string c15 = CampaignBase.JoinURL;
            if (CampaignBase.ShowJoinRequestEmail == true)
            {
                c16 = CampaignBase.JoinRequestEmail;
            }
            string c17 = CampaignBase.RegistrationURL;
            if (CampaignBase.ShowRegistrationNotificationEmail == true)
            {
                c18 = CampaignBase.RegistrationNotificationEmail;
            }
            // Registration Notification Email            

            BuildContacts(c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18);
            CampaignDescription.Text = CampaignBase.WebPageDescription.Replace("\n", "<br>");
            txtMembershipFee.Text = CampaignBase.MembershipFee.ToString();
            txtFrequency.Text = CampaignBase.MembershipFeeFrequency;
            txtMinimumAge.Text = CampaignBase.MinimumAge.ToString();
            txtSupervisedAge.Text = CampaignBase.MinimumAgeWithSupervision.ToString();
            txtWaiver1.Text = "Waiver source TBD"; //TODO-Rick-2 Find the two (or more) waivers - may have to allow for even more
            txtWaiver2.Text = "Waiver source 2 TBD";
            txtConsent.Text = "Consent TBD"; //TODO-Rick-3 Find out where the consent is stored (what is consent anyway?)
        }

        private void BuildContacts(string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, string c10, string c11, string c12,
            string c13, string c14, string c15, string c16, string c17, string c18)
        {
            // Load My Campaigns selection
            string hrefline = "";
            string CampaignInfoCheck = (c1 + c2);
            if (CampaignInfoCheck != "")
            {
                CampaignInfoCheck = "True";
            }
            string CharacterInfoCheck = (c3 + c4);
            if (CharacterInfoCheck != "")
            {
                CharacterInfoCheck = "True";
            }
            string CharacterHistoryCheck = (c5 + c6);
            if (CharacterHistoryCheck != "")
            {
                CharacterHistoryCheck = "True";
            }
            string InfoSkillCheck = (c7 + c8);
            if (InfoSkillCheck != "")
            {
                InfoSkillCheck = "True";
            }
            string ProductionSkillCheck = (c9 + c10);
            if (ProductionSkillCheck != "")
            {
                ProductionSkillCheck = "True";
            }
            string PELCheck = (c11 + c12);
            if (PELCheck != "")
            {
                PELCheck = "True";
            }
            string CPNotificationCheck = (c13 + c14);
            if (CPNotificationCheck != "")
            {
                CPNotificationCheck = "True";
            }
            string JoinCheck = (c15 + c16);
            if (JoinCheck != "")
            {
                JoinCheck = "True";
            }
            string RegistrationCheck = (c17 + c18);
            if (RegistrationCheck != "")
            {
                RegistrationCheck = "True";
            }
            // Constant values
            string DoubleQuote = "\"";
            int liLinesNeeded = 63;
            DataTable ContactsTable = new DataTable();
            ContactsTable.Columns.Add("href_li");
            for (int i = 0; i <= liLinesNeeded; i++)
            {
                //build on case of i
                switch (i)
                {
                    case 0:
                        hrefline = "<ul class=" + DoubleQuote + "col-sm-12" + DoubleQuote + ">";
                        hrefline = "<table>";
                        break;
                    case 1:
                        if (CampaignInfoCheck == "True")
                        {
                            hrefline = "<li>";
                            hrefline = "<tr>";
                        }
                        break;
                    case 2:
                        if (CampaignInfoCheck == "True")
                        {
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                            hrefline = "";
                        }
                        break;
                    case 3:
                        //c1 = CampaignGMURL
                        //c2 = CampgaignGMEmail
                        if (CampaignInfoCheck == "True")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + ">Campaign Info</li>";
                            hrefline = "<td>Campaign Info</td><td>&nbsp;&nbsp;</td>";
                        }
                        break;
                    case 4:
                        if (c1 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + c1 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Campaign URL</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + c1 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Campaign URL</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 5:
                        if (c2 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + "mailto:" + c2 + DoubleQuote + ">Information Email</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + "mailto:" + c2 + DoubleQuote + ">Information Email</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 6:
                        if (CampaignInfoCheck == "True")
                        {
                            hrefline = "</ul>";
                            hrefline = "";
                        }
                        break;
                    case 7:
                        if (CampaignInfoCheck == "True")
                        {
                            hrefline = "</li>";
                            hrefline = "</tr>";
                        }
                        break;
                    case 8:  // New row
                        if (CharacterInfoCheck == "True")
                        {
                            hrefline = "<li>";
                            hrefline = "<tr>";
                        }
                        break;
                    case 9:
                        if (CharacterInfoCheck == "True")
                        {
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                            hrefline = "";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 10:
                        //c3 = CharacterURL
                        //c4 = CharacterEmail
                        if (CharacterInfoCheck == "True")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + ">Character</li>";
                            hrefline = "<td>Character</td><td></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 11:
                        if (c3 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + c3 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Character URL</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + c3 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Character URL</a></td>";
                        }
                        break;
                    case 12:
                        if (c4 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + "mailto:" + c4 + DoubleQuote + ">Character Email</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + "mailto:" + c4 + DoubleQuote + ">Character Email</a></td>";
                        }
                        break;
                    case 13:
                        if (CharacterInfoCheck == "True")
                        {
                            hrefline = "</ul>";
                            hrefline = "</tr>";
                        }
                        break;
                    case 14:  // New row
                        if (CharacterHistoryCheck == "True")
                        {
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                            hrefline = "<tr>";
                        }
                        break;
                    case 15:
                        //c5 = CharHistoryURl
                        //c6 = CharHistoryEmail
                        if (CharacterHistoryCheck == "True")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + ">Character History</li>";
                            hrefline = "<td>Character History</td><td></td>";
                        }
                        break;
                    case 16:
                        if (c5 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + c5 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Character History URL</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + c5 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Character History URL</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 17:
                        if (c6 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + "mailto:" + c6 + DoubleQuote + ">Character History Email</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + "mailto:" + c6 + DoubleQuote + ">Character History Email</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 18:
                        if (CharacterHistoryCheck == "True")
                        {
                            hrefline = "</ul>";
                            hrefline = "";
                        }
                        break;
                    case 19:
                        if (CharacterHistoryCheck == "True")
                        {
                            hrefline = "</li>";
                            hrefline = "</tr>";
                        }
                        break;
                    case 20:  // New row
                        if (InfoSkillCheck == "True")
                        {
                            hrefline = "<li>";
                            hrefline = "<tr>";
                        }
                        break;
                    case 21:
                        if (InfoSkillCheck == "True")
                        {
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                            hrefline = "";
                        }
                        break;
                    case 22:
                        //c7 = InfoSkillsURL
                        //c8 = InfoSkillsEmail
                        if (InfoSkillCheck == "True")
                        {
                            hrefline = "<li>Info Skills</li>";
                            hrefline = "<td>Info Skills</td><td></td>";
                        }
                        break;
                    case 23:
                        if (c7 != "")
                        {
                            hrefline = "<li><a href=" + DoubleQuote + c7 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote +">Info Skills URL</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + c7 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote +">Info Skills URL</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 24:
                        if (c8 != "")
                        {
                            hrefline = "<li><a href=" + DoubleQuote + "mailto:" + c8 + DoubleQuote + ">Info Skills Email</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + "mailto:" + c8 + DoubleQuote + ">Info Skills Email</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 25:
                        if (InfoSkillCheck == "True")
                        {
                            hrefline = "</ul>";
                            hrefline = "</tr>";
                        }
                        break;
                    case 26:  // New row
                        if (ProductionSkillCheck == "True")
                        {
                            hrefline = "</li>";
                            hrefline = "<tr>";
                        }
                        break;
                    case 27:
                        if (ProductionSkillCheck == "True")
                        {
                            hrefline = "<li>";
                            hrefline = "";
                        }
                        break;
                    case 28:
                        if (ProductionSkillCheck == "True")
                        {
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                            hrefline = "";
                        }
                        break;
                    case 29:
                        //c9 = ProdSkillsURL
                        //c10 = ProdSkillsEmail
                        if (ProductionSkillCheck == "True")
                        {
                            hrefline = "<li>Production Skills</li>";
                            hrefline = "<td>Production Skills</td><td></td>";
                        }
                        break;
                    case 30:
                        if (c9 != "")
                        {
                            hrefline = "<li><a href=" + DoubleQuote + c9 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote +">Production Skills URL</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + c9 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote +">Production Skills URL</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 31:
                        if (c10 != "")
                        {
                            hrefline = "<li><a href=" + DoubleQuote + "mailto:" + c10 + DoubleQuote + ">Production Skills Email</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + "mailto:" + c10 + DoubleQuote + ">Production Skills Email</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 32:
                        if (ProductionSkillCheck == "True")
                        {
                            hrefline = "</ul>";
                            hrefline = "</tr>";
                        }
                        break;
                    case 33:  // New row
                        if (PELCheck == "True")
                            hrefline = "</li>";
                        hrefline = "</tr>";
                        break;
                    case 34:
                        if (PELCheck == "True")
                        {
                            hrefline = "<li>";
                            hrefline = "";
                        }
                        break;
                    case 35:
                        if (PELCheck == "True")
                        {
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                            hrefline = "";
                        }
                        break;
                    case 36:
                        //c11 = PELURL
                        //c12 = PELEmail
                        if (PELCheck == "True")
                        {
                            hrefline = "<li>PEL</li>";
                            hrefline = "<td>PEL</td><td></td>";
                        }
                        break;
                    case 37:
                        if (c11 != "")
                        {
                            hrefline = "<li><a href=" + DoubleQuote + c11 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote +">PEL URL</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + c11 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">PEL URL</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 38:
                        if (c12 != "")
                        {
                            hrefline = "<li><a href=" + DoubleQuote + "mailto:" + c12 + DoubleQuote + ">PEL Email</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + "mailto:" + c12 + DoubleQuote + ">PEL Email</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 39:
                        if (PELCheck == "True")
                        {
                            hrefline = "</ul>";
                            hrefline = "</tr>";
                        }
                        break;
                    case 40:
                        if (PELCheck == "True")
                        {
                            hrefline = "</li>";
                            hrefline = "";
                        }
                        break;

                        //New line 1 CP Notification
                    case 41:
                        if (CPNotificationCheck == "True")
                        {
                            hrefline = "<li>";
                            hrefline = "<tr>";
                        }
                        break;
                    case 42:
                        if (CPNotificationCheck == "True")
                        {
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                            hrefline = "";
                        }
                        break;
                    case 43:
                        //c13 = CP URL
                        //c14 = CP Email
                        if (CPNotificationCheck == "True")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + ">Points</li>";
                            hrefline = "<td>Points</td><td>&nbsp;&nbsp;</td>";
                        }
                        break;
                    case 44:
                        if (c13 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + c13 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Points URL</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + c13 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Points URL</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 45:
                        if (c14 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + "mailto:" + c14 + DoubleQuote + ">Points Email</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + "mailto:" + c14 + DoubleQuote + ">Points Email</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 46:
                        if (CPNotificationCheck == "True")
                        {
                            hrefline = "</ul>";
                            hrefline = "";
                        }
                        break;
                    case 47:
                        if (CPNotificationCheck == "True")
                        {
                            hrefline = "</li>";
                            hrefline = "</tr>";
                        }
                        break;


                        //New line 2 Join
                    case 48:
                        if (JoinCheck == "True")
                        {
                            hrefline = "<li>";
                            hrefline = "<tr>";
                        }
                        break;
                    case 49:
                        if (JoinCheck == "True")
                        {
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                            hrefline = "";
                        }
                        break;
                    case 50:
                        //c15 = Join URL
                        //c16 = Join Email
                        if (JoinCheck == "True")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + ">Sign-up</li>";
                            hrefline = "<td>Sign-up</td><td>&nbsp;&nbsp;</td>";
                        }
                        break;
                    case 51:
                        if (c15 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + c15 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Sign-up URL</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + c15 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Sign-up URL</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 52:
                        if (c6 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + "mailto:" + c16 + DoubleQuote + ">Sign-up Email</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + "mailto:" + c16 + DoubleQuote + ">Sign-up Email</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 53:
                        if (JoinCheck == "True")
                        {
                            hrefline = "</ul>";
                            hrefline = "";
                        }
                        break;
                    case 54:
                        if (JoinCheck == "True")
                        {
                            hrefline = "</li>";
                            hrefline = "</tr>";
                        }
                        break;


                        //New line 3 Registration
                    case 55:
                        if (RegistrationCheck == "True")
                        {
                            hrefline = "<li>";
                            hrefline = "<tr>";
                        }
                        break;
                    case 56:
                        if (RegistrationCheck == "True")
                        {
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                            hrefline = "";
                        }
                        break;
                    case 57:
                        //c17 = Registration URL
                        //c18 = Registration Email
                        if (RegistrationCheck == "True")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + ">Registration</li>";
                            hrefline = "<td>Registration</td><td>&nbsp;&nbsp;</td>";
                        }
                        break;
                    case 58:
                        if (c17 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + c17 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Registration URL</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + c17 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Registration URL</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 59:
                        if (c18 != "")
                        {
                            hrefline = "<li class=" + DoubleQuote + "col=sm-6" + DoubleQuote + "><a href=" + DoubleQuote + "mailto:" + c18 + DoubleQuote + ">Registration Email</a></li>";
                            hrefline = "<td><a href=" + DoubleQuote + "mailto:" + c18 + DoubleQuote + ">Registration Email</a></td>";
                        }
                        else
                        {
                            hrefline = "<td></td>";
                        }
                        break;
                    case 60:
                        if (RegistrationCheck == "True")
                        {
                            hrefline = "</ul>";
                            hrefline = "";
                        }
                        break;
                    case 61:
                        if (RegistrationCheck == "True")
                        {
                            hrefline = "</li>";
                            hrefline = "</tr>";
                        }
                        break;

                    case 62:  // End
                        hrefline = "</ul>";
                        hrefline = "</table>";
                        break;
                }
                DataRow ContactsRow = ContactsTable.NewRow();
                ContactsRow["href_li"] = hrefline;
                ContactsTable.Rows.Add(ContactsRow);
                menu_ul_contacts.DataSource = ContactsTable;
                menu_ul_contacts.DataBind();
                hrefline = "";
            }
        }
    }
}