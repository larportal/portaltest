<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharUserDefined.aspx.cs" Inherits="LarpPortal.Character.CharUserDefined" EnableViewState="true" %>

<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>

<asp:Content ID="Scripts" ContentPlaceHolderID="CharHeaderScripts" runat="server">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CharHeaderMain" runat="server">

    <div class="mainContent tab-content col-lg-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Goals & Preferences" />
        </div>

        <div class="row col-sm-12" style="padding-bottom: 0px; padding-left: 15px; padding-right: 0px; margin-right: -15px;">
            <%-- border: 1px solid black;">--%>
            <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">
                <CharSelecter:CSelect ID="oCharSelect" runat="server" />
            </div>
            <div class="col-sm-2 text-right" style="padding-right: 0px;">
                <asp:Button ID="btnSaveTop" runat="server" CssClass="StandardButton" Text="Save" Width="100px" OnClick="btnSubmit_Click" />
            </div>
        </div>

        <br />
        <div id="divUserDef1" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px; padding-top: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>
                        <asp:Label ID="lblUserDef1" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField1" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                            <asp:Label ID="lblUserField1" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divUserDef2" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>
                        <asp:Label ID="lblUserDef2" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField2" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                            <asp:Label ID="lblUserField2" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divUserDef3" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>
                        <asp:Label ID="lblUserDef3" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField3" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                            <asp:Label ID="lblUserField3" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divUserDef4" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>
                        <asp:Label ID="lblUserDef4" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField4" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                            <asp:Label ID="lblUserField4" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divUserDef5" runat="server" class="row" style="padding-left: 15px; margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>
                        <asp:Label ID="lblUserDef5" runat="server" /></h2>
                    <div class="panel-body" style="height: 100px">
                        <div class="panel-container search-criteria">
                            <asp:TextBox ID="tbUserField5" runat="server" Style="width: 100%" Rows="4" TextMode="MultiLine" />
                            <asp:Label ID="lblUserField5" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="padding-left: 15px; margin-bottom: 20px; text-align: right;">
            <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Text="Save" Width="100px" OnClick="btnSubmit_Click" />
        </div>
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Items
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
