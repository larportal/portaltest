using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharUserDefined : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["CurrentCharacter"] == null)
                ViewState["CurrentCharacter"] = "";

            if (!IsPostBack)
            {
                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", "CharacterMaster.Page_Load");
                ddlCharacterSelector.DataTextField = "CharacterAKA";
                ddlCharacterSelector.DataValueField = "CharacterID";
                ddlCharacterSelector.DataSource = dtCharacters;
                ddlCharacterSelector.DataBind();

                divUserDef1.Visible = false;
                divUserDef2.Visible = false;
                divUserDef3.Visible = false;
                divUserDef4.Visible = false;
                divUserDef5.Visible = false;
                
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
                            bool UseValue = false;
                            lblCampaign.Text = dRow["CampaignName"].ToString();
                            if (Boolean.TryParse(dRow["UseUserDefinedField1"].ToString(), out UseValue))
                                if (UseValue)
                                {
                                    divUserDef1.Visible = true;
                                    lblUserDef1.Text = dRow["UserDefinedField1"].ToString();
                                }

                            if (Boolean.TryParse(dRow["UseUserDefinedField2"].ToString(), out UseValue))
                                if (UseValue)
                                {
                                    divUserDef2.Visible = true;
                                    lblUserDef2.Text = dRow["UserDefinedField2"].ToString();
                                }

                            if (Boolean.TryParse(dRow["UseUserDefinedField3"].ToString(), out UseValue))
                                if (UseValue)
                                {
                                    divUserDef3.Visible = true;
                                    lblUserDef3.Text = dRow["UserDefinedField3"].ToString();
                                }

                            if (Boolean.TryParse(dRow["UseUserDefinedField4"].ToString(), out UseValue))
                                if (UseValue)
                                {
                                    divUserDef4.Visible = true;
                                    lblUserDef4.Text = dRow["UserDefinedField4"].ToString();
                                }

                            if (Boolean.TryParse(dRow["UseUserDefinedField5"].ToString(), out UseValue))
                                if (UseValue)
                                {
                                    divUserDef5.Visible = true;
                                    lblUserDef5.Text = dRow["UserDefinedField5"].ToString();
                                }
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
            SortedList sParam = new SortedList();
            sParam.Add("@CharacterID", Session["SelectedCharacter"].ToString());

            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            DataTable dtUserDefined = Classes.cUtilities.LoadDataTable("select * from CHCharacterUserDefined where CharacterID = @CharacterID", sParam, 
                "LARPortal", Session["UserName"].ToString(), lsRoutineName, Classes.cUtilities.LoadDataTableCommandType.Text);

            foreach (DataRow dRow in dtUserDefined.Rows)
            {
                tbUserField1.Text = dRow["UserDefinedField1"].ToString();
                tbUserField2.Text = dRow["UserDefinedField2"].ToString();
                tbUserField3.Text = dRow["UserDefinedField3"].ToString();
                tbUserField4.Text = dRow["UserDefinedField4"].ToString();
                tbUserField5.Text = dRow["UserDefinedField5"].ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Classes.cCharacter cChar = new Classes.cCharacter();

            int iCharNum;

            if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharNum))
            {
                cChar.LoadCharacter(iCharNum);
                cChar.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);

                SortedList sParam = new SortedList();
                sParam.Add("@CharacterID", Session["SelectedCharacter"].ToString());
                sParam.Add("@UserDefinedField1", tbUserField1.Text.Trim());
                sParam.Add("@UserDefinedField2", tbUserField2.Text.Trim());
                sParam.Add("@UserDefinedField3", tbUserField3.Text.Trim());
                sParam.Add("@UserDefinedField4", tbUserField4.Text.Trim());
                sParam.Add("@UserDefinedField5", tbUserField5.Text.Trim());

                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                DataTable dtUserDefined = Classes.cUtilities.LoadDataTable("uspInsUpdCHCharacterUserDefined", sParam, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

                string jsString = "alert('Character " + cChar.AKA + " has been saved.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
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
