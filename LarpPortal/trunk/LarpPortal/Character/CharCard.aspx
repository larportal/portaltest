<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="CharCard.aspx.cs" Inherits="LarpPortal.Character.CharCard" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .TableLabel
        {
            font-weight: bold;
            text-align: right;
            padding-left: 10px;
            padding-right: 5px;
            font-size: 10pt;
        }

        .LeftRightPadding
        {
            padding-left: 10px;
            padding-right: 10px;
        }

        .PrintButton
        {
            padding-left: 10px;
            padding-right: 10px;
            font-size: 16px;
        }

        .HeaderLabel
        {
            font-size: 24px;
            font-weight: bold;
        }

        @media print
        {
            Label
            {
                font-size: 10pt;
            }
            .hiddenOnPrint
            {
                display: none;
            }
        }
        Label
        {
            font-size: 10pt;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%" border="0">
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblCharName" runat="server" CssClass="HeaderLabel" /></td>
                    <td colspan="2" style="text-align: right;">
                        <asp:Button ID="printButton" runat="server" CssClass="PrintButton hiddenOnPrint" Text="Print" OnClientClick="javascript:window.print();" /></td>
                </tr>
                <tr>
                    <td class="TableLabel">Common Name: </td>
                    <td>
                        <asp:Label ID="lblAKA" runat="server" /></td>
                    <td class="TableLabel">Full Name: </td>
                    <td colspan="3">
                        <asp:Label ID="lblFullName" runat="server" /></td>
                </tr>
                <tr>
                    <td class="TableLabel">Race: </td>
                    <td>
                        <asp:Label ID="lblRace" runat="server" /></td>
                    <td class="TableLabel">World: </td>
                    <td>
                        <asp:Label ID="lblOrigin" runat="server" /></td>
                    <td class="TableLabel">Player Name: </td>
                    <td>
                        <asp:Label ID="lblPlayerName" runat="server" /></td>
                </tr>
            </table>
            <br />
            <br />
            <asp:GridView ID="gvNonCost" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray" AlternatingRowStyle-BackColor="Linen">
                <Columns>
                    <asp:BoundField DataField="Key" HeaderText="Descriptor" HeaderStyle-CssClass="LeftRightPadding" ItemStyle-CssClass="LeftRightPadding" />
                    <asp:BoundField DataField="Value" HeaderText="Descriptor Value" HeaderStyle-CssClass="LeftRightPadding" ItemStyle-CssClass="LeftRightPadding" />
                </Columns>
            </asp:GridView>
            <br />
            <br />
            <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="false" RowStyle-VerticalAlign="top" HeaderStyle-BackColor="LightGray" AlternatingRowStyle-BackColor="Linen">
                <Columns>
                    <asp:BoundField DataField="SkillName" HeaderText="Skill" HeaderStyle-CssClass="LeftRightPadding" ItemStyle-CssClass="LeftRightPadding" />
                    <asp:BoundField DataField="CPCostPaid" ItemStyle-HorizontalAlign="Right" HeaderText="Cost" HeaderStyle-CssClass="LeftRightPadding" ItemStyle-CssClass="LeftRightPadding" />
                    <asp:BoundField DataField="FullDescription" ItemStyle-HorizontalAlign="Left" HeaderText="Complete Card Description" HtmlEncode="false" ItemStyle-CssClass="LeftRightPadding"
                        HeaderStyle-CssClass="LeftRightPadding" />
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
