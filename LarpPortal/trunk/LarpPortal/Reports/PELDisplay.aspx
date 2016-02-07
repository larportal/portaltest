<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="PELDisplay.aspx.cs" Inherits="LarpPortal.PELs.PELDisplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
    <style type="text/css">
        .StandardButton
        {
            background-color: #093760;
            border-color: black;
            border-width: 1px;
            border-style: solid;
            color: white;
            padding: 0px 0px 0px 0px;
        }

            .StandardButton:hover
            {
                background-color: lightblue;
            }
    </style>
			<div class="contentArea">
                <aside>

                </aside>
				<div class="mainContent tab-content">
					<section id="larping-info">
                        <asp:Button ID="btnCloseTop" runat="server" OnClick="btnCloseTop_Click" Text="Close" CssClass="StandardButton" />
						<div class="row">
							<div class="panel-wrapper col-md-10">
								<div class="panel">
									<div class="panel-header">
										<h2><asp:Label ID="lblPanelHeader" runat="server"></asp:Label></h2>
									</div>
									<div class="panel-body">
										<div class="panel-container">
                                            <asp:Label ID="lblWhatsNewDetail" runat="server"></asp:Label>
                                            <div class="row">
                                                <br /><br /><br />&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" CssClass="StandardButton" />
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

