using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class AboutUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "AboutUs";
            Classes.cLogin AboutUs = new Classes.cLogin();
            AboutUs.getAboutUs();
            lblAboutUs.Text = AboutUs.AboutUsText;
        }
    }
}