<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="CheckIn.aspx.cs" Inherits="LarpPortal.Reports.CheckIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <aside></aside>
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-12">Check-in Report</h3>
        </div>
        <asp:Panel ID="pnlParameters" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="col-sm-6">
                    <div class="col-sm-12 form-group">
                        <asp:Label ID="lblEvent" runat="server" AssociatedControlID="ddlEvent" Text="Event"></asp:Label>
                        <asp:DropDownList ID="ddlEvent" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged" Visible="true">
                            <asp:ListItem Value="0" Text="All events"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 form-group">

                    </div>

                </div>
                <div class="col-sm-5">

                </div>

                <div class="col-sm-1">
                    <div class="row">
                        <div style="padding-right: 20px; padding-top: 2px">
                            <asp:Button ID="btnRunReport" runat="server" CssClass="StandardButton"  Width="77px" Text="Run Report" Visible="false" OnClick="btnRunReport_Click" />
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
                                        <h2>Check-in </h2><asp:Label ID="lblEventName" runat="server" Text=""></asp:Label>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvCheckList" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvCheckList_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed"
                                                    >
                                                    <Columns>
                                                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                                        <asp:BoundField DataField="CharacterAKA" HeaderText="Character" />
                                                        <asp:BoundField DataField="TeamName" HeaderText="Team" />
                                                        <asp:BoundField DataField="Housing" HeaderText="Housing" />
                                                        <asp:BoundField DataField="Role" HeaderText="Role" />
                                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                                        <asp:BoundField DataField="Partial" HeaderText="Partial Event" />
                                                        <asp:BoundField DataField="ArrivedAt" HeaderText="Arrival Date/Time" />
                                                        <asp:BoundField DataField="LeftAt" HeaderText="Left At" />
                                                        <asp:BoundField DataField="SetupCleanup" HeaderText="Setup/ Cleanup" />
                                                        <asp:BoundField DataField="NPCCPAssignment" HeaderText="NPC CP To" />
                                                        <asp:BoundField DataField="EventPayment" HeaderText="Event Payment" />
                                                        <asp:BoundField DataField="EventPaymentDate" HeaderText="Pay Date" />
                                                        <asp:BoundField DataField="EventPaymentAmount" HeaderText="Paid Amt" />
                                                        <asp:BoundField DataField="Comments" HeaderText="Comments" />
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