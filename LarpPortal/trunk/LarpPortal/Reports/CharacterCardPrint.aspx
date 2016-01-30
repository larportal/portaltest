<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="CharacterCardPrint.aspx.cs" Inherits="LarpPortal.Reports.CharacterCardPrint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <aside></aside>
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-12">Character Cards</h3>
        </div>
        <asp:Panel ID="pnlParameters" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="col-sm-11">
                    <div class="col-sm-11 form-group">
                        <asp:RadioButton ID="rdoButtonCampaign" runat="server" AutoPostBack="true" GroupName="grpPrintBy" Text="Campaign" OnCheckedChanged="grpPrintBy_CheckedChanged" />
                        &nbsp;&nbsp;&nbsp
                        <asp:RadioButton ID="rdoButtonEvent" runat="server" AutoPostBack="true" GroupName="grpPrintBy" Text="Event" OnCheckedChanged="grpPrintBy_CheckedChanged" />
                    </div>
                    <div class="col-sm-12 form-group">

                    </div>
                    <div class="col-sm-12 form-group">
<%--                        <label for="ddlOrderBy" class="col-sm-5 control-label">Sort By</label>
                        <asp:DropDownList ID="ddlOrderBy" runat="server">
                            <asp:ListItem Value="1" Text="Event Date (ascending)"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Event Date (descending)"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Campaign, Event Date"></asp:ListItem>
                        </asp:DropDownList>--%>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="col-sm-12 form-group">
                        <label for="ddlEvent" runat="server" visible="false">Event</label>
                        <asp:DropDownList ID="ddlEvent" runat="server" Visible="false">
                            <asp:ListItem Value="0" Text="All events"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 form-group">

                    </div>
                </div>

                <div class="col-sm-1">
                    <div class="row">
                        <div style="padding-right: 20px; padding-top: 2px">
                            <asp:Button ID="btnRunReport" runat="server" CssClass="StandardButton" Visible="false"  Width="77px" Text="Run" OnClick="btnRunReport_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div style="padding-right: 20px; padding-top: 2px">
                            <asp:Button ID="btnExportExcel" runat="server" CssClass="StandardButton" Width="77px" Text="Excel" OnClick="btnExportExcel_Click" Visible="false" />
                        </div>
                    </div>
<%--                    <div class="row">
                        <div style="padding-right: 20px; padding-top: 2px">
                            <asp:Button ID="btnExportCSV" runat="server" CssClass="StandardButton" Width="77px"  Text="CSV" OnClick="btnExportCSV_Click" Visible="false" />
                        </div>
                    </div>--%>
                </div>
            </div>
        </asp:Panel>

<%--        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
                <asp:Panel ID="pnlReportOutput" runat="server" Visible="false">
                    <div class="form-horizontal col-sm-12">
                        <div class="row">
                            <div id="Div1" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Grid Header (e.g. Event Calendar)</h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvCalendar" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvCalendar_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed"
                                                    >
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
            </div>--%>

        </div>
</asp:Content>

