<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="LARPortalError.aspx.cs" Inherits="LarpPortal.LARPortalError" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
		<div class="contentArea">
            <aside></aside>
			<div class="mainContent tab-content">
				<section id="larping-info">
					<h1>LARP Portal Error</h1>
					<div class="row">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;An error has occured.  Any unsaved work has been lost.<br /><br />
                        <div>
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="hplLogin" runat="server" NavigateUrl="~/index.aspx">Return to LARP Portal login</asp:HyperLink><br /><br />
                        </div>
					</div><!-- .row -->
				</section>
			</div><!-- mainContent .tab-content -->
		</div><!-- contentArea -->
    </asp:Content>
