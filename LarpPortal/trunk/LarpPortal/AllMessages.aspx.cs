using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class AllMessages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "Total";
            //Message counts should be set programatically before loading the sidebar master
            Session["RSVPMessageCount"] = "0";
            Session["UnreadMessageCount"] = "0";
            Session["TotalMessageCount"] = "0";
        }
    }
}