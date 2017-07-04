using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Profile
{
    public partial class PlayerRoles : System.Web.UI.Page
    {
        private string _UserName = "";
        private int _UserID = 0;
        private bool _ReloadUser = false;
        private DataTable _dtRoles;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
            {
                int iUserID;
                if (int.TryParse(Session["UserID"].ToString(), out iUserID))
                    _UserID = iUserID;
            }
            btnCloseMessage.Attributes.Add("data-dismiss", "modal");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (!IsPostBack)
            {
                SortedList sParams = new SortedList();

                Classes.cUserCampaigns CampaignChoices = new Classes.cUserCampaigns();
                CampaignChoices.Load(_UserID);
                ddlUserCampaigns.DataTextField = "CampaignName";
                ddlUserCampaigns.DataValueField = "CampaignID";
                ddlUserCampaigns.DataSource = CampaignChoices.lsUserCampaigns;
                ddlUserCampaigns.DataBind();
                ddlUserCampaigns_SelectedIndexChanged(null, null);
            }
        }

        protected void ddlUserCampaigns_SelectedIndexChanged(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            SortedList sParams = new SortedList();
            sParams.Add("@UserID", _UserID);
            sParams.Add("@CampaignID", ddlUserCampaigns.SelectedValue);
            DataSet dsRoles = cUtilities.LoadDataSet("uspGetPlayerRoles", sParams, "LARPortal", _UserName, lsRoutineName);
            _dtRoles = dsRoles.Tables[0];

            DataView dv = new DataView(dsRoles.Tables[0]);
            dv.Sort = "DisplayGroup";
            DataTable distinctValues = dv.ToTable(true, "DisplayGroup");

            rptRoles.DataSource = distinctValues;
            rptRoles.DataBind();
        }

        protected void gvFullRoleList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[7].Text = "<div style='text-align: center;'>Give role to<br>" + hidDisplayName.Value + "</div>";
        }

        protected void rptRoles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView RptrRow = (DataRowView) e.Item.DataItem;
            DataView dv = new DataView(_dtRoles, "DisplayGroup = '" + RptrRow[0].ToString() + "'", "DisplayDescription", DataViewRowState.CurrentRows);
            string sContents = "";

            foreach (DataRowView dRow in dv)
            {
                sContents += "<li>" + dRow["RoleDescription"].ToString() + " - " + dRow["DisplayDescription"].ToString() + "</li>";
            }
            Label lblDesc = (Label)e.Item.FindControl("lblDesc");
            lblDesc.Text = sContents;
        }
    }
}