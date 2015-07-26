<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PELApprovalList.aspx.cs" Inherits="LarpPortal.PELs.PELApprovalList" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="PEL (Post Event Letter)" />
        </div>
        <%--        <div class="row" style="padding-left: 15px;">
            Campaigns with Unapproved PELS:
            <asp:DropDownList ID="ddlCampaignWithPELCount" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaignWithPELCount_SelectedIndexChanged" />
        </div>--%>
        <br />

        <asp:MultiView ID="mvPELs" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwPELs" runat="server">
                <div class="row" style="padding-left: 15px; padding-right: 15px;">
                    <asp:Image ID="imgBlank1" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                    Event Date:
                    <asp:DropDownList ID="ddlEventDate" runat="server" AutoPostBack="true" />
                    <asp:Image ID="imgBlank2" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                    Character Name:
                    <asp:DropDownList ID="ddlCharacterName" runat="server" AutoPostBack="true" />
                    <asp:Image ID="imgBlank3" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                    Event Name:
                    <asp:DropDownList ID="ddlEventName" runat="server" AutoPostBack="true" />
                    <asp:Image ID="imgBlank4" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                    PEL Status:
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" />
                </div>

                <div class="row" style="padding-left: 15px; padding-right: 15px; padding-top: 10px;">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Event List</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <div style="max-height: 500px; overflow-y: auto;">
                                        <asp:GridView ID="gvPELList" runat="server" AutoGenerateColumns="false" OnRowCommand="gvPELList_RowCommand" GridLines="None"
                                            CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Width="99%">
                                            <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" Font-Size="24pt" />
                                            <EmptyDataTemplate>
                                                There are no PELs that meet your criteria.
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="CampaignName" HeaderText="Campaign" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventStartDate" HeaderText="Event Date" DataFormatString="{0: MM/dd/yyyy}" ItemStyle-Wrap="false"
                                                    HeaderStyle-Wrap="false" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="CharacterAKA" HeaderText="Character" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventName" HeaderText="Event" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventDescription" HeaderText="Event Description" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="PELStatus" HeaderText="Status" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCommand" runat="server" CommandArgument='<%# Eval("RegistrationID") %>' CommandName='<%# Eval("ButtonText") %>Item'
                                                            Text='View' CssClass="StandardButton" />
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
                    There are no PELs waiting to be processed.
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
