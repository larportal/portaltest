<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CharacterSelect.ascx.cs" Inherits="LarpPortal.controls.CharacterSelect" %>

<div class="row col-sm-12">
    <%-- style="border: solid 1px black;">--%>
    <b>Selected Character:</b>&nbsp;
    <asp:DropDownList ID="ddlCharacterSelector" runat="server" AutoPostBack="true" Style="padding-right: 10px;" OnSelectedIndexChanged="ddlCharacterSelector_SelectedIndexChanged" />
    <asp:DropDownList ID="ddlCampCharSelector" runat="server" AutoPostBack="true" Style="padding-right: 10px;" OnSelectedIndexChanged="ddlCampCharSelector_SelectedIndexChanged" />
    <asp:Label ID="lblNoCharacters" runat="server" Text="There are no characters available for that campaign." Visible="false" />
    <asp:RadioButton ID="rbMyCharacters" GroupName="CharacterGroup" runat="server" AutoPostBack="true" Text="My Characters" Style="padding-right: 10px;" OnCheckedChanged="rbMyCharacters_CheckedChanged" />
    <asp:RadioButton ID="rbCampaignCharacters" GroupName="CharacterGroup" runat="server" AutoPostBack="true" Text="Campaign Characters" Style="padding-right: 10px;" OnCheckedChanged="rbMyCharacters_CheckedChanged" />
    <asp:Label ID="lblCampaign" runat="server" Text="Campaign: " />
    <asp:DropDownList ID="ddlCampaigns" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaigns_SelectedIndexChanged" />
    <asp:Label ID="lblSelectedCampaign" runat="server" Text="" />
    <asp:Label ID="lblSelectedCharCampaign" runat="server" Text="" />
    <asp:HiddenField ID="hidNumMyChar" runat="server" Value="0" />
    <asp:HiddenField ID="hidNumMyCamp" runat="server" Value="0" />
</div>
