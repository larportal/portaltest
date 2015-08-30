<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="EventRegistration.aspx.cs" Inherits="LarpPortal.Events.EventRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MemberCampaignsContent" runat="server">


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

        .NoWrap
        {
            text-wrap: none;
            white-space: nowrap;
        }

        div
        {
            border: 1px solid black;
        }

        input[type="radio"]
        {
            height: 20px;
            width: 20px;
        }
    </style>

    <%--    <asp:UpdatePanel ID="upMainUpdate" runat="server">
        <ContentTemplate>--%>
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Event RSVP and Registration" />
        </div>
        <div class="row col-lg-12" style="padding-left: 15px; padding-bottom: 10px;">
            <div class="col-lg-6">
                <%--            <table class="col-lg-12">
                <tr style="vertical-align: top;">--%>
                <div class="col-lg-12">
                    <asp:Panel ID="pnlEvents" runat="server" ScrollBars="Vertical" Height="200px">
                        <asp:GridView ID="gvEvents" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowCommand="gvEvents_RowCommand"
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" CssClass="table table-striped table-hover table-condensed">
                            <EmptyDataRowStyle BackColor="Transparent" />
                            <EmptyDataTemplate>
                                <div class="row">
                                    <div class="text-center" style="background-color: transparent;">You have no events to selected. Please select from the boxes below.</div>
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelect" runat="server" CommandArgument='<%# Eval("EventID") %>' Text="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EventID" />
                                <asp:BoundField DataField="StartDate" HeaderText="Event Date" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField DataField="EventName" HeaderText="Event Name" />
                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                                <asp:BoundField DataField="SiteLocation" HeaderText="Location" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

                    <br />
                    <br />

                    <div class="panel" id="pnlEventDetails" runat="server" style="display: none;">
                        <div class="panelheader">
                            <h2>Event Details</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria">
                                    <div class="row">
                                        <div class="TableLabel col-lg-3">Event Name:</div>
                                        <div class="col-lg-9">
                                            <asp:Label ID="lblEventName" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="TableLabel col-lg-3">Description:</div>
                                        <div class="col-lg-9">
                                            <asp:Label ID="lblDesc" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="TableLabel NoWrap col-lg-3">
                                            Start Date/Time:
                                        </div>
                                        <div class="NoWrap col-lg-3">
                                            <asp:Label ID="lblStartDate" runat="server" />
                                        </div>
                                        <div class="TableLabel NoWrap col-lg-3">
                                            End Date/Time:
                                        </div>
                                        <div class="NoWrap col-lg-3">
                                            <asp:Label ID="lblEndDate" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="TableLabel NoWrap col-lg-3">
                                            Site: 
                                        </div>
                                        <div class="col-lg-9">
                                            <asp:Label ID="lblSiteName" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="TableLabel NoWrap col-lg-3">Open Registration: </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblOpenRegDate" runat="server" />
                                        </div>
                                        <div class="TableLabel NoWrap col-lg-3">
                                            Pre Reg Price: 
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblPreRegPrice" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="TableLabel col-lg-3">
                                            Pre Reg Date: 
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblPreRegDate" runat="server" />
                                        </div>
                                        <div class="TableLabel NoWrap col-lg-3">
                                            Reg Price: 
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblRegPrice" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="TableLabel NoWrap col-lg-3">
                                            Info Skill Due: 
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblInfoSkillDue" runat="server" />
                                        </div>
                                        <div class="TableLabel NoWrap col-lg-3">
                                            At Door Price: 
                                        </div>
                                        <div class="NoWrap col-lg-3">
                                            <asp:Label ID="lblAtDoorPrice" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="TableLabel NoWrap col-lg-3">
                                            PEL Due: 
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblPELDue" runat="server" />
                                        </div>
                                        <div class="TableLabel NoWrap col-lg-3">
                                            PC Meal Combo: 
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblPCMealCombo" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="divTableLabel NoWrap col-lg-6">
                                            &nbsp;
                                        </div>
                                        <div class="TableLabel NoWrap col-lg-3">NPC Meal Combo: </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblNPCMealCombo" runat="server" />
                                        </div>
                                    </div>
                                    <%--                <tr>
                    <td class="TableLabel NoWrap">Available: </td>
                    <td colspan="3">
                        <asp:Image ID="imgPCFoodService" runat="server" ImageUrl="~/img/delete.png" />PC Food Service</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="3">
                        <asp:Image ID="imgNPCFoodService" runat="server" ImageUrl="~/img/delete.png" />NPC Food Service</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="3">
                        <asp:Image ID="imgCookingAllowed" runat="server" ImageUrl="~/img/delete.png" />Cooking Allowed</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="3">
                        <asp:Image ID="imgRefrigerator" runat="server" ImageUrl="~/img/delete.png" />Regridgerator</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="3">
                        <asp:Image ID="imgMenu" runat="server" ImageUrl="~/img/delete.png" />Menu Prices&nbsp;&nbsp<asp:HyperLink ID="hpEventMeal" runat="server" Text="Menu" Style="text-decoration-line: underline"
                            NavigateUrl="EventMenu.aspx" />
                    </td>
                </tr>

            </table>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                Please RSVP for each Scheduled Event for plot planning.<br />
                An RSVP can be given before Registration Opens/ Players must still register.<br />

                <div style="padding-left: 50px">
                    <div class="input-group col-lg-4">
                        <div class="input-group-addon">
                            <label class="radio">
                                <input type="radio" value="B" name="RSVP" />Plan to attend this event</label>
                            <label class="radio">
                                <input type="radio" value="A" name="RSVP" />I am unable to attend this event</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row col-lg-6">
                <div class="col-lg-6">
                    My Registration Status:
                    <asp:Label ID="lblRegStatus" runat="server" Text="Don't know." />
                </div>
                <div class="col-lg-6">

                    <div class="panel-container search-criteria">
                        <div class="row">
                            <div class="TableLabel col-lg-7">RSVP: </div>
                            <div class="col-lg-5">
                                5/6/2016
                                            <asp:Label ID="Label1" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="TableLabel col-lg-7">Registration Date:</div>
                            <div class="col-lg-5">
                                <asp:Label ID="Label2" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="TableLabel NoWrap col-lg-7">
                                Payment Date: 
                            </div>
                            <div class="NoWrap col-lg-5">
                                <asp:Label ID="Label3" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="TableLabel NoWrap col-lg-7">
                                Event Payment Amount:
                            </div>
                            <div class="NoWrap col-lg-5">
                                $0.00
                            </div>
                        </div>
                        <div class="row">
                            <div class="TableLabel NoWrap col-lg-7">
                                Food Payment Amount:
                            </div>
                            <div class="NoWrap col-lg-5">
                                $0.00
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="row col-lg-12 text-right">
            <asp:Button ID="btnSave" runat="server" CssClass="StandardButton col-lg-1" Width="150px" Text="Save Registration" />
        </div>
        <asp:Label ID="lblMessage" runat="server" />
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
