<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharItems.aspx.cs" Inherits="LarpPortal.Character.CharItems" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active">
        <section role="form" class="form-horizontal form-condensed">
            <div class="row">
                <h1 class="col-xs-10" style="padding-top: 0px;"><asp:Label ID="lblHeader" runat="server" /></h1>
            </div>
            <div class="form-horizontal">
                <div class="col-sm-8">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="panel-wrapper">
                                <div class="panel">
                                    <div class="panel-header">
                                        <h2>Costume</h2>
                                    </div>
                                    <div class="panel-body">
                                        <div class="panel-container scroll">
                                            <textarea name="taCostume" id="taCostume" runat="server" rows="3" class="col-xs-12"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="panel-wrapper">
                                <div class="panel">
                                    <div class="panel-header">
                                        <h2>Make-up</h2>
                                    </div>
                                    <div class="panel-body">
                                        <div class="panel-container scroll">
                                            <textarea name="taMakeup" id="taMakeup" runat="server" rows="3" class="col-xs-12"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="panel-wrapper">
                                <div class="panel">
                                    <div class="panel-header">
                                        <h2>Accessories</h2>
                                    </div>
                                    <div class="panel-body">
                                        <div class="panel-container scroll">
                                            <textarea name="taAccessories" id="taAccessories" runat="server" rows="3" class="col-xs-12"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="panel-wrapper">
                                <div class="panel">
                                    <div class="panel-header">
                                        <h2>Weapons and Armor</h2>
                                    </div>
                                    <div class="panel-body">
                                        <div class="panel-container scroll">
                                            <textarea name="taWeapons" id="taWeapons" runat="server" rows="3" class="col-xs-12"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="panel-wrapper">
                                <div class="panel">
                                    <div class="panel-header">
                                        <h2>Other Items</h2>
                                    </div>
                                    <div class="panel-body">
                                        <div class="panel-container scroll">
                                            <textarea name="taItemsOther" id="taItemsOther" runat="server" rows="3" class="col-xs-12"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="row">
                        <div class="panel-wrapper">
                            <div class="uploadFile col-xs-12">
                                <div class="row">
                                    <span class="input-group-btn">
                                        <asp:FileUpload ID="fuItem" runat="server" CssClass="btn btn-default btn-sm btnFile col-sm-6" ToolTip="Here's where you specify the file name." />
                                        <span class="col-sm-1">&nbsp;</span>
                                        <asp:Button ID="btnUpload" runat="server" CssClass="StandardButton col-sm-2" Text="Upload File" OnClick="btnUpload_Click" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="center-block pre-scrollable scroll-500" style="display: inline-block; overflow-y: auto;">
                                <asp:DataList ID="dlItems" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" OnDeleteCommand="dlItems_DeleteCommand">
                                    <AlternatingItemStyle BackColor="Transparent" />
                                    <ItemStyle BorderColor="Transparent" BorderWidth="20" />
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Image ID="Image1" ImageUrl='<%# Eval("PictureURL") %>' runat="server" Width="100" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button1" runat="server" Text="Remove" CommandName="Delete" CommandArgument='<%# Eval("PictureID") %>' /></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1 col-sm-offset-10">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="StandardButton" Text="Save" OnClick="btnSubmit_Click" />
                </div>
            </div>
            </section>
    </div>
</asp:Content>
