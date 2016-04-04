<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="CampaignSetup.aspx.cs" Inherits="LarpPortal.Campaigns.CampaignSetup" %>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="MemberStyles">
    <style type="text/css">
        .TableTextBox {
            border: 1px solid black;
            padding-left: 5px;
            padding-right: 5px;
        }

        th, tr:nth-child(even) > td {
            background-color: transparent;
        }

        .CampaignInfoTable {
            border-collapse: collapse;
        }

            .CampaignInfoTable td {
                padding: 4px;
            }

        div {
            border: 0px solid black;
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

        .vertical-center-row {
            display: table-cell;
            vertical-align: middle;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MemberCampaignsContent" runat="server">
    <div class="mainContent tab-content col-lg-12 input-group">
        <section id="campaign-info" class="campaign-info tab-pane active">
            <div role="form" class="form-horizontal">
                <div class="col-lg-12 NoPadding">
                    <h1 class="col-lg-12">Campaign Setup Information</h1>
                </div>
                <div class="row col-sm-12 NoPadding" style="padding-left: 25px;">
                    <asp:MultiView ID="mvCampaignInfo" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vwDemographics" runat="server">
                            <div class="row col-lg-12 NoPadding">
                                <div class="col-lg-6 NoPadding">
                                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                                        <div class="panelheader NoPadding">
                                            <h2>Demographics</h2>
                                            <div class="panel-body NoPadding">
                                                <div class="panel-container">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Campaign Name: </div>
                                                        <div class="col-sm-3 NoPadding">
                                                            <asp:Label ID="lblCampaignName" runat="server" Text="" />
                                                        </div>
                                                        <div class="TableLabel col-lg-2" style="position: relative;">Status: </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:DropDownList ID="ddlCampaignStatus" CssClass="NoPadding" runat="server" Style="z-index: 500; position: relative;" OnSelectedIndexChanged="ddlCampaignStatus_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                            Game System: 
                                                        </div>
                                                        <div class="col-sm-3 NoPadding">
                                                            <asp:DropDownList ID="ddlGameSystem" CssClass="NoPadding" runat="server" Style="z-index: 500; position: relative" OnSelectedIndexChanged="ddlGameSystem_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                        <div class="TableLabel col-lg-2" style="position: relative;">Date Started: </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:TextBox ID="tbDateStarted" runat="server" CssClass="TableTextBox" />
                                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="tbDateStarted" Format="MM/dd/yyyy" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                            Owner: 
                                                        </div>
                                                        <div class="col-sm-3 NoPadding">
                                                            <asp:Label ID="lblOwner" runat="server" />
                                                        </div>
                                                        <div class="TableLabel col-lg-2" style="position: relative;">Exp.End Date: </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:TextBox ID="tbExpectedEndDate" runat="server" CssClass="TableTextBox" />
                                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="tbExpectedEndDate" Format="MM/dd/yyyy" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                            Campaign Zip: 
                                                        </div>
                                                        <div class="col-sm-3 NoPadding">
                                                            <asp:TextBox ID="tbCampaignZip" runat="server" />
                                                        </div>
                                                        <div class="TableLabel col-lg-2" style="position: relative;">Actual End Date: </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:TextBox ID="tbActualEndDate" runat="server" CssClass="TableTextBox" />
                                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="tbActualEndDate" Format="MM/dd/yyyy" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                            Primary Site: 
                                                        </div>
                                                        <div class="col-sm-3 NoPadding">
                                                            <asp:DropDownList ID="ddlPrimarySite" CssClass="NoPadding" runat="server" Style="z-index: 500; position: relative;" OnSelectedIndexChanged="ddlPrimarySite_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="TableLabel col-lg-2">
                                                            Avg # Events:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:TextBox ID="tbAvgNoEvents" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                            Emergency Contact: 
                                                        </div>
                                                        <div class="col-sm-3 NoPadding">
                                                            <asp:TextBox ID="tbEmergencyContact" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="TableLabel col-sm-2">
                                                            Phone: 
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:TextBox ID="tbEmergencyPhone" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                            Style:
                                                        </div>
                                                        <div class="col-sm-3 NoPadding">
                                                            <asp:DropDownList ID="ddlStyle" CssClass="NoPadding" runat="server" Style="z-index: 500; position: relative" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                        <div class="TableLabel col-lg-2">
                                                            Tech Level:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:DropDownList ID="ddlTechLevel" CssClass="NoPadding" runat="server" Style="z-index: 500; position: relative" OnSelectedIndexChanged="ddlTechLevel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                            Size:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:DropDownList ID="ddlSize" CssClass="NoPadding" runat="server" Style="z-index: 500; position: relative" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                        <%--                                                        <div class="TableLabel col-sm-3">
                                                            TBD:
                                                        </div>
                                                                                                                    <div class="col-lg-3 NoPadding">
                                                                <asp:Label ID="lblTBD" runat="server" />
                                                            </div>--%>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                            Genres:
                                                        </div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:Label ID="lblGenres" runat="server" />
                                                        </div>
                                                        <div class="col-lg-2 NoPadding">
                                                            <asp:Button ID="btnEditGenres" runat="server" CssClass="StandardButton" Text="Edit" OnClick="btnEditGenres_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                            Periods:
                                                        </div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:Label ID="lblPeriods" runat="server" />
                                                        </div>
                                                        <div class="col-lg-2 NoPadding">
                                                            <asp:Button ID="btnEditPeriods" runat="server" CssClass="StandardButton" Text="Edit" OnClick="btnEditPeriods_Click" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-lg-6 NoPadding" style="padding-left: 15px;">
                                    <div class="panel NoPadding" style="padding-top: 0px; padding-bottom: 0px; min-height: 50px;">
                                        <div class="panelheader NoPadding">
                                            <h2>Web Page Description</h2>
                                            <div class="panel-body NoPadding">
                                                <div class="panel-container NoPadding">
                                                    <div class="row PrePostPadding">
                                                        <div class="col-lg-12 NoPadding" style="padding-left: 5px;">
                                                            <asp:TextBox ID="tbWebPageDescription" runat="server"  TextMode="MultiLine" CssClass="col-lg-12 NoPadding" Rows="6"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 14px;"></div>
                                    <div class="col-lg-12 NoPadding">
                                        <div class="panel NoPadding" style="padding-top: 0px; padding-bottom: 0px; min-height: 50px;">
                                            <div class="panelheader NoPadding">
                                                <h2>Requirements</h2>
                                                <div class="panel-body NoPadding">
                                                    <div class="panel-container NoPadding">
                                                        <div class="row PrePostPadding">
                                                            <div class="TableLabel col-sm-3">
                                                                Membership Fee:
                                                            </div>
                                                            <div class="col-sm-3 NoPadding">
                                                                <asp:TextBox ID="tbMembershipFee" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="TableLabel col-lg-2">
                                                                Frequency:
                                                            </div>
                                                            <div class="col-lg-3 NoPadding">
                                                                <asp:DropDownList ID="ddlFrequency" CssClass="NoPadding" runat="server" Style="z-index: 500; position: relative" OnSelectedIndexChanged="ddlFrequency_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="row PrePostPadding">
                                                            <div class="TableLabel col-sm-3">
                                                                Minimum Age:
                                                            </div>
                                                            <div class="col-sm-3 NoPadding">
                                                                <asp:TextBox ID="tbMinAge" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="TableLabel col-lg-2">
                                                                Supervised Age:
                                                            </div>
                                                            <div class="col-lg-3 NoPadding">
                                                                <asp:TextBox ID="tbSuperAge" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row PrePostPadding">
                                                            <div class="TableLabel col-sm-3">
                                                                Waiver:
                                                            </div>
                                                            <div class="col-sm-3 NoPadding">
                                                                <asp:DropDownList ID="ddlWaiver" CssClass="NoPadding" runat="server" Style="z-index: 500; position: relative" OnSelectedIndexChanged="ddlWaiver_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

