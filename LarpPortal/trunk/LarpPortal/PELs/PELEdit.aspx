<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PELEdit.aspx.cs" Inherits="LarpPortal.PELs.PELEdit" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">
    <style type="text/css">
        .div1
        {
            border: 1px solid black;
        }
    </style>
    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row" style="padding-left: 15px; padding-top: 10px;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="PEL (Post Event Letter)" />
                <asp:Label ID="lblEditMessage" runat="server" Font-Size="18px" Style="font-weight: 500" Visible="false" />
            </div>
            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
                <asp:Label ID="lblEventInfo" runat="server" />
            </div>
            <asp:HiddenField ID="hidRegistrationID" runat="server" />
            <asp:HiddenField ID="hidPELID" runat="server" />
            <asp:HiddenField ID="hidTextBoxEnabled" runat="server" Value="1" />
            <div id="divQuestions" runat="server" style="max-height: 500px; overflow-y: auto; margin-right: 10px;" >
                <asp:Repeater ID="rptQuestions" runat="server">
                    <ItemTemplate>
                        <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="panelheader">
                                    <h2>Question: <%# Eval("Question") %></h2>
                                    <div class="panel-body">
                                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                            <asp:TextBox ID="tbAnswer" runat="server" Text='<%# Eval("Answer") %>' Columns="100" Style="width: 100%"
                                                BorderColor="black" BorderStyle="Solid" BorderWidth="1" TextMode="MultiLine" Rows="4"
                                                Visible="<%# TextBoxEnabled %>" />
                                            <asp:Label ID="lblAnswer" runat="server" Text='<%# Eval("Answer") %>' Enabled="<%# !(TextBoxEnabled) %>" />
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
            <br />

            <asp:Panel ID="pnlStaffComments" runat="server" Visible="false">
                <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Staff Comments</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <asp:TextBox ID="tbStaffComment" runat="server" Columns="100" Style="width: 100%"
                                        BorderColor="black" BorderStyle="Solid" BorderWidth="1" TextMode="MultiLine" Rows="4" />
                                    <asp:Label ID="lblStaffComment" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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
                                                    <td style="padding-left: 10px; padding-right: 10px;"><asp:TextBox ID="tbCPAwarded" runat="server" Columns="6" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Text="5.0" /></td>
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
                    <asp:Button ID="btnSubmit" runat="server" Text="Save And Submit" OnCommand="ProcessButton" CommandName="Submit" CssClass="StandardButton" Width="150px" />
                    <asp:Image ID="imgSpacer" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnCommand="ProcessButton" CommandName="Save" CssClass="StandardButton" Width="150px" />
                </div>
            </div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
