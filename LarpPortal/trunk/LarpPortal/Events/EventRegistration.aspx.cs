﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

//    7/1/2016  JBradshaw  Now it will delete the opportunity when any button is pressed and add only when the person actually registers.

namespace LarpPortal.Events
{
    public partial class EventRegistration : System.Web.UI.Page
    {
        private bool _Reload = true;
        private int _UserID = 0;
        private string _UserName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            tbSelectedMeals.Attributes.Add("Placehold", "Select Meals");
            ddlFullEvent.Attributes.Add("onchange", "ddl_changed(this);");
            ddlSendToCampaign.Attributes.Add("onChange", "ddlSendToCampaign(this);");
            tbSelectedMeals.Attributes.Add("placeholder", "Click to select the meals you want.");
            ddlPaymentChoice.Attributes.Add("onChange", "enablePayNowButton(this);");
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            btnClose.Attributes.Add("data-dismiss", "modal");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            try
            {
                if ((Session["CampaignID"] != null) && (_Reload))
                {
                    int iCampaignID;
                    if (int.TryParse(Session["CampaignID"].ToString(), out iCampaignID))
                    {
                        SortedList sParams = new SortedList();

                        divEvents.Visible = false;
                        divNoEvents.Visible = false;
                        divNoPriv.Visible = false;

                        sParams.Add("@UserID", _UserID);
                        sParams.Add("@CampaignID", iCampaignID.ToString());
                        DataSet dsRoles = cUtilities.LoadDataSet("uspGetPlayerRoles", sParams, "LARPortal", _UserName, lsRoutineName);
                        if (new DataView(dsRoles.Tables[0], "CanRegisterForEvents = 1", "", DataViewRowState.CurrentRows).Count == 0)
                        {
                            divNoPriv.Visible = true;
                            return;
                        }

                        sParams = new SortedList();
                        sParams.Add("@CampaignID", iCampaignID.ToString());
                        DataSet dtEventInfo = Classes.cUtilities.LoadDataSet("uspGetEventInfo", sParams, "LARPortal", _UserName, "EventRegistration.gvEvents_RowCommand");

                        dtEventInfo.Tables[0].TableName = "EventInfo";
                        dtEventInfo.Tables[1].TableName = "Housing";
                        dtEventInfo.Tables[2].TableName = "PaymentType";

                        // Eventually the row filter needs to be     StatusName = 'Scheduled' and RegistrationOpenDateTime <= GetDate() and RegistrationCloseDateTime >= GetDate()
                        DataView dvEventInfo = new DataView(dtEventInfo.Tables["EventInfo"], "PELDeadlineDate > '" + System.DateTime.Today.ToShortDateString() + "'",    // "StatusName = 'Scheduled'",    // and RegistrationOpenDateTime > '" + System.DateTime.Today + "'",
                            "", DataViewRowState.CurrentRows);

                        if (dvEventInfo.Count == 0)
                        {
                            divNoEvents.Visible = true;
                            return;
                        }

                        divEvents.Visible = true;
                        divNoEvents.Visible = false;

                        //                    mvPlayerInfo.SetActiveView(vwPlayerInfo);

                        DataTable dtEventDates = dvEventInfo.ToTable(true, "StartDate", "EventID", "EventName");

                        // Could do this as a computed column - but I want to specify the format.
                        dtEventDates.Columns.Add("DisplayStartDate", typeof(string));
                        DateTime dtTemp;

                        foreach (DataRow dRow in dtEventDates.Rows)
                            if (DateTime.TryParse(dRow["StartDate"].ToString(), out dtTemp))
                                dRow["DisplayStartDate"] = dtTemp.ToString("MM/dd/yyyy") + " - " + dRow["EventName"].ToString();

                        DataView dvEventDate = new DataView(dtEventDates, "", "StartDate", DataViewRowState.CurrentRows);

                        ddlEventDate.DataSource = dvEventDate;
                        ddlEventDate.DataTextField = "DisplayStartDate";
                        ddlEventDate.DataValueField = "EventID";
                        ddlEventDate.DataBind();

                        int iEventID = 0;
                        dvEventDate = new DataView(dtEventDates, "StartDate > '" + DateTime.Today.ToShortDateString() + "'", "StartDate", DataViewRowState.CurrentRows);
                        if (dvEventDate.Count > 0)
                            if (int.TryParse(dvEventDate[0]["EventID"].ToString(), out iEventID))
                            {
                                try
                                {
                                    ddlEventDate.SelectedValue = iEventID.ToString();
                                }
                                catch
                                {
                                    // Nothing to do. Keep going.
                                }
                            }

                        if (ddlEventDate.Items.Count > 0)
                            ddlEventDate_SelectedIndexChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                string l = ex.Message;
            }
        }

        /// <summary>
        /// Send an email to both campaign owners and Larportal.
        /// </summary>
        protected void NotifyOfNewRegistration(bool bNewRegistrion)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["CampaignID"] == null)
                return;

            SortedList sParams = new SortedList();
            string strSubject = "";
            sParams.Add("@CampaignID", Session["CampaignID"].ToString());

            DataTable dtCampaignInfo = Classes.cUtilities.LoadDataTable("uspGetCampaignByCampaignID", sParams, "LARPortal", _UserName, lsRoutineName);

            if (dtCampaignInfo.Rows.Count > 0)
            {
                DataRow drCampInfo = dtCampaignInfo.Rows[0];

                strSubject = "";
                if (bNewRegistrion)
                    strSubject = "New " + drCampInfo["CampaignName"].ToString() + " event registration - " + lblPlayerName.Text;
                else
                    strSubject = "Change to registration for " + lblPlayerName.Text + " for Campaign " + drCampInfo["CampaignName"].ToString();

                string strBody;
                strBody = lblPlayerName.Text + " has just registered for the upcoming " + drCampInfo["CampaignName"].ToString() + " event.  <br>" +
                    "Email: " + hidPlayerEMail.Value + "<br>";

                if (hidCharAKA.Value.Length > 0)
                    strBody += "Character: " + ddlCharacterList.SelectedItem.Text + "<br>";
                //strBody += "Character: " + hidCharAKA.Value + "<br>";

                strBody += "Payment Method: " + ddlPaymentChoice.SelectedItem.Text + "<br>" +
                    "Player Comments: " + tbComments.Text + "<br>" +
                    "Housing Requests: " + tbReqstdHousing.Text + "<br>" +
                    "&nbsp;&nbsp;nbsp;" + tbComments.Text;

                string sCampaignEMail = drCampInfo["RegistrationNotificationEMail"].ToString();
                Classes.cEmailMessageService RegistrationEmail = new Classes.cEmailMessageService();

                try
                {
                    string sSendTo = "support@larportal.com";
                    if (!string.IsNullOrEmpty(sCampaignEMail))
                        sSendTo += ";" + sCampaignEMail;

                    RegistrationEmail.SendMail(strSubject, strBody, hidPlayerEMail.Value, "", sCampaignEMail, "Registration", _UserName);
                }
                catch (Exception)
                {
                    //lblUsernameISEmail.Text = "There was an issue. Please contact us at support@larportal.com for assistance.";
                    //lblUsernameISEmail.Visible = true;
                }
            }
        }



        /// <summary>
        /// Send an email to both campaign owners and Larportal of the registration being cancelled.
        /// </summary>
        protected void NotifyOfCancelRegistration()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["CampaignID"] == null)
                return;

            SortedList sParams = new SortedList();
            string strSubject = "";
            sParams.Add("@CampaignID", Session["CampaignID"].ToString());

            DataTable dtCampaignInfo = Classes.cUtilities.LoadDataTable("uspGetCampaignByCampaignID", sParams, "LARPortal", _UserName, lsRoutineName);

            if (dtCampaignInfo.Rows.Count > 0)
            {
                DataRow drCampInfo = dtCampaignInfo.Rows[0];

                strSubject = "";
                strSubject = "Cancellation of registration for " + lblPlayerName.Text + " for campaign " + drCampInfo["CampaignName"].ToString();

                string strBody;
                strBody = lblPlayerName.Text + " has just cancelled the registration for the upcoming " + drCampInfo["CampaignName"].ToString() + " event on " + lblEventStartDate.Text + ".<br>" +
                    "Email: " + hidPlayerEMail.Value + "<br>";

                if (hidCharAKA.Value.Length > 0)
                    strBody += "Character: " + hidCharAKA.Value + "<br>";

                strBody += "Payment Method: " + ddlPaymentChoice.SelectedItem.Text + "<br>" +
                    "Player Comments: " + tbComments.Text + "<br>" +
                    "Housing Requests: " + tbReqstdHousing.Text;

                string sCampaignEMail = drCampInfo["RegistrationNotificationEMail"].ToString();
                Classes.cEmailMessageService RegistrationEmail = new Classes.cEmailMessageService();

                try
                {
                    string sSendTo = "support@larportal.com";
                    if (!string.IsNullOrEmpty(sCampaignEMail))
                        sSendTo += ";" + sCampaignEMail;

                    RegistrationEmail.SendMail(strSubject, strBody, hidPlayerEMail.Value, "", sCampaignEMail, "Registration", _UserName);
                }
                catch (Exception)
                {
                    //lblUsernameISEmail.Text = "There was an issue. Please contact us at support@larportal.com for assistance.";
                    //lblUsernameISEmail.Visible = true;
                }
            }
        }

        protected void ddlEventDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bIncludeReg = false;

            hidRegistrationID.Value = "";
            hidCharacterID.Value = "";
            hidTeamMember.Value = "";
            hidCampaignName.Value = "";
            hidCampaignEMail.Value = "";
            hidCharAKA.Value = "";
            hidPlayerEMail.Value = "";
            hidRegistrationStatusID.Value = "";
            hidPayPalTypeID.Value = "";
            hidHasPCChar.Value = "";
            hidRegOpen.Value = "";
            hidRegClose.Value = "";
            hidEventClose.Value = "";
            hidPELClose.Value = "";
            hidCurrentRegStatus.Value = "";

            SortedList sParams = new SortedList();
            sParams.Add("@EventID", ddlEventDate.SelectedValue);
            sParams.Add("@UserID", _UserID);
            DataSet dsEventInfo = Classes.cUtilities.LoadDataSet("uspGetEventInfo", sParams, "LARPortal", _UserName, "EventRegistration.gvEvents_RowCommand");

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

            ViewState["Meals"] = dsEventInfo.Tables["Meals"];

            DateTime dtEventStartDateTime = DateTime.MaxValue;
            DateTime dtEventEndDateTime = DateTime.MaxValue;
            DateTime dtEventRegOpenDateTime = DateTime.MaxValue;
            DateTime dtEventRegCloseDateTime = DateTime.MaxValue;

            foreach (DataRow dRow in dsEventInfo.Tables["EventInfo"].Rows)
            {
                lblEventName.Text = dRow["EventName"].ToString();
                lblEventStatus.Text = dRow["StatusName"].ToString();
                lblEventDescription.Text = dRow["EventDescription"].ToString();
                lblInGameLocation.Text = dRow["IGEventLocation"].ToString();
                //                lblPaymentInstructions1.Text = dRow["PaymentInstructions"].ToString().Replace(Environment.NewLine, "<br>");
                lblPaymentInstructions2.Text = dRow["PaymentInstructions"].ToString().Replace(Environment.NewLine, "<br>");

                hidCampaignName.Value = dRow["CampaignName"].ToString();
                hidCampaignEMail.Value = dRow["RegistrationNotificationEMail"].ToString();

                lblSiteLocation.Text = dRow["SiteName"].ToString() + " " + dRow["SiteAddress1"].ToString() + " " + dRow["SiteCity"].ToString() + " " +
                    dRow["SiteStateID"].ToString() + ", " + dRow["SitePostalCode"].ToString();

                DateTime dtTemp;
                double dTemp;

                if (DateTime.TryParse(dRow["RegistrationOpenDateTime"].ToString(), out dtTemp))
                {
                    lblEventOpenDate.Text = string.Format("{0: MM/dd/yy hh:mm tt}", dtTemp);
                    hidRegOpen.Value = dtTemp.ToString();
                }
                else
                    hidRegOpen.Value = DateTime.MaxValue.ToString();

                if (DateTime.TryParse(dRow["RegistrationCloseDateTime"].ToString(), out dtTemp))
                {
                    lblEventCloseDate.Text = string.Format("{0: MM/dd/yy hh:mm tt}", dtTemp);
                    hidRegClose.Value = dtTemp.ToString();
                }
                else
                    hidRegClose.Value = DateTime.MaxValue.ToString();

                if (DateTime.TryParse(dRow["PELDeadlineDate"].ToString(), out dtTemp))
                    hidPELClose.Value = dtTemp.ToString();
                else
                    hidPELClose.Value = DateTime.MaxValue.ToString();

                if (DateTime.TryParse(dRow["PaymentDueDate"].ToString(), out dtTemp))
                    lblPaymentDue.Text = string.Format("{0: MM/dd/yy}", dtTemp);

                if (double.TryParse(dRow["PreregistrationPrice"].ToString(), out dTemp))
                    lblPreRegPrice.Text = dTemp.ToString("C");
                if (DateTime.TryParse(dRow["PreregistrationDeadline"].ToString(), out dtTemp))
                    lblPreRegDate.Text = string.Format("{0: MM/dd/yy}", dtTemp);
                if (double.TryParse(dRow["LateRegistrationPrice"].ToString(), out dTemp))
                    lblRegPrice.Text = string.Format("{0:C}", dTemp);
                if (DateTime.TryParse(dRow["StartDateTime"].ToString(), out dtTemp))
                {
                    lblEventStartDate.Text = string.Format("{0: MM/dd/yy hh:mm tt}", dtTemp);
                    dtEventStartDateTime = dtTemp;
                }
                if (DateTime.TryParse(dRow["EndDateTime"].ToString(), out dtTemp))
                {
                    lblEventEndDate.Text = string.Format("{0: MM/dd/yy hh:mm tt}", dtTemp);
                    dtEventEndDateTime = dtTemp;
                }
                ViewState["EventEndDateTime"] = dtTemp;

                if (double.TryParse(dRow["CheckinPrice"].ToString(), out dTemp))
                    lblDoorPrice.Text = dTemp.ToString("C");
                if (DateTime.TryParse(dRow["PELDeadlineDate"].ToString(), out dtTemp))
                    lblPELDueDate.Text = string.Format("{0: MM/dd/yy}", dtTemp);
                if (DateTime.TryParse(dRow["InfoSkillDeadlineDate"].ToString(), out dtTemp))
                    lblInfoSkillDueDate.Text = string.Format("{0: MM/dd/yy}", dtTemp);
                SetCheckDisplay(dRow["PCFoodService"], imgPCFoodService);
                SetCheckDisplay(dRow["NPCFoodService"], imgNPCFoodService);
                SetCheckDisplay(dRow["CookingFacilitiesAvailable"], imgCookingAllowed);
            }

            lblWhyRSVP.Visible = false;
            lblPastEvent.Visible = false;
            lblClosedToPC.Visible = false;

            //if (DateTime.Now > dtEventEndDateTime)
            //{
            //    mvButtons.SetActiveView(vwAlreadyHappened);
            //}
            //else
            //{
            //    if ((DateTime.Now >= dtEventRegOpenDateTime) &&
            //        (DateTime.Now <= dtEventRegCloseDateTime))
            //    {
            //        mvButtons.SetActiveView(vwRegisterButtons);
            //        mvEventScheduledOpen.SetActiveView(vwEventRegistrationOpen);
            //        lblWhyRSVP.Visible = true;
            //    }
            //    else
            //    {
            //        mvButtons.SetActiveView(vwRSVPButtons);
            //        mvEventScheduledOpen.SetActiveView(vwEventRegistrationNotOpen);
            //    }
            //}

            tbArriveDate.Text = dtEventStartDateTime.ToString("MM/dd/yyyy");
            tbArriveTime.Text = dtEventStartDateTime.ToString("hh:mm tt");
            tbDepartDate.Text = dtEventEndDateTime.ToString("MM/dd/yyyy");
            tbDepartTime.Text = dtEventEndDateTime.ToString("hh:mm tt");

            lblRegistrationStatus.Text = "Not Registered";

            if (dsEventInfo.Tables["Character"].Rows.Count > 0)
            {
                ddlCharacterList.DataSource = dsEventInfo.Tables["Character"];
                ddlCharacterList.DataTextField = "CharacterAKA";
                ddlCharacterList.DataValueField = "CharacterID";
                ddlCharacterList.DataBind();
                ddlCharacterList.SelectedIndex = 0;
                ddlCharacterList.Visible = true;
                lblCharacter.Visible = false;

                hidCharAKA.Value = dsEventInfo.Tables["Character"].Rows[0]["CharacterAKA"].ToString();
                hidPlayerEMail.Value = dsEventInfo.Tables["Character"].Rows[0]["EMailAddress"].ToString();
            }
            else
            {
                hidCharAKA.Value = "";
            }

            if (dsEventInfo.Tables["Character"].Rows.Count == 1)
            {
                ddlCharacterList.Visible = false;
                lblCharacter.Visible = true;
                lblCharacter.Text = ddlCharacterList.Items[0].Text;
            }

            if (dsEventInfo.Tables["Character"].Rows.Count == 0)
                hidHasPCChar.Value = "0";
            else
                hidHasPCChar.Value = "1";

            //ddlHousing.DataSource = new DataView(dsEventInfo.Tables["Housing"], "", "Description", DataViewRowState.CurrentRows);
            //ddlHousing.DataTextField = "Description";
            //ddlHousing.DataValueField = "HousingTypeID";
            //ddlHousing.DataBind();

            //ddlHousing.ClearSelection();
            //foreach (ListItem dHousing in ddlHousing.Items)
            //{
            //    if (dHousing.Text.ToUpper().Contains("TEAM ONLY"))
            //    {
            //        ddlHousing.ClearSelection();
            //        dHousing.Selected = true;
            //    }
            //}

            //            btnRegister.Text = "Register";

            DataView dvJustRoleNames = new DataView(dsEventInfo.Tables["RolesForEvent"], "", "", DataViewRowState.CurrentRows);
            DataTable dtJustRoleNames = dvJustRoleNames.ToTable(true, "RoleAlignmentID", "Description");

            ddlRoles.DataSource = dtJustRoleNames;
            ddlRoles.DataTextField = "Description";
            ddlRoles.DataValueField = "RoleAlignmentID";
            ddlRoles.DataBind();

            // Make the button to stay and be an NPC invisible. Only want to display this is the person has the NPC role.
            btnStayAsNPC.Visible = false;

            foreach (DataRow dRole in dtJustRoleNames.Rows)
            {
                if ((dRole["Description"].ToString().ToUpper().Contains("NPC")) ||
                    (dRole["Description"].ToString().ToUpper().Contains("STAFF")))
                {
                    btnStayAsNPC.Visible = true;
                }
            }

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
                    ddlRoles.ClearSelection();
                    lblRole.Visible = false;
                    ddlRoles.Visible = true;
                    ddlRoles.Items[0].Selected = true;
                }
            }

            ddlRoles_SelectedIndexChanged(null, null);

            ddlSendToCampaign.ClearSelection();

            hidRegistrationID.Value = "-1";
            btnPayNow.Visible = false;
            Session.Remove("RegistrationID");

            foreach (DataRow dCharInfo in dsEventInfo.Tables["Character"].Rows)
            {
                lblCharacter.Text = dCharInfo["CharacterAKA"].ToString().Trim();
            }

            hidCurrentRegStatus.Value = "";

            if (bIncludeReg)
            {
                foreach (DataRow dUserInfo in dsEventInfo.Tables["PlayerInfo"].Rows)
                {
                    lblPlayerName.Text = dUserInfo["FirstName"].ToString().Trim() + " " + dUserInfo["LastName"].ToString().Trim();
                    lblEMail.Text = dUserInfo["EMailAddress"].ToString().Trim();
                }

                if (dsEventInfo.Tables["Meals"].Rows.Count > 0)
                {
                    mvMenu.SetActiveView(vwFoodAvail);
                    cbMealList.DataSource = dsEventInfo.Tables["Meals"];
                    cbMealList.DataTextField = "MealDescription";
                    cbMealList.DataValueField = "EventMealID";
                    cbMealList.DataBind();
                    cbMealList.ClearSelection();
                    foreach (DataRow dMeal in dsEventInfo.Tables["Meals"].Rows)
                    {
                        if (dMeal["RegistrationMealsID"] != DBNull.Value)
                        {
                            ListItem li = cbMealList.Items.FindByValue(dMeal["EventMealID"].ToString());
                            if (li != null)
                                li.Selected = true;
                        }
                    }
                    cbFoodList_SelectedIndexChanged(null, null);
                }
                else
                    mvMenu.SetActiveView(vwNoFood);

                if (dsEventInfo.Tables["Teams"].Rows.Count > 0)
                {
                    ddlTeams.Visible = true;
                    ddlTeams.DataSource = dsEventInfo.Tables["Teams"];
                    ddlTeams.DataTextField = "TeamName";
                    ddlTeams.DataValueField = "TeamID";
                    ddlTeams.DataBind();
                    ddlTeams.ClearSelection();
                    ddlTeams.Items[0].Selected = true;
                    ddlTeams.Items.Insert(0, new ListItem("No Team", "-1"));
                    lblNoTeams.Visible = false;
                }
                else
                {
                    ddlTeams.Visible = false;
                    lblNoTeams.Visible = true;
                }

                ddlPaymentChoice.DataSource = dsEventInfo.Tables["PaymentType"];
                ddlPaymentChoice.DataTextField = "Description";
                ddlPaymentChoice.DataValueField = "PaymentTypeID";
                ddlPaymentChoice.DataBind();
                ddlPaymentChoice.Items.Insert(0, new ListItem("No Payment", "0"));
                ddlPaymentChoice.ClearSelection();                                  // JBradshaw 7/10/2016   Request #1288
                ddlPaymentChoice.Items[0].Selected = true;

                DateTime dtArrivalDateTime = DateTime.MinValue;
                DateTime dtDepartureDatetime = DateTime.MinValue;
                foreach (DataRow dReg in dsEventInfo.Tables["Registration"].Rows)
                {
                    hidRegistrationID.Value = dReg["RegistrationID"].ToString();
                    lblRegistrationStatus.Text = dReg["RegistrationStatus"].ToString();
                    hidRegistrationStatusID.Value = dReg["RegistrationStatusID"].ToString();
                    hidCurrentRegStatus.Value = dReg["RegistrationStatusID"].ToString();

                    tbReqstdHousing.Text = dReg["ReqstdHousing"].ToString();
                    lblReqstdHousing.Text = dReg["ReqstdHousing"].ToString();
                    lblAssignHousing.Text = dReg["AssignHousing"].ToString();

                    if (hidRegistrationID.Value != "-1")
                    {
                        Session["RegistrationID"] = hidRegistrationID.Value;
                        DataView dvPayPal = new DataView(dsEventInfo.Tables["PaymentType"], "IsPayPal = 1", "", DataViewRowState.CurrentRows);
                        if (dvPayPal.Count > 0)
                            hidPayPalTypeID.Value = dvPayPal[0]["PaymentTypeID"].ToString();
                        else
                            hidPayPalTypeID.Value = "";
                    }

                    //ddlHousing.ClearSelection();
                    //if (dReg["CampaignHousingTypeID"] != DBNull.Value)
                    //{
                    //    ListItem lHousing = ddlHousing.Items.FindByValue(dReg["CampaignHousingTypeID"].ToString());
                    //    if (lHousing != null)
                    //        lHousing.Selected = true;
                    //}
                    //if (ddlHousing.SelectedIndex < 0)
                    //{
                    //    if (ddlHousing.Items.Count > 0)
                    //        ddlHousing.Items[0].Selected = true;
                    //}

                    ddlPaymentChoice.ClearSelection();
                    if (dReg["EventPaymentTypeID"] != DBNull.Value)
                    {
                        ListItem lPaymentType = ddlPaymentChoice.Items.FindByValue(dReg["EventPaymentTypeID"].ToString());
                        if (lPaymentType != null)
                            lPaymentType.Selected = true;
                    }
                    if (ddlPaymentChoice.SelectedIndex < 0)
                        ddlPaymentChoice.Items[0].Selected = true;

                    btnPayNow.Visible = true;

                    if (ddlPaymentChoice.SelectedValue == hidPayPalTypeID.Value)
                        btnPayNow.Attributes.Add("display", "");
                    else
                        btnPayNow.Attributes.Add("display", "none");

                    //string sRegistration = dReg["RegistrationStatus"].ToString().ToUpper();
                    //if ((sRegistration == "APPROVED") ||
                    //    (sRegistration == "WAIT LIST") ||
                    //    (sRegistration == "WAITING TEAM APPROVAL") ||
                    //    (sRegistration == "REGISTERED BY OTHER"))
                    //{
                    //    btnRegister.Text = "Change Registration";
                    //    btnRegister.Width = Unit.Pixel(200);
                    //}

                    //if (sRegistration == "APPROVED")
                    //{
                    //    lblReqstdHousing.Visible = true;
                    //    tbReqstdHousing.Visible = false;
                    //}
                    //else
                    //{
                    lblReqstdHousing.Visible = false;
                    tbReqstdHousing.Visible = true;
                    //}

                    DateTime dtTemp;
                    if (DateTime.TryParse(dReg["EventPaymentDate"].ToString(), out dtTemp))
                        lblPaymentStatus.Text = "Paid";
                    else
                        lblPaymentStatus.Text = "Unpaid";


                    if (dReg["ExpectedArrivalDate"] != DBNull.Value)
                    {
                        try
                        {
                            DateTime dtTempDate = (DateTime)dReg["ExpectedArrivalDate"];
                            if (dReg["ExpectedArrivalTime"] != DBNull.Value)
                                dtTempDate += ((TimeSpan)(dReg["ExpectedArrivalTime"]));
                            else
                                dtTempDate += dtEventStartDateTime.TimeOfDay;
                            dtArrivalDateTime = dtTempDate; // Get this far - it's OK.
                        }
                        catch
                        {
                            // Just in case. Actually don't need to do anything here. 
                        }
                    }
                    if (dReg["ExpectedDepartureDate"] != DBNull.Value)
                    {
                        try
                        {
                            DateTime dtTempDate = (DateTime)dReg["ExpectedDepartureDate"];
                            if (dReg["ExpectedDepartureTime"] != DBNull.Value)
                                dtTempDate += (TimeSpan)dReg["ExpectedDepartureTime"];
                            else
                                dtTempDate += dtEventEndDateTime.TimeOfDay;
                            dtDepartureDatetime = dtTempDate;
                        }
                        catch
                        {
                            // Just in case. Actually don't need to do anything here. 
                        }
                    }

                    divFullEventNo.Style.Add("display", "none");

                    if ((dtArrivalDateTime != DateTime.MinValue) ||
                        (dtDepartureDatetime != DateTime.MinValue))
                    {
                        if ((dtArrivalDateTime != dtEventStartDateTime) ||
                            (dtDepartureDatetime != dtEventEndDateTime))
                        {
                            ListItem liNo = ddlFullEvent.Items.FindByValue("N");
                            if (liNo != null)
                            {
                                ddlFullEvent.ClearSelection();
                                liNo.Selected = true;
                            }
                            if (dtArrivalDateTime != DateTime.MinValue)
                            {
                                tbArriveDate.Text = dtArrivalDateTime.ToString("MM/dd/yyyy");
                                tbArriveTime.Text = dtArrivalDateTime.ToString("HH:mm");
                            }
                            if (dtDepartureDatetime != DateTime.MinValue)
                            {
                                tbDepartDate.Text = dtDepartureDatetime.ToString("MM/dd/yyyy");
                                tbDepartTime.Text = dtDepartureDatetime.ToString("HH:mm");
                            }

                            divFullEventNo.Style.Add("display", "block");
                        }
                    }

                    if (dReg["TeamID"].ToString() != "")
                    {
                        ddlTeams.ClearSelection();
                        foreach (ListItem li in ddlTeams.Items)
                            if (li.Value == dReg["TeamID"].ToString())
                                li.Selected = true;
                    }
                    ddlRoles_SelectedIndexChanged(null, null);
                    if (dReg["RoleAlignmentID"].ToString() != "")
                    {
                        ddlRoles.ClearSelection();
                        bool bSelectionFound = false;

                        foreach (ListItem li in ddlRoles.Items)
                            if (li.Value == dReg["RoleAlignmentID"].ToString())
                            {
                                ddlRoles.ClearSelection();
                                li.Selected = true;
                                bSelectionFound = true;
                            }
                        ddlRoles_SelectedIndexChanged(null, null);
                        if (bSelectionFound)
                        {
                            if (ddlRoles.SelectedItem.Text != "PC")
                            {
                                if (dReg["NPCCampaignID"] != DBNull.Value)
                                    if (dReg["NPCCampaignID"].ToString() != "")
                                    {
                                        foreach (ListItem liItem in ddlSendToCampaign.Items)
                                            if (liItem.Value == dReg["NPCCampaignID"].ToString())
                                            {
                                                ddlSendToCampaign.ClearSelection();
                                                liItem.Selected = true;
                                            }
                                    }
                            }
                        }
                    }
                    if (dReg["CharacterID"].ToString() != "")
                    {
                        ddlCharacterList.ClearSelection();
                        foreach (ListItem li in ddlCharacterList.Items)
                            if (li.Value == dReg["CharacterID"].ToString())
                                li.Selected = true;
                        if ((ddlCharacterList.SelectedIndex < 0) && (ddlCharacterList.Items.Count > 0))
                            ddlCharacterList.SelectedIndex = 0;
                    }
                    tbComments.Text = dReg["PlayerCommentsToStaff"].ToString();
                }
            }

            _Reload = false;
        }

        protected void SetCheckDisplay(object oBool, Image imgCheckBox)
        {
            if (oBool == DBNull.Value)
                imgCheckBox.ImageUrl = "~/img/Unchecked-Checkbox-icon.png";
            else if (oBool == null)
                imgCheckBox.ImageUrl = "~/img/Unchecked-Checkbox-icon.png";
            else
            {
                bool bChecked;
                if (bool.TryParse(oBool.ToString(), out bChecked))
                    if (bChecked)
                        imgCheckBox.ImageUrl = "~/img/Checked-Checkbox-icon.png";
                    else
                        imgCheckBox.ImageUrl = "~/img/Unchecked-Checkbox-icon.png";
                else
                    imgCheckBox.ImageUrl = "~/img/Checked-Checkbox-icon.png";
            }
        }

        protected void RSVPEvent(object sender, CommandEventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            string sStatusToSearchFor = "";
            string sRegistrationMessage = "";

            switch (e.CommandName.ToUpper())
            {
                case "RSVP":
                    sStatusToSearchFor = "RSVP-Plan to Attend";
                    sRegistrationMessage = "Thank you for RSVPing.";
                    break;

                default:
                    sStatusToSearchFor = "RSVP-Cannot Attend";
                    sRegistrationMessage = "Thank you for RSVPing.";
                    break;
            }

            SortedList sParams = new SortedList();
            string sSQL = "select StatusID, StatusName from MDBStatus " +
                "where StatusType like 'Registration' and StatusName = '" + sStatusToSearchFor + "' " +
                    "and DateDeleted is null";
            DataTable dtRegStatus = Classes.cUtilities.LoadDataTable(sSQL, sParams, "LARPortal", _UserName, lsRoutineName + ".GetStatuses",
                Classes.cUtilities.LoadDataTableCommandType.Text);

            int iRegStatus = 0;

            if (dtRegStatus.Rows.Count > 0)
            {
                if (int.TryParse(dtRegStatus.Rows[0]["StatusID"].ToString(), out iRegStatus))
                    hidRegistrationStatusID.Value = iRegStatus.ToString();
            }

            int iRegistrationID = 0;
            int.TryParse(hidRegistrationID.Value, out iRegistrationID);
            int iRoleAlignment = 0;
            int.TryParse(ddlRoles.SelectedValue, out iRoleAlignment);
            int iEventID = 0;
            int.TryParse(ddlEventDate.SelectedValue, out iEventID);

            sParams = new SortedList();
            sParams.Add("@RegistrationID", hidRegistrationID.Value);
            sParams.Add("@UserID", _UserID);
            sParams.Add("@EventID", ddlEventDate.SelectedValue);
            sParams.Add("@RoleAlignmentID", ddlRoles.SelectedValue);
            sParams.Add("@DateRegistered", DateTime.Now);
            sParams.Add("@PlayerCommentsToStaff", tbComments.Text.Trim());
            if (iRegStatus != 0)
                sParams.Add("@RegistrationStatus", iRegStatus);

            DataTable dtUser = Classes.cUtilities.LoadDataTable("uspRegisterForEvent", sParams, "LARPortal", _UserName, lsRoutineName);

            foreach (DataRow dRegRecord in dtUser.Rows)
            {
                iRegistrationID = 0;
                int.TryParse(dRegRecord["RegistrationID"].ToString(), out iRegistrationID);
                Session["RegistrationID"] = iRegistrationID;
            }

            lblRegistrationMessage.Text = sRegistrationMessage;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            ddlEventDate_SelectedIndexChanged(null, null);
        }

        protected void CancelReg(object sender, CommandEventArgs e)
        {
            SortedList sParam = new SortedList();
            sParam.Add("@RegistrationID", hidRegistrationID.Value);
            sParam.Add("@UserID", _UserID);

            try
            {
                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
                DataTable dtUser = Classes.cUtilities.LoadDataTable("uspRegistrationCancel", sParam, "LARPortal", _UserName, lsRoutineName);

                NotifyOfCancelRegistration();
                lblRegistrationMessage.Text = "Your registration has been cancelled.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                ddlEventDate_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
            }

            hidRegistrationID.Value = "";
            hidCharacterID.Value = "";
            hidTeamMember.Value = "";
            hidCampaignName.Value = "";
            hidCampaignEMail.Value = "";
            hidCharAKA.Value = "";
            hidPlayerEMail.Value = "";
            hidRegistrationStatusID.Value = "";
            hidPayPalTypeID.Value = "";
            hidHasPCChar.Value = "";
            hidRegOpen.Value = "";
            hidRegClose.Value = "";
            hidEventClose.Value = "";
            hidPELClose.Value = "";
            hidCurrentRegStatus.Value = "";

            _Reload = true;
        }

        protected void Register(object sender, CommandEventArgs e)
        {
            bool bNewRegistration = false;
            string sRegistrationMessage = "";

            if ((hidRegistrationID.Value == "-1") || (hidRegistrationID.Value.Length == 0) ||
                (hidCurrentRegStatus.Value.ToString().ToUpper().Contains("RSVP")))
            {
                bNewRegistration = true;
                sRegistrationMessage = "Thank you for registering for the event.";
            }
            else
                sRegistrationMessage = "Your registration has been updated.";

            int iRegistrationID = 0;
            int iRoleAlignment = 0;
            int iEventID = 0;
            int iCharacterID = 0;
            int.TryParse(hidRegistrationID.Value, out iRegistrationID);
            int.TryParse(ddlRoles.SelectedValue, out iRoleAlignment);
            int.TryParse(ddlEventDate.SelectedValue, out iEventID);
            int.TryParse(ddlCharacterList.SelectedValue, out iCharacterID);

            SortedList sParam = new SortedList();
            sParam.Add("@RegistrationID", hidRegistrationID.Value);
            sParam.Add("@UserID", _UserID);
            sParam.Add("@EventID", ddlEventDate.SelectedValue);
            sParam.Add("@RoleAlignmentID", ddlRoles.SelectedValue);
            sParam.Add("@CharacterID", ddlCharacterList.SelectedValue);
            sParam.Add("@DateRegistered", DateTime.Now);
            sParam.Add("@EventPaymentTypeID", ddlPaymentChoice.SelectedValue);
            sParam.Add("@PlayerCommentsToStaff", tbComments.Text.Trim());
            sParam.Add("@ReqstdHousing ", tbReqstdHousing.Text);

            // If Staff person there is no character so blank it out so that it will not be included in the email.
            switch (ddlRoles.SelectedItem.Text.ToUpper())
            {
                case "NPC":
                    hidCharAKA.Value = "";      // Rick - 8/16/2016 - Replace next line to emulate staff problem resolution
                    // hidCharAKA.Value = ddlCharacterList.SelectedItem.Text;
                    sParam.Add("@NPCCampaignID", ddlSendToCampaign.SelectedValue);
                    break;

                case "PC":
                    sParam.Add("@NPCCampaignID", ddlSendToCampaign.SelectedValue);
                    break;

                case "STAFF":
                    hidCharAKA.Value = "";
                    sParam.Add("@NPCCampaignID", ddlSendToCampaign.SelectedValue);
                    break;
            }

            if (ddlTeams.Visible)
                sParam.Add("@TeamID", ddlTeams.SelectedValue);

            bool bFullEvent = true;
            if (ddlFullEvent.SelectedValue == "N")
            {
                bFullEvent = false;
                sParam.Add("@PartialEvent", true);
                DateTime dtTemp;
                if (DateTime.TryParse(tbArriveDate.Text, out dtTemp))
                {
                    sParam.Add("@ExpectedArrivalDate", string.Format("{0:MM/dd/yyyy}", dtTemp));
                    if (DateTime.TryParse(tbArriveTime.Text, out dtTemp))
                    {
                        sParam.Add("@ExpectedArrivalTime", string.Format("{0:HH:mm:ss}", dtTemp));
                    }
                }
                if (DateTime.TryParse(tbDepartDate.Text, out dtTemp))
                {
                    sParam.Add("@ExpectedDepartureDate", string.Format("{0:MM/dd/yyyy}", dtTemp));
                    if (DateTime.TryParse(tbDepartTime.Text, out dtTemp))
                    {
                        sParam.Add("@ExpectedDepartureTime", string.Format("{0:HH:mm:ss}", dtTemp));
                    }
                }
            }
            else
            {
                sParam.Add("@PartialEvent", false);
                bFullEvent = true;
            }

            try
            {
                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
                DataTable dtUser = Classes.cUtilities.LoadDataTable("uspRegisterForEvent", sParam, "LARPortal", _UserName, lsRoutineName);

                foreach (DataRow dRegRecord in dtUser.Rows)
                {
                    iRegistrationID = 0;
                    int.TryParse(dRegRecord["RegistrationID"].ToString(), out iRegistrationID);
                    Session["RegistrationID"] = iRegistrationID;

                    InsertCPOpportunity(iRoleAlignment, iCharacterID, iEventID, iRegistrationID, bFullEvent);

                    SortedList sParamsClearMeals = new SortedList();
                    sParamsClearMeals.Add("@RegistrationID", dRegRecord["RegistrationID"].ToString());
                    Classes.cUtilities.PerformNonQuery("uspClearRegistrationMeals", sParamsClearMeals, "LARPortal", _UserName);

                    if (ViewState["Meals"] != null)
                    {
                        DataTable dtMeals = ViewState["Meals"] as DataTable;
                        foreach (DataRow dMeals in dtMeals.Rows)
                        {
                            ListItem liMeal = cbMealList.Items.FindByValue(dMeals["EventMealID"].ToString());
                            if (liMeal.Selected)
                            {
                                SortedList sMealParms = new SortedList();
                                sMealParms.Add("@EventMealID", liMeal.Value);
                                sMealParms.Add("@RegistrationID", dRegRecord["RegistrationID"].ToString());
                                Classes.cUtilities.LoadDataTable("uspInsUpdCMRegistrationMeals", sMealParms, "LARPortal", _UserName, "EventRegistration.btnRegister.SavingMeals");
                            }
                        }
                    }
                }

                if (bNewRegistration)
                    NotifyOfNewRegistration(bNewRegistration);

                lblRegistrationMessage.Text = sRegistrationMessage;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                if ((bNewRegistration) && (ddlPaymentChoice.SelectedItem.Text.ToUpper() == "PAYPAL"))
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openPayPalWindow();", true);

                ddlEventDate_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                //                mvPlayerInfo.SetActiveView(vwError);
            }
        }


        protected void cbFoodList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sMeals = "";
            for (int i = 0; i < cbMealList.Items.Count; i++)
            {
                if (cbMealList.Items[i].Selected)
                {
                    sMeals += cbMealList.Items[i].Text + ", ";
                }
            }
            sMeals = sMeals.Trim();
            if ((sMeals.EndsWith(",")) && (sMeals.Length > 0))
                sMeals = sMeals.Substring(0, sMeals.Length - 1);
            tbSelectedMeals.Text = sMeals;
            _Reload = false;
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dtRegOpen = DateTime.MaxValue;
            DateTime dtRegClose = DateTime.MaxValue;
            DateTime dtEventClose = DateTime.MaxValue;
            DateTime dtPELClose = DateTime.MaxValue;

            DateTime dtTemp;
            if (DateTime.TryParse(hidRegOpen.Value, out dtTemp))
                dtRegOpen = dtTemp;

            if (DateTime.TryParse(hidRegClose.Value, out dtTemp))
                dtRegClose = dtTemp;

            if (DateTime.TryParse(hidEventClose.Value, out dtTemp))
                dtEventClose = dtTemp;

            if (DateTime.TryParse(hidPELClose.Value, out dtTemp))
                dtPELClose = dtTemp;

            //Added Post Event
            //Approved
            //Canceled
            //Hold
            //Paid/No Show
            //Registered by Other
            //RSVP-Cannot Attend
            //RSVP-Plan to Attend
            //Tentative
            //TryToRegister
            //Wait List
            //Waiting Team Approval

            btnCancel.Visible = false;
            //            btnCancelRSVP.Visible = false;
            btnChange.Visible = false;
            btnRegister.Visible = false;
            btnRSVP.Visible = false;
            btnRSVPNo.Visible = false;

            lblWhyRSVP.Visible = false;
            lblAlreadyHappened.Visible = false;
            lblClosedToPC.Visible = false;
            lblPastEvent.Visible = false;

            DateTime dtCurrentDate = DateTime.Now;

            dtCurrentDate = DateTime.Now;       // This is done for testing. Assign the date to a variable that gets used further on.

            if (dtCurrentDate < dtRegOpen)
            {
                lblWhyRSVP.Visible = true;
                btnRSVP.Visible = true;
                btnRSVPNo.Visible = true;

                //                if (hidCurrentRegStatus.Value == "")
                //                {
                //                    // New registration.
                //                    btnRSVP.Visible = true;
                //                }
                //                else
                //                {
                //                    btnChange.Visible = true;
                ////                    btnCancel.Visible = true;

                //                    if (hidCurrentRegStatus.Value == "64")      // They have already registered as a Plan To Attend.
                //                        btnCancelRSVP.Visible = true;
                //                    else
                //                        btnRSVP.Visible = true;

                //                    // Assume person has already RSVP so they can either change it or cancel it.
                //                    //btnChange.Visible = true;
                //                    //btnCancelRSVP.Visible = true;
                //                    //btnRSVPNo.Visible = true;
                //                }
            }
            else
            {
                // It's after registration has opened.
                if ((dtCurrentDate >= dtRegOpen) &&
                    (dtCurrentDate <= dtRegClose))
                {
                    // It's during registration period so anybody can register.
                    if (hidCurrentRegStatus.Value == "")
                    {
                        // New registration.
                        btnRegister.Visible = true;
                    }
                    else
                    {
                        // They have already registered so they can change or cancel it.
                        btnChange.Visible = true;
                        btnCancel.Visible = true;
                    }
                }
                else
                {
                    // It's after the registration has closed.
                    if ((dtCurrentDate > dtRegClose) &&
                        (dtCurrentDate <= dtPELClose))
                    {
                        if (ddlRoles.SelectedItem.Text == "PC")
                        {
                            // PC Only.
                            if (hidCurrentRegStatus.Value == "")
                            {
                                lblClosedToPC.Visible = true;
                            }
                            else
                            {
                                btnChange.Visible = true;
                                btnCancel.Visible = true;
                            }
                        }
                        else
                        {
                            // Non-PC.
                            if (hidCurrentRegStatus.Value == "")
                            {
                                btnRegister.Visible = true;
                            }
                            else
                            {
                                btnCancel.Visible = true;
                                btnChange.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        lblPastEvent.Visible = true;
                        // If we got here - it means that event is closed so we can't change anything.
                    }
                }
            }

            {
                //if (hidHasPCChar.Value == "0")
                //{
                //    // The person has no character so we need to display the message saying they need to create the character.
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openNoPCChar();", true);
                //   // _Reload = false;
                //   // return;
                //}

                //    mvCharacters.SetActiveView(vwCharacter);
                divTeams.Visible = true;
                //}
                //else
                //{
                //    mvCharacters.SetActiveView(vwSendCPTo);
                //    divTeams.Visible = false;

                divTeams.Visible = false;
                divSendCPTo.Visible = false;
                divCharacters.Visible = false;

                if (ddlRoles.SelectedItem.Text != "PC")
                {
                    divHouseAssign.Visible = false;
                    divHousePref.Visible = false;
                    divSendCPTo.Visible = true;
                    //divTeams.Visible = false;
                    //ddlCharacterList.Visible = false;
                    Classes.cUserCampaigns CampaignChoices = new Classes.cUserCampaigns();
                    CampaignChoices.Load(_UserID);
                    if (CampaignChoices.CountOfUserCampaigns == 0)
                        Response.Redirect("~/NoCurrentCampaignAssociations.aspx");

                    ddlSendToCampaign.DataTextField = "CampaignName";
                    ddlSendToCampaign.DataValueField = "CampaignID";
                    ddlSendToCampaign.DataSource = CampaignChoices.lsUserCampaigns;
                    ddlSendToCampaign.DataBind();
                    ddlSendToCampaign.Items.Add(new ListItem("Other", "-1"));
                }
                else
                {
                    divHouseAssign.Visible = true;
                    divHousePref.Visible = true;
                    divTeams.Visible = true;
                    //    ddlCharacterList.Visible = true;
                    divCharacters.Visible = true;
                }
            }
            _Reload = false;
        }

        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
        //}

        protected void InsertCPOpportunity(int RoleAlignment, int iCharacterID, int iEventID, int iRegistrationID, bool bFullEvent)
        {
            int iCampaignID = 0;
            if (Session["CampaignID"] != null)
                int.TryParse(Session["CampaignID"].ToString(), out iCampaignID);

            int iReasonID = 0;
            switch (ddlRoles.SelectedItem.Text)
            {
                case "PC":
                    if (bFullEvent)
                        iReasonID = 3;
                    else
                        iReasonID = 8;
                    break;
                case "NPC":
                    if (bFullEvent)
                        iReasonID = 1;
                    else
                        iReasonID = 7;
                    break;
                case "Staff":
                    iReasonID = 12;
                    break;
                default:
                    break;
            }

            Classes.cPoints cPoints = new Classes.cPoints();
            cPoints.DeleteRegistrationCPOpportunity(_UserID, iRegistrationID);

            cPoints.CreateRegistrationCPOpportunity(_UserID, iCampaignID, RoleAlignment, iCharacterID, iReasonID, iEventID, iRegistrationID);
        }

        protected void btnCreateACharacter_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Character/CharAdd.aspx", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openNoPCChar();", true);
            _Reload = false;
            return;
        }

        protected void btnStayAsNPC_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeNoPCChar();", true);
            _Reload = false;
            return;
        }
    }
}
