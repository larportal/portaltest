<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharHistory.aspx.cs" Inherits="LarpPortal.Character.CharHistory" EnableViewState="true" %>
<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>

<asp:Content ID="Scripts" ContentPlaceHolderID="CharHeaderScripts" runat="server">

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function closeModal() {
            $('#myModal').hide();
        }
    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CharHeaderMain" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character History" />
        </div>

        <div class="row col-sm-12" style="padding-bottom: 0px; padding-left: 15px; padding-right: 0px;">    <%-- border: 1px solid black;">--%>
<%--            <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">--%>
            <CharSelecter:CSelect ID="oCharSelect" runat="server" />
<%--            </div>
            <div class="col-sm-2">
                Here.
            </div>--%>
        </div>

        <div class="row" style="padding-left: 15px; margin-bottom: 20px">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>History</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                            <asp:TextBox ID="tbHistory" runat="server" TextMode="MultiLine" Rows="10" Width="100%" />
                            <asp:Label ID="lblHistory" runat="server" Width="100%" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:MultiView ID="mvButtons" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwButtons" runat="server">
                <div class="row col-sm-12" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                    <div class="col-sm-4">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="StandardButton" Width="150px" OnClick="btnCancel_Click" />
                    </div>
                    <div class="col-sm-8" style="text-align: right;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Save And Submit" CssClass="StandardButton" Width="150px" OnCommand="btnSubmitOrSave_Command" CommandName="SUBMIT" />
                        <asp:Image ID="imgSpacer" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="StandardButton" Width="150px" OnCommand="btnSubmitOrSave_Command" CommandName="SAVE" />
                    </div>
                </div>
            </asp:View>
            <asp:View ID="vwAlreadySubmitted" runat="server">
                <div class="row col-sm-12 text-center" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                    <br />
                    <strong>The history has been submitted. It cannot be changed.</strong>
                </div>
            </asp:View>
            <asp:View ID="vwAlreadyApproved" runat="server">
                <div class="row col-sm-12 text-center" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                    <br />
                    <strong>The history has been approved. It cannot be changed.</strong>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>

    <div class="modal" id="myModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Character History
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblCharHistoryMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnClose_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
