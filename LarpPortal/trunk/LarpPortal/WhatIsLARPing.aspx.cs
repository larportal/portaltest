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
    public partial class WhatIsLARPing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string WhatIsLARPingText;
            Session["ActiveLeftNav"] = "WhatIsLARPing";
            Classes.cLogin WhatIsIt = new Classes.cLogin();
            WhatIsIt.getWhatIsLARPing();
            WhatIsLARPingText = WhatIsIt.WhatIsLARPingText;
            lblWhatIsLARPing.Text = WhatIsLARPingText;
        }
    }
}