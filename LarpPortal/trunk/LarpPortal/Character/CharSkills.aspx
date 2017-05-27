<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharSkills.aspx.cs" Inherits="LarpPortal.Character.CharSkills" EnableEventValidation="false" %>

<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>

<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="CharHeaderScripts">
    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>

    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }

        function openMessageWithText(Msg) {
            var lblmodalMessage = document.getElementById("<%= lblmodalMessage.ClientID %>");
            if (lblmodalMessage) {
                lblmodalMessage.innerHTML = Msg;
            }
            $('#modalMessage').modal('show');
        }

        function openErrorWithText(Msg) {
            var lblmodalError = document.getElementById("<%= lblmodalError.ClientID %>");
            if (lblmodalError) {
                lblmodalError.innerHTML = Msg;
            }
            $('#modalError').modal('show');
        }
    </script>
</asp:Content>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="CharHeaderStyles">
    <style type="text/css">
        div {
            border: 0px solid black;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CharHeaderMain" runat="server">

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Skills
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

    <div class="modal" id="modalError" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: red;">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Skills
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblmodalError" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseError" runat="server" Text="Close" Width="150px" CssClass="StandardButton" />
                </div>
            </div>
        </div>
    </div>

    <div class="mainContent tab-content col-lg-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Skills" />
        </div>

        <div class="row col-sm-12" style="padding-bottom: 0px; padding-left: 15px; padding-right: 0px;">
            <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">
                <CharSelecter:CSelect ID="oCharSelect" runat="server" />
            </div>
            <div class="col-sm-2 text-right">
                <asp:Button ID="btnSave" runat="server" Width="100px" CssClass="StandardButton" Text="Save" OnClick="btnSave_Click" />
            </div>
        </div>

        <div class="row col-sm-12" style="padding-left: 15px; padding-bottom: 0px; padding-right: 0px;" id="divExcl" runat="server">
            <asp:CheckBox ID="cbxShowExclusions" runat="server" Text="Show Exclusions" AutoPostBack="true" OnCheckedChanged="cbxShowExclusions_CheckedChanged" />
        </div>

        <asp:Label ID="lblSpacer" runat="server" />
        <div class="row" style="padding-left: 15px; padding-top: 20px; margin-top: 0px;">
            <div class="panel" style="padding-top: 0px;">
                <div class="panelheader">
                    <h2>Character Skills</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria" style="padding-left: 0px; padding-right: 0px; padding-top: 0px; padding-bottom: 0px;">   
                            <iframe id="FrameSkills" name="FrameSkills" src="CharSkill.aspx"
                                style="border: 0px solid green; width: 100%; height: 600px" />  
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row col-sm-12" style="padding-bottom: 0px; padding-left: 15px; padding-right: 0px;">
            <div class="col-sm-12 text-right">
                <asp:Button ID="btnSaveBottom" runat="server" Width="100px" CssClass="StandardButton" Text="Save" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>

<%--    <script>
        openMessage();
    </script>--%>



</asp:Content>
