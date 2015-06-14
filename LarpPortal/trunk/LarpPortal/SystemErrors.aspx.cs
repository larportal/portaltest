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
    public partial class SystemErrors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            string sWhereClause = "";
            switch (ddlDataAmount.SelectedValue)
            {
                case                     "D":
                    sWhereClause = "where WhenHappened >= DateAdd(d, -1, GetDate())";
                    break;

                case "T":
                    sWhereClause = "where WhenHappened >= Convert(date, GetDate())";
                    break;

                case "W":
                    sWhereClause = "where WhenHappened >= DateAdd(w, -1, GetDate())";
                    break;

                case "M":
                    sWhereClause = "where WhenHappened > DateAdd(m, -1, GetDate())";
                    break;

                default:
                    sWhereClause = "";
                    break;
            }

            string sSQL = "select * from SystemErrors " + sWhereClause + " order by WhenHappened desc";

            SortedList sParams = new SortedList();
            DataTable dtErrors = Classes.cUtilities.LoadDataTable(sSQL, sParams, "Audit", "ErrorPage", "Errors.Page_PreRender", 
                Classes.cUtilities.LoadDataTableCommandType.Text);
            gvErrors.DataSource = dtErrors;
            gvErrors.DataBind();
        }

        protected void ddlDataAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Just force page to reload. PreRender does everything.
        }
    }
}