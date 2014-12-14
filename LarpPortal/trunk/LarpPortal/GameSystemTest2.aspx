<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameSystemTest2.aspx.cs" Inherits="LarpPortal.GameSystemTest2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Table runat="server" Width="569px">
            <asp:TableRow>
                <asp:TableCell align="right">Game System ID:&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbGameSystemID" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell align="right">Game System Name:&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbGameSystemName" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell align="right">URL:&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbGameSystemURL" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="tbGameSystemWebPageDescription" runat="server"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnUpdateGameSystem" runat="server" Text="Update" OnClick="btnUpdateGameSystem_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnAddGameSystem" runat="server" Text="Add" OnClick="btnAddGameSystem_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnDeleteGameSystem" runat="server" Text="Delete" OnClick="btnDeleteGameSystem_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell align="right">Player Role ID:&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbPlayerRoleID" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </form>
</body>
</html>
