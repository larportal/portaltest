<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharSkills.aspx.cs" Inherits="LarpPortal.Character.CharSkills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainContent tab-content col-lg-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Skills" />
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
                <tr>
                    <td style="background-color: transparent; padding-top: 10px;"></td>
                    <td colspan="6" style="background-color: transparent; padding-top: 10px;">Skill validation may take a few moments.  Please wait while screen refreshes <u>BEFORE</u> Saving.</td>
                </tr>
            </table>
        </div>

        <div class="row" style="padding-left: 15px;">
            <div class="panel" style="padding-top: 5px;">
                <div class="panelheader">
                    <h2>Character Skills</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria">
                            <iframe id="FrameSkills" name="FrameSkills" src="CharSkill.aspx"
                                style="border: 0px solid green; width: 100%; height: 600px" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
