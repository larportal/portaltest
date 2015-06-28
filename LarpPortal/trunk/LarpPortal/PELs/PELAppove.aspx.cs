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
    public partial class PELAppove : System.Web.UI.Page
    {
        public bool TextBoxEnabled = true;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (hidTextBoxEnabled.Value == "1")
                TextBoxEnabled = true;

            if (!IsPostBack)
            {
                DataTable dtQuestions = new DataTable();
                bool bUser = true;

                if ((Request.QueryString["RegistrationID"] == null) &&
                    (Request.QueryString["Approving"] == null))
                    Response.Redirect("PELList.aspx", true);
                if (Request.QueryString["RegistrationID"] != null)
                    hidRegistrationID.Value = Request.QueryString["RegistrationID"];
                else if (Request.QueryString["Approving"] != null)
                {
                    hidRegistrationID.Value = Request.QueryString["Approving"];
                    bUser = false;
                }
                else
                    Response.Redirect("PELList.aspx", true);

                SortedList sParams = new SortedList();
                sParams.Add("@RegistrationID", hidRegistrationID.Value);

                dtQuestions = Classes.cUtilities.LoadDataTable("uspGetPELQuestions", sParams, "LARPortal", Session["UserName"].ToString(), "PELEdit.Page_PreRender");

                string sEventInfo = "";
                if (dtQuestions.Rows.Count > 0)
                {
                    sEventInfo = "<b>Event: </b> " + dtQuestions.Rows[0]["EventDescription"].ToString();

                    DateTime dtEventDate;
                    if (DateTime.TryParse(dtQuestions.Rows[0]["EventStartDate"].ToString(), out dtEventDate))
                        sEventInfo += "&nbsp;&nbsp;<b>Event Date: </b> " + dtEventDate.ToShortDateString();

                    sEventInfo += "&nbsp;&nbsp;<b>Character: </b> " + dtQuestions.Rows[0]["CharacterAKA"].ToString();

                    if (!bUser)
                    {
                        sEventInfo += "&nbsp;&nbsp;<b>User: </b> ";
                        if (dtQuestions.Rows[0]["FirstName"].ToString().Trim().Length > 0)
                            sEventInfo += dtQuestions.Rows[0]["FirstName"].ToString().Trim() + " ";
                        if (dtQuestions.Rows[0]["MiddleName"].ToString().Trim().Length > 0)
                            sEventInfo += dtQuestions.Rows[0]["MiddleName"].ToString().Trim() + " ";
                        if (dtQuestions.Rows[0]["LastName"].ToString().Trim().Length > 0)
                            sEventInfo += dtQuestions.Rows[0]["LastName"].ToString().Trim();
                    }

                    lblEventInfo.Text = sEventInfo;

                    int iTemp;
                    if (int.TryParse(dtQuestions.Rows[0]["PELID"].ToString(), out iTemp))
                        hidPELID.Value = iTemp.ToString();
                    if (dtQuestions.Rows[0]["PELDateApproved"] != DBNull.Value)
                    {
                        btnSubmit.Visible = false;
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
                        }
                    }
                    else if (dtQuestions.Rows[0]["PELDateSubmitted"] != DBNull.Value)
                    {
                        btnSubmit.Visible = false;
                        if (bUser)
                        {
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
                            }
                        }
                        else
                        {
                            btnSave.Text = "Approve";
                            btnSave.CommandName = "Approve";
                            DateTime dtTemp;
                            pnlStaffComments.Visible = true;
                            divQuestions.Attributes.Add("style", "max-height: 400px; overflow-y: auto; margin-right: 10px;");
                            if (DateTime.TryParse(dtQuestions.Rows[0]["PELDateApproved"].ToString(), out dtTemp))
                            {
                                lblEditMessage.Visible = true;
                                lblEditMessage.Text = "<br>This PEL was approved on " + dtTemp.ToShortDateString();
                                TextBoxEnabled = false;
                                hidTextBoxEnabled.Value = "0";
                                pnlStaffComments.Visible = true;
                                double dCPAwarded = 0;
                                double.TryParse(dtQuestions.Rows[0]["CPAwarded"].ToString(), out dCPAwarded);
                                lblCPAwarded.Text = "For completing this PEL, this person was awarded " + String.Format("{0:0.##}", dCPAwarded) + " CP.";
                                mvCPAwarded.SetActiveView(vwCPAwardedDisplay);
                            }
                            else if (DateTime.TryParse(dtQuestions.Rows[0]["PELDateSubmitted"].ToString(), out dtTemp))
                            {
                                lblEditMessage.Visible = true;
                                lblEditMessage.Text = "<br>This PEL was submitted on " + dtTemp.ToShortDateString();
                                TextBoxEnabled = false;
                                hidTextBoxEnabled.Value = "0";
                            }
                        }
                    }
                }

                foreach (DataRow dRow in dtQuestions.Rows)
                {
                    dRow["Answer"] = dRow["Answer"].ToString().Replace("\n", "<br>");
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
                sParams.Add("@Comments", tbStaffComment.Text);
                sParams.Add("@DateApproved", DateTime.Now);

                Classes.cUtilities.PerformNonQuery("uspInsUpdCMPELs", sParams, "LARPortal", Session["UserName"].ToString());
                Session["UpdatePELMessage"] = "alert('The PEL has been approved.');";
            }

            if (Request.QueryString["Approving"] != null)
                Response.Redirect("PELApprovalList.aspx", true);
            else
                Response.Redirect("PELList.aspx", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Approving"] != null)
                Response.Redirect("PELApprovalList.aspx", true);
            else
                Response.Redirect("PELList.aspx", true);
        }
    }
}