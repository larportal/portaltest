using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.PELs
{
    public partial class PELEdit : System.Web.UI.Page
    {
        public bool TextBoxEnabled = true;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (hidTextBoxEnabled.Value == "1")
                TextBoxEnabled = true;

            if (!IsPostBack)
            {
                DataTable dtQuestions = new DataTable();

                if (Request.QueryString["RegistrationID"] == null)
                    Response.Redirect("PELList.aspx", true);

                hidRegistrationID.Value = Request.QueryString["RegistrationID"];

                SortedList sParams = new SortedList();
                sParams.Add("@RegistrationID", hidRegistrationID.Value);

                dtQuestions = Classes.cUtilities.LoadDataTable("uspGetPELQuestionsAndAnswers", sParams, "LARPortal", Session["UserName"].ToString(), "PELEdit.Page_PreRender");

                int iCharacterID = 0;
                int iUserID = 0;

                string sEventInfo = "";
                if (dtQuestions.Rows.Count > 0)
                {
                    hidPELTemplateID.Value = dtQuestions.Rows[0]["PELTemplateID"].ToString();
                    sEventInfo = "<b>Event: </b> " + dtQuestions.Rows[0]["EventDescription"].ToString();

                    DateTime dtEventDate;
                    if (DateTime.TryParse(dtQuestions.Rows[0]["EventStartDate"].ToString(), out dtEventDate))
                    {
                        sEventInfo += "&nbsp;&nbsp;<b>Event Date: </b> " + dtEventDate.ToShortDateString();
                        ViewState["EventDate"] = dtEventDate.ToShortDateString();
                    }

                    ViewState["EventDescription"] = dtQuestions.Rows[0]["EventDescription"].ToString();
                    ViewState["PELNotificationEMail"] = dtQuestions.Rows[0]["PELNotificationEMail"].ToString();
                    ViewState["PlayerName"] = dtQuestions.Rows[0]["NickName"].ToString();

                    int.TryParse(dtQuestions.Rows[0]["CharacterID"].ToString(), out iCharacterID);
                    int.TryParse(dtQuestions.Rows[0]["UserID"].ToString(), out iUserID);
                    
                    if (iCharacterID != 0)
                    {
                        // A character.
                        sEventInfo += "&nbsp;&nbsp;<b>Character: </b> " + dtQuestions.Rows[0]["CharacterAKA"].ToString();
                        ViewState["CharacterAKA"] = dtQuestions.Rows[0]["CharacterAKA"].ToString();
                    }
                    else
                    {
                        // Non character.
                        sEventInfo += "&nbsp;&nbsp;<b>Player: </b> " + dtQuestions.Rows[0]["NickName"].ToString();
                        ViewState["CharacterAKA"] = "a non-player.";
                    }


                    int.TryParse(dtQuestions.Rows[0]["CharacterID"].ToString(), out iCharacterID);
                    if (iCharacterID != 0)
                    {
                        Classes.cCharacter cChar = new Classes.cCharacter();
                        cChar.LoadCharacter(iCharacterID);
                        imgPicture.ImageUrl = "/img/BlankProfile.png";    // Default it to this so if it is not set it will display the blank profile picture.
                        if (cChar.ProfilePicture != null)
                            if (!string.IsNullOrEmpty(cChar.ProfilePicture.PictureURL))
                                imgPicture.ImageUrl = cChar.ProfilePicture.PictureURL;
                        imgPicture.Attributes["onerror"] = "this.src='~/img/BlankProfile.png';";
                    }
                    else
                    {
                        Classes.cPlayer PLDemography = null;

                        string uName = "";
                        int uID = 0;
                        if ( !string.IsNullOrEmpty(Session["Username"].ToString()))
                            uName = Session["Username"].ToString();
                        int.TryParse(Session["UserID"].ToString(), out uID);

                        PLDemography = new Classes.cPlayer(uID, uName);

                        imgPicture.ImageUrl = "/img/BlankProfile.png";    // Default it to this so if it is not set it will display the blank profile picture.
                        if (!string.IsNullOrEmpty(PLDemography.UserPhoto))
                            imgPicture.ImageUrl = PLDemography.UserPhoto;
                        imgPicture.Attributes["onerror"] = "this.src='~/img/BlankProfile.png';";
                    }

                    lblEventInfo.Text = sEventInfo;

                    int iTemp;
                    if (int.TryParse(dtQuestions.Rows[0]["PELID"].ToString(), out iTemp))
                        hidPELID.Value = iTemp.ToString();
                    if (dtQuestions.Rows[0]["PELDateApproved"] != DBNull.Value)
                    {
                        btnSubmit.Visible = false;
                        pnlSaveReminder.Visible = false;
                        btnSave.Text = "Done";
                        btnSave.CommandName = "Done";
                        DateTime dtTemp;
                        if (DateTime.TryParse(dtQuestions.Rows[0]["PELDateApproved"].ToString(), out dtTemp))
                        {
                            lblEditMessage.Visible = true;
                            lblEditMessage.Text = "<br>This PEL was approved on " + dtTemp.ToShortDateString() + " and cannot be edited.";
                            TextBoxEnabled = false;
                            hidTextBoxEnabled.Value = "0";
                            btnCancel.Visible = false;
                            foreach (DataRow dRow in dtQuestions.Rows)
                            {
                                dRow["Answer"] = dRow["Answer"].ToString().Replace("\n", "<br>");
                            }
                        }
                    }
                    else if (dtQuestions.Rows[0]["PELDateSubmitted"] != DBNull.Value)
                    {
                        btnSubmit.Visible = false;
                        pnlSaveReminder.Visible = false;
                        btnSave.Text = "Done";
                        btnSave.CommandName = "Done";
                        DateTime dtTemp;
                        if (DateTime.TryParse(dtQuestions.Rows[0]["PELDateSubmitted"].ToString(), out dtTemp))
                        {
                            lblEditMessage.Visible = true;
                            lblEditMessage.Text = "<br>This PEL was submitted on " + dtTemp.ToShortDateString() + " and cannot be edited.";
                            TextBoxEnabled = false;
                            hidTextBoxEnabled.Value = "0";
                            btnCancel.Visible = false;
                            foreach (DataRow dRow in dtQuestions.Rows)
                            {
                                dRow["Answer"] = dRow["Answer"].ToString().Replace("\n", "<br>");
                            }
                        }
                    }
                }

                DataView dvQuestions = new DataView(dtQuestions, "", "SortOrder", DataViewRowState.CurrentRows);
                rptQuestions.DataSource = dvQuestions;
                rptQuestions.DataBind();
            }
        }

        protected void ProcessButton(object sender, CommandEventArgs e)
        {
            int iPELID = -1;
            int iTemp;
            if (int.TryParse(hidPELID.Value, out iTemp))
                iPELID = iTemp;

            if ((e.CommandName.ToUpper() == "SAVE") || (e.CommandName.ToUpper() == "SUBMIT"))
            {
                foreach (RepeaterItem item in rptQuestions.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        SortedList sParams = new SortedList();

                        TextBox tbAnswer = (TextBox)item.FindControl("tbAnswer");
                        HiddenField hidQuestionID = (HiddenField)item.FindControl("hidQuestionID");
                        HiddenField hidAnswerID = (HiddenField)item.FindControl("hidAnswerID");
                        int iQuestionID = 0;
                        int iAnswerID = 0;
                        int iPELTemplateID = 0;

                        if (hidQuestionID != null)
                            int.TryParse(hidQuestionID.Value, out iQuestionID);
                        if (hidAnswerID != null)
                            int.TryParse(hidAnswerID.Value, out iAnswerID);
                        if (hidPELTemplateID != null)
                            int.TryParse(hidPELTemplateID.Value, out iPELTemplateID);

                        if (iPELID == 0)
                            iPELID = -1;
                        if (iAnswerID == 0)
                            iAnswerID = -1;

                        sParams.Add("@PELAnswerID", iAnswerID);
                        sParams.Add("@PELQuestionsID", iQuestionID);
                        sParams.Add("@PELID", iPELID);
                        sParams.Add("@PELTemplateID", iPELTemplateID);
                        sParams.Add("@Answer", tbAnswer.Text);
                        sParams.Add("@RegistrationID", hidRegistrationID.Value);

                        DataSet dsPELS = new DataSet();
                        dsPELS = Classes.cUtilities.LoadDataSet("uspPELsAnswerInsUpd", sParams, "LARPortal", Session["UserName"].ToString(), "PELEdit.btnSave_Click");

                        if ((dsPELS.Tables.Count > 1) && (iPELID == -1))
                        {
                            if (int.TryParse(dsPELS.Tables[1].Rows[0]["PELID"].ToString(), out iTemp))
                            {
                                iPELID = iTemp;
                                hidPELID.Value = iTemp.ToString();
                            }
                        }
                    }
                    Session["UpdatePELMessage"] = "alert('The PEL has been saved.');";
                }

            }
            if (e.CommandName.ToUpper() == "SUBMIT")
            {
                SortedList sParams = new SortedList();
                sParams.Add("@UserID", Session["UserID"].ToString());
                sParams.Add("@PELID", iPELID);
                sParams.Add("@DateSubmitted", DateTime.Now);

                Classes.cUtilities.PerformNonQuery("uspInsUpdCMPELs", sParams, "LARPortal", Session["UserName"].ToString());
                Session["UpdatePELMessage"] = "alert('The PEL has been saved and submitted.');";
                SendEmailPELSubmitted();
            }

            Response.Redirect("PELList.aspx", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PELList.aspx", true);
        }

        private void SendEmailPELSubmitted()
        {
            if (ViewState["PELNotificationEMail"].ToString().Length > 0)
            {
                string sPlayerName = ViewState["PlayerName"].ToString();
                string sCharacterName = "";
                if (ViewState["CharacterAKA"] != null)
                    sCharacterName = ViewState["CharacterAKA"].ToString();

                string sEventDate = "";
                if (ViewState["EventDate"] != null)
                    sEventDate = ViewState["EventDate"].ToString();

                string sEventDesc = ViewState["EventDescription"].ToString();
                string sPELNotificationEMail = ViewState["PELNotificationEMail"].ToString();

                string sSubject = "PEL Submitted: The " + sPlayerName + " has submitted a PEL.";
                string sBody = "The " + sPlayerName + " has submitted a PEL for " + sCharacterName + " for the event " + sEventDesc + " that took place on " + sEventDesc;

                Classes.cEmailMessageService cEMS = new Classes.cEmailMessageService();
                /// Todo Verify email.
//                cEMS.SendMail(sSubject, sBody, sPELNotificationEMail, "", "support@larportal.com");



                sBody += "   This email would have gone to " + sPELNotificationEMail;
                cEMS.SendMail(sSubject, sBody, "support@larportal.com", "", "support@larportal.com");
            }
        }
    }
}