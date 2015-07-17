using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.PELs
{
    public partial class PELList : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            DataTable dtPELs = new DataTable();
            SortedList sParams = new SortedList();
            ///TODO: Put in userID in PELList.
            if (Session["CampaignID"] == null)
                return;

            sParams.Add("@UserID", Session["UserID"].ToString());
            sParams.Add("@CampaignID", Session["CampaignID"].ToString());

            dtPELs = Classes.cUtilities.LoadDataTable("uspGetPELsForUser", sParams, "LARPortal", Session["UserName"].ToString(), "PELList.Page_PreRender");

            if (dtPELs.Rows.Count > 0)
            {
                mvPELList.SetActiveView(vwPELList);

                if (dtPELs.Columns["PELStatus"] == null)
                    dtPELs.Columns.Add(new DataColumn("PELStatus", typeof(string)));
                if (dtPELs.Columns["ButtonText"] == null)
                    dtPELs.Columns.Add(new DataColumn("ButtonText", typeof(string)));

                foreach (DataRow dRow in dtPELs.Rows)
                {
                    if (dRow["DateApproved"] != System.DBNull.Value)
                    {
                        dRow["PELStatus"] = "Approved";
                        dRow["ButtonText"] = "View";
                    }
                    else if (dRow["DateSubmitted"] != System.DBNull.Value)
                    {
                        dRow["PELStatus"] = "Submitted";
                        dRow["ButtonText"] = "View";
                    }
                    else if (dRow["DateStarted"] != System.DBNull.Value)
                    {
                        dRow["PELStatus"] = "Started";
                        dRow["ButtonText"] = "Edit";
                    }
                    else
                    {
                        dRow["PELStatus"] = "";
                        dRow["ButtonText"] = "Create";
                        int iPELID;
                        if (int.TryParse(dRow["RegistrationID"].ToString(), out iPELID))
                            dRow["PELID"] = iPELID * -1;
                    }
                }

                DataView dvPELs = new DataView(dtPELs, "", "ActualArrivalDate desc, ActualArrivalTime desc, RegistrationID desc", DataViewRowState.CurrentRows);
                gvPELList.DataSource = dvPELs;
                gvPELList.DataBind();
            }
            else
            {
                mvPELList.SetActiveView(vwNoPELs);
                lblCampaignName.Text = Session["CampaignName"].ToString();
            }

            if (Session["UpdatePELMessage"] != null)
            {
                string jsString = Session["UpdatePELMessage"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", jsString, true);
                Session.Remove("UpdatePELMessage");
            }
        }

        protected void gvPELList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sRegistrationID = e.CommandArgument.ToString();
            Response.Redirect("PELEdit.aspx?RegistrationID=" + sRegistrationID, true);
        }
    }
}