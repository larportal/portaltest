<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharItems.aspx.cs" Inherits="LarpPortal.Character.CharItems" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active">
            <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Items" />
            </div>
            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
                <table>
                    <tr style="vertical-align: middle;">
                        <td style="width: 10px"></td>
                        <td>
                            <b>Selected Character:</b>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="ddlCharacterSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacterSelector_SelectedIndexChanged" />
                        </td>
                        <td style="padding-left: 20px;">
                            <b>Campaign:</b>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Label ID="lblCampaign" runat="server" Text="Fifth Gate" />
                        </td>
                        <td style="padding-left: 20px;">
                            <b>Last Update:</b>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Label ID="lblUpdateDate" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="col-sm-8">
                <div class="row" style="padding-left: 15px; margin-bottom: 20px">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Costumes</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <asp:TextBox ID="tbCostume" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" style="padding-left: 15px; margin-bottom: 20px">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Make-up</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <asp:TextBox ID="tbMakeup" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" style="padding-left: 15px; margin-bottom: 20px">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Accessories</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <asp:TextBox ID="tbAccessories" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" style="padding-left: 15px; margin-bottom: 20px">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Weapons &amp; Armor</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <asp:TextBox ID="tbWeapons" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" style="padding-left: 15px; margin-bottom: 20px">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Other Items</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <asp:TextBox ID="tbOtherItems" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div class="col-sm-4">
                <div class="row" style="padding-left: 10px;">
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
        <div class="row">
            <div class="col-sm-1 col-sm-offset-10">
                <asp:Button ID="btnSubmit" runat="server" CssClass="StandardButton" Text="Save" OnClick="btnSubmit_Click" />
            </div>
        </div>
<%--        </section>--%>
    </div>
</asp:Content>
