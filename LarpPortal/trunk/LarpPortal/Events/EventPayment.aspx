<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="EventPayment.aspx.cs" Inherits="LarpPortal.Events.EventPayment" %>

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
        <asp:HiddenField ID="hidItemName" runat="server" />
        <asp:Label ID="lblHeader" runat="server"></asp:Label>
        <div class="row PrePostPadding">
            <asp:Label ID="lblRegistrationText" runat="server" Text="RegistrationText"></asp:Label>
        </div>
        <div class="row PrePostPadding">
            <asp:CheckBox ID="chkRegistration" runat="server" Text="Add registration payment ($80.00)" />
        </div>
        <div class="row PrePostPadding">
            <asp:Label ID="lblFoodText" runat="server" Text="FoodText"></asp:Label>
        </div>
        <div class="row PrePostPadding">
            <asp:CheckBox ID="chkSaturdayBrunch" runat="server" Text="Saturday Brunch ($6.00)" />
        </div>
        <div class="row PrePostPadding">
            <asp:CheckBox ID="chkSaturdayDinner" runat="server" Text="Saturday Dinner ($8.00)" />
        </div>
        <div class="row PrePostPadding">
            <asp:CheckBox ID="chkSundayBrunch" runat="server" Text="Sunday Brunch ($6.00)" />
        </div>
        <div class="row PrePostPadding">
            <asp:CheckBox ID="chkAllMeals" runat="server" Text="All three meals ($20.00)" />
        </div>
        <div class="row PrePostPadding" runat="server">&nbsp;</div>
        <asp:Button ID="btnCalculateOrder" runat="server" CssClass="StandardButton" Text="Calculate Amount" OnClick="btnCalculateOrder_Click" />
        <div class="row PrePostPadding" runat="server">&nbsp;</div>
        <div class="row PrePostPadding" runat="server">
            <div>
                <asp:Label ID="lblOrderTotalDisplay" runat="server" Visible="false"></asp:Label><br />
            </div>
            <div>
                <asp:Label ID="lblOrderTotalSection" runat="server" Visible="false"></asp:Label>
            </div>
            <div>
                <asp:ImageButton ID="btnPayPalTotal" runat="server" ImageUrl="https://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif" PostBackUrl="https://secure.paypal.com/cgi-bin/webscr" Visible="false" OnClick="btnPayPalTotal_Click" />
            </div>
            <div>
                <asp:Label ID="lblClosePayPalForm" runat="server" Text="</form>"></asp:Label>
            </div>
        </div>

        <asp:Label ID="lblFooter" runat="server" Visible="false"></asp:Label>

        <div class="row PrePostPadding">&nbsp;</div>
        <div class="row PrePostPadding">
            <div class="col-sm-11"></div>
            <div class="col-sm-1">
                <asp:Button ID="btnClose" runat="server" CssClass="StandardButton" Text="Close" OnClick="btnClose_Click" />
            </div>
        </div>
    </div>
</asp:Content>

