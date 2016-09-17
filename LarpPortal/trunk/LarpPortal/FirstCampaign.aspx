<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortalMasterReferences.Master" AutoEventWireup="true" CodeBehind="FirstCampaign.aspx.cs" Inherits="LarpPortal.General.FirstCampaign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
    <div class="contentArea">
        <aside>
        </aside>
        <div class="mainContent tab-content">
            <section id="public-campaigns">
                <div class="col-lg-12 NoPadding">
                    <h1 class="col-lg-12">Select your first campaign</h1>
                </div>
                <div class="row">
                    <asp:Label ID="lblWelcome" runat="server"></asp:Label>
                </div>
                <div class="row">
                    <div class="panel-wrapper col-sm-3">
                        <div class="panel">
                            <div class="panelheader">
                                <h2>Campaign Search</h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container  search-criteria">
                                    <asp:TreeView ID="tvCampaign" runat="server" Visible="true" OnSelectedNodeChanged="tvCampaign_SelectedNodeChanged" ExpandDepth="0"></asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
