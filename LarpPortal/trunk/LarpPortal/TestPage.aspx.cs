using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnBack.Enabled = false;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (tc.ActiveTab == Panel1)
                tc.ActiveTab = panel2;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (tc.ActiveTab == panel2)
                tc.ActiveTab = Panel1;
        }
    }
}