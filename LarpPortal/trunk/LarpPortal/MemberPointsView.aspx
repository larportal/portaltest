<%@ Page Title="" Language="C#" MasterPageFile="~/MemberPoints.master" AutoEventWireup="true" CodeBehind="MemberPointsView.aspx.cs" Inherits="LarpPortal.MemberPointsView" %>

<asp:Content ID="MemberPoints" ContentPlaceHolderID="PointsContent" runat="server">
    <div class="contentArea">
        <div role="form" class="form-horizontal form-inline" style="padding-left: 30px;">
            <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="View Character Points" />
            </div>
            <div class="panel">
                <div class="panel-header">
                    <h2>Total Points<asp:Label ID="lblGridHeader" runat="server"></asp:Label></h2>
                </div>
                <div class="panel-body search-criteria" style="padding-bottom: 5px;">
                    <div style="max-height: 500px; overflow-y: auto;">
                        <asp:GridView ID="gvPointsList" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-striped table-hover table-condensed" 
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" OnRowDataBound="gvPointsList_RowDataBound" OnRowCommand="gvPointsList_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="ReceiptDate" HeaderText=" Earn Date" DataFormatString="{0: MM/dd/yyyy}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="FullDescription" HeaderText="Type" ItemStyle-Wrap="false"
                                    HeaderStyle-Wrap="false" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="AdditionalNotes" HeaderText="Description" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="CPAmount" HeaderText="Points" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="StatusName" HeaderText="Status" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="CPApprovedDate" HeaderText="Spend Date" DataFormatString="{0: MM/dd/yyyy}" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="RecvFromCampaign" HeaderText="Earned At" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="OwningPlayer" HeaderText="Earned By" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="ReceivingCampaign" HeaderText="Spent At" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="Character" HeaderText="Spent On" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="ReceivingPlayer" HeaderText="Transfer To" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:BoundField DataField="CPApprovedDate" HeaderText="Approved" DataFormatString="{0: MM/dd/yyyy}" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:TemplateField HeaderText="Apply To">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCharacters" runat="server" Visible="true"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:Button ID="btnApplyBanked" runat="server" Visible="true" CommandName="ApplyBanked" Text="Apply" CssClass="StandardButton" 
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidSentToCampaignPlayerID" runat="server" Value='<%# Eval("SentToCampaignPlayerID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidPlayerCPAuditID" runat="server" Value='<%# Eval("PlayerCPAuditID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidTotalCharacterCap" runat="server" Value='<%# Eval("TotalCharacterCap") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
