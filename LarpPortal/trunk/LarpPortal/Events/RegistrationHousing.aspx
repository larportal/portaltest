<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="RegistrationHousing.aspx.cs" Inherits="LarpPortal.Events.RegistrationHousing" %>

<asp:Content ID="Styles" ContentPlaceHolderID="MemberStyles" runat="server">
    <style type="text/css">
        .max-width-300 {
            max-width: 300px;
        }

        .TableTextBox {
            border: 1px solid black;
        }

        th, tr:nth-child(even) > td {
            background-color: #ffffff;
        }

        .CharInfoTable {
            border-collapse: collapse;
        }

            .CharInfoTable td {
                padding: 4px;
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
            <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Width="125" Text="Save" OnClick="btnSave_Click" UseSubmitBehavior="false" />
        </div>
    </div>

    <div id="character-info" class="character-info tab-pane active">
        <section role="form">
            <div class="col-sm-12">
                <div class="row" style="padding-left: -15px;">
                    <asp:UpdatePanel ID="upHousing" runat="server">
                        <ContentTemplate>
                            <div id="Div1" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Event Registrations</h2>
                                        <div class="panel-body">
                                            <div class="panel-container">
                                                <!-- style="height: 500px; overflow: auto;">  -->
                                                <asp:GridView ID="gvRegistrations" runat="server" AutoGenerateColumns="false" GridLines="None" HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed" AllowSorting="true" OnSorting="gvRegistrations_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidRegistrationID" runat="server" Value='<%# Eval("RegistrationID") %>' />
                                                                <asp:HiddenField ID="hidRegStatusID" runat="server" Value='<%# Eval("RegistrationStatusID") %>' />
                                                                <asp:HiddenField ID="hidOrigHousing" runat="server" Value='<%# Eval("OrigHousing") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Player Name" ItemStyle-Wrap="true" SortExpression="PlayerName" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPlayerName" runat="server" Text='<%# Eval("PlayerName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Role" ItemStyle-Wrap="false" SortExpression="RoleAlignmentDescription" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRole" runat="server" Text='<%# Eval("RoleAlignmentDescription") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Character Name" ItemStyle-Wrap="true" SortExpression="CharacterName" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCharacterName" runat="server" Text='<%# Eval("CharacterName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Team Name" ItemStyle-Wrap="true" SortExpression="TeamName" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTeamName" runat="server" Text='<%# Eval("TeamName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reg Status" SortExpression="RegistrationStatus" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRegStatus" runat="server" Text='<%# Eval("RegistrationStatus") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlRegStatus" runat="server" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Req Housing" SortExpression="ReqstdHousing" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHousing" runat="server" Text='<%# Eval("ReqstdHousing") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comments" SortExpression="PlayerCommentsToStaff" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="Blue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblComments" runat="server" Text='<%# Eval("PlayerCommentsToStaff") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Assigned Housing" SortExpression="AssignHousing" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="blue">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="tbAssignHousing" runat="server" Text='<%# Eval("AssignHousing") %>' CssClass="TableTextBox" Width="150px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </section>
    </div>
    <asp:HiddenField ID="hidApprovedStatus" runat="server" />
</asp:Content>
