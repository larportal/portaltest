using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Profile
{
    public partial class PlayerPreferences : System.Web.UI.Page
    {
        protected int _UserID = 0;
        protected string _UserName = "";
        protected bool _bTextingAvailable = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["Username"] != null)
                _UserName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);

            if (!IsPostBack)
            {
                lblMessage.Text = "";

                cUser Demography = new Classes.cUser(_UserName, "Password");
                cPlayer PLDemography = new Classes.cPlayer(_UserID, _UserName);

                List<cPhone> MobilePhone = Demography.UserPhones.Where(x => x.PhoneType == "Mobile").ToList();
                if (MobilePhone.Count > 0)
                {
                    List<cPhone> ValidText = MobilePhone.Where(x => x.Provider != "").ToList();
                    if (ValidText.Count > 0)
                        hidMobileNumber.Value = "Texts will be sent to " + MobilePhone[0].AreaCode + MobilePhone[0].PhoneNumber + ", Provider: " + MobilePhone[0].Provider;
                    else
                        hidMobileNumber.Value = "Cannot send texts because the mobile phone provider is not filled in.";
                }
                else
                    hidMobileNumber.Value = "Cannot send texts because the mobile phone number is not filled in.";

                hidEMail.Value = "EMails will be sent to " + Demography.PrimaryEmailAddress.EmailAddress;
                hidPlayerProfileID.Value = PLDemography.PlayerProfileID.ToString();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "Demographics";
            SortedList sParams = new SortedList();
            sParams.Add("@UserID", _UserID);
            DataTable dtUserNotifications = Classes.cUtilities.LoadDataTable("uspGetPlayerNotificationPrefs", sParams, "LARPortal", _UserName, "");
            DataView dvUserNotifications = new DataView(dtUserNotifications, "", "DisplayDesc", DataViewRowState.CurrentRows);

            gvPref.DataSource = dvUserNotifications;
            gvPref.DataBind();
        }

        protected void gvPref_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                _bTextingAvailable = false;
                Label lblHeaderEMail = (Label)e.Row.Cells[1].FindControl("lblHeaderEMail");
                Label lblHeaderText = (Label)e.Row.Cells[1].FindControl("lblHeaderText");
                if ((lblHeaderEMail != null) &&
                    (lblHeaderText != null))
                {
                    lblHeaderEMail.Text = hidEMail.Value;
                    if (hidMobileNumber.Value.Length > 0)
                    {
                        lblHeaderText.Text += hidMobileNumber.Value;
                        if (!hidMobileNumber.Value.ToUpper().StartsWith("CANNOT"))
                            _bTextingAvailable = true;
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButtonList rblList = (RadioButtonList)e.Row.Cells[0].FindControl("rblOptions");
                if (rblList != null)
                {
                    DataRowView dRow = (DataRowView)e.Row.DataItem;
                    rblList.SelectedIndex = 0;

                    string sAttribValue = dRow["PlayerNotificationPreferenceID"].ToString();
                    try
                    {
                        rblList.SelectedValue = dRow["DeliveryMethod"].ToString();
                    }
                    catch
                    {
                        // Means it was invalid.
                        rblList.SelectedIndex = 0;
                    }

                    rblList.Attributes.Add("RowValues", sAttribValue);
                    if (_bTextingAvailable)
                        rblList.Items[2].Enabled = true;
                    else
                    {
                        rblList.Items[2].Enabled = false;
                    }
                }
            }
        }

        protected void rblOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rbl = (RadioButtonList)sender;
            string PlayerNotificationPreferenceID = rbl.Attributes["RowValues"];

            SortedList sParam = new SortedList();

            sParam.Add("@PlayerNotificationPreferenceID", PlayerNotificationPreferenceID);
            sParam.Add("@UserID", _UserID);
            sParam.Add("@DeliveryMethod", rbl.SelectedValue);
            cUtilities.PerformNonQuery("uspInsUpdPLPlayerNotificationPreferences", sParam, "LARPortal", _UserName);
        }

        protected void rblHeaderOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rblHeaderList = (RadioButtonList)sender;
            SortedList sParam = new SortedList();
            sParam.Add("@UserID", _UserID);
            sParam.Add("@DeliveryMethod", rblHeaderList.SelectedValue);
            sParam.Add("@PlayerProfileID", hidPlayerProfileID.Value);
            sParam.Add("@SetAll", 1);

            cUtilities.PerformNonQuery("uspInsUpdPLPlayerNotificationPreferences", sParam, "LARPortal", _UserName);
        }
    }
}
