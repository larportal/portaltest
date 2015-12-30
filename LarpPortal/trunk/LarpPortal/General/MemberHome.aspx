<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="MemberHome.aspx.cs" Inherits="LarpPortal.General.MemberHome" %>

<asp:Content ID="MemberStyles" runat="server" ContentPlaceHolderID="HeaderStyles">
    <style type="text/css">
        .TableTextBox {
            border: 1px solid black;
            padding-left: 5px;
            padding-right: 5px;
        }

        th, tr:nth-child(even) > td {
            background-color: transparent;
        }

        .CharInfoTable {
            border-collapse: collapse;
        }

            .CharInfoTable td {
                padding: 4px;
            }

        div {
            border: 0px solid black;
        }

        span {
            vertical-align: middle;
        }

        .NoPadding {
            padding-left: 0px;
            padding-right: 0px;
        }

        .TextEntry {
            border: 1px solid black;
            padding: 0px;
        }

        .PnlDesign {
            border: solid 1px #000000;
            height: 150px;
            width: 330px;
            overflow-y: scroll;
            background-color: white;
            font-size: 15px;
            font-family: Arial;
        }

        .txtbox {
            background-image: url(../img/download.png);
            background-position: right top;
            background-repeat: no-repeat;
            cursor: pointer;
            border: 1px solid #A9A9A9;
        }

        .container {
            display: table;
            vertical-align: middle;
        }

        .button_class {
            display: table;
        }

        .button {
            display: table-cell;
            width: 100%;
        }

        .vertical-center-row {
            display: table-cell;
        }
    </style>

    <link rel="stylesheet" href="http://localhost:49282/code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainPage" runat="server">
    <div class="contentArea">
        <aside>
        </aside>
        <div class="mainContent tab-content">
            <section id="larping-info">
                <div class="row">
                    <div class="panel-wrapper col-md-10">
                        <div class="panel">
                            <div class="panel-header">
                                <h2>Player Quick Navigation Menu</h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container">

                                    <div class="row PrePostPadding">
                                        <div class="button_class col-sm-3">
                                            <asp:Button ID="btnAddCampaign" runat="server" CssClass="StandardButton" Text=" SIGN UP for a Campaign " OnClick="btnAddCampaign_Click" Width="100%" Height="50px" Font-Size="Medium" />
                                        </div>
                                        <div style="text-align: left" class="text-justify vertical-center-row col-sm-9">
                                            <asp:Label ID="lblAddCampaign" runat="server" Text="Search for campaigns to PC, NPC or Staff. To sign up, select a PC and/or NPC role. Use this same screen to add a secondary role in order to register and play both sides of a campaign." Font-Size="Medium"></asp:Label>
                                            </div>
                                    </div>

                                    <div class="row PrePostPadding">
                                        <div class="TableLabel col-sm-3">
                                        </div>
                                        <div class="TableLabel col-sm-9">
                                        </div>
                                    </div>

                                    <div class="row PrePostPadding">
                                        <div class="button_class col-sm-3">
                                            <asp:Button ID="btnGotoCharacterSkills" runat="server" CssClass="StandardButton" Text=" UPDATE MY Characters " OnClick="btnGotoCharacterSkills_Click" Width="100%" Height="50px" Font-Size="Medium" />
                                        </div>
                                        <div style="text-align: left" class="text-justify vertical-center-row col-sm-9">
                                            <asp:Label ID="lblGoToCharacterSkills" runat="server" Text="Add or maintain your character.  You can describe your character, buy skills, define your relationships, provide plot with information, and print your character card." Font-Size="Medium"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row PrePostPadding">
                                        <div class="TableLabel col-sm-3">
                                        </div>
                                        <div class="TableLabel col-sm-9">
                                        </div>
                                    </div>

                                    <div class="row PrePostPadding">
                                        <div class="button_class col-sm-3">
                                            <asp:Button ID="btnRegisterForEvent" runat="server" CssClass="StandardButton" Text=" REGISTER for an Event " OnClick="btnRegisterForEvent_Click" Width="100%" Height="50px" Font-Size="Medium" />
                                        </div>
                                        <div style="text-align: left" class="text-justify vertical-center-row col-sm-9">
                                            <asp:Label ID="lblRegisterForEvent" runat="server" Text="Register for an event as a PC, NPC or Staff.  Each role is prompted to provide specific information needed for logistics." Font-Size="Medium"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row PrePostPadding">
                                        <div class="TableLabel col-sm-3">
                                        </div>
                                        <div class="TableLabel col-sm-9">
                                        </div>
                                    </div>

                                    <div class="row PrePostPadding">
                                        <div class="button_class col-sm-3">
                                            <asp:Button ID="btnWritePEL" runat="server" CssClass="StandardButton" Text=" WRITE MY PEL " OnClick="btnWritePEL_Click" Width="100%" Height="50px" Font-Size="Medium" />
                                        </div>
                                        <div style="text-align: left" class="text-justify vertical-center-row col-sm-9">
                                            <asp:Label ID="lblWritePEL" runat="server" Text="Write, edit, save and submit your new post event summary letters (PELs) and view your historical submissions." Font-Size="Medium"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row PrePostPadding">
                                        <div class="TableLabel col-sm-3">
                                        </div>
                                        <div class="TableLabel col-sm-9">
                                        </div>
                                    </div>

                                    <div class="row PrePostPadding">
                                        <div class="button_class col-sm-3">
                                            <asp:Button ID="btnCheckPoints" runat="server" CssClass="StandardButton" Text=" VIEW MY Points " OnClick="btnCheckPoints_Click" Width="100%" Height="50px" Font-Size="Medium" />
                                        </div>
                                        <div style="text-align: left" class="text-justify vertical-center-row col-sm-9">
                                            <asp:Label ID="lblCheckPoints" runat="server" Text="View your earned, spent and banked points at a campaign level." Font-Size="Medium"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
