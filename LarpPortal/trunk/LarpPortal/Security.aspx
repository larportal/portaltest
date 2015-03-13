<%@ Page Title="" Language="C#" MasterPageFile="~/MemberProfile.master" AutoEventWireup="true" CodeBehind="Security.aspx.cs" Inherits="LarpPortal.Security" %>
<asp:Content ID="Security" ContentPlaceHolderID="Demographics" runat="server">
    <asp:Label ID="WIP" runat="server" BackColor="Yellow">My Profile - Security - Placeholder page - in progress</asp:Label>
      <div class="mainContent tab-content">
        <section id="security" class="security tab-pane" style="display:block">
	        <div role="form" class="form-horizontal form-condensed">
		        <div class="row">
			        <h1 class="col-sm-4">My Profile - Security</h1>
		        </div>
		        <div class="userSecurity">
			        <div class="formGroup row">
                        <asp:Label ID="lblUsernameLabel" runat="server" CssClass="control-label col-sm-3">Username</asp:Label>
				        <div class="user-name col-sm-3">
                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
				        </div>
			        </div>
			        <div class="formGroup row">
                        <asp:Label ID="lblFirstNameLabel" runat="server" Cssclass="control-label col-sm-3">First</asp:Label>
                        <div class="first-name col-sm-3">
                            <asp:Label ID="lblFirstName" runat="server"></asp:Label>
				        </div>
                        <asp:Label ID="lblMILabel" runat="server" Cssclass="control-label col-sm-1">MI</asp:Label>
				        <div class="middle-initial col-sm-1">
                            <asp:Label ID="lblMI" runat="server" />
				        </div>
                        <asp:Label ID="lblLastNameLabel" runat="server" Cssclass="control-label col-sm-3">Last</asp:Label>
				        <div class="last-name col-sm-3">
                            <asp:Label ID="lblLastName" runat="server"></asp:Label>
				        </div>
                        <asp:Label ID="lblNickNameLabel" runat="server" Cssclass="control-label col-sm-1">Nick Name</asp:Label>
				        <div class="nick-name col-sm-3">
                            <asp:Label ID="lblNickName" runat="server"></asp:Label>
				        </div>
			        </div>
			        <div class="formGroup row">
				        <label for="password2" class="control-label col-sm-1">Password</label>
				        <div class="password col-sm-3">
					        <input type="password" id="password2"  placeholder="password" value="" class="form-control">
				        </div>
			        </div>
			        <div class="formGroup row">
				        <label for="password-3" class="control-label col-sm-1">Password</label>
				        <div class="password col-sm-3">
					        <input type="password" id="password-3"  placeholder="password" value="" class="form-control">
				        </div>
			        </div>
			        <div class="security-questions">
				        <h2>Security Questions:</h2>
				        <div class="row">
					        <p class="col-sm-7">Please enter 1 or more security questions for use in future email and password validation</p>
				        </div>
				        <div class="form-group row">

					        <label for="security-question-1" class="control-label col-sm-1">Q 1</label>
					        <div class="col-sm-5">
						        <input type="text" name="security-question-1" id="security-question-1" placeholder="Enter your security question 1" value="Enter your security question 1" class="form-control">
					        </div>
					        <label for="security-answer-1" class="control-label col-sm-1">A 1</label>
					        <div class="col-sm-5">
						        <input type="text" name="security-answer-1" id="security-answer-1" placeholder="Enter your security answer 1" value="Enter your security answer 1" class="form-control">
					        </div>

				        </div>
				        <div class="form-group row">
					        <label for="security-question-2" class="control-label col-sm-1">Q 2</label>
					        <div class="col-sm-5">
						        <input type="text" name="security-question-2" id="security-question-2" placeholder="Enter your security question 2" value="Enter your security question 2" class="form-control">
					        </div>
					        <label for="security-answer-2" class="control-label col-sm-1">A 2</label>
					        <div class="col-sm-5">
						        <input type="text" name="security-answer-2" id="security-answer-2" placeholder="Enter your security answer 2" value="Enter your security answer 2" class="form-control">
					        </div>
				        </div>
				        <div class="form-group row">
					        <label for="security-question-3" class="control-label col-sm-1">Q 3</label>
					        <div class="col-sm-5">
						        <input type="text" name="security-question-3" id="security-question-3" placeholder="Enter your security question 3" value="Enter your security question 3" class="form-control">
					        </div>
					        <label for="security-answer-3" class="control-label col-sm-1">A 3</label>
					        <div class="col-sm-5">
						        <input type="text" name="security-answer-3" id="security-answer-3" placeholder="Enter your security answer 3" value="Enter your security answer 3" class="form-control">
					        </div>
				        </div>
			        </div>
		        </div>
		        <div class="row">
			        <div class="col-sm-1 col-sm-offset-11">
				        <button type="submit" class="btn btn-default">Save</button>
			        </div>
		        </div>
	        </div>
        </section>
      </div>
</asp:Content>