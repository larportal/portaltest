<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharHistory.aspx.cs" Inherits="LarpPortal.Character.CharHistory" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character History" />
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
                        <asp:Label ID="lblCampaign" runat="server" Text="" />
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

        <div class="row" style="padding-left: 15px; margin-bottom: 20px">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>History</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                            <asp:TextBox ID="tbHistory" runat="server" TextMode="MultiLine" Rows="10" Width="100%" />
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
