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
            if(!IsPostBack)
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
            _dtRegStatus = dsRegistrations.Tables[2];
            _dtCampaignHousing = dsRegistrations.Tables[3];
            _dtCampaignPaymentTypes = dsRegistrations.Tables[4];

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
                //CheckBox cbDisplayDesc = (CheckBox)gvSkills.Rows[e.RowIndex].FindControl("cbDisplayDesc");
                //CheckBox cbDisplayIncant = (CheckBox)gvSkills.Rows[e.RowIndex].FindControl("cbDisplayIncant");
                //TextBox tbPlayDesc = (TextBox)gvSkills.Rows[e.RowIndex].FindControl("tbPlayDesc");
                //TextBox tbPlayIncant = (TextBox)gvSkills.Rows[e.RowIndex].FindControl("tbPlayIncant");
                //HiddenField hidSkillID = (HiddenField)gvSkills.Rows[e.RowIndex].FindControl("hidSkillID");

                //if (Session["SkillList"] != null)
                //{
                //    DataTable dtSkills = Session["SkillList"] as DataTable;
                //    DataView dvRow = new DataView(dtSkills, "CharacterSkillsStandardID = " + hidSkillID.Value, "", DataViewRowState.CurrentRows);
                //    foreach (DataRowView dRow in dvRow)
                //    {
                //        if (cbDisplayDesc.Checked)
                //            dRow["CardDisplayDescription"] = true;
                //        else
                //            dRow["CardDisplayDescription"] = false;
                //        if (cbDisplayIncant.Checked)
                //            dRow["CardDisplayIncant"] = true;
                //        else
                //            dRow["CardDisplayIncant"] = false;
                //        dRow["PlayerDescription"] = tbPlayDesc.Text;
                //        dRow["PlayerIncant"] = tbPlayIncant.Text;
                //    }
                //    Session["SkillList"] = dtSkills;
                //}
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

                    DropDownList ddlHousing = (DropDownList)e.Row.FindControl("ddlHousing");
                    if (ddlHousing != null)
                    {
                        ddlHousing.DataSource = _dtCampaignHousing;
                        ddlHousing.DataTextField = "Description";
                        ddlHousing.DataValueField = "HousingTypeID";
                        ddlHousing.DataBind();
                        HiddenField hidCampaignHousingTypeID = (HiddenField)e.Row.FindControl("hidCampaignHousingTypeID");
                        if (hidCampaignHousingTypeID != null)
                            foreach (ListItem li in ddlHousing.Items)
                                if (li.Value == hidCampaignHousingTypeID.Value)
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
    }
}