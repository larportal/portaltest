<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="LarpPortal.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
			<div class="contentArea">
                <aside></aside>
				<div class="mainContent tab-content">
					<section id="larping-info">
						<h1>About LARP Portal</h1>
						<div class="row">
							<div class="panel-wrapper col-md-10">
								<div class="panel">
									<div class="panel-header">
										<h2>About Us</h2>
									</div>
									<div class="panel-body">
										<div class="panel-container">
                                            <asp:Label ID="lblAboutUs" runat="server"></asp:Label>
										</div><!-- .panel-container -->
									</div><!-- .panel-body -->
								</div><!-- .panel -->
							</div><!-- .panel-wrapper -->
						</div><!-- .row -->
					</section>
				</div><!-- mainContent .tab-content -->
			</div><!-- contentArea -->
</asp:Content>
