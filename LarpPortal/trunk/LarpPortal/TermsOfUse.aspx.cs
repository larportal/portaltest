using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class TermsOfUse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "TermsOfUse";
            Classes.cLogin TermsOfUse = new Classes.cLogin();
            TermsOfUse.getTermsOfUse();
            lblTermsOfUse.Text = TermsOfUse.TermsOfUseText;
        }
    }
}