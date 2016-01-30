<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="PELSearch.aspx.cs" Inherits="LarpPortal.Reports.PELSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <aside></aside>
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-12">PEL Keyword Search</h3>
        </div>
        <asp:Panel ID="pnlParameters" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="col-sm-5">
                    <div class="col-sm-12 form-group">

                    </div>


                    <div class="col-sm-12 form-group">
                        <div class="TableLabel col-sm-3">Keyword: </div>
                        <div class="col-sm-9 NoPadding" style="background-color:white">
                            <asp:TextBox ID="txtKeyword" runat="server" Width="75%"></asp:TextBox>
                        </div>                        
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
                                        <h2>PEL Keyword Search Results</h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvPELSearch" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvPELSearch_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed"
                                                    >
                                                    <Columns>
                                                        <asp:BoundField DataField="PlayerType" HeaderText="Type" />
                                                        <asp:BoundField DataField="characteraka" HeaderText="Character" />
                                                        <asp:BoundField DataField="Player" HeaderText="Player" />
                                                        <asp:BoundField DataField="EventDate" HeaderText="Event Date" />
                                                        <asp:BoundField DataField="eventname" HeaderText="Event Description" />
                                                        <asp:BoundField DataField="PELString" HeaderText="PEL String" />
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

