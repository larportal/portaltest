<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DevelopmentList.aspx.cs" Inherits="LarpPortal.DevelopmentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Development List</title>
    <style type="text/css">
        .LeftRightPadding
        {
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="text-align: center;">
                <h1>Development List</h1>
                Display Tasks For:
                <asp:DropDownList ID="ddlWho" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWho_SelectedIndexChanged" /><asp:Image ID="imgBlank1" runat="server"
                    ImageUrl="~/img/blank.gif" Width="25px" Height="0" /><asp:CheckBox ID="cbIncludeCompleted" runat="server" Text="Include Completed" AutoPostBack="true" />
                    <asp:Image ID="imgBlank2" runat="server" ImageUrl="~/img/blank.gif" Width="25px" Height="0" /><asp:Button ID="btnSubmit" runat="server"
                    Text="Load Data" CssClass="LeftRightPadding" OnClick="btnSubmit_Click" />
            </div>
            <br />
            <asp:GridView ID="gvDevelopmentList" runat="server" AutoGenerateColumns="false" RowStyle-VerticalAlign="Top" OnSorting="gvDevelopmentList_Sorting"
                AlternatingRowStyle-BackColor="Linen" HeaderStyle-BackColor="LightGray" AllowSorting="true">
                <Columns>
                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" 
                        SortExpression="Description" />
                    <asp:BoundField DataField="PrioritySequence" HeaderText="Priority" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" 
                        ItemStyle-HorizontalAlign="Right" SortExpression="PrioritySequence" />
                    <asp:BoundField DataField="RequestedBy" HeaderText="Requested By" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" 
                        SortExpression="RequestedBy" />
                    <asp:BoundField DataField="AssignedTo" HeaderText="Assigned To" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" SortExpression="AssignedTo" 
                        DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="RequestedCompletionDate" HeaderText="Req Complete Date" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" 
                        SortExpression="RequestedCompletionDate" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="ProjectedCompletionDate" HeaderText="Projected Completed Date" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" 
                        SortExpression="ProjectedCompletionDate" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="DateCompleted" HeaderText="Date Completed" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" 
                        SortExpression="DateCompleted" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="DateAdded" HeaderText="Date Added" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" 
                        SortExpression="DateAdded" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="PMComments" HeaderText="PM Comments" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
