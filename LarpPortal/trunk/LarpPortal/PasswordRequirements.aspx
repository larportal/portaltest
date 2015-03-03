<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortalMasterReferences.Master" AutoEventWireup="true" CodeBehind="PasswordRequirements.aspx.cs" Inherits="LarpPortal.PasswordRequirements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
    <div class="contentArea">
        <aside>

        </aside>
	    <div class="mainContent tab-content">
		    <section id="larping-info">
			    <h1>Password Requirements</h1>
			    <div class="row">
				    <div class="panel-wrapper col-md-10">
					    <div class="panel">
						    <div class="panel-header">
							    <h2>LARP Portal Password Requirements</h2>
						    </div>
						    <div class="panel-body">
							    <div class="panel-container">
                                    <asp:Label ID="lblAboutUs" runat="server"></asp:Label>
                                        LARP Portal requires the following for all passwords:
                                        <p>Minimum length of seven (7) characters</p>
                                        <p>Must contain at least one (1) uppercase letter</p>
                                        <p>Must contain at least one (1) lower case letter</p>
                                        <p>Must contain at least one (1) number</p>
                                        <p>Must contain at least one (1) special character</p>
							    </div>
						    </div>
					    </div>
				    </div>
			    </div>
		    </section>
	    </div>
    </div>
</asp:Content>