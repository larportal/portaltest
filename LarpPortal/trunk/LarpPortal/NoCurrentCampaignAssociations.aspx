<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="NoCurrentCampaignAssociations.aspx.cs" Inherits="LarpPortal.NoCurrentCampaignAssociations" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
		<div class="contentArea">
            <aside></aside>
			<div class="mainContent tab-content">
				<section id="larping-info">
					<h1>No Current Campaign Associations</h1>
					<div class="row">
                        You currently are not associated with any campaigns.  Would you like to sign up for a campaign?
                        <i>Need to put in a Yes/No choice with appropriate choice actions.<br />
                            &nbsp;&nbsp;&nbsp;'Yes' should take the player to the campaign sign-up page. <br />
                            &nbsp;&nbsp;&nbsp;'No' should just sort of make faces at them and tell them to stop being stupid and pick a different tab.
                        </i>
					</div><!-- .row -->
				</section>
			</div><!-- mainContent .tab-content -->
		</div><!-- contentArea -->
    </asp:Content>
