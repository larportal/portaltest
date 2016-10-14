<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="HistorySearch.aspx.cs" Inherits="LarpPortal.Reports.HistorySearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <aside></aside>
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-12">Character History Keyword Search</h3>
        </div>
        <asp:Panel ID="pnlParameters" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="col-sm-7">
                    <div class="col-sm-12 form-group">
                        <div class="TableLabel col-sm-5">Search: </div>
                        <div class="col-sm-7 NoPadding" style="background-color:white">
                            <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                        </div>                        
                    </div>
                    <div class="col-sm-12 form-group">

                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="col-sm-12 form-group">

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
                                        <h2>History Keyword Search Results</h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvHistorySearch" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvHistorySearch_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed"
                                                    >
                                                    <Columns>
                                                        <asp:BoundField DataField="CharacterAKA" HeaderText="Character" />
                                                        <asp:BoundField DataField="Player" HeaderText="Player" />
                                                        <asp:BoundField DataField="HistoryString" HeaderText="History String" />
                                                        <asp:HyperLinkField DataNavigateUrlFields="Redirecter" Text="View complete History" Target="_blank" 
                                                            ControlStyle-Font-Size="X-Small" ItemStyle-Wrap="false" 
                                                            ControlStyle-ForeColor="DarkBlue" ControlStyle-Font-Underline="true" />
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

