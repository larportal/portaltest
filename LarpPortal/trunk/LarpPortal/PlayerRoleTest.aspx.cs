using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class PlayerRoleTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // start of role test
            int User = 2;
            int Campaign = 0;
            int RoleID = 0;
            Classes.cPlayerRoles PlayerRoles = new Classes.cPlayerRoles();
            PlayerRoles.Load(User, Campaign, RoleID, Convert.ToDateTime("01/01/1960")); // Last parameter = Role expirations greater than this, basically "all"
            // end of role test
            // start of transaction test
            
            // end of transaction test

        }
    }
}
