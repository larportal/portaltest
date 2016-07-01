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
        LogWriter oLog = new LogWriter();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Profile";

            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            oLog.AddLogMessage("Starting Profile Master", "Profile.Master.Page_Load", "", Session.SessionID);

            if (!IsPostBack)
            {
                string PageName = Path.GetFileName(Request.PhysicalPath).ToUpper();

                if (PageName.Contains("DEMOGRAPHICS"))
                    liDemographics.Attributes.Add("class", "active");
                else if (PageName.Contains("SECURITY"))
                    liSecurity.Attributes.Add("class", "active");
                else if (PageName.Contains("PLAYERRESUME"))
                    liPlayerInventory.Attributes.Add("class", "active");
                else if (PageName.Contains("LARPRESUME"))
                    liLARPResume.Attributes.Add("class", "active");
                else if (PageName.Contains("MEDICAL"))
                    liMedical.Attributes.Add("class", "active");
                else if (PageName.Contains("WAIVERSCONSENT"))
                    liWaiversConsent.Attributes.Add("class", "active");
                else if (PageName.Contains("PLAYERPREFERENCES"))
                    liPlayerPreferences.Attributes.Add("class", "active");
                else if (PageName.Contains("PLAYERINVENTORY"))
                    liPlayerInventory.Attributes.Add("class", "active");
            }
            oLog.AddLogMessage("Done Profile Master", lsRoutineName, "", Session.SessionID);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }
    }
}
