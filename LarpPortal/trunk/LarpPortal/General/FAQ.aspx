<%@ Page Title="" Language="C#" MasterPageFile="~/LARPing.master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="LarpPortal.General.FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LARPingContent" runat="server">
    <div class="contentArea">
        <div role="form" class="form-horizontal form-inline" style="padding-left: 30px;">
            <br />
            &nbsp;&nbsp;
            <asp:Panel ID="pnlFAQ" runat="server" CssClass="panel" Width="900px" ScrollBars="Auto" Wrap="true">
                <div class="panel-header">
                    <h2><asp:Label ID="lblFAQHeader" runat="server" Text="FAQs"></asp:Label></h2>
                     <div class="panel-body" style="padding-bottom: 5px">
                        <div class="panel-container search-criteria">
                            <div style="max-height: 500px; overflow-y: auto;">
                                <asp:TreeView ID="tvFAQ" runat="server" Visible="true" OnSelectedNodeChanged="tvFAQ_SelectedNodeChanged" ExpandDepth="0" NodeWrap="true"></asp:TreeView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
