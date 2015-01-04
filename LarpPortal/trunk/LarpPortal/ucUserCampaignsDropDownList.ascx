<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUserCampaignsDropDownList.ascx.cs" Inherits="LarpPortal.ucUserCampaignsDropDownList" %>
        <asp:Label ID="lblSelectCampaign" runat="server">&nbsp;&nbsp;Select Campaign</asp:Label><br />&nbsp;
        <asp:DropDownList ID="ddlUserCampaigns" runat="server" CssClass="col-sm-10" AutoPostBack="true" OnSelectedIndexChanged="ddlUserCampaigns_SelectedIndexChanged">
            <asp:ListItem Text="Madrigal" Value="1"></asp:ListItem>
            <asp:ListItem Text="Cottington Woods" Value="7"></asp:ListItem>
            <asp:ListItem Text="Mirror, Mirror" Value="8"></asp:ListItem> 
            <asp:ListItem Text="Seven Virtues" Value="15"></asp:ListItem>
        </asp:DropDownList>
    <br />
