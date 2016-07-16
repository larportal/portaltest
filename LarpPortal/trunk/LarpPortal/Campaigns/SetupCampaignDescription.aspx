<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="SetupCampaignDescription.aspx.cs" ValidateRequest="false" Inherits="LarpPortal.Campaigns.SetupCampaignDescription" %>
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
    <div class="mainContent tab-content col-lg-5 input-group">
        <section id="campaign-info" class="campaign-info tab-pane active">
            <div role="form" class="form-horizontal">
                <div class="col-sm-12 NoPadding">
                    <h1 class="col-sm-12">Campaign Setup Information</h1>
                </div>
                <div class="row">
                    <div class="col-sm-10"></div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnSaveChanges" runat="server" Text="Save" OnClick="btnSaveChanges_Click" />
                    </div>
                </div>
                <div class="row col-sm-12 NoPadding" style="padding-left: 25px;">
                    <asp:Panel ID="pnlWebPageDescription" runat="server">
                        <div class="col-lg-12 NoPadding" style="padding-left: 15px;">
                            <div class="panel NoPadding" style="padding-top: 0px; padding-bottom: 0px; min-height: 50px;">
                                <div class="panelheader NoPadding">
                                    <h2>Web Page Description</h2>
                                    <div class="panel-body NoPadding">
                                        <div class="panel-container NoPadding">
                                            <div class="row PrePostPadding">
                                                <div class="col-sm-12 NoPadding" style="padding-left: 5px;">
                                                    <asp:TextBox ID="tbWebPageDescription" runat="server" TextMode="MultiLine" CssClass="col-sm-12 NoPadding" Rows="10" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-sm-10"></div>
                        <div class="col-sm-2">
                            <asp:Button ID="btnSaveRepeat" runat="server" Text="Save" OnClick="btnSaveChanges_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>


