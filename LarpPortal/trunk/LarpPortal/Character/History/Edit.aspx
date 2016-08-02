﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="LarpPortal.Character.History.Edit" MasterPageFile="~/Character/Character.Master" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="HistoryScripts" runat="server" ContentPlaceHolderID="CharHeaderScripts">
</asp:Content>


<asp:Content ID="HistoryStyles" runat="server" ContentPlaceHolderID="CharHeaderStyles">
    <style type="text/css">
        editArea {
            overflow: scroll;
            height: 425px;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="PELList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <asp:Timer ID="Timer1" runat="server" Interval="300000" OnTick="Timer1_Tick"></asp:Timer>
        <div id="character-info" class="character-info tab-pane active col-sm-12">
            <div class="row col-lg-12" style="padding-left: 15px; padding-top: 10px;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character History" />
                <%--                    <tr>
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
                </table>--%>
            </div>
            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
            </div>
            <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
                <table>
                    <tr style="vertical-align: middle;">
                        <td style="width: 10px"></td>
                        <td>
                            <b>Selected Character:</b>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:DropDownList ID="ddlCharacterSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacterSelector_SelectedIndexChanged" />
                        </td>
<%--                        <td style="padding-left: 20px;">
                            <b>Campaign:</b>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Label ID="lblCampaign" runat="server" Text="" />
                        </td>
                        <td style="padding-left: 20px;">
                            <b>Last Update:</b>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Label ID="lblUpdateDate" runat="server" />
                        </td>--%>
                    </tr>
                </table>
            </div>

            <asp:UpdatePanel ID="upAutoSave" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hidRegistrationID" runat="server" />
                    <asp:HiddenField ID="hidPELID" runat="server" />
                    <asp:HiddenField ID="hidPELTemplateID" runat="server" />
                    <asp:HiddenField ID="hidTextBoxEnabled" runat="server" Value="1" />
                    <asp:HiddenField ID="hidAutoSaveText" runat="server" />
                    <asp:Label ID="lblSaveMessage" runat="server" Text="Starting...." />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>

            <div id="divHistory" runat="server" style="max-height: 500px; overflow-y: auto; margin-right: 10px;">
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
            </div>

            <div class="row" style="padding-left: 15px; margin-bottom: 20px; width: 100%;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2>Character History</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria" style="padding-bottom: 10px; height: 500px;">
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
</asp:Content>