<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="AdminMaintenance.aspx.cs" Inherits="LarpPortal.AdminMaintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
			<div class="contentArea">
                <aside>

                </aside>
				<div class="mainContent tab-content">
					<section id="larping-info">
						<div class="row">
							<div class="panel-wrapper col-md-10">
								<div class="panel">
									<div class="panel-header">
										<h2>LARP Portal Administrator Pages</h2>
									</div>
									<div class="panel-body">
										<div class="panel-container">
                                            <asp:HyperLink ID="hplViewLogins" runat="server" NavigateUrl="~/AdminViewLogins.aspx" Target="_blank">View Logins</asp:HyperLink><br /><br />
                                            <asp:HyperLink ID="hplDailyFifthGateCharacters" runat="server" NavigateUrl="~/AdminDailyFifthGateCharacters.aspx" Target="_blank">Daily Fifth Gate Characters</asp:HyperLink><br /><br />
                                            <asp:HyperLink ID="hplDailyFifthGateCharacterSkills" runat="server" NavigateUrl="~/AdminDailyFifthGateCharacterSkills.aspx" Target="_blank">Daily Fifth Gate Character Skills</asp:HyperLink><br /><br />
                                            <asp:HyperLink ID="hlDisplaySystemErrors" runat="server" NavigateUrl="~/SystemErrors.aspx" Target="_blank">Error Log</asp:HyperLink><br /><br />
                                            <asp:HyperLink ID="hlDisplayDevoList" runat="server" NavigateUrl="~/DevelopmentList.aspx" Target="_blank">Display Development List</asp:HyperLink>
										</div>
									</div>
								</div>
							</div>
						</div>
					</section>
				</div>
			</div>
</asp:Content>