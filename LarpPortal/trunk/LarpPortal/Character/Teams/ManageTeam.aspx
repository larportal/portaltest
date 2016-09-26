<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageTeam.aspx.cs" Inherits="LarpPortal.Character.Teams.ManageTeam" MasterPageFile="~/Character/Character.Master" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="HistoryScripts" runat="server" ContentPlaceHolderID="CharHeaderScripts">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
        function closeMessage() {
            $('#modalMessage').hide();
        }

        function openError() {
            $('#modalError').modal('show');
        }
        function closeError() {
            $('#modelError').hide();
        }
    </script>

    <script src="../../Scripts/jquery-1.11.3.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/bootstrap.js"></script>
</asp:Content>


<asp:Content ID="HistoryStyles" runat="server" ContentPlaceHolderID="CharHeaderStyles">
    <style type="text/css">
        editArea {
            overflow: scroll;
            height: 425px;
        }

        div {
            border: 0px solid black;
        }

        .table th, .table td, .table {
            border-top: none !important;
            border-left: none !important;
            border-bottom: none !important;
            border-right: none !important;
        }

        .fixed-table-container {
            border: 0px;
            border-bottom: none !important;
        }

        .thNoBorder, .tdNoBorder {
            padding: 0px 0px 0px 0px;
            margin: 0px 0px 0px 0px;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="PELList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row col-lg-12" style="padding-left: 15px; padding-top: 10px;">
                <div class="col-lg-6" style="padding-left: 0px;">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Characters" />
                </div>
                <div class="col-lg-6 text-center">
                    <asp:Label ID="lblChangesNotSaved" runat="server" Font-Bold="true" Text="Changes have not been saved." Style="padding-right: 25px;" Visible="false" Font-Size="X-Large" />
                </div>
            </div>

            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
                <div class="row col-lg-12">
                    <div class="col-lg-2 text-right">
                        <b>Selected Character: </b>
                    </div>
                    <div class="col-lg-10 test-left">
                        <asp:DropDownList ID="ddlCharacterSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacterSelector_SelectedIndexChanged" />
                    </div>
                </div>
            </div>

            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
                <div class="row col-lg-12">
                    <div class="col-lg-2 text-right">
                        <b>Team: </b>
                    </div>
                    <div class="col-lg-4 text-left">
                        <asp:DropDownList ID="ddlTeams" runat="server" AutoPostBack="true" />
                        <asp:Label ID="lblTeamName" runat="server" Visible="false" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6">
                    <div class="panel col-lg-12" style="padding-top: 0px; padding-bottom: 0px; padding-left: 0px; padding-right: 0px;">
                        <div class="panelheader">
                            <h2>Members</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="max-height: 400px; overflow-y: auto;">
                                    <asp:GridView ID="gvAvailable" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover table-condensed"
                                        OnRowCommand="gvAvailable_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="CharacterAKA" HeaderText="Character" />
                                            <asp:BoundField DataField="Message" HeaderText="" ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnInvite" runat="server" CssClass="StandardButton" Text="Invite" Width="100px"
                                                        Visible='<%# Eval("DisplayInvite") == "1" %>' CommandArgument='<%# Eval("CharacterID") %>' CommandName="InviteChar" OnClientClick="setScrollValue();" />
                                                    <asp:Button ID="btnDeny" runat="server" CssClass="StandardButton" Text="Deny" Width="100px"
                                                        Visible='<%# Eval("DisplayApprove") == "1" %>' CommandArgument='<%# Eval("CharacterID") %>' CommandName="DenyChar" OnClientClick="setScrollValue();" />
                                                    <asp:Button ID="btnApprove" runat="server" CssClass="StandardButton" Text="Approve" Width="100px"
                                                        Visible='<%# Eval("DisplayApprove") == "1" %>' CommandArgument='<%# Eval("CharacterID") %>' CommandName="ApproveChar" OnClientClick="setScrollValue();" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6">
                    <div class="panel col-lg-12" style="padding-top: 0px; padding-bottom: 0px; padding-left: 0px; padding-right: 0px;">
                        <div class="panelheader">
                            <h2>Current Teams</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="max-height: 300px; overflow-y: auto;">
                                    <div class="row col-lg-12" style="padding-right: 0px;">
                                        <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover table-condensed" GridLines="both"
                                            OnRowCommand="gvAvailable_RowCommand" RowStyle-CssClass="thNoBorder tdNoBorder" HeaderStyle-CssClass="thNoBorder tdNoBorder">
                                            <Columns>
                                                <asp:BoundField DataField="CharacterAKA" HeaderText="Character" ItemStyle-CssClass="col-lg-12" />
                                                <asp:TemplateField HeaderText="Approver" ItemStyle-Width="80" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkBoxApprover" runat="server" Checked='<%# Eval("Approval") %>' AutoPostBack="true" OnCheckedChanged="chkBoxApprover_CheckedChanged" />
                                                        <asp:HiddenField ID="hidCharacterID" runat="server" Value='<%# Eval("CharacterID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Message" HeaderText="" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="col-sm-12" ItemStyle-Wrap="false" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAccept" runat="server" CssClass="StandardButton" Text="Accept" Width="100px"
                                                            Visible='<%# Eval("DisplayAccept") == "1" %>' CommandArgument='<%# Eval("CharacterID") %>' CommandName="ApproveChar" OnClientClick="setScrollValue();" />
                                                        <asp:Button ID="btnDeny" runat="server" CssClass="StandardButton" Text="Deny" Width="100px"
                                                            Visible='<%# Eval("DisplayAccept") == "1" %>' CommandArgument='<%# Eval("CharacterID") %>' CommandName="DenyChar" OnClientClick="setScrollValue();" />
                                                        <asp:Button ID="btnRevoke" runat="server" CssClass="StandardButton" Text="Revoke" Width="100px"
                                                            Visible='<%# Eval("DisplayRevoke") == "1" %>' CommandArgument='<%# Eval("CharacterID") %>' CommandName="RevokeChar" OnClientClick="setScrollValue();" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row text-right col-lg-12" style="padding-right: 0px;">
                        <asp:Label ID="lblChangesNotSaved2" runat="server" Font-Bold="true" Text="Changes have not been saved." Style="padding-right: 25px;" Visible="false" Font-Size="Larger" />
                        <asp:Button ID="btnSave" runat="server" Width="100px" Text="Save" CssClass="StandardButton" Style="padding-right: 0px; margin-right: 0px; margin-top: 10px;" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Manage A Team
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblmodalMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnCloseMessage_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalError" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: red;">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Info
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblmodalError" runat="server" /></p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseError" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnCloseError_Click" />
                </div>
            </div>
        </div>
    </div>





    <asp:HiddenField ID="hidNotificationEMail" runat="server" Value="" />
    <asp:HiddenField ID="hidCampaignID" runat="server" Value="" />
    <asp:HiddenField ID="hidTeamID" runat="server" Value="" />
    <asp:HiddenField ID="hidScollPosition" runat="server" Value="" />

    <script type="text/javascript">
        function setScrollValue() {
            var divObj = $get('divAvailable');
            var obj = $get('<%= hidScollPosition.ClientID %>');
            if (obj) obj.value = divObj.scrollTop;
        }
        function pageLoad() {
            var divObj = $get('divAvailable');
            var obj = $get('<%= hidScollPosition.ClientID %>');
            if (divObj) divObj.scrollTop = obj.value;
        }
    </script>
</asp:Content>
