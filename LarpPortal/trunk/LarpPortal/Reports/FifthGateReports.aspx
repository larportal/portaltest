<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="FifthGateReports.aspx.cs" Inherits="LarpPortal.Reports.FifthGateReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
			<div class="contentArea">
                <aside>

                </aside>
				<div class="mainContent tab-content">
					<section id="larping-info">
						<div class="row">
							<div class="panel-wrapper col-md-10">
								<div class="panel">
                                    <h2><asp:Label ID="lblBlank" runat="server" Text="This page left intentionally blank."></asp:Label></h2>
                                    <asp:Panel id="pnlReports" runat="server" Visible="false">
									    <div class="panel-header">
										    <h2>Fifth Gate Reporting</h2>
									    </div>
									    <div class="panel-body">
										    <div class="panel-container">
                                                <asp:Table ID="tblAdmin" runat="server" CssClass="table">
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                                            <asp:HyperLink ID="hplFifthGateCharacters" runat="server" NavigateUrl="~/Reports/FifthGate/FifthGateCharacters.aspx" Target="_blank">Fifth Gate Characters</asp:HyperLink>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                                            <asp:HyperLink ID="hplFifthGateCharacterSkills" runat="server" NavigateUrl="~/Reports/FifthGate/FifthGateCharacterSkills.aspx" Target="_blank">Fifth Gate Character Skills</asp:HyperLink>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                                            <asp:HyperLink ID="hplSF2Registration" runat="server" NavigateUrl="~/Reports/FifthGate/SF2Registrations.aspx" Target="_blank">Silverfire Event 2 Registration</asp:HyperLink>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                                            <asp:HyperLink ID="hplWB2Registration" runat="server" NavigateUrl="~/Reports/FifthGate/WB2Registrations.aspx" Target="_blank">Wrathborn Event 2 Registration</asp:HyperLink>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
										    </div>
									    </div>
                                    </asp:Panel>
								</div>
							</div>
						</div>
					</section>
				</div>
			</div>
</asp:Content>