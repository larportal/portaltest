<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharItems.aspx.cs" Inherits="LarpPortal.Character.CharItems" EnableViewState="true" %>

<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>

<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="CharHeaderScripts">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="Style" runat="server" ContentPlaceHolderID="CharHeaderStyles">
    <style type="text/css">
        .divLocal {
            border: 0px solid black;
            padding: 15px 0px 0px 0px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CharHeaderMain" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active">
            <div class="row divLocal" style="padding-top: 10px; padding-bottom: 10px;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Items" />
            </div>
            <div class="row col-sm-12 divLocal" style="padding-bottom: 20px; padding-right: 0px;">
                <div class="col-sm-10" style="padding: 0px 0px 0px 0px;">
                    <CharSelecter:CSelect ID="oCharSelect" runat="server" />
                </div>
                <div class="col-sm-2 text-right">
                    <asp:Button ID="btnSaveTop" runat="server" CssClass="StandardButton" Width="100px" Text="Save" OnClick="btnSave_Click" />
                </div>
            </div>

            <div class="row col-sm-12 divLocal">
                <div class="col-sm-8">
                    <div class="row" style="margin-bottom: 20px">
                        <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="panelheader">
                                <h2>Costumes</h2>
                                <div class="panel-body">
                                    <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                        <asp:TextBox ID="tbCostume" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                        <asp:Label ID="lblCostume" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-bottom: 20px">
                        <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="panelheader">
                                <h2>Make-up</h2>
                                <div class="panel-body">
                                    <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                        <asp:TextBox ID="tbMakeup" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                        <asp:Label ID="lblMakeup" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-bottom: 20px">
                        <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="panelheader">
                                <h2>Accessories</h2>
                                <div class="panel-body">
                                    <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                        <asp:TextBox ID="tbAccessories" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                        <asp:Label ID="lblAccessories" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-bottom: 20px">
                        <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="panelheader">
                                <h2>Weapons &amp; Armor</h2>
                                <div class="panel-body">
                                    <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                        <asp:TextBox ID="tbWeapons" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                        <asp:Label ID="lblWeapons" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-bottom: 20px">
                        <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="panelheader">
                                <h2>Other Items</h2>
                                <div class="panel-body">
                                    <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                        <asp:TextBox ID="tbOtherItems" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                        <asp:Label ID="lblOtherItems" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="row" style="padding-left: 10px;" id="divAddPicture" runat="server">
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
                    <div class="row" style="padding-left: 10px;">
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
        </div>
        <div class="row col-sm-12 text-right divLocal">
            <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Width="100px" Text="Save" OnClick="btnSave_Click" />
        </div>
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Items
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblmodalMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
