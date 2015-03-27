<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.master" AutoEventWireup="true" CodeBehind="PublicCampaigns.aspx.cs" Inherits="LarpPortal.PublicCampaigns1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
    <div class="contentArea">
        <aside>
            <div class="panel-wrapper">
                <p>Find By:</p>
                <asp:DropDownList ID="ddlOrderBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged">
                    <asp:ListItem>Game System</asp:ListItem>
                    <asp:ListItem>Campaign</asp:ListItem>
                    <asp:ListItem>Genre</asp:ListItem>
                    <asp:ListItem>Style</asp:ListItem>
                    <asp:ListItem>Tech Level</asp:ListItem>
                    <asp:ListItem>Size</asp:ListItem>
                </asp:DropDownList>
                <p>Filter By (Choose multiple options to narrow the search):</p>
                <ul class="list-unstyled">
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkGameSystem" runat="server" AutoPostBack="true" OnCheckedChanged="chkGameSystem_CheckedChanged" />
                                Game System:
                            </label>
                            <asp:DropDownList ID="ddlGameSystem" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlGameSystem_SelectedIndexChanged">
                                <asp:ListItem Text="Accelerant" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Alliance" Value="14"></asp:ListItem>
                                <asp:ListItem Text="Amtgard" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Belegarth" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Dagorhir" Value="8"></asp:ListItem>
                                <asp:ListItem Text="GLASS" Value="10"></asp:ListItem>
                                <asp:ListItem Text="NERO" Value="2"></asp:ListItem>
                                <asp:ListItem Text="White Wolf" Value="5"></asp:ListItem>
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
                                <asp:ListItem Text="Aftermath LARP" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Alliance Calgary" Value="66"></asp:ListItem>
                                <asp:ListItem Text="Alliance CT Calderie" Value="73"></asp:ListItem>
                                <asp:ListItem Text="Alliance Deadland" Value="67"></asp:ListItem>
                                <asp:ListItem Text="Alliance Gettysburg" Value="68"></asp:ListItem>
                                <asp:ListItem Text="Amtguard" Value="60"></asp:ListItem>
                                <asp:ListItem Text="Aralis 2" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Ascendant" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Bloodlines" Value="35"></asp:ListItem>
                                <asp:ListItem Text="Brittanis Dark Arthurian Adventure" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Camp Hatchet" Value="72"></asp:ListItem>
                                <asp:ListItem Text="Celestial Kingdom - Bifost" Value="21"></asp:ListItem>
                                <asp:ListItem Text="Celestial Kingdom - Dragon Skull Keep" Value="22"></asp:ListItem>
                                <asp:ListItem Text="Clockwork Skies" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Cottington Woods" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Endgame" Value="16"></asp:ListItem>
                                <asp:ListItem Text="Invictus" Value="17"></asp:ListItem>
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
                                <asp:ListItem Text="Cyberpunk" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Dystopian" Value="30"></asp:ListItem>
                                <asp:ListItem Text="Fairy Tale" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Goth" Value="28"></asp:ListItem>
                                <asp:ListItem Text="Heroic" Value="9"></asp:ListItem>
                                <asp:ListItem Text="High Fantasy" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Horror" Value="11"></asp:ListItem>
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
                                <asp:ListItem Text="Freeform" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Live Combat" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Re-Enactment" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Theater" Value="2"></asp:ListItem>
                                <asp:ListItem Text="War Games" Value="4"></asp:ListItem>
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
                                <asp:ListItem Text="Bronze Age" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Digital Age" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Industrial Revolution" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Iron Age" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Mechanized Age" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Medieval" Value="4"></asp:ListItem>
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
                                <asp:ListItem Text="20-40" Value="2"></asp:ListItem>
                                <asp:ListItem Text="41-60" Value="3"></asp:ListItem>
                                <asp:ListItem Text="61-80" Value="4"></asp:ListItem>
                                <asp:ListItem Text="81-100" Value="5"></asp:ListItem>
                                <asp:ListItem Text="101-150" Value="6"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkZipCode" runat="server" AutoPostBack="true" OnCheckedChanged="chkZipCode_CheckedChanged" />
                                Zip Code:
                            </label>
                            <asp:TextBox ID="txtZipCode" runat="server" AutoPostBack="true" Visible="false" OnTextChanged="txtZipCode_TextChanged"></asp:TextBox>
                            <p>
                                <asp:Label ID="lblMileRadius" runat="server">Distance from Zip Code:</asp:Label></p>
                            <asp:DropDownList ID="ddlMileRadius" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlMileRadius_SelectedIndexChanged">
                                <asp:ListItem Text="10 miles" Value="1"></asp:ListItem>
                                <asp:ListItem Text="25 miles" Value="2"></asp:ListItem>
                                <asp:ListItem Text="50 miles" Value="3"></asp:ListItem>
                                <asp:ListItem Text="100 miles" Value="4"></asp:ListItem>
                                <asp:ListItem Text="150 miles" Value="5"></asp:ListItem>
                                <asp:ListItem Text="200 miles" Value="6"></asp:ListItem>
                                <asp:ListItem Text="300 miles" Value="7"></asp:ListItem>
                                <asp:ListItem Text="400 miles" Value="8"></asp:ListItem>
                                <asp:ListItem Text="500 miles" Value="9"></asp:ListItem>
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
                                <h2>Campaign Search</h2>
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
                                    <asp:Table ID="tblSelectors" runat="server">
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
                                    <asp:Table ID="tblAddCampaigns" runat="server" CellPadding ="30">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:RadioButtonList ID="btnSignUp" runat="server"  RepeatLayout="Table">
            <%--                                        <asp:ListItem Text="PC" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="NPC" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Both" Value="3"></asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                                <asp:Button ID="btnSignUpForCampaign" runat="server" Visible="false" Text="Submit Request" OnClick="btnSignUpForCampaign_Click" />
                                                <asp:Label ID="lblSignUpMessage" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                Current Roles:

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
