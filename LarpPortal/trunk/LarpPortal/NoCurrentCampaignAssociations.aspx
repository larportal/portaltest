<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="NoCurrentCampaignAssociations.aspx.cs" Inherits="LarpPortal.NoCurrentCampaignAssociations" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
		<div class="contentArea">
            <aside></aside>
			<div class="mainContent tab-content">
				<section id="larping-info">
					<h1>No Current Campaign Associations</h1>
					<div class="row">
                        You currently are not associated with any campaigns.  Would you like to add campaigns to your campaign list?
                        <div>
                            <asp:HyperLink ID="hplSignUpYes" runat="server" NavigateUrl="~/PublicCampaigns.aspx">Yes, I'd like to add campaigns.</asp:HyperLink><br /><br />
                        </div>
                        <div>
                            <asp:HyperLink ID="hplSignUpNo" runat="server" NavigateUrl="~/MemberDemographics.aspx">No thanks, take me to my profile.</asp:HyperLink><br /><br />
                        </div>
					</div><!-- .row -->
				</section>
			</div><!-- mainContent .tab-content -->
		</div><!-- contentArea -->
    </asp:Content>
