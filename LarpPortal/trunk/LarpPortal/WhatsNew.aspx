<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.Master" AutoEventWireup="true" CodeBehind="WhatsNew.aspx.cs" Inherits="LarpPortal.WhatsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">
    <div class="contentArea">
        <aside>
        </aside>
        <div class="mainContent tab-content">
            <section id="whats-new">
                <div class="row">
                    <div class="panel-wrapper col-md-10">
                        <div class="panel">
                            <div class="panel-header">
                                <h2>What's New?</h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container">
                                    <asp:Label ID="lblWhatsNew" runat="server"></asp:Label>
                                </div>
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <div style="max-height: 500px; overflow-y: auto; margin-right: 10px;">
                                        <asp:GridView ID="gvWhatsNewList" runat="server" AutoGenerateColumns="false" OnRowCommand="gvWhatsNewList_RowCommand" GridLines="None"
                                            CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" BorderWidth="1">
                                            <Columns>
                                                <asp:BoundField DataField="ReleaseDate" HeaderText="Date" DataFormatString="{0: MM/dd/yyyy}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="ModuleName" HeaderText="Module" ItemStyle-Wrap="false"
                                                    HeaderStyle-Wrap="false" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="BriefName" HeaderText="Name" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="ShortDescription" HeaderText="Description" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:HyperLinkField DataNavigateUrlFields="Redirecter" Text="See more" Target="_blank" ControlStyle-Font-Size="X-Small"
                                                    ItemStyle-Wrap="false" ControlStyle-ForeColor="DarkBlue" ControlStyle-Font-Underline="true" />
                                                <%--                                        <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnCommand" runat="server" CommandArgument='<%# Eval("Redirecter") %>' CommandName='<%# Eval("ButtonText") %>Item' 
                                                                    Style="padding-left: 10px; padding-right: 10px;" Text='<%# Eval("ButtonText") %>' CssClass="StandardButton" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
