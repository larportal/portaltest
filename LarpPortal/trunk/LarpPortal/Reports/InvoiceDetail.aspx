<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="InvoiceDetail.aspx.cs" Inherits="LarpPortal.Reports.InvoiceDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <aside></aside>
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-12">Invoice Detail</h3>
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
                    <div class="col-sm-12 form-group">
                        <asp:Label ID="lblQuestion" runat="server" AssociatedControlID="ddlQuestion" Visible="false" Text="Question"></asp:Label>
                        <asp:DropDownList ID="ddlQuestion" runat="server" Visible="false">
                            <asp:ListItem Value="0" Text="First Question"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-5">
                    <div class="col-sm-12 form-group">
                        <asp:Label ID="lblRole" runat="server" AssociatedControlID="ddlRole" Visible="false" Text="Player Type"></asp:Label>
                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" Visible="false">
                            <asp:ListItem Value="0" Text="PC"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 form-group">

                    </div>
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
                                        <h2>Summary PEL Answers</h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvAnswers" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvAnswers_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed"
                                                    >
                                                    <Columns>
                                                        <asp:BoundField DataField="Question" HeaderText="Question" />
                                                        <asp:BoundField DataField="Answer" HeaderText="Answer" />
                                                        <asp:BoundField DataField="CharacterAKA" HeaderText="Character" />
                                                        <asp:BoundField DataField="Player" HeaderText="Player" />
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

