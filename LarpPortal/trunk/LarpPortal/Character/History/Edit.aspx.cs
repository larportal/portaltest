﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character.History
{
    public partial class Edit : System.Web.UI.Page
    {
        bool _Reload = false;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int iUserID = 0;
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out iUserID);

            if (iUserID == 0)
                iUserID = -1;

            //if (Session["CurrentCharacter"] == null)
            //    Session["CurrentCharacter"] = -1;
            if (Session["SelectedCharacter"] == null)
                Response.Redirect("../CharInfo.aspx", true);

//            string sCurrent = Session["CurrentCharacter"].ToString();
//            string sSelected = Session["SelectedCharacter"].ToString();
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
                    lblHistory.Text = cCharHist.History;

                    if (cCharHist.DateSubmitted.HasValue)
                    {
                        ckEditor.Visible = false;
                        lblHistory.Visible = true;
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        ckEditor.Visible = true;
                        lblHistory.Visible = false;
                        btnSave.Visible = true;
                        btnSubmit.Visible = true;
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
                        cCharHist.DateSubmitted = DateTime.Now;

                    cCharHist.Save(iCharID, iUserID, Session["UserName"].ToString());
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PELList.aspx", true);
        }

        private void SendEmailPELSubmitted(string sEmailBody)
        {
            try
            {
                if (ViewState["PELNotificationEMail"].ToString().Length > 0)
                {
                    string sPlayerName = ViewState["PlayerName"].ToString();
                    string sCharacterName = "";
                    if (ViewState["CharacterAKA"] != null)
                        sCharacterName = ViewState["CharacterAKA"].ToString();

                    string sEventDate = "";
                    if (ViewState["EventDate"] != null)
                    {
                        DateTime dtTemp;
                        if (DateTime.TryParse(ViewState["EventDate"].ToString(), out dtTemp))
                            sEventDate = " that took place on " + ViewState["EventDate"].ToString();
                    }

                    string sEventName = ViewState["EventName"].ToString();
                    string sPELNotificationEMail = ViewState["PELNotificationEMail"].ToString();

                    string sSubject = "PEL Submitted: " + sPlayerName + " has submitted a PEL.";
                    string sBody = sPlayerName + " has submitted a PEL for " + sCharacterName + " for the event " + sEventName + sEventDate + "<br><br>" +
                        sEmailBody;

                    Classes.cEmailMessageService cEMS = new Classes.cEmailMessageService();
                    cEMS.SendMail(sSubject, sBody, sPELNotificationEMail, "" , "", "CharacterHistory", Session["Username"].ToString());
                }
            }
            catch (Exception ex)
            {
                // Write the exception to error log and then throw it again...
                Classes.ErrorAtServer lobjError = new Classes.ErrorAtServer();
                lobjError.ProcessError(ex, "PELEdit.aspx.SendEmailPELSubmitted", "", Session.SessionID);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            AutosaveAnswers();
            lblSaveMessage.Text = "Saving at " + DateTime.Now.ToString();
        }

        protected void AutosaveAnswers()
        {
            int iPELID = -1;
            int iTemp = 0;

            if (int.TryParse(hidPELID.Value, out iTemp))
                iPELID = iTemp;

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
    }
}