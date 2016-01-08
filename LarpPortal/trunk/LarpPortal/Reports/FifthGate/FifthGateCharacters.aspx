<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="FifthGateCharacters.aspx.cs" Inherits="LarpPortal.Reports.FifthGate.FifthGateCharacters" %>
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
										<h2>Fifth Gate Characters</h2>
									</div>
									<div class="panel-body">
										<div class="panel-container-table">
                                            <div class="row">
                                                <asp:Button ID="Button1" runat="server" Text=" Export to Excel " OnClick="btnExportExcel_Click" />
                                                <asp:Button ID="Button2" runat="server" Text=" Export to csv " OnClick="btnExportcsv_Click" />
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="lblCharacters" runat="server" >Build the table here programatically.  If you see this code the build failed.</asp:Label>
                                            </div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</section>
				</div>
			</div>
</asp:Content>
