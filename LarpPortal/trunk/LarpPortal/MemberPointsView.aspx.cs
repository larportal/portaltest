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
    public partial class MemberPointsView : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "PointsView";
            string uName = "";
            int uID = 0;
            if (Session["Username"] != null)
                uName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                uID = (Session["UserID"].ToString().ToInt32());
            BuildCPAuditTable(uID);
        }

        private void BuildCPAuditTable(int UserID)
        {
            int CampaignID = 0;
            int CharacterID = 0;
            if (Session["CampaignID"] != null)
                CampaignID = (Session["CampaignID"].ToString().ToInt32());
            string CampaignDDL = "";
            if(Session["CampaignName"] != null)
                CampaignDDL = Session["CampaignName"].ToString();
            Classes.cTransactions CPAudit = new Classes.cTransactions();
            DataTable dtCPAudit = new DataTable();
            dtCPAudit = CPAudit.GetCPAuditList(UserID, CampaignID, CharacterID);
            DataView dvPoints = new DataView(dtCPAudit, "", "", DataViewRowState.CurrentRows);
            gvPointsList.DataSource = dvPoints;
            gvPointsList.DataBind();
        }

    }
}