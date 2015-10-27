<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PELApprove.aspx.cs" Inherits="LarpPortal.PELs.PELApprove" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">
    <style type="text/css">
        .div1
        {
            border: 1px solid black;
        }

        th, tr:nth-child(even) > td
        {
            background-color: transparent;
        }
    </style>
    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row col-lg-12" style="padding-left: 15px; padding-top: 10px;">
                <table class="col-lg-12">
                    <tr>
                        <td style="width: 80%;">
                            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="PEL (Post Event Letter)" />
                        </td>
                        <td rowspan="3" style="width: 20%; text-align: right;">
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
                            <asp:Label ID="lblEventInfo" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
            </div>
            <asp:HiddenField ID="hidRegistrationID" runat="server" />
            <asp:HiddenField ID="hidPELID" runat="server" />
            <div id="divQuestions" runat="server" style="max-height: 500px; overflow-y: auto; margin-right: 10px;">
                <asp:Repeater ID="rptQuestions" runat="server" OnItemCommand="rptQuestions_ItemCommand" OnItemDataBound="rptQuestions_ItemDataBound">
                    <ItemTemplate>
                        <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="panelheader">
                                    <h2>Question: <%# Eval("Question") %></h2>
                                    <div class="panel-body">
                                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                            <asp:Label ID="lblAnswer" runat="server" Text='<%# Eval("Answer") %>' />
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
                                                    <asp:Button ID="btnAddComment" runat="server" Text="Add Staff Only Comment" CommandName="EnterComment" CommandArgument='<%# Eval("PELAnswerID") %>' CssClass="StandardButton" Style="height: 20px;" /></td>
                                                <td>
                                                    <asp:Panel ID="pnlCommentSection" runat="server" Visible="false" Style="vertical-align: top;">
                                                        <table>
                                                            <tr style="vertical-align: top;">
                                                                <td>
                                                                    <asp:Image ID="imgProfilePicture" runat="server" Width="75" Height="75" /></td>
                                                                <td>
                                                                    <asp:TextBox ID="tbNewComment" runat="server" TextMode="MultiLine" Rows="4" Columns="80" /></td>
                                                                <td>
                                                                    <asp:Button ID="btnSaveNewComment" runat="server" Text="Save" CssClass="StandardButton" Width="100" Height="20px"
                                                                        CommandName="AddComment" CommandArgument='<%# Eval("PELAnswerID") %>' /></td>
                                                                <td>
                                                                    <asp:Button ID="btnCancelComment" runat="server" Text="Cancel" CssClass="StandardButton" Width="100" Height="20px"
                                                                        CommandName="CancelComment" /></td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:DataList ID="dlComments" runat="server" AlternatingItemStyle-BackColor="linen">
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
                        <asp:HiddenField ID="hidQuestionID" runat="server" Value='<%# Eval("PELQuestionID") %>' />
                        <asp:HiddenField ID="hidAnswerID" runat="server" Value='<%# Eval("PELAnswerID") %>' />
                    </ItemTemplate>
                </asp:Repeater>
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
                                                    <td>For completing this PEL, the person should be awarded </td>
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

            <div class="row col-sm-12" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                <div class="col-sm-4">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="StandardButton" Width="150px" OnClick="btnCancel_Click" />
                </div>
                <div class="col-sm-8" style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Approve" OnCommand="ProcessButton" CommandName="Approve" CssClass="StandardButton" Width="150px" />
                </div>
            </div>
            <br />
            <br />
        </div>
    </div>

    <asp:HiddenField ID="hidCampaignCPOpportunityDefaultID" runat="server" />
    <asp:HiddenField ID="hidReasonID" runat="server" />
    <asp:HiddenField ID="hidCampaignPlayerID" runat="server" />
    <asp:HiddenField ID="hidCharacterID" runat="server" />
    <asp:HiddenField ID="hidCampaignID" runat="server" />
    <asp:HiddenField ID="hidCharacterAKA" runat="server" />
    <asp:HiddenField ID="hidEventID" runat="server" />
    <asp:HiddenField ID="hidEventDesc" runat="server" />
    <asp:HiddenField ID="hidPELNotificationEMail" runat="server" />
    <asp:HiddenField ID="hidEventDate" runat="server" />
    <asp:HiddenField ID="hidPlayerName" runat="server" />
</asp:Content>
