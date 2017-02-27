<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MissingEvent.aspx.cs" Inherits="LarpPortal.PELs.MissingEvent" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ID="ScriptSection" runat="server" ContentPlaceHolderID="MemberScripts">
    <script type="text/javascript">
        function DisplayRoles(DropDownListRoles) {
            var Role = DropDownListRoles.options[DropDownListRoles.selectedIndex].text;
            if (Role != null) {
                var trPCStaff = document.getElementById('<%= divPCStaff.ClientID %>');
                var trNPC = document.getElementById('<%= divNPC.ClientID %>');
                var trSendCPOther = document.getElementById('<%= divSendOther.ClientID %>');
                if ((Role == 'PC') || (Role == 'Staff')) {
                    trPCStaff.style.display = 'block';
                    trNPC.style.display = 'none';
                    trSendCPOther.style.display = 'none';
                }
                else {
                    trPCStaff.style.display = 'none';
                    trNPC.style.display = 'block';
                    trSendCPOther.style.display = 'block';
                }
            }
        }

        //function closeRegistration() {
        //    $('#modalRegistration').hide();
        //}

        function openMessage() {
            $('#modalMessage').modal('show');
            return false;
        }
        function closeMessage() {
            $('#modalMessage').hide();
        }

        //function openRegistration() {
        //    $('#modalRegistration').modal('show');
        //    return false;
        //}

    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="MemberStyles">
    <style>
        div {
            border: 0px solid black;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px; vertical-align: bottom;">
            <div class="col-sm-6">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Post Event Registration" />
            </div>
        </div>

        <div class="row col-xs-10" style="padding-left: 15px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>Event List</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                            <div style="margin-right: 10px;" runat="server" id="divEventList">
                                <div class="row">
                                    <div class="TableLabel col-sm-2">
                                        Event List:
                                    </div>
                                    <div class="col-sm-9 NoLeftPadding">
                                        <asp:DropDownList ID="ddlMissedEvents" runat="server" OnSelectedIndexChanged="ddlMissedEvents_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="row Padding5">
                                    <div class="TableLabel col-sm-2">Role: </div>
                                    <div class="col-sm-9 NoLeftPadding">
                                        <asp:DropDownList ID="ddlRoles" runat="server" /><asp:Label ID="lblRole" runat="server" />
                                    </div>
                                </div>
                                <div class="row Padding5" id="divPCStaff" runat="server">
                                    <div class="col-sm-2 TableLabel">
                                        Character:
                                    </div>
                                    <div class="col-sm-9 NoLeftPadding">
                                        <asp:DropDownList ID="ddlCharacterList" runat="server" /><asp:Label ID="lblCharacter" runat="server" />
                                    </div>
                                </div>

                                <div class="row Padding5" id="divNPC" runat="server">
                                    <div class="col-sm-2 TableLabel">
                                        Send CP to:
                                    </div>
                                    <div class="col-sm-9 NoLeftPadding">
                                        <asp:DropDownList ID="ddlSendToCampaign" runat="server" />
                                    </div>
                                </div>

                                <div class="row Padding5" style="padding-right: 0px;" id="divSendOther" runat="server">
                                    <div class="col-sm-2 TableLabel">Comments To Staff:</div>
                                    <div class="col-sm-10 NoLeftPadding">
                                        <asp:TextBox ID="tbSendToCPOther" runat="server" CssClass="col-lg-12 col-sm-12" Style="padding: 0px;" MaxLength="500" Rows="4" TextMode="MultiLine" />
                                    </div>
                                </div>
                            </div>

                            <div id="divNoEvents" runat="server">
                                <h1>There are no events that you have not registered for yet.</h1>
                            </div>

                            <div class="row" style="padding-top: 20px;">
                                <div class="col-xs-6">
                                    <asp:Button ID="btnReturn" runat="server" Text="Return To PELs" Width="150px" CssClass="StandardButton" PostBackUrl="~/PELs/PELList.aspx" />
                                </div>
                                <div class="col-xs-6 text-right">
                                    <asp:Button ID="btnRegisterForEvent" runat="server" Text="Register For Event" Width="150px" CssClass="StandardButton" OnClick="btnRegisterForEvent_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style type="text/css">
        .Padding5 {
            padding-top: 5px;
        }
    </style>


    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Registration
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnCloseMessage_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
