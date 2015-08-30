using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Events
{
    public partial class EventRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["CampaignID"] == null)
                return;

            SortedList sParams = new SortedList();
            sParams.Add("@CampaignID", Session["CampaignID"]);

            DataTable dtCampaignEvents = Classes.cUtilities.LoadDataTable("uspGetCampaignEvents", sParams, "LARPortal", Session["UserName"].ToString(), "EventRegistrations.Page_PreRender");

            gvEvents.DataSource = dtCampaignEvents;
            gvEvents.DataBind();
        }

        protected void gvEvents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SortedList sParams = new SortedList();
            sParams.Add("@EventID", e.CommandArgument.ToString());
            DataTable dtEventInfo = Classes.cUtilities.LoadDataTable("uspGetEventInfo", sParams, "LARPortal", Session["UserName"].ToString(), "EventRegistration.gvEvents_RowCommand");

            pnlEventDetails.Style.Add("display", "none");

            foreach (DataRow dRow in dtEventInfo.Rows)
            {
                pnlEventDetails.Style.Add("display", "block");
                lblEventName.Text = dRow["EventName"].ToString();
                lblDesc.Text = dRow["EventDescription"].ToString();

                DateTime dtTemp;
                if (DateTime.TryParse(dRow["StartDateTime"].ToString(), out dtTemp))
                    lblStartDate.Text = dtTemp.ToString("MM/dd/yyyy HH:mm:ss");
                if (DateTime.TryParse(dRow["EndDateTime"].ToString(), out dtTemp))
                    lblEndDate.Text = dtTemp.ToString("MM/dd/yyyy HH:mm:ss");
                lblSiteName.Text = dRow["SiteName"].ToString().Trim() + ", " + dRow["SiteLocation"].ToString().Trim();

                if (DateTime.TryParse(dRow["RegistrationOpenDate"].ToString(), out dtTemp))
                    lblOpenRegDate.Text = dtTemp.ToString("MM/dd/yyyy");
                else
                    lblOpenRegDate.Text = "n/a";

                double dTemp;
                if (double.TryParse(dRow["PreregistrationPrice"].ToString(), out dTemp))
                    lblPreRegPrice.Text = dTemp.ToString("C");

                if (DateTime.TryParse(dRow["RegistrationOpenDate"].ToString(), out dtTemp))
                    lblPreRegDate.Text = dtTemp.ToString("MM/dd/yyyy hh:mm:ss");

                lblPreRegDate.Text = "What field?";

                lblRegPrice.Text = "What field?";

                if (DateTime.TryParse(dRow["InfoSkillDeadlineDate"].ToString(), out dtTemp))
                    lblInfoSkillDue.Text = dtTemp.ToString("MM/dd/yyyy");
                else
                    lblInfoSkillDue.Text = "n/a";

                lblAtDoorPrice.Text = "What field?";

                if (DateTime.TryParse(dRow["PELDeadlineDate"].ToString(), out dtTemp))
                    lblPELDue.Text = dtTemp.ToString("MM/dd/yyyy");
                else
                    lblPELDue.Text = "n/a";

                lblPCMealCombo.Text = "What field?";
                lblNPCMealCombo.Text = "What field?";

                //if (dRow["PCFoodService"].ToString() == "1")
                //    imgPCFoodService.ImageUrl = "~/img/checkbox.png";

                //if (dRow["NPCFoodService"].ToString() == "1")
                //    imgNPCFoodService.ImageUrl = "~/img/checkbox.png";

                //if (dRow["CookingFacilitiesAvailable"].ToString() == "1")
                //    imgCookingAllowed.ImageUrl = "~/img/checkbox.png";

            }
        }
    }
}





//<%--        <section id="character-info" class="character-info tab-pane active">

//            <div class="row">
//                <div class="col-sm-12">
//                    <asp:Label ID="lblHelp" runat="server" Text="Fill in Information to describe your character. Some items are automatically updated after events." />
//                </div>
//            </div>


//            <div class="row" style="padding-left: 15px;">
//                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
//                    <div class="panelheader">
//                        <h2>Character Information</h2>
//                        <div class="panel-body">
//                            <div class="panel-container search-criteria">
//                                <table class="CharInfoTable" border="0">
//                                    <tr>
//                                        <td class="TableLabel">Character</td>
//                                        <td>
//                                            <asp:TextBox ID="tbFirstName" runat="server" CssClass="TableTextBox" /></td>
//                                        <td>
//                                            <asp:TextBox ID="tbMiddleName" runat="server" CssClass="TableTextBox" /></td>
//                                        <td>
//                                            <asp:TextBox ID="tbLastName" runat="server" CssClass="TableTextBox" /></td>
//                                        <td class="TableLabel">Birthplace</td>
//                                        <td>
//                                            <asp:TextBox ID="tbOrigin" runat="server" CssClass="TableTextBox" /></td>
//                                        <td class="TableLabel">Status</td>
//                                        <td>
//                                            <asp:Label ID="lblStatus" runat="server" /><asp:DropDownList ID="ddlStatus" runat="server" Visible="false" /></td>
//                                        <td rowspan="4" style="width: 35px;">&nbsp;</td>
//                                        <td>To add a profile picture, use the browse button below.
//                                        </td>
//                                    </tr>

//                                    <tr>
//                                        <td class="TableLabel">AKA</td>
//                                        <td colspan="3">
//                                            <asp:TextBox ID="tbAKA" runat="server" CssClass="TableTextBox" /></td>
//                                        <td class="TableLabel">Home</td>
//                                        <td>
//                                            <asp:TextBox ID="tbHome" runat="server" CssClass="TableTextBox" /></td>
//                                        <td class="TableLabel">Last Event</td>
//                                        <td>
//                                            <asp:TextBox ID="tbDateLastEvent" runat="server" BackColor="LightGray" Enabled="false" /><asp:Label ID="lblDateLastEvent" runat="server" /></td>
//                                        <td>
//                                            <asp:FileUpload ID="ulFile" runat="server" />
//                                        </td>
//                                    </tr>

//                                    <tr>
//                                        <td class="TableLabel">Type</td>
//                                        <td colspan="1">
//                                            <asp:TextBox ID="tbType" runat="server" BackColor="LightGray" Enabled="false" /></td>
//                                        <td colspan="2"></td>
//                                        <td class="TableLabel">Team</td>
//                                        <td>
//                                            <asp:TextBox ID="tbTeam" runat="server" BackColor="LightGray" Enabled="false" />
//                                            <asp:Label ID="lblTeam" runat="server" /></td>
//                                        <td class="TableLabel"># of Deaths</td>
//                                        <td>
//                                            <asp:TextBox ID="tbNumOfDeaths" runat="server" BackColor="LightGray" Enabled="false" /></td>
//                                        <td style="text-align: right;">
//                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="StandardButton" OnClick="btnSavePicture_Click" Width="100" /></td>
//                                    </tr>

//                                    <tr style="vertical-align: top;">
//                                        <td class="TableLabel">DOB</td>
//                                        <td>
//                                            <asp:TextBox ID="tbDOB" runat="server" CssClass="TableTextBox" />
//                                        </td>
//                                        <td></td>
//                                        <td></td>
//                                        <td class="TableLabel">Race</td>
//                                        <td>
//                                            <asp:DropDownList ID="ddlRace" runat="server" /></td>
//                                        <td class="TableLabel">DOD</td>
//                                        <td>
//                                            <asp:TextBox ID="tbDOD" runat="server" BackColor="LightGray" Enabled="false" />
//                                            <asp:Label ID="lblDOD" runat="server" />
//                                        </td>
//                                        <th>
//                                            <asp:Image ID="imgCharacterPicture" runat="server" Width="125" /></th>
//                                    </tr>

//                                    <tr>
//                                        <td colspan="9"></td>
//                                        <td style="text-align: center;">
//                                            <asp:Button ID="btnClearPicture" runat="server" CssClass="StandardButton" Width="100" Text="Clear Picture" OnClick="btnClearPicture_Click" />
//                                        </td>
//                                    </tr>
//                                </table>
//                            </div>
//                        </div>
//                    </div>
//                </div>
//            </div>

//            <br />

//            <div class="row" style="padding-left: 15px;">
//                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
//                    <div class="panelheader">
//                        <h2>Non Cost Character Descriptors</h2>
//                        <div class="panel-body">
//                            <div class="panel-container search-criteria">
//                                <div style="margin-bottom: 2px; font-size: larger;">Select criteria that describes your character.</div>
//                                <asp:GridView ID="gvDescriptors" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowCommand="gvDescriptors_RowCommand"
//                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" DataKeyNames="CharacterAttributesBasicID"
//                                    CssClass="table table-striped table-hover table-condensed">
//                                    <EmptyDataRowStyle BackColor="Transparent" />
//                                    <EmptyDataTemplate>
//                                        <div class="row">
//                                            <div class="text-center" style="background-color: transparent;">You have no descriptors selected. Please select from the boxes below.</div>
//                                        </div>
//                                    </EmptyDataTemplate>
//                                    <Columns>
//                                        <asp:BoundField DataField="CharacterDescriptor" HeaderText="Character Descriptor" />
//                                        <asp:BoundField DataField="DescriptorValue" HeaderText="Value" />
//                                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="40" ItemStyle-Wrap="false">
//                                            <ItemTemplate>
//                                                <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CommandName="DeleteDesc" CssClass="NoRightPadding"
//                                                    ImageUrl="~/img/delete.png" CommandArgument='<%# Eval("CharacterAttributesBasicID") %>' Width="16px" />
//                                            </ItemTemplate>
//                                        </asp:TemplateField>
//                                    </Columns>
//                                </asp:GridView>
//                                <table class="CharInfoTable">
//                                    <tr>
//                                        <td><b>Character Descriptor</b></td>
//                                        <td>
//                                            <asp:DropDownList ID="ddlDescriptor" runat="server" Style="min-width: 150px;" AutoPostBack="true" OnSelectedIndexChanged="ddlDescriptor_SelectedIndexChanged" /></td>
//                                    </tr>
//                                    <tr>
//                                        <td style="text-align: right;"><b>Name</b></td>
//                                        <td>
//                                            <asp:DropDownList ID="ddlName" runat="server" Style="min-width: 150px;" /></td>
//                                    </tr>
//                                    <tr>
//                                        <td></td>
//                                        <td style="text-align: right">
//                                            <asp:Button ID="btnAddDesc" runat="server" Width="100px" Text="Add" CssClass="StandardButton" OnClick="btnAddDesc_Click" /></td>
//                                    </tr>
//                                </table>
//                            </div>
//                        </div>
//                    </div>
//                </div>
//            </div>

//            <div class="row" style="padding-left: 15px; text-align: right; padding-top: 10px;">
//                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="StandardButton" Width="100px" OnClick="btnSave_Click" />
//            </div>--%>


//        </section>
//    </div>