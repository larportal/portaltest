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
            if (!IsPostBack)
            {
                BindData();
                ddlCampaignWithPELCount_SelectedIndexChanged(null, null);
            }
            if (Session["UpdatePELMessage"] != null)
            {
                string jsString = Session["UpdatePELMessage"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", jsString, true);
                Session.Remove("UpdatePELMessage");
            }
        }

        protected void ddlCampaignWithPELCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sValue = ddlCampaignWithPELCount.SelectedValue;
            Session["SessionToApprove"] = sValue;

            DataTable dtPELs = new DataTable();
            dtPELs = BindData();

            ddlCampaignWithPELCount.SelectedIndex = -1;
            foreach (ListItem li in ddlCampaignWithPELCount.Items)
            {
                if (sValue == li.Value)
                    li.Selected = true;
                else
                    li.Selected = false;
            }

            if (ddlCampaignWithPELCount.SelectedIndex == -1)
                if (ddlCampaignWithPELCount.Items.Count > 0)
                    ddlCampaignWithPELCount.SelectedIndex = 0;

            if (ddlCampaignWithPELCount.SelectedIndex >= 0)
            {
                string sFilter = "CampaignID = " + sValue + " and DateSubmitted is not null";
                //if (!cbDisplayApproved.Checked)
                    sFilter += " and DateApproved is null";
                DataView dvPELs = new DataView(dtPELs, sFilter, "", DataViewRowState.CurrentRows);
                gvPELList.DataSource = dvPELs;
                gvPELList.DataBind();
            }
        }

        protected DataTable BindData()
        {
            SortedList sParams = new SortedList();
            DataSet dsPELs = new DataSet();
            dsPELs = Classes.cUtilities.LoadDataSet("uspGetPELsToApprove", sParams, "LARPortal", Session["UserName"].ToString(), "PELApprovalList.Page_PreRender");

            ddlCampaignWithPELCount.DataSource = dsPELs.Tables[1];
            ddlCampaignWithPELCount.DataTextField = "DisplayValue";
            ddlCampaignWithPELCount.DataValueField = "CampaignID";
            ddlCampaignWithPELCount.DataBind();

            ddlCampaignWithPELCount.SelectedIndex = -1;
            if (Session["SessionToApprove"] != null)
            {
                foreach (ListItem li in ddlCampaignWithPELCount.Items)
                    if (li.Value == Session["SessionToApprove"].ToString())
                        li.Selected = true;
                    else
                        li.Selected = false;
            }

            if ((ddlCampaignWithPELCount.Items.Count > 0) && (ddlCampaignWithPELCount.SelectedIndex == -1 ))
            {
                ddlCampaignWithPELCount.Items[0].Selected = true;
            }

            DataTable dtPELs = dsPELs.Tables[0];

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
                else if (dRow["DateStarted"] != System.DBNull.Value)
                {
                    dRow["PELStatus"] = "Started";
                    dRow["ButtonText"] = "Edit";
                }
            }

            return dtPELs;
        }

        protected void gvPELList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sRegistrationID = e.CommandArgument.ToString();
            Response.Redirect("PELEdit.aspx?Approving=" + sRegistrationID, false);
        }
    }
}