using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.PELs
{
    public partial class PELApprove : System.Web.UI.Page
    {
        public bool TextBoxEnabled = true;
        private DataTable _dtPELComments = new DataTable();

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtQuestions = new DataTable();

                double dCPEarned = 0.0;
                if (Request.QueryString["RegistrationID"] != null)
                    hidRegistrationID.Value = Request.QueryString["RegistrationID"];
                else
                    Response.Redirect("PELApprovalList.aspx", true);

                SortedList sParams = new SortedList();
                sParams.Add("@RegistrationID", hidRegistrationID.Value);

                int iCharacterID = 0;
                int iUserID = 0;

                dtQuestions = Classes.cUtilities.LoadDataTable("uspGetPELQuestionsAndAnswers", sParams, "LARPortal", Session["UserName"].ToString(), "PELEdit.Page_PreRender");

                string sEventInfo = "";
                if (dtQuestions.Rows.Count > 0)
                {
                    sEventInfo = "<b>Event: </b> " + dtQuestions.Rows[0]["EventDescription"].ToString();

                    DateTime dtEventDate;
                    if (DateTime.TryParse(dtQuestions.Rows[0]["EventStartDate"].ToString(), out dtEventDate))
                        sEventInfo += "&nbsp;&nbsp;<b>Event Date: </b> " + dtEventDate.ToShortDateString();

                    int.TryParse(dtQuestions.Rows[0]["CharacterID"].ToString(), out iCharacterID);
                    int.TryParse(dtQuestions.Rows[0]["UserID"].ToString(), out iUserID);

                    if (iCharacterID != 0)
                    {
                        // A character.
                        sEventInfo += "&nbsp;&nbsp;<b>Character: </b> " + dtQuestions.Rows[0]["CharacterAKA"].ToString();
                    }

                    sEventInfo += "&nbsp;&nbsp;<b>Player: </b> " + dtQuestions.Rows[0]["NickName"].ToString();

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
                        if (!string.IsNullOrEmpty(Session["Username"].ToString()))
                            uName = Session["Username"].ToString();

                        PLDemography = new Classes.cPlayer(iUserID, uName);

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
                        double.TryParse(dtQuestions.Rows[0]["CPAwarded"].ToString(), out dCPEarned);
                        btnSave.Text = "Done";
                        btnSave.CommandName = "Done";
                        DateTime dtTemp;
                        if (DateTime.TryParse(dtQuestions.Rows[0]["PELDateApproved"].ToString(), out dtTemp))
                        {
                            lblEditMessage.Visible = true;
                            lblEditMessage.Text = "<br>This PEL was approved on " + dtTemp.ToShortDateString() + " and cannot be edited.";
                            TextBoxEnabled = false;
                            btnCancel.Visible = false;
                        }
                    }
                    else if (dtQuestions.Rows[0]["PELDateSubmitted"] != DBNull.Value)
                    {
                        btnSave.Text = "Approve";
                        btnSave.CommandName = "Approve";
                        DateTime dtTemp;
                        //                        pnlStaffComments.Visible = true;
                        divQuestions.Attributes.Add("style", "max-height: 400px; overflow-y: auto; margin-right: 10px;");
                        //if (DateTime.TryParse(dtQuestions.Rows[0]["PELDateApproved"].ToString(), out dtTemp))
                        //{
                        //    lblEditMessage.Visible = true;
                        //    lblEditMessage.Text = "<br>This PEL was approved on " + dtTemp.ToShortDateString();
                        //    TextBoxEnabled = false;
                        //    //                          pnlStaffComments.Visible = true;
                        //    double dCPAwarded = 0;
                        //    double.TryParse(dtQuestions.Rows[0]["CPAwarded"].ToString(), out dCPAwarded);
                        //    lblCPAwarded.Text = "For completing this PEL, this person was awarded " + String.Format("{0:0.##}", dCPAwarded) + " CP.";
                        //    mvCPAwarded.SetActiveView(vwCPAwardedDisplay);
                        //}
                        //else 
                        double.TryParse(dtQuestions.Rows[0]["CPEarn"].ToString(), out dCPEarned);
                        if (DateTime.TryParse(dtQuestions.Rows[0]["PELDateSubmitted"].ToString(), out dtTemp))
                        {
                            lblEditMessage.Visible = true;
                            lblEditMessage.Text = "<br>This PEL was submitted on " + dtTemp.ToShortDateString();
                            TextBoxEnabled = false;
                        }
                    }
                    if (hidPELID.Value.Length != 0)
                    {
                        // Load the comments for this PEL so we can display them in DataItemBound.
                        SortedList sParamsForComments = new SortedList();
                        sParamsForComments.Add("@PELID", hidPELID.Value);
                        _dtPELComments = Classes.cUtilities.LoadDataTable("uspGetPELStaffComments", sParamsForComments, "LARPortal", Session["UserName"].ToString(),
                            "PELApprove.Page_PreRender");
                    }
                }

                foreach (DataRow dRow in dtQuestions.Rows)
                {
                    dRow["Answer"] = dRow["Answer"].ToString().Replace("\n", "<br>");
                }

                tbCPAwarded.Text = dCPEarned.ToString("0.0");
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

                        if (hidQuestionID != null)
                            int.TryParse(hidQuestionID.Value, out iQuestionID);
                        if (hidAnswerID != null)
                            int.TryParse(hidAnswerID.Value, out iAnswerID);

                        if (iPELID == 0)
                            iPELID = -1;
                        if (iAnswerID == 0)
                            iAnswerID = -1;

                        sParams.Add("@PELAnswerID", iAnswerID);
                        sParams.Add("@PELQuestionsID", iQuestionID);
                        sParams.Add("@PELID", iPELID);
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
            }
            if (e.CommandName.ToUpper() == "APPROVE")
            {
                SortedList sParams = new SortedList();
                sParams.Add("@UserID", Session["UserID"].ToString());
                sParams.Add("@PELID", iPELID);

                double dCPAwarded;
                if (double.TryParse(tbCPAwarded.Text, out dCPAwarded))
                    sParams.Add("@CPAwarded", dCPAwarded);
                //            sParams.Add("@Comments", tbStaffComment.Text);
                sParams.Add("@DateApproved", DateTime.Now);

                Classes.cUtilities.PerformNonQuery("uspInsUpdCMPELs", sParams, "LARPortal", Session["UserName"].ToString());
                Session["UpdatePELMessage"] = "alert('The PEL has been approved.');";
            }

            Response.Redirect("PELApprovalList.aspx", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PELApprovalList.aspx", true);
        }

        protected void rptQuestions_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string i = e.CommandArgument.ToString();
            if (e.CommandName.ToUpper() == "ENTERCOMMENT")
            {
                Panel pnlNewCommentPanel = (Panel)e.Item.FindControl("pnlCommentSection");
                if (pnlNewCommentPanel != null)
                {
                    pnlNewCommentPanel.Visible = true;
                    Image imgPlayerImage = (Image)e.Item.FindControl("imgProfilePicture");
                    if (imgPlayerImage != null)
                    {
                        string uName = "";
                        int uID = 0;

                        if (Session["Username"] != null)
                            uName = Session["Username"].ToString();
                        if (Session["UserID"] != null)
                            int.TryParse(Session["UserID"].ToString(), out uID);

                        Classes.cPlayer PLDemography = new Classes.cPlayer(uID, uName);
                        string pict = PLDemography.UserPhoto;
                        imgPicture.Attributes["onerror"] = "this.src='~/img/BlankProfile.png';";

                        //if (pict == "")
                        //{
                        //    imgPlayerImage.ImageUrl = "http://placehold.it/150x150";
                        //}
                        //else
                        //{
                            imgPlayerImage.ImageUrl = pict;
                        //}
                    }

                    Button btnAddComment = (Button)e.Item.FindControl("btnAddComment");
                    if (btnAddComment != null)
                        btnAddComment.Visible = false;
                }
            }
            else if (e.CommandName.ToUpper() == "ADDCOMMENT")
            {
                int iAnswerID;
                if (int.TryParse(e.CommandArgument.ToString(), out iAnswerID))
                {
                    TextBox tbNewComment = (TextBox)e.Item.FindControl("tbNewComment");
                    if (tbNewComment != null)
                    {
                        if (tbNewComment.Text.Length > 0)
                        {
                            SortedList sParams = new SortedList();
                            sParams.Add("@UserID", Session["UserID"]);
                            sParams.Add("@PELAnswerID", iAnswerID);
                            sParams.Add("@PELStaffCommentID", "-1");
                            sParams.Add("@CommenterID", Session["UserID"]);
                            sParams.Add("@StaffComments", tbNewComment.Text.Trim());

                            MethodBase lmth = MethodBase.GetCurrentMethod();
                            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                            DataTable dtAddResponse = Classes.cUtilities.LoadDataTable("uspInsUpdCMPELStaffComments", sParams, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

                            SortedList sParamsForComments = new SortedList();
                            sParamsForComments.Add("@PELID", hidPELID.Value);
                            _dtPELComments = Classes.cUtilities.LoadDataTable("uspGetPELStaffComments", sParamsForComments, "LARPortal", Session["UserName"].ToString(),
                                "PELApprove.Page_PreRender");

                            DataList dlComments = e.Item.FindControl("dlComments") as DataList;
                            GetComments(iAnswerID.ToString(), dlComments);
                        }
                    }
                    Panel pnlCommentSection = (Panel)e.Item.FindControl("pnlCommentSection");
                    if (pnlCommentSection != null)
                        pnlCommentSection.Visible = false;
                    Button btnAddComment = (Button)e.Item.FindControl("btnAddComment");
                    if (btnAddComment != null)
                        btnAddComment.Visible = true;
                }
            }
            else if (e.CommandName.ToUpper() == "CANCELCOMMENT")
            {
                Panel pnlCommentSection = (Panel)e.Item.FindControl("pnlCommentSection");
                if (pnlCommentSection != null)
                    pnlCommentSection.Visible = false;
                Button btnAddComment = (Button)e.Item.FindControl("btnAddComment");
                if (btnAddComment != null)
                    btnAddComment.Visible = false;
            }
        }

        public void rptQuestions_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dr = (DataRowView)DataBinder.GetDataItem(e.Item);
                string sAnswerID = dr["PELAnswerID"].ToString();

                DataList dlComments = e.Item.FindControl("dlComments") as DataList;
                if (dlComments != null)
                {
                    GetComments(sAnswerID, dlComments);
                }
                TextBox tbNewComment = (TextBox)e.Item.FindControl("tbNewComment");
                if (tbNewComment != null)
                    tbNewComment.Attributes.Add("PlaceHolder", "Enter comment here.");
            }
        }

        protected void btnAddComment_Command(object sender, CommandEventArgs e)
        {

        }

        protected void GetComments(string sAnswerID, DataList dlComments)
        {
            foreach (DataRow dRow in _dtPELComments.Rows)
            {
                string sProfileFileName = HttpContext.Current.Request.PhysicalApplicationPath + dRow["UserPhoto"].ToString();
                sProfileFileName = sProfileFileName.Replace("~/img/Player/", "img\\Player\\");
//                sProfileFileName = sProfileFileName.Replace("~/img/Player/", @"\\");

                //if ((dRow["UserPhoto"].ToString().ToUpper().Contains("/IMG/P")) && (dRow["UserPhoto"].ToString().Length > 0))
                //    dRow["UserPhoto"] = dRow["UserPhoto"].ToString() + "/IMG/PLAYER/";
                if (!File.Exists(sProfileFileName))
                    dRow["UserPhoto"] = "/img/BlankProfile.png";
            }
                //if ((!dRow["UserPhoto"].ToString().ToUpper().Contains("/IMG/P")) && (dRow["UserPhoto"].ToString().Length > 0))
                //    dRow["UserPhoto"] = dRow["UserPhoto"].ToString() + "/IMG/PLAYER/";

            DataView dvComments = new DataView(_dtPELComments, "PELAnswerID = '" + sAnswerID + "'", "DateAdded desc", DataViewRowState.CurrentRows);
            dlComments.DataSource = dvComments;
            dlComments.DataBind();
        }
    }
}




// OnItemDataBound="rptQuestions_ItemDataBound">
