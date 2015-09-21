<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CharHistoryApprovalList.aspx.cs" Inherits="LarpPortal.StaffFunctions.CharHistoryApprovalList" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ID="Styles" ContentPlaceHolderID="MemberStyles" runat="server">
    <style type="text/css">
        div
        {
            border: 0px solid black;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="CharHistApp" runat="server">
    <div class="mainContent tab-content col-sm-12" style="padding-left: 0px; padding-right: 0px;">
        <div class="row col-lg-12" style="padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: bold" Text="Character History Approval" />
        </div>
        <br />

        <div class="row col-lg-12">
            <asp:Image ID="imgBlank1" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
            Character Name:
                    <asp:DropDownList ID="ddlCharacterName" runat="server" AutoPostBack="true" />
            <asp:Image ID="imgBlank2" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
            Player Name:
                    <asp:DropDownList ID="ddlPlayerName" runat="server" AutoPostBack="true" />
            <asp:Image ID="imgBlank3" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
            PEL Status:
                    <asp:DropDownList ID="ddlHistoryStatus" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="No Filter" Value="" Selected="True" />
                        <asp:ListItem Text="Approved" Value="Approved" />
                        <asp:ListItem Text="Submitted" Value="Submitted" />
                        <asp:ListItem Text="Not Submitted" Value="Not Submitted" />
                    </asp:DropDownList>
        </div>

        <div class="row col-lg-12" style="padding-top: 10px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>Characters</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                            <div style="max-height: 500px; overflow-y: auto;">
                                <asp:GridView ID="gvCharacterList" runat="server" AutoGenerateColumns="false" OnRowCommand="gvCharacterList_RowCommand" GridLines="None"
                                    CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Width="99%">
                                    <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" Font-Size="24pt" />
                                    <EmptyDataTemplate>
                                        There are no character histories that meet your criteria.
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="CampaignName" HeaderText="Campaign" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="PlayerName" HeaderText="Player Name" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <%--                                        <asp:BoundField DataField="RoleAlignment" HeaderText="Role" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />--%>
                                        <asp:BoundField DataField="CharacterAKA" HeaderText="Character" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="HistoryStatus" HeaderText="Status" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                            <ItemTemplate>
                                                <asp:Button ID="btnCommand" runat="server" CommandArgument='<%# Eval("CharacterID") %>' Text='View' CssClass="StandardButton" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
