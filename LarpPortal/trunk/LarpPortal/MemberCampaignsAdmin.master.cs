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
    public partial class MemberCampaignsAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Campaigns";
            //BruteForceLeftNav(); //This one brute forces the build of the left nav (more or less)
            //BuildLeftNav(); // This one builds the left nav through code
        }

        private void BruteForceLeftNav()
        {
            string hrefline;
            string DoubleQuote = "\"";
            string ActiveState;
            int liLinesNeeded = 39;
            DataTable LeftNavTable = new DataTable();
            LeftNavTable.Columns.Add("href_li");
            for (int i = 0; i <= liLinesNeeded; i++)
            {
                ActiveState = ">";
                //build on case of i
                switch (i)
                {
                    case 0:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignInfoSetup")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        hrefline = "<li" + ActiveState + "<label class=" + DoubleQuote + "tree-toggle" + DoubleQuote + "><a href=" + DoubleQuote + "><a href=" +
                            DoubleQuote + "PageUnderConstruction.aspx" + DoubleQuote + ">Campaign Info</a></label><ul>";
                        //<li class="active"><label class="tree-toggle"><a href="lblCampaignInfoSetup" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Campaign Info<span class="caret"></span></a></label><ul class="tree nav nav-pills>

                        break;
                    case 1:
                        if (Session["ActiveLeftNav"].ToString() == "SetupInfo")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li class="active"><label class="tree-toggle"><a href="lblCampaignInfoSetup" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Campaign Info<span class="caret"></span></a></label><ul class="tree nav nav-pills>
                        hrefline = "<li class=" + DoubleQuote + "<label class=" + DoubleQuote + "tree-toggle=" + DoubleQuote + "><a href=" + DoubleQuote + "";
                        break;
                    case 2:
                        if (Session["ActiveLeftNav"].ToString() == "SetupPeople")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Set Up: Campaign Info</a></li>
                        hrefline = "";
                        break;
                    case 3:
                        if (Session["ActiveLeftNav"].ToString() == "SetupPeople")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Set Up: Campaign People</a></li>
                        hrefline = "";
                        break;
                    case 4:
                        if (Session["ActiveLeftNav"].ToString() == "SetupRules")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Set Up: Campaign Places</a></li>
                        hrefline = "";
                        break;
                    case 5:
                        if (Session["ActiveLeftNav"].ToString() == "SetupRulesIndex")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Set Up: Campaign Rules</a></li>
                        hrefline = "";
                        break;
                    case 6:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignMessages")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Set Up: Campaign Rules Index</a></li></ul></li>
                        hrefline = "";
                        break;
                    case 7:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignMessagesView")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><label class="tree-toggle"><a href="lblCampaignMessages" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Campaign Messages<span class="caret"></span></a></label><ul class="tree nav nav-pills>
                        hrefline = "";
                        break;
                    case 8:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignMessagesSend")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">View Previous</a></li>
                        hrefline = "";
                        break;
                    case 9:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignMessagesPreviewScheduled")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Send New</a></li>
                        hrefline = "";
                        break;
                    case 10:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCalendar")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Preview Scheduled</a></li></ul></li>
                        hrefline = "";
                        break;
                    case 11:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetup")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><label class="tree-toggle"><a href="lblCampaignEventSetup" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Events<span class="caret"></span></a></label><ul class="tree nav nav-pills>
                        hrefline = "";
                        break;
                    case 12:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetupPlanning")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><label class="tree-toggle"><a href="lblCampaignEventSetup" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Calendar</a></label></li>
                        hrefline = "";
                        break;
                    case 13:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetupDefaults")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Planning</a></li>
                        hrefline = "";
                        break;
                    case 14:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetupMarketing")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Defaults</a></li>
                        hrefline = "";
                        break;
                    case 15:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetupFoodOptions")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Marketing</a></li>
                        hrefline = "";
                        break;
                    case 16:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventHousing")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Food Options</a></li>
                        hrefline = "";
                        break;
                    case 17:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventPayments")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Assign Housing</a></li>

                        hrefline = "";
                        break;
                    case 18:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventCheckIn")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Record Payments</a></li>

                        hrefline = "";
                        break;
                    case 19:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventDonations")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Check-In</a></li>

                        hrefline = "";
                        break;
                    case 20:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventCheckOut")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Accept Donations</a></li>

                        hrefline = "";
                        break;
                    case 21:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacters")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Approve Check-Out</a></li></ul></li>

                        hrefline = "";
                        break;
                    case 22:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersNPCInfo")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><label class="tree-toggle"><a href="lblCampaignCharacters" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Characters<span class="caret"></span></a></label><ul class="tree nav nav-pills>

                        hrefline = "";
                        break;
                    case 23:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersNPCItems")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">NPC Info</a></li>

                        hrefline = "";
                        break;
                    case 24:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersNPCHistory")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">NPC Items</a></li>

                        hrefline = "";
                        break;
                    case 25:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersSetupTraitsAndAttributes")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">NPC History</a></li>

                        hrefline = "";
                        break;
                    case 26:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersSetupSkillHeadersAndTypes")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Traits & Attributes</a></li>

                        hrefline = "";
                        break;
                    case 27:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersSetupSkills")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Skill Headers & Types</a></li>

                        hrefline = "";
                        break;
                    case 28:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignRoles")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Skills</a></li></ul></li>

                        hrefline = "";
                        break;
                    case 29:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignRolesAssign")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><label class="tree-toggle"><a href="lblCampaignRoles" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Roles<span class="caret"></span></a></label><ul class="tree nav nav-pills>

                        hrefline = "";
                        break;
                    case 30:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignRolesSetup")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Assign Roles</a></li>

                        hrefline = "";
                        break;
                    case 31:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPoints")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Roles</a></li></ul></li>

                        hrefline = "";
                        break;
                    case 32:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPointsSetupStandard")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><label class="tree-toggle"><a href="lblCampaignCharacterBuildPoints" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Character Build Points<span class="caret"></span></a></label><ul class="tree nav nav-pills>

                        hrefline = "";
                        break;
                    case 33:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPointsSetupOther")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Standard Points</a></li>

                        hrefline = "";
                        break;
                    case 34:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPointsAssign")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Setup Other Points</a></li>

                        hrefline = "";
                        break;
                    case 35:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPointsAccept")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        //<li><a href="PageUnderConstruction.aspx" data-toggle="pill">Assign Points</a></li>

                        hrefline = "";
                        break;
                    case 36:
                        if (Session["ActiveLeftNav"].ToString() == "PELSetup")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }

                        // <li><a href="PageUnderConstruction.aspx" data-toggle="pill">Accept Points</a></li></ul></li>

                        hrefline = "";
                        break;
                    case 37:
                        if (Session["ActiveLeftNav"].ToString() == "InventoryProps")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        // <li><label class="tree-toggle"><a href="lblInventoryProps" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Inventory/Props<span class="caret"></span></a></label></li>
                        hrefline = "";
                        break;
                    case 38:
                        if (Session["ActiveLeftNav"].ToString() == "SiteLocationUse")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        //<li><label class="tree-toggle"><a href="lblSiteLocationUse" data-toggle="pill"><a href="PageUnderConstruction.aspx" data-toggle="pill">Site Location Use Setup<span class="caret"></span></a></label></li>
                        hrefline = "";
                        break;

                        //hrefline = Text1 + ActiveState + TreeToggle + Text2 + PageName + Text3 + LineText + SpanClass + Text4 + LineEnd;
                        // Next three lines should be uncommented?  They're currently unreachable so commented out to get rid of the warning
                        //DataRow LeftNavRow = LeftNavTable.NewRow();
                        //LeftNavRow["href_li"] = hrefline;
                        //LeftNavTable.Rows.Add(LeftNavRow);
                        
                        // TODO-Rick-2 Uncomment the next two lines when running the section and creating the nav by code
                        //menu_ul_membercampaignsadmin.DataSource = LeftNavTable;
                        //menu_ul_membercampaignsadmin.DataBind();
                }
            }
        }

        private void BuildLeftNav()
        {
            // Load My Campaigns selection
            string hrefline;
            // Constant values
            string DoubleQuote = "\"";
            string Text1 = "<li";
            string Text2 = "<a href=" + DoubleQuote;
            string Text3 = DoubleQuote + " data-toggle=" + DoubleQuote + "pill" + DoubleQuote + ">";
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
            int liLinesNeeded = 38; // REPLACE WITH NUMBER OF MENU ITEMS NEEDED (base 0 base) - Controls left nav behavior
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
            LineEnd1 = "</label><ul class=" + DoubleQuote + "tree nav nav-pills>";
            LineEnd2 = "</li>";
            LineEnd3 = "</li></ul></li>";
            LineEnd4 = "</label></li>";
            for (int i = 0; i <= liLinesNeeded; i++)
            {
                ActiveState = ">";
                //build on case of i
                switch (i)
                {
                    case 0:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignInfoSetup")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "lblCampaignInfoSetup" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd1;
                        //PageName = "CampaignSetupInfo.aspx";
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Campaign Info";
                        break;
                    case 1:
                        if (Session["ActiveLeftNav"].ToString() == "SetupInfo")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign Info";
                        break;
                    case 2:
                        if (Session["ActiveLeftNav"].ToString() == "SetupPeople")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign People";
                        break;

                    case 3:
                        if (Session["ActiveLeftNav"].ToString() == "SetupPeople")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign Places";
                        break;
                    case 4:
                        if (Session["ActiveLeftNav"].ToString() == "SetupRules")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign Rules";
                        break;
                    case 5:
                        if (Session["ActiveLeftNav"].ToString() == "SetupRulesIndex")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Set Up: Campaign Rules Index";
                        break;
                    case 6:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignMessages")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "lblCampaignMessages" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd1;
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Campaign Messages";
                        break;
                    case 7:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignMessagesView")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "View Previous";
                        break;
                    case 8:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignMessagesSend")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Send New";
                        break;
                    case 9:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignMessagesPreviewScheduled")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Preview Scheduled";
                        break;
                    case 10:
                        //This one is just a link?
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCalendar")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "lblCampaignEventSetup" + Toggle1b;
                        SpanClass = SC2;
                        LineEnd = LineEnd4;
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Calendar";
                        break;
                    case 11:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetup")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "lblCampaignEventSetup" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd1;
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Events";
                        break;
                    case 12:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetupPlanning")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Planning";
                        break;
                    case 13:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetupDefaults")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Defaults";
                        break;

                    case 14:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetupMarketing")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Marketing";
                        break;
                    case 15:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventSetupFoodOptions")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Food Options";
                        break;
                    case 16:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventHousing")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Assign Housing";
                        break;
                    case 17:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventPayments")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Record Payments";
                        break;
                    case 18:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventCheckIn")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Check-In";
                        break;
                    case 19:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventDonations")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Accept Donations";
                        break;
                    case 20:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignEventCheckOut")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Approve Check-Out";
                        LineEnd = LineEnd3;
                        break;
                    case 21:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacters")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "lblCampaignCharacters" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd1;
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Characters";
                        break;
                    case 22:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersNPCInfo")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "NPC Info";
                        break;
                    case 23:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersNPCItems")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "NPC Items";
                        break;
                    case 24:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersNPCHistory")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "NPC History";
                        break;
                    case 25:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersSetupTraitsAndAttributes")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Traits & Attributes";
                        break;
                    case 26:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersSetupSkillHeadersAndTypes")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Skill Headers & Types";
                        break;
                    case 27:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharactersSetupSkills")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Skills";
                        LineEnd = LineEnd3;
                        break;
                    case 28:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignRoles")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "lblCampaignRoles" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd1;
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Roles";
                        break;
                    case 29:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignRolesAssign")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Assign Roles";
                        break;
                    case 30:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignRolesSetup")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Roles";
                        LineEnd = LineEnd3;
                        break;
                    case 31:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPoints")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "lblCampaignCharacterBuildPoints" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd1;
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Character Build Points";
                        break;
                    case 32:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPointsSetupStandard")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Standard Points";
                        break;
                    case 33:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPointsSetupOther")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Setup Other Points";
                        break;
                    case 34:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPointsAssign")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Assign Points";
                        break;
                    case 35:
                        if (Session["ActiveLeftNav"].ToString() == "CampaignCharacterBuildPointsAccept")
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
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Accept Points";
                        break;
                    case 36:
                        if (Session["ActiveLeftNav"].ToString() == "PELSetup")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "PELSetup" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd4;
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "PEL";
                        break;
                    case 37:
                        //Just a link?
                        if (Session["ActiveLeftNav"].ToString() == "InventoryProps")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "lblInventoryProps" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd4;
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Inventory/Props";
                        break;
                    case 38:
                        //Just a link?
                        if (Session["ActiveLeftNav"].ToString() == "SiteLocationUse")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        TreeToggle = Toggle1a + "lblSiteLocationUse" + Toggle1b;
                        SpanClass = SC1;
                        LineEnd = LineEnd4;
                        PageName = "PageUnderConstruction.aspx";
                        LineText = "Site Location Use Setup";
                        break;
                }
                hrefline = Text1 + ActiveState + TreeToggle + Text2 + PageName + Text3 + LineText + SpanClass + Text4 + LineEnd;
                DataRow LeftNavRow = LeftNavTable.NewRow();
                LeftNavRow["href_li"] = hrefline;
                LeftNavTable.Rows.Add(LeftNavRow);

                //TODO-Rick-2 Uncomment the next two lines when running live defining the nav by code
                //menu_ul_membercampaignsadmin.DataSource = LeftNavTable;
                //menu_ul_membercampaignsadmin.DataBind();
            }
        }
    }
}