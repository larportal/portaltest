﻿<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="LarpPortal.AboutUs" %>
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
										<h2>About LARP Portal</h2>
									</div>
									<div class="panel-body">
										<div class="panel-container">
                                            <asp:Label ID="lblAboutUs" runat="server"></asp:Label>
										</div>
									</div>
								</div>
							</div>
						</div>
					</section>
				</div>
			</div>
</asp:Content>
