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
    public partial class EventDefaults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tbStartTime.Attributes.Add("Placeholder", "Time");
            tbEndTime.Attributes.Add("Placeholder", "Time");
            tbMaxPCCount.Attributes.Add("Placeholder", "#");
            tbBaseNPCCount.Attributes.Add("Placeholder", "#");
            tbOpenRegDate.Attributes.Add("Placeholder", "# days b4 event");
            tbCapThresholdNotification.Attributes.Add("Placeholder", "#");
            tbOpenRegTime.Attributes.Add("Placeholder", "Time");
            tbPreRegDeadline.Attributes.Add("Placeholder", "# days b4 event");
            tbPaymentDate.Attributes.Add("Placeholder", "# days b4 event");
            tbPreRegistrationPrice.Attributes.Add("Placeholder", "Price");
            tbRegPrice.Attributes.Add("Placeholder", "Price");
            tbAtDoorPrice.Attributes.Add("Placeholder", "Price");
            tbInfoSkillDue.Attributes.Add("Placeholder", "# days b4 event");
            tbPELDue.Attributes.Add("Placeholder", "# days post event");

            SortedList sParams = new SortedList();
            sParams.Add("@StatusType", "Registration");
            Classes.cUtilities.LoadDropDownList(ddlDefaultRegStatus, "uspGetStatus", sParams, "StatusName", "StatusID", "LARPortal", Session["UserName"].ToString(), "EventDefaults.Page_Load");
            ddlDefaultRegStatus.Items.Insert(0, new ListItem("No default", "0"));

            sParams = new SortedList();
            Classes.cUtilities.LoadDropDownList(ddlSiteList, "uspGetSites", sParams, "SiteName", "SiteID", "LARPortal", Session["UserName"].ToString(), "EventDefaults.Page_Load");
            ddlSiteList.Items.Insert(0, new ListItem("No default", "0"));
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["CampaignID"] != null)
            {
                int iCampaignID;
                if (int.TryParse(Session["CampaignID"].ToString(), out iCampaignID))
                {
                    SortedList sParams = new SortedList();
                    sParams.Add("@CampaignID", iCampaignID);

                    DataTable dtDefaults = Classes.cUtilities.LoadDataTable("uspGetCampaignEventDefaults", sParams, "LARPortal", Session["UserName"].ToString(), "EventDefaults.Page_PreRender");

                    DateTime dtTemp;

                    foreach (DataRow dRow in dtDefaults.Rows)
                    {
                        hidDefaultID.Value = dRow["CMCampaignEventDefaultID"].ToString();

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
                            }
                        }

                        GetDBInt(dRow["MaximumPCCount"], tbMaxPCCount);
                        GetDBInt(dRow["BaseNPCCount"], tbBaseNPCCount);
                        GetDBInt(dRow["DaysToRegistrationOpenDate"], tbOpenRegDate);

                        GetDBInt(dRow["DaysToPreregistrationDeadline"], tbPreRegDeadline);
                        GetDBInt(dRow["DaysToPaymentDue"], tbPaymentDate);
                        GetDBMoney(dRow["PreregistrationPrice"], tbPreRegistrationPrice);
                        GetDBMoney(dRow["LateRegistrationPrice"], tbRegPrice);
                        GetDBMoney(dRow["CheckinPrice"], tbAtDoorPrice);
                        GetDBInt(dRow["DaysToInfoSkillDeadlineDate"], tbInfoSkillDue);
                        GetDBInt(dRow["DaysToPELDeadlineDate"], tbPELDue);
                        GetDBInt(dRow["MaximumPCCount"], tbMaxPCCount);
                        GetDBInt(dRow["BaseNPCCount"], tbBaseNPCCount);
                        GetDBInt(dRow["NPCOverrideRatio"], tbOverrideRatio);
                        GetDBInt(dRow["CapThresholdNotification"], tbCapThresholdNotification);

                        bool bTemp;

                        if (bool.TryParse(dRow["CapMetNotification"].ToString(), out bTemp))
                        {
                            string sSearchValue = "No";
                            if (bTemp)
                                sSearchValue = "Yes";
                            ListItem SelectItem = ddlCapNearNotification.Items.FindByValue(sSearchValue);
                            if (SelectItem != null)
                            {
                                ddlCapNearNotification.ClearSelection();
                                SelectItem.Selected = true;
                            }
                        }

                        if (bool.TryParse(dRow["AutoApproveWaitListOpenings"].ToString(), out bTemp))
                        {
                            string sSearchValue = "No";
                            if (bTemp)
                                sSearchValue = "Yes";
                            ListItem SelectItem = ddlAutoApproveWaitlist.Items.FindByValue(sSearchValue);
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

        private int GetDBInt(object oValue, TextBox sValue)
        {
            int iValue = 0;
            sValue.Text = "";
            if (oValue != DBNull.Value)
            {
                if (int.TryParse(oValue.ToString(), out iValue))
                    sValue.Text = iValue.ToString();
            }
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

        private double GetDBMoney(object oValue, TextBox sValue)
        {
            double dValue = 0;
            sValue.Text = "";
            if (oValue != DBNull.Value)
            {
                if (double.TryParse(oValue.ToString(), out dValue))
                    sValue.Text = string.Format("{0:0.00}", dValue);
            }
            return dValue;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dtTemp;
            double dTemp;
            int iTemp;

            SortedList sParams = new SortedList();

            sParams.Add("@UserID", Session["UserID"].ToString());
            sParams.Add("@CMCampaignEventDefaultID", hidDefaultID.Value);

            if (ddlDefaultRegStatus.SelectedValue == "0")
                sParams.Add("@RegistrationStatusReset", 1);
            else
                sParams.Add("@RegistrationStatus", ddlDefaultRegStatus.SelectedValue);

            if (DateTime.TryParse(tbStartTime.Text, out dtTemp))
                sParams.Add("@EventStartTime", dtTemp.ToShortTimeString());
            else
                sParams.Add("@EventStartTimeReset", 1);

            if (DateTime.TryParse(tbEndTime.Text, out dtTemp))
                sParams.Add("@EventEndTime", dtTemp.ToShortTimeString());
            else
                sParams.Add("@EventEndTimeReset", 1);

            if (DateTime.TryParse(tbOpenRegTime.Text, out dtTemp))
                sParams.Add("@RegistrationOpenTime", dtTemp.ToShortTimeString());
            else
                sParams.Add("@RegistrationOpenTimeReset", 1);

            if (ddlSiteList.SelectedValue == "0")
                sParams.Add("@PrimarySiteIDReset", 1);
            else
                sParams.Add("@PrimarySiteID", ddlSiteList.SelectedValue);

            if (int.TryParse(tbMaxPCCount.Text, out iTemp))
                sParams.Add("@MaximumPCCount", iTemp);
            else
                sParams.Add("@MaximumPCCountReset", 1);

            if (int.TryParse(tbBaseNPCCount.Text, out iTemp))
                sParams.Add("@BaseNPCCount", iTemp);
            else
                sParams.Add("@BaseNPCCountReset", 1);

            if (int.TryParse(tbOverrideRatio.Text, out iTemp))
                sParams.Add("@NPCOverrideRatio", iTemp);
            else
                sParams.Add("@NPCOverrideRatioReset", 1);

            if (int.TryParse(tbOpenRegDate.Text, out iTemp))
                sParams.Add("@DaysToRegistrationOpenDate", iTemp);
            else
                sParams.Add("@DaysToRegistrationOpenDateReset", 1);

            if (int.TryParse(tbPreRegDeadline.Text, out iTemp))
                sParams.Add("@DaysToPreregistrationDeadline", iTemp);
            else
                sParams.Add("@DaysToPreregistrationDeadlineReset", 1);

            if (int.TryParse(tbPaymentDate.ToString(), out iTemp))
                sParams.Add("@DaysToPaymentDue", iTemp);
            else
                sParams.Add("@DaysToPaymentDueReset", 1);

            if (int.TryParse(tbInfoSkillDue.Text, out iTemp))
                sParams.Add("@DaysToInfoSkillDeadlineDate", iTemp);
            else
                sParams.Add("@DaysToInfoSkillDeadlineDateReset", 1);

            if (int.TryParse(tbPELDue.Text, out iTemp))
                sParams.Add("@DaysToPELDeadlineDate", iTemp);
            else
                sParams.Add("@DaysToPELDeadlineDateReset", 1);

            if (int.TryParse(tbCapThresholdNotification.Text, out iTemp))
                sParams.Add("@CapThresholdNotification", iTemp);
            else
                sParams.Add("@CapThresholdNotificationReset", 1);

            if (double.TryParse(tbPreRegistrationPrice.Text, out dTemp))
                sParams.Add("@PreregistrationPrice", dTemp);
            else
                sParams.Add("@PreregistrationPriceReset", 1);

            if (double.TryParse(tbRegPrice.Text, out dTemp))
                sParams.Add("@LateRegistrationPrice", dTemp);
            else
                sParams.Add("@LateRegistrationPriceReset", 1);

            if (double.TryParse(tbAtDoorPrice.Text, out dTemp))
                sParams.Add("@CheckinPrice", dTemp);
            else
                sParams.Add("@CheckinPriceReset", 1);

            if (ddlCapNearNotification.SelectedValue.ToUpper() == "YES")
                sParams.Add("@CapMetNotification", 1);
            else if (ddlCapNearNotification.SelectedValue.ToUpper() == "NO")
                sParams.Add("@CapMetNotification", 0);
            else
                sParams.Add("@CapMetNotificationReset", 1);

            if (ddlAutoApproveWaitlist.SelectedValue.ToUpper() == "YES")
                sParams.Add("@AutoApproveWaitListOpenings", 1);
            else if (ddlAutoApproveWaitlist.SelectedValue.ToUpper() == "NO")
                sParams.Add("@AutoApproveWaitListOpenings", 0);
            else
                sParams.Add("@AutoApproveWaitListOpeningsReset", 1);

            sParams.Add("@PaymentInstructions", tbPaymentInstructions.Text);

            //@DaysToAutoCancelOtherPlayerRegistration
            //@DaysToAutoCancelOtherPlayerRegistrationReset
            //@DaysToInfoSkillDeadlineDate
            //@DaysToInfoSkillDeadlineDateReset

            Classes.cUtilities.PerformNonQuery("uspInsUpdCMCampaignEventDefaults", sParams, "LARPortal", Session["UserName"].ToString());

            lblRegistrationMessage.Text = "The event defaults have been changed for the campaign.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
}
