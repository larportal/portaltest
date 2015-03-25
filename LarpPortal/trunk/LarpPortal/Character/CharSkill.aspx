<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CharSkill.aspx.cs" Inherits="LarpPortal.Character.CharSkill" %>

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
    </style>
</head>
<body style="width: 95%; height: 580px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server" />
        <asp:UpdatePanel ID="upSkill" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="TableItems" style="vertical-align: top;">
                        <td style="width: 40%;" class="TableItems">
                            <asp:Panel ID="pnlTreeView" runat="server" ScrollBars="Vertical" Height="500px">
                                <asp:TreeView ID="tvSkills" runat="server" SkipLinkText="" BorderColor="Black" BorderStyle="Solid" BorderWidth="0" ShowCheckBoxes="All"
                                    ShowLines="false" OnTreeNodeCheckChanged="tvSkills_TreeNodeCheckChanged" Font-Underline="false" CssClass="TreeItems"
                                    LeafNodeStyle-CssClass="TreeItems" NodeStyle-CssClass="TreeItems">
                                    <LevelStyles>
                                        <asp:TreeNodeStyle Font-Underline="false" />
                                    </LevelStyles>
                                </asp:TreeView>
                            </asp:Panel>
                        </td>
                        <td style="width: 40%; padding-right: 20px;" class="TableItems">
                            <div id="divDesc" />
                            <br />
                            <asp:TextBox ID="tbPlayerComments" runat="server" Visible="false" />
                        </td>
                        <td style="width: 20%;" class="TableItems" align="right">
                            <asp:GridView ID="gvCostList" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvCostList_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Skill" HeaderText="Skill" />
                                    <asp:BoundField DataField="Cost" HeaderText="Cost" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" />
                        <td align="right"><asp:Button ID="btnSave" runat="server" Text="&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;" OnClick="btnSave_Click" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
