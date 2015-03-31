<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharAdd.aspx.cs" Inherits="LarpPortal.Character.CharAdd" %>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="CharHeaderStyles">
    <style type="text/css">
        .NoRightPadding
        {
            padding-right: 0px;
        }

        .NoLeftPadding
        {
            padding-left: 0px;
        }

        [disabled]
        { /* Text and background colour, medium red on light yellow */
            color: #933;
            background-color: #ffc;
        }

        .control-label
        {
            text-align: right;
        }

        .WithBorder
        {
            border: 1px solid black;
        }

        /*div, label
        {
            border: 1px solid black;
        }*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active">
            <asp:MultiView ID="mvCharacterCreate" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwCreateCharacter" runat="server">
                    <div class="form-horizontal">
                        <div class="col-sm-12">
                            <div class="row">
                                <h1 class="col-sm-12">Create New Character</h1>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-3 text-right NoRightPadding">
                                    <label for="ddlCampaign" class="control-label">Campaign</label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlUserCampaigns" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-3 text-right NoRightPadding">
                                    <label for="tbCharacterName" class="control-label">Character Name</label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbCharacterName" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="row">&nbsp;</div>
                        <div class="row">&nbsp;</div>
                        <div class="row">&nbsp;</div>
                        <div class="row">&nbsp;</div>

                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-10">&nbsp;</div>
                                <div class="col-sm-2 NoRightPadding">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="StandardButton" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vwNoCampaigns" runat="server">
                    <br />
                    <br />
                    <br />
                    <div class="form-horizontal">
                        <div class="col-sm-10">
                            <div class="row">
                                <h1 class="col-sm-12 text-center">You are currently not a member of any campaigns.</h1>
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <div class="row">
                                <h1 class="col-sm-12 text-center">You cannot create a character until you have joined a campaign.</h1>
                            </div>
                        </div>

                        <div class="col-sm-10">
                            <div class="row">
                                <h1 class="col-sm-12 text-center">Click <a href="../PublicCampaigns.aspx" style="background-color: white;">here</a> to request to join a campaign.</h1>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
