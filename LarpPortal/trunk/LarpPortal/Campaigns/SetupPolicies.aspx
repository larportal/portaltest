<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="SetupPolicies.aspx.cs" Inherits="LarpPortal.Campaigns.SetupPolicies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MemberStyles" runat="server">
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

        .spaced input[type="radio"] {
            margin-right: 5px;
            margin-left: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MemberCampaignsContent" runat="server">
    <div class="mainContent tab-content col-lg-10 input-group">
        <section id="campaign-info" class="campaign-info tab-pane active">
            <div role="form" class="form-horizontal">
                <div class="col-sm-12 NoPadding">
                    <h1 class="col-sm-12">Campaign Setup Information</h1>
                </div>
<%--                <div class="row">
                    <div class="col-sm-10"></div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnSaveChanges" runat="server" CssClass="StandardButton" Text="Save" OnClick="btnSaveChanges_Click" />
                    </div>
                </div>
                <div class="row PrePostPadding"></div>--%>
                <div class="row col-sm-12 NoPadding" style="padding-left: 25px;">
                    <asp:Panel ID="pnlPolicy" runat="server">
                        <div class="col-sm-12 NoPadding" style="padding-left: 15px;">
                            <div class="panel NoPadding" style="padding-top: 0px; padding-bottom: 0px; min-height: 50px;">
                                <div class="panelheader NoPadding">
                                    <h2>Policies</h2>
                                    <div class="panel-body NoPadding">
                                        <div class="panel-container NoPadding">
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-sm-3">
                                                    Allow Character Rebuilds:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:CheckBox ID="chkAllowCharacterRebuilds" runat="server" />
                                                </div>
                                                <div class="TableLabel col-sm-3">
                                                    Allow CP Donation:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:CheckBox ID="chkAllowCPDonation" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">

                                                <div class="TableLabel col-sm-3">
                                                    Share Location Use Notes:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:CheckBox ID="chkShareLocationUseNotes" runat="server" />
                                                </div>
                                                <div class="TableLabel col-sm-3">
                                                    NPC Approval Required:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:CheckBox ID="chkNPCApprovalRequired" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-sm-3">
                                                    Use Campaign Characters:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:CheckBox ID="chkUseCampaignCharacters" runat="server" />
                                                </div>
                                                <div class="TableLabel col-sm-3">
                                                    PC Approval Required:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:CheckBox ID="chkPCApprovalRequired" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding"></div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-sm-3">
                                                    PEL Approval Level:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:DropDownList ID="ddlPELApprovalLevel" runat="server">

                                                    </asp:DropDownList>
                                                </div>
                                                <div class="TableLabel col-sm-3">
                                                    Character Approval Level:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:DropDownList ID="ddlCharacterApprovalLevel" runat="server">

                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding"></div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-sm-3">
                                                    Earliest Point Application Year:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:TextBox ID="tbEarliestCPApplicationYear" runat="server" Style="width: 95%" AutoPostBack="true" OnTextChanged="tbEarlestCPApplicationYear_TextChanged">

                                                    </asp:TextBox>
                                                </div>
                                                <div class="TableLabel col-sm-3">
                                                    Event Character Cap:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:TextBox ID="tbEventCharacterCap" runat="server" Style="width: 95%" AutoPostBack="true" OnTextChanged="tbEventCharacterCap_TextChanged">

                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-sm-3">
                                                    Max Points Per Year:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:TextBox ID="tbMaximumCPPerYear" runat="server" Style="width: 95%" AutoPostBack="true" OnTextChanged="tbMaximumCPPerYear_TextChanged">

                                                    </asp:TextBox>
                                                </div>
                                                <div class="TableLabel col-sm-3">
                                                    Total Character Cap:
                                                </div>
                                                <div class="col-sm-3 NoPadding">
                                                    <asp:TextBox ID="tbTotalCharacterCap" runat="server" Style="width: 95%" AutoPostBack="true" OnTextChanged="tbTotalCharacterCap_TextChanged">

                                                    </asp:TextBox>
                                                </div>
                                                <div class="row PrePostPadding">
                                                <div class="TableLabel col-sm-3">
                                                    Cross Campaign Posting:
                                                </div>
                                                <div class="col-sm-9 NoPadding">
                                                    <asp:TextBox ID="tbCrossCampaignPosting" runat="server" Style="width: 95%" TextMode="MultiLine" Rows="5" ></asp:TextBox>
                                                </div>
                                                </div>
                                                <div class="row PrePostPadding"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row PrePostPadding"></div>
                    <div class="row PrePostPadding">
                        <div class="col-lg-11"></div>
                        <div class="col-lg-1">
                            <asp:Button ID="btnSaveRepeat" runat="server" CssClass="StandardButton" Text="Save" OnClick="btnSaveChanges_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>


