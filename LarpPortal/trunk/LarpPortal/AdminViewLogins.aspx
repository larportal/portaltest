<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="AdminViewLogins.aspx.cs" Inherits="LarpPortal.AdminViewLogins" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
			<div class="contentArea">
                <aside>

                </aside>
				<div class="mainContent tab-content">
					<section id="larping-info">
                        <%--<input type=button name=print value="Print" onclick="javascript: window.print()">--%>
						<div class="row">
							<div class="panel-wrapper col-md-10">
								<div class="panel">
									<div class="panel-header">
										<h2>View Logins</h2>
									</div>
									<div class="panel-body">
										<div class="panel-container-table">
                                            <asp:Label ID="lblViewLogins" runat="server" >Build the table here programatically.  If you see this code the build failed.</asp:Label>
										</div>
									</div>
								</div>
							</div>
						</div>
					</section>
				</div>
			</div>
</asp:Content>
