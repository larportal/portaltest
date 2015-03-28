<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharHistory.aspx.cs" Inherits="LarpPortal.Character.CharHistory" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="character-history tab-pane">
        <section role="form" class="form-horizontal form-condensed">
            <div class="row">
                <h1 class="col-xs-10" style="padding-top: 0px;"><asp:Label ID="lblHeader" runat="server" /></h1>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label class="col-xs-4 control-label">Character Name</label>
                        <div class="col-xs-8">
                            <input type="text" name="character-history-character-name" id="character-history-character-name" value="" placeholder="Character Name" class="form-control">
                        </div>
                    </div>
                    <!-- form-group -->
                </div>
                <!-- col-xs-6 -->
            </div>
<%--            <div class="row">
                <div class="col-xs-6">
                    <div class="panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h2 class="col-xs-2">People</h2>
                                        </td>
                                        <td style="width: 60%; text-align: right;">
                                            <label class="panel-label">Search Characters: &nbsp;&nbsp;</label>
                                            <asp:DropDownList ID="ddlCharacterFilter" runat="server" BackColor="White" ForeColor="Black" />&nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <!-- form-group -->
                            </div>
                            <!-- .panel-header -->
                            <div class="panel-body">
                                <div class="panel-container-table" style="height: 200px; overflow-y: auto">
                                    <asp:GridView ID="gvRelationships" AutoGenerateColumns="false" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed"
                                        OnRowEditing="gvRelationships_RowEditing" OnRowDeleting="gvRelationships_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="RelationDescription" HeaderText="Description" />
                                            <asp:BoundField DataField="NamedInRules" HeaderText="Named In Rules" />
                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="40" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit" ImageUrl="~/img/Edit.gif" CommandArgument='<%# Eval("id") %>' Width="16px" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete" ImageUrl="~/img/delete.png" CommandArgument='<%# Eval("id") %>' Width="16px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!-- .panel-container -->
                            </div>
                        </div>
                    </div>
                </div>
                <!-- col-xs-6 -->
                <div class="col-xs-6">
                    <div class="panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h2 class="col-xs-2">Places</h2>
                                        </td>
                                        <td style="width: 60%; text-align: right;">
                                            <label class="panel-label">Search Places: &nbsp;&nbsp;</label>
                                            <asp:DropDownList ID="ddlPlacesFilter" runat="server" BackColor="White" ForeColor="Black" />&nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <!-- form-group -->
                            </div>
                            <!-- .panel-header -->
                            <div class="panel-body">
                                <div class="panel-container-table" style="height: 200px; overflow-y: auto">
                                    <asp:GridView ID="gvPlaces" AutoGenerateColumns="false" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed" DataKeyNames="ID"
                                        OnRowCancelingEdit="gvPlaces_RowCancelingEdit" OnRowUpdating="gvPlaces_RowUpdating" OnRowDataBound="gvPlaces_RowDataBound"
                                        OnRowEditing="gvPlaces_RowEditing" OnRowDeleting="gvPlaces_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Name") %>' runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlPlaces" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="PlaceName" HeaderText="Place Name" />
                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="40" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit" ImageUrl="~/img/Edit.gif" CommandArgument='<%# Eval("id") %>' Width="16px" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete" ImageUrl="~/img/trash-button.jpg" CommandArgument='<%# Eval("id") %>' Width="16px" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="btnUpdate" runat="server" CausesValidation="false" CommandName="Update" ImageUrl="~/img/update_icon.png" CommandArgument='<%# Eval("id") %>' Width="16px" />
                                                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" ImageUrl="~/img/delete.png" CommandArgument='<%# Eval("id") %>' Width="16px" />
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


--%>







            <!-- row -->
            <div class="row">
                <div class="panel-wrapper col-xs-12">
                    <div class="panel">
                        <div class="panel-header">
                            <h2>Character History</h2>
                        </div>
                        <textarea name="taHistory" id="taHistory" rows="4" class="col-xs-12" runat="server" placeholder="Type your characters history here" />
                    </div>
                </div>
                <div class="col-xs-12 panel-wrapper">
                    <div class="panel">
                        <div class="panel-header">
                            <h2>Comments</h2>
                        </div>
                        <textarea name="taComments" id="taComments" rows="2" class="col-xs-12" runat="server" />
                    </div>
                </div>
            </div>
            <span class="col-xs-11 panel-wrapper text-right">&nbsp;
            </span>
            <span class="col-xs-1" style="float: right;">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default input-group button form-control" Style="height: 30px;" Text="Save" OnClick="btnSave_Click" />
            </span>
        </section>
    </div>
    <br />

</asp:Content>
