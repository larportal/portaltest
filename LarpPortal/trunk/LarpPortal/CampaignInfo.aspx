<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="CampaignInfo.aspx.cs" Inherits="LarpPortal.CampaignInfo" %>
<asp:Content ID="MemberCampaigns" ContentPlaceHolderID="MemberCampaignsContent" runat="server">
    <asp:Label ID="WIP" runat="server" BackColor="Yellow">Member Campaigns - Campaign Info - Placeholder page - in progress</asp:Label>
    <br /><br />
    This is the landing page for all members upon clicking the Campaign tab.<br />
    1) On Load Run LoadUserCampaigns in cUser class.<br />
        &nbsp;&nbsp;&nbsp;&nbsp;a) Populate user campaign list<br />
        &nbsp;&nbsp;&nbsp;&nbsp;b) Put last visited campaign on top<br />
    2) Load the last visited campaign pages in proper mode based on roles.
</asp:Content>