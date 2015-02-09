<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="LarpPortal.index" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="no-js">
<!--<![endif]-->
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>LARP Portal</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Place favicon.ico and apple-touch-icon.png in the root directory -->
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="stylesheet" href="css/main.css">
    <script src="js/vendor/modernizr-2.6.2.min.js"></script>
</head>
<body>
    <!--[if lt IE 7]>
		<p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> to improve your experience.</p>
		<![endif]-->
    <!-- Add your site or application content here -->
    <div class="pageWrap">
        <div class="content">
            <section class="welcome">
                <div class="logo">
                    <h1>LARP Portal</h1>
                    <p>The Gateway to Managing Your LARPs</p>
                </div>
            </section>
            <!-- .welcome -->
            <section class="signIn">
                <form role="form" runat="server">
                    <h2 class="form-signin-heading">Member Login</h2>
                    <div class="form-signin" role="form">
                    <div>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVUserName" runat="server" ErrorMessage="Username is required" 
                            ControlToValidate="txtUserName" ValidationGroup="LoginGroup" display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>                                       
                    <div>
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVPassword" runat="server" ErrorMessage="Password is required" 
                            ControlToValidate="txtPassword" ValidationGroup="LoginGroup" display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:Label ID="lblSecurityResetCode" runat="server" ToolTip=""></asp:Label>
                        <asp:TextBox ID="txtSecurityResetCode" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div>
                        <asp:Table id="tblLoginButton" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Button ID="btnLogin" CssClass="btn btn-primary" runat="server" Text="Login" 
                                                            CausesValidation="true" ValidationGroup="LoginGroup" OnClick="btnLogin_Click" />
                                    <asp:Button ID="btnValidateAccount" CssClass="btn btn-primary" runat="server" Text="Login" OnClick="btnValidateAccount_Click" />
                                </asp:TableCell>
                                <asp:TableCell>&nbsp;&nbsp;</asp:TableCell>
                                <asp:TableCell Wrap="True">
                                    <asp:Label ID="lblInvalidLogin" runat="server" ForeColor="Red">Invalid username and password combination</asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                    <div><asp:TextBox ID="txtName" runat="server"></asp:TextBox></div>         
                    <div><asp:TextBox ID="txtUserID" runat="server"></asp:TextBox></div>
                    <div><asp:TextBox ID="txtLastLocation" runat="server"></asp:TextBox></div>
                    </div>
                <p><asp:Label ID="GuestLogin" runat="server"></asp:Label></p>
                <h2 class="form-signin-heading">Sign Up for Larp Portal</h2>
                <div class="form-signin" role="form">
                    <div>
                        <asp:Label ID="lblSignUpErrors" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVNewUsername" runat="server" ErrorMessage="Username is required" 
                            ControlToValidate="txtNewUsername" ValidationGroup="SignUpGroup" display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVFirstName" runat="server" ErrorMessage="First name is required" 
                            ControlToValidate="txtFirstName" ValidationGroup="SignUpGroup" display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVLastName" runat="server" ErrorMessage="Last name is required" 
                            ControlToValidate="txtLastName" ValidationGroup="SignUpGroup" display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ErrorMessage="Email is required" 
                            ControlToValidate="txtEmail" ValidationGroup="SignUpGroup" display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:Label ID="lblPasswordReqs" runat="server" ToolTip=""></asp:Label>
                        <asp:TextBox ID="txtPasswordNew" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVPasswordNew" runat="server" ErrorMessage="Password is required" 
                            ControlToValidate="txtPasswordNew" ValidationGroup="SignUpGroup" display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:TextBox ID="txtPasswordNewRetype" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVPasswordNewRetype" runat="server" ErrorMessage="Password is required" 
                            ControlToValidate="txtPasswordNewRetype" ValidationGroup="SignUpGroup" display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <label class="checkbox">
                            <asp:CheckBox ID="chkTermsOfUse" runat="server" OnCheckedChanged="chkTermsOfUse_CheckedChanged" autopostback="true"/>
                            I agree to the <asp:HyperLink ID="TermsOfUse" runat="server" NavigateURL="~/TermsOfUse.aspx" Target="_blank">Terms Of Use</asp:HyperLink>
                        </label>
                    </div>
                    <asp:Button ID="btnSignUp" CssClass="btn btn-primary" runat="server" Text="Sign Up" 
                        CausesValidation="true" ValidationGroup="SignUpGroup" OnClick="btnSignUp_Click" />
                    <span class="learnMore"><asp:HyperLink ID="HyperLink1" runat="server" NavigateURL="~/LearnMore.aspx" Target="_blank">Learn More</asp:HyperLink></span>
                </div>
                </form>
            </section>
            <!-- .signIn -->
        </div>
        <!-- .content -->
    </div>
    <!-- .pageWrap -->

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="js/vendor/jquery-1.10.2.min.js"><\/script>')</script>
    <script src="js/plugins.js"></script>
    <script src="js/main.js"></script>

    <!-- Google Analytics: change UA-XXXXX-X to be your site's ID and uncomment to use. 
  <script>
  	(function(b,o,i,l,e,r){b.GoogleAnalyticsObject=l;b[l]||(b[l]=
  		function(){(b[l].q=b[l].q||[]).push(arguments)});b[l].l=+new Date;
  	e=o.createElement(i);r=o.getElementsByTagName(i)[0];
  	e.src='//www.google-analytics.com/analytics.js';
  	r.parentNode.insertBefore(e,r)}(window,document,'script','ga'));
  	ga('create','UA-XXXXX-X');ga('send','pageview');
  </script> -->
</body>
</html>
