<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="SiteList.aspx.cs" Inherits="LarpPortal.Reports.SiteList1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <aside></aside>
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-5">Site List</h3>
        </div>
        <asp:Panel ID="pnlParameters" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="col-sm-5">
                    <div class="col-sm-12 form-group">
                        <label for="ddlCountryChoice" class="col-sm-6 control-label">Country</label>
                        <asp:DropDownList ID="ddlCountry" runat="server">

                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 form-group">

                    </div>
                    <div class="col-sm-12 form-group">
<%--                        <label for="ddlState" class="col-sm-5 control-label">State</label>
                        <asp:DropDownList ID="ddlState" runat="server">

                        </asp:DropDownList>--%>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="col-sm-12 form-group">
                        <label for="ddlState" class="col-sm-5 control-label">State</label>
                        <asp:DropDownList ID="ddlState" runat="server">

                        </asp:DropDownList>
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
                                        <h2>LARP Portal Site List</h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvSites" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvSites_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed"
                                                    >
                                                    <Columns>
                                                        <asp:BoundField DataField="Country" HeaderText="Country" />
                                                        <asp:BoundField DataField="State" HeaderText="State" />
                                                        <asp:BoundField DataField="City" HeaderText="City" />
                                                        <asp:BoundField DataField="SiteName" HeaderText="Site" />
                                                        <asp:BoundField DataField="Address1" HeaderText="Address" />
                                                        <asp:BoundField DataField="PostalCode" HeaderText="Zip" />
                                                        <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                                                        <asp:BoundField DataField="Extension" HeaderText="Ext" />
                                                        <asp:HyperLinkField HeaderText="Web Site" DataTextField="URL" DataNavigateUrlFields="URL" 
                                                            DataNavigateUrlFormatString="http://{0}" Target="_blank" />
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
