<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemErrors.aspx.cs" Inherits="LarpPortal.SystemErrors" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="text-align: center;">
            <h1>System Errors</h1>
                How much data to display:
                <asp:DropDownList ID="ddlDataAmount" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDataAmount_SelectedIndexChanged">
                    <asp:ListItem Text="Last 24 hours" Value="D" Selected="true" />
                    <asp:ListItem Text="Today Only" Value="T" />
                    <asp:ListItem Text="The Past Week" Value="W" />
                    <asp:ListItem Text="The Past Month" Value="M" />
                </asp:DropDownList>
            </div>
            <br />
            <asp:GridView ID="gvErrors" runat="server" AutoGenerateColumns="false" RowStyle-VerticalAlign="Top" AlternatingRowStyle-BackColor="Linen" HeaderStyle-BackColor="LightGray">
                <Columns>
                    <asp:TemplateField>
                        <ItemStyle Wrap="false" />
                        <HeaderTemplate>
                            Error Information
                        </HeaderTemplate>
                        <ItemTemplate>
                        <b>When Happened:</b> <%# Eval("WhenHappened") %><br />
                            <b>Error Location:</b> <%# Eval("ErrorLocation") %><br />
                            <b>Error Type:</b> <%# Eval("ErrorType") %><br />
                            <b>System Errors ID:</b> <%# Eval("SystemErrorsID") %><br />
                            <b>Session ID:</b>  <%# Eval("SessionID") %><br />
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ErrorMessage" HeaderText="Error Message" HtmlEncode="false" />
                    <asp:BoundField DataField="AddInfo" HeaderText="Add Info" HtmlEncode="false" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
