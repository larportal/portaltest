﻿using System;
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
        private string _UserName = "";
        private int _UserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["CurrentCharacter"] == null)
                ViewState["CurrentCharacter"] = "";

            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
            oCharSelect.CharacterChanged += oCharSelect_CharacterChanged;

            if (!IsPostBack)
            {
                divUserDef1.Visible = false;
                divUserDef2.Visible = false;
                divUserDef3.Visible = false;
                divUserDef4.Visible = false;
                divUserDef5.Visible = false;
            }
        }
        


        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            divUserDef1.Visible = false;
            divUserDef2.Visible = false;
            divUserDef3.Visible = false;
            divUserDef4.Visible = false;
            divUserDef5.Visible = false;

            oCharSelect.LoadInfo();

            if (oCharSelect.CharacterID.HasValue)
            {
                SortedList sParam = new SortedList();
                sParam.Add("@CharacterID", oCharSelect.CharacterID.Value);
                DataTable dtUserDef = Classes.cUtilities.LoadDataTable("uspGetCharacterUserDef", sParam, "LARPortal", _UserName, lsRoutineName);
                foreach (DataRow dRow in dtUserDef.Rows)
                {
                    bool UseValue = false;
                    if (Boolean.TryParse(dRow["UseUserDefinedField1"].ToString(), out UseValue))
                        if (UseValue)
                        {
                            divUserDef1.Visible = true;
                            lblUserDef1.Text = dRow["UserDefinedField1"].ToString();
                            tbUserField1.Text = dRow["Field1Value"].ToString();
                            lblUserField1.Text = dRow["Field1Value"].ToString();
                        }

                    if (Boolean.TryParse(dRow["UseUserDefinedField2"].ToString(), out UseValue))
                        if (UseValue)
                        {
                            divUserDef2.Visible = true;
                            lblUserDef2.Text = dRow["UserDefinedField2"].ToString();
                            tbUserField2.Text = dRow["Field2Value"].ToString();
                            lblUserField2.Text = dRow["Field2Value"].ToString();
                        }

                    if (Boolean.TryParse(dRow["UseUserDefinedField3"].ToString(), out UseValue))
                        if (UseValue)
                        {
                            divUserDef3.Visible = true;
                            lblUserDef3.Text = dRow["UserDefinedField3"].ToString();
                            tbUserField3.Text = dRow["Field3Value"].ToString();
                            lblUserField3.Text = dRow["Field3Value"].ToString();
                        }

                    if (Boolean.TryParse(dRow["UseUserDefinedField4"].ToString(), out UseValue))
                        if (UseValue)
                        {
                            divUserDef4.Visible = true;
                            lblUserDef4.Text = dRow["UserDefinedField4"].ToString();
                            tbUserField4.Text = dRow["Field4Value"].ToString();
                            lblUserField4.Text = dRow["Field4Value"].ToString();
                        }

                    if (Boolean.TryParse(dRow["UseUserDefinedField5"].ToString(), out UseValue))
                        if (UseValue)
                        {
                            divUserDef5.Visible = true;
                            lblUserDef5.Text = dRow["UserDefinedField5"].ToString();
                            tbUserField5.Text = dRow["Field5Value"].ToString();
                            lblUserField5.Text = dRow["Field5Value"].ToString();
                        }

                    if ((oCharSelect.CharacterInfo.CharacterType != 1) && (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters))
                    {
                        btnSave.Enabled = false;
                        btnSave.CssClass = "btn-default";
                        btnSave.Style["background-color"] = "grey";
                        btnSaveTop.Enabled = false;
                        btnSaveTop.CssClass = "btn-default";
                        btnSaveTop.Style["background-color"] = "grey";

                        tbUserField1.Visible = false;
                        lblUserField1.Visible = true;
                        tbUserField2.Visible = false;
                        lblUserField2.Visible = true;
                        tbUserField3.Visible = false;
                        lblUserField3.Visible = true;
                        tbUserField4.Visible = false;
                        lblUserField4.Visible = true;
                        tbUserField5.Visible = false;
                        lblUserField5.Visible = true;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnSave.Style["background-color"] = null;
                        btnSave.CssClass = "StandardButton";
                        btnSaveTop.Enabled = true;
                        btnSaveTop.Style["background-color"] = null;
                        btnSaveTop.CssClass = "StandardButton";

                        tbUserField1.Visible = true;
                        lblUserField1.Visible = false;
                        tbUserField2.Visible = true;
                        lblUserField2.Visible = false;
                        tbUserField3.Visible = true;
                        lblUserField3.Visible = false;
                        tbUserField4.Visible = true;
                        lblUserField4.Visible = false;
                        tbUserField5.Visible = true;
                        lblUserField5.Visible = false;
                    }
                }

                DataTable dtUserValues = Classes.cUtilities.LoadDataTable("uspGetCharacterUserDef", sParam, "LARPortal", _UserName, lsRoutineName);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();
            if (oCharSelect.CharacterID.HasValue)
            {
                SortedList sParam = new SortedList();
                sParam.Add("@CharacterID", oCharSelect.CharacterID.Value);
                sParam.Add("@UserDefinedField1", tbUserField1.Text.Trim());
                sParam.Add("@UserDefinedField2", tbUserField2.Text.Trim());
                sParam.Add("@UserDefinedField3", tbUserField3.Text.Trim());
                sParam.Add("@UserDefinedField4", tbUserField4.Text.Trim());
                sParam.Add("@UserDefinedField5", tbUserField5.Text.Trim());

                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                DataTable dtUserDefined = Classes.cUtilities.LoadDataTable("uspInsUpdCHCharacterUserDefined", sParam, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

                lblmodalMessage.Text = "Character " + oCharSelect.CharacterInfo.AKA + " has been saved.";
                btnCloseMessage.Attributes.Add("data-dismiss", "modal");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", "openMessage();", true);
            }
        }

        protected void oCharSelect_CharacterChanged(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (oCharSelect.CharacterInfo != null)
            {
                if (oCharSelect.CharacterID.HasValue)
                {
                    Classes.cUser UserInfo = new Classes.cUser(_UserName, "PasswordNotNeeded");
                    UserInfo.LastLoggedInCampaign = oCharSelect.CharacterInfo.CampaignID;
                    UserInfo.LastLoggedInCharacter = oCharSelect.CharacterID.Value;
                    UserInfo.LastLoggedInMyCharOrCamp = (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters ? "M" : "C");
                    UserInfo.Save();
                }
            }
        }
    }
}
