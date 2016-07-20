using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

namespace LarpPortal.Events
{
    public partial class RegistrationHousing : System.Web.UI.Page
    {
        public bool _Reload = false;
        public DataTable _dtRegStatus = new DataTable();
        public DataTable _dtCampaignHousing = new DataTable();
        public DataTable _dtCampaignPaymentTypes = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortField"] = "PlayerName";
                ViewState["SortDir"] = "asc";
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if ((!IsPostBack) || (_Reload))
            {
                SortedList sParams = new SortedList();
                sParams.Add("@CampaignID", Session["CampaignID"].ToString());
                DataTable dtEvents = Classes.cUtilities.LoadDataTable("uspGetCampaignEvents", sParams, "LARPortal", Session["UserName"].ToString(), "RegistrationApproval.Page_PreRender");

                DataView dvEvents = new DataView(dtEvents, "", "StartDate", DataViewRowState.CurrentRows);

                if (dtEvents.Columns["DisplayValue"] == null)
                    dtEvents.Columns.Add("DisplayValue", typeof(string));

                DateTime dtMostReventEvent = DateTime.MaxValue;
                string sMostRecentEventID = "";

                foreach (DataRow dRow in dtEvents.Rows)
                {
                    DateTime dtTemp;
                    if (DateTime.TryParse(dRow["StartDate"].ToString(), out dtTemp))
                    {
                        dRow["DisplayValue"] = dRow["EventName"].ToString() + " " + dtTemp.ToShortDateString();
                        if ((dtTemp > DateTime.Today) &&
                            (dtTemp <= dtMostReventEvent))
                        {
                            dtMostReventEvent = dtTemp;
                            sMostRecentEventID = dRow["EventID"].ToString();
                        }
                    }
                    else
                        dRow["DisplayValue"] = dRow["EventName"].ToString();
                }

                ddlEvent.DataSource = dvEvents;
                ddlEvent.DataTextField = "DisplayValue";
                ddlEvent.DataValueField = "EventID";
                ddlEvent.DataBind();

                if (sMostRecentEventID != "")
                {
                    ddlEvent.ClearSelection();
                    foreach (ListItem li in ddlEvent.Items)
                        if (li.Value == sMostRecentEventID)
                            li.Selected = true;
                }
                ddlEvent_SelectedIndexChanged(null, null);
            }
        }

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateGrid();
        }


        private void PopulateGrid()
        {
            SortedList sParams = new SortedList();
            sParams.Add("@EventID", ddlEvent.SelectedValue);
            DataSet dsRegistrations = Classes.cUtilities.LoadDataSet("uspGetEventRegistrations", sParams, "LARPortal", Session["UserName"].ToString(), "RegistrationApproval.ddlEvent_SelectedIndexChanged");

            foreach (DataRow dEventInfo in dsRegistrations.Tables[0].Rows)
            {
                string sEventInfo = "";

                DateTime dtEventDate;
                if (DateTime.TryParse(dEventInfo["StartDate"].ToString(), out dtEventDate))
                    sEventInfo = "<b>Event Date: </b> " + dtEventDate.ToShortDateString();

                if (DateTime.TryParse(dEventInfo["EndDate"].ToString(), out dtEventDate))
                    sEventInfo += " - " + dtEventDate.ToShortDateString();

                sEventInfo += "&nbsp&nbsp&nbsp<b>Site: </b> " + dEventInfo["SiteName"].ToString() + "<br>";

                int iNumReg = dsRegistrations.Tables[1].Select("RegistrationStatus = 'Approved'").Length;
                int iNumNotReg = dsRegistrations.Tables[1].Select("RegistrationStatus <> 'Approved'").Length;

                sEventInfo += "<b>Number Registered: </b>" + dsRegistrations.Tables[1].Rows.Count.ToString() +
                    "&nbsp;&nbsp;<b>Number Approved: </b>" + iNumReg.ToString() +
                    "&nbsp;&nbsp;<b>Number Not Approved: </b>" + iNumNotReg.ToString();

                lblEventInfo.Text = sEventInfo;
            }

            if ( dsRegistrations.Tables[1].Columns["OrigHousing"] == null )
                dsRegistrations.Tables[1].Columns.Add("OrigHousing", typeof(string));

            foreach (DataRow dRow in dsRegistrations.Tables[1].Rows)
            {
                dRow["OrigHousing"] = dRow["AssignHousing"].ToString();
            }

            Session["HousingRegs"] = dsRegistrations.Tables[1];
            gvRegistrations.DataSource = dsRegistrations.Tables[1];
            gvRegistrations.DataBind();
            _Reload = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvRow in  gvRegistrations.Rows)
                {
                    HiddenField hidRegistrationID = (HiddenField) gvRow.FindControl("hidRegistrationID");
                    HiddenField hidOrigHousing = (HiddenField) gvRow.FindControl("hidOrigHousing");
                    TextBox tbAssignHousing = (TextBox) gvRow.FindControl("tbAssignHousing");
                    if ((hidRegistrationID != null) &&
                        (hidOrigHousing != null) &&
                        (tbAssignHousing != null))
                    {
                        if (hidOrigHousing.Value.Trim() != tbAssignHousing.Text.Trim())
                        {
                            SortedList sParams = new SortedList();
                            sParams.Add("@RegistrationID", hidRegistrationID.Value);
                            sParams.Add("@AssignHousing", tbAssignHousing.Text.Trim());
                            Classes.cUtilities.PerformNonQuery("uspInsUpdCMRegistrations", sParams, "LARPortal", Session["UserName"].ToString());
                            _Reload = true;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        protected void gvRegistrations_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtRegs = Session["HousingRegs"] as DataTable;

            foreach (GridViewRow i in gvRegistrations.Rows)
            {
                HiddenField hidRegistrationID = (HiddenField)i.FindControl("hidRegistrationID");
                TextBox tbAssignHousing = (TextBox)i.FindControl("tbAssignHousing");
                DataRow[] FoundRows = dtRegs.Select("RegistrationID = " + hidRegistrationID.Value);
                if (FoundRows.Length > 0)
                    FoundRows[0]["AssignHousing"] = tbAssignHousing.Text;
            }
            Session["HousingRegs"] = dtRegs;

            if (e.SortExpression == ViewState["SortField"].ToString())
            {
                if (ViewState["SortDir"].ToString() == "ASC")
                    ViewState["SortDir"] = "DESC";
                else
                    ViewState["SortDir"] = "ASC";
            }
            else
            {
                ViewState["SortField"] = e.SortExpression;
                ViewState["SortDir"] = "ASC";
            }

            string sSort = ViewState["SortField"].ToString() + " " + ViewState["SortDir"].ToString();

            DataView dvDisplay = new DataView(dtRegs, "", sSort, DataViewRowState.CurrentRows);
            gvRegistrations.DataSource = dvDisplay;
            gvRegistrations.DataBind();
        }
    }
}