using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.PELs
{
    public partial class PELList : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            DataTable dtPELs = new DataTable();
            SortedList sParams = new SortedList();

            if (Session["CampaignID"] == null)
                return;

            ddlRoles.Attributes.Add("OnChange", "DisplayRoles(this);");

            sParams.Add("@UserID", Session["UserID"].ToString());
            sParams.Add("@CampaignID", Session["CampaignID"].ToString());

            dtPELs = Classes.cUtilities.LoadDataTable("uspGetPELsForUser", sParams, "LARPortal", Session["UserName"].ToString(), "PELList.Page_PreRender");

            if (dtPELs.Rows.Count > 0)
            {
                mvPELList.SetActiveView(vwPELList);

                if (dtPELs.Columns["PELStatus"] == null)
                    dtPELs.Columns.Add(new DataColumn("PELStatus", typeof(string)));
                if (dtPELs.Columns["ButtonText"] == null)
                    dtPELs.Columns.Add(new DataColumn("ButtonText", typeof(string)));
                if (dtPELs.Columns["DisplayAddendum"] == null)
                    dtPELs.Columns.Add(new DataColumn("DisplayAddendum", typeof(Boolean)));

                foreach (DataRow dRow in dtPELs.Rows)
                {
                    dRow["DisplayAddendum"] = false;
                    if (dRow["DateApproved"] != System.DBNull.Value)
                    {
                        dRow["PELStatus"] = "Approved";
                        dRow["ButtonText"] = "View";
                        dRow["DisplayAddendum"] = true;
                    }
                    else if (dRow["DateSubmitted"] != System.DBNull.Value)
                    {
                        dRow["PELStatus"] = "Submitted";
                        dRow["ButtonText"] = "View";
                        dRow["DisplayAddendum"] = true;
                    }
                    else if (dRow["DateStarted"] != System.DBNull.Value)
                    {
                        dRow["PELStatus"] = "Started";
                        dRow["ButtonText"] = "Edit";
                        dRow["DisplayAddendum"] = false;
                    }
                    else
                    {
                        dRow["PELStatus"] = "";
                        dRow["ButtonText"] = "Create";
                        dRow["DisplayAddendum"] = false;
                        int iPELID;
                        if (int.TryParse(dRow["RegistrationID"].ToString(), out iPELID))
                            dRow["PELID"] = iPELID * -1;
                    }
                }

                DataView dvPELs = new DataView(dtPELs, "", "ActualArrivalDate desc, ActualArrivalTime desc, RegistrationID desc", DataViewRowState.CurrentRows);
                gvPELList.DataSource = dvPELs;
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

            int iCampaignID;
            int iUserID;

            if ((int.TryParse(Session["CampaignID"].ToString(), out iCampaignID)) &&
                (int.TryParse(Session["UserID"].ToString(), out iUserID)))
            {
                sParams = new SortedList();
                sParams.Add("@CampaignID", iCampaignID.ToString());
                sParams.Add("@UserID", iUserID.ToString());
                DataSet dsEventInfo = Classes.cUtilities.LoadDataSet("uspGetMissedEvents", sParams, "LARPortal", Session["UserName"].ToString(), "PELList.GetMissedRegistrations");

                ddlMissedEvents.DataTextField = "EventName";
                ddlMissedEvents.DataValueField = "EventID";
                ddlMissedEvents.DataSource = dsEventInfo.Tables[0];
                ddlMissedEvents.DataBind();

                if (dsEventInfo.Tables[1] != null)
                {
                    if (dsEventInfo.Tables[1].Rows.Count == 1)
                    {
                        ddlCharacterList.Visible = false;
                        lblCharacter.Text = dsEventInfo.Tables[1].Rows[0]["CharacterAKA"].ToString();
                        lblCharacter.Visible = true;
                    }
                    else
                    {
                        ddlCharacterList.Visible = true;
                        ddlCharacterList.DataTextField = "CharacterAKA";
                        ddlCharacterList.DataValueField = "CharacterID";
                        ddlCharacterList.DataSource = dsEventInfo.Tables[1];
                        ddlCharacterList.DataBind();
                        lblCharacter.Visible = false;
                        if (ddlCharacterList.Items.Count > 0)
                        {
                            ddlCharacterList.ClearSelection();
                            ddlCharacterList.Items[0].Selected = true;
                        }
                    }
                    if (dsEventInfo.Tables[2] != null)
                    {
                        if (dsEventInfo.Tables[2].Rows.Count == 1)
                        {
                            ddlRoles.Visible = false;
                            lblRole.Text = dsEventInfo.Tables[2].Rows[0]["Description"].ToString();
                            lblRole.Visible = true;
                            if ((lblRole.Text.ToUpper() == "PC") || (lblRole.Text.ToUpper() == "STAFF"))
                            {
                                divPCStaff.Visible = true;
                                divNPC.Visible = false;
                                divSendOther.Visible = false;
                            }
                            else
                            {
                                divPCStaff.Visible = false;
                                divNPC.Visible = true;
                                divSendOther.Visible = true;
                            }
                        }
                        else
                        {
                            ddlRoles.Visible = true;
                            ddlRoles.DataTextField = "Description";
                            ddlRoles.DataValueField = "RoleAlignmentID";
                            ddlRoles.DataSource = dsEventInfo.Tables[2];
                            ddlRoles.DataBind();
                            lblRole.Visible = false;
                            if (ddlRoles.Items.Count > 0)
                            {
                                ddlRoles.ClearSelection();
                                ddlRoles.Items[0].Selected = true;

                                if ((ddlRoles.SelectedItem.Text.ToUpper() == "PC") || (ddlRoles.SelectedItem.Text.ToUpper() == "STAFF"))
                                {
                                    divPCStaff.Style.Add("display", "block");
                                    divNPC.Style.Add("display", "none");
                                    divSendOther.Style.Add("display", "none");
                                }
                                else
                                {
                                    divPCStaff.Style.Add("display", "none");
                                    divNPC.Style.Add("display", "block");
                                    divSendOther.Style.Add("display", "block");
                                }
                            }
                        }

                        if (dsEventInfo.Tables[3] != null)
                        {
                            ddlSendToCampaign.DataTextField = "CampaignName";
                            ddlSendToCampaign.DataValueField = "CampaignID";
                            ddlSendToCampaign.DataSource = dsEventInfo.Tables[3];
                            ddlSendToCampaign.DataBind();
                        }
                    }
                }
            }
        }

        protected void gvPELList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sRegistrationID = e.CommandArgument.ToString();
            if (e.CommandName.ToUpper() == "ADDENDUM")
                Response.Redirect("PELAddAddendum.aspx?RegistrationID=" + sRegistrationID, true);
            else
                Response.Redirect("PELEdit.aspx?RegistrationID=" + sRegistrationID, true);
        }

        protected void ddlMissedEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortedList sParams = new SortedList();
            sParams.Add("@EventID", ddlMissedEvents.SelectedValue);
            int iUserID;
            if (Session["UserID"] != null)
            {
                if (int.TryParse(Session["UserID"].ToString(), out iUserID))
                    sParams.Add("@UserID", iUserID);
            }
            DataSet dsEventInfo = Classes.cUtilities.LoadDataSet("uspGetEventInfo", sParams, "LARPortal", Session["UserName"].ToString(), "EventRegistration.gvEvents_RowCommand");

            dsEventInfo.Tables[0].TableName = "EventInfo";
            dsEventInfo.Tables[1].TableName = "Housing";
            dsEventInfo.Tables[2].TableName = "PaymentType";

            if (dsEventInfo.Tables.Count >= 5)
            {
                dsEventInfo.Tables[3].TableName = "Character";
                dsEventInfo.Tables[4].TableName = "Teams";
                dsEventInfo.Tables[5].TableName = "Registration";
                dsEventInfo.Tables[6].TableName = "RolesForEvent";
                dsEventInfo.Tables[7].TableName = "RegistrationStatuses";
                dsEventInfo.Tables[8].TableName = "Meals";
                dsEventInfo.Tables[9].TableName = "PlayerInfo";
            }

            foreach (DataRow dRow in dsEventInfo.Tables["EventInfo"].Rows)
            {
                ViewState["EventStartDate"] = dRow["StartDate"].ToString();
                ViewState["EventStartTime"] = dRow["StartTime"].ToString();
                ViewState["EventEndDate"] = dRow["EndDate"].ToString();
                ViewState["EventEndTime"] = dRow["EndTime"].ToString();
            }

            if (dsEventInfo.Tables["Character"].Rows.Count > 0)
            {
                ddlCharacterList.DataSource = dsEventInfo.Tables["Character"];
                ddlCharacterList.DataTextField = "CharacterAKA";
                ddlCharacterList.DataValueField = "CharacterID";
                ddlCharacterList.DataBind();
                ddlCharacterList.SelectedIndex = 0;
                ddlCharacterList.Visible = true;
                lblCharacter.Visible = false;
            }

            if (dsEventInfo.Tables["Character"].Rows.Count == 1)
            {
                ddlCharacterList.Visible = false;
                lblCharacter.Visible = true;
                lblCharacter.Text = ddlCharacterList.Items[0].Text;
            }

            DataView dvJustRoleNames = new DataView(dsEventInfo.Tables["RolesForEvent"], "", "", DataViewRowState.CurrentRows);
            DataTable dtJustRoleNames = dvJustRoleNames.ToTable(true, "RoleAlignmentID", "Description");

            ddlRoles.DataSource = dtJustRoleNames;
            ddlRoles.DataTextField = "Description";
            ddlRoles.DataValueField = "RoleAlignmentID";
            ddlRoles.DataBind();

            if (dtJustRoleNames.Rows.Count == 1)
            {
                ddlRoles.Visible = false;
                lblRole.Text = ddlRoles.Items[0].Text;
                lblRole.Visible = true;
            }
            else
            {
                if (dtJustRoleNames.Rows.Count > 1)
                {
                    lblRole.Visible = false;
                    ddlRoles.Visible = true;
                    ddlRoles.Items[0].Selected = true;
                }
            }

            ddlSendToCampaign.ClearSelection();

            foreach (DataRow dCharInfo in dsEventInfo.Tables["Character"].Rows)
            {
                lblCharacter.Text = dCharInfo["CharacterAKA"].ToString().Trim();
            }
        }

        protected void btnCloseRegisterForEvent_Click(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            SortedList sParam = new SortedList();
            int iRegStatus = -1;

            string sSQL = "select StatusID, StatusName from MDBStatus " +
                "where StatusType like 'Registration' " +
                    "and StatusName = 'Added Post Event'" +
                    "and DateDeleted is null";
            DataTable dtRegStatus = Classes.cUtilities.LoadDataTable(sSQL, sParam, "LARPortal", Session["UserName"].ToString(), "EventRegistration.btnRegister_Command",
                Classes.cUtilities.LoadDataTableCommandType.Text);

            if (dtRegStatus.Rows.Count > 0)
                int.TryParse(dtRegStatus.Rows[0]["StatusID"].ToString(), out iRegStatus);

            sParam = new SortedList();

            int iRegistrationID = -1;
            int iRoleAlignment = 0;
            int iEventID = 0;
            int.TryParse(ddlRoles.SelectedValue, out iRoleAlignment);
            int.TryParse(ddlMissedEvents.SelectedValue, out iEventID);
            int iCharacterID = 0;
            int.TryParse(ddlCharacterList.SelectedValue, out iCharacterID);

            sParam.Add("@RegistrationID", iRegistrationID);
            sParam.Add("@UserID", Session["UserID"].ToString());
            sParam.Add("@EventID", iEventID);
            sParam.Add("@RoleAlignmentID", ddlRoles.SelectedValue);
            sParam.Add("@CharacterID", ddlCharacterList.SelectedValue);
            sParam.Add("@DateRegistered", DateTime.Now);
            if (ddlRoles.SelectedItem.Text != "PC")
                sParam.Add("@NPCCampaignID", ddlSendToCampaign.SelectedValue);

            sParam.Add("@RegistrationStatus", iRegStatus);
            sParam.Add("@PartialEvent", false);
            sParam.Add("@ExpectedArrivalDate", ViewState["EventStartDate"].ToString());
            sParam.Add("@ExpectedArrivalTime", ViewState["EventStartTime"].ToString());
            sParam.Add("@ExpectedDepartureDate", ViewState["EventEndDate"].ToString());
            sParam.Add("@ExpectedDepartureTime", ViewState["EventEndTime"].ToString());

            try
            {
                DataTable dtUser = Classes.cUtilities.LoadDataTable("uspRegisterForEvent", sParam, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

                foreach (DataRow dRegRecord in dtUser.Rows)
                {
                    iRegistrationID = 0;
                    int.TryParse(dRegRecord["RegistrationID"].ToString(), out iRegistrationID);
                    InsertCPOpportunity(iRoleAlignment, iCharacterID, iEventID, iRegistrationID);
                }

                lblMessage.Text = "You have been registered for the event.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                lblMessage.Text = "There was a problem registering for the event. LARPortal support has been notified.";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
            }
        }

        protected void btnCloseMessage_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
        }

        protected void InsertCPOpportunity(int RoleAlignment, int iCharacterID, int iEventID, int iRegistrationID)
        {
            int iCampaignID = 0;
            int iUserID = 0;
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out iUserID);
            if (Session["CampaignID"] != null)
                int.TryParse(Session["CampaignID"].ToString(), out iCampaignID);

            int iReasonID = 0;
            switch (ddlRoles.SelectedItem.Text)
            {
                case "PC":
                    iReasonID = 3;
                    break;
                case "NPC":
                    iReasonID = 1;
                    break;
                case "Staff":
                    RoleAlignment = 12;
                    break;
                default:
                    break;
            }

            Classes.cPoints cPoints = new Classes.cPoints();

            // Currently have no need to delete. The Opportunty should be there currently. Leaving in case we need it.
            // cPoints.DeleteRegistrationCPOpportunity(iUserID, iRegistrationID);

            cPoints.CreateRegistrationCPOpportunity(iUserID, iCampaignID, RoleAlignment, iCharacterID, iReasonID, iEventID, iRegistrationID);
        }
    }
}