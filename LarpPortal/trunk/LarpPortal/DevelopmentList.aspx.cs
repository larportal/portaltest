using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class DevelopmentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortOrder"] = "ASC";
                ViewState["SortExp"] = "PrioritySequence";
                ViewState["Who"] = "ALL";
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //SortedList sParams = new SortedList();
            //if (cbIncludeCompleted.Checked)
            //    sParams.Add("@IncludeCompleted", "1");

            //DataTable dtErrors = Classes.cUtilities.LoadDataTable("uspGetDevelopmentList", sParams, "LARPortal", "DevelopmentList", "DevelopmentList.Page_PreRender",
            //    Classes.cUtilities.LoadDataTableCommandType.Text);
            //gvDevelopmentList.DataSource = dtErrors;
            //gvDevelopmentList.DataBind();

            //ddlWho.DataSource = new DataView(dtErrors.DefaultView.ToTable(true, "AssignedTo"), "", "AssignedTo", DataViewRowState.CurrentRows);
            //ddlWho.DataValueField = "AssignedTo";
            //ddlWho.DataTextField = "AssignedTo";
            //ddlWho.DataBind();

            BindData();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void gvDevelopmentList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sSortExp = e.SortExpression.ToUpper();
            if (sSortExp.ToUpper() == ViewState["SortExp"].ToString().ToUpper())
                ViewState["SortOrder"] = (ViewState["SortOrder"].ToString() == "ASC") ? "DESC" : "ASC";

            else
            {
                ViewState["SortExp"] = sSortExp;
                ViewState["SortOrder"] = "ASC";
            }
        }

        private void BindData()
        {
            SortedList sParams = new SortedList();
            if (cbIncludeCompleted.Checked)
                sParams.Add("@IncludeCompleted", "1");

            DataTable dtDevoList = Classes.cUtilities.LoadDataTable("uspGetDevelopmentList", sParams, "LARPortal", "DevelopmentList", "DevelopmentList.Page_PreRender",
                Classes.cUtilities.LoadDataTableCommandType.Text);

            string sFilter = "";

            if (ViewState["Who"].ToString().ToUpper() != "ALL")
                sFilter = "AssignedTo = '" + ViewState["Who"].ToString().ToUpper() + "'";

            DataView dvDevoList = new DataView(dtDevoList, sFilter, ViewState["SortExp"].ToString() + " " + ViewState["SortOrder"].ToString(), DataViewRowState.CurrentRows);
            gvDevelopmentList.DataSource = dvDevoList;
            gvDevelopmentList.DataBind();

            DataTable dtAssigned = dtDevoList.DefaultView.ToTable(true, "AssignedTo");
            DataRow NewRow = dtAssigned.NewRow();
            NewRow["AssignedTo"] = "All";
            dtAssigned.Rows.InsertAt(NewRow, 0);

            DataView dvAssignedTo = new DataView(dtAssigned, "", "AssignedTo", DataViewRowState.CurrentRows);
            ddlWho.DataSource = dvAssignedTo;
            ddlWho.DataValueField = "AssignedTo";
            ddlWho.DataTextField = "AssignedTo";
            ddlWho.DataBind();

            if (ViewState["Who"] != null)
                foreach (ListItem li in ddlWho.Items)
                    if (li.Value.ToUpper() == ViewState["Who"].ToString().ToUpper())
                        li.Selected = true;

            if (ddlWho.SelectedIndex < 0)
                ddlWho.SelectedIndex = 0;
        }

        protected void ddlWho_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Who"] = ddlWho.SelectedValue;
        }
    }
}