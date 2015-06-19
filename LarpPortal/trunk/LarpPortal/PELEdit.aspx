<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PELEdit.aspx.cs" Inherits="LarpPortal.PELEdit" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">
    <style type="text/css">
        .div1
        {
            border: 1px solid black;
        }
    </style>
    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="PEL (Post Event Letter)" />
            </div>
            <asp:HiddenField ID="hidRegistrationID" runat="server" />
            <asp:HiddenField ID="hidPELID" runat="server" />
            <asp:Repeater ID="rptQuestions" runat="server">
                <ItemTemplate>
                    <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                        <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="panelheader">
                                <h2>Question: <%# Eval("Question") %></h2>
                                <div class="panel-body">
                                    <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                        <asp:TextBox ID="tbAnswer" runat="server" Text='<%# Eval("Answer") %>' Columns="100" Style="width: 100%"
                                            BorderColor="black" BorderStyle="Solid" BorderWidth="1" TextMode="MultiLine" Rows="4" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hidQuestionID" runat="server" Value='<%# Eval("PELQuestionID") %>' />
                    <asp:HiddenField ID="hidAnswerID" runat="server" Value='<%# Eval("PELAnswerID") %>' />
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <div class="col-sm-12" style="text-align: right;">
                <asp:Button ID="btnSubmit" runat="server" Text="Save And Submit" OnCommand="ProcessButton" CommandName="Submit" CssClass="StandardButton" Width="150px" />
                <asp:Image ID="imgSpacer" runat="server" ImageUrl="~/img/blank.gif" Height="0" Width="25" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnCommand="ProcessButton" CommandName="Save" CssClass="StandardButton" Width="150px" />
            </div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
