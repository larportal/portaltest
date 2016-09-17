<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTeam.aspx.cs" Inherits="LarpPortal.Character.Teams.CreateTeam" MasterPageFile="~/Character/Character.Master" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

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
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="PELList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row col-lg-12" style="padding-left: 15px; padding-top: 10px;">
                <div class="col-lg-10" style="padding-left: 0px;">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Teams" />
                </div>
            </div>

            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
                <div class="row col-lg-12">
                    <b>Selected Character: </b>
                    <asp:DropDownList ID="ddlCharacterSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacterSelector_SelectedIndexChanged" />
                </div>
            </div>

            <div class="row col-lg-12" style="padding-left: 15px; margin-bottom: 20px;">
                <div class="panel col-lg-6" style="padding-top: 0px; padding-bottom: 0px; padding-left: 0px; padding-right: 0px;">
                    <div class="panelheader">
                        <h2>Current Teams</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria" style="padding-bottom: 10px; height: 500px; overflow-y: scroll;">
                                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover table-condensed">
                                    <Columns>
                                        <asp:BoundField DataField="TeamName" HeaderText="Team Name" />
                                    </Columns>
                                    <EmptyDataRowStyle ForeColor="Red" Font-Size="X-Large" />
                                    <EmptyDataTemplate>
                                        There are no teams for that campaign.
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-0-5">&nbsp;</div>
                <div class="col-lg-5 text-right">
                    <div class="row col-lg-12">
                        <asp:TextBox ID="tbNewTeamName" runat="server" CssClass="form-control TableTextBox col-lg-12" Style="padding-left: 10px; padding-right: 10px;" />
                    </div>
                    <div class="row col-lg-12 text-right" style="padding-top: 20px;">
                        <asp:Button ID="btnCreateTeam" runat="server" Width="150" Text="Create Team" CssClass="StandardButton" OnClick="btnCreateTeam_Click" />
                    </div>
                    <div class="row col-lg-12 text-center" style="padding-top: 40px;">
                        <asp:RequiredFieldValidator ID="rfvNewTeamName" runat="server" Font-Size="20pt" Font-Bold="true" Font-Italic="true" 
                            ControlToValidate="tbNewTeamName" Text="You must enter a team name" Display="Dynamic" />
                        <asp:Label ID="lblAlreadyExists" runat="server" Text="That team already exists" Font-Size="20px" Visible="false" />
                    </div>
                </div>
            </div>
        </div>
        <br />
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
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnCloseMessage_Click" />
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidNotificationEMail" runat="server" Value="" />
    <asp:HiddenField ID="hidCampaignID" runat="server" Value="" />
</asp:Content>
