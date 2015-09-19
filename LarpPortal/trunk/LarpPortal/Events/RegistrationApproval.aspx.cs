using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Events
{
    public partial class RegistrationApproval : System.Web.UI.Page
    {
        public bool _Reload = false;
        public DataTable _dtRegStatus = new DataTable();
        public DataTable _dtCampaignHousing = new DataTable();
        public DataTable _dtCampaignPaymentTypes = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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

            _dtRegStatus = dsRegistrations.Tables[2];
            _dtCampaignHousing = dsRegistrations.Tables[3];
            _dtCampaignPaymentTypes = dsRegistrations.Tables[4];

            if (dsRegistrations.Tables[1].Columns["DisplayPayment"] == null)
                dsRegistrations.Tables[1].Columns.Add("DisplayPayment", typeof(string));

            foreach (DataRow dRow in dsRegistrations.Tables[1].Rows)
            {
                if ((dRow["EventPaymentDescription"] == DBNull.Value) ||
                    (dRow["EventPaymentDescription"] == null))
                    dRow["DisplayPayment"] = "Not Paid";
                else
                {
                    double dPayment;
                    if (double.TryParse(dRow["EventPaymentAmount"].ToString(), out dPayment))
                        dRow["DisplayPayment"] = dRow["EventPaymentDescription"].ToString() + " / " + string.Format("{0:c}", dPayment);
                    else
                        dRow["DisplayPayment"] = "Not Paid";
                }
                if (dRow["RoleAlignmentDescription"].ToString() != "PC")
                    dRow["CharacterName"] = dRow["RoleAlignmentDescription"].ToString();
            }

            gvRegistrations.DataSource = dsRegistrations.Tables[1];
            gvRegistrations.DataBind();
            _Reload = false;
        }

        protected void gvRegistrations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRegistrations.EditIndex = -1;
            PopulateGrid();
        }

        protected void gvRegistrations_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRegistrations.EditIndex = e.NewEditIndex;
            PopulateGrid();
        }

        protected void gvRegistrations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                HiddenField hidRegistrationID = (HiddenField)gvRegistrations.Rows[e.RowIndex].FindControl("hidRegistrationID");
                TextBox tbPayment = (TextBox)gvRegistrations.Rows[e.RowIndex].FindControl("tbPayment");
                DropDownList ddlPaymentType = (DropDownList)gvRegistrations.Rows[e.RowIndex].FindControl("ddlPaymentType");
                Calendar calPaymentDate = (Calendar)gvRegistrations.Rows[e.RowIndex].FindControl("calPaymentDate");
                DropDownList ddlRegStatus = (DropDownList)gvRegistrations.Rows[e.RowIndex].FindControl("ddlRegStatus");

                if ((hidRegistrationID != null) &&
                    (tbPayment != null) &&
                    (ddlPaymentType != null) &&
                    (calPaymentDate != null) &&
                    (ddlRegStatus != null))
                {
                    SortedList sParam = new SortedList();
                    sParam.Add("@RegistrationID", hidRegistrationID.Value);
                    sParam.Add("@RegistrationStatus", ddlRegStatus.SelectedValue);
                    sParam.Add("@EventPaymentDate", calPaymentDate.SelectedDate);
                    sParam.Add("@EventPaymentTypeID", ddlPaymentType.SelectedValue);
                    sParam.Add("@EventPaymentAmount", tbPayment.Text);
                    Classes.cUtilities.PerformNonQuery("uspInsUpdCMRegistrations", sParam, "LARPortal", Session["UserName"].ToString());
                }
            }
            catch (Exception ex)
            {
                string l = ex.Message;
            }

            gvRegistrations.EditIndex = -1;
            PopulateGrid();
        }

        protected void gvRegistrations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dRow = e.Row.DataItem as DataRowView;

                    Calendar calPaymentDate = (Calendar)e.Row.FindControl("calPaymentDate");
                    if (calPaymentDate != null)
                    {
                        calPaymentDate.SelectedDate = DateTime.Today;
                        HiddenField hidPaymentDate = (HiddenField)e.Row.FindControl("hidPaymentDate");
                        if (hidPaymentDate != null)
                        {
                            DateTime dtPaymentDate;
                            if (DateTime.TryParse(hidPaymentDate.Value, out dtPaymentDate))
                            {
                                calPaymentDate.SelectedDate = dtPaymentDate;
                            }
                        }
                    }

                    DropDownList ddlPaymentType = (DropDownList)e.Row.FindControl("ddlPaymentType");
                    if (ddlPaymentType != null)
                    {
                        ddlPaymentType.DataSource = _dtCampaignPaymentTypes;
                        ddlPaymentType.DataTextField = "Description";
                        ddlPaymentType.DataValueField = "PaymentTypeID";
                        ddlPaymentType.DataBind();
                        HiddenField hidPaymentTypeID = (HiddenField)e.Row.FindControl("hidPaymentTypeID");
                        if (hidPaymentTypeID != null)
                            foreach (ListItem li in ddlPaymentType.Items)
                                if (li.Value == hidPaymentTypeID.Value)
                                    li.Selected = true;
                    }

                    DropDownList ddlRegStatus = (DropDownList)e.Row.FindControl("ddlRegStatus");
                    if (ddlRegStatus != null)
                    {
                        ddlRegStatus.DataSource = _dtRegStatus;
                        ddlRegStatus.DataTextField = "StatusName";
                        ddlRegStatus.DataValueField = "StatusID";
                        ddlRegStatus.DataBind();
                        HiddenField hidRegStatusID = (HiddenField)e.Row.FindControl("hidRegistrationStatusID");
                        if (hidRegStatusID != null)
                            foreach (ListItem li in ddlRegStatus.Items)
                                if (li.Value == hidRegStatusID.Value)
                                    li.Selected = true;
                    }
                }
            }

        }

        protected void gvRegistrations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string t = e.CommandName;
        }

        protected void btnApproveAll_Click(object sender, EventArgs e)
        {
            SortedList sParams = new SortedList();
            sParams.Add("@EventID", ddlEvent.SelectedValue);
            Classes.cUtilities.PerformNonQuery("uspEventApproveAllReg", sParams, "LARPortal", Session["UserName"].ToString());
        }
    }
}