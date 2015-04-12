<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CharPlace.aspx.cs" Inherits="LarpPortal.Character.CharPlace" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }
        }

        var cX = 0;
        var cY = 0;
        var rX = 0;
        var rY = 0;

        function UpdateCursorPosition(e) {
            cX = e.pageX;
            cY = e.pageY;
        }
        function UpdateCursorPositionDocAll(e) {
            cX = event.clientX;
            cY = event.clientY;
        }

        if (document.all) {
            document.onmousemove = UpdateCursorPositionDocAll;
        }
        else {
            document.onmousemove = UpdateCursorPosition;
        }

        function AssignPosition(d) {
            if (self.pageYOffset) {
                rX = self.pageXOffset;
                rY = self.pageYOffset;
            }
            else if (document.documentElement && document.documentElement.scrollTop) {
                rX = document.documentElement.scrollLeft;
                rY = document.documentElement.scrollTop;
            }
            else if (document.body) {
                rX = document.body.scrollLeft;
                rY = document.body.scrollTop;
            }
            if (document.all) {
                cX += rX;
                cY += rY;
            }
            d.style.left = (cX + 10) + "px";
            d.style.top = (cY + 10) + "px";
            d.style.left = "500px";
            d.style.top = "50px";
        }

        function HideContent(d) {
            if (d.length < 1) { return; }
            document.getElementById(d).style.display = "none";
        }

        function ShowContent(d) {
            if (d.length < 1) { return; }
            var dd = document.getElementById(d);
            AssignPosition(dd);
            dd.style.display = "block";
        }

        function ReverseContentDisplay(d) {
            if (d.length < 1) { return; }
            var dd = document.getElementById(d);
            AssignPosition(dd);
            if (dd.style.display == "none") { dd.style.display = "block"; }
            else { dd.style.display = "none"; }
        }
        //-->
    </script>
    <style type="text/css">
        body
        {
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
            font-size: 14px;
            line-height: 1.42857;
            /*color: #333333;
            background-color: #b2c68d;*/
        }

        .form-control
        {
            display: block;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857;
            color: #555555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            -webkit-transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
            transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
        }

        .form-horizontal .control-label
        {
            text-align: right;
            margin-bottom: 0;
            padding-top: 7px;
        }

        .label
        {
            display: inline-block;
            /*max-width: 100%;*/
            margin-bottom: 5px;
            font-weight: bold;
        }

        .TreeItems
        {
            text-decoration: none;
        }

        .TableItems
        {
            text-wrap: none;
            vertical-align: top;
        }

        .PointsLabel
        {
            text-align: right;
            padding-right: 10px;
            font-weight: bold;
        }

        .TextAlignRight
        {
            text-align: right;
        }

        .GridViewItem
        {
            padding-left: 5px;
            padding-right: 5px;
        }
    </style>
</head>
<body style="width: 95%; height: 580px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server" />
        <asp:UpdatePanel ID="upSkill" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="TableItems" style="vertical-align: top;">
                        <td style="width: 30%;" class="TableItems" rowspan="2">
                            <asp:Panel ID="pnlTreeView" runat="server" ScrollBars="Vertical" Height="500px">
                                <asp:TreeView ID="tvSkills" runat="server" SkipLinkText="" BorderColor="Black" BorderStyle="Solid" BorderWidth="0" ShowCheckBoxes="none"
                                    ShowLines="false" Font-Underline="false" CssClass="TreeItems"
                                    OnSelectedNodeChanged="tvSkills_SelectedNodeChanged"
                                    LeafNodeStyle-CssClass="TreeItems" NodeStyle-CssClass="TreeItems">
                                    <LevelStyles>
                                        <asp:TreeNodeStyle Font-Underline="false" />
                                    </LevelStyles>
                                </asp:TreeView>
                            </asp:Panel>
                        </td>
                        <td style="width: 70%; padding-right: 20px;" class="TableItems">
                            <asp:GridView ID="gvPlaces" runat="server" AutoGenerateColumns="false" GridLines="none"
                                AlternatingRowStyle-BackColor="Linen" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid"
                                OnRowCommand="gvPlaces_RowCommand" Caption="<span style='font-size: larger; font-weight: bold;'>Character Places</span>">
                                <Columns>
                                    <asp:BoundField DataField="PlaceName" HeaderText="Place Name" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewItem" />
                                    <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewItem" />
                                    <asp:BoundField DataField="Locale" HeaderText="Locale" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewItem" />
                                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="16" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CommandName="EditItem" CssClass="NoRightPadding"
                                                ImageUrl="~/img/edit.gif" CommandArgument='<%# Eval("CampaignPlaceID") %>' Width="16px"
                                                Visible='<%# Eval("ShowButton") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="16" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CommandName="DeleteItem" CssClass="NoRightPadding"
                                                ImageUrl="~/img/delete.png" CommandArgument='<%# Eval("CampaignPlaceID") %>' Width="16px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:MultiView ID="mvAddingItems" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vwNewItemButton" runat="server">
                                    <asp:Button ID="btnAddNewPlace" runat="server" Text="Add New Place" OnClick="btnAddNewPlace_Click" />
                                </asp:View>

                                <asp:View ID="vwNewPlace" runat="server">
                                    <table border="0">
                                        <tr>
                                            <td colspan="2"><b style="font-size: larger;">Add a new place for this character.</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><b>Place Name</b></td>
                                            <td><b>Located In</b></td>
                                            <td><b>Player Comments</b></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:TextBox ID="tbPlaceName" runat="server" Columns="30" /></td>
                                            <td style="vertical-align: top;">
                                                <asp:DropDownList ID="ddlLocalePlaces" runat="server" /></td>

                                            <td>
                                                <asp:TextBox ID="tbPlayerComments" runat="server" Columns="50" Rows="6" TextMode="MultiLine" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hidPlaceID" runat="server" /></td>
                                            <td>&nbsp;</td>
                                            <td align="right">
                                                <asp:Button ID="btnSaveNewPlace" runat="server" Text="Save New Place" OnClick="btnSaveNewPlace_Click" /></td>
                                        </tr>
                                    </table>
                                    <br />
                                </asp:View>

                                <asp:View ID="vwExistingPlace" runat="server">
                                    <table border="0">
                                        <tr>
                                            <td colspan="2"><b style="font-size: larger;">Add a campaign place to this character.</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><b>Place Name</b></td>
                                            <td><b>Locale</b></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:Label ID="lblPlaceName" runat="server" /></td>
                                            <td>
                                                <asp:Label ID="lblLocale" runat="server" /></td>
                                        </tr>
                                        <tr id="trAlreadySelected" runat="server">
                                            <td colspan="2">
                                                That place has already been added. Please choose another place.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnCancelAdding" runat="server" Text="Cancel" OnClick="btnCancelAdding_Click" /></td>
                                            <td align="right">
                                                <asp:Button ID="btnSaveExistingPlace" runat="server" Text="Save Existing Place" OnClick="btnSaveExistingPlace_Click" /></td>
                                        </tr>
                                    </table>
                                    <br />
                                </asp:View>
                            </asp:MultiView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" />
                        <td align="right">
                            <asp:Button ID="btnSave" runat="server" Text="&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;" OnClick="btnSave_Click" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
