<%@ Page Title="" Language="C#" MasterPageFile="~/Calendar/Calendars.master" AutoEventWireup="true" CodeBehind="CalendarRpt.aspx.cs" Inherits="LarpPortal.CalendarRpt.CalendarRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <div role="form" class="form-horizontal form-condensed">
        <div class="row col-sm-12">
            <h3 class="col-xs-12">Events Calendar Report </h3>
        </div>
        <div class="form-inline col-xs-12">
            <div class="form-group">
                <label for="ddlEventDateRange">Date Range</label>
                <asp:DropDownList ID="ddlEventDateRange" runat="server">
                    <asp:ListItem Value="1" Text="All scheduled"></asp:ListItem>
                    <%--                            <asp:ListItem Value="2" Text="Next 6 months"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Last 3 months"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Last 6 months"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Last 12 months"></asp:ListItem>--%>
                    <asp:ListItem Value="6" Text="All historical"></asp:ListItem>
                </asp:DropDownList>

                <label for="ddlOrderBy" style="padding-left: 25px;">Sort By</label>
                <asp:DropDownList ID="ddlOrderBy" runat="server" style="margin-right: 50px;">
                    <asp:ListItem Value="StartDate" Text="Event Date (ascending)"></asp:ListItem>
                    <%--<asp:ListItem Value="2" Text="Event Date (descending)"></asp:ListItem>--%>
                    <asp:ListItem Value="CampaignName, StartDate" Text="Campaign, Event Date"></asp:ListItem>
                </asp:DropDownList>

            <asp:Button ID="btnExportExcel" runat="server" CssClass="StandardButton" Width="125px" Text="Export To Excel" OnClick="btnExportExcel_Click" Visible="false" />
            <asp:Button ID="btnRunReport" runat="server" CssClass="StandardButton" Width="125px" Text="Run Report" OnClick="btnRunReport_Click" />
            </div>

        </div>

        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
                <asp:Panel ID="pnlReportOutput" runat="server" Visible="false">
                    <div class="form-horizontal col-sm-12">
                        <div class="row">
                            <div id="Div1" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Event Calendar</h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvCalendar" runat="server"
                                                    AutoGenerateColumns="false"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed">
                                                    <Columns>
                                                        <asp:BoundField DataField="CampaignName" HeaderText="Campaign" />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date" />
                                                        <asp:BoundField DataField="EventName" HeaderText="Event Description" />
                                                        <asp:BoundField DataField="SiteName" HeaderText="Location" />
                                                        <asp:BoundField DataField="City" HeaderText="City" />
                                                        <asp:BoundField DataField="StateID" HeaderText="State" />
                                                        <asp:BoundField DataField="RoleDescription" HeaderText="Role" />
                                                        <asp:BoundField DataField="RegistrationStatusName" HeaderText="Status" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </section>
        </div>
    </div>
</asp:Content>
