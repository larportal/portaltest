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
    public partial class PELList : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            DataTable dtPELs = new DataTable();
            SortedList sParams = new SortedList();
            ///TODO: Put in userID in PELList.
            if (Session["CampaignID"] == null)
                return;

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

                ddlListToSelect.DataTextField = "EventName";
                ddlListToSelect.DataValueField = "EventID";
                ddlListToSelect.DataSource = dsEventInfo.Tables[0];
                ddlListToSelect.DataBind();

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
                    }
                    if (dsEventInfo.Tables[2] != null)
                    {
                        if (dsEventInfo.Tables[2].Rows.Count == 1)
                        {
                            ddlRoles.Visible = false;
                            lblRole.Text = dsEventInfo.Tables[2].Rows[0]["Description"].ToString();
                            lblRole.Visible = true;
                        }
                        else
                        {
                            ddlRoles.Visible = true;
                            ddlRoles.DataTextField = "RoleDescription";
                            ddlRoles.DataValueField = "RoleAlignmentID";
                            ddlRoles.DataSource = dsEventInfo.Tables[2];
                            ddlRoles.DataBind();
                            lblRole.Visible = false;
                        }
                    }
                }
                if (lblCharacter.Text == "NPC")
                {
                    trNPC.Visible = true;
                    trSendCPOther.Visible = true;
                    trPCStaff.Visible = false;
                }
                else
                {
                    trNPC.Visible = false;
                    trSendCPOther.Visible = false;
                    trPCStaff.Visible = true;
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

        protected void ddlListToSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bIncludeReg = false;

            SortedList sParams = new SortedList();
            sParams.Add("@EventID", ddlListToSelect.SelectedValue);
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
                bIncludeReg = true;
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
                //hidCampaignName.Value = dRow["CampaignName"].ToString();
                //hidCampaignEMail.Value = dRow["RegistrationNotificationEMail"].ToString();
            }

            //if ((DateTime.Now >= dtEventRegOpenDateTime) &&
            //    (DateTime.Now <= dtEventRegCloseDateTime))
            //{
            //    mvButtons.SetActiveView(vwRegisterButtons);
            //    mvEventScheduledOpen.SetActiveView(vwEventRegistrationOpen);
            //}
            //else
            //{
            //    mvButtons.SetActiveView(vwRSVPButtons);
            //    mvEventScheduledOpen.SetActiveView(vwEventRegistrationNotOpen);
            //}

            if (dsEventInfo.Tables["Character"].Rows.Count > 0)
            {
                ddlCharacterList.DataSource = dsEventInfo.Tables["Character"];
                ddlCharacterList.DataTextField = "CharacterAKA";
                ddlCharacterList.DataValueField = "CharacterID";
                ddlCharacterList.DataBind();
                ddlCharacterList.SelectedIndex = 0;
                ddlCharacterList.Visible = true;
                lblCharacter.Visible = false;

                //hidCharAKA.Value = dsEventInfo.Tables["Character"].Rows[0]["CharacterAKA"].ToString();
                //hidPlayerEMail.Value = dsEventInfo.Tables["Character"].Rows[0]["EMailAddress"].ToString();
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

            ddlRoles_SelectedIndexChanged(null, null);

            ddlSendToCampaign.ClearSelection();

            //hidRegistrationID.Value = "-1";

            foreach (DataRow dCharInfo in dsEventInfo.Tables["Character"].Rows)
            {
                lblCharacter.Text = dCharInfo["CharacterAKA"].ToString().Trim();
            }

            if (bIncludeReg)
            {
                foreach (DataRow dUserInfo in dsEventInfo.Tables["PlayerInfo"].Rows)
                {
                    //lblPlayerName.Text = dUserInfo["FirstName"].ToString().Trim() + " " + dUserInfo["LastName"].ToString().Trim();
                    //lblEMail.Text = dUserInfo["EMailAddress"].ToString().Trim();
                }

            }
        }


        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoles.SelectedItem.Text == "PC")
            {
                trPCStaff.Visible = true;
                trNPC.Visible = false;
                trSendCPOther.Visible = false;
            }
            else
            {
                trPCStaff.Visible = false;
                trNPC.Visible = true;
                trSendCPOther.Visible = true;


                int uID = 0;
                if (Session["UserID"] != null)
                {
                    if (int.TryParse(Session["UserID"].ToString(), out uID))
                    {
                        Classes.cUserCampaigns CampaignChoices = new Classes.cUserCampaigns();
                        CampaignChoices.Load(uID);
                        if (CampaignChoices.CountOfUserCampaigns == 0)
                            Response.Redirect("~/NoCurrentCampaignAssociations.aspx");

                        ddlSendToCampaign.DataTextField = "CampaignName";
                        ddlSendToCampaign.DataValueField = "CampaignID";
                        ddlSendToCampaign.DataSource = CampaignChoices.lsUserCampaigns;
                        ddlSendToCampaign.DataBind();
                        ddlSendToCampaign.Items.Add(new ListItem("Other", "-1"));
                    }
                }
            }
        }









    }
}