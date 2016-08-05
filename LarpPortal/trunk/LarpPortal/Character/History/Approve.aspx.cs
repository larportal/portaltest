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

namespace LarpPortal.Character.History
{
    public partial class Approve : System.Web.UI.Page
    {
        public bool TextBoxEnabled = true;

        private DataSet _dsHistory = new DataSet();
        const int CHARHISTORY = 0;
        const int STAFFCOMMENTS = 1;
        const int ADDENDUMS = 2;
        const int STAFFADDENDUMCOMMENTS = 3;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            _dsHistory = new DataSet();

            double dCPEarned = 0.0;
            if (Request.QueryString["CharacterID"] != null)
                hidCharacterID.Value = Request.QueryString["CharacterID"];
            else
                Response.Redirect("ApprovalList.aspx", true);

            SortedList sParams = new SortedList();
            sParams.Add("@CharacterID", hidCharacterID.Value);

            int iCharacterID = 0;
            int iUserID = 0;

            _dsHistory = Classes.cUtilities.LoadDataSet("uspGetCharacterHistory", sParams, "LARPortal", Session["UserName"].ToString(), "PELEdit.Page_PreRender");

            string sCharacterInfo = "";
            if (_dsHistory.Tables[CHARHISTORY].Rows.Count > 0)
            {
                DataRow drHistory = _dsHistory.Tables[CHARHISTORY].Rows[0];
                sCharacterInfo = "<b>Character: </b> " + drHistory["CharacterAKA"].ToString();

                int.TryParse(drHistory["CharacterID"].ToString(), out iCharacterID);
                int.TryParse(drHistory["CampaignPlayerID"].ToString(), out iUserID);

                hidCampaignName.Value = drHistory["CampaignName"].ToString();
                hidNotificationEMail.Value = drHistory["CharacterHistoryNotificationEmail"].ToString();
                if (hidNotificationEMail.Value.Length == 0)
                    hidNotificationEMail.Value = "support@larportal.com";

                hidEmail.Value = drHistory["EmailAddress"].ToString();

                int iCampaignPlayerID = 0;
                if (int.TryParse(drHistory["CampaignPlayerID"].ToString(), out iCampaignPlayerID))
                    hidCampaignPlayerID.Value = iCampaignPlayerID.ToString();

                int.TryParse(drHistory["CharacterID"].ToString(), out iCharacterID);
                hidCharacterAKA.Value = drHistory["CharacterAKA"].ToString();
                imgPicture.ImageUrl = "/img/BlankProfile.png";    // Default it to this so if it is not set it will display the blank profile picture.

                hidCharacterID.Value = iCharacterID.ToString();
                hidCampaignID.Value = drHistory["CampaignID"].ToString();

                lblHistory.Text = drHistory["CharacterHistory"].ToString();
                ckHistory.Text = "Staff has reopened the character history for " + hidCharacterAKA.Value + " for revisions.  Please make changes and resubmit the history.<br><br>" +
                    "Thank you<br>" +
                    drHistory["CampaignName"].ToString() + " staff<br><br>" +
                    drHistory["CharacterHistory"].ToString();

                lblCharacterInfo.Text = sCharacterInfo;

                if (drHistory["DateHistoryApproved"] != DBNull.Value)
                {
                    double.TryParse(drHistory["CPAwarded"].ToString(), out dCPEarned);
                    btnApprove.Text = "Done";
                    btnApprove.CommandName = "Done";
                    DateTime dtTemp;
                    if (DateTime.TryParse(drHistory["DateHistoryApproved"].ToString(), out dtTemp))
                    {
                        lblEditMessage.Visible = true;
                        lblEditMessage.Text = "<br>This history was approved on " + dtTemp.ToShortDateString() + " and cannot be edited.";
                        TextBoxEnabled = false;
                        btnCancel.Visible = false;
                        btnReject.Visible = false;
                    }
                }
                else if (drHistory["DateHistorySubmitted"] != DBNull.Value)
                {
                    btnApprove.Text = "Approve";
                    btnApprove.CommandName = "Approve";
                    btnReject.Visible = true;
                    DateTime dtTemp;
                    divQuestions.Attributes.Add("style", "max-height: 400px; overflow-y: auto; margin-right: 10px;");
                    double.TryParse(drHistory["CPEarn"].ToString(), out dCPEarned);
                    if (DateTime.TryParse(drHistory["DateHistorySubmitted"].ToString(), out dtTemp))
                    {
                        lblEditMessage.Visible = true;
                        lblEditMessage.Text = "<br>This history was submitted on " + dtTemp.ToShortDateString();
                        TextBoxEnabled = false;
                        hidSubmitDate.Value = dtTemp.ToShortDateString();
                    }
                }
            }

            tbCPAwarded.Text = dCPEarned.ToString("0.0");

            if (_dsHistory.Tables[ADDENDUMS] != null)
            {
                DataView dvAddendum = new DataView(_dsHistory.Tables[ADDENDUMS], "", "DateAdded desc", DataViewRowState.CurrentRows);
                rptAddendum.DataSource = dvAddendum;
                rptAddendum.DataBind();
            }
            if (_dsHistory.Tables[STAFFCOMMENTS] != null)
            {
                DataView dvStaffComments = new DataView(_dsHistory.Tables[STAFFCOMMENTS], "", "DateAdded desc", DataViewRowState.CurrentRows);
                dlComments.DataSource = dvStaffComments;
                dlComments.DataBind();
            }
        }

        protected void ProcessButton(object sender, CommandEventArgs e)
        {
            int iCharacterID = -1;
            int iTemp;
            if (int.TryParse(hidCharacterID.Value, out iTemp))
                iCharacterID = iTemp;

            if (e.CommandName.ToUpper() == "APPROVE")
            {
                SortedList sParams = new SortedList();
                sParams.Add("@UserID", Session["UserID"].ToString());
                sParams.Add("@CharacterID", iCharacterID);

                sParams.Add("@DateHistoryApproved", DateTime.Now);

                Classes.cUtilities.PerformNonQuery("uspInsUpdCHCharacters", sParams, "LARPortal", Session["UserName"].ToString());
                Session["UpdateHistoryMessage"] = "alert('The character history has been approved.');";

                Classes.cPoints Points = new Classes.cPoints();
                int UserID = 0;
                int CampaignPlayerID = 0;
                int CharacterID = 0;
                int CampaignCPOpportunityDefaultID = 0;
                int CampaignID = 0;
                double CPAwarded = 0.0;

                int.TryParse(Session["UserID"].ToString(), out UserID);
                int.TryParse(hidCampaignPlayerID.Value, out CampaignPlayerID);
                int.TryParse(hidCharacterID.Value, out CharacterID);
                int.TryParse(hidCampaignCPOpportunityDefaultID.Value, out CampaignCPOpportunityDefaultID);
                int.TryParse(hidCampaignID.Value, out CampaignID);
                double.TryParse(tbCPAwarded.Text, out CPAwarded);

                DateTime dtDateSubmitted;
                if (!DateTime.TryParse(hidSubmitDate.Value, out dtDateSubmitted))
                    dtDateSubmitted = DateTime.Now;

                Points.AssignHistoryPoints(UserID, CampaignPlayerID, CharacterID, CampaignCPOpportunityDefaultID, CampaignID, CPAwarded, dtDateSubmitted);

                Response.Redirect("ApprovalList.aspx", true);
            }
            else if (e.CommandName.ToUpper() == "REJECT")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApprovalList.aspx", true);
        }

        //protected void rptQuestions_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    string i = e.CommandArgument.ToString();
        //    if (e.CommandName.ToUpper() == "ENTERCOMMENT")
        //    {
        //        Panel pnlNewCommentPanel = (Panel)e.Item.FindControl("pnlCommentSection");
        //        if (pnlNewCommentPanel != null)
        //        {
        //            pnlNewCommentPanel.Visible = true;
        //            Image imgPlayerImage = (Image)e.Item.FindControl("imgProfilePicture");
        //            if (imgPlayerImage != null)
        //            {
        //                string uName = "";
        //                int uID = 0;

        //                if (Session["Username"] != null)
        //                    uName = Session["Username"].ToString();
        //                if (Session["UserID"] != null)
        //                    int.TryParse(Session["UserID"].ToString(), out uID);

        //                Classes.cPlayer PLDemography = new Classes.cPlayer(uID, uName);
        //                string pict = PLDemography.UserPhoto;
        //                imgPicture.Attributes["onerror"] = "this.src='~/img/BlankProfile.png';";
        //                imgPlayerImage.ImageUrl = pict;
        //            }

        //            Button btnAddComment = (Button)e.Item.FindControl("btnAddComment");
        //            if (btnAddComment != null)
        //                btnAddComment.Visible = false;
        //        }
        //    }
        //    else if (e.CommandName.ToUpper() == "ADDCOMMENT")
        //    {
        //        int iAnswerID;
        //        if (int.TryParse(e.CommandArgument.ToString(), out iAnswerID))
        //        {
        //            TextBox tbNewComment = (TextBox)e.Item.FindControl("tbNewComment");
        //            if (tbNewComment != null)
        //            {
        //                if (tbNewComment.Text.Length > 0)
        //                {
        //                    SortedList sParams = new SortedList();
        //                    sParams.Add("@UserID", Session["UserID"]);
        //                    sParams.Add("@PELAnswerID", iAnswerID);
        //                    sParams.Add("@PELStaffCommentID", "-1");
        //                    sParams.Add("@CommenterID", Session["UserID"]);
        //                    sParams.Add("@StaffComments", tbNewComment.Text.Trim());

        //                    MethodBase lmth = MethodBase.GetCurrentMethod();
        //                    string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

        //                    DataTable dtAddResponse = Classes.cUtilities.LoadDataTable("uspInsUpdCHCharacterHistoryStaffComments", sParams, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

        //                    //SortedList sParamsForComments = new SortedList();
        //                    //sParamsForComments.Add("@CharacterID", hidPELID.Value);
        //                    //_dtPELComments = Classes.cUtilities.LoadDataTable("uspGetPELStaffComments", sParamsForComments, "LARPortal", Session["UserName"].ToString(),
        //                    //    "PELApprove.Page_PreRender");


        //                    DataList dlComments = e.Item.FindControl("dlComments") as DataList;
        //                    GetComments(iAnswerID.ToString(), dlComments);
        //                    SendStaffCommentEMail(dtAddResponse);
        //                }
        //            }
        //            Panel pnlCommentSection = (Panel)e.Item.FindControl("pnlCommentSection");
        //            if (pnlCommentSection != null)
        //                pnlCommentSection.Visible = false;
        //            Button btnAddComment = (Button)e.Item.FindControl("btnAddComment");
        //            if (btnAddComment != null)
        //                btnAddComment.Visible = true;
        //        }
        //    }
        //    else if (e.CommandName.ToUpper() == "CANCELCOMMENT")
        //    {
        //        Panel pnlCommentSection = (Panel)e.Item.FindControl("pnlCommentSection");
        //        if (pnlCommentSection != null)
        //            pnlCommentSection.Visible = false;
        //        Button btnAddComment = (Button)e.Item.FindControl("btnAddComment");
        //        if (btnAddComment != null)
        //            btnAddComment.Visible = false;
        //    }
        //}

        //public void rptQuestions_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        //{
        //    if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        //    {
        //        DataRowView dr = (DataRowView)DataBinder.GetDataItem(e.Item);
        //        string sAnswerID = dr["PELAnswerID"].ToString();

        //        DataList dlComments = e.Item.FindControl("dlComments") as DataList;
        //        if (dlComments != null)
        //        {
        //            GetComments(sAnswerID, dlComments);
        //        }
        //        TextBox tbNewComment = (TextBox)e.Item.FindControl("tbNewComment");
        //        if (tbNewComment != null)
        //            tbNewComment.Attributes.Add("PlaceHolder", "Enter comment here.");
        //    }
        //}

        //protected void btnAddComment_Command(object sender, CommandEventArgs e)
        //{

        //}

        protected void GetComments(string sAnswerID, DataTable dtComments)
        {
            foreach (DataRow dRow in dtComments.Rows)
            {
                string sProfileFileName = HttpContext.Current.Request.PhysicalApplicationPath + dRow["UserPhoto"].ToString();
                sProfileFileName = sProfileFileName.Replace("~/img/Player/", "img\\Player\\");
                if (!File.Exists(sProfileFileName))
                    dRow["UserPhoto"] = "/img/BlankProfile.png";
            }
        }


        protected void rptAddendum_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dr = (DataRowView)DataBinder.GetDataItem(e.Item);
                string sAnswerID = dr["PELsAddendumID"].ToString();

                DataList dlComments = e.Item.FindControl("dlStaffComments") as DataList;
                if (dlComments != null)
                {
                    GetAddendumComments(sAnswerID, _dsHistory.Tables[3], dlComments);
                }
                TextBox tbNewComment = (TextBox)e.Item.FindControl("tbNewStaffCommentAddendum");
                if (tbNewComment != null)
                    tbNewComment.Attributes.Add("PlaceHolder", "Enter comment here.");
            }
        }

        protected void rptAddendum_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string i = e.CommandArgument.ToString();
            if (e.CommandName.ToUpper() == "ENTERCOMMENT")
            {
                Panel pnlNewCommentPanel = (Panel)e.Item.FindControl("pnlStaffCommentSection");
                if (pnlNewCommentPanel != null)
                {
                    pnlNewCommentPanel.Visible = true;
                    Image imgPlayerImage = (Image)e.Item.FindControl("imgStaffCommentProfilePicture");
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
                        imgPlayerImage.ImageUrl = pict;
                    }

                    Button btnAddComment = (Button)e.Item.FindControl("btnAddStaffComment");
                    if (btnAddComment != null)
                        btnAddComment.Visible = false;
                }
            }
            else if (e.CommandName.ToUpper() == "ADDCOMMENT")
            {
                int iAddendumID;
                if (int.TryParse(e.CommandArgument.ToString(), out iAddendumID))
                {
                    TextBox tbNewComment = (TextBox)e.Item.FindControl("tbNewStaffCommentAddendum");
                    if (tbNewComment != null)
                    {
                        if (tbNewComment.Text.Length > 0)
                        {
                            SortedList sParams = new SortedList();
                            sParams.Add("@UserID", Session["UserID"]);
                            sParams.Add("@CharAddendumsStaffCommentID", "-1");
                            sParams.Add("@CharAddendumID", iAddendumID);
                            sParams.Add("@CommenterID", Session["UserID"]);
                            sParams.Add("@StaffComments", tbNewComment.Text.Trim());

                            MethodBase lmth = MethodBase.GetCurrentMethod();
                            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                            DataTable dtAddResponse = Classes.cUtilities.LoadDataTable("uspInsUpdCHCharacterHistoryAddendumsStaffComments", sParams, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

                            DataList dlComments = e.Item.FindControl("dlStaffComments") as DataList;
                            GetAddendumComments(iAddendumID.ToString(), dtAddResponse, dlComments);
                            SendStaffAddendumCommentEMail(dtAddResponse);
                        }
                    }
                    Panel pnlCommentSection = (Panel)e.Item.FindControl("pnlStaffCommentSection");
                    if (pnlCommentSection != null)
                        pnlCommentSection.Visible = false;
                    Button btnAddComment = (Button)e.Item.FindControl("btnAddStaffComment");
                    if (btnAddComment != null)
                        btnAddComment.Visible = true;
                }
            }
            else if (e.CommandName.ToUpper() == "CANCELCOMMENT")
            {
                Panel pnlCommentSection = (Panel)e.Item.FindControl("pnlStaffCommentSection");
                if (pnlCommentSection != null)
                    pnlCommentSection.Visible = false;
                Button btnAddComment = (Button)e.Item.FindControl("btnAddStaffComment");
                if (btnAddComment != null)
                    btnAddComment.Visible = true;
            }
        }

        protected void GetAddendumComments(string sAddendumID, DataTable dtAddendums, DataList dtAddendumComments)
        {

            foreach (DataRow dRow in dtAddendums.Rows)
            {
                //string sProfileFileName = HttpContext.Current.Request.PhysicalApplicationPath + dRow["UserPhoto"].ToString();
                //sProfileFileName = sProfileFileName.Replace("~/img/Player/", "img\\Player\\");
                //if (!File.Exists(sProfileFileName))
                dRow["UserPhoto"] = "/img/BlankProfile.png";
            }

            DataView dvComments = new DataView(_dsHistory.Tables[3], "CharacterHistoryAddendumID = '" + sAddendumID + "'", "DateAdded desc", DataViewRowState.CurrentRows);
            dlComments.DataSource = dvComments;
            dlComments.DataBind();
        }




        protected void SendStaffAddendumCommentEMail(DataTable dtComments)
        {
            if (hidCharacterID.Value.Length > 0)
            {
                string sSubject = Session["UserName"].ToString() + " has added a comment to a character history addendum.";
                string sBody = Session["UserName"].ToString() + " has added a comment to a character history addendum for " + hidPlayerName.Value + "<br><br>";

                string AddendumText = "";
                string sCommentTable = "<table border='1'><tr><th>Date Added</th><th>Added By</th><th>Comment</th></tr>";

                DataView dvComments = new DataView(dtComments, "", "DateAdded desc", DataViewRowState.CurrentRows);
                foreach (DataRowView dRow in dvComments)
                {
                    AddendumText = dRow["Addendum"].ToString();

                    sCommentTable += "<tr><td>";

                    DateTime dtTemp;
                    if (DateTime.TryParse(dRow["DateAdded"].ToString(), out dtTemp))
                        sCommentTable += dtTemp.ToShortDateString();

                    sCommentTable += "</td><td>" + dRow["UserName"].ToString() + "</td><td>" + dRow["StaffComments"].ToString() + "</td></tr>";
                }

                sCommentTable += "</table>";

                sBody += "The original addendum was:<br>" + AddendumText.Replace("\n", "<br>") + "<br><br>";
                sBody += "The comments for the addendum are newest first:<br><br>";

                sBody += sCommentTable;

                Classes.cEmailMessageService cEMS = new Classes.cEmailMessageService();
                cEMS.SendMail(sSubject, sBody, hidNotificationEMail.Value, "", "", "CharHistoryStaffCommentsAddendum", Session["Username"].ToString());
            }
        }

        #region StaffComment

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            pnlCommentSection.Visible = true;
            string uName = "";
            int uID = 0;

            if (Session["Username"] != null)
                uName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out uID);

            Classes.cPlayer PLDemography = new Classes.cPlayer(uID, uName);
            string pict = PLDemography.UserPhoto;
            imgPicture.Attributes["onerror"] = "this.src='~/img/BlankProfile.png';";
            imgStaffPicture.ImageUrl = pict;

            btnAddComment.Visible = false;
        }

        protected void btnSaveNewComment_Click(object sender, EventArgs e)
        {
            if (tbNewComment.Text.Length > 0)
            {
                SortedList sParams = new SortedList();
                sParams.Add("@UserID", Session["UserID"]);
                sParams.Add("@CharacterID", hidCharacterID.Value);
                sParams.Add("@CharacterHistoryStaffCommentID", "-1");
                sParams.Add("@CommenterID", Session["UserID"]);
                sParams.Add("@StaffComments", tbNewComment.Text.Trim());

                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                Classes.cUtilities.LoadDataTable("uspInsUpdCHCharacterHistoryStaffComments", sParams, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

                sParams = new SortedList();
                sParams.Add("@CharacterID", hidCharacterID.Value);

                _dsHistory = Classes.cUtilities.LoadDataSet("uspGetCharacterHistory", sParams, "LARPortal", Session["UserName"].ToString(), lsRoutineName);
                SendStaffCommentEMail(_dsHistory.Tables[STAFFCOMMENTS]);
            }
            pnlCommentSection.Visible = false;
            btnAddComment.Visible = true;
        }

        protected void btnCancelComment_Click(object sender, EventArgs e)
        {
            pnlCommentSection.Visible = false;
            btnAddComment.Visible = true;
        }

        protected void SendStaffCommentEMail(DataTable dtComments)
        {
            DateTime dtTemp;
            if (hidCharacterID.Value.Length > 0)
            {
                string sSubject = Session["UserName"].ToString() + " has added a comment to a character history.";
                string sBody = Session["UserName"].ToString() + " has added a comment to a character history for " + hidPlayerName.Value + "<br><br>";

                string sCommentTable = "<table border='1'><tr><th>Date Added</th><th>Added By</th><th>Comment</th></tr>";

                DataView dvComments = new DataView(dtComments, "", "DateAdded desc", DataViewRowState.CurrentRows);
                foreach (DataRowView dRow in dvComments)
                {
                    sCommentTable += "<tr><td>";

                    if (DateTime.TryParse(dRow["DateAdded"].ToString(), out dtTemp))
                        sCommentTable += dtTemp.ToShortDateString();

                    sCommentTable += "</td><td>" + dRow["UserName"].ToString() + "</td><td>" + dRow["StaffComments"].ToString() + "</td></tr>";
                }

                sCommentTable += "</table>";
                sBody += sCommentTable;

                Classes.cEmailMessageService cEMS = new Classes.cEmailMessageService();
                cEMS.SendMail(sSubject, sBody, hidNotificationEMail.Value, "", "", "CharHistory Staff Comments", Session["Username"].ToString());
            }
        }

        #endregion

        protected void btnReject_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            int iCharacterID = -1;
            int iTemp;
            if (int.TryParse(hidCharacterID.Value, out iTemp))
                iCharacterID = iTemp;

            SortedList sParams = new SortedList();
            sParams.Add("@UserID", Session["UserID"].ToString());
            sParams.Add("@CharacterID", iCharacterID);

            sParams.Add("@DateHistoryApproved", DateTime.Now);

            Classes.cUtilities.PerformNonQuery("uspInsUpdCHCharacters", sParams, "LARPortal", Session["UserName"].ToString());
            Session["UpdateHistoryMessage"] = "alert('The character history has been approved.');";

            Classes.cPoints Points = new Classes.cPoints();
            int UserID = 0;
            int CampaignPlayerID = 0;
            int CharacterID = 0;
            int CampaignCPOpportunityDefaultID = 0;
            int CampaignID = 0;
            double CPAwarded = 0.0;

            int.TryParse(Session["UserID"].ToString(), out UserID);
            int.TryParse(hidCampaignPlayerID.Value, out CampaignPlayerID);
            int.TryParse(hidCharacterID.Value, out CharacterID);
            int.TryParse(hidCampaignCPOpportunityDefaultID.Value, out CampaignCPOpportunityDefaultID);
            int.TryParse(hidCampaignID.Value, out CampaignID);
            double.TryParse(tbCPAwarded.Text, out CPAwarded);

            DateTime dtDateSubmitted;
            if (!DateTime.TryParse(hidSubmitDate.Value, out dtDateSubmitted))
                dtDateSubmitted = DateTime.Now;

            Classes.cUser User = new Classes.cUser(Session["UserName"].ToString(), "PasswordNotNeeded");
            string sSubject = "Character history for " + hidCharacterAKA.Value + " had been approved.";

            string sBody = "The staff " + hidCampaignName.Value + " has approved the character history for " + hidCharacterAKA.Value + "<br><br>" +
                "You have been awarded " + CPAwarded.ToString() + " CP.<br><br>Character History:<br><br>" +
                ckHistory.Text;
            string sEmailToSendTo = hidEmail.Value;
            Classes.cEmailMessageService cEMS = new Classes.cEmailMessageService();
            cEMS.SendMail(sSubject, sBody, sEmailToSendTo, "", "", "CharacterHistory", Session["Username"].ToString());

            Points.AssignHistoryPoints(UserID, CampaignPlayerID, CharacterID, CampaignCPOpportunityDefaultID, CampaignID, CPAwarded, dtDateSubmitted);

            Response.Redirect("ApprovalList.aspx", true);
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            SortedList sParams = new SortedList();
            sParams.Add("@UserID", Session["UserID"].ToString());
            sParams.Add("@CharacterID", hidCharacterID.Value);
            sParams.Add("@ClearHistorySubmitted", true);

            Classes.cUtilities.PerformNonQuery("uspInsUpdCHCharacters", sParams, "LARPortal", Session["UserName"].ToString());
            Session["UpdateHistoryMessage"] = "alert('The character history has been sent back to the user.');";

            Classes.cUser User = new Classes.cUser(Session["UserName"].ToString(), "PasswordNotNeeded");
            string sSubject = "Character history for " + hidCharacterAKA.Value + " needs revision";
            Classes.cEmailMessageService cEMS = new Classes.cEmailMessageService();
            cEMS.SendMail(sSubject, ckHistory.Text, hidEmail.Value, "", "", "CharacterHistory", Session["Username"].ToString());

            Response.Redirect("ApprovalList.aspx", true);
        }
    }
}