<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAddendum.aspx.cs" Inherits="LarpPortal.Character.History.AddAddendum" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">
    <style type="text/css">
        .div1 {
            border: 1px solid black;
        }

        th, tr:nth-child(even) > td {
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
<%--            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
            </div>--%>
            <asp:HiddenField ID="hidRegistrationID" runat="server" />
            <asp:HiddenField ID="hidPELID" runat="server" />

            <div id="div1" runat="server" style="overflow-y: auto; margin-right: 10px;" class="row col-sm-12">
                <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Addendum</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <asp:TextBox ID="tbAddendum" runat="server" TextMode="MultiLine" Columns="100" Style="width: 100%"
                                        BorderColor="black" BorderStyle="Solid" BorderWidth="1" Rows="4" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row col-sm-12" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                <div class="col-sm-4">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="StandardButton" Width="150px" OnClick="btnCancel_Click" />
                </div>
                <div class="col-sm-4 text-center">
                    <b>Everything below is read only.</b>
                </div>
                <div class="col-sm-4" style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="StandardButton" Width="150px" OnClick="btnSave_Click" />
                </div>
            </div>

            <div class="row col-sm-12"><hr /></div>
            <br />
            <br />

            <div id="divQuestions" runat="server" style="max-height: 400px; overflow-y: auto; margin-right: 10px;" class="row col-sm-12">
                <asp:Repeater ID="rptAddendum" runat="server">
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
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Repeater ID="rptQuestions" runat="server">
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
                        </div>
                        <asp:HiddenField ID="hidQuestionID" runat="server" Value='<%# Eval("PELQuestionID") %>' />
                        <asp:HiddenField ID="hidAnswerID" runat="server" Value='<%# Eval("PELAnswerID") %>' />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
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
    <asp:HiddenField ID="hidSubmitDate" runat="server" />
</asp:Content>
