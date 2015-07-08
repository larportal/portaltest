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
                                            <asp:Table ID="tblAdmin" runat="server" CssClass="table">
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <b><u>LARP Portal Admin Functions</u></b>
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <b><u>Campaign Reports <i>(Hopefully temporary)</i></u></b>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:HyperLink ID="hplViewLogins" runat="server" NavigateUrl="~/Admin/AdminViewLogins.aspx" Target="_blank">View Logins</asp:HyperLink>
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:HyperLink ID="hplDailyFifthGateCharacters" runat="server" NavigateUrl="~/Admin/AdminDailyFifthGateCharacters.aspx" Target="_blank">Daily Fifth Gate Characters</asp:HyperLink>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:HyperLink ID="hlDisplaySystemErrors" runat="server" NavigateUrl="~/Admin/SystemErrors.aspx" Target="_blank">Error Log</asp:HyperLink>
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:HyperLink ID="hplDailyFifthGateCharacterSkills" runat="server" NavigateUrl="~/Admin/AdminDailyFifthGateCharacterSkills.aspx" Target="_blank">Daily Fifth Gate Character Skills</asp:HyperLink>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:HyperLink ID="hlDisplayDevoList" runat="server" NavigateUrl="~/Admin/DevelopmentList.aspx" Target="_blank">Display Development List</asp:HyperLink>
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:HyperLink ID="hplDailyMadrigalCharacters" runat="server" NavigateUrl="~/Admin/AdminDailyMadrigalCharacters.aspx" Target="_blank">Daily Madrigal Characters</asp:HyperLink>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:HyperLink ID="hplDailyMadrigalCharacterSkills" runat="server" NavigateUrl="~/Admin/AdminDailyMadrigalCharacterSkills.aspx" Target="_blank">Daily Madrigal Character Skills</asp:HyperLink>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
										</div>
									</div>
								</div>
							</div>
						</div>
					</section>
				</div>
			</div>
</asp:Content>