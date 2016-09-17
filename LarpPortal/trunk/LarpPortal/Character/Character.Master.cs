using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Character
{
    public partial class CharacterMaster : System.Web.UI.MasterPage
    {
        LogWriter oLog = new LogWriter();
        string _UserName = "";
        int _UserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
            if (_UserID == 0)
                _UserID = -1;

            Session["ActiveTopNav"] = "Characters";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            oLog.AddLogMessage("Starting Character Master", "Character.Master.Page_Load", "", Session.SessionID);

            string PageName = Path.GetFileName(Request.PhysicalPath).ToUpper();
            PageName = Request.Url.AbsoluteUri.ToUpper();

            if (!IsPostBack)
            {
                if (PageName.Contains("CHARINFO"))
                    liInfo.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARITEMS"))
                    liItems.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARHISTORY"))
                    liHistory.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARSKILLS"))
                    liSkills.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARREQUESTS"))
                    liRequests.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARRELATIONSHI"))
                    liRelationShips.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARPLACES"))
                    liPlaces.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARREQUESTS"))
                    liPlaces.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARUSERDEFINED"))
                    liGoals.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARADD"))
                    liAddNewChar.Attributes.Add("class", "active");
                else if (PageName.Contains("CARDCUSTOM"))
                    liCardCust.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARCARDORDER"))
                    liCharCharOrder.Attributes.Add("class", "active");
                else if (PageName.Contains("/HISTORY/EDIT"))
                    liHistory.Attributes.Add("class", "active");
                else if (PageName.Contains("TEAMS/CREATETEAM"))
                    liCreateTeam.Attributes.Add("class", "active");
                else if (PageName.Contains("JOINTEAM"))
                    liJoinTeam.Attributes.Add("class", "active");
                else if (PageName.Contains("TEAMS/MANAGETEAM"))
                    liManageTeam.Attributes.Add("class", "active");

                if (Session["SelectedCharacter"] == null)
                {
                    SortedList slParameters = new SortedList();
                    slParameters.Add("@intUserID", Session["UserID"].ToString());
                    oLog.AddLogMessage("About to load the character IDs", "Characters.Master.Page_Load", "", Session.SessionID);
                    DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                        "LARPortal", "Character", "CharacterMaster.Page_Load");

                    if (dtCharacters.Rows.Count > 0)
                        Session["SelectedCharacter"] = dtCharacters.Rows[0]["CharacterID"].ToString();
                    else if (!PageName.Contains("CHARADD"))
                        Response.Redirect("/Character/CharAdd.aspx");
                    oLog.AddLogMessage("Done loading the character IDs", "Characters.Master.Page_Load", "", Session.SessionID);
                }
            }
            if (PageName.Contains("/TEAMS/"))
            {
                if (Session["SelectedCharacter"] != null)
                {
                    int iCharID = 0;
                    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                    {
                        SortedList slParameters = new SortedList();
                        slParameters.Add("@CharacterID", iCharID.ToString());
                        DataTable dtTeams = cUtilities.LoadDataTable("uspGetTeamsForCampaignAndCharacterInd", slParameters, "LARPortal", _UserName, lsRoutineName + ".CampaignAndCharacterInd");

                        DataView dv = new DataView(dtTeams, "Approval = 1", "", DataViewRowState.CurrentRows);
                        if (dv.Count > 0)
                            liManageTeam.Visible = true;
                        else
                            liManageTeam.Visible = false;
                    }
                }
                ulTeams.Visible = true;
            }
            else
                ulTeams.Visible = false;

            oLog.AddLogMessage("Done Character Master", "Characters.Master.Page_Load", "", Session.SessionID);
        }
    }
}
