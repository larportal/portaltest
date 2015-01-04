<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="PageUnderConstruction.aspx.cs" Inherits="LarpPortal.PageUnderConstruction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
			<div class="contentArea">
                <aside></aside>
				<div class="mainContent tab-content">
					<section id="larping-info">
						<asp:Image ID="imgUnderConstruction" AlternateText="Page Under Construction" runat="server" ImageAlign="Left" ImageUrl="\img\ThisPageUnderConstruction.jpg" />
                        <br />
                        <asp:Image ID="imgWorkers" AlternateText="We're hard at work" runat="server" ImageAlign="Left" ImageUrl="\img\construction.gif" />
					</section>
				</div><!-- mainContent .tab-content -->
			</div><!-- contentArea -->
</asp:Content>
