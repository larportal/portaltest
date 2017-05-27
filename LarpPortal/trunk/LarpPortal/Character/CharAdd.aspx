<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharAdd.aspx.cs" Inherits="LarpPortal.Character.CharAdd" %>

<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>

<asp:Content ID="ScriptSection" runat="server" ContentPlaceHolderID="CharHeaderScripts">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
        function closeMessage() {
            $('#modalMessage').hide();
        }
    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CharHeaderMain" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active">
            <asp:MultiView ID="mvCharacterCreate" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwCreateCharacter" runat="server">
                    <div class="form-horizontal">
                        <div class="col-sm-12">
                            <div class="row">
                                <h1 class="col-sm-12">Create New Character</h1>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-3 text-right NoRightPadding">
                                    <label for="ddlCampaign" class="control-label">Campaign</label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlUserCampaigns" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserCampaigns_SelectedIndexChanged" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12" runat="server" id="divPlayerType">
                            <div class="row">
                                <div class="col-sm-3 text-right NoRightPadding">
                                    <label for="ddlCharacterType" class="control-label">Character Type</label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlCharacterType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacterType_SelectedIndexChanged">
                                        <asp:ListItem Text="PC" Value="1" Selected="true" />
                                        <asp:ListItem Text="NPC" Value="2" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12" runat="server" id="divPlayer">
                            <div class="row">
                                <div class="col-sm-3 text-right NoRightPadding">
                                    <label for="ddlPlayer" class="control-label">Player</label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlPlayer" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="rfvddlPlayer" runat="server" ControlToValidate="ddlPlayer" InitialValue=""
                                        ErrorMessage="* Required" Font-Bold="true" Font-Italic="true" ForeColor="Red" Display="Dynamic" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-3 text-right NoRightPadding">
                                    <label for="tbCharacterName" class="control-label">Character Name</label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbCharacterName" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="rfvCharacterName" runat="server" ControlToValidate="tbCharacterName" InitialValue=""
                                        ErrorMessage="* Required" Font-Bold="true" Font-Italic="true" ForeColor="Red" Display="Dynamic" Font-Size="larger" />
                                </div>
                            </div>
                        </div>

                        <div class="row">&nbsp;</div>
                        <div class="row">&nbsp;</div>
                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-3">
                                    &nbsp;
                                </div>
                                <div class="col-sm-4 text-center" style="font-size: larger">
                                    The character name is the name by which your character is commonly known. You will be able 
                                    to enter a different first, middle and last name after saving this screen. 
                                </div>
                            </div>
                        </div>

                        <div class="row">&nbsp;</div>
                        <div class="row">&nbsp;</div>

                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-8">&nbsp;</div>
                                <div class="col-sm-2 NoRightPadding">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="StandardButton" Width="100px" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vwNoCampaigns" runat="server">
                    <br />
                    <br />
                    <br />
                    <div class="form-horizontal">
                        <div class="col-sm-10">
                            <div class="row">
                                <h1 class="col-sm-12 text-center">You are currently not a member of any campaigns.</h1>
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <div class="row">
                                <h1 class="col-sm-12 text-center">You cannot create a character until you have joined a campaign.</h1>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="row">
                                <h1 class="col-sm-12 text-center">Click <a href="../PublicCampaigns.aspx" style="background-color: white;">here</a> to request to join a campaign.</h1>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Info
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblmodalMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseError" runat="server" Text="Close" Width="150px" CssClass="StandardButton" />
                </div>
            </div>
        </div>
    </div>

    <CharSelecter:CSelect ID="oCharSelect" runat="server" Visible="false" />

</asp:Content>
