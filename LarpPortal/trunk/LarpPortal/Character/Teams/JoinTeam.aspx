<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JoinTeam.aspx.cs" Inherits="LarpPortal.Character.Teams.JoinTeam" MasterPageFile="~/Character/Character.Master" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>


<asp:Content ID="HistoryScripts" runat="server" ContentPlaceHolderID="CharHeaderScripts">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
        function closeMessage() {
            $('#modelMessage').hide();
        }

        function showNewAddendum() {
            document.getElementById('divNewAddendum').style.display = "block";
        }

        function hideNewAddendum() {
            document.getElementById('divNewAddendum').style.display = "none";
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
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="CharHeaderMain" ID="PELList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row col-lg-12" style="padding-left: 15px; padding-top: 10px;">
                <div class="col-lg-10" style="padding-left: 0px;">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Teams" />
                </div>
            </div>

            <div class="row col-sm-12" style="padding-bottom: 15px; padding-left: 15px; padding-right: 0px;">
                <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">
                    <CharSelecter:CSelect ID="oCharSelect" runat="server" />
                </div>
                <div class="col-sm-2">
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6">
                    <div class="panel col-lg-12" style="padding-top: 0px; padding-bottom: 0px; padding-left: 0px; padding-right: 0px;">
                        <div class="panelheader">
                            <h2>Teams</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="max-height: 400px; overflow-y: auto;">
                                    <asp:GridView ID="gvAvailable" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover table-condensed" ShowHeader="false"
                                        OnRowCommand="gvAvailable_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="TeamName" HeaderText="Character" />
                                            <asp:BoundField DataField="Message" HeaderText="" ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-CssClass="text-right" ItemStyle-Width="250">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnJoin" runat="server" CssClass="StandardButton" Text="Join Team" Width="100px"
                                                        Visible='<%# Eval("Join") == "1" %>' CommandArgument='<%# Eval("TeamID") %>' CommandName="JoinTeam" OnClientClick="setScrollValue();" />
                                                    <asp:Button ID="btnAccept" runat="server" CssClass="StandardButton" Text="Accept" Width="100px"
                                                        Visible='<%# Eval("Accept") == "1" %>' CommandArgument='<%# Eval("TeamID") %>' CommandName="AcceptInvite" OnClientClick="setScrollValue();" />
                                                    <asp:Button ID="btnDecline" runat="server" CssClass="StandardButton" Text="Decline" Width="100px"
                                                        Visible='<%# Eval("Accept") == "1" %>' CommandArgument='<%# Eval("TeamID") %>' CommandName="DeclineInvite" OnClientClick="setScrollValue();" />
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
                                        <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover table-condensed" GridLines="none"
                                            ShowHeader="false" OnRowCommand="gvAvailable_RowCommand" Visible="true">
                                            <Columns>
                                                <asp:BoundField DataField="TeamName" HeaderText="Character" />
                                                <asp:BoundField DataField="Message" HeaderText="" ItemStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField ItemStyle-Width="150" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnLeave" runat="server" CssClass="StandardButton" Text="Leave" Width="100px"
                                                            Visible='<%# Eval("DisplayLeave") == "1" %>' CommandArgument='<%# Eval("TeamID") %>' CommandName="LeaveTeam" OnClientClick="setScrollValue();" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="StandardButton" Text="Cancel" Width="100px"
                                                            Visible='<%# Eval("Requested").ToString() == "1" %>' CommandArgument='<%# Eval("TeamID") %>' CommandName="CancelRequest" OnClientClick="setScrollValue();" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
<%--                    <div class="text-right col-lg-12" style="padding-right: 0px;">
                        <asp:Button ID="btnSave" runat="server" Width="100px" Text="Save" CssClass="StandardButton" Style="padding-right: 0px; margin-right: 0px; margin-top: 10px;" OnClick="btnSave_Click" />
                    </div>--%>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character History
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblmodalMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" />
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidNotificationEMail" runat="server" Value="" />
    <asp:HiddenField ID="hidCampaignID" runat="server" Value="" />
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
