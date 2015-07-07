using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace LarpPortal.General
{
    public partial class FAQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "FAQ";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int CategoryID = 0;
            string CategoryName = "";
            if(!IsPostBack)
            {
                if ((Request.QueryString["CategoryID"] == null))
                {
                    CategoryID = 1;  // Use the CategoryID (ID 1) as the default if we somehow get here with a NULL ID
                }
                else
                {
                    CategoryID = Int32.Parse(Request.QueryString["CategoryID"]);
                }
                if((Request.QueryString["CategoryName"] == null))
                {
                    CategoryName = "";
                    lblFAQHeader.Text = "FAQs - General";
                }
                else
                {
                    CategoryName = Request.QueryString["CategoryName"].ToString();
                    lblFAQHeader.Text = "FAQs - " + CategoryName;
                }
                BuildFAQTree(CategoryID);
            }
        }

        protected void BuildFAQTree(int FAQCategoryID)
        {
            int QuestionID = 0;
            string QuestionText = "";
            string AnswerText = "";
            int iTemp;
            TreeNode FAQQuestionNode;
            TreeNode FAQAnswerNode;
            tvFAQ.Nodes.Clear();
            string stStoredProc = "uspGetFAQQuestions";
            string stCallingMethod = "WhatsNew.aspx.Page_PreRender";
            DataTable dtFAQQuestions = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@FAQCategoryID", FAQCategoryID);
            dtFAQQuestions = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod);
            foreach (DataRow dRow in dtFAQQuestions.Rows)
            {
                if (int.TryParse(dRow["FAQID"].ToString(), out iTemp))
                    QuestionID = iTemp;
                QuestionText = dRow["Question"].ToString();
                AnswerText = dRow["Answer"].ToString();
                FAQQuestionNode = new TreeNode(QuestionText, QuestionID.ToString());
                FAQQuestionNode.Selected = false;
                FAQQuestionNode.NavigateUrl = "";
                tvFAQ.Nodes.Add(FAQQuestionNode);
                FAQAnswerNode = new TreeNode(AnswerText, QuestionID.ToString());
                FAQQuestionNode.ChildNodes.Add(FAQAnswerNode);
            }
            
        }

        protected void tvFAQ_SelectedNodeChanged(object sender, EventArgs e)
        {

        }
    }
}