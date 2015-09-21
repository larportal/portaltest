using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.StaffFunctions
{
    public partial class CharHistoryApprovalList : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //if (Session["UpdatePELMessage"] != null)
            //{
            //    string jsString = Session["UpdatePELMessage"].ToString();
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", jsString, true);
            //    Session.Remove("UpdatePELMessage");
            //}
            SortedList sParams = new SortedList();
            DataTable dtCharacterHistory = new DataTable();

            int iSessionID = 0;
            int.TryParse(Session["CampaignID"].ToString(), out iSessionID);
            sParams.Add("@CampaignID", iSessionID);

            dtCharacterHistory = Classes.cUtilities.LoadDataTable("uspGetCampaignCharacters", sParams, "LARPortal", Session["UserName"].ToString(), "CharHistoryApprovalList.Page_PreRender");

            if (dtCharacterHistory.Columns["HistoryStatus"] == null)
                dtCharacterHistory.Columns.Add("HistoryStatus", typeof(string));

            string sSelectedChar = "";
            string sSelectedPlayer = "";

            foreach (DataRow dRow in dtCharacterHistory.Rows)
            {
                //if (dRow["RoleAlignment"].ToString() != "PC")
                //    dRow["CharacterAKA"] = dRow["RoleAlignment"].ToString();

                if (dRow["DateHistoryApproved"] != DBNull.Value)
                    dRow["HistoryStatus"] = "Approved";
                else if (dRow["DateHistorySubmitted"] != DBNull.Value)
                    dRow["HistoryStatus"] = "Submitted";
                else
                    dRow["HistoryStatus"] = "Not Submitted";
            }

            // While creating the filter am also saving the selected values so we can go back and have the drop down list use them.
            string sRowFilter = "(1 = 1)";      // This is so it's easier to build the filter string. Now can always say 'and ....'

            if (ddlCharacterName.SelectedIndex > 0)
            {
                sRowFilter += " and (CharacterAKA = '" + ddlCharacterName.SelectedValue.Replace("'", "''") + "')";
                sSelectedChar = ddlCharacterName.SelectedValue;
            }

            if (ddlPlayerName.SelectedIndex > 0)
            {
                sRowFilter += " and (PlayerName = '" + ddlPlayerName.SelectedValue.Replace("'", "''") + "')";
                sSelectedPlayer = ddlCharacterName.SelectedValue;
            }

            if (ddlHistoryStatus.SelectedIndex > 0)
                sRowFilter+= " and (HistoryStatus = '" + ddlHistoryStatus.SelectedValue + "')";

            DataView dvCharacterHistory = new DataView(dtCharacterHistory, sRowFilter, "CharacterAKA", DataViewRowState.CurrentRows);
            gvCharacterList.DataSource = dvCharacterHistory;
            gvCharacterList.DataBind();

            DataView view = new DataView(dtCharacterHistory, sRowFilter, "CharacterAKA", DataViewRowState.CurrentRows);
            DataTable dtDistinctChars = view.ToTable(true, "CharacterAKA");

            ddlCharacterName.DataSource = dtDistinctChars;
            ddlCharacterName.DataTextField = "CharacterAKA";
            ddlCharacterName.DataValueField = "CharacterAKA";
            ddlCharacterName.DataBind();
            ddlCharacterName.Items.Insert(0, new ListItem("No Filter", ""));
            ddlCharacterName.SelectedIndex = -1;
            if (sSelectedChar != "")
                foreach (ListItem li in ddlCharacterName.Items)
                    if (li.Value == sSelectedChar)
                        li.Selected = true;
                    else
                        li.Selected = false;
            if (ddlCharacterName.SelectedIndex == -1)     // Didn't find what was selected.
                ddlCharacterName.SelectedIndex = 0;

            view = new DataView(dtCharacterHistory, sRowFilter, "PlayerName", DataViewRowState.CurrentRows);
            DataTable dtDistinctPlayers = view.ToTable(true, "PlayerName");

            ddlPlayerName.DataSource = dtDistinctPlayers;
            ddlPlayerName.DataTextField = "PlayerName";
            ddlPlayerName.DataValueField = "PlayerName";
            ddlPlayerName.DataBind();
            ddlPlayerName.Items.Insert(0, new ListItem("No Filter", ""));
            ddlPlayerName.SelectedIndex = -1;
            if (sSelectedPlayer != "")
                foreach (ListItem li in ddlPlayerName.Items)
                    if (li.Value == sSelectedPlayer)
                        li.Selected = true;
                    else
                        li.Selected = false;
            if (ddlPlayerName.SelectedIndex == -1)     // Didn't find what was selected.
                ddlPlayerName.SelectedIndex = 0;

        }

        protected void gvCharacterList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sCharID = e.CommandArgument.ToString();
            Response.Redirect("CharHistoryApproval.aspx?CharID=" + sCharID, false);
        }
    }
}