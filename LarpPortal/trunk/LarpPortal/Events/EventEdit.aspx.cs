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
    public partial class EventEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tbStartTime.Attributes.Add("Placeholder", "Time");
            tbEndTime.Attributes.Add("Placeholder", "Time");
            tbMaxPCCount.Attributes.Add("Placeholder", "#");
            tbBaseNPCCount.Attributes.Add("Placeholder", "#");
            tbOpenRegDate.Attributes.Add("Placeholder", "MM/DD/YYYY");
            tbOpenRegTime.Attributes.Add("Placeholder", "Time");
            tbCloseRegDate.Attributes.Add("Placeholder", "MM/DD/YYYY");
            tbCloseRegTime.Attributes.Add("Placeholder", "Time");
            tbCapThresholdNotification.Attributes.Add("Placeholder", "#");
            tbPreRegDeadline.Attributes.Add("Placeholder", "MM/DD/YYYY");
            tbPaymentDate.Attributes.Add("Placeholder", "MM/DD/YYYY");
            tbPreRegistrationPrice.Attributes.Add("Placeholder", "Price");
            tbRegPrice.Attributes.Add("Placeholder", "Price");
            tbAtDoorPrice.Attributes.Add("Placeholder", "Price");
            tbInfoSkillDue.Attributes.Add("Placeholder", "MM/DD/YYYY");
            tbPELDue.Attributes.Add("Placeholder", "MM/DD/YYYY");
            tbStartDate.Attributes.Add("onChange", "CalcDates();");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            DateTime dtTemp;
            int iTemp;
            double dTemp;
            bool bTemp;

            if (!IsPostBack)
            {
                SortedList sParams = new SortedList();
                sParams.Add("@StatusType", "Registration");
                DataTable dtRegStatuses = Classes.cUtilities.LoadDataTable("uspGetStatus", sParams, "LARPortal", Session["UserName"].ToString(), "EventDefaults.Page_Load");
                DataView dvRegStatus = new DataView(dtRegStatuses, "(StatusName = 'Approved') or (StatusName = 'Wait List')", "StatusName", DataViewRowState.CurrentRows);
                ddlDefaultRegStatus.DataSource = dvRegStatus;
                ddlDefaultRegStatus.DataTextField = "StatusName";
                ddlDefaultRegStatus.DataValueField = "StatusID";
                ddlDefaultRegStatus.DataBind();

                ddlDefaultRegStatus.Items.Insert(0, new ListItem("Choose Value", ""));

                sParams = new SortedList();
                Classes.cUtilities.LoadDropDownList(ddlSiteList, "uspGetSites", sParams, "SiteName", "SiteID", "LARPortal", Session["UserName"].ToString(), "EventDefaults.Page_Load");

                bool bRecordLoaded = false;
                if (Request.QueryString["EventID"] != null)
                {
                    int iEventID = 0;
                    if (int.TryParse(Request.QueryString["EventID"], out iEventID))
                    {
                        sParams = new SortedList();
                        sParams.Add("@EventID", iEventID);
                        DataSet dsEventInfo = Classes.cUtilities.LoadDataSet("uspGetEventInfo", sParams, "LARPortal", Session["UserName"].ToString(), "EventEdit.Page_PreRender");

                        foreach (DataRow dEventInfo in dsEventInfo.Tables[0].Rows)
                        {
                            sParams = new SortedList();
                            bRecordLoaded = true;
                            sParams.Add("@EventID", iEventID);
                            tbEventName.Text = dEventInfo["EventName"].ToString();
                            tbGameLocation.Text = dEventInfo["IGEventLocation"].ToString();
                            tbEventDescription.Text = dEventInfo["EventDescription"].ToString();
                            ddlDefaultRegStatus.ClearSelection();
                            foreach (ListItem liStatus in ddlDefaultRegStatus.Items)
                                if (liStatus.Value == dEventInfo["StatusID"].ToString())
                                {
                                    ddlDefaultRegStatus.ClearSelection();
                                    liStatus.Selected = true;
                                }

                            if (DateTime.TryParse(dEventInfo["StartDate"].ToString(), out dtTemp))
                                tbStartDate.Text = dtTemp.ToShortDateString();
                            if (DateTime.TryParse(dEventInfo["StartTime"].ToString(), out dtTemp))
                                tbStartTime.Text = dtTemp.ToString("HH:mm");

                            if (DateTime.TryParse(dEventInfo["EndDate"].ToString(), out dtTemp))
                                tbEndDate.Text = dtTemp.ToShortDateString();
                            if (DateTime.TryParse(dEventInfo["EndTime"].ToString(), out dtTemp))
                                tbEndTime.Text = dtTemp.ToString("HH:mm");

                            foreach (ListItem liSite in ddlSiteList.Items)
                            {
                                if (liSite.Value == dEventInfo["SiteID"].ToString())
                                {
                                    ddlSiteList.ClearSelection();
                                    liSite.Selected = true;
                                }
                            }

                            GetDBInt(dEventInfo["MaximumPCCount"], tbMaxPCCount, true);
                            GetDBInt(dEventInfo["BaseNPCCount"], tbBaseNPCCount, true);
                            GetDBInt(dEventInfo["NPCOverrideRatio"], tbOverrideRatio, true);
                            GetDBInt(dEventInfo["CapThresholdNotification"], tbCapThresholdNotification, true);

                            if (DateTime.TryParse(dEventInfo["RegistrationOpenDate"].ToString(), out dtTemp))
                                tbOpenRegDate.Text = dtTemp.ToShortDateString();
                            if (DateTime.TryParse(dEventInfo["RegistrationOpenTime"].ToString(), out dtTemp))
                            {
                                tbOpenRegTime.Text = dtTemp.ToString("HH:mm"); ;
                                tbCloseRegTime.Text = dtTemp.ToString("HH:mm"); ;
                            }

                            GetDBMoney(dEventInfo["LateRegistrationPrice"], tbAtDoorPrice, true);

                            //if (DateTime.TryParse(dEventInfo["Payment
                            //    sParams.Add("@PaymentDueDate", dtTemp.ToShortDateString());
                            if (int.TryParse(dEventInfo["MaximumPCCount"].ToString(), out iTemp))
                                tbMaxPCCount.Text = iTemp.ToString();
                            if (int.TryParse(dEventInfo["BaseNPCCount"].ToString(), out iTemp))
                                tbBaseNPCCount.Text = iTemp.ToString();
                            if (int.TryParse(dEventInfo["NPCOverrideRatio"].ToString(), out iTemp))
                                tbOverrideRatio.Text = iTemp.ToString();

                            if (int.TryParse(dEventInfo["CapThresholdNotification"].ToString(), out iTemp))
                                tbCapThresholdNotification.Text = iTemp.ToString();
                            if (dEventInfo["CapMetNotification"] != DBNull.Value)
                            {
                                if (bool.TryParse(dEventInfo["CapMetNotification"].ToString(), out bTemp))
                                    foreach (ListItem litem in ddlCapNearNotification.Items)
                                        if (bTemp.ToString().ToUpper() == litem.Value.ToUpper())
                                        {
                                            ddlCapNearNotification.ClearSelection();
                                            litem.Selected = true;
                                        }
                            }

                            if (DateTime.TryParse(dEventInfo["RegistrationOpenDate"].ToString(), out dtTemp))
                                tbOpenRegDate.Text = dtTemp.ToShortDateString();
                            if (DateTime.TryParse(dEventInfo["RegistrationOpenTime"].ToString(), out dtTemp))
                                tbOpenRegTime.Text = dtTemp.ToString("HH:mm");
                            if (DateTime.TryParse(dEventInfo["RegistrationCloseDate"].ToString(), out dtTemp))
                                tbCloseRegDate.Text = dtTemp.ToShortDateString();
                            if (DateTime.TryParse(dEventInfo["RegistrationCloseTime"].ToString(), out dtTemp))
                                tbCloseRegTime.Text = dtTemp.ToString("HH:mm");
                            if (DateTime.TryParse(dEventInfo["PreregistrationDeadline"].ToString(), out dtTemp))
                                tbPreRegDeadline.Text = dtTemp.ToShortDateString();
                            if (Double.TryParse(dEventInfo["PreregistrationPrice"].ToString(), out dTemp))
                                tbPreRegistrationPrice.Text = string.Format("{0:0.00}", dTemp);
                            if (Double.TryParse(dEventInfo["LateRegistrationPrice"].ToString(), out dTemp))
                                tbRegPrice.Text = string.Format("{0:0.00}", dTemp);
                            if (Double.TryParse(dEventInfo["CheckinPrice"].ToString(), out dTemp))
                                tbAtDoorPrice.Text = string.Format("{0:0.00}", dTemp);
                            if (DateTime.TryParse(dEventInfo["InfoSkillDeadlineDate"].ToString(), out dtTemp))
                                tbInfoSkillDue.Text = dtTemp.ToShortDateString();
                            if (DateTime.TryParse(dEventInfo["PELDeadlineDate"].ToString(), out dtTemp))
                                tbPELDue.Text = dtTemp.ToShortDateString();
                            if (dEventInfo["PCFoodService"] != DBNull.Value)
                            {
                                if (bool.TryParse(dEventInfo["PCFoodService"].ToString(), out bTemp))
                                    foreach (ListItem litem in ddlPCFoodService.Items)
                                        if (litem.Value.ToUpper() == bTemp.ToString().ToUpper())
                                        {
                                            ddlPCFoodService.ClearSelection();
                                            litem.Selected = true;
                                        }
                            }
                            if (dEventInfo["NPCFoodService"] != DBNull.Value)
                            {
                                if (bool.TryParse(dEventInfo["NPCFoodService"].ToString(), out bTemp))
                                    foreach (ListItem litem in ddlNPCFoodService.Items)
                                        if (litem.Value.ToUpper() == bTemp.ToString().ToUpper())
                                        {
                                            ddlNPCFoodService.ClearSelection();
                                            litem.Selected = true;
                                        }
                            }
                        }
                    }
                }

                if (!bRecordLoaded)
                {
                    if (Session["CampaignID"] != null)
                    {
                        int iCampaignID;
                        if (int.TryParse(Session["CampaignID"].ToString(), out iCampaignID))
                        {
                            hidCampaignID.Value = iCampaignID.ToString();
                            sParams = new SortedList();
                            sParams.Add("@CampaignID", iCampaignID);

                            DataTable dtDefaults = Classes.cUtilities.LoadDataTable("uspGetCampaignEventDefaults", sParams, "LARPortal", Session["UserName"].ToString(), "EventDefaults.Page_PreRender");

                            foreach (DataRow dRow in dtDefaults.Rows)
                            {
                                hidEventID.Value = "-1";

                                if (dRow["EventStartTime"] != DBNull.Value)
                                {
                                    if (DateTime.TryParse(dRow["EventStartTime"].ToString(), out dtTemp))
                                    {
                                        string sTime;
                                        sTime = dtTemp.ToString("HH:mm");
                                        tbStartTime.Text = sTime;
                                    }
                                }

                                if (dRow["EventEndTime"] != DBNull.Value)
                                {
                                    if (DateTime.TryParse(dRow["EventEndTime"].ToString(), out dtTemp))
                                    {
                                        string sTime;
                                        sTime = dtTemp.ToString("HH:mm");
                                        tbEndTime.Text = sTime;
                                    }
                                }

                                if (dRow["RegistrationOpenTime"] != DBNull.Value)
                                {
                                    if (DateTime.TryParse(dRow["RegistrationOpenTime"].ToString(), out dtTemp))
                                    {
                                        string sTime;
                                        sTime = dtTemp.ToString("HH:mm");
                                        tbOpenRegTime.Text = sTime;

                                        tbCloseRegTime.Text = sTime;
                                    }
                                }

                                if (dRow["RegistrationStatus"] != DBNull.Value)
                                    foreach (ListItem liStatus in ddlDefaultRegStatus.Items)
                                        if (liStatus.Value == dRow["RegistrationStatus"].ToString())
                                        {
                                            ddlDefaultRegStatus.ClearSelection();
                                            liStatus.Selected = true;
                                        }

                                if (dRow["PrimarySiteID"] != DBNull.Value)
                                    foreach (ListItem liSite in ddlSiteList.Items)
                                        if (liSite.Value == dRow["PrimarySiteID"].ToString())
                                        {
                                            ddlSiteList.ClearSelection();
                                            liSite.Selected = true;
                                        }

                                GetDBInt(dRow["MaximumPCCount"], tbMaxPCCount, true);
                                GetDBInt(dRow["BaseNPCCount"], tbBaseNPCCount, true);
                                GetDBInt(dRow["DaysToRegistrationOpenDate"], hidDaysToRegistrationOpenDate, true);

                                GetDBInt(dRow["DaysToPreregistrationDeadline"], hidDaysToPreregistrationDeadline, true);
                                GetDBInt(dRow["DaysToPaymentDue"], hidDaysToPaymentDue, true);
                                GetDBMoney(dRow["PreregistrationPrice"], tbPreRegistrationPrice, true);
                                GetDBMoney(dRow["LateRegistrationPrice"], tbRegPrice, true);
                                GetDBMoney(dRow["CheckinPrice"], tbAtDoorPrice, true);

                                GetDBInt(dRow["DaysToInfoSkillDeadlineDate"], hidDaysToInfoSkillDeadlineDate, true);
                                GetDBInt(dRow["DaysToPELDeadlineDate"], hidDaysToPELDeadlineDate, true);
                                GetDBInt(dRow["NPCOverrideRatio"], tbOverrideRatio, true);
                                GetDBInt(dRow["CapThresholdNotification"], tbCapThresholdNotification, true);

                                if (bool.TryParse(dRow["CapMetNotification"].ToString(), out bTemp))
                                {
                                    ListItem SelectItem = ddlCapNearNotification.Items.FindByValue(bTemp.ToString());
                                    if (SelectItem != null)
                                    {
                                        ddlCapNearNotification.ClearSelection();
                                        SelectItem.Selected = true;
                                    }
                                }

                                if (bool.TryParse(dRow["AutoApproveWaitListOpenings"].ToString(), out bTemp))
                                {
                                    ListItem SelectItem = ddlAutoApproveWaitlist.Items.FindByValue(bTemp.ToString());
                                    if (SelectItem != null)
                                    {
                                        ddlAutoApproveWaitlist.ClearSelection();
                                        SelectItem.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
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


        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
        }

        private int GetDBInt(object oValue, TextBox sValue, bool bSetDefaultValue)
        {
            int iValue = 0;
            sValue.Text = "";
            if (oValue != DBNull.Value)
            {
                if (int.TryParse(oValue.ToString(), out iValue))
                    sValue.Text = iValue.ToString();
                else
                    if (bSetDefaultValue)
                        sValue.Text = "0";
            }
            else
                if (bSetDefaultValue)
                    sValue.Text = "0";
            return iValue;
        }

        private int GetDBInt(object oValue, HiddenField hField, bool bSetDefaultValue)
        {
            int iValue = 0;
            hField.Value = "";
            if (oValue != DBNull.Value)
            {
                if (int.TryParse(oValue.ToString(), out iValue))
                    hField.Value = iValue.ToString();
                else
                    if (bSetDefaultValue)
                        hField.Value = "0";
            }
            else
                if (bSetDefaultValue)
                    hField.Value = "0";
            return iValue;
        }

        private double GetDBDouble(object oValue, TextBox sValue)
        {
            double dValue = 0;
            sValue.Text = "";
            if (oValue != DBNull.Value)
            {
                if (double.TryParse(oValue.ToString(), out dValue))
                    sValue.Text = dValue.ToString();
            }
            return dValue;
        }

        private double GetDBMoney(object oValue, TextBox sValue, bool bSetDefaultValue)
        {
            double dValue = 0;
            sValue.Text = "";
            if (oValue != DBNull.Value)
            {
                if (double.TryParse(oValue.ToString(), out dValue))
                    sValue.Text = string.Format("{0:0.00}", dValue);
                else
                    if (bSetDefaultValue)
                        sValue.Text = "0.00";
            }
            else
                if (bSetDefaultValue)
                    sValue.Text = "0.00";
            return dValue;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dtTemp;
            double dTemp;
            int iTemp;

            SortedList sParams = new SortedList();

            sParams.Add("@UserID", Session["UserID"].ToString());
            sParams.Add("@EventID", hidEventID.Value);
            sParams.Add("@CampaignID", hidCampaignID.Value);
            sParams.Add("@EventName", tbEventName.Text.Trim());
            sParams.Add("@EventDescription", tbEventDescription.Text.Trim());
            sParams.Add("@IGEventLocation", tbGameLocation.Text.Trim());
            sParams.Add("@PaymentInstructions", tbPaymentInstructions.Text.ToString());
            sParams.Add("@SiteID", ddlSiteList.SelectedValue);

            if (DateTime.TryParse(tbStartDate.Text, out dtTemp))
                sParams.Add("@StartDate", dtTemp.ToShortDateString());
            if (DateTime.TryParse(tbStartTime.Text, out dtTemp))
                sParams.Add("@StartTime", dtTemp.ToShortTimeString());
            if (DateTime.TryParse(tbEndDate.Text, out dtTemp))
                sParams.Add("@EndDate", dtTemp.ToShortDateString());
            if (DateTime.TryParse(tbEndTime.Text, out dtTemp))
                sParams.Add("@EndTime", dtTemp.ToShortTimeString());
            if (DateTime.TryParse(tbPaymentDate.Text, out dtTemp))
                sParams.Add("@PaymentDueDate", dtTemp.ToShortDateString());
            if (int.TryParse(tbMaxPCCount.Text, out iTemp))
                sParams.Add("@MaximumPCCount", iTemp);
            if (int.TryParse(tbBaseNPCCount.Text, out iTemp))
                sParams.Add("@BaseNPCCount", iTemp);
            if (int.TryParse(tbOverrideRatio.Text, out iTemp))
                sParams.Add("@NPCOverrideRatio", iTemp);
            if (int.TryParse(tbCapThresholdNotification.Text, out iTemp))
                sParams.Add("@CapThresholdNotification", iTemp);
                sParams.Add("@CapMetNotification", ddlCapNearNotification.SelectedValue);
            if (DateTime.TryParse(tbOpenRegDate.Text, out dtTemp))
                sParams.Add("@RegistrationOpenDate", dtTemp.ToShortDateString());
            if (DateTime.TryParse(tbOpenRegTime.Text, out dtTemp))
                sParams.Add("@RegistrationOpenTime", dtTemp.ToShortTimeString());
            if (DateTime.TryParse(tbCloseRegDate.Text, out dtTemp))
                sParams.Add("@RegistrationCloseDate", dtTemp.ToShortDateString());
            if (DateTime.TryParse(tbCloseRegTime.Text, out dtTemp))
                sParams.Add("@RegistrationCloseTime", dtTemp.ToShortTimeString());
            if (DateTime.TryParse(tbPreRegDeadline.Text, out dtTemp))
                sParams.Add("@PreregistrationDeadline", dtTemp.ToShortDateString());
            if (Double.TryParse(tbPreRegistrationPrice.Text, out dTemp))
                sParams.Add("@PreregistrationPrice", dTemp);
            if (Double.TryParse(tbRegPrice.Text, out dTemp))
                sParams.Add("@LateRegistrationPrice", dTemp);
            if (Double.TryParse(tbAtDoorPrice.Text, out dTemp))
                sParams.Add("@CheckinPrice", dTemp);
            if (DateTime.TryParse(tbInfoSkillDue.Text, out dtTemp))
                sParams.Add("@InfoSkillDeadlineDate", dtTemp.ToShortDateString());
            if (DateTime.TryParse(tbPELDue.Text, out dtTemp))
                sParams.Add("@PELDeadlineDate", dtTemp.ToShortDateString());
            sParams.Add("@PCFoodService", ddlPCFoodService.SelectedValue);
            sParams.Add("@NPCFoodService", ddlNPCFoodService.SelectedValue);

            // These do appear on the screen. - Left them as a reminder and can set where they should go.
            // @DaysToAutoCancelOtherPlayerRegistration INT = NULL,
            // @AutoApproveWaitListOpenings             INT = NULL,
            // @DecisionByDate                          DATE = NULL,
            // @NotificationDate                        DATE = NULL,
            // @SharePlanningInfo                       BIT = NULL,
            // @StatusID                                INT = NULL,

            sParams.Add("@StatusID", 50);

            try
            {
                Classes.cUtilities.PerformNonQuery("uspInsUpdCMEvents", sParams, "LARPortal", Session["UserName"].ToString());

                lblRegistrationMessage.Text = "The event defaults have been changed for the campaign.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
                string t = ex.Message;
            }
        }
    }
}
