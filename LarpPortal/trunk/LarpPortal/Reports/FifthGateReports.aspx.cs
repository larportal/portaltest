using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Reports
{
    public partial class FifthGateReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            string strUID = Session["UserID"].ToString();
            string strSecRole = Session["SecurityRole"].ToString();
            if( strUID == "98" || strUID == "436" || strSecRole == "100")   //James or Robin or LARP Portal Admin
            {
                pnlReports.Visible = true;
                lblBlank.Visible = false;
            }
        }
    }
}