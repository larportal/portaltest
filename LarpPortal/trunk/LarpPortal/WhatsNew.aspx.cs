using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class WhatsNew : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "WhatsNew";
            string stStoredProc = "uspGetWhatsNew";
            string stCallingMethod = "WhatsNew.aspx.Page_PreRender";
            DataTable dtWhatsNew = new DataTable();
            SortedList sParams = new SortedList();
            dtWhatsNew = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod);
            if (dtWhatsNew.Columns["ButtonText"] == null)
                dtWhatsNew.Columns.Add(new DataColumn("ButtonText", typeof(string)));

            foreach (DataRow dRow in dtWhatsNew.Rows)
            {
                dRow["ButtonText"] = "See More";
            }
            DataView dvWhatsNew = new DataView(dtWhatsNew, "", "ReleaseDate desc, ModuleName, WhatsNewID", DataViewRowState.CurrentRows);
            gvWhatsNewList.DataSource = dvWhatsNew;
            gvWhatsNewList.DataBind();

        }

        protected void gvWhatsNewList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


    }
}
