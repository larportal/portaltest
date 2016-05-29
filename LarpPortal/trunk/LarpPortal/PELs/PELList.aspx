﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PELList.aspx.cs" Inherits="LarpPortal.PELs.PELList" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="PEL (Post Event Letter)" />
        </div>
        <div class="row" style="padding-left: 15px;">
            <asp:Label ID="lblCharInfo" runat="server" />
        </div>
        <div class="row" style="padding-left: 15px;">
            <asp:MultiView ID="mvPELList" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwPELList" runat="server">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Event List</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <div style="max-height: 500px; overflow-y: auto; margin-right: 10px;">
                                        <asp:GridView ID="gvPELList" runat="server" AutoGenerateColumns="false" OnRowCommand="gvPELList_RowCommand" GridLines="None"
                                            CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" BorderWidth="1">
                                            <Columns>
                                                <asp:BoundField DataField="CampaignName" HeaderText="Campaign" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventStartDate" HeaderText="Event Date" DataFormatString="{0: MM/dd/yyyy}" ItemStyle-Wrap="false"
                                                    HeaderStyle-Wrap="false" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="CharacterAKA" HeaderText="Character" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventName" HeaderText="Event Name" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventDescription" HeaderText="Event Description" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="PELStatus" HeaderText="Status" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAddendum" runat="server" CommandName="Addendum" Text="&nbsp;&nbsp;Add Addendum&nbsp;&nbsp;" Width="100px" CssClass="StandardButton" 
                                                            Visible='<%# Eval("DisplayAddendum") %>' CommandArgument='<%# Eval("RegistrationID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCommand" Width="100px" runat="server" CommandArgument='<%# Eval("RegistrationID") %>' CommandName='<%# Eval("ButtonText") %>Item'
                                                            Style="" Text='<%# Eval("ButtonText") %>' CssClass="StandardButton" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vwNoPELs" runat="server">
                    <p>
                        <strong>You do not have any open PEL for the campaign <asp:Label ID="lblCampaignName" runat="server" />.
                        </strong>
                    </p>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
