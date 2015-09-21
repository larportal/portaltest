<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="CharHistoryApproval.aspx.cs" Inherits="LarpPortal.StaffFunctions.CharHistoryApproval"
    EnableViewState="true" %>

<asp:Content ID="Scripts" ContentPlaceHolderID="MemberScripts" runat="server">

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function closeModal() {
            $('#myModal').hide();
        }
    </script>

    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="Styles" ContentPlaceHolderID="MemberStyles" runat="server">
    <style type="text/css">
        div
        {
            border: 0px solid black;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MemberCampaignsContent" runat="server">

    <div class="mainContent tab-content col-sm-12" style="padding-left: 0px; padding-right: 0px;">
        <div class="row col-lg-12" style="padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: bold" Text="Character History Approval" />
        </div>
        <div class="row col-lg-12" style="padding-bottom: 10px; font-size: 16px;">
            <b>Selected Character:</b>
            <asp:Label ID="lblCharacter" runat="server" />
        </div>

        <div class="row col-lg-12" style="margin-bottom: 20px">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>History</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                            <asp:Label ID="lblHistory" runat="server" Width="100%" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row col-lg-12" style="margin-bottom: 20px;">
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                <div class="panelheader">
                    <h2>CP Award</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                            <asp:MultiView ID="mvCPAwarded" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vwCPAwardedEntry" runat="server">
                                    <table>
                                        <tr>
                                            <td>For completing this character history, the person should be awarded </td>
                                            <td style="padding-left: 10px; padding-right: 10px;">
                                                <asp:TextBox ID="tbCPAwarded" runat="server" Columns="6" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Text="0.0" /></td>
                                            <td>CP.</td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="vwCPAwardedDisplay" runat="server">
                                    <asp:Label ID="lblCPAwarded" runat="server" />
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row col-sm-12" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
            <div class="col-sm-4">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="StandardButton" Width="150px" PostBackUrl="~/StaffFunctions/CharHistoryApprovalList.aspx" />
            </div>
            <div class="col-sm-8" style="text-align: right;">
                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="StandardButton" Width="150px" OnCommand="btnApprove_Command" CommandName="APPROVE" />
            </div>
        </div>
    </div>

    <div class="modal" id="myModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Character History Approval
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblCharApprovedMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" PostBackUrl="~/StaffFunctions/CharHistoryApprovalList.aspx" CssClass="StandardButton" />
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidCampaignCPOpportunityDefaultID" runat="server" Value="" />
    <asp:HiddenField ID="hidCharacterName" runat="server" />
    <asp:HiddenField ID="hidPlayerName" runat="server" />
    <asp:HiddenField ID="hidPlayerEMail" runat="server" />
</asp:Content>
