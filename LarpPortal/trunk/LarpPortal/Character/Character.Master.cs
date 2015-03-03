using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharacterMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PageName = Path.GetFileName(Request.PhysicalPath).ToUpper();
                if (PageName.Contains("CHARINFO"))
                    liInfo.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARITEMS"))
                    liItems.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARHISTORY"))
                    liHistory.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARSKILLS"))
                    liSkills.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARREQUESTS"))
                    liRequests.Attributes.Add("class", "active");
                else if (PageName.Contains("CHARPOINTS"))
                    liPoints.Attributes.Add("class", "active");

                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", 2);
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", "CharacterMaster.Page_Load");
                ddlCharacterSelector.DataTextField = "CharacterAKA";
                ddlCharacterSelector.DataValueField = "CharacterID";
                ddlCharacterSelector.DataSource = dtCharacters;
                ddlCharacterSelector.DataBind();

                if (ddlCharacterSelector.Items.Count > 1)
                {
                    if (Session["SelectedCharacter"] != null)
                    {
                        string sCurrentUser = Session["SelectedCharacter"].ToString();
                        foreach (ListItem liAvailableUser in ddlCharacterSelector.Items)
                        {
                            if (sCurrentUser == liAvailableUser.Value)
                                liAvailableUser.Selected = true;
                            else
                                liAvailableUser.Selected = false;
                        }
                    }
                    else
                    {
                        ddlCharacterSelector.Items[1].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    }

                    if (ddlCharacterSelector.SelectedIndex == 0)
                    {
                        ddlCharacterSelector.Items[0].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    }
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (ddlCharacterSelector.SelectedValue != "")
            {
                string t = ddlCharacterSelector.SelectedValue;
            }
        }

        protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
        }
    }
}


























//<%--        <asp:Table ID="Table1" runat="server">
//    <asp:TableRow VerticalAlign="top">
//        <asp:TableCell Wrap="false" VerticalAlign="Top" Width="227" BorderWidth="0">
//            <div class="contentArea">
//                <nav class="userSubNav">
//                    <asp:DropDownList ID="ddlCharacterSelector" runat="server" CssClass="col-xs-12" Width="227" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacterSelector_SelectedIndexChanged" />
//                    <div class="col-xs-12">
//                        <ul class="nav nav-pills panel-wrapper list-unstyled scroll-500">
//                            <li class="active"><a href="CharInfo.aspx" data-toggle="pill" class="main-toggle">Character Info</a>
//                                <ul style="list-style: none;">
//                                    <li>Demographics</li>
//                                    <li>Non Cost Descriptors</li>
//                                </ul>
//                            </li>
//                            <li><a href="CharItems.aspx" data-toggle="pill" class="main-toggle">Character Items</a>
//                                <ul style="list-style: none;">
//                                    <li>Costume</li>
//                                    <li>Makeup</li>
//                                    <li>Accessories</li>
//                                    <li>Armor and Weapons</li>
//                                    <li>Other Items</li>
//                                </ul>
//                            </li>
//                            <li><a href="CharHistory.aspx" data-toggle="pill" class="main-toggle">Character History</a></li>
//                            <li><a href="#character-skills" data-toggle="pill" class="main-toggle">Character Skills</a></li>
//                            <li><a href="#character-info-skills-requests" data-toggle="pill" class="main-toggle">Character Requests</a></li>
//                            <li><a href="#character-info-skills-requests" data-toggle="pill" class="main-toggle">Character Point/Build Mgmt</a></li>
//                        </ul>
//                    </div>
//                </nav>
//            </div>
//        </asp:TableCell>--%>







