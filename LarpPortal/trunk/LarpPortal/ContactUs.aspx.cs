using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "ContactUs";
            Classes.cLogin ContactUs = new Classes.cLogin();
            ContactUs.getContactUs();
            lblContactUs.Text = ContactUs.ContactUsText;
        }
    }
}