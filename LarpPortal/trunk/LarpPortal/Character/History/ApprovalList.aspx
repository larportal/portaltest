<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalList.aspx.cs" Inherits="LarpPortal.Character.History.ApprovalList" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ContentPlaceHolderID="MemberStyles" ID="ApprovalListStyles" runat="server">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
        function closeMessage() {
            $('#modelMessage').hide();
        }
    </script>

    <script src="../../Scripts/jquery-1.11.3.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/bootstrap.js"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="HistoryApprovalList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Histories" />
        </div>

        <asp:MultiView ID="mvPELs" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwPELs" runat="server">
                <div class="row" style="padding-left: 15px; padding-right: 15px; padding-top: 10px;">
<%--                    <asp:Image ID="imgBlank1" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />--%>
                    Character Name:
                    <asp:DropDownList ID="ddlCharacterName" runat="server" AutoPostBack="true" />
                    <asp:Image ID="imgBlank2" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                    Status:
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="No Filter" Value="" />
                        <asp:ListItem Text="Approved Only" Value="A" />
                        <asp:ListItem Text="Submitted Only" Value="S" />
                    </asp:DropDownList>
                    <asp:Image ID="imgBlank3" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                    <asp:Button ID="btnApproveAll" runat="server" Text="Approve All" CssClass="StandardButton" OnClick="btnApproveAll_Click" Width="125" Visible="false" />
                </div>

                <div class="row" style="padding-left: 15px; padding-right: 15px; padding-top: 10px;">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Character Histories</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <div style="max-height: 500px; overflow-y: auto;">
                                        <asp:GridView ID="gvHistoryList" runat="server" AutoGenerateColumns="false" OnRowCommand="gvHistoryList_RowCommand" GridLines="None"
                                            CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Width="99%">
                                            <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" Font-Size="24pt" />
                                            <EmptyDataTemplate>
                                                There are no histories that meet your criteria.
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hidCharacterID" runat="server" Value='<%# Eval("CharacterID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CampaignName" HeaderText="Campaign" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="PlayerName" HeaderText="Player Name" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="CharacterAKA" HeaderText="Character" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />

                                                <asp:BoundField DataField="ShortHistory" HeaderText="History" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />

                                                <asp:BoundField DataField="HistoryStatus" HeaderText="Status" 
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnView" runat="server" CommandArgument='<%# Eval("CharacterID") %>' CommandName='View'
                                                            Text='View' CssClass="StandardButton LeftRightPadding" Width="100px" />
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
            </asp:View>
            <asp:View ID="vwNoPELs" runat="server">
                <div class="row" style="padding-top: 30px; padding-left: 30px; color: red; font-weight: bold; font-size: 24pt">
                    There are no histories waiting to be processed.
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
