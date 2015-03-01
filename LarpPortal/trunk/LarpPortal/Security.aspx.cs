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
    public partial class Security : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "Security";
            string uName = "";
            int uID = 0;
            if (Session["Username"] != null)
                uName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                uID = (Session["UserID"].ToString().ToInt32());
            Classes.cUser Demography = new Classes.cUser(uName, "Password");
            Classes.cPlayer PLDemography = new Classes.cPlayer(uID, uName);
            lblUsername.Text = uName;
            lblFirstName.Text = Demography.FirstName;
            if (Demography.MiddleName.Length > 0)
                lblMI.Text = Demography.MiddleName.Substring(0, 1);
            lblLastName.Text = Demography.LastName;
            lblNickName.Text = Demography.NickName;
        }
    }
}