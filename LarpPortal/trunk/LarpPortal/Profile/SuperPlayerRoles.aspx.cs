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
    public partial class SuperPlayerRoles : System.Web.UI.Page
    {
        private string _UserName = "";
        private int _UserID = 0;

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
                DataTable dtUsers = cUtilities.LoadDataTable("uspGetUsers", sParams, "LARPortal", _UserName, lsRoutineName);
                DataView dvUsers = new DataView(dtUsers, "", "loginUserName", DataViewRowState.CurrentRows);
                ddlPlayer.DataSource = dvUsers;
                ddlPlayer.DataTextField = "loginUserName";
                ddlPlayer.DataValueField = "UserID";
                ddlPlayer.DataBind();
                ddlPlayer_SelectedIndexChanged(null, null);
            }
        }

        protected void ddlPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            Classes.cUserCampaigns CampaignChoices = new Classes.cUserCampaigns();
            if (!string.IsNullOrEmpty(ddlPlayer.SelectedValue))
            {
                int iPlayerID;
                if (int.TryParse(ddlPlayer.SelectedValue, out iPlayerID))
                {
                    CampaignChoices.Load(iPlayerID);
                    DataView dvList = new DataView(cUtilities.CreateDataTable(CampaignChoices.lsUserCampaigns), "", "CampaignName", DataViewRowState.CurrentRows);

                    ddlCampaign.DataTextField = "CampaignName";
                    ddlCampaign.DataValueField = "CampaignID";
                    ddlCampaign.DataSource = dvList;
                    ddlCampaign.DataBind();
                    if (ddlCampaign.Items.Count > 0)
                    {
                        ddlCampaign.ClearSelection();
                        ddlCampaign.Items[0].Selected = true;
                        ddlCampaign_SelectedIndexChanged(null, null);
                    }
                }
            }
        }

        protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            SortedList sParams = new SortedList();
            sParams.Add("@UserID", ddlPlayer.SelectedValue);
            sParams.Add("@CampaignID", ddlCampaign.SelectedValue);
            DataSet dsRoles = cUtilities.LoadDataSet("uspGetPlayerRoles", sParams, "LARPortal", _UserName, lsRoutineName);

            cbxRolesCamp1.Items.Clear();
            cbxRolesCamp2.Items.Clear();
            cbxRolesDB.Items.Clear();
            cbxRolesTeam.Items.Clear();

            int iCount = 0;
            int iNumCampRoles = new DataView(dsRoles.Tables[1], "RoleTier = 'Campaign'", "RoleDescription", DataViewRowState.CurrentRows).Count;

            DataView dvFullRoleList = new DataView(dsRoles.Tables[1], "", "RoleDescription", DataViewRowState.CurrentRows);
            foreach (DataRowView dRow in dvFullRoleList)
            {
                ListItem newRole = new ListItem(dRow["RoleDescription"].ToString(), dRow["RoleID"].ToString());
                newRole.Attributes.Add("Title", dRow["Comments"].ToString());

                DataView dvPlayerRole = new DataView(dsRoles.Tables[0], "RoleID = " + dRow["RoleID"].ToString(), "", DataViewRowState.CurrentRows);
                if (dvPlayerRole.Count > 0)
                {
                    newRole.Selected = true;
                    // Means Role is there. Add an attribute to checkbox that tells what the record number is.
                    //newRole.Attributes.Add("CampaignPlayerRoleID", dvPlayerRole[0]["CampaignPlayerRoleID"].ToString());
                }
                else
                {
                    newRole.Selected = false;
                    //newRole.Attributes.Add("CampaignPlayerRoleID", "-1");
                }

                switch (dRow["RoleTier"].ToString().ToUpper())
                {
                    case "CAMPAIGN":
                        if (iCount++ < ((iNumCampRoles / 2) + 1))
                            cbxRolesCamp1.Items.Add(newRole);
                        else
                            cbxRolesCamp2.Items.Add(newRole);
                        break;

                    case "DATABASE":
                        cbxRolesDB.Items.Add(newRole);
                        break;

                    default:
                        cbxRolesTeam.Items.Add(newRole);
                        break;
                }
            }
            hidCampaignPlayerID.Value = "";
                if (dsRoles.Tables[0].Rows.Count > 0)
                    hidCampaignPlayerID.Value = dsRoles.Tables[0].Rows[0]["CampaignPlayerID"].ToString();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hidCampaignPlayerID.Value.Length > 0)
            {
                try
                {
                    MethodBase lmth = MethodBase.GetCurrentMethod();
                    string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                    SortedList sParams = new SortedList();
                    sParams.Add("@UserID", ddlPlayer.SelectedValue);
                    sParams.Add("@CampaignID", ddlCampaign.SelectedValue);
                    DataSet dsRoles = cUtilities.LoadDataSet("uspGetPlayerRoles", sParams, "LARPortal", _UserName, lsRoutineName);

                    foreach (ListItem Role in cbxRolesCamp1.Items)
                        ProcessRoles(dsRoles.Tables[0], Role);
                    foreach (ListItem Role in cbxRolesCamp2.Items)
                        ProcessRoles(dsRoles.Tables[0], Role);
                    foreach (ListItem Role in cbxRolesDB.Items)
                        ProcessRoles(dsRoles.Tables[0], Role);
                    foreach (ListItem Role in cbxRolesTeam.Items)
                        ProcessRoles(dsRoles.Tables[0], Role);
                }
                catch
                {
                }

                lblmodalMessage.Text = "The player has been updated.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
            }
        }


        private void ProcessRoles(DataTable UserRoles, ListItem liRole)
        {
            DataView dvUserRole = new DataView(UserRoles, "RoleID = " + liRole.Value, "", DataViewRowState.CurrentRows);
            string CampRoleID = "-1";
            if (dvUserRole.Count > 0)
                CampRoleID = dvUserRole[0]["CampaignPlayerRoleID"].ToString();

            if (liRole.Selected)
            {
                SortedList sParams = new SortedList();
                sParams.Add("@UserID", _UserID);
                sParams.Add("@CampaignPlayerRoleID", CampRoleID);
                if (CampRoleID == "-1")
                {
                    sParams.Add("@CampaignPlayerID", hidCampaignPlayerID.Value);
                    sParams.Add("@RoleID", liRole.Value);
                }
                sParams.Add("@Comments", "Updated by " + _UserName);
                cUtilities.PerformNonQuery("uspInsUpdCMCampaignPlayerRoles", sParams, "LARPortal", _UserName);
            }
            else
            {
                if (CampRoleID != "-1")
                {
                    SortedList sParams = new SortedList();
                    sParams.Add("@UserID", _UserID);
                    sParams.Add("@RecordID", CampRoleID);
                    cUtilities.PerformNonQuery("uspDelCMCampaignPlayerRoles", sParams, "LARPortal", _UserName);
                }
            }
        }


    }
}