using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.PELs
{
    public partial class PELApprovalList : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["UpdatePELMessage"] != null)
            {
                string jsString = Session["UpdatePELMessage"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", jsString, true);
                Session.Remove("UpdatePELMessage");
            }
            BindData();
        }

        protected DataTable BindData()
        {
            SortedList sParams = new SortedList();
            DataTable dtPELs = new DataTable();

            int iSessionID = 0;
            int.TryParse(Session["CampaignID"].ToString(), out iSessionID);
            sParams.Add("@CampaignID", iSessionID);

            dtPELs = Classes.cUtilities.LoadDataTable("uspGetPELsToApprove", sParams, "LARPortal", Session["UserName"].ToString(), "PELApprovalList.Page_PreRender");

            if (dtPELs.Columns["PELStatus"] == null)
                dtPELs.Columns.Add(new DataColumn("PELStatus", typeof(string)));
            if (dtPELs.Columns["ButtonText"] == null)
                dtPELs.Columns.Add(new DataColumn("ButtonText", typeof(string)));

            foreach (DataRow dRow in dtPELs.Rows)
            {
                if (dRow["DateApproved"] != System.DBNull.Value)
                {
                    dRow["PELStatus"] = "Approved";
                    dRow["ButtonText"] = "View";
                }
                else if (dRow["DateSubmitted"] != System.DBNull.Value)
                {
                    dRow["PELStatus"] = "Submitted";
                    dRow["ButtonText"] = "View";
                }
            }

            DataView dvPELs = new DataView(dtPELs, "", "PELStatus desc, DateSubmitted", DataViewRowState.CurrentRows);
            gvPELList.DataSource = dvPELs;
            gvPELList.DataBind();

            return dtPELs;
        }

        protected void gvPELList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sRegistrationID = e.CommandArgument.ToString();
            Response.Redirect("PELApprove.aspx?RegistrationID=" + sRegistrationID, false);
        }
    }
}