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
    public partial class ddlUserCampaigns : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ddlUserCampaignList.Items.Clear();
            //string uName = "";
            //string uPassword = "Password";
            //int uID = 0;
            ////ListItem CampaignListItem = new ListItem();
            //if (Session["Username"] != null)
            //    uName = Session["Username"].ToString();
            //if (Session["UserID"] != null)
            //    uID = (Session["UserID"].ToString().ToInt32());
            //List(cUserCampaign) CampaignListItem = new cUserCampaign(uID, uName);
            //Classes.cUser CampaignChoices = new Classes.cUser(uName, uPassword);
            

        }

        protected void ddlUserCampaignList_SelectedIndexChanged(object sender, EventArgs e)
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
