<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="ReportsList.aspx.cs" Inherits="LarpPortal.Reports.ReportsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <div class="contentArea col-sm-12">
        <div role="form" class="form-horizontal col-sm-12" style="padding-left: 30px; width: 800px;">
            <div class="row" style="column-span:all; padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="" />
            </div>
            <div class="panel" style="column-span:all; ">
                <div class="panel-header">
                    <h2>Reports<asp:Label ID="lblGridHeader" runat="server"></asp:Label></h2>
                </div>
                <div class="panel-body search-criteria" style="padding-bottom: 5px;">
                    <div style="max-height: 500px; overflow-y: auto; ">
                        <asp:GridView ID="gvReportsList" runat="server" AutoGenerateColumns="false" GridLines="None"
                            CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" BorderWidth="1">
                            <Columns>
                                <asp:BoundField DataField="Category" HeaderText="Category" HeaderStyle-Font-Underline="true" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                <asp:HyperLinkField DataTextField="ReportName" HeaderText=" Report Name" HeaderStyle-Font-Underline="true" DataNavigateUrlFields="PageNameURL"  ControlStyle-ForeColor="DarkBlue" ControlStyle-Font-Underline="true"
                                    
                                   ControlStyle-Font-Size="Small" ItemStyle-Font-Underline="true" />
                                <asp:BoundField DataField="ReportDescription" HeaderText="Description" HeaderStyle-Font-Underline="true" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
