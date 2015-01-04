using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace LarpPortal
{
    public partial class ucUserCampaignsDropDownList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlUserCampaigns.SelectedIndex = 0;
            //ddlUserCampaigns.Items.Clear();
            //string uName = "";
            //string uPassword = "Password";
            //int uID = 0;
            //if (Session["Username"] != null)
            //    uName = Session["Username"].ToString();
            //if (Session["UserID"] != null)
            //    uID = (Session["UserID"].ToString().ToInt32());
            //Classes.cUser CampaignChoices = new Classes.cUser(uName, uPassword);
            //List<cUserCampaign> listUserCampaigns = new List<cUserCampaign>();
            //ddlUserCampaigns.DataTextField = "CampaignName";
            //ddlUserCampaigns.DataValueField = "CampaignID";
            //ddlUserCampaigns.DataSource = listUserCampaigns;
            //ddlUserCampaigns.DataBind();
            //TODO - Rick - If user has no campaign associations, redirect to NoCurrentCampaignAssociations.aspx
        }

        protected void ddlUserCampaigns_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string otherdesc = txtGenderOther.Text;
            //if(ddlGender.SelectedValue == "O")
            //{
            //    txtGenderOther.Visible = true;
            //    if (otherdesc == "")
            //        otherdesc = "Enter description";
            //    txtGenderOther.Attributes.Add("Placeholder", otherdesc);
            //    txtGenderOther.Focus();
            //}
            //else
            //{
            //    txtGenderOther.Visible = false;
            //}
        }
    }
}