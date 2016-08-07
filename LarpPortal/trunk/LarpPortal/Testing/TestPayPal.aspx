<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="TestPayPal.aspx.cs" Inherits="LarpPortal.Testing.TestPayPal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderStyles" runat="server">
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

        .row {
            padding-left: 10px;
            padding-right: 10px;
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

<asp:Content ID="Content2" ContentPlaceHolderID="MainPage" runat="server">
    <div class="contentArea">
        <aside></aside>
        <div class="mainContent tab-content col-lg-6 input-group">
            <section id="campaign-info" class="campaign-info tab-pane active">
                <div role="form" class="form-horizontal">
                    <div class="col-sm-12 NoPadding">
                        <h1 class="col-sm-12">Campaign Name - Event Registration Payment</h1>
                    </div>
                    <div class="row col-sm-12 NoPadding" style="padding-left: 25px;">
                        <asp:Panel ID="pnlPaymentOptions" runat="server">
                            <div class="col-lg-12 NoPadding" style="padding-left: 15px;">
                                <div class="panel NoPadding" style="padding-top: 0px; padding-bottom: 0px; min-height: 50px;">
                                    <div class="panelheader NoPadding">
                                        <h2>Insert Event and Character here</h2>
                                        <div class="panel-body NoPadding">
                                            <div class="panel-container NoPadding">
                                                <div class="row PrePostPadding">
                                                    Event Registration ($80.00 per person):
                                                </div>
                                                <div class="row PrePostPadding">
<%--                                                    <asp:Label ID="lblRegistrationFormStart" runat="server">
                                                       
                                                    </asp:Label>--%>
                                                </div>
                                                <div class="row PrePostPadding">
                                                    At the current campsite, meal services are available during Adventure Weekends, and may be paid for ahead of time 
                                                    or at check-in - please note that the camp staff cannot accept payment for meals during meal times:
                                                </div>
                                                <div class="row PrePostPadding">&nbsp;</div>
                                                <div class="row PrePostPadding">
                                                    Saturday Brunch at 10AM ($6.00 per person):
                                                </div>
                                                <div class="row PrePostPadding">
                                                    <div role="form">
                                                        <input name="cmd" value="_xclick" type="hidden">
                                                        <input name="business" value="rciccolini@gmail.com" type="hidden">
                                                        <input name="return" value="http://www.larp.com/madrigal" type="hidden">
                                                        <input name="item_name" value="Saturday Brunch Meal Payment" type="hidden">
                                                        <input name="item_number" value="021" type="hidden">
                                                        <input name="amount" value="6.00" type="hidden">
                                                        <asp:ImageButton ID="btnSaturdayBrunch" runat="server" ImageUrl="https://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif" PostBackUrl="https://secure.paypal.com/cgi-bin/webscr" OnClick="btnSaturdayBrunch_Click" />
                                                    </div>
                                                </div>
                                                <div class="row PrePostPadding">&nbsp;</div>
                                                <div class="row PrePostPadding">
                                                    Saturday Dinner at 6PM ($8.00 per person):
                                                </div>
                                                <div class="row PrePostPadding">
                                                    <div role="form">
                                                        <input name="cmd" value="_xclick" type="hidden">
                                                        <input name="business" value="rciccolini@gmail.com" type="hidden">
                                                        <input name="return" value="http://www.larp.com/madrigal" type="hidden">
                                                        <input name="item_name" value="Saturday Dinner Meal Payment" type="hidden">
                                                        <input name="item_number" value="022" type="hidden">
                                                        <input name="amount" value="8.00" type="hidden">
                                                        <asp:ImageButton ID="btnSaturdayDinner" runat="server" ImageUrl="https://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif" PostBackUrl="https://secure.paypal.com/cgi-bin/webscr" OnClick="btnSaturdayDinner_Click" />
                                                    </div>
                                                </div>
                                                <div class="row PrePostPadding">&nbsp;</div>
                                                <div class="row PrePostPadding">
                                                    Sunday Brunch at 10AM ($6.00 per person):
                                                </div>
                                                <div class="row PrePostPadding">
                                                    <div role="form">
                                                        <input name="cmd" value="_xclick" type="hidden">
                                                        <input name="business" value="rciccolini@gmail.com" type="hidden">
                                                        <input name="return" value="http://www.larp.com/madrigal" type="hidden">
                                                        <input name="item_name" value="Sunday Brunch Meal Payment" type="hidden">
                                                        <input name="item_number" value="023" type="hidden">
                                                        <input name="amount" value="6.00" type="hidden">
                                                        <asp:ImageButton ID="btnSundayBrunch" runat="server" ImageUrl="https://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif" PostBackUrl="https://secure.paypal.com/cgi-bin/webscr" OnClick="btnSundayBrunch_Click" />
                                                    </div>
                                                </div>
                                                <div class="row PrePostPadding">&nbsp;</div>
                                                <div class="row PrePostPadding">
                                                    All three meals ($20.00 per person):
                                                </div>
                                                <div class="row PrePostPadding">
                                                    <div role="form">
                                                        <input name="cmd" value="_xclick" type="hidden">
                                                        <input name="business" value="rciccolini@gmail.com" type="hidden">
                                                        <input name="return" value="http://www.larp.com/madrigal" type="hidden">
                                                        <input name="item_name" value="Combined Three-Meal Payment" type="hidden">
                                                        <input name="item_number" value="025" type="hidden">
                                                        <input name="amount" value="20.00" type="hidden">
                                                        <asp:ImageButton ID="btnAllMeals" runat="server" ImageUrl="https://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif" PostBackUrl="https://secure.paypal.com/cgi-bin/webscr" OnClick="btnAllMeals_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row PrePostPadding">&nbsp;</div>
                        <div class="row PrePostPadding">
                            <div class="col-sm-10"></div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnClose" runat="server" CssClass="StandardButton" Text="Close" OnClick="btnClose_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
