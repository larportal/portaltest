using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

namespace LarpPortal
{
    public partial class WhatsNewDetail : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "WhatsNewDetail";
            if (!IsPostBack)
            {
                int intWhatsNewID;
                DateTime ReleaseDate;
                string ModuleName = "";
                string ReleaseName = "";
                string DetailedDescription = "";
                DateTime dtTemp;
                //string dq = "\"";
                string TableCode = "";               
                if ((Request.QueryString["WhatsNewID"] == null))
                {
                    intWhatsNewID = 1;  // Use the What's New? announcement (ID 1) as the default if we somehow get here with a NULL ID
                }
                else
                {
                    intWhatsNewID = Int32.Parse(Request.QueryString["WhatsNewID"]);
                }
                string stStoredProc = "uspGetWhatsNew";
                string stCallingMethod = "WhatsNewDetail.aspx.Page_PreRender";
                DataSet dsWhatsNewDetail = new DataSet();
                SortedList sParams = new SortedList();
                sParams.Add("@WhatsNewID", intWhatsNewID);
                dsWhatsNewDetail = Classes.cUtilities.LoadDataSet(stStoredProc, sParams, "LARPortal", Session["UserName"].ToString(), stCallingMethod);
                dsWhatsNewDetail.Tables[0].TableName = "MDBWhatsNew";

                foreach (DataRow dRow in dsWhatsNewDetail.Tables["MDBWhatsNew"].Rows)
                {
                    if (DateTime.TryParse(dRow["ReleaseDate"].ToString(), out dtTemp))
                        ReleaseDate = dtTemp;
                    ModuleName = dRow["ModuleName"].ToString();
                    ReleaseName = dRow["BriefName"].ToString();
                    DetailedDescription = dRow["LongDescription"].ToString();
                    lblPanelHeader.Text = "What's New? - " + ModuleName + " - " + ReleaseName;
                    TableCode = "<table><tr><td>" + DetailedDescription + "</td></tr></table>";
                }
                //TableCode = TableCode + "<br><table><tr><td></td><td><asp:Button ID=" + dq + "btnClose" + dq + " runat=" + dq + "server" + dq + " OnClick=" +
                //    dq + "btnClose_Click" + dq + " Text=" + dq + "Close" + dq + " CssClass=" + dq + "StandardButton" + dq + " /></td></tr></table>";
                lblWhatsNewDetail.Text = TableCode;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
    }
}