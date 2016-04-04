using LarpPortal.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class MemberCampaigns : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["AlternatePageURL"] = "/CampaignInfo.aspx";     // This is where users will go if they don't have permission to be on a page they're on
            Session["ActiveTopNav"] = "Campaigns";
            string PageName = Path.GetFileName(Request.PhysicalPath).ToUpper();
            if (PageName.Contains("CAMPAIGNINFO"))
                Session["ActiveLeftNav"] = "CampaignInfo";
            else if (PageName.Contains("RULESVIEW"))
                Session["ActiveLeftNav"] = "Rules";
            else if (PageName.Contains("CALENDAR"))
                Session["ActiveLeftNav"] = "Calendar";
            else if (PageName.Contains("EVENT"))
                Session["ActiveLeftNav"] = "Events";
            else if (PageName.Contains("CAMPAIGNCHARACTER"))
                Session["ActiveLeftNav"] = "CampaignCharacter";
            else if (PageName.Contains("ROLES"))
                Session["ActiveLeftNav"] = "Roles";
            else if (PageName.Contains("POINTS"))
                Session["ActiveLeftNav"] = "Points";
            else if (PageName.Contains("PEL"))
                Session["ActiveLeftNav"] = "PEL";
            else if (PageName.Contains("INVENTORY"))
                Session["ActiveLeftNav"] = "Inventory";

            BuildLeftNav(); // This one builds the left nav through code
        }

        private void BuildLeftNav()
        {
            // Session["PlayerRoleString"] formatted as CampaignID:/RoleID/RoleID/RoleID/ where CampaignID should be the currently selected CampaignID and RoleID is all the RoleIDs the player has for that campaign.
            // Campaign and roles can be extracted as substrings
            // For example 33:/7/8/ would be Campaign 33 (Fifth Gate) with Roles 7 (Event NPC) and 8 (PC)

            string ReqPage = "";
            string ActiveNav;
            if (Session["ActiveLeftNav"] != null)
                ActiveNav = Session["ActiveLeftNav"].ToString();
            else
                ActiveNav = "CampaignInfo";  // Set default
            string PlayerRoles = "";
            if (Session["PlayerRoleString"] != null)
                PlayerRoles = Session["PlayerRoleString"].ToString();
            string LastLoggedInLocation = "";
            if (Session["LastLoggedInLocation"] != null)
                LastLoggedInLocation = Session["LastLoggedInLocation"].ToString();
            // Load My Campaigns selection
            string hrefline;
            string hreflinecumulative = "";

            // Constant values
            string DoubleQuote = "\"";
            hreflinecumulative = "<ul class=" + DoubleQuote + "nav nav-pills nav-stacked" + DoubleQuote + ">";
            string Text1 = "<li";
            string Text2 = "<a href=" + DoubleQuote;
            string Text3 = DoubleQuote + " data-toggle=" + DoubleQuote + "pill" + DoubleQuote + ">";

            Text3 = DoubleQuote + ">";

            string Text4 = "/a>";
            // Must be defined in each case
            string ActiveState;
            string TreeToggle;
            string Toggle1a; // Value 1 for TreeToggle for top level choices and choices with no indent
            string Toggle1b; // Value 2 for TreeToggle for top level choices
            string Toggle2; // Value for TreeToggle for all other choices
            string PageName;
            string LineText;
            string SpanClass;
            string SC1; // Value for SpanClass for top level choices
            string SC2; // Value for SpanClass for all other choices
            string LineEnd;
            string LineEnd1;    // Value for LineEnd for top level choices
            string LineEnd2;    // Value for LineEnd for middle level choices
            string LineEnd3;    // Value for LineEnd for bottom level choices
            string LineEnd4;    // Value for choices with no indent
            int liLinesNeeded = 53; // REPLACE WITH NUMBER OF MENU ITEMS NEEDED (base 0 base) - Controls left nav behavior
            bool SkipLine = false;
            DataTable LeftNavTable = new DataTable();
            LeftNavTable.Columns.Add("href_li");
            ActiveState = ">";
            TreeToggle = "";
            Toggle1a = "<label class=" + DoubleQuote + "tree-toggle" + DoubleQuote + "><a href=" + DoubleQuote;
            Toggle1b = DoubleQuote + " data-toggle=" + DoubleQuote + "pill" + DoubleQuote + ">";
            Toggle2 = "";
            PageName = "~/DESTINATIONPAGE.aspx";
            LineText = "";
            SpanClass = "<";
            SC1 = "<span class=" + DoubleQuote + "caret" + DoubleQuote + "></span><";
            SC2 = "<";
            LineEnd = "";
            LineEnd1 = "</li>"; // "</label><ul class=" + DoubleQuote + "tree nav nav-pills>";
            LineEnd2 = "</li>";
            LineEnd3 = "</li>"; // "</li></ul></li>";
            LineEnd4 = "</label></li>";
            for (int i = 0; i <= liLinesNeeded; i++)
            {
                ActiveState = ">";
                //build on case of i
                switch (i)
                {
                    case 0:
                        ReqPage = "/CampaignInfo.aspx";
                        if (PageName == "CAMPAIGNINFO")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle2; // Toggle1a + "lblCampaignInfoSetup" + Toggle1b;
                        if (PlayerRoles.Contains("/5/") || PlayerRoles.Contains("/28/") || PlayerRoles.Contains("/32/"))
                        {
                            SpanClass = SC1;
                        }
                        else
                        {
                            SpanClass = SC2;
                        }
                        LineEnd = LineEnd1;
                        PageName = ReqPage;
                        LineText = "Campaign Info";
                        break;

                    case 1:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignInfo" && PlayerRoles.Contains("/28/"))
                        {
                            if (PageName == "CAMPAIGNINFOSUCAMPAIGNINFO")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Set Up: Campaign Info";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 2:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignInfo" && PlayerRoles.Contains("/32/"))
                        {
                            if (PageName == "CAMPIAGNINFOSUPEOPLE")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Set Up: Campaign People";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }

                        break;

                    case 3:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignInfo" && PlayerRoles.Contains("/32/"))
                        {
                            if (PageName == "CAMPAIGNINFOSUPLACES")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Set Up: Campaign Places";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }

                        break;

                    case 4:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignInfo" && (PlayerRoles.Contains("/32/") || PlayerRoles.Contains("/5/")))
                        {
                            if (PageName == "CAMPAIGNINFOSURULES")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Set Up: Campaign Rules";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 5:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignInfo" && (PlayerRoles.Contains("/32/") || PlayerRoles.Contains("/5/")))
                        {
                            if (PageName == "CAMPAIGNINFOSURULESINDEX")
                            {
                                // ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd3;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Set Up: Campaign Rules Index";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 6:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (PageName == "CAMPAIGNMESSAGES" && (PlayerRoles.Contains("/4/") || PlayerRoles.Contains("/28/")))
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle2; // Toggle1a + "lblCampaignMessages" + Toggle1b;
                        if (PlayerRoles.Contains("/4/") || PlayerRoles.Contains("/28/"))
                        {
                            SpanClass = SC1;
                        }
                        else
                        {
                            SpanClass = SC2;
                        }
                        LineEnd = LineEnd1;
                        PageName = ReqPage;
                        LineText = "Campaign Messages";
                        break;

                    case 7:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignMessages" && PlayerRoles.Contains("/4/"))
                        {
                            if (PageName == "CAMPAIGNMESSAGESVIEWPREV")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;View Previous";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 8:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignMessages" && PlayerRoles.Contains("/28/"))
                        {
                            if (PageName == "CAMPAIGNMESSAGESSENDNEW")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Send New";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 9:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignMessages" && PlayerRoles.Contains("/28/"))
                        {
                            if (PageName == "CAMPAIGNMESSAGESPREVSCHEDULED")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd3;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Preview Scheduled";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    //Missing 10 - Rules
                    //Missing 11 - Rules Section - iterative per section

                    case 12:
                        ReqPage = "/PageUnderConstruction.aspx";
                        //This one is just a link?
                        if (PageName == "CALENDARVIEW")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle2; // Toggle1a + "lblCampaignEventSetup" + Toggle1b;
                        SpanClass = SC2;
                        LineEnd = LineEnd4;
                        PageName = ReqPage;
                        LineText = "Calendar";
                        break;

                    //Missing 13 - Special events
                    //Missing 14 - Scheduling
                    //Missing 15 - Event scheduling

                    case 16:
                        ReqPage = "/Events/EventRegistration.aspx";
                        if (PageName == "EVENTS")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle2; // Toggle1a + "lblCampaignEventSetup" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd1;
                        PageName = ReqPage;
                        LineText = "Event Registrations/RSVP";
                        break;

                    case 17:
                        ReqPage = "/Events/RegistrationApproval.aspx";
                        if (ActiveNav == "Events" && (PlayerRoles.Contains("/3/") || PlayerRoles.Contains("/28/")))
                        {
                            if (PageName == "EVENTS")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2; // Toggle1a + "lblCampaignEventSetup" + Toggle1b;
                            SpanClass = SC1;
                            LineEnd = LineEnd1;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Registration Approval";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    //Missing 18 - Shopping cart / payment

                    case 19:
                        ReqPage = "/events/eventlist";
                        if (ActiveNav == "Events" && (PlayerRoles.Contains("/3/") || PlayerRoles.Contains("/28/")))
                        {
                            if (PageName == "EVENTSSUPLANNING")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Events";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 20:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Events" && (PlayerRoles.Contains("/3/") || PlayerRoles.Contains("/28/")))
                        {
                            if (PageName == "EVENTSSUDEFAULTS")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Defaults";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    //Missing 21 - Setup scheduling

                    case 22:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Events" && (PlayerRoles.Contains("/3/") || PlayerRoles.Contains("/28/")))
                        {
                            if (PageName == "EVENTSSUMARKETING")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Marketing";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 23:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Events" && PlayerRoles.Contains("/33/"))
                        {
                            if (PageName == "EVENTSSUFOODOPTIONS")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Food Options";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 24:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Events" && PlayerRoles.Contains("/11/"))
                        {
                            if (PageName == "EVENTSASSIGNHOUSING")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Assign Housing";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 25:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Events" && PlayerRoles.Contains("/22/"))
                        {
                            if (PageName == "EVENTSRECORDPAYMENTS")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Record Payments";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 26:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Events" && PlayerRoles.Contains("/16/"))
                        {
                            if (PageName == "EVENTSCHECKIN")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Check-In";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 27:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Events" && PlayerRoles.Contains("/34/"))
                        {
                            if (PageName == "EVENTSACCEPTDONATIONS")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Accept Donations";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 28:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Events" && PlayerRoles.Contains("/35/"))
                        {
                            if (PageName == "EVENTSCHECKOUT")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd3;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Approve Check-Out";
                            LineEnd = LineEnd3;
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 29:
                        SkipLine = true;
                        //ReqPage = "/PageUnderConstruction.aspx";
                        //if (PageName == "CAMPAIGNCHARACTERS" && (PlayerRoles.Contains("/4/") || PlayerRoles.Contains("/5/") || PlayerRoles.Contains("/20/")))
                        //{
                        //    ActiveState = " class=\"active\">";
                        //}
                        //else
                        //{
                        //    ActiveState = ">";
                        //}
                        //TreeToggle = Toggle2; // Toggle1a + "lblCampaignCharacters" + Toggle1b;
                        //if (PlayerRoles.Contains("/5/") || PlayerRoles.Contains("/4/") || PlayerRoles.Contains("/20/"))
                        //{
                        //    SpanClass = SC1;
                        //}
                        //else
                        //{
                        //    SpanClass = SC2;
                        //}
                        //LineEnd = LineEnd1;
                        //PageName = ReqPage;
                        //LineText = "Characters";
                        break;

                    case 30:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignCharacter" && (PlayerRoles.Contains("/4/") || PlayerRoles.Contains("/20/")))
                        {
                            if (LastLoggedInLocation == "CAMPIAGNINFOSUPEOPLE")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;NPC Info";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 31:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignCharacter" && (PlayerRoles.Contains("/4/") || PlayerRoles.Contains("/20/")))
                        {
                            if (PageName == "CAMPAIGNCHARACTERITEMS")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;NPC Items";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 32:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignCharacter" && (PlayerRoles.Contains("/4/") || PlayerRoles.Contains("/20/")))
                        {
                            if (PageName == "CAMPAIGNCHARACTERHISTORY")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;&nbsp;NPC History";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    //Missing 33 - NPC Skills

                    case 34:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignCharacter" && PlayerRoles.Contains("/5/"))
                        {
                            if (PageName == "CAMPAIGNCHARACTERSUTRAITS")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Traits & Attributes";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 35:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignCharacter" && PlayerRoles.Contains("/5/"))
                        {
                            if (PageName == "CAMPAIGNCHARACTERSUSKILLTYPES")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Skill Headers & Types";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 36:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "CampaignCharacter" && PlayerRoles.Contains("/5/"))
                        {
                            if (PageName == "CAMPAIGNCHARACTERSUSKILLS")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd3;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Skills";
                            LineEnd = LineEnd3;
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 37:
                        ReqPage = "/PageUnderConstruction.aspx"; // = "/Roles/Roles.aspx";
                        if (PageName == "ROLES")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle2; // Toggle1a + "lblCampaignRoles" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd1;
                        PageName = ReqPage;
                        LineText = "Roles";
                        break;

                    //Missing 38 - My roles

                    case 39:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Roles" && PlayerRoles.Contains("/21/"))
                        {
                            if (PageName == "ROLESASSIGNROLES")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Assign Roles";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 40:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Roles" && PlayerRoles.Contains("/1/"))
                        {
                            if (PageName == "ROLESSUROLES")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd3;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Roles";
                            LineEnd = LineEnd3;
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 41:
                        ReqPage = "/Points/PointsAssign.aspx";
                        PageName = "POINTS";
                        //if (ActiveNav == "Points" && PlayerRoles.Contains("/28/"))
                        if (PlayerRoles.Contains("/28/"))
                        {
                            if (PageName == "POINTS") // && (PlayerRoles.Contains("/34/") || PlayerRoles.Contains("/28/") || PlayerRoles.Contains("/35/")))
                            {
                                if ((PlayerRoles.Contains("/34/") || PlayerRoles.Contains("/28/") || PlayerRoles.Contains("/35/")))
                                {
                                    ActiveState = " class=\"active\">";
                                    PageName = ReqPage;
                                }
                                else
                                {
                                    ActiveState = ">";
                                    PageName = "/CampaignInfo.aspx";
                                }

                                TreeToggle = Toggle2; // Toggle1a + "lblCampaignCharacterBuildPoints" + Toggle1b;
                                SpanClass = SC1;
                                LineEnd = LineEnd1;
                                LineText = "Character Build Points";
                            }
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                        //
                        //ReqPage = "/PELs/PELApprovalList";
                        //if (ActiveNav == "PEL" && (PlayerRoles.Contains("/28/") || PlayerRoles.Contains("/4/")))
                        //{
                        //    if (PageName == "PELApprovalList" && (PlayerRoles.Contains("/28/") || PlayerRoles.Contains("/4/")))
                        //    {
                        //        ActiveState = " class=\"active\">";
                        //    }
                        //    else
                        //    {
                        //        ActiveState = ">";
                        //    }
                        //    TreeToggle = Toggle2; // Toggle1a + "PELSetup" + Toggle1b;
                        //    SpanClass = SC1;
                        //    LineEnd = LineEnd4;
                        //    PageName = ReqPage;
                        //    LineText = "&nbsp;&nbsp;&nbsp;PEL Approval List";
                        //}
                        //else
                        //{
                        //    if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                        //        Session["CurrentPagePermission"] = "False";
                        //    SkipLine = true;
                        //}
                        //break;
                        //

                    case 42:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Points" && PlayerRoles.Contains("/28/"))
                        {
                            if (PageName == "POINTSSUSTANDARDS")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Standard Points";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    //Missing 43 - Modify game system points

                    case 44:
                        ReqPage = "/PageUnderConstruction.aspx";    // = "/Points/PointsSetupNonStandard.aspx";
                        if (ActiveNav == "Points" && PlayerRoles.Contains("/34/"))
                        {
                            if (PageName == "POINTSSUOTHER")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Setup Other Points";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 45:
                        ReqPage = "/PageUnderConstruction.aspx";   // = "/Points/PointsAssign.aspx";
                        if (ActiveNav == "Points" && (PlayerRoles.Contains("/35/") || PlayerRoles.Contains("/28/")))
                        {
                            if (PageName == "POINTSASSIGN")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd2;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Assign Points";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 46:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "Points" && PlayerRoles.Contains("/28/"))
                        {
                            if (PageName == "POINTSACCEPT")
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2;
                            SpanClass = SC2;
                            LineEnd = LineEnd3;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;Accept Points";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 47:
                        ReqPage = "/PELs/PELList.aspx";
                        if (PageName == "PEL")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle2; // Toggle1a + "lblCampaignRoles" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd1;
                        PageName = ReqPage;
                        LineText = "PELs";
                        break;

                    case 48:
                        ReqPage = "/PageUnderConstruction.aspx";
                        if (ActiveNav == "PEL" && PlayerRoles.Contains("/28/"))
                        {
                            if (PageName == "PELSU" && PlayerRoles.Contains("/28/"))
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2; // Toggle1a + "PELSetup" + Toggle1b;
                            SpanClass = SC1;
                            LineEnd = LineEnd4;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;PEL Setup";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    case 49:
                        ReqPage = "/PELs/PELApprovalList";
                        if (ActiveNav == "PEL" && (PlayerRoles.Contains("/28/") || PlayerRoles.Contains("/4/")))
                        {
                            if (PageName == "PELApprovalList" && (PlayerRoles.Contains("/28/") || PlayerRoles.Contains("/4/")))
                            {
                                ActiveState = " class=\"active\">";
                            }
                            else
                            {
                                ActiveState = ">";
                            }
                            TreeToggle = Toggle2; // Toggle1a + "PELSetup" + Toggle1b;
                            SpanClass = SC1;
                            LineEnd = LineEnd4;
                            PageName = ReqPage;
                            LineText = "&nbsp;&nbsp;&nbsp;PEL Approval List";
                        }
                        else
                        {
                            if (LastLoggedInLocation == ReqPage && ReqPage != "/PageUnderConstruction.aspx")
                                Session["CurrentPagePermission"] = "False";
                            SkipLine = true;
                        }
                        break;

                    //case 50:
                        //if (PageName == "PELApprove" && PlayerRoles.Contains("/28/"))
                        //{
                        //    ActiveState = " class=\"active\">";
                        //}
                        //else
                        //{
                        //    ActiveState = ">";
                        //}
                        //TreeToggle = Toggle2; // Toggle1a + "PELSetup" + Toggle1b;
                        //SpanClass = SC1;
                        //LineEnd = LineEnd4;
                        //PageName = "/PageUnderConstruction.aspx";
                        //PageName = "/PELs/PELApprove";
                        //LineText = "&nbsp;&nbsp;&nbsp;PEL Approval";
                        //break;

                    //case 51:
                        //if (PageName == "PELEdit" && PlayerRoles.Contains("/28/"))
                        //{
                        //    ActiveState = " class=\"active\">";
                        //}
                        //else
                        //{
                        //    ActiveState = ">";
                        //}
                        //TreeToggle = Toggle2; // Toggle1a + "PELSetup" + Toggle1b;
                        //SpanClass = SC1;
                        //LineEnd = LineEnd4;
                        //PageName = "/PageUnderConstruction.aspx";
                        //PageName = "/PELs/PELEdit";
                        //LineText = "&nbsp;&nbsp;&nbsp;PEL Edit";
                        //break;

                    case 52:
                        ReqPage = "/PageUnderConstruction.aspx";
                        //Just a link?
                        if (PageName == "INVENTORY" && PlayerRoles.Contains("/36/"))
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle2; // Toggle1a + "lblInventoryProps" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd4;
                        PageName = ReqPage;
                        LineText = "Inventory/Props";
                        break;

                    //case 38:
                    //    //Just a link?
                    //    if (Session["ActiveLeftNav"].ToString() == "SiteLocationUse")
                    //    {
                    //        ActiveState = " class=\"active\">";
                    //    }
                    //    else
                    //    {
                    //        ActiveState = ">";
                    //    }
                    //    TreeToggle = Toggle1a + "lblSiteLocationUse" + Toggle1b;
                    //    SpanClass = SC1;
                    //    LineEnd = LineEnd4;
                    //    PageName = "/PageUnderConstruction.aspx";
                    //    LineText = "Site Location Use Setup";
                    //    break;
                    default:
                        SkipLine = true;
                        break;
                }

                if (SkipLine == false)
                {
                    hrefline = Text1 + ActiveState + TreeToggle + Text2 + PageName + Text3 + LineText + SpanClass + Text4 + LineEnd;
                    DataRow LeftNavRow = LeftNavTable.NewRow();
                    LeftNavRow["href_li"] = hrefline;
                    LeftNavTable.Rows.Add(LeftNavRow);
                    hreflinecumulative = hreflinecumulative + hrefline;
                 lblLeftNav.Text = hreflinecumulative;
                }
                SkipLine = false;
                //TODO-Rick-2 Uncomment the next two lines when running live when defining the nav by code
                //menu_ul_membercampaignsadmin.DataSource = LeftNavTable;
                //menu_ul_membercampaignsadmin.DataBind();
            }
            lblLeftNav.Text += "</ul>";
        }

    }
}