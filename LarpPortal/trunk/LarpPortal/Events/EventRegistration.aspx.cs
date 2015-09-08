using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Events
{
    public partial class EventRegistration : System.Web.UI.Page
    {
        private bool _Reload = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            tbSelectedMeals.Attributes.Add("Placehold", "Select Meals");
            ddlFullEvent.Attributes.Add("onchange", "ddl_changed(this);");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if ((Session["CampaignID"] != null) && (_Reload))
            {
                int iCampaignID;
                if (int.TryParse(Session["CampaignID"].ToString(), out iCampaignID))
                {
                    SortedList sParams = new SortedList();
                    sParams.Add("@CampaignID", iCampaignID.ToString());
                    DataSet dtEventInfo = Classes.cUtilities.LoadDataSet("uspGetEventInfo", sParams, "LARPortal", Session["UserName"].ToString(), "EventRegistration.gvEvents_RowCommand");

                    dtEventInfo.Tables[0].TableName = "EventInfo";
                    dtEventInfo.Tables[1].TableName = "Housing";
                    dtEventInfo.Tables[2].TableName = "PaymentType";

                    // Eventually the row filter needs to be     StatusName = 'Scheduled' and RegistrationOpenDateTime <= GetDate() and RegistrationCloseDateTime >= GetDate()
                    DataView dvEventInfo = new DataView(dtEventInfo.Tables["EventInfo"], "StatusName = 'Scheduled'",    // and RegistrationOpenDateTime > '" + System.DateTime.Today + "'",
                        "", DataViewRowState.CurrentRows);

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

                    if (ddlEventDate.Items.Count > 0)
                        ddlEventDate_SelectedIndexChanged(null, null);
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
        }

        protected void NotifyOfNewRegistration()
        {
            //string strBody;
            ////string strFromUser = "support";
            ////string strFromDomain = "larportal.com";
            ////string strFrom = strFromUser + "@" + strFromDomain;
            ////string strSMTPPassword = "Piccolo1";
            //string strSubject = "New Silverfire event registration - " + lblPlayerName.Text;
            //string strTeam = "";
            //if (ddlTeams.SelectedIndex >= 0)
            //    strTeam = ddlTeams.SelectedItem.Text;
            //strBody = lblPlayerName.Text + " has just registered for the upcoming Silverfire event.  <br>Email: " + lblPlayerEmail.Text + "<br>Character: " + lblCharacterAKA.Text + "<br>Team: ";
            //strBody = strBody + strTeam + "<br>Payment Method: " + ddlPaymentType.SelectedItem.Text + "<br>Player Comments: " + tbComment.Text;
            ////string EmailAddress = "fifthgategm@gmail.com";
            ////MailMessage mail = new MailMessage(strFrom, EmailAddress);
            ////mail.Bcc.Add("support@larportal.com");
            ////SmtpClient client = new SmtpClient("smtpout.secureserver.net", 80);
            ////client.EnableSsl = false;
            ////client.UseDefaultCredentials = false;
            ////client.Credentials = new System.Net.NetworkCredential(strFrom, strSMTPPassword);
            ////client.Timeout = 10000;
            ////mail.Subject = strSubject;
            ////mail.Body = strBody;
            ////mail.IsBodyHtml = true;

            //Classes.cEmailMessageService RegistrationEmail = new Classes.cEmailMessageService();

            //try
            //{
            //    //client.Send(mail);
            //    RegistrationEmail.SendMail(strSubject, strBody, "fifthgategm@gmail.com", lblPlayerEmail.Text, "support@larportal.com");
            //}
            //catch (Exception)
            //{
            //    //lblUsernameISEmail.Text = "There was an issue. Please contact us at support@larportal.com for assistance.";
            //    //lblUsernameISEmail.Visible = true;
            //}
        }

        protected void ddlEventDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bIncludeReg = false;

            SortedList sParams = new SortedList();
            sParams.Add("@EventID", ddlEventDate.SelectedValue);
            int iUserID;
            if (Session["UserID"] != null)
            {
                if (int.TryParse(Session["UserID"].ToString(), out iUserID))
                    sParams.Add("@UserID", iUserID);
            }
            DataSet dtEventInfo = Classes.cUtilities.LoadDataSet("uspGetEventInfo", sParams, "LARPortal", Session["UserName"].ToString(), "EventRegistration.gvEvents_RowCommand");

            dtEventInfo.Tables[0].TableName = "EventInfo";
            dtEventInfo.Tables[1].TableName = "Housing";
            dtEventInfo.Tables[2].TableName = "PaymentType";

            if (dtEventInfo.Tables.Count >= 5)
            {
                bIncludeReg = true;
                dtEventInfo.Tables[3].TableName = "Character";
                dtEventInfo.Tables[4].TableName = "Teams";
                dtEventInfo.Tables[5].TableName = "Registration";
                dtEventInfo.Tables[6].TableName = "RolesForEvent";
                dtEventInfo.Tables[7].TableName = "RegistrationStatuses";
                dtEventInfo.Tables[8].TableName = "Meals";
            }

            DateTime dtEventStartDateTime = DateTime.MinValue;
            DateTime dtEventEndDateTime = DateTime.MinValue;
            DateTime dtEventRegOpenDateTime = DateTime.MinValue;
            DateTime dtEventRegCloseDateTime = DateTime.MaxValue;

            foreach (DataRow dRow in dtEventInfo.Tables["EventInfo"].Rows)
            {
                lblEventName.Text = dRow["EventName"].ToString();
                lblEventStatus.Text = dRow["StatusName"].ToString();
                lblEventDescription.Text = dRow["EventDescription"].ToString();
                lblInGameLocation.Text = dRow["IGEventLocation"].ToString();

                lblSiteLocation.Text = dRow["SiteName"].ToString() + " " + dRow["SiteAddress1"].ToString() + " " + dRow["SiteCity"].ToString() + " " +
                    dRow["SiteStateID"].ToString() + ", " + dRow["SitePostalCode"].ToString();

                DateTime dtTemp;
                double dTemp;

                if (DateTime.TryParse(dRow["RegistrationOpenDateTime"].ToString(), out dtTemp))
                {
                    lblEventOpenDate.Text = string.Format("{0: MM/dd/yy hh:mm tt}", dtTemp);
                    dtEventRegOpenDateTime = dtTemp;
                }
                if (DateTime.TryParse(dRow["RegistrationCloseDateTime"].ToString(), out dtTemp))
                    dtEventRegCloseDateTime = dtTemp;

                if (DateTime.TryParse(dRow["PaymentDueDate"].ToString(), out dtTemp))
                    lblPaymentDue.Text = string.Format("{0: MM/dd/yy}", dtTemp);

                if (double.TryParse(dRow["PreregistrationPrice"].ToString(), out dTemp))
                    lblPreRegPrice.Text = dTemp.ToString("C");
                if (DateTime.TryParse(dRow["PreregistrationDeadline"].ToString(), out dtTemp))
                    lblPreRegDate.Text = string.Format("{0: MM/dd/yy}", dtTemp);
                if (double.TryParse(dRow["LateRegistrationPrice"].ToString(), out dTemp))
                    lblRegPrice.Text = string.Format("{0: MM/dd/yy}", dtTemp);
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

            if ((DateTime.Now >= dtEventRegOpenDateTime) &&
                (DateTime.Now <= dtEventRegCloseDateTime))
                mvButtons.SetActiveView(vwRegisterButtons);
            else
                mvButtons.SetActiveView(vwRSVPButtons);

            tbArriveDate.Text = dtEventStartDateTime.ToString("MM/dd/yyyy");
            tbArriveTime.Text = dtEventStartDateTime.ToString("hh:mm tt");
            tbDepartDate.Text = dtEventEndDateTime.ToString("MM/dd/yyyy");
            tbDepartTime.Text = dtEventEndDateTime.ToString("hh:mm tt");

            lblRegistrationStatus.Text = "Not Registered";

            if (dtEventInfo.Tables["Character"].Rows.Count > 0)
            {
                ddlCharacterList.DataSource = dtEventInfo.Tables["Character"];
                ddlCharacterList.DataTextField = "CharacterAKA";
                ddlCharacterList.DataValueField = "CharacterID";
                ddlCharacterList.DataBind();
                ddlCharacterList.SelectedIndex = 0;
                ddlCharacterList.Visible = true;
                lblCharacter.Visible = false;
            }

            if (dtEventInfo.Tables["Character"].Rows.Count == 1)
            {
                ddlCharacterList.Visible = false;
                lblCharacter.Visible = true;
                lblCharacter.Text = ddlCharacterList.Items[0].Text;
            }

            ddlHousing.DataSource = new DataView(dtEventInfo.Tables["Housing"], "", "Description", DataViewRowState.CurrentRows);
            ddlHousing.DataTextField = "Description";
            ddlHousing.DataValueField = "HousingTypeID";
            ddlHousing.DataBind();

            ddlRoles.DataSource = dtEventInfo.Tables["RolesForEvent"];
            ddlRoles.DataTextField = "Description";
            ddlRoles.DataValueField = "RoleAlignmentID";
            ddlRoles.DataBind();

            if (dtEventInfo.Tables["RolesForEvent"].Rows.Count == 1)
            {
                ddlRoles.Visible = false;
                lblRole.Text = ddlRoles.Items[0].Text;
                lblRole.Visible = true;
            }
            else
            {
                lblRole.Visible = false;
                ddlRoles.Visible = true;
                ddlRoles.Items[0].Selected = true;
            }

            hidRegistrationID.Value = "-1";

            foreach (DataRow dCharInfo in dtEventInfo.Tables["Character"].Rows)
            {
                lblPlayerName.Text = dCharInfo["PlayerName"].ToString();
                lblEMail.Text = dCharInfo["EmailAddress"].ToString();
                lblCharacter.Text = dCharInfo["CharacterAKA"].ToString().Trim();

                if (bIncludeReg)
                {
                    if (dtEventInfo.Tables["Meals"].Rows.Count > 0)
                    {
                        mvMenu.SetActiveView(vwFoodAvail);
                        cbMealList.DataSource = dtEventInfo.Tables["Meals"];
                        cbMealList.DataTextField = "MealDescription";
                        cbMealList.DataValueField = "EventMealID";
                        cbMealList.DataBind();
                    }
                    else
                        mvMenu.SetActiveView(vwNoFood);

                    if (dtEventInfo.Tables["Teams"].Rows.Count > 0)
                    {
                        ddlTeams.Visible = true;
                        ddlTeams.DataSource = dtEventInfo.Tables["Teams"];
                        ddlTeams.DataTextField = "TeamName";
                        ddlTeams.DataValueField = "TeamID";
                        ddlTeams.DataBind();
                        ddlTeams.Items.Insert(0, new ListItem("No Team", "0"));
                        lblNoTeams.Visible = false;
                    }
                    else
                    {
                        ddlTeams.Visible = false;
                        lblNoTeams.Visible = true;
                    }

                    ddlPaymentChoice.DataSource = dtEventInfo.Tables["PaymentType"];
                    ddlPaymentChoice.DataTextField = "Description";
                    ddlPaymentChoice.DataValueField = "PaymentTypeID";
                    ddlPaymentChoice.DataBind();
                    ddlPaymentChoice.Items.Insert(0, new ListItem("No Payment", "0"));

                    DateTime dtArrivalDateTime = DateTime.MinValue;
                    DateTime dtDepartureDatetime = DateTime.MinValue;


                    foreach (DataRow dReg in dtEventInfo.Tables["Registration"].Rows)
                    {
                        hidRegistrationID.Value = dReg["RegistrationID"].ToString();
                        lblRegistrationStatus.Text = dReg["RegistrationStatus"].ToString();

                        btnRegister.Text = "Change Registration";
                        btnRegister.Width = Unit.Pixel(200);


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
                        if (dReg["RoleAlignmentID"].ToString() != "")
                        {
                            ddlRoles.ClearSelection();
                            foreach (ListItem li in ddlRoles.Items)
                                if (li.Value == dReg["RoleAlignmentID"].ToString())
                                    li.Selected = true;
                        }
                        if (dReg["CharacterID"].ToString() != "")
                        {
                            ddlCharacterList.ClearSelection();
                            foreach (ListItem li in ddlCharacterList.Items)
                                if (li.Value == dReg["CharacterID"].ToString())
                                    li.Selected = true;
                            if (ddlCharacterList.SelectedIndex < 0)
                                ddlCharacterList.SelectedIndex = 0;
                        }
                    }
                }
            }


            //        <asp:DropDownList ID="ddlTeams" runat="server" />
            //        <asp:DropDownList ID="ddlHousing" runat="server" />
            //        <asp:DropDownList ID="ddlMealPlan" runat="server" />
            //        <asp:TextBox ID="tbPayment" runat="server" TextMode="MultiLine" CssClass="col-lg-12" Rows="4" /></div>
            //        <asp:DropDownList ID="ddlPaymentChoice" runat="server" /></div>
            //        <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" CssClass="col-lg-12" Rows="4" /></div>
            //<asp:HiddenField ID="hidEventID" runat="server" />
            //<asp:HiddenField ID="hidCharacterID" runat="server" />
            //<asp:HiddenField ID="hidTeamMember" runat="server" Value="0" />

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

        protected void btnRegister_Command(object sender, CommandEventArgs e)
        {
            SortedList sParam = new SortedList();
            sParam.Add("@RegistrationID", hidRegistrationID.Value);
            sParam.Add("@UserID", Session["UserID"].ToString());
            sParam.Add("@EventID", ddlEventDate.SelectedValue);
            sParam.Add("@RoleAlignmentID", ddlRoles.SelectedValue);
            sParam.Add("@CharacterID", ddlCharacterList.SelectedValue);
            sParam.Add("@DateRegistered", DateTime.Now);
            sParam.Add("@EventPaymentTypeID", ddlPaymentChoice.SelectedValue);
            sParam.Add("@PlayerCommentsToStaff", tbComments.Text.Trim());
            if (hidTeamMember.Value == "1")
                if (ddlTeams.SelectedIndex != 0)
                    sParam.Add("@TeamID", ddlTeams.SelectedValue);

            string sStatusToSearchFor = "";

            switch (e.CommandName.ToString().ToUpper())
            {
                case "RSVPATTEND":
                    sStatusToSearchFor = "RSVP-Plan to Attend";
                    break;

                case "RSVPCANNOTATTEND":
                    sStatusToSearchFor = "RSVP-Cannot Attend";
                    break;

                case "UNREGISTER":
                    break;

                case "REGISTER":
                    sStatusToSearchFor = "Wait List";
                    break;
            }

            if (sStatusToSearchFor != "")
            {
                SortedList sParams = new SortedList();
                string sSQL = "select StatusID, StatusName from MDBStatus " +
                    "where StatusType like 'Registration' " +
                        "and DateDeleted is null";
                DataTable dtRegStatus = Classes.cUtilities.LoadDataTable(sSQL, sParams, "LARPortal", Session["UserName"].ToString(), "EventRegistration.btnRegister_Command",
                    Classes.cUtilities.LoadDataTableCommandType.Text);
                DataView dvRegStatus = new DataView(dtRegStatus, "StatusName = '" + sStatusToSearchFor + "'", "", DataViewRowState.CurrentRows);
                if (dvRegStatus.Count > 0)
                {
                    int iRegStatus;
                    if (int.TryParse(dvRegStatus[0]["StatusID"].ToString(), out iRegStatus))
                        sParam.Add("@RegistrationStatus", iRegStatus);
                }
            }

            if (ddlFullEvent.SelectedValue == "N")
            {
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

            try
            {
                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
                DataTable dtUser = Classes.cUtilities.LoadDataTable("uspInsUpdCMRegistrations", sParam, "LARPortal", Session["UserName"].ToString(), lsRoutineName);
                mvPlayerInfo.SetActiveView(vwRegistered);
                string jsString = "alert('Character " + lblCharacter.Text + " has been registered.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
                NotifyOfNewRegistration();
            }
            catch
            {
                mvPlayerInfo.SetActiveView(vwError);
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
            if (ddlRoles.SelectedItem.Text == "PC")
            {
                mvCharacters.SetActiveView(vwCharacter);
                divTeams.Visible = true;
                divHousing.Visible = true;
            }
            else
            {
                mvCharacters.SetActiveView(vwSendCPTo);
                divTeams.Visible = false;
                divHousing.Visible = false;
            }
            _Reload = false;
        }
    }
}
