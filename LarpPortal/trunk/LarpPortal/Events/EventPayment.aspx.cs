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
using LarpPortal.Classes;
using System.Net;
using System.Net.Mail;

namespace LarpPortal.Events
{
    public partial class EventPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Using session variable RegistrationID look up campaign, event, character
            int RegistrationID = 0;
            if(Session["RegistrationID"] == null)
            {
                Session["RegistrationID"] = 8529;  // 8529 Rick Madrigal registration for testing.  Remove this line when being called
            }
            int.TryParse(Session["RegistrationID"].ToString(), out RegistrationID);
            if(RegistrationID == 0)
            {
                ClosePage();
            }
            string strUserName = Session["UserName"].ToString();
            lblRegistrationText.Text = "<p>Pay for Madrigal with PayPal!</p>";
            lblRegistrationText.Text += "<p>As of 2011, Madrigal has changed our payment system so that there ";
            lblRegistrationText.Text += "is no longer any Membership fee - insurance and overhead fees have ";
            lblRegistrationText.Text += "been rolled into the event cost. This way, NPCs play for free!</p>";
            lblRegistrationText.Text += "<p>This means that Adventure Weekends now cost $80, no matter when you register:</p>";
            lblFoodText.Text = "<p>At the current campsite, meal services are available during Adventure Weekends, and may be paid for ahead of time or at check-in - ";
            lblFoodText.Text += " please note that the camp staff cannot accept payment for meals during meal times:</p>";
            if (RegistrationID == 0)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);
            }
            else
            {
                string lsRoutineName = "EventPayment.PageLoad";
                string stStoredProc = "uspGetPaymentPageCode";
                int RoleAlignmentID = 0;
                string CharacterAKA = "";
                string EventName = "";
                string CampaignName = "";
                int PageCodeID = 0;
                SortedList slParams = new SortedList();
                slParams.Add("@RegistrationID", RegistrationID);
                DataTable dtPaymentPageCode = cUtilities.LoadDataTable(stStoredProc, slParams, "LARPortal", strUserName, lsRoutineName);
                if(dtPaymentPageCode.Rows.Count == 0)
                {
                    lblHeader.Text = "There are no online payment options for this campaign";
                    lblHeader.Visible = true;
                }
                else
                {
                    foreach (DataRow dRow in dtPaymentPageCode.Rows)
                    {
                        int.TryParse(dRow["PaymentPageID"].ToString(), out PageCodeID);
                        int.TryParse(dRow["RoleAlignmentID"].ToString(), out RoleAlignmentID);
                        CharacterAKA = dRow["CharacterAKA"].ToString();
                        EventName = dRow["EventName"].ToString();
                        CampaignName = dRow["CampaignName"].ToString();
                        switch (RoleAlignmentID)
                        {
                            case 2:
                                CharacterAKA = "NPC";
                                break;
                            case 3:
                                CharacterAKA = "Staff";
                                break;
                            default:
                                CharacterAKA = "Character-" + CharacterAKA;
                                break;
                        }
                        hidItemName.Value = EventName + " - " + CharacterAKA;
                    }
                    GetPageCode(PageCodeID, CharacterAKA, EventName, CampaignName);
                }
            }
        }

        protected void GetPageCode(int PaymentPageID, string CharacterAKA, string EventName, string CampaignName)
        {
            string lsRoutineName = "EventPayment.PageLoad.PaymentPageCode";
            string stStoredProc = "uspGetPaymentPageTypeCode";
            string strUserName = Session["UserName"].ToString();
            SortedList slParams = new SortedList();
            slParams.Add("@PaymentPageID", PaymentPageID);
            slParams.Add("@CharacterName", CharacterAKA);
            slParams.Add("@EventName", EventName);
            slParams.Add("@CampaignName", CampaignName);
            DataTable dtPaymentPageCode = cUtilities.LoadDataTable(stStoredProc, slParams, "LARPortal", strUserName, lsRoutineName);
            if (dtPaymentPageCode.Rows.Count == 0)
            {
                lblHeader.Text = "There are no online payment options for this campaign";
                lblHeader.Visible = true;
            }
            else
            {
                string DescriptionKey;
                string PageCode;
                string ButtonName;
                bool ButtonVisible = false;
                foreach (DataRow dRow in dtPaymentPageCode.Rows)
                {
                    DescriptionKey = dRow["DescriptionKey"].ToString();
                    PageCode = dRow["PageCode"].ToString();
                    ButtonName = dRow["ButtonName"].ToString();
                    bool.TryParse(dRow["ButtonVisible"].ToString(), out ButtonVisible);
                    switch (DescriptionKey)
                    {
                        case "Header":
                            lblHeader.Text = PageCode;
                            lblHeader.Visible = true;
                            break;
                        case "Footer":
                            lblFooter.Text = PageCode;
                            lblFooter.Visible = true;
                            break;
                    //    case "Registration":
                    //        lblRegistraton.Text = PageCode;
                    //        lblRegistraton.Visible = true;
                    //        //if (ButtonVisible == true)
                    //            //btnRegistration.Visible = true;
                    //        break;
                    //    case "SaturdayBrunch":
                    //        lblSaturdayBrunch.Text = PageCode;
                    //        lblSaturdayBrunch.Visible = true;
                    //        if (ButtonVisible == true)
                    //            btnSaturdayBrunch.Visible = true;
                    //        break;
                    //    case "SaturdayDinner":
                    //        lblSaturdayDinner.Text = PageCode;
                    //        lblSaturdayDinner.Visible = true;
                    //        if (ButtonVisible == true)
                    //            btnSaturdayDinner.Visible = true;
                    //        break;
                    //    case "SundayBrunch":
                    //        lblSundayBrunch.Text = PageCode;
                    //        lblSundayBrunch.Visible = true;
                    //        if (ButtonVisible == true)
                    //            btnSundayBrunch.Visible = true;
                    //        break;
                    //    case "AllMeals":
                    //        lblAllMeals.Text = PageCode;
                    //        lblAllMeals.Visible = true;
                    //        if (ButtonVisible == true)
                    //            btnAllMeals.Visible = true;
                    //        break;
                        default:

                            break;
                    }

                }                
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void ClosePage()
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);
        }

        protected void btnRegistration_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnSaturdayBrunch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnSaturdayDinner_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnSundayBrunch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnAllMeals_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnCalculateOrder_Click(object sender, EventArgs e)
        {
            if(chkRegistration.Checked == true || chkSaturdayBrunch.Checked == true || chkSaturdayDinner.Checked == true || chkSundayBrunch.Checked == true || chkAllMeals.Checked == true)
            {
                double OrderTotal = 0;
                string TotalSectionHTML;
                int CartCounter = 0;
                string strCartCounter = CartCounter.ToString();
                TotalSectionHTML = "<form></form><form action=\"http://www.paypal.com/cgi-bin/webscr\" method=\"post\">";
                TotalSectionHTML += "<input type=\"hidden\" name=\"cmd\" value=\"_cart\">";
                TotalSectionHTML += "<input type=\"hidden\" name=\"upload\" value=\"1\">";
                TotalSectionHTML += "<input type=\"hidden\" name=\"business\" value=\"rciccolini@gmail.com\">";
                TotalSectionHTML += "<input type=\"hidden\" name=\"currency_code\" value=\"USD\">";
                if(chkRegistration.Checked == true)
                {
                    CartCounter = CartCounter + 1;
                    strCartCounter = CartCounter.ToString();
                    TotalSectionHTML += "<input type=\"hidden\" name=\"item_name_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"" + hidItemName.Value + "\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"number_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"1\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"amount_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"80\">";
                    OrderTotal += 80;
                }
                if(chkSaturdayBrunch.Checked == true)
                {
                    CartCounter = CartCounter + 1;
                    strCartCounter = CartCounter.ToString();
                    TotalSectionHTML += "<input type=\"hidden\" name=\"item_name_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"Saturday Brunch\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"number_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"21\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"amount_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"6\">";
                    OrderTotal += 6;
                }
                if(chkSaturdayDinner.Checked == true)
                {
                    CartCounter = CartCounter + 1;
                    strCartCounter = CartCounter.ToString();
                    TotalSectionHTML += "<input type=\"hidden\" name=\"item_name_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"Saturday Dinner\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"number_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"22\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"amount_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"8\">";
                    OrderTotal += 8;
                }
                if(chkSundayBrunch.Checked == true)
                {
                    CartCounter = CartCounter + 1;
                    strCartCounter = CartCounter.ToString();
                    TotalSectionHTML += "<input type=\"hidden\" name=\"item_name_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"Sunday Brunch\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"number_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"23\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"amount_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"6\">";
                    OrderTotal += 6;
                }
                if(chkAllMeals.Checked == true)
                {
                    CartCounter = CartCounter + 1;
                    strCartCounter = CartCounter.ToString();
                    TotalSectionHTML += "<input type=\"hidden\" name=\"item_name_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"All three meals\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"number_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"25\">";
                    TotalSectionHTML += "<input type=\"hidden\" name=\"amount_";
                    TotalSectionHTML += strCartCounter;
                    TotalSectionHTML += "\" value=\"20\">";
                    OrderTotal += 20;
                }
                lblOrderTotalSection.Text = TotalSectionHTML;
                lblOrderTotalSection.Visible = true;
                lblOrderTotalDisplay.Text = "Your total is $" + OrderTotal.ToString();
                lblOrderTotalDisplay.Visible = true;
                btnPayPalTotal.Visible = true;
            }
            else
            {
                string jsString = "alert('You need to select at least one option.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
            }
        }

        protected void btnPayPalTotal_Click(object sender, ImageClickEventArgs e)
        {

        }


    }
}