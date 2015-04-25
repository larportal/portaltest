using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class LARPortalError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["LoginName"] = "Guest";
            Session["SecurityRole"] = 0;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["LoginName"] = "Guest";
            Session["SecurityRole"] = 0;
        }
    }
}