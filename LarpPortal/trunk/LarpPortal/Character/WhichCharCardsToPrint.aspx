<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="WhichCharCardsToPrint.aspx.cs" Inherits="LarpPortal.Character.WhichCharCardsToPrint" %>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="CharHeaderStyles">
    <style type="text/css">
        .TableTextBox
        {
            border: 1px solid black;
        }

        th, tr:nth-child(even) > td
        {
            background-color: #ffffff;
        }

        .CharInfoTable
        {
            border-collapse: collapse;
        }

            .CharInfoTable td
            {
                padding: 4px;
            }
    </style>
</asp:Content>

<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="CharHeaderScripts">
    <script>
        function HideTextBox(ddlId) {
            var ControlName = document.getElementById(ddlId.id);

            var EventList = document.getElementById("divEvent");
            var CharList = document.getElementById("divChar");
            var PrintButton = document.getElementById("<%= btnPrint.ClientID %>");

            if (ControlName.value == "EVENT") {
                EventList.style.display = '';
                CharList.style.display = 'none';
                PrintButton.style.display = '';
            }
            else if (ControlName.value == "CHARACTER") {
                EventList.style.display = 'none';
                CharList.style.display = '';
                PrintButton.style.display = ''
            }
            else if (ControlName.value == "CAMPAIGN") {
                EventList.style.display = 'none';
                CharList.style.display = 'none';
                PrintButton.style.display = '';
            }
            else {
                PrintButton.style.display = 'none';
            }

            return false;
        }


        function PrintCards() {
            var WhichOnes = document.getElementById("<%= ddlCharacterSelector.ClientID %>");
            var EventID = document.getElementById("<%= ddlEvents.ClientID %>");
            var CharList = document.getElementById("<%= ddlCharacters.ClientID %>");

            if (WhichOnes.value == "EVENT") {
                window.open('MultipleCharCards.aspx?EventID='+EventID.value, '_blank', 'toolbar=0,location=0,menubar=0');
            }
            else if (WhichOnes.value == "CHARACTER") {
                window.open('MultipleCharCards.aspx?CharacterID='+CharList.value, '_blank', 'toolbar=0,location=0,menubar=0');
            }
            else {
                var CampaignID = document.getElementById("<%= hidCampaignID.ClientID %>");
                window.open('MultipleCharCards.aspx?CampaignID='+CampaignID.value, '_blank', 'toolbar=0,location=0,menubar=0');
            }
            return false;
        }
    </script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upPanel" runat="server">
        <ContentTemplate>
            <div class="mainContent tab-content col-sm-12">
                <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Printing Character Cards" />
                </div>
                <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
                    <div class="col-lg-3 text-right" style="padding-right: 10px;">
                        <b>Select which characters should be printed:</b>
                    </div>
                    <div class="col-lg-3">
                        <asp:DropDownList ID="ddlCharacterSelector" runat="server">
                            <asp:ListItem Text="Select ..." Value="" Selected="true" />
                            <asp:ListItem Text="For an entire campaign" Value="CAMPAIGN" />
                            <asp:ListItem Text="For an event" Value="EVENT" />
                            <asp:ListItem Text="A single character" Value="CHARACTER" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row" id="divEvent" style="padding-left: 15px; padding-bottom: 10px; display: none;">
                    <div class="col-lg-3 text-right" style="padding-right: 10px;">
                        <b>Select which event should be printed:</b>
                    </div>
                    <div class="col-lg-3">
                        <asp:DropDownList ID="ddlEvents" runat="server" />
                    </div>
                </div>

                <div class="row" id="divChar" style="padding-left: 15px; padding-bottom: 10px; display: none;">
                    <div class="col-lg-3 text-right" style="padding-right: 10px;">
                        <b>Select which character should be printed:</b>
                    </div>
                    <div class="col-lg-3">
                        <asp:DropDownList ID="ddlCharacters" runat="server" />
                    </div>
                </div>

                <div class="row" style="padding-left: 15px; text-align: right; padding-top: 25px;">
                    <div class="col-lg-6 text-right">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="StandardButton" Width="100px" OnClientClick="PrintCards(); return false;" style="display: none;"/>
                        </div>
                </div>
            </div>
            <asp:Label ID="lblMessage" runat="server" />
            <asp:HiddenField ID="hidCampaignID" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
