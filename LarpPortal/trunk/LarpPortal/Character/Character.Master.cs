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

namespace LarpPortal.Character
{
    public partial class CharacterMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Characters";

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
                //else if (PageName.Contains("CHARPOINTS"))
                //    liPoints.Attributes.Add("class", "active");
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

                if (Session["SelectedCharacter"] == null)
                {
                    SortedList slParameters = new SortedList();
                    slParameters.Add("@intUserID", Session["UserID"].ToString());
                    DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                        "LARPortal", "Character", "CharacterMaster.Page_Load");

                    if (dtCharacters.Rows.Count > 0)
                        Session["SelectedCharacter"] = dtCharacters.Rows[0]["CharacterID"].ToString();
                    else if (!PageName.Contains("CHARADD"))
                        Response.Redirect("CharAdd.aspx");
                }
            }
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
