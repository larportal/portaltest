using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class MultipleCharCards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<int> lCharIDs = new List<int>();
            string sProcedureName = "";

            SortedList sParams = new SortedList();
            if (Request.QueryString["EventID"] != null)
            {
                int iEventID = 0;
                if (int.TryParse(Request.QueryString["EventID"], out iEventID))
                    sParams.Add("@EventID", iEventID);
                sProcedureName = "prGetCharactersForEvent";
            }
            else if (Request.QueryString["CampaignID"] != null)
            {
                int iCampaignID = 0;
                if (int.TryParse(Request.QueryString["CampaignID"], out iCampaignID))
                    sParams.Add("@CampaignID", iCampaignID);
                sProcedureName = "uspGetCharactersForCampaign";
            }
            else if (Request.QueryString["CharacterID"] != null)
            {
                int iCharacterID = 0;
                if (int.TryParse(Request.QueryString["CharacterID"], out iCharacterID))
                    sParams.Add("@CharacterID", iCharacterID);
                sProcedureName = "uspGetCharactersForCampaign";
            }

            if (sParams.Count == 0)
                return;

            DataTable dtChars = new DataTable();

            dtChars = Classes.cUtilities.LoadDataTable(sProcedureName, sParams, "LARPortal", Session["UserID"].ToString(), "CharCard.Page_Load.GetChar");

            rptrCharacter.DataSource = dtChars;
            rptrCharacter.DataBind();
        }

        protected void rptrCharacter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hidID = (HiddenField)e.Item.FindControl("hidCharID");
            if (hidID != null)
            {
                int iCharID;
                if (int.TryParse(hidID.Value, out iCharID))
                {

                    Classes.cCharacter cChar = new Classes.cCharacter();
                    cChar.LoadCharacter(iCharID);

                    Label lblTotalCP = (Label)e.Item.FindControl("lblTotalCP");
                    if (lblTotalCP != null)
                        lblTotalCP.Text = cChar.TotalCP.ToString("0.00");

                    DataTable dtCharacterSkills = new DataTable();
                    SortedList sParams = new SortedList();
                    sParams.Add("@CharacterID", cChar.CharacterID);

                    MethodBase lmth = MethodBase.GetCurrentMethod();
                    string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                    dtCharacterSkills = Classes.cUtilities.LoadDataTable("uspGetCharCardSkills", sParams, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

                    if (dtCharacterSkills.Columns["FullDescription"] == null)
                        dtCharacterSkills.Columns.Add(new DataColumn("FullDescription", typeof(string)));

                    double CPCost;
                    double CPSpent = 0.0;

                    foreach (DataRow dSkillRow in dtCharacterSkills.Rows)
                    {
                        if (double.TryParse(dSkillRow["CPCostPaid"].ToString(), out CPCost))
                            CPSpent += CPCost;

                        string FullDesc = "";
                        bool bDisplay;
                        if (bool.TryParse(dSkillRow["CardDisplayDescription"].ToString(), out bDisplay))
                        {
                            if (bDisplay)
                            {
                                if (dSkillRow["SkillCardDescription"].ToString().Trim().Length > 0)
                                    FullDesc += dSkillRow["SkillCardDescription"].ToString().Trim() + "; ";
                            }
                        }

                        if (dSkillRow["PlayerDescription"].ToString().Trim().Length > 0)
                            FullDesc += dSkillRow["PlayerDescription"].ToString().Trim() + "; ";

                        if (bool.TryParse(dSkillRow["CardDisplayIncant"].ToString(), out bDisplay))
                        {
                            if (bDisplay)
                            {
                                if (dSkillRow["SkillIncant"].ToString().Trim().Length > 0)
                                    FullDesc += "<i>" + dSkillRow["SkillIncant"].ToString().Trim() + "</i>; ";
                            }
                        }

                        if (dSkillRow["PlayerIncant"].ToString().Trim().Length > 0)
                            FullDesc += "<b><i>" + dSkillRow["PlayerIncant"].ToString().Trim() + "</b></i>";

                        FullDesc = FullDesc.Trim();
                        if (FullDesc.EndsWith(";"))
                            FullDesc = FullDesc.Substring(0, FullDesc.Length - 1);

                        dSkillRow["FullDescription"] = FullDesc;
                    }

                    Label lblCPSpent = (Label)e.Item.FindControl("lblCPSpent");
                    if (lblCPSpent != null)
                        lblCPSpent.Text = CPSpent.ToString("0.00");

                    Label lblCPAvail = (Label)e.Item.FindControl("lblCPAvail");
                    if (lblCPAvail != null)
                        lblCPAvail.Text = (cChar.TotalCP - CPSpent).ToString("0.00");

                    Dictionary<string, string> NonCost = new Dictionary<string, string>();

                    foreach (Classes.cDescriptor Desc in cChar.Descriptors)
                    {
                        if (NonCost.ContainsKey(Desc.CharacterDescriptor))
                            NonCost[Desc.CharacterDescriptor] += Desc.DescriptorValue + ", ";
                        else
                            NonCost.Add(Desc.CharacterDescriptor, Desc.DescriptorValue + ", ");
                    }

                    foreach (string KeyValue in NonCost.Keys.ToList())
                    {
                        if (NonCost[KeyValue].EndsWith(", "))
                            NonCost[KeyValue] = NonCost[KeyValue].Substring(0, NonCost[KeyValue].Length - 2);
                    }

                    DataView dvSkills = new DataView(dtCharacterSkills, "", "DisplayOrder", DataViewRowState.CurrentRows);
                    GridView gvSkills = (GridView)e.Item.FindControl("gvSkills");
                    if (gvSkills != null)
                    {
                        gvSkills.DataSource = dvSkills;
                        gvSkills.DataBind();
                    }

                    GridView gvNonCost = (GridView)e.Item.FindControl("gvNonCost");
                    if (gvNonCost != null)
                    {
                        gvNonCost.DataSource = NonCost;
                        gvNonCost.DataBind();
                    }

                    //Classes.cUser User = new Classes.cUser(Session["Username"].ToString(), "PasswordNotNeeded");

                    //Label lblPlayerName = (Label)e.Item.FindControl("lblPlayerName");
                    //if (lblPlayerName != null)
                    //    lblPlayerName.Text = User.FirstName + " " + User.MiddleName + " " + User.LastName;
                }
            }
        }
    }
}












//<asp:Repeater ID="rptrCharacter" runat="server">
//    <ItemTemplate>
//        <table border="0">
//            <tr style="vertical-align: top;">
//                <td colspan="6">
//                    <asp:Label ID="lblCharName" runat="server" CssClass="HeaderLabel" /></td>
//                <td style="text-align: right; width: 300px;" class="hiddenOnPrint" rowspan="4">
//                    <asp:Button ID="printButton" runat="server" CssClass="PrintButton" Text="Print" OnClientClick="javascript:window.print();" /></td>
//            </tr>
//            <tr>
//                <td class="TableLabel">Common Name: </td>
//                <td>
//                    <asp:Label ID="lblAKA" runat="server" /></td>
//                <td class="TableLabel">Full Name: </td>
//                <td colspan="3">
//                    <asp:Label ID="lblFullName" runat="server" /></td>
//            </tr>
//            <tr>
//                <td class="TableLabel">Race: </td>
//                <td>
//                    <asp:Label ID="lblRace" runat="server" /></td>
//                <td class="TableLabel">World: </td>
//                <td>
//                    <asp:Label ID="lblOrigin" runat="server" /></td>
//                <td class="TableLabel">Player Name: </td>
//                <td>
//                    <asp:Label ID="lblPlayerName" runat="server" /></td>
//            </tr>
//            <tr>
//                <td class="TableLabel">Total CP: </td>
//                <td>
//                    <asp:Label ID="lblTotalCP" runat="server" /></td>
//                <td class="TableLabel">Total Spent: </td>
//                <td>
//                    <asp:Label ID="lblCPSpent" runat="server" /></td>
//                <td class="TableLabel">Total Avail: </td>
//                <td>
//                    <asp:Label ID="lblCPAvail" runat="server" /></td>
//            </tr>
//        </table>
//        <br />
//        <br />
//        <asp:GridView ID="gvNonCost" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray" AlternatingRowStyle-BackColor="Linen">
//            <Columns>
//                <asp:BoundField DataField="Key" HeaderText="Descriptor" HeaderStyle-CssClass="LeftRightPadding" ItemStyle-CssClass="LeftRightPadding" />
//                <asp:BoundField DataField="Value" HeaderText="Descriptor Value" HeaderStyle-CssClass="LeftRightPadding" ItemStyle-CssClass="LeftRightPadding" />
//            </Columns>
//        </asp:GridView>
//        <br />
//        <br />
//        <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="false" RowStyle-VerticalAlign="top" HeaderStyle-BackColor="LightGray" AlternatingRowStyle-BackColor="Linen">
//            <Columns>
//                <asp:BoundField DataField="SkillName" HeaderText="Skill" HeaderStyle-CssClass="LeftRightPadding" ItemStyle-CssClass="LeftRightPadding" ItemStyle-Wrap="false" />
//                <asp:BoundField DataField="CPCostPaid" ItemStyle-HorizontalAlign="Right" HeaderText="Cost" HeaderStyle-CssClass="LeftRightPadding" ItemStyle-CssClass="LeftRightPadding"
//                    DataFormatString="{0:0.00}" />
//                <asp:BoundField DataField="FullDescription" ItemStyle-HorizontalAlign="Left" HeaderText="Complete Card Description" HtmlEncode="false" ItemStyle-CssClass="LeftRightPadding"
//                    HeaderStyle-CssClass="LeftRightPadding" />
//                <asp:BoundField DataField="DisplaySkill" Visible="false" />
//                <asp:BoundField DataField="DisplayOrder" Visible="false" />
//            </Columns>
//        </asp:GridView>
//    </ItemTemplate>
//</asp:Repeater>
