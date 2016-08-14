<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="LarpPortal.Character.History.Edit" MasterPageFile="~/Character/Character.Master" %>

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
        <asp:Timer ID="Timer1" runat="server" Interval="300000" OnTick="Timer1_Tick"></asp:Timer>
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row col-lg-12" style="padding-left: 15px; padding-top: 10px;">
                <div class="col-lg-10" style="padding-left: 0px;">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character History" />
                </div>
                <div class="col-lg-2" style="vertical-align: middle;">
                    <%--Don't take out the text below. It makes it so the header and the button line up correctly.--%>
                    <span style="font-size: 24px; visibility: hidden;">Hidden</span>
                    <asp:Button ID="btnAddAddendum" runat="server" Text="Add Addendum" CssClass="StandardButton" Width="125" OnClientClick="showNewAddendum(); return false;" />
                </div>
            </div>

            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
                <div class="row col-lg-12">
                    <b>Selected Character: </b>
                    <asp:DropDownList ID="ddlCharacterSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacterSelector_SelectedIndexChanged" />
                </div>
            </div>

            <asp:UpdatePanel ID="upAutoSave" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hidRegistrationID" runat="server" />
                    <asp:HiddenField ID="hidPELID" runat="server" />
                    <asp:HiddenField ID="hidPELTemplateID" runat="server" />
                    <asp:HiddenField ID="hidTextBoxEnabled" runat="server" Value="1" />
                    <asp:HiddenField ID="hidAutoSaveText" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>

            <div id="divNewAddendum" class="col-sm-12" style="padding-left: 15px; margin-bottom: 10px; display: none; padding-right: 45px;">
                <div class="panel row" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2>Addendum</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                <CKEditor:CKEditorControl ID="CKEAddendum" BasePath="/ckeditor/" runat="server" Height="100px"></CKEditor:CKEditorControl>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px;">
                    <div class="col-lg-6">
                        <asp:Button ID="btnCancelAddendum" runat="server" Text="Cancel" CssClass="StandardButton" Width="100" Height="20px" OnClientClick="hideNewAddendum(); return false;" />
                    </div>
                    <div class="col-lg-6 text-right">
                        <asp:Button ID="btnSaveAddendum" runat="server" Text="Save" CssClass="StandardButton" Width="100" Height="20px" OnClick="btnSaveAddendum_Click" />
                    </div>
                </div>
            </div>

            <div class="row">&nbsp;</div>
            <asp:Repeater ID="rptAddendum" runat="server">
                <ItemTemplate>
                    <div class="row" style="padding-left: 15px; padding-right: 15px; margin-bottom: 0px; width: 100%;">
                        <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="panelheader">
                                <h2><%# Eval("Title") %></h2>
                                <div class="panel-body">
                                    <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblAddendum" runat="server" Text='<%# Eval("Addendum") %>' />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </ItemTemplate>
            </asp:Repeater>

            <div class="row col-lg-12" style="padding-left: 15px; margin-bottom: 20px;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2>Character History</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria" style="padding-bottom: 10px; height: 500px; overflow-y: scroll;">
                                <div>
                                    <CKEditor:CKEditorControl ID="ckEditor" BasePath="/ckeditor/" runat="server" Height="410px"></CKEditor:CKEditorControl>
                                    <asp:Label ID="lblHistory" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />

        <div class="row col-sm-12" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
            <div class="col-sm-12" style="text-align: right;">
                <asp:Button ID="btnSubmit" runat="server" Text="Save And Submit" OnCommand="ProcessButton" CommandName="Submit" CssClass="StandardButton" Width="150px" />
                <asp:Image ID="imgSpacer" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnCommand="ProcessButton" CommandName="Save" CssClass="StandardButton" Width="150px" />
            </div>
        </div>
        <br />
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

</asp:Content>
