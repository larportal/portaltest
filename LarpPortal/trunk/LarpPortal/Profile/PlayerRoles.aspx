<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Profile.Master" AutoEventWireup="true" CodeBehind="PlayerRoles.aspx.cs" Inherits="LarpPortal.Profile.PlayerRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ProfileHeaderScripts" runat="server">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
        function closeMessage() {
            $('#modelMessage').hide();
        }
    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ProfileHeaderStyles" runat="server">
    <style>
        .radio.radiobuttonlist input[type="radio"],
        .checkbox.checkboxlist input[type="checkbox"] {
            margin-left: 0;
        }

        .radio.radiobuttonlist label,
        .checkbox.checkboxlist label {
            margin-bottom: 4px;
            margin-left: 0;
        }

        fieldset legend {
            border: 0;
            font-size: inherit;
            font-weight: 700;
            margin-bottom: 0;
        }

        fieldset .radio,
        fieldset .checkbox {
            margin-top: 4px;
        }

        .NarrowTable {
            white-space: nowrap !important;
            width: 1% !important;
            border-style: none !important;
        }

        .WhiteRow {
            white-space: nowrap;
            background-color: #ffffff;
        }

        .GreyRow {
            white-space: nowrap;
            background-color: #eeeeee;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ProfileMain" runat="server">
    <div class="col-lg-12 row">
        <h2>Roles</h2>
    </div>
    <div class="col-lg-12 row">
        Campaign:
        <asp:DropDownList ID="ddlUserCampaigns" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserCampaigns_SelectedIndexChanged" />
    </div>

    <br />
    <br />

    <asp:Repeater ID="rptRoles" runat="server" OnItemDataBound="rptRoles_ItemDataBound">
        <ItemTemplate>
            <div class="col-lg-12 row">
                <h2><%# Eval("DisplayGroup") %></h2>
            </div>
            <ul class="col-lg-12">
                <asp:Label ID="lblDesc" runat="server" />
            </ul>
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <br>

    <asp:HiddenField ID="hidCampaignPlayerID" runat="server" />
    <asp:HiddenField ID="hidDisplayName" runat="server" />

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Player Roles
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
</asp:Content>
