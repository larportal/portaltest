using System;
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

namespace LarpPortal.Character
{
    public partial class CharCardOrder : System.Web.UI.Page
    {
        private DataTable _dtPlaces = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("SkillList");
                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", "CharacterMaster.Page_Load");
                ddlCharacterSelector.DataTextField = "CharacterAKA";
                ddlCharacterSelector.DataValueField = "CharacterID";
                ddlCharacterSelector.DataSource = dtCharacters;
                ddlCharacterSelector.DataBind();

                if (ddlCharacterSelector.Items.Count > 0)
                {
                    ddlCharacterSelector.ClearSelection();

                    if (Session["SelectedCharacter"] != null)
                    {
                        string sSelectedCharacter = Session["SelectedCharacter"].ToString();
                        DataRow[] drValue = dtCharacters.Select("CharacterID = " + Session["SelectedCharacter"].ToString());
                        foreach (DataRow dRow in drValue)
                        {
                            DateTime DateChanged;
                            if (DateTime.TryParse(dRow["DateChanged"].ToString(), out DateChanged))
                                lblUpdateDate.Text = DateChanged.ToShortDateString();
                            else
                                lblUpdateDate.Text = "Unknown";
                            lblCampaign.Text = dRow["CampaignName"].ToString();
                        }
                        string sCurrentUser = Session["SelectedCharacter"].ToString();
                        foreach (ListItem liAvailableUser in ddlCharacterSelector.Items)
                        {
                            if (sCurrentUser == liAvailableUser.Value)
                                liAvailableUser.Selected = true;
                            else
                                liAvailableUser.Selected = false;
                        }
                    }
                    else
                    {
                        ddlCharacterSelector.Items[0].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    }

                    if (ddlCharacterSelector.SelectedIndex == 0)
                    {
                        ddlCharacterSelector.Items[0].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    }
                    ddlCharacterSelector.Items.Add(new ListItem("Add a new character", "-1"));
                }
                else
                    Response.Redirect("CharAdd.aspx");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                ShowGrid();
            }
        }

        protected void btnSaveCharacter_Click(object sender, EventArgs e)
        {
            int iCharID;
            if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
            {
                foreach (GridViewRow dRow in gvSkills.Rows)
                {
                    HiddenField hidSkillID = (HiddenField)dRow.FindControl("hidSkillID");
                    TextBox tbSortOrder = (TextBox)dRow.FindControl("tbSortOrder");
                    if ((hidSkillID != null) &&
                         (tbSortOrder != null))
                    {
                        SortedList sParams = new SortedList();
                        sParams.Add("@CharacterSkillID", hidSkillID.Value);
                        sParams.Add("@SortOrder", tbSortOrder.Text);
                        Classes.cUtilities.PerformNonQuery("uspInsUpdCHCharacterSkills", sParams, "LARPortal", Session["UserName"].ToString());
                    }

                    //        string jsString = "alert('Character " + ddlCharacterSelector.SelectedItem.Text + " has been saved.');";
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    //                "MyApplication",
                    //                jsString,
                    //                true);
                    //    }
                    //}
                }
                ShowGrid();
            }
        }

        protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCharacterSelector.SelectedValue == "-1")
                Response.Redirect("CharAdd.aspx");

            if (Session["SelectedCharacter"].ToString() != ddlCharacterSelector.SelectedValue)
            {
                Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                Response.Redirect("CharInfo.aspx");
            }
        }

        protected void gvSkills_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dRow = e.Row.DataItem as DataRowView;

                    string t = dRow["CardDisplayIncant"].ToString();
                }
            }
        }

        private void ShowGrid()
        {
            int iCharID;
            if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
            {
                Classes.cCharacter cChar = new Classes.cCharacter();
                cChar.LoadCharacter(iCharID);

                DataTable dtCharactersForCampaign = new DataTable();
                SortedList sParam = new SortedList();
                sParam.Add("@CharacterID", iCharID);
                dtCharactersForCampaign = Classes.cUtilities.LoadDataTable("uspGetCharCardSkills", sParam, "LARPortal", Session["LoginName"].ToString(), "");
                gvSkills.DataSource = dtCharactersForCampaign;
                gvSkills.DataBind();
            }
        }
    }
}