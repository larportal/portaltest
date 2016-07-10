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
            string sCommandName = e.CommandName.ToUpper();

            switch ( sCommandName )
            {
                case "EDIT":
                    Response.Redirect("EventEdit.aspx?EventID=" + sEventID, true);
                    break;

                case "CANCELLED":
                case "COMPLETED":
                    SortedList sGetStatusParams = new SortedList();
                    sGetStatusParams.Add("@StatusType", "Event");

                    DataTable dtStatus = Classes.cUtilities.LoadDataTable("[uspGetStatus]", sGetStatusParams, "LARPortal", Session["UserName"].ToString(), "EventList.gvPELList_RowCommand");
                    dtStatus.CaseSensitive = false;
                    //foreach (DataRow dStatus in dtStatus.Rows)
                    //    dStatus["StatusName"] = dStatus["StatusName"].ToString().ToUpper();
                    DataView dvStatus = new DataView(dtStatus, "StatusName = '" + sCommandName + "'", "", DataViewRowState.CurrentRows);
                    if ( dvStatus.Count > 0 )
                    {
                        SortedList sUpdateEvent = new SortedList();
                        sUpdateEvent.Add("@UserID", Session["UserID"].ToString());
                        sUpdateEvent.Add("@StatusID", dvStatus[0]["StatusID"].ToString());
                        sUpdateEvent.Add("@EventID", sEventID);         // Added JLB 6/7/2016
                        Classes.cUtilities.PerformNonQuery("uspInsUpdCMEvents", sUpdateEvent, "LARPortal", Session["UserName"].ToString());
                    }
                    break;

                case "DELETEEVENT":         // J.Bradshaw  Request # 1290    Was Delete which defaulted to the row delete command which isn't defined.
                    SortedList sDeleteParms = new SortedList();
                    sDeleteParms.Add("@RecordID", sEventID);
                    sDeleteParms.Add("@UserID", Session["UserID"].ToString());
                    Classes.cUtilities.PerformNonQuery("uspDelCMEvents", sDeleteParms, "LARPortal", Session["UserName"].ToString());
                    break;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("EventEdit.aspx?EventID=-1", true);
        }

        protected void gvPELList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dRow = (DataRowView)e.Row.DataItem;
                string sEventStatus = dRow["EventStatus"].ToString().ToUpper();

                if ((sEventStatus == "COMPLETED") ||
                     (sEventStatus == "CANCELLED"))
                {
                    Button btnEdit = (Button)e.Row.FindControl("btnEdit");
                    if (btnEdit != null)
                        btnEdit.Visible = false;
                    Button btnCancel = (Button)e.Row.FindControl("btnCancel");
                    if (btnCancel != null)
                        btnCancel.Visible = false;
                    Button btnComplete = (Button)e.Row.FindControl("btnComplete");
                    if (btnComplete != null)
                        btnComplete.Visible = false;
                }
            }
        }
    }
}