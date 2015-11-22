using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Events
{
    public partial class EventList : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            DataTable dtEvents = new DataTable();
            SortedList sParams = new SortedList();
            if (Session["CampaignID"] == null)
                return;

            sParams.Add("@CampaignID", Session["CampaignID"].ToString());

            dtEvents = Classes.cUtilities.LoadDataTable("uspGetEventsForCampaign", sParams, "LARPortal", Session["UserName"].ToString(), "PELList.Page_PreRender");

            if (dtEvents.Rows.Count > 0)
            {
                mvPELList.SetActiveView(vwPELList);

                DataView dvEvents = new DataView(dtEvents, "", "StartDateTime desc", DataViewRowState.CurrentRows);
                gvPELList.DataSource = dvEvents;
                gvPELList.DataBind();
            }
            else
            {
                mvPELList.SetActiveView(vwNoPELs);
                lblCampaignName.Text = Session["CampaignName"].ToString();
            }

            if (Session["UpdatePELMessage"] != null)
            {
                string jsString = Session["UpdatePELMessage"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", jsString, true);
                Session.Remove("UpdatePELMessage");
            }
        }

        protected void gvPELList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sEventID = e.CommandArgument.ToString();
            Response.Redirect("EventEdit.aspx?EventID=" + sEventID, true);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("EventEdit.aspx?EventID=-1", true);
        }
    }
}