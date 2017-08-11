<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index1.aspx.cs" Inherits="LarpPortal.Index1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="contentArea">
        <aside></aside>
        <div class="mainContent tab-content col-lg-6 input-group">
            <section id="campaign-info" class="campaign-info tab-pane active">
                <form id="frmHidFields" runat="server">
                    <div role="form" class="form-horizontal">
                        <div class="col-sm-12 NoPadding">
                            <h1 class="col-sm-12">Welcome to LARP Portal</h1>
                        </div>
                        <div class="row col-sm-12 NoPadding" style="padding-left: 25px;">
                            <div class="col-lg-12 NoPadding" style="padding-left: 15px;">
                                <div class="panel NoPadding" style="padding-top: 0px; padding-bottom: 0px; min-height: 50px;">
                                    <div class="panelheader NoPadding">
                                        Select a starting campaign.
                                    </div>
                                    <div class="panel-body NoPadding">
                                        <div class="panel-container NoPadding">
                                            <asp:Label ID="lblPageText" runat="server" Text=""></asp:Label>
                                            <asp:DropDownList ID="ddlCampaign" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaign_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="row">
                                            <br /><br />Select how you're going to participate in the campaign.<br />
                                            If you're going to staff the game, sign up as an NPC and notify the GM/owner.
                                        </div>
                                        <div class="row">
                                            <asp:CheckBoxList ID="cblRole" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cblRole_SelectedIndexChanged">
                                                <asp:ListItem Text="PC" Value="PC"></asp:ListItem>
                                                <asp:ListItem Text="NPC" Value="NPC"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row"></div>
                        <div class="row PrePostPadding">
                            <div class="col-sm-11">
                                <asp:HiddenField ID="hidRole" runat="server" />
                                <asp:HiddenField ID="hidCampaign" runat="server" />
                            </div>
                            <div class="col-sm-1">
                                <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Visible="false" Text="Sign up" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </form>
            </section>
        </div>
    </div>
</body>
</html>

