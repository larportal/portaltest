using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class PELEdit : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtQuestions = new DataTable();

                if (Request.QueryString["RegistrationID"] == null)
                    Response.Redirect("PELList.aspx", true);

                hidRegistrationID.Value = Request.QueryString["RegistrationID"];
                SortedList sParams = new SortedList();
                sParams.Add("@RegistrationID", hidRegistrationID.Value);

                dtQuestions = Classes.cUtilities.LoadDataTable("uspGetPELQuestions", sParams, "LARPortal", Session["UserName"].ToString(), "PELEdit.Page_PreRender");

                if (dtQuestions.Rows.Count > 0)
                {
                    int iTemp;
                    if (int.TryParse(dtQuestions.Rows[0]["PELID"].ToString(), out iTemp))
                        hidPELID.Value = iTemp.ToString();
                }

                rptQuestions.DataSource = dtQuestions;
                rptQuestions.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int iPELID = -1;
            int iTemp;
            if (int.TryParse(hidPELID.Value, out iTemp))
                iPELID = iTemp;

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
            }

            string jsString = "alert('The PEL has been saved.');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    "MyApplication",
                    jsString,
                    true);
            Response.Redirect("PELList.aspx", true);
        }

        protected void ProcessButton(object sender, CommandEventArgs e)
        {
            int iPELID = -1;
            int iTemp;
            if (int.TryParse(hidPELID.Value, out iTemp))
                iPELID = iTemp;

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
            }

            string jsString = "alert('The PEL has been saved.');";

            if (e.CommandName.ToUpper() == "SUBMIT")
            {
                SortedList sParams = new SortedList();
                sParams.Add("@UserID", Session["UserID"].ToString());
                sParams.Add("@PELID", iPELID);
                sParams.Add("@DateSubmitted", DateTime.Now);

                Classes.cUtilities.PerformNonQuery("uspInsUpdCMPELs", sParams, "LARPortal", Session["UserName"].ToString());
                jsString = "alert('The PEL has been saved and submitted.');";
            }

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    "MyApplication",
                    jsString,
                    true);
            Response.Redirect("PELList.aspx", true);
        }
    }
}