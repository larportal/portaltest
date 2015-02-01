<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUserCampaignsDropDownList.ascx.cs" Inherits="LarpPortal.ucUserCampaignsDropDownList" %>
        <asp:Label ID="lblSelectCampaign" runat="server">&nbsp;&nbsp;Select Campaign</asp:Label><br />&nbsp;
        <asp:DropDownList ID="ddlUserCampaigns" runat="server" CssClass="col-sm-10" AutoPostBack="true" OnSelectedIndexChanged="ddlUserCampaigns_SelectedIndexChanged">
        </asp:DropDownList>
    <br />
