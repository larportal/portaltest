<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.master" AutoEventWireup="true" CodeBehind="PublicCampaigns.aspx.cs" Inherits="LarpPortal.PublicCampaigns1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
    <div class="contentArea">
        <aside>
            <div class="panel-wrapper">
                <p>Sort By:</p>
                <asp:DropDownList ID="ddlOrderBy" runat="server">
                    <asp:ListItem>Game System</asp:ListItem>
                    <asp:ListItem>Campaign</asp:ListItem>
                    <asp:ListItem>Genre</asp:ListItem>
                    <asp:ListItem>Style</asp:ListItem>
                    <asp:ListItem>Tech Level</asp:ListItem>
                    <asp:ListItem>Size</asp:ListItem>
                    <asp:ListItem>Zip Code</asp:ListItem>
                </asp:DropDownList>
                <p>Search By:</p>
                <ul class="list-unstyled">
                    <li>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkGameSystem" runat="server" />
                                Game System:
                            </label>
                            <asp:DropDownList ID="ddlGameSystem" runat="server">
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
                                <asp:CheckBox ID="chkCampaign" runat="server" />
                                Campaign:
                            </label>
                            <asp:DropDownList ID="ddlCampaign" runat="server">
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
                                <asp:CheckBox ID="chkGenre" runat="server" />
                                Genre:
                            </label>
                            <asp:DropDownList ID="DropDownList1" runat="server">
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
                                <asp:CheckBox ID="chkStyle" runat="server" />
                                Style:
                            </label>
                            <asp:DropDownList ID="ddlStyle" runat="server">
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
                                <asp:CheckBox ID="chkTechLevel" runat="server" />
                                Tech Level:
                            </label>
                            <asp:DropDownList ID="ddlTechLevel" runat="server">
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
                                <asp:CheckBox ID="chkSize" runat="server" />
                                Size:
                            </label>
                            <asp:DropDownList ID="ddlSize" runat="server">
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
                                <asp:CheckBox ID="chkZipCode" runat="server" />
                                Zip Code:
                            </label>
                            <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
                            <p>
                                <asp:Label ID="lblMileRadius" runat="server">Distance from Zip Code:</asp:Label></p>
                            <asp:DropDownList ID="ddlMileRadius" runat="server">
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
                                <asp:CheckBox ID="chkEndedCampaigns" runat="server" />
                                Include campaigns that have ended
                            </label>
                        </div>
                    </li>
                </ul>
                <div>
                    <a href="~/CampaignInfo.aspx" data-toggle="pill">Member Campaigns</a>
                </div>
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
                                        <ul>
                                            <li>
                                                <label class="tree-toggle">Selector 1 <span class="caret"></span></label>
                                                <ul class="tree">
                                                    <li><a href="#">Sub-selector 1-2</a></li>
                                                    <li><a href="#">Sub-selctor 1-2</a></li>
                                                </ul>
                                            </li>
                                            <li>
                                                <label class="tree-toggle nav-header">Selector 2<span class="caret"> </span></label>
                                                <ul class="tree">
                                                    <li><a href="#">Sub-Selctor 2-1</a></li>
                                                    <li><a href="#">Sub-Selctor 2-1</a></li>
                                                </ul>
                                            </li>
                                            <li>
                                                <label class="tree-toggle nav-header">Selector 3<span class="caret"> </span></label>
                                                <ul class="tree">
                                                    <li><a href="#">Sub-Selctor 3-1</a></li>
                                                    <li><a href="#">Sub-Selctor 3-2</a></li>
                                                    <li><a href="#">Sub-Selctor 3-3</a></li>
                                                    <li><a href="#">Sub-Selctor 3-4</a></li>
                                                </ul>
                                            </li>
                                            <li>
                                                <label class="tree-toggle nav-header">Selector 4<span class="caret"> </span></label>
                                                <ul class="tree">
                                                    <li><a href="#">Sub-Selctor 4-1</a></li>
                                                    <li><a href="#">Sub-Selctor 4-2</a></li>
                                                    <li><a href="#">Sub-Selctor 4-3</a></li>
                                                    <li><a href="#">Sub-Selctor 4-4</a></li>
                                                </ul>
                                            </li>
                                            <li>
                                                <label class="tree-toggle nav-header">Selector 5<span class="caret"> </span></label>
                                                <ul class="tree">
                                                    <li><a href="#">Sub-Selctor 5-1</a></li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- .panel-wrapper -->

                    <div class="panel-wrapper col-sm-9">
                        <a href="#">Link to Site</a>
                        <img src="http://placehold.it/820x130" alt="Banner of Game">
                    </div>

                    <div class="col-md-5 panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <h2>Campaign Overview</h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container campaign-overview">
                                    <p>What is Cottington Woods? Cottington Woods is o LARP set in the Written World, o world of fairy tales, toll tales, campfire tales, legends and lore. It is intended to be dark yet heroic, where we supply the dark, and our players will provide the heroic. Although we stretch the definitions, our player characters ore oil human and very mortal</p>

                                    <p>Is the game tongue ir cheek? Most emphatically no~ While it is the case that sometimes embarrassing things may transpire, or o curse may sometimes make o character look less heroic than he might like, the game is not designed to be humorous. Instead, the game will be somewhat dark and sometimes horrific</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <h2>Selectors for Campaign</h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container">
                                    <ul>
                                        <li>Sys</li>
                                        <li>Genre</li>
                                        <li>Style</li>
                                        <li>Tech Level</li>
                                        <li>Size</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

        </div>
        <!-- mainContent .tab-content -->
    </div>
    <!-- contentArea -->

    <footer>
        <div>
            <p>&copy; Copyright 2014 LARP Portal - Placeholder for gratuitous footer</p>
        </div>
    </footer>

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
