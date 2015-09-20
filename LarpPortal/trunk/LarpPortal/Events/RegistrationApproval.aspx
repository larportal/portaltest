<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="RegistrationApproval.aspx.cs" Inherits="LarpPortal.Events.RegistrationApproval" %>

<asp:Content ID="Styles" ContentPlaceHolderID="MemberStyles" runat="server">
    <style type="text/css">
        .max-width-300
        {
            max-width: 300px;
        }
    </style>
</asp:Content>

<asp:Content ID="Scripts" ContentPlaceHolderID="MemberScripts" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MemberCampaignsContent" runat="server">

    <div class="row col-sm-12" style="padding-top: 10px; padding-bottom: 10px;">
        <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Font-Bold="true" Text="Event Registrations" />
    </div>
    <div class="row col-sm-12" style="">
        <div class="col-sm-10">
            <div class="col-sm-4">
                <strong>Event: </strong>
                <asp:DropDownList ID="ddlEvent" runat="server" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="0" Text="Select Event/Date"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-8">
                <asp:Label ID="lblEventInfo" runat="server" Text="Event Info" />
            </div>
        </div>
        <div class="text-right">
            <asp:Button ID="btnApproveAll" runat="server" CssClass="StandardButton" Width="125" Text="Approve All" OnClick="btnApproveAll_Click" />
        </div>
    </div>

    <div id="character-info" class="character-info tab-pane active">
        <section role="form">
            <div class="col-sm-12">
                <div class="row" style="padding-left: -15px;">
                    <div id="Div1" class="panel-wrapper" runat="server">
                        <div class="panel">
                            <div class="panelheader">
                                <h2>Event Registrations</h2>
                                <div class="panel-body">
                                    <div class="panel-container" style="height: 500px; overflow: auto;">
                                        <asp:GridView ID="gvRegistrations" runat="server" OnRowDataBound="gvRegistrations_RowDataBound" OnRowCommand="gvRegistrations_RowCommand"
                                            OnRowEditing="gvRegistrations_RowEditing" OnRowUpdating="gvRegistrations_RowUpdating" OnRowCancelingEdit="gvRegistrations_RowCancelingEdit"
                                            AutoGenerateColumns="false"
                                            GridLines="None"
                                            HeaderStyle-Wrap="false"
                                            CssClass="table table-striped table-hover table-condensed">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hidRegistrationID" runat="server" Value='<%# Eval("RegistrationID") %>' />
                                                        <asp:HiddenField ID="hidRegStatusID" runat="server" Value='<%# Eval("RegistrationStatusID") %>' />
                                                        <asp:HiddenField ID="hidCampaignHousingID" runat="server" Value='<%# Eval("CampaignHousingTypeID") %>' />
                                                        <asp:HiddenField ID="hidPaymentTypeID" runat="server" Value='<%# Eval("EventPaymentTypeID") %>' />
                                                        <asp:HiddenField ID="hidPaymentDate" runat="server" Value='<%# Eval("EventPaymentDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Player Name" ItemStyle-Wrap="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPlayerName" runat="server" Text='<%# Eval("PlayerName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Role" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRole" runat="server" Text='<%# Eval("RoleAlignmentDescription") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Character Name" ItemStyle-Wrap="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCharacterName" runat="server" Text='<%# Eval("CharacterName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Team Name" ItemStyle-Wrap="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTeamName" runat="server" Text='<%# Eval("TeamName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Housing">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHousing" runat="server" Text='<%# Eval("CampaignHousing") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaymentType" runat="server" Text='<%# Eval("DisplayPayment") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlPaymentType" runat="server" /><br />
                                                        <asp:TextBox ID="tbPayment" runat="server" Text='<%# Eval("EventPaymentAmount", "{0:0.00}") %>' CssClass="TextBox" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEventPaymentDate" runat="server" Text='<%# Eval("EventPaymentDate", "{0:d}") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Calendar ID="calPaymentDate" runat="server" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comments" ItemStyle-CssClass="max-width-300">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComments" runat="server" Text='<%# Eval("PlayerCommentsToStaff") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reg Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRegStatus" runat="server" Text='<%# Eval("RegistrationStatus") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlRegStatus" runat="server" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnApprove" runat="server" CommandName="Approve" Text="Approve" Width="100px" CssClass="StandardButton" />
                                                        <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" Width="100px" CssClass="StandardButton" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" Width="100px" CssClass="StandardButton" />
                                                        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" Width="100px" CssClass="StandardButton" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
