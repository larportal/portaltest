<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaignsAdmin.master" AutoEventWireup="true" CodeBehind="CampaignInfoSetup.aspx.cs" Inherits="LarpPortal.CampaignInfoSetup" %>

<asp:Content ID="CampaignInfoSetup" ContentPlaceHolderID="MemberCampaignsAdminContent" runat="server">
    <div class="mainContent tab-content">
        <section id="campaign-info" class="campaign-info tab-pane active">
            <div role="form" class="form-horizontal form-condensed">
                <div class="col-sm-12">
                    <h1 class="col-sm-6">Campaign Information -
                        <asp:Label ID="lblTopCampaignName" runat="server"></asp:Label>
                        <asp:Label ID="lblWIP" runat="server"></asp:Label></h1>
                    <div class="panel-wrapper">
                        <div class="uploadFile col-sm-4">
                            <div class="input-group">
                                <input type="text" class="form-control" placeholder="Browse to add photo">
                                <span class="input-group-btn">
                                    <span class="btn btn-default btn-sm btnFile">Browse<input type="file" multiple></span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-5">
                        <div class="panel-wrapper">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Demographics</h2>
                                </div>
                                <div class="panel-body">
                                    <div class="panel-container scroll-200">
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtCampaignName" ID="lblCampaignName" runat="server" CssClass="col-sm-3 control-label">Name</asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtCampaignName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtStarted" ID="lblStarted" runat="server" CssClass="col-sm-3 control-label">Started</asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtStarted" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtExpectedEnd" ID="LabelExpectedEnd" runat="server" CssClass="col-sm-3 control-label">Expected End</asp:Label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtExpectedEnd" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <asp:Label AssociatedControlID="txtNumEvents" ID="lblNumEvents" runat="server" CssClass="col-sm-3 control-label"># Events</asp:Label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtNumEvents" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtGameSystem" ID="lblGameSystem" runat="server" CssClass="col-sm-3 control-label">Game System</asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtGameSystem" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtGenre1" ID="lblGenre1" runat="server" CssClass="col-sm-3 control-label">Genre 1</asp:Label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtGenre1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <asp:Label AssociatedControlID="txtGenre2" ID="lblGenre2" runat="server" CssClass="col-sm-3 control-label">Genre 2</asp:Label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtGenre2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtStyle" ID="lblStyle" runat="server" CssClass="col-sm-3 control-label">Style</asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtStyle" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtTechLevel" ID="lblTechLevel" runat="server" CssClass="col-sm-3 control-label">Tech Level</asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtTechLevel" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtSize" ID="lblSize" runat="server" CssClass="col-sm-3 control-label">Size</asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtSize" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtAvgNumEvents" ID="lblAvgNumEvents" runat="server" CssClass="col-sm-3 control-label">Average # Events</asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtAvgNumEvents" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-wrapper">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Contacts</h2>
                                </div>
                                <div class="panel-body">
                                    <asp:Repeater ID="menu_ul_contacts" runat="server">
                                        <HeaderTemplate>
                                            <div class="panel-container scroll-150">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("href_li")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-5">
                        <div class="panel-wrapper">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Setting Description</h2>
                                </div>
                                <div class="panel-body">
                                    <div id="ThisIsTheScroll" class="panel-container scroll-200">
                                        <asp:Label ID="CampaignDescription" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel-wrapper">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Requirements</h2>
                                </div>
                                <div class="panel-body">
                                    <div class="panel-container scroll-150">
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtMembershipFee" ID="lblMembershipFee" runat="server" CssClass="col-sm-4 control-label">Membership Fee</asp:Label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtMembershipFee" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtFrequency" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtMinimumAge" ID="lblMinimumAge" runat="server" CssClass="col-sm-4 control-label">Minimum Age</asp:Label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtMinimumAge" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtSupervisedAge" ID="lblSupervisedAge" runat="server" CssClass="col-sm-4 control-label">Supervised Age</asp:Label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtSupervisedAge" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <%--													<label for="campaign-req-waiver" class="col-sm-4 control-label">Waiver</label>
													<div class="col-sm-4">
														<input type="email" class="form-control" id="campaign-req-waiver" placeholder="Req. Waiver">
													</div>--%>
                                            <asp:Label AssociatedControlID="txtWaiver1" ID="lblWaiver1" runat="server" CssClass="col-sm-4 control-label">Waiver 1</asp:Label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtWaiver1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <p class="form-control-static"><a href="#">Waiver Link</a></p>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtWaiver2" ID="lblWaiver2" runat="server" CssClass="col-sm-4 control-label">Waiver 2</asp:Label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtWaiver2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label AssociatedControlID="txtConsent" ID="lblConsent" runat="server" CssClass="col-sm-4 control-label">Consent</asp:Label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtConsent" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2 scroll-500">
                        <div class="panel-wrapper">
                            <img src="http://placehold.it/125x125" alt="Campaign Photo">
                        </div>
                        <div class="panel-wrapper">
                            <img src="http://placehold.it/125x125" alt="Campaign Photo">
                        </div>
                        <div class="panel-wrapper">
                            <img src="http://placehold.it/125x125" alt="Campaign Photo">
                        </div>
                        <div class="panel-wrapper">
                            <img src="http://placehold.it/125x125" alt="Campaign Photo">
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
