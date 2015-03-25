<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharPlaces.aspx.cs" Inherits="LarpPortal.Character.CharPlaces" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="character-history tab-pane">
        <section role="form" class="form-horizontal form-condensed">
            <div class="row">
                <h1 class="col-xs-4" style="padding-top: 0px;">Character Places</h1>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h2 class="col-xs-2">Places</h2>
                                        </td>
                                        <td style="width: 60%; text-align: right;">
                                            <label class="panel-label">Places: &nbsp;&nbsp;</label>
                                            <asp:DropDownList ID="ddlPlacesFilter" runat="server" BackColor="White" ForeColor="Black" />&nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <!-- form-group -->
                            </div>
                            <!-- .panel-header -->
                            <div class="panel-body">
                                <div class="panel-container-table" style="height: 200px; overflow-y: auto">
                                    <asp:GridView ID="gvPlaces" AutoGenerateColumns="false" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed" 
                                        DataKeyNames="CampaignPlaceID"
                                        OnRowCancelingEdit="gvPlaces_RowCancelingEdit" OnRowUpdating="gvPlaces_RowUpdating" OnRowDataBound="gvPlaces_RowDataBound"
                                        OnRowEditing="gvPlaces_RowEditing" OnRowDeleting="gvPlaces_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("PlaceName") %>' runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlPlaces" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Locale">



                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CampaignPlaceID" HeaderText="CampaignPlaceID" />
                                            <asp:BoundField DataField="RulebookDescription" HeaderText="Rulebook Description" />
<%--                                            <asp:TemplateField HeaderText="Listed">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListedInRuleBook" Text='<%# Eval("ListedInRuleBook") %>' runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="cbListedInRuleBook" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="40" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit" ImageUrl="~/img/Edit.gif" CommandArgument='<%# Eval("CampaignPlaceID") %>' Width="16px" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete" ImageUrl="~/img/trash-button.jpg" CommandArgument='<%# Eval("CampaignPlaceID") %>' Width="16px" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="btnUpdate" runat="server" CausesValidation="false" CommandName="Update" ImageUrl="~/img/update_icon.png" CommandArgument='<%# Eval("CampaignPlaceID") %>' Width="16px" />
                                                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" ImageUrl="~/img/delete.png" CommandArgument='<%# Eval("CampaignPlaceID") %>' Width="16px" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!-- .panel-container -->
                            </div>
                        </div>
                    </div>
                    <!-- panel-wrapper -->
                </div>
                <!-- col-xs-6 -->
            </div>
            <!-- row -->
            <br />
            <span class="col-xs-11 panel-wrapper text-right">&nbsp;
            </span>
            <span class="col-xs-1" style="float: right;">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default input-group button form-control" Style="height: 30px;" Text="Save" OnClick="btnSave_Click" />
            </span>
        </section>
    </div>
    <br />
    <div align="left">
    <asp:TreeView ID="tvPlaces" runat="server" ShowCheckBoxes="all" />
        </div>
</asp:Content>
