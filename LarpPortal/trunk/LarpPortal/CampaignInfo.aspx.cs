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
                lblTopCampaignName.Text = Session["CampaignName"].ToString();
                lblWIP.BackColor = System.Drawing.Color.Yellow;
                lblWIP.Text = " (WIP)";
            }
            int CampaignID;
            string UserName;
            int UserID;
            if (Session["Username"] != null)
                UserName = Session["Username"].ToString();
            else
            {
                UserName = "";
                //If we have a null Username here should we assume all is FUBAR and redirect to login?
                Response.Redirect("~/index.aspx");
            }
            if (Session["CampaignID"] == null)
            {
                Classes.cUser User = new Classes.cUser(UserName, "PasswordNotNeeded");
                CampaignID = User.LastLoggedInCampaign;
            }
            else
            {
                CampaignID = ((int)Session["CampaignID"]);
            }
            if (Session["UserID"] != null)
                UserID = ((int)Session["UserID"]);
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
            txtGenre1.Text = "Genre1 TBD";
            txtGenre2.Text = "Genre2 TBD";
            txtStyle.Text = CampaignBase.StyleDescription;
            txtTechLevel.Text = CampaignBase.TechLevelName;
            txtSize.Text = CampaignBase.MarketingCampaignSize.ToString();
            txtAvgNumEvents.Text = CampaignBase.ProjectedNumberOfEvents.ToString();
            // TODO-Jack-Define contacts variables and pass them through programatically to build contacts section
            string c1 = CampaignBase.URL;    //CampaignGMURL
            string c2 = CampaignBase.InfoRequestEmail;    //CampgaignGMEmail
            string c3 = CampaignBase.CharacterGeneratorURL;   //CharacterURL
            string c4 = CampaignBase.CharacterNotificationEMail;    //CharacterEmail
            string c5 = CampaignBase.CharacterHistoryURL;   //CharHistoryURl
            string c6 = CampaignBase.CharacterHistoryNotificationEmail;   //CharHistoryEmail
            string c7 = CampaignBase.InfoSkillURL;   //InfoSkillsURL
            string c8 = CampaignBase.InfoSkillEMail;   //InfoSkillsEmail
            string c9 = CampaignBase.ProductionSkillURL; //ProdSkillsURL
            string c10 = CampaignBase.ProductionSkillEMail; //ProdSkillsEmail
            string c11 = CampaignBase.PELSubmissionURL;  //PELURL
            string c12 = CampaignBase.PELNotificationEMail;    //PELEmail
            BuildContacts(c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12);
            CampaignDescription.Text = CampaignBase.WebPageDescription;
            txtMembershipFee.Text = CampaignBase.MembershipFee.ToString();
            txtFrequency.Text = CampaignBase.MembershipFeeFrequency;
            txtMinimumAge.Text = CampaignBase.MinimumAge.ToString();
            txtSupervisedAge.Text = CampaignBase.MinimumAgeWithSupervision.ToString();
            txtWaiver1.Text = "Waiver source TBD"; //TODO-Rick-2 Find the two (or more) waivers - may have to allow for even more
            txtWaiver2.Text = "Waiver source 2 TBD";
            txtConsent.Text = "Consent TBD"; //TODO-Rick-3 Find out where the consent is stored (what is consent anyway?)
        }

        private void BuildContacts(string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, string c10, string c11, string c12)
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
            // Constant values
            string DoubleQuote = "\"";
            int liLinesNeeded = 42;
            DataTable ContactsTable = new DataTable();
            ContactsTable.Columns.Add("href_li");
            for (int i = 0; i <= liLinesNeeded; i++)
            {
                //build on case of i
                switch (i)
                {
                    case 0:
                        hrefline = "<ul class=" + DoubleQuote + "col-sm-12" + DoubleQuote + ">";
                        break;
                    case 1:
                        if (CampaignInfoCheck == "True")
                            hrefline = "<li>";
                        break;
                    case 2:
                        if (CampaignInfoCheck == "True")
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                        break;
                    case 3:
                        //c1 = CampaignGMURL
                        //c2 = CampgaignGMEmail
                        if (CampaignInfoCheck == "True")
                            hrefline = "<li class=" + DoubleQuote + "col=sm-4" + DoubleQuote + ">Campaign Info</li>";
                        break;
                    case 4:
                        if (c1 != "")
                            hrefline = "<li class=" + DoubleQuote + "col=sm-4" + DoubleQuote + "><a href=" + DoubleQuote + c1 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Campaign URL</a></li>";
                        break;
                    case 5:
                        if (c2 != "")
                            hrefline = "<li class=" + DoubleQuote + "col=sm-4" + DoubleQuote + "><a href=" + DoubleQuote + "mailto:" + c2 + DoubleQuote + ">Information Email</a></li>";
                        break;
                    case 6:
                        if (CampaignInfoCheck == "True")
                            hrefline = "</ul>";
                        break;
                    case 7:
                        if (CampaignInfoCheck == "True")
                            hrefline = "</li>";
                        break;
                    case 8:
                        if (CharacterInfoCheck == "True")
                            hrefline = "<li>";
                        break;
                    case 9:
                        if (CharacterInfoCheck == "True")
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                        break;
                    case 10:
                        //c3 = CharacterURL
                        //c4 = CharacterEmail
                        if (CharacterInfoCheck == "True")
                            hrefline = "<li class=" + DoubleQuote + "col=sm-4" + DoubleQuote + ">Character</li>";
                        break;
                    case 11:
                        if (c3 != "")
                            hrefline = "<li class=" + DoubleQuote + "col=sm-4" + DoubleQuote + "><a href=" + DoubleQuote + c3 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Character URL</a></li>";
                        break;
                    case 12:
                        if (c4 != "")
                            hrefline = "<li class=" + DoubleQuote + "col=sm-4" + DoubleQuote + "><a href=" + DoubleQuote + "mailto:" + c4 + DoubleQuote + ">Character Email</a></li>";
                        break;
                    case 13:
                        if (CharacterInfoCheck == "True")
                            hrefline = "</ul>";
                        break;
                    case 14:
                        if (CharacterHistoryCheck == "True")
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                        break;
                    case 15:
                        //c5 = CharHistoryURl
                        //c6 = CharHistoryEmail
                        if (CharacterHistoryCheck == "True")
                            hrefline = "<li class=" + DoubleQuote + "col=sm-4" + DoubleQuote + ">Character History</li>";
                        break;
                    case 16:
                        if (c5 != "")
                            hrefline = "<li class=" + DoubleQuote + "col=sm-4" + DoubleQuote + "><a href=" + DoubleQuote + c5 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote + ">Character History URL</a></li>";
                        break;
                    case 17:
                        if (c6 != "")
                            hrefline = "<li class=" + DoubleQuote + "col=sm-4" + DoubleQuote + "><a href=" + DoubleQuote + "mailto:" + c6 + DoubleQuote + ">Character History Email</a></li>";
                        break;
                    case 18:
                        if (CharacterHistoryCheck == "True")
                            hrefline = "</ul>";
                        break;
                    case 19:
                        if (CharacterHistoryCheck == "True")
                            hrefline = "</li>";
                        break;
                    case 20:
                        if (InfoSkillCheck == "True")
                            hrefline = "<li>";
                        break;
                    case 21:
                        if (InfoSkillCheck == "True")
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                        break;
                    case 22:
                        //c7 = InfoSkillsURL
                        //c8 = InfoSkillsEmail
                        if (InfoSkillCheck == "True")
                            hrefline = "<li>Info Skills</li>";
                        break;
                    case 23:
                        if (c7 != "")
                            hrefline = "<li><a href=" + DoubleQuote + c7 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote +">Info Skills URL</a></li>";
                        break;
                    case 24:
                        if (c8 != "")
                            hrefline = "<li><a href=" + DoubleQuote + "mailto:" + c8 + DoubleQuote + ">InfoSkills@campaign.com</a></li>";
                        break;
                    case 25:
                        if (InfoSkillCheck == "True")
                            hrefline = "</ul>";
                        break;
                    case 26:
                        if (ProductionSkillCheck == "True")
                            hrefline = "</li>";
                        break;
                    case 27:
                        if (ProductionSkillCheck == "True")
                            hrefline = "<li>";
                        break;
                    case 28:
                        if (ProductionSkillCheck == "True")
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                        break;
                    case 29:
                        //c9 = ProdSkillsURL
                        //c10 = ProdSkillsEmail
                        if (ProductionSkillCheck == "True")
                            hrefline = "<li>Production Skills</li>";
                        break;
                    case 30:
                        if (c9 != "")
                            hrefline = "<li><a href=" + DoubleQuote + c9 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote +">Production Skills URL</a></li>";
                        break;
                    case 31:
                        if (c10 != "")
                            hrefline = "<li><a href=" + DoubleQuote + "mailto:" + c10 + DoubleQuote + ">Production Skills Email</a></li>";
                        break;
                    case 32:
                        if (ProductionSkillCheck == "True")
                            hrefline = "</ul>";
                        break;
                    case 33:
                        if (PELCheck == "True")
                            hrefline = "</li>";
                        break;
                    case 34:
                        if (PELCheck == "True")
                            hrefline = "<li>";
                        break;
                    case 35:
                        if (PELCheck == "True")
                            hrefline = "<ul class=" + DoubleQuote + "list-inline" + DoubleQuote + ">";
                        break;
                    case 36:
                        //c11 = PELURL
                        //c12 = PELEmail
                        if (PELCheck == "True")
                            hrefline = "<li>PEL</li>";
                        break;
                    case 37:
                        if (c11 != "")
                            hrefline = "<li><a href=" + DoubleQuote + c11 + DoubleQuote + " target=" + DoubleQuote + "_blank" + DoubleQuote +">PEL URL</a></li>";
                        break;
                    case 38:
                        if (c12 != "")
                            hrefline = "<li><a href=" + DoubleQuote + "mailto:" + c12 + DoubleQuote + ">PEL Email</a></li>";
                        break;
                    case 39:
                        if (PELCheck == "True")
                            hrefline = "</ul>";
                        break;
                    case 40:
                        if (PELCheck == "True")
                            hrefline = "</li>";
                        break;
                    case 41:
                        hrefline = "</ul>";
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