<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="SkillTable.aspx.cs" Inherits="LarpPortal.SkillTable" %>

<html>
<head>
    <title></title>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>

    <script type="text/javascript">

        function GetContent(d) {
            $.ajax({
                contentType: "application/json; charset=utf-8",
                data: "{'SkillNodeID':'" + d.toString() + "'}",
                url: "/SkillNodeInfo.asmx/getSkillNodeRequirements",
                type: "POST",
                dataType: 'json',
                success: function (result) {
                    divDesc.innerHTML = result.d;
                },
                error: function (xhr, msg) {
                    alert("error: " + xhr.responseText + "   " + msg.toString());
                }
            });
        }
    </script>
</head>
<body>

    <form runat="server">

        <table>
            <tr style="vertical-align: top;">
                <td style="max-width: 400px; min-width: 300px;">
                    <div id="pnlTreeView" style="height: 500px; overflow-y: scroll;">

                        <asp:TreeView ID="tvSkills" runat="server" SkipLinkText="" BorderColor="Black" BorderStyle="Solid" BorderWidth="0" ShowCheckBoxes="All"
                            ShowLines="false" Font-Underline="false" CssClass="TreeItems" EnableClientScript="false"
                            LeafNodeStyle-CssClass="TreeItems" NodeStyle-CssClass="TreeItems">
                            <LevelStyles>
                                <asp:TreeNodeStyle Font-Underline="false" />
                            </LevelStyles>
                        </asp:TreeView>
                    </div>
                </td>
                <td>
                    <div id="divDesc" />
                </td>
            </tr>
        </table>

    </form>
</body>
</html>

