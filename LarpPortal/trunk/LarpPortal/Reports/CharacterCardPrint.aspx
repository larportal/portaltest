<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="CharacterCardPrint.aspx.cs" Inherits="LarpPortal.Reports.CharacterCardPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <aside></aside>
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-12">Character Cards</h3>
        </div>
        <asp:Panel ID="pnlParameters" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="row">
                    <asp:RadioButton ID="rdoButtonCampaign" runat="server" AutoPostBack="true" GroupName="grpPrintBy" Text=" Campaign" OnCheckedChanged="grpPrintBy_CheckedChanged" />
                </div>
                <div class="row">
                    <asp:RadioButton ID="rdoButtonEvent" runat="server" AutoPostBack="true" GroupName="grpPrintBy" Text=" Event" OnCheckedChanged="grpPrintBy_CheckedChanged" />
                    <label for="ddlEvent" runat="server" visible="false">Event</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlEvent" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="All events"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="row">
                    <asp:RadioButton ID="rdoButtonCharacter" runat="server" AutoPostBack="true" GroupName="grpPrintBy" Text="Character" OnCheckedChanged="grpPrintBy_CheckedChanged" />
                    <label for="ddlCharacter" runat="server" visible="false">Character</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlCharacter" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacter_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Select character"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="row">
                    <div style="padding-right: 20px; padding-top: 2px">
                        <asp:Button ID="btnRunReport" runat="server" CssClass="StandardButton" Visible="false" Width="77px" Text="Run" OnClick="btnRunReport_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

