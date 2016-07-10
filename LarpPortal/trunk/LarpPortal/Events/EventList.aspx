<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="LarpPortal.Events.EventList" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">
    <div class="mainContent tab-content col-lg-12">
        <div class="row col-lg-12" style="padding-left: -15px; padding-top: 10px; padding-bottom: 10px;">
            <div class="col-lg-5">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Event List" />
            </div>
            <div class="col-lg-3">
                <asp:CheckBox ID="cbDisplayOnlyOpenEvents" Text="Display Only Open Events" AutoPostBack="true" runat="server" />
            </div>
            <div class="col-lg-4 text-right">
                <asp:Button ID="btnCreate" Width="200px" runat="server" Style="" Text='Create New Event' CssClass="StandardButton" OnClick="btnCreate_Click"  />
            </div>
        </div>
        <div class="row col-lg-12" style="padding-left: 0px;">
            <asp:MultiView ID="mvPELList" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwPELList" runat="server">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Event List</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <div style="max-height: 500px; overflow-y: auto; margin-right: 10px;">
                                        <asp:GridView ID="gvPELList" runat="server" AutoGenerateColumns="false" OnRowCommand="gvPELList_RowCommand" GridLines="None"
                                            CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" 
                                            OnRowDataBound="gvPELList_RowDataBound" BorderWidth="1">
                                            <Columns>
                                                <asp:BoundField DataField="CampaignName" HeaderText="Campaign" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventName" HeaderText="Event Name" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventDescription" HeaderText="Event Description" HeaderStyle-Wrap="false" ItemStyle-Wrap="true"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="StartDateTime" HeaderText="Event Start Date" DataFormatString="{0: MM/dd/yyyy hh:mm tt}" ItemStyle-Wrap="false"
                                                    HeaderStyle-Wrap="false" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EndDateTime" HeaderText="Event End Date" DataFormatString="{0: MM/dd/yyyy hh:mm tt}" ItemStyle-Wrap="false"
                                                    HeaderStyle-Wrap="false" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventStatus" HeaderText="Event Status" ItemStyle-Wrap="false"
                                                    HeaderStyle-Wrap="false" ItemStyle-CssClass="LeftRightPassing" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit" Width="100px" runat="server" CommandArgument='<%# Eval("EventID") %>' CommandName='EDIT'
                                                            Style="" Text='Edit' CssClass="StandardButton" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCancel" Width="100px" runat="server" CommandArgument='<%# Eval("EventID") %>' CommandName='CANCELLED'
                                                            Style="" Text='Cancel' CssClass="StandardButton" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnComplete" Width="100px" runat="server" CommandArgument='<%# Eval("EventID") %>' CommandName='COMPLETED'
                                                            Style="" Text='Complete' CssClass="StandardButton" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDelete" Width="100px" runat="server" CommandArgument='<%# Eval("EventID") %>' CommandName='DELETEEVENT'
                                                            Style="" Text='Delete' CssClass="StandardButton" />
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
                        <strong>You do not have any open PEL for the campaign
                            <asp:Label ID="lblCampaignName" runat="server" />.
                        </strong>
                    </p>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
