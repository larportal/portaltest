<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortalMasterReferences.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="LarpPortal.ForgotPassword" %>
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
							    <h2>LARP Portal Forgot Username or Password?</h2>
						    </div>
						    <div class="panel-body">
							    <div class="panel-container"><p></p>
                                    <asp:Panel ID="pnlIDYourself" runat="server">
                                        <b>Section 1 - Fill in all three fields and click 'Get Password'</b><p></p>
                                        <asp:Label ID="lblEmailAddress" runat="server">Email Address: </asp:Label>
                                        <asp:TextBox ID="txtEmailAddress" runat="server" TextMode="Email" AutoPostBack="true" OnTextChanged="txtEmailAddress_TextChanged"></asp:TextBox><p></p>

                                        <asp:Label ID="lblUsername" runat="server">Username: </asp:Label>
                                        <asp:TextBox ID="txtUsername" runat="server" AutoPostBack="true" OnTextChanged="txtUsername_TextChanged"></asp:TextBox><br />
                                        <asp:Label ID="lblForgotUsername" runat="server">Forgot your username or new to the system?  Fill in the email address and click 'Forgot Username'</asp:Label>
                                        <asp:Button ID="btnForgotUsername" runat="server" OnClick="btnForgotUsername_Click" CssClass="btn btn-primary" Text="Forgot Username" /><br />
                                        <asp:Label ID="lblUsernameISEmail" runat="server"></asp:Label>
                                        <p></p>
                                        <asp:Label ID="lblLastName" runat="server">Last Name: </asp:Label>
                                        <asp:TextBox ID="txtLastName" runat="server" AutoPostBack="true" OnTextChanged="txtLastName_TextChanged"></asp:TextBox>
                                        <p></p>
                                        <asp:Button ID="btnGetPassword" runat="server" OnClick="btnGetPassword_Click" CssClass="btn btn-primary" Text="Get Password" />
                                        <asp:Label ID="lblInvalidCombination" runat="server" ForeColor="Red">That username, email address, last name combination is not valid.  Click OK and try again.</asp:Label>
                                        <p></p><asp:Button ID="btnInvalidCombination" runat="server" OnClick="btnInvalidCombination_Click" CssClass="btn btn-primary" Text="OK" />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlVariables" runat="server">
                                        <asp:Label ID="Q1" runat="server"></asp:Label>
                                        <asp:Label ID="Q1Update" runat="server">0</asp:Label>
                                        <asp:Label ID="Q2" runat="server"></asp:Label>
                                        <asp:Label ID="Q2Update" runat="server">0</asp:Label>
                                        <asp:Label ID="Q3" runat="server"></asp:Label>
                                        <asp:Label ID="Q3Update" runat="server">0</asp:Label>
                                        <asp:Label ID="A1" runat="server"></asp:Label>
                                        <asp:Label ID="A1Update" runat="server">0</asp:Label>
                                        <asp:Label ID="A2" runat="server"></asp:Label>
                                        <asp:Label ID="A2Update" runat="server">0</asp:Label>
                                        <asp:Label ID="A3" runat="server"></asp:Label>
                                        <asp:Label ID="A3Update" runat="server">0</asp:Label>
                                        <asp:Label ID="UserSecurityID" runat="server"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlSetQuestion" runat="server">
                                        <asp:Label ID="lblSetQuestions" runat="server">
                                            It would appear you haven't set any security questions yet.  You have to have at least one.
                                            You can have up to three.  You decide how many and you decide the questions and the answers.
                                            If you forget your password you will have to answer all questions that you set so you decide how much security you want.<br /><br />
                                            Enter each security question or answer and then tab or enter to move on to the next field.
                                        </asp:Label><p></p>
                                        <asp:Label ID="lblSecurityQ1" runat="server">Security Question 1: </asp:Label>
                                        <asp:TextBox ID="txtSecurityQ1" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblSecurityA1" runat="server">Security Answer 1: </asp:Label>
                                        <asp:TextBox ID="txtsecurityA1" runat="server" AutoPostBack="true"  OnTextChanged="txtsecurityA1_TextChanged"></asp:TextBox><p></p>
                                        <asp:Label ID="lblWantQ2" runat="server">Do you want a second question for added security? </asp:Label>
                                        <asp:Button ID="btnWantQ2Yes" runat="server" OnClick="btnWantQ2Yes_Click" Text="Yes" />
                                        <asp:Button ID="btnWantQ2No" runat="server" OnClick="btnWantQ2No_Click" Text="No" /><p></p>
                                        <asp:Label ID="lblSecurityQ2" runat="server">Security Question 2: </asp:Label>
                                        <asp:TextBox ID="txtSecurityQ2" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblSecurityA2" runat="server">Security Answer 2: </asp:Label>
                                        <asp:TextBox ID="txtSecurityA2" runat="server" AutoPostBack="true" OnTextChanged="txtSecurityA2_TextChanged"></asp:TextBox><p></p>
                                        <asp:Label ID="lblWantQ3" runat="server">Do you want a third question for even more security? </asp:Label>
                                        <asp:Button ID="btnWantQ3Yess" runat="server" OnClick="btnWantQ3Yes_Click" Text="Yes" />
                                        <asp:Button ID="btnWantQ3No" runat="server" OnClick="btnWantQ3No_Click" Text="No" /><p></p>
                                        <asp:Label ID="lblSecurityQ3" runat="server">Security Question 3: </asp:Label>
                                        <asp:TextBox ID="txtSecurityQ3" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblSecurityA3" runat="server">Security Answer 3: </asp:Label>
                                        <asp:TextBox ID="txtsecurityA3" runat="server" AutoPostBack="true" OnTextChanged="txtsecurityA3_TextChanged"></asp:TextBox><p></p>                                        
                                        <asp:Label ID="lblResetPassword" runat="server">Click OK to reset your password</asp:Label>
                                        <asp:Button ID="btnResetPassword" runat="server" OnClick="btnResetPassword_Click" Text="OK" />
                                    </asp:Panel>
                                    <asp:Panel id="pnlAnswerQuestion" runat="server">
                                        <asp:Label ID="lblAskQuestion1" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtAnswerQuestion1" runat="server" AutoPostBack="true" OnTextChanged="txtAnswerQuestion1_TextChanged"></asp:TextBox><p></p>
                                        <asp:Label ID="lblAnsweredQuestion1Wrong" runat="server"></asp:Label>

                                        <asp:Label ID="lblAskQuestion2" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtAnswerQuestion2" runat="server" AutoPostBack="true" OnTextChanged="txtAnswerQuestion2_TextChanged"></asp:TextBox><p></p>
                                        <asp:Label ID="lblAnsweredQuestion2Wrong" runat="server"></asp:Label>

                                        <asp:Label ID="lblAskQuestion3" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtAnswerQuestion3" runat="server" AutoPostBack="true" OnTextChanged="txtAnswerQuestion3_TextChanged"></asp:TextBox><p></p>
                                        <asp:Label ID="lblAnsweredQuestion3Wrong" runat="server"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel id="pnlSetPasswords" runat="server">
                                        <asp:Label ID="lblPasswordRequirements" runat="server">
                                            Password requirements:<br />
                                            LARP Portal login passwords must be at least 7 characters long and contain at least <br />
                                            1 uppercase letter, 1 lowercse letter, 1 number and 1 special character<br />
                                        </asp:Label><p></p>
                                        <asp:Label ID="lblNewPassword" runat="server">New Password: </asp:Label>
                                        <asp:TextBox ID="txtNewPassword" runat="server" AutoPostBack="true" OnTextChanged="txtNewPassword_TextChanged"></asp:TextBox><p></p>
                                        <asp:Label ID="lblNewPasswordRetype" runat="server">Retype New Password: </asp:Label>
                                        <asp:TextBox ID="txtNewPasswordRetype" runat="server" AutoPostBack="true" OnTextChanged="txtNewPasswordRetype_TextChanged"></asp:TextBox><p></p>
                                        <asp:Label ID="lblPasswordErrors" runat="server" ForeColor="Red"></asp:Label><p></p>
                                        <asp:Button ID="btnSubmitPasswordChange" runat="server" OnClick="btnSubmitPasswordChange_Click" Text="Submit" />
                                    </asp:Panel>
                                    <asp:Panel id="pnlFinalStep" runat="server">
                                        <asp:Label ID="lblDone" runat="server">
                                            Your password has been reset.  Click the ok button to return to the login screen.
                                        </asp:Label>
                                        <asp:Button ID="btnDone" runat="server" OnClick="btnDone_Click" Text="OK" />
                                    </asp:Panel>
							    </div>
						    </div>
					    </div>
				    </div>
			    </div>
		    </section>
	    </div>
    </div>
</asp:Content>
