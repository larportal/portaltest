<%@ Page Title="" Language="C#" MasterPageFile="~/LARPing.master" AutoEventWireup="true" CodeBehind="WhatIsLARPing.aspx.cs" Inherits="LarpPortal.WhatIsLARPing" %>
<asp:Content ID="WhatIsLARPing" ContentPlaceHolderID="LARPingContent" runat="server">
			<div class="contentArea">
                <%--<aside></aside>--%>
				<div class="mainContent tab-content">
					<section id="larping-info">
						<h1>What is LARPing</h1>
						<div class="row">
							<div class="panel-wrapper col-md-10">
								<div class="panel">
									<div class="panel-header">
										<h2>LARPing</h2>
									</div>
									<div class="panel-body">
										<div class="panel-container">
                                            <asp:Label ID="lblWhatIsLARPing" runat="server"></asp:Label>
										</div><!-- .panel-container -->
									</div><!-- .panel-body -->
								</div><!-- .panel -->
							</div><!-- .panel-wrapper -->
						</div><!-- .row -->
					</section>
				</div><!-- mainContent .tab-content -->
			</div><!-- contentArea -->
</asp:Content>
