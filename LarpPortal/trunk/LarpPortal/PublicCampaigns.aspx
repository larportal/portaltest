<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.master" AutoEventWireup="true" CodeBehind="PublicCampaigns.aspx.cs" Inherits="LarpPortal.PublicCampaigns1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
    <div class="contentArea">
        <aside>
            <div class="panel-wrapper">
                <p><b>Find By:</b></p>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged">
                    <asp:ListItem>Game System</asp:ListItem>
                    <asp:ListItem>Campaign</asp:ListItem>
                    <asp:ListItem>Genre</asp:ListItem>
                    <asp:ListItem>Style</asp:ListItem>
                    <asp:ListItem>Tech Level</asp:ListItem>
                    <asp:ListItem>Size</asp:ListItem>
                </asp:DropDownList>
                <br /><br />
                <p><b>Filter By:</b> (Choose multiple options to narrow the search):</p>
                <ul class="list-unstyled">
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkGameSystem" runat="server" AutoPostBack="true" OnCheckedChanged="chkGameSystem_CheckedChanged" />
                                Game System:
                            </label>
                            <asp:DropDownList ID="ddlGameSystem" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlGameSystem_SelectedIndexChanged">
                                <asp:ListItem Text="Accelerant" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkCampaign" runat="server" AutoPostBack="true" OnCheckedChanged="chkCampaign_CheckedChanged" />
                                Campaign:
                            </label>
                            <asp:DropDownList ID="ddlCampaign" runat="server" Visible="false" OnSelectedIndexChanged="ddlCampaign_SelectedIndexChanged">
                                <asp:ListItem Text="7th Kingdom" Value="34"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkGenre" runat="server" AutoPostBack="true" OnCheckedChanged="chkGenre_CheckedChanged" />
                                Genre:
                            </label>
                            <asp:DropDownList ID="ddlGenre" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlGenre_SelectedIndexChanged">
                                <asp:ListItem Text="Adventure" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkStyle" runat="server" AutoPostBack="true" OnCheckedChanged="chkStyle_CheckedChanged" />
                                Style:
                            </label>
                            <asp:DropDownList ID="ddlStyle" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged">
                                <asp:ListItem Text="Boffer" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkTechLevel" runat="server" AutoPostBack="true" OnCheckedChanged="chkTechLevel_CheckedChanged" />
                                Tech Level:
                            </label>
                            <asp:DropDownList ID="ddlTechLevel" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlTechLevel_SelectedIndexChanged">
                                <asp:ListItem Text="Age of Sail" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkSize" runat="server" AutoPostBack="true" OnCheckedChanged="chkSize_CheckedChanged" />
                                Size:
                            </label>
                            <asp:DropDownList ID="ddlSize" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                                <asp:ListItem Text="No limit" Value="0"></asp:ListItem>
                                <asp:ListItem Text="<20" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkZipCode" runat="server" AutoPostBack="true" OnCheckedChanged="chkZipCode_CheckedChanged" />
                                Area / Zip Code:
                            </label>
                            <asp:TextBox ID="txtZipCode" runat="server" AutoPostBack="true" Visible="false" OnTextChanged="txtZipCode_TextChanged"></asp:TextBox>
                            <asp:DropDownList ID="ddlMileRadius" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlMileRadius_SelectedIndexChanged">
                                <asp:ListItem Text="10 miles" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkEndedCampaigns" runat="server" AutoPostBack="true" OnCheckedChanged="chkEndedCampaigns_CheckedChanged"/>
                                Include campaigns that have ended
                            </label>
                        </div>
                    </li>
                </ul>
            </div>
        </aside>
        <div class="mainContent tab-content">
            <section id="public-campaigns">
                <div class="row">
                    <div class="panel-wrapper col-sm-3">
                        <div class="panel">
                            <div class="panelheader">
                                <h2>Campaign Search<asp:Label ID="lblCampaignSearchBy" runat="server"> by Game System</asp:Label></h2>
                                <div class="panel-body">
                                    <div class="panel-container  search-criteria">
                                        <asp:TreeView ID="tvGameSystem" runat="server" Visible="true" OnSelectedNodeChanged="tvGameSystem_SelectedNodeChanged" ExpandDepth="0"></asp:TreeView>
                                        <asp:TreeView ID="tvCampaign" runat="server" Visible="false" OnSelectedNodeChanged="tvCampaign_SelectedNodeChanged" ExpandDepth="0" ></asp:TreeView>
                                        <asp:TreeView ID="tvGenre" runat="server" Visible="false" OnSelectedNodeChanged="tvGenre_SelectedNodeChanged" ExpandDepth="0" ></asp:TreeView>
                                        <asp:TreeView ID="tvStyle" runat="server" Visible="false" OnSelectedNodeChanged="tvStyle_SelectedNodeChanged" ExpandDepth="0" ></asp:TreeView>
                                        <asp:TreeView ID="tvTechLevel" runat="server" Visible="false" OnSelectedNodeChanged="tvTechLevel_SelectedNodeChanged" ExpandDepth="0" ></asp:TreeView>
                                        <asp:TreeView ID="tvSize" runat="server" Visible="false" OnSelectedNodeChanged="tvSize_SelectedNodeChanged" ExpandDepth="0" ></asp:TreeView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="pnlImageURL" runat="server">
                    <div class="panel-wrapper col-sm-9">
                        <asp:Table ID="tblCampaignImage" runat="server" Height="130" Width="820">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Image ID="imgCampaignImage" runat="server" AlternateText="Game/Campaign Logo" ImageUrl="img/Logo/CM-1-Madrigal.jpg" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <p>
                        <asp:HyperLink ID="hplLinkToSite" runat="server" NavigateURL="." Target="_blank" Font-Underline="true" ></asp:HyperLink></p>
                    </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlOverview" runat="server">
                    <div class="col-md-5 panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <h2><asp:Label ID="lblGorC1" runat="server"></asp:Label> Overview</h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container campaign-overview">
                                    <asp:Label ID="lblCampaignOverview" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlSelectors" runat="server">
                    <div class="col-md-4 panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <h2><asp:Label ID="lblGorC2" runat="server"></asp:Label></h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container">
                                    <asp:Table ID="tblSelectors" runat="server" Width="100%">
                                        <asp:TableHeaderRow>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblGameSystem1" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                &nbsp;&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblGameSystem2" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableHeaderRow>
                                        <asp:TableHeaderRow>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblGenre1" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                &nbsp;&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblGenre2" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableHeaderRow>
                                        <asp:TableHeaderRow>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblStyle1" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                &nbsp;&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblStyle2" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableHeaderRow>
                                        <asp:TableHeaderRow>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblTechLevel1" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                &nbsp;&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblTechLevel2" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableHeaderRow>
                                        <asp:TableHeaderRow>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblSize1" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                &nbsp;&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblSize2" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableHeaderRow>
                                        <asp:TableHeaderRow>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblLocation1" runat="server">Location:</asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                &nbsp;&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblLocation2" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableHeaderRow>
                                        <asp:TableHeaderRow>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblEvent1" runat="server">Next Event:</asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                &nbsp;&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblEvent2" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableHeaderRow>
                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlSignUpForCampaign" runat="server">
                        <div class="col-md-4 panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <h2><asp:Label ID="lblSignUp" runat="server"></asp:Label>Add to Your Campaigns</h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container">
                                    <asp:Table ID="tblAddCampaigns" runat="server" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell VerticalAlign="Top">
                                                Available Roles:<br />
<%--                                                <asp:RadioButtonList ID="btnSignUp" runat="server" RepeatDirection="Horizontal"  RepeatLayout="Table">
                                                </asp:RadioButtonList>--%>
                                                <asp:CheckBoxList ID="chkSignUp" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table"></asp:CheckBoxList>
                                                <asp:Button ID="btnSignUpForCampaign" runat="server" CssClass="btn btn-primary" Visible="false" Text="Submit Request" OnClick="btnSignUpForCampaign_Click" />
                                                <asp:Label ID="lblSignUpMessage" runat="server"></asp:Label>
                                                <asp:Label ID="lblCurrentCampaign" runat="server" Visible="false"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell VerticalAlign="Top">
                                                &nbsp;&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell VerticalAlign="Top">
                                                Current Roles:
                                    <asp:Repeater ID="listCurrentRoles" runat="server">
                                        <HeaderTemplate>
                                            <div class="panel-container scroll-150">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("RoleDescription")%><br />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                                <asp:Label ID="lblCurrentRoles" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                    </asp:Panel>
                </div>
            </section>
        </div>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="js/vendor/jquery-1.10.2.min.js"><\/script>')</script>
    <script src="js/bootstrap/tab.js"></script>
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
</asp:Content>
