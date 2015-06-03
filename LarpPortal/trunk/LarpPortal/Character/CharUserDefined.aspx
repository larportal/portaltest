<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharUserDefined.aspx.cs" Inherits="LarpPortal.Character.CharUserDefined" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mainContent tab-content col-lg-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Goals & Preferences" />
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

        <div id="divUserDef1" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>
                        <asp:Label ID="lblUserDef1" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField1" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divUserDef2" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>
                        <asp:Label ID="lblUserDef2" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField2" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divUserDef3" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>
                        <asp:Label ID="lblUserDef3" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField3" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divUserDef4" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>
                        <asp:Label ID="lblUserDef4" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField4" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divUserDef5" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2><asp:Label ID="lblUserDef5" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField5" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="padding-left: 15px; margin-bottom: 20px; text-align: right;">
            <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Text="Save" Width="100px" OnClick="btnSubmit_Click" />
        </div>
    </div>
</asp:Content>
