<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PELEdit.aspx.cs" Inherits="LarpPortal.PELs.PELEdit" MasterPageFile="~/MemberCampaigns.master" %>

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
        <asp:Timer ID="Timer1" runat="server" Interval="300000" OnTick="Timer1_Tick"></asp:Timer>
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row col-lg-12" style="padding-left: 15px; padding-top: 10px;">
                <table class="col-lg-12">
                    <tr>
                        <td style="width: 80%;" colspan="2">
                            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="PEL (Post Event Letter)" />
                        </td>
                        <td rowspan="3" style="width: 20%; text-align: right;">
                            <asp:Image ID="imgPicture" runat="server" Width="100px" Height="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">
                            <asp:Label ID="lblEditMessage" runat="server" Font-Size="18px" Style="font-weight: 500" Visible="false" />
                        </td>
                        <td style="width: 30%; text-align: center;" rowspan="2">
                            <asp:Panel ID="pnlSaveReminder" runat="server" Visible="true">
                                <strong>Note: </strong>Remember to click save to save your PEL and submit when it's complete to submit it to staff.
                                <asp:Button ID="btnTopSave" runat="server" Text="Save" OnCommand="ProcessButton" CommandName="Save" CssClass="StandardButton" Width="150px" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEventInfo" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
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
            <div id="divQuestions" runat="server" style="max-height: 500px; overflow-y: auto; margin-right: 10px;">
                <asp:Repeater ID="rptQuestions" runat="server">
                    <ItemTemplate>
                        <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="panelheader">
                                    <h2>Question:
                                        <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>' /></h2>
                                    <div class="panel-body">
                                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                            <asp:TextBox ID="tbAnswer" runat="server" Text='<%# Eval("Answer") %>' Columns="100" Style="width: 100%"
                                                BorderColor="black" BorderStyle="Solid" BorderWidth="1" TextMode="MultiLine" Rows="4"
                                                Visible="<%# TextBoxEnabled %>" />
                                            <asp:Label ID="lblAnswer" runat="server" Text='<%# Eval("Answer") %>' Visible="<%# !(TextBoxEnabled) %>" />
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
