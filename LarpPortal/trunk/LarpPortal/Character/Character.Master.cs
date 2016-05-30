using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Character
{
    public partial class CharacterMaster : System.Web.UI.MasterPage
    {
        LogWriter oLog = new LogWriter();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Characters";

            oLog.AddLogMessage("Starting Character Master", "Character.Master.Page_Load", "", Session.SessionID);

            if (!IsPostBack)
            {
                string PageName = Path.GetFileName(Request.PhysicalPath).ToUpper();
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
                        Response.Redirect("CharAdd.aspx");
                    oLog.AddLogMessage("Done loading the character IDs", "Characters.Master.Page_Load", "", Session.SessionID);
                }
            }
            oLog.AddLogMessage("Done Character Master", "Characters.Master.Page_Load", "", Session.SessionID);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        //protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCharacterSelector.SelectedValue == "-1")
        //        Response.Redirect("CharAdd.aspx");

        //    if (Session["SelectedCharacter"].ToString() != ddlCharacterSelector.SelectedValue)
        //    {
        //        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
        //        Response.Redirect("CharInfo.aspx");
        //    }
        //}
    }
}
