<%@ Page Title="" Language="C#" MasterPageFile="~/MemberProfile.master" AutoEventWireup="true" CodeBehind="MemberSecurity.aspx.cs" Inherits="LarpPortal.Security" %>
<asp:Content ID="Security" ContentPlaceHolderID="Demographics" runat="server">
<script type="text/javascript">
    function clearError() {
            var x = document.getElementById("lblError");
            if (x != null)
                x.innerHTML = ""
        }
</script>
    <asp:Label ID="WIP" runat="server" BackColor="Yellow">My Profile - Security - Placeholder page - in progress</asp:Label>
      <div class="mainContent tab-content">
        <section id="security" class="security tab-pane" style="display:block">
	        <div role="form" class="form-horizontal form-condensed">
		        <div class="row">
			        <h1 class="col-sm-4">My Profile - Security</h1>
		        </div>
		        <div class="userSecurity">
			        <div class="formGroup row">
                        <asp:Label ID="lblUsernameLabel" runat="server" CssClass="control-label col-sm-1">Username</asp:Label>
				        <div class="user-name col-sm-3">
                            <asp:TextBox ID="txtUsername" runat="server" ReadOnly="true"></asp:TextBox>
				        </div>
			        </div>
			        <div class="formGroup row">
                        <asp:Label ID="lblFirstNameLabel" runat="server" Cssclass="control-label col-sm-1">First</asp:Label>
                        <div class="first-name col-sm-2">
                            <asp:TextBox ID="txtFirstName" runat="server" ReadOnly="true"></asp:TextBox>
				        </div>
                        <asp:Label ID="lblMILabel" runat="server" Cssclass="control-label col-sm-1">MI</asp:Label>
				        <div class="middle-initial col-sm-1">
                            <asp:TextBox ID="txtMI" runat="server"  ReadOnly="true"/>
				        </div>
                        <asp:Label ID="lblLastNameLabel" runat="server" Cssclass="control-label col-sm-1">Last</asp:Label>
				        <div class="last-name col-sm-2">
                            <asp:TextBox ID="txtLastName" runat="server" ReadOnly="true"></asp:TextBox>
				        </div>
                        <asp:Label ID="lblNickNameLabel" runat="server" Cssclass="control-label col-sm-2">Nick Name</asp:Label>
				        <div class="nick-name col-sm-2">
                            <asp:TextBox ID="txtNickName" runat="server" ReadOnly="true"></asp:TextBox>
				        </div>
			        </div>
                    <div class="formGroup row">
                        <asp:Label ID="lblError" runat="server" Font-Bold="true" Cssclass="control-label col-sm-4"/>
                    </div>
			        <div class="formGroup row">                        
                        <asp:Label ID="lblPassword" runat="server" Cssclass="control-label col-sm-1">Password</asp:Label>
				        <div class="pass-word col-sm-3">
                            <asp:TextBox ID="txtPassword" runat="server" placeholder="password" TextMode="Password"  BackColor="White"></asp:TextBox>
				        </div>                         
			        </div>
			        <div class="formGroup row">
				        <asp:Label ID="lblPassword2" runat="server" Cssclass="control-label col-sm-1">Password</asp:Label>
				        <div class="pass-word col-sm-3">
                            <asp:TextBox ID="txtPassword2" runat="server" placeholder="password" TextMode="Password" BackColor="White"></asp:TextBox>
				        </div>
			        </div>
			        <div class="security-questions">
				        <h2>Security Questions:</h2>
				        <div class="row">
					        <p class="col-sm-7">Please enter 1 or more security questions for use in future email and password validation</p>
				        </div>
                        <div class="formGroup row">
                            <asp:Label ID="lblErrorQuestions" runat="server" Font-Bold="true" Cssclass="control-label col-sm-4"/>
                        </div>
				        <div class="form-group row">
					        <label for="security-question-1" class="control-label col-sm-1">Q 1</label>
					        <div class="col-sm-5">
						        <asp:TextBox id="txtSecurityQuestion1" runat="server" placeholder="Enter your security question 1" class="form-control"/>
					        </div>
					        <label for="security-answer-1" class="control-label col-sm-1">A 1</label>
					        <div class="col-sm-5">
                                <asp:TextBox id="txtSecurityAnswer1" runat="server" placeholder="Enter your security answer 1" class="form-control"/>
					        </div>
				        </div>
                        <div class="formGroup row">
                            <asp:Label ID="lblErrorQuestion2" runat="server" Font-Bold="true" Cssclass="control-label col-sm-4"/>
                        </div>
				        <div class="form-group row">
					        <label for="security-question-2" class="control-label col-sm-1">Q 2</label>
					        <div class="col-sm-5">
						        <asp:TextBox id="txtSecurityQuestion2" runat="server" placeholder="Enter your security question 2" class="form-control"/>
					        </div>
					        <label for="security-answer-1" class="control-label col-sm-1">A 2</label>
					        <div class="col-sm-5">
                                <asp:TextBox id="txtSecurityAnswer2" runat="server" placeholder="Enter your security answer 2" class="form-control"/>
					        </div>
				        </div>
                        <div class="formGroup row">
                            <asp:Label ID="lblErrorQuestion3" runat="server" Font-Bold="true" Cssclass="control-label col-sm-4"/>
                        </div>
                        <div class="form-group row">
					        <label for="security-question-3" class="control-label col-sm-1">Q 3</label>
					        <div class="col-sm-5">
						        <asp:TextBox id="txtSecurityQuestion3" runat="server" placeholder="Enter your security question 3" class="form-control"/>
					        </div>
					        <label for="security-answer-1" class="control-label col-sm-1">A 3</label>
					        <div class="col-sm-5">
                                <asp:TextBox id="txtSecurityAnswer3" runat="server" placeholder="Enter your security answer 3" class="form-control"/>
					        </div>
				        </div>
			        </div>
		        </div>
		        <div class="row">
			        <div class="col-sm-2 col-sm-offset-10">
				        <asp:Button ID="btnSave" runat="server" Text="Save" type="submit" OnClick="btnSave_Click" class="btn btn-default"/>
			        </div>
		        </div>
	        </div>
        </section>
      </div>
</asp:Content>