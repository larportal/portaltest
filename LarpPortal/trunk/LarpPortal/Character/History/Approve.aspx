<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="LarpPortal.Character.History.Approve" MasterPageFile="~/MemberCampaigns.master" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ContentPlaceHolderID="MemberScripts" runat="server" ID="Scripts">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
        function closeMessage() {
            $('#modelMessage').hide();
        }
    </script>

    <script src="../../Scripts/jquery-1.11.3.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="CharacterHistoryList" runat="server">
    <style type="text/css">
        .div1 {
            border: 1px solid black;
        }

        th, tr:nth-child(even) > td {
            background-color: transparent;
        }

        @media (min-width: 768px) {
            .modal-xl {
                width: 90%;
                max-width: 1200px;
            }
        }
        editArea {
            overflow: scroll;
            height: 425px;
        }
    </style>
    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row col-lg-12" style="padding-left: 15px; padding-top: 10px;">
                <table class="col-lg-12">
                    <tr>
                        <td style="width: 80%;">
                            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character History" />
                        </td>
                        <td rowspan="4" style="width: 20%; text-align: right;">
                            <asp:Image ID="imgPicture" runat="server" Width="100px" Height="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEditMessage" runat="server" Font-Size="18px" Style="font-weight: 500" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCharacterInfo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
            </div>
            <div id="divQuestions" runat="server" style="max-height: 500px; overflow-y: auto; margin-right: 10px;">
                <asp:Repeater ID="rptAddendum" runat="server" OnItemCommand="rptAddendum_ItemCommand" OnItemDataBound="rptAddendum_ItemDataBound">
                    <ItemTemplate>
                        <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
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
                            <div style="padding-top: 5px;">
                                <asp:UpdatePanel ID="upComments" runat="server">
                                    <ContentTemplate>
                                        <table>
                                            <tr style="vertical-align: top;">
                                                <td>
                                                    <asp:Button ID="btnAddStaffComment" runat="server" Text="Add Staff Only Comment" CommandName="EnterComment"
                                                        CommandArgument='<%# Eval("PELsAddendumID") %>' CssClass="StandardButton" Style="height: 20px;" /></td>
                                                <td>
                                                    <asp:Panel ID="pnlStaffCommentSection" runat="server" Visible="false" Style="vertical-align: top;">
                                                        <table>
                                                            <tr style="vertical-align: top;">
                                                                <td>
                                                                    <asp:Image ID="imgStaffCommentProfilePicture" runat="server" Width="75" Height="75" /></td>
                                                                <td>
                                                                    <asp:TextBox ID="tbNewStaffCommentAddendum" runat="server" TextMode="MultiLine" Rows="4" Columns="80" /></td>
                                                                <td>
                                                                    <asp:Button ID="btnSaveNewStaffComment" runat="server" Text="Save" CssClass="StandardButton" Width="100" Height="20px"
                                                                        CommandName="AddComment" CommandArgument='<%# Eval("PELsAddendumID") %>' /></td>
                                                                <td>
                                                                    <asp:Button ID="btnCancelStaffComment" runat="server" Text="Cancel" CssClass="StandardButton" Width="100" Height="20px"
                                                                        CommandName="CancelComment" /></td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:DataList ID="dlStaffComments" runat="server" AlternatingItemStyle-BackColor="linen">
                                                        <HeaderTemplate>
                                                            <table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr style="vertical-align: top;" class="panel-body panel-container search-criteria">
                                                                <td class="LeftRightPadding" style="padding-bottom: 5px;">
                                                                    <asp:Image runat="server" Width="50" Height="50" ImageUrl='<%# Eval("UserPhoto") %>' /></td>
                                                                <td class="LeftRightPadding" style="padding-bottom: 5px;">
                                                                    <asp:Label runat="server" Text='<%# Eval("UserName") %>' Font-Bold="true" /></td>
                                                                <td class="LeftRightPadding" style="padding-bottom: 5px;">
                                                                    <asp:Label runat="server" Text='<%# Eval("DateAdded") %>' /></td>
                                                                <td class="LeftRightPadding" style="padding-bottom: 5px;">
                                                                    <asp:Label runat="server" Text='<%# Eval("StaffComments") %>' /></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:DataList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>

                <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Character History</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblHistory" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="padding-top: 5px;">
                        <asp:UpdatePanel ID="upComments" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr style="vertical-align: top;">
                                        <td>
                                            <asp:Button ID="btnAddComment" runat="server" Text="Add Staff Only Comment" OnClick="btnAddComment_Click" CssClass="StandardButton" Style="height: 20px;" /></td>
                                        <td>
                                            <asp:Panel ID="pnlCommentSection" runat="server" Visible="false" Style="vertical-align: top;">
                                                <table>
                                                    <tr style="vertical-align: top;">
                                                        <td>
                                                            <asp:Image ID="imgStaffPicture" runat="server" Width="75" Height="75" /></td>
                                                        <td>
                                                            <asp:TextBox ID="tbNewComment" runat="server" TextMode="MultiLine" Rows="4" Columns="80" /></td>
                                                        <td>
                                                            <asp:Button ID="btnSaveNewComment" runat="server" Text="Save" CssClass="StandardButton" Width="100" Height="20px" OnClick="btnSaveNewComment_Click" /></td>
                                                        <td>
                                                            <asp:Button ID="btnCancelComment" runat="server" Text="Cancel" CssClass="StandardButton" Width="100" Height="20px" OnClick="btnCancelComment_Click" /></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:DataList ID="dlComments" runat="server" AlternatingItemStyle-BackColor="linen">
                                                <HeaderTemplate>
                                                    <table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr style="vertical-align: top;" class="panel-body panel-container search-criteria">
                                                        <%--                                                        <td class="LeftRightPadding" style="padding-bottom: 5px;">
                                                            <asp:Image runat="server" Width="50" Height="50" ImageUrl='<%# Eval("UserPhoto") %>' /></td>--%>
                                                        <td class="LeftRightPadding" style="padding-bottom: 5px;">
                                                            <asp:Label runat="server" Text='<%# Eval("UserName") %>' Font-Bold="true" /></td>
                                                        <td class="LeftRightPadding" style="padding-bottom: 5px;">
                                                            <asp:Label runat="server" Text='<%# Eval("DateAdded") %>' /></td>
                                                        <td class="LeftRightPadding" style="padding-bottom: 5px;">
                                                            <asp:Label runat="server" Text='<%# Eval("StaffComments") %>' /></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <br />

            <asp:Panel ID="pnlStaffComments" runat="server" Visible="true">
                <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>CP Award</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <asp:MultiView ID="mvCPAwarded" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="vwCPAwardedEntry" runat="server">
                                            <table>
                                                <tr>
                                                    <td>For completing this history, the person should be awarded </td>
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
            </asp:Panel>

            <div class="row col-sm-12" style="padding-left: 15px; margin-bottom: 20px; width: 100%; padding-right: 0px;">
                <div class="col-sm-4">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="StandardButton" Width="150px" OnClick="btnCancel_Click" />
                </div>
                <div class="col-sm-8" style="text-align: right;">
                    <asp:Button ID="btnReject" runat="server" Text="Needs Revision" ToolTip="By clicking this it will go back to the user for revisions."
                        CssClass="StandardButton LeftRightPadding" Width="150px" OnClick="btnReject_Click" />
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="StandardButton NoRightPadding" Width="150px" OnClick="btnApprove_Click" />
                    <asp:Button ID="btnDone" runat="server" Text="Done" CssClass="StandardButton NoRightPadding" Width="150px" OnClick="btnDone_Click" />
                </div>
            </div>
            <br />
            <br />
        </div>
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog modal-xl"">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character History Needs Revisions
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <CKEditor:CKEditorControl ID="ckHistory" runat="server" Height="410px" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSendMessage" runat="server" Text="Send Message" Width="150px" CssClass="StandardButton" OnClick="btnSendMessage_Click" />
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidCampaignID" runat="server" />
    <asp:HiddenField ID="hidCharacterAKA" runat="server" />
    <asp:HiddenField ID="hidEventID" runat="server" />
    <asp:HiddenField ID="hidCharacterID" runat="server" />
    <asp:HiddenField ID="hidEmail" runat="server" />
    <asp:HiddenField ID="hidNotificationEMail" runat="server" />
    <asp:HiddenField ID="hidCampaignCPOpportunityDefaultID" runat="server" />
    <asp:HiddenField ID="hidCampaignPlayerID" runat="server" />
    <asp:HiddenField ID="hidEventDesc" runat="server" />
    <asp:HiddenField ID="hidEventDate" runat="server" />
    <asp:HiddenField ID="hidPlayerName" runat="server" />
    <asp:HiddenField ID="hidSubmitDate" runat="server" />
    <asp:HiddenField ID="hidCampaignName" runat="server" />
</asp:Content>
