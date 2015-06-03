using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharRelationships : System.Web.UI.Page
    {
        private DataTable _dtPlaces = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["NewRecID"] = 0;
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

                if (Session["SelectedCharacter"] != null)
                {
                    int iCharID;
                    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                    {
                        Classes.cCharacter cChar = new Classes.cCharacter();
                        cChar.LoadCharacter(iCharID);

                        DataTable dtCharactersForCampaign = new DataTable();
                        SortedList sParam = new SortedList();
                        sParam.Add("@CampaignID", cChar.CampaignID);
                        dtCharactersForCampaign = Classes.cUtilities.LoadDataTable("prGetCharactersForCampaign", sParam, "LARPortal", Session["LoginName"].ToString(), "");

                        DataView dvCharactersForCampaign = new DataView(dtCharactersForCampaign, "", "CharacterAKA", DataViewRowState.CurrentRows);
                        gvList.DataSource = dvCharactersForCampaign;
                        gvList.DataBind();

                        sParam = new SortedList();
                        DataTable dtRelations = new DataTable();
                        dtRelations = Classes.cUtilities.LoadDataTable("select * from CHRelationTypes", sParam, "LARPortal", Session["LoginName"].ToString(),
                            lsRoutineName, Classes.cUtilities.LoadDataTableCommandType.Text);
                        DataView dvRelations = new DataView(dtRelations, "", "RelationDescription", DataViewRowState.CurrentRows);
                        ddlRelationship.DataTextField = "RelationDescription";
                        ddlRelationship.DataValueField = "RelationTypeID";
                        ddlRelationship.DataSource = dvRelations;
                        ddlRelationship.DataBind();
                        ddlRelationship.Visible = true;

                        ddlRelationship.Attributes.Add("OnChange", "CheckForOther();");
                        tbOther.Style["display"] = "none";

                        ddlRelationshipNonChar.DataTextField = "RelationDescription";
                        ddlRelationshipNonChar.DataValueField = "RelationTypeID";
                        ddlRelationshipNonChar.DataSource = dvRelations;
                        ddlRelationshipNonChar.DataBind();
                        ddlRelationshipNonChar.Visible = true;

                        ddlRelationshipNonChar.Attributes.Add("OnChange", "CheckForOtherNonChar();");
                        tbOtherNonChar.Style["display"] = "none";

                        ViewState["Rel"] = cChar.Relationships;

                        BindRelat(cChar.Relationships);
                    }
                }
            }
        }

        protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int t = gvList.SelectedIndex;
            string sChar = gvList.SelectedDataKey.Value.ToString();
            sChar = (gvList.SelectedRow.FindControl("lblCharacterAKA") as Label).Text;
            lblCharacter.Text = "Relationship to " + sChar;
            mvAddingRelationship.SetActiveView(vwExistingCharacter);
            ddlRelationship.SelectedIndex = 0;
            tbOther.Text = "";
            tbOther.Style["Display"] = "none";
            tbPlayerComments.Text = "";
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // Hiding the Select Button Cell in Header Row.
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Hiding the Select Button Cells showing for each Data Row. 
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");

                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvList, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected void btnAddNewRelate_Click(object sender, EventArgs e)
        {
            mvAddingRelationship.SetActiveView(vwNewRelate);
            tbOtherNonChar.Text = "";
            tbOtherNonChar.Style["Display"] = "none";
            tbOther.Text = "";
            tbOther.Style["Display"] = "none";
            tbCharacterName.Text = "";
            tbPlayerComments.Text = "";
            tbPlayerCommentsNonChar.Text = "";
        }

        protected void btnSaveNonChar_Click(object sender, EventArgs e)
        {
            int NewRecID = (int)ViewState["NewRecID"];
            NewRecID--;
            ViewState["NewRecID"] = NewRecID;

            List<Classes.cRelationship> Rel = new List<Classes.cRelationship>();

            if (ViewState["Rel"] != null)
                Rel = ViewState["Rel"] as List<Classes.cRelationship>;

            Classes.cRelationship NewRel = new Classes.cRelationship();

            if (hidRelateID.Value != "")
            {
                int iRelID;
                if (!int.TryParse(hidRelateID.Value, out iRelID))
                    return;

                NewRel = Rel.Find(x => x.CharacterRelationshipID == iRelID);
                if (NewRel == null)
                    return;
            }
            else
            {
                NewRel = new Classes.cRelationship();
                NewRel.CharacterRelationshipID = NewRecID;
            }

            NewRel.RelationTypeID = Convert.ToInt32(ddlRelationshipNonChar.SelectedValue);
            NewRel.RelationCharacterID = -1;

            if (ddlRelationshipNonChar.SelectedItem.Text.ToUpper() == "OTHER")
            {
                NewRel.RelationDescription = tbOtherNonChar.Text;
                NewRel.OtherDescription = tbOtherNonChar.Text;
                NewRel.RelationTypeID = -1;
            }
            else
                NewRel.RelationDescription = ddlRelationshipNonChar.SelectedItem.Text;

            NewRel.CharacterRelationshipID = NewRecID;
            NewRel.Name = tbCharacterName.Text;
            NewRel.PlayerComments = tbPlayerCommentsNonChar.Text;

            if (hidRelateID.Value == "")
                Rel.Add(NewRel);

            BindRelat(Rel);
            ViewState["Rel"] = Rel;
            hidRelateID.Value = "";
            mvAddingRelationship.SetActiveView(vwNewRelateButton);
        }

        protected void btnCancelAdding_Click(object sender, EventArgs e)
        {
            mvAddingRelationship.SetActiveView(vwNewRelateButton);
        }

        protected void btnSaveExistingRelate_Click(object sender, EventArgs e)
        {
            int NewRecID = (int)ViewState["NewRecID"];
            NewRecID--;
            ViewState["NewRecID"] = NewRecID;

            List<Classes.cRelationship> Rel = new List<Classes.cRelationship>();

            if (ViewState["Rel"] != null)
                Rel = ViewState["Rel"] as List<Classes.cRelationship>;

            Classes.cRelationship NewRel = new Classes.cRelationship();

            if (hidRelateID.Value != "")
            {
                int iRelID;
                if (!int.TryParse(hidRelateID.Value, out iRelID))
                    return;

                NewRel = Rel.Find(x => x.CharacterRelationshipID == iRelID);
                if (NewRel == null)
                    return;
            }
            else
            {
                NewRel = new Classes.cRelationship();
                NewRel.CharacterRelationshipID = NewRecID;
                NewRel.Name = (gvList.SelectedRow.FindControl("lblCharacterAKA") as Label).Text;
                int iRelationCharID;
                if (int.TryParse(gvList.SelectedDataKey.Value.ToString(), out iRelationCharID))
                    NewRel.RelationCharacterID = iRelationCharID;
            }

            NewRel.RelationTypeID = Convert.ToInt32(ddlRelationship.SelectedValue);
            if (ddlRelationship.SelectedItem.Text.ToUpper() == "OTHER")
            {
                NewRel.RelationDescription = tbOther.Text;
                NewRel.OtherDescription = tbOther.Text;
                NewRel.RelationTypeID = -1;
            }
            else
                NewRel.RelationDescription = ddlRelationship.SelectedItem.Text;

            NewRel.PlayerComments = tbPlayerComments.Text;

            if (hidRelateID.Value == "")
                Rel.Add(NewRel);

            BindRelat(Rel);
            ViewState["Rel"] = Rel;
            hidRelateID.Value = "";
            mvAddingRelationship.SetActiveView(vwNewRelateButton);
        }

        private void BindRelat(List<Classes.cRelationship> Rel)
        {
            if (Rel.Count > 0)
            {
                DataTable dtRelat = Classes.cUtilities.CreateDataTable(Rel);
                int DeleteStatus = (int) Enum.Parse(typeof(Classes.RecordStatuses), Classes.RecordStatuses.Delete.ToString());
                DataView dvRelat = new DataView(dtRelat, "RecordStatus <> " + DeleteStatus.ToString(), "", DataViewRowState.CurrentRows);

                gvRelationships.DataSource = dvRelat;
            }
            else
                gvRelationships.DataSource = null;

            gvRelationships.DataBind();
        }

        protected void gvRelationships_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "EDITITEM":
                    {
                        List<Classes.cRelationship> Rel = new List<Classes.cRelationship>();

                        if (ViewState["Rel"] != null)
                            Rel = ViewState["Rel"] as List<Classes.cRelationship>;

                        int iRelID;
                        if (int.TryParse(e.CommandArgument.ToString(), out iRelID))
                        {
                            var CharRel = Rel.Find(x => x.CharacterRelationshipID == iRelID);
                            if (CharRel != null)
                            {
                                hidRelateID.Value = iRelID.ToString();
                                if (CharRel.RelationCharacterID < 0)
                                {
                                    mvAddingRelationship.SetActiveView(vwNewRelate);
                                    tbCharacterName.Text = CharRel.Name;
                                    if (CharRel.RelationTypeID == -1)
                                    {
                                        tbOtherNonChar.Text = CharRel.OtherDescription;
                                        tbOtherNonChar.Style["Display"] = "block";
                                    }
                                    else
                                        tbOtherNonChar.Style["Display"] = "none";

                                    ddlRelationshipNonChar.SelectedIndex = -1;
                                    foreach (ListItem li in ddlRelationshipNonChar.Items)
                                    {
                                        if ((CharRel.RelationTypeID == -1) && (li.Text.ToUpper() == "OTHER"))
                                            li.Selected = true;
                                        else
                                            if (CharRel.RelationTypeID.ToString() == li.Value)
                                                li.Selected = true;
                                    }
                                    lblCharacter.Text = "Relationship to " + CharRel.Name;
                                    tbPlayerCommentsNonChar.Text = CharRel.PlayerComments;
                                }
                                else
                                {
                                    mvAddingRelationship.SetActiveView(vwExistingCharacter);
                                    if (CharRel.RelationTypeID == -1)
                                    {
                                        tbOther.Text = CharRel.OtherDescription;
                                        tbOther.Style["Display"] = "block";
                                    }
                                    else
                                        tbOther.Style["Display"] = "none";

                                    ddlRelationship.SelectedIndex = -1;
                                    foreach (ListItem li in ddlRelationship.Items)
                                    {
                                        if ((CharRel.RelationTypeID == -1) && (li.Text.ToUpper() == "OTHER"))
                                            li.Selected = true;
                                        else
                                            if (CharRel.RelationTypeID.ToString() == li.Value)
                                                li.Selected = true;
                                    }
                                    lblCharacter.Text = "Relationship to " + CharRel.Name;
                                    tbPlayerComments.Text = CharRel.PlayerComments;
                                }
                            }
                        }
                    }
                    break;

                case "DELETEITEM":
                    {
                        int iRelID;
                        if (int.TryParse(e.CommandArgument.ToString(), out iRelID))
                        {
                            List<Classes.cRelationship> Rel = new List<Classes.cRelationship>();

                            if (ViewState["Rel"] != null)
                                Rel = ViewState["Rel"] as List<Classes.cRelationship>;

                            var CharRel = Rel.Find(x => x.CharacterRelationshipID == iRelID);
                            if (CharRel != null)
                            {
                                if (CharRel.CharacterRelationshipID < 0)
                                    Rel.Remove(CharRel);
                                else
                                    CharRel.RecordStatus = Classes.RecordStatuses.Delete;
                                BindRelat(Rel);
                                ViewState["Rel"] = Rel;
                                hidRelateID.Value = "";
                                mvAddingRelationship.SetActiveView(vwNewRelateButton);
                            }
                        }
                    }
                    break;
            }
        }

        protected void btnSaveCharacter_Click(object sender, EventArgs e)
        {
            List<Classes.cRelationship> Rel = new List<Classes.cRelationship>();

            if (ViewState["Rel"] != null)
            {
                Rel = ViewState["Rel"] as List<Classes.cRelationship>;
                int iCharID;
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    Classes.cCharacter cChar = new Classes.cCharacter();
                    cChar.LoadCharacter(iCharID);

                    for (int i = 0; i < Rel.Count; i++)
                    {
                        if (Rel[i].CharacterRelationshipID < 0)
                            Rel[i].CharacterRelationshipID = -1;
                        Rel[i].CharacterID = iCharID;
                    }

                    //foreach (Classes.cRelationship Relat in Rel)
                    //{
                    //    if (Relat.RelationCharacterID < 0)
                    //        Relat.RelationCharacterID = -1;
                    //    Relat.CharacterID = iCharID;
                    //}

                    cChar.Relationships = Rel;
                    cChar.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);
                    string jsString = "alert('Character " + cChar.AKA + " has been saved.');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                            "MyApplication", jsString, true);
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
    }
}