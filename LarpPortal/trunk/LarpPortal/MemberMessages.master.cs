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
    public partial class MemberMessages : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "Messages";
            string hrefline;
            string ActiveState;
            string PageName;
            string LineText;
            int liLinesNeeded = 2;
            string MessageCount = "";
            DataTable LeftNavTable = new DataTable();
            LeftNavTable.Columns.Add("href_li");
            ActiveState = ">";
            PageName = "~/DESTINATIONPAGE.aspx";
            LineText = "";
            for (int i = 0; i <= liLinesNeeded; i++)
            {
                //build on case of i
                switch (i)
                {
                    case 0:
                        // Requires RSVP with a count after
                        if (Session["ActiveLeftNav"].ToString() == "RSVP")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "RSVPMessages.aspx";
                        LineText = "Requires RSVP";
                        MessageCount = " <span class=" + "\"" + "badge" + "\"" + ">" + Session["RSVPMessageCount"].ToString() + "</span>";
                        break;
                    case 1:
                        // Unread with a count after
                        if (Session["ActiveLeftNav"].ToString() == "Unread")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "UnreadMessages.aspx";
                        LineText = "Unread";
                        MessageCount = " <span class=" + "\"" + "badge" + "\"" + ">" + Session["UnreadMessageCount"].ToString() + "</span>";
                        break;
                    case 2:
                        // Total with a count after
                        if (Session["ActiveLeftNav"].ToString() == "Total")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "AllMessages.aspx";
                        LineText = "Total";
                        MessageCount = " <span class=" + "\"" + "badge" + "\"" + ">" + Session["TotalMessageCount"].ToString() + "</span>";
                        break;

                    case 3:
                        if (Session["ActiveLeftNav"].ToString() == "Placeholder4")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "Placeholder4.aspx";
                        LineText = "Menu Item TBD 4";
                        break;
                    case 4:
                        if (Session["ActiveLeftNav"].ToString() == "Placeholder5")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "Placeholder5.aspx";
                        LineText = "Menu Item TBD 5";
                        break;
                    case 5:
                        if (Session["ActiveLeftNav"].ToString() == "Placeholder6")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "Placeholder6.aspx";
                        LineText = "Menu Item TBD 6";
                        break;
                    case 6:
                        if (Session["ActiveLeftNav"].ToString() == "Placeholder7")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "Placeholder7.aspx";
                        LineText = "Menu Item TBD 7";
                        break;
                    case 7:
                        if (Session["ActiveLeftNav"].ToString() == "Placeholder8")
                        {
                            ActiveState = " class=\"active\">";
                        }
                        else
                        {
                            ActiveState = ">";
                        }
                        PageName = "Placeholder8.aspx";
                        LineText = "Menu Item TBD 8";
                        break;
                }
                // For the sake of debugging we're going to temporarily force MessageCount to be nothing.  Comment out the next line for production.
                // MessageCount = "";
                hrefline = "<li" + ActiveState + "<a href=" + "\"" + PageName + "\"" + " data-toggle=" + "\"" + "pill" + "\"" + ">" + LineText + MessageCount + "</a></li>";
                DataRow LeftNavRow = LeftNavTable.NewRow();
                LeftNavRow["href_li"] = hrefline;
                LeftNavTable.Rows.Add(LeftNavRow);
                menu_ul_membermessages.DataSource = LeftNavTable;
                menu_ul_membermessages.DataBind();
            }
        }
    }
}