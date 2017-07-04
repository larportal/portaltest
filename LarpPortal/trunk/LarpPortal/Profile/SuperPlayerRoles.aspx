<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Profile.Master" AutoEventWireup="true" CodeBehind="SuperPlayerRoles.aspx.cs" Inherits="LarpPortal.Profile.SuperPlayerRoles" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ProfileMain" runat="server">
    <div class="col-lg-12 row">
        <h2>Player roles</h2>
    </div>
    <div class="col-lg-12 row">
        <asp:DropDownList ID="ddlPlayer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPlayer_SelectedIndexChanged" />
        Campaign
        <asp:DropDownList ID="ddlCampaign" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaign_SelectedIndexChanged" />
    </div>

    <div class="col-lg-12 row" style="margin-top: 20px;">
        <div class="col-lg-5" style="margin-left: -15px;">
            <b>Campaign Roles</b>
        </div>
        <div class="col-lg-3" style="margin-left: 15px;">
            <b>Database Roles</b>
        </div>
        <div class="col-lg-3" style="margin-left: 15px;">
            <b>Team Roles</b>
        </div>
    </div>
    <div class="col-lg-12 row">
        <div class="col-lg-5" style="border: 1px solid black; padding-left: 0px;">
            <asp:CheckBoxList ID="cbxRolesCamp1" runat="server" RepeatLayout="flow" RepeatDirection="vertical" CssClass="col-lg-6" />
            <asp:CheckBoxList ID="cbxRolesCamp2" runat="server" RepeatLayout="flow" RepeatDirection="vertical" CssClass="col-lg-6" />
        </div>
        <div class="col-lg-3" style="border: 1px solid black; margin-left: 15px;">
            <asp:CheckBoxList ID="cbxRolesDB" runat="server" RepeatLayout="flow" RepeatDirection="vertical" />
        </div>
        <div class="col-lg-3" style="border: 1px solid black; margin-left: 15px;">
            <asp:CheckBoxList ID="cbxRolesTeam" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical" />
        </div>
    </div>

    <div class="col-lg-12 text-right row">
        <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Width="100" Text="Save" OnClick="btnSave_Click" />
    </div>
    <asp:HiddenField ID="hidCampaignPlayerID" runat="server" />

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
