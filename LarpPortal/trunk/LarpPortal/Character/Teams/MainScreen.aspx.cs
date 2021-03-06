﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character.Teams
{
    public partial class MainScreen : System.Web.UI.Page
    {
        bool _Reload = false;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int iUserID = 0;
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out iUserID);

            if (iUserID == 0)
                iUserID = -1;

            if (Session["SelectedCharacter"] == null)
                Response.Redirect("../CharInfo.aspx", true);

            if ((!IsPostBack) || (_Reload))
            {
                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", "CharacterMaster.Page_Load");
                ddlCharacterSelector.DataTextField = "CharacterAKA";
                ddlCharacterSelector.DataValueField = "CharacterID";
                ddlCharacterSelector.DataSource = dtCharacters;
                ddlCharacterSelector.DataBind();

                int iCharID;
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    ddlCharacterSelector.ClearSelection();
                    foreach (ListItem li in ddlCharacterSelector.Items)
                    {
                        if (li.Value == iCharID.ToString())
                        {
                            ddlCharacterSelector.ClearSelection();
                            li.Selected = true;
                        }
                    }

                    Classes.cCharacterHistory cCharHist = new Classes.cCharacterHistory();
                    cCharHist.Load(iCharID, iUserID);

                    ckEditor.Text = cCharHist.History;
                    lblHistory.Text = cCharHist.History.Replace("<ul>", @"<ul style=""list-style-type: disc;"">").Replace("<li>", @"<li style=""margin-left: 15px;"">");

                    hidNotificationEMail.Value = cCharHist.NotificationEMail;

                    if (cCharHist.Addendums.Count > 0)
                    {
                        DataTable dtDispAdd = new DataTable();
                        dtDispAdd.Columns.Add("Title", typeof(string));
                        dtDispAdd.Columns.Add("Addendum", typeof(string));
                        dtDispAdd.Columns.Add("DateAdded", typeof(DateTime));

                        foreach (Classes.cCharacterHistoryAddendum cAdd in cCharHist.Addendums)
                        {
                            DataRow dNewRow = dtDispAdd.NewRow();
                            dNewRow["Title"] = "Addendum added " + cAdd.DateAdded.ToString();
                            dNewRow["Addendum"] = cAdd.Addendum;
                            dNewRow["DateAdded"] = cAdd.DateAdded;
                            dtDispAdd.Rows.Add(dNewRow);
                        }
                        DataView dvDispAdd = new DataView(dtDispAdd, "", "DateAdded desc", DataViewRowState.CurrentRows);
                        rptAddendum.DataSource = dvDispAdd;
                        rptAddendum.DataBind();
                    }

                    if (cCharHist.DateSubmitted.HasValue)
                    {
                        ckEditor.Visible = false;
                        lblHistory.Visible = true;
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                        btnAddAddendum.Visible = true;
                    }
                    else
                    {
                        ckEditor.Visible = true;
                        lblHistory.Visible = false;
                        btnSave.Visible = true;
                        btnSubmit.Visible = true;
                        btnAddAddendum.Visible = false;
                    }
                }
            }
        }

        protected void ProcessButton(object sender, CommandEventArgs e)
        {
            int iUserID = 0;
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out iUserID);
            iUserID = iUserID == 0 ? -1 : iUserID;

            if (Session["SelectedCharacter"] != null)
            {
                string sSelected = Session["SelectedCharacter"].ToString();
                int iCharID;
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    Classes.cCharacterHistory cCharHist = new Classes.cCharacterHistory();
                    cCharHist.Load(iCharID, iUserID);
                    cCharHist.History = ckEditor.Text;
                    if (e.CommandName.ToUpper() == "SUBMIT")
                    {
                        cCharHist.DateSubmitted = DateTime.Now;
                        lblmodalMessage.Text = "The character history has been submitted.";
                        SendSubmittedEmail(ckEditor.Text, cCharHist);
                    }
                    else
                        lblmodalMessage.Text = "The character history has been saved.";

                    cCharHist.Save(iCharID, iUserID, Session["UserName"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
                    _Reload = true;
                }
            }
        }

        private void SendSubmittedEmail(string sHistory, Classes.cCharacterHistory cHist)
        {
            try
            {
                if (hidNotificationEMail.Value.Length > 0)
                {
                    Classes.cUser User = new Classes.cUser(Session["UserName"].ToString(), "PasswordNotNeeded");
                    string sSubject = cHist.CampaignName + " character history from " + cHist.PlayerName + " - " + cHist.CharacterAKA;

                    string sBody = (string.IsNullOrEmpty(User.NickName) ? User.FirstName : User.NickName) +
                        " " + User.LastName + " has submitted a character history for " + cHist.CharacterAKA + ".<br><br>" +
                         sHistory;
                    Classes.cEmailMessageService cEMS = new Classes.cEmailMessageService();
                    cEMS.SendMail(sSubject, sBody, cHist.NotificationEMail, "", "", "CharacterHistory", Session["Username"].ToString());
                }
            }
            catch (Exception ex)
            {
                // Write the exception to error log and then throw it again...
                Classes.ErrorAtServer lobjError = new Classes.ErrorAtServer();
                lobjError.ProcessError(ex, "CharacterEdit.aspx.SendSubmittedEmail", "", Session.SessionID);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            AutosaveAnswers();
        }

        protected void AutosaveAnswers()
        {
//            int iPELID = -1;
            int iTemp = 0;

            // Nothing to do....
            iTemp = iTemp + 15;

            //if (int.TryParse(hidPELID.Value, out iTemp))
            //    iPELID = iTemp;

            //foreach (RepeaterItem item in rptQuestions.Items)
            //{
            //    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            //    {
            //        SortedList sParams = new SortedList();

            //        Label lblQuestion = (Label)item.FindControl("lblQuestion");
            //        TextBox tbAnswer = (TextBox)item.FindControl("tbAnswer");
            //        HiddenField hidQuestionID = (HiddenField)item.FindControl("hidQuestionID");
            //        HiddenField hidAnswerID = (HiddenField)item.FindControl("hidAnswerID");
            //        int iQuestionID = 0;
            //        int iAnswerID = 0;
            //        int iPELTemplateID = 0;

            //        if (hidQuestionID != null)
            //            int.TryParse(hidQuestionID.Value, out iQuestionID);
            //        if (hidAnswerID != null)
            //            int.TryParse(hidAnswerID.Value, out iAnswerID);
            //        if (hidPELTemplateID != null)
            //            int.TryParse(hidPELTemplateID.Value, out iPELTemplateID);

            //        if (iPELID == 0)
            //            iPELID = -1;
            //        if (iAnswerID == 0)
            //            iAnswerID = -1;

            //        sParams.Add("@PELAnswerID", iAnswerID);
            //        sParams.Add("@PELQuestionsID", iQuestionID);
            //        sParams.Add("@PELID", iPELID);
            //        sParams.Add("@PELTemplateID", iPELTemplateID);
            //        sParams.Add("@Answer", tbAnswer.Text);
            //        sParams.Add("@RegistrationID", hidRegistrationID.Value);

            //        DataSet dsPELS = new DataSet();
            //        dsPELS = Classes.cUtilities.LoadDataSet("uspPELsAnswerInsUpd", sParams, "LARPortal", Session["UserName"].ToString(), "PELEdit.btnSave_Click");
            //        if (iAnswerID == -1)
            //        {
            //            int.TryParse(dsPELS.Tables[0].Rows[0]["PELAnswerID"].ToString(), out iTemp);
            //            hidAnswerID.Value = iTemp.ToString();
            //        }

            //        if ((dsPELS.Tables.Count > 1) && (iPELID == -1))
            //        {
            //            if (int.TryParse(dsPELS.Tables[1].Rows[0]["PELID"].ToString(), out iTemp))
            //            {
            //                iPELID = iTemp;
            //                hidPELID.Value = iTemp.ToString();
            //            }
            //        }
            //    }
            //}
        }

        protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCharacterSelector.SelectedValue == "-1")
                Response.Redirect("../CharAdd.aspx");

            if (Session["SelectedCharacter"].ToString() != ddlCharacterSelector.SelectedValue)
            {
                Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                _Reload = true;
            }
        }

        protected void btnCloseMessage_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeMessage();", true);
        }

        protected void btnSaveAddendum_Click(object sender, EventArgs e)
        {
            int iCharID = 0;
            int iUserID = 0;
            if ((int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID)) &&
                (int.TryParse(Session["UserID"].ToString(), out iUserID)))
            {
                Classes.cCharacterHistoryAddendum cHistAdd = new Classes.cCharacterHistoryAddendum(iCharID);
                cHistAdd.Addendum = CKEAddendum.Text;
                cHistAdd.Save(Session["UserName"].ToString(), iUserID);
            }
            _Reload = true;
        }
    }
}