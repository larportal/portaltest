using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Profile
{
    public partial class ProfileMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Player";

            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (!IsPostBack)
            {
                string PageName = Path.GetFileName(Request.PhysicalPath).ToUpper();

                if (PageName.Contains("PROFILE"))
                    liProfile.Attributes.Add("class", "active");
                else if (PageName.Contains("SECURITY"))
                    liSecurity.Attributes.Add("class", "active");
                else if (PageName.Contains("PREFERENCE"))
                    liPlayerPerferences.Attributes.Add("class", "active");
                else if (PageName.Contains("INVENTORY"))
                    liInventory.Attributes.Add("class", "active");
                else if (PageName.Contains("LARPRESUME"))
                    liLARPResume.Attributes.Add("class", "active");
                else if (PageName.StartsWith("RESUME"))
                    liResume.Attributes.Add("class", "active");
                else if (PageName.Contains("MEDICAL"))
                    liMedical.Attributes.Add("class", "active");
                else if (PageName.Contains("WAIVERS"))
                    liWaiversConsent.Attributes.Add("class", "active");
                else if (PageName.Contains("PLAYERROLES"))
                    liRoles.Attributes.Add("class", "active");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }
    }
}
