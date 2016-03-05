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
    public partial class CharCardCustomization : System.Web.UI.Page
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

                showgrid();
            }
        }

        protected void btnSaveCharacter_Click(object sender, EventArgs e)
        {
            int iCharID;
            if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
            {
                if (Session["SkillList"] != null)
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
                    {
                        conn.Open();

                        DataTable dtSkills = Session["SkillList"] as DataTable;
                        foreach (DataRow dRow in dtSkills.Rows)
                        {
                            string s = Session["UserID"].ToString();

                            using (SqlCommand CmdUpdate = new SqlCommand("uspInsUpdCHCharacterSkills", conn))
                            {
                                CmdUpdate.CommandType = CommandType.StoredProcedure;
                                CmdUpdate.Parameters.AddWithValue("@CharacterSkillID", dRow["CharacterSkillID"].ToString());

                                bool bTemp;
                                if (bool.TryParse(dRow["CardDisplayDescription"].ToString(), out bTemp))
                                    CmdUpdate.Parameters.AddWithValue("@CardDisplayDescription", bTemp);
                                else
                                    CmdUpdate.Parameters.AddWithValue("@CardDisplayDescription", true);

                                if (bool.TryParse(dRow["CardDisplayIncant"].ToString(), out bTemp))
                                    CmdUpdate.Parameters.AddWithValue("@CardDisplayIncant", bTemp);
                                else
                                    CmdUpdate.Parameters.AddWithValue("@CardDisplayIncant", true);

                                CmdUpdate.Parameters.AddWithValue("@PlayerDescription", dRow["PlayerDescription"].ToString());
                                CmdUpdate.Parameters.AddWithValue("@PlayerIncant", dRow["PlayerIncant"].ToString());
                                CmdUpdate.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserID"].ToString()));

                                try
                                {
                                    CmdUpdate.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    string t = ex.Message;
                                }
                            }
                        }

                        string jsString = "alert('Character " + ddlCharacterSelector.SelectedItem.Text + " has been saved.');";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                "MyApplication",
                                jsString,
                                true);
                    }
                }
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

        protected void gvSkills_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSkills.EditIndex = -1;
            showgrid();
        }

        protected void gvSkills_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSkills.EditIndex = e.NewEditIndex;
            showgrid();
        }

        protected void gvSkills_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                CheckBox cbDisplayDesc = (CheckBox)gvSkills.Rows[e.RowIndex].FindControl("cbDisplayDesc");
                CheckBox cbDisplayIncant = (CheckBox)gvSkills.Rows[e.RowIndex].FindControl("cbDisplayIncant");
                TextBox tbPlayDesc = (TextBox)gvSkills.Rows[e.RowIndex].FindControl("tbPlayDesc");
                TextBox tbPlayIncant = (TextBox)gvSkills.Rows[e.RowIndex].FindControl("tbPlayIncant");
                HiddenField hidSkillID = (HiddenField)gvSkills.Rows[e.RowIndex].FindControl("hidSkillID");

                if (Session["SkillList"] != null)
                {
                    DataTable dtSkills = Session["SkillList"] as DataTable;
                    DataView dvRow = new DataView(dtSkills, "CharacterSkillID = " + hidSkillID.Value, "", DataViewRowState.CurrentRows);
                    foreach (DataRowView dRow in dvRow)
                    {
                        if (cbDisplayDesc.Checked)
                            dRow["CardDisplayDescription"] = true;
                        else
                            dRow["CardDisplayDescription"] = false;
                        if (cbDisplayIncant.Checked)
                            dRow["CardDisplayIncant"] = true;
                        else
                            dRow["CardDisplayIncant"] = false;
                        dRow["PlayerDescription"] = tbPlayDesc.Text;
                        dRow["PlayerIncant"] = tbPlayIncant.Text;
                    }
                    Session["SkillList"] = dtSkills;
                }
            }
            catch (Exception ex)
            {
                string l = ex.Message;
            }

            gvSkills.EditIndex = -1;
            showgrid();
        }

        protected void gvSkills_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dRow = e.Row.DataItem as DataRowView;

                    string t = dRow["CardDisplayIncant"].ToString();
                    //if (dRow["DisplayDesc"].ToString().ToUpper().StartsWith("D"))
                    //{
                    //    CheckBox cbDisplayDesc = (CheckBox)e.Row.FindControl("cbDisplayDesc");
                    //    if (cbDisplayDesc != null)
                    //        cbDisplayDesc.Checked = true;
                    //}
                    //if (dRow["DisplayIncant"].ToString().ToUpper().StartsWith("D"))
                    //{
                    //    CheckBox cbDisplayIncant = (CheckBox)e.Row.FindControl("cbDisplayIncant");
                    //    if (cbDisplayIncant != null)
                    //        cbDisplayIncant.Checked = true;
                    //}
                }
            }
        }

        private void showgrid()
        {
            if (Session["SkillList"] == null)
            {
                int iCharID;
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    Classes.cCharacter cChar = new Classes.cCharacter();
                    cChar.LoadCharacter(iCharID);

                    DataTable dtCharactersForCampaign = new DataTable();
                    SortedList sParam = new SortedList();
                    sParam.Add("@CharacterID", iCharID);
                    dtCharactersForCampaign = Classes.cUtilities.LoadDataTable("uspGetCharacterSkillSet", sParam, "LARPortal", Session["LoginName"].ToString(), "");

                    bool bTemp = false;

                    foreach (DataRow dRow in dtCharactersForCampaign.Rows)
                    {
                        if (!bool.TryParse(dRow["CardDisplayDescription"].ToString(), out bTemp))
                            dRow["CardDisplayDescription"] = true;
                        if (!bool.TryParse(dRow["CardDisplayIncant"].ToString(), out bTemp))
                            dRow["CardDisplayIncant"] = true;
                    }

                    Session["SkillList"] = dtCharactersForCampaign;
                }
            }
            DataTable dtSkills = Session["SkillList"] as DataTable;

            DataView dvSkills = new DataView(dtSkills, "", "DisplayOrder", DataViewRowState.CurrentRows);
            gvSkills.DataSource = dvSkills;
            gvSkills.DataBind();
        }
    }
}