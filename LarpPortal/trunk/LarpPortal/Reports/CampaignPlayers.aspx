<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="CampaignPlayers.aspx.cs" Inherits="LarpPortal.Reports.CampaignPlayers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <aside></aside>
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-12">Campaign Players List</h3>
        </div>
        <asp:Panel ID="pnlParameters" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="col-sm-5">
                    <div class="col-sm-12 form-group">
<%--                        <label for="ddlEventDateRange" class="col-sm-5 control-label">Date Range</label>
                        <asp:DropDownList ID="ddlEventDateRange" runat="server">
                            <asp:ListItem Value="1" Text="All scheduled"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Next 6 months"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Last 3 months"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Last 6 months"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Last 12 months"></asp:ListItem>
                            <asp:ListItem Value="6" Text="All historical"></asp:ListItem>
                        </asp:DropDownList>--%>
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
<%--                        <label for="ddlCampaignChoice" class="col-sm-5 control-label">Campaigns</label>
                        <asp:DropDownList ID="ddlCampaignChoice" runat="server" AutoPostBack="true">
                            <asp:ListItem Value="1" Text="Include all my campaigns"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Include only selected campaign"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Include campaigns in the selected game system"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Include all LARP Portal campaigns"></asp:ListItem>
                        </asp:DropDownList>--%>
                    </div>
                    <div class="col-sm-12 form-group">

                    </div>
                </div>

                <div class="col-sm-1">
                    <div class="row">
                        <div style="padding-right: 20px; padding-top: 2px">
                            <asp:Button ID="btnRunReport" runat="server" CssClass="StandardButton"  Width="77px" Text="Run Report" OnClick="btnRunReport_Click" />
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

        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
                <asp:Panel ID="pnlReportOutput" runat="server" Visible="false">
                    <div class="form-horizontal col-sm-12">
                        <div class="row">
                            <div id="Div1" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Campaign Players</h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvCampaignPlayers" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvCampaignPlayers_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed"
                                                    >
                                                    <Columns>
                                                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                                        <asp:BoundField DataField="Nickname" HeaderText="Nickname" />
                                                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                                        <asp:TemplateField HeaderText="Email (click to send)">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hplEmail" runat="server" Font-Underline="true" Text='<%# Eval("EmailAddress") %>' NavigateUrl='<%# "mailto:" + Eval("EmailAddress") %>' ></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="EmailAddress" HeaderText="Email Address" />--%>
                                                        <asp:BoundField DataField="PC" HeaderText="PC" />
                                                        <asp:BoundField DataField="NPC" HeaderText="NPC" />
                                                        <asp:BoundField DataField="Staff" HeaderText="Staff" />
                                                        <asp:BoundField DataField="LastEventRegDate" HeaderText="Last Registered Event Date" />
                                                        <asp:BoundField DataField="Role" HeaderText="Last Role" />
                                                        <asp:BoundField DataField="CharacterPlayed" HeaderText="Character Played" />
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
