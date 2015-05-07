<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharInfo.aspx.cs" Inherits="LarpPortal.Character.CharInfo" %>

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
        .ReadOnly
        {
            border-width: 0px 0px 0px 0px;
            border-color: transparent;
            border-style: none;
            background-color: lightgray;
            color: darkgray;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active">
            <div class="col-sm-12">
                <div class="row">
                    <h1 class="col-sm-10">
                        <asp:Label ID="lblHeader" runat="server" /></h1>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-10">
                        <asp:Label ID="lblHelp" runat="server" Text="Fill in Information to describe your character. Some items are automatically updated after events." />
                    </div>
                </div>
            </div>

            <div class="form-horizontal">
                <div class="col-sm-9">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-1 text-right NoRightPadding">
                                <label for="tbCharacter" class="control-label">Character</label>
                            </div>
                            <div class="col-sm-1-5 NoRightPadding">
                                <asp:TextBox ID="tbFirstName" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-1 NoRightPadding NoLeftPadding">
                                <asp:TextBox ID="tbMiddleName" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-2 NoLeftPadding NoRightPadding">
                                <asp:TextBox ID="tbLastName" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-1 text-right NoLeftPadding NoRightPadding">
                                <label for="tbOrigin" class="control-label">Birthplace</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="tbOrigin" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-1 text-right NoLeftPadding NoRightPadding">
                                <label for="tbStatus" class="control-label">Status</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Visible="false" />
                                <asp:Label ID="lblStatus" runat="server" CssClass="form-control ReadOnly" />


                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-1 text-right NoRightPadding">
                                <label class="control-label text-right">AKA</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="tbAKA" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-0-5"></div>
                            <div class="col-sm-2 text-right NoLeftPadding NoRightPadding">
                                <label for="tbHome" class="control-label">Home</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="tbHome" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-1 text-right NoLeftPadding NoRightPadding">
                                <label for="tbDateLastEvent" class="control-label">Last Event</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="tbDateLastEvent" runat="server" CssClass="form-control" Visible="false" />
                                <asp:Label ID="lblDateLastEvent" runat="server" CssClass="form-control ReadOnly" />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-1 text-right NoRightPadding">
                                <label for="tbType" class="control-label">Type</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblType" runat="server" CssClass="form-control ReadOnly" />
                            </div>
                            <div class="col-sm-0-5"></div>
                            <div class="col-sm-2 text-right NoLeftPadding NoRightPadding">
                                <label for="tbTeam" class="control-label">Team</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="tbTeam" runat="server" CssClass="form-control" Visible="false" />
                                <asp:Label ID="lblTeam" runat="server" CssClass="form-control ReadOnly" />
                            </div>
                            <div class="col-sm-1 text-right NoLeftPadding NoRightPadding">
                                <label for="tbNumOfDeaths" class="control-label"># of Deaths</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="tbNumOfDeaths" runat="server" CssClass="form-control" ReadOnly="true" Visible="false" />
                                <asp:Label ID="lblNumOfDeaths" runat="server" CssClass="form-control ReadOnly" />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-1 text-right NoRightPadding">
                                <label for="tbDOB" class="control-label">DOB</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="tbDOB" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-0-5"></div>
                            <div class="col-sm-2 text-right NoLeftPadding NoRightPadding">
                                <label for="tbRace" class="control-label">Race</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlRace" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-1 text-right NoLeftPadding NoRightPadding">
                                <label for="tbDOD" class="control-label">DOD</label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="tbDOD" runat="server" CssClass="form-control" ReadOnly="true" Visible="false" />
                                <asp:Label ID="lblDOD" runat="server" CssClass="form-control ReadOnly" />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <h1 class="col-sm-8">Non Cost Character Descriptors</h1>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-8">Select criteria that describes your character.</div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="pnlCharDesc" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-10">
                                <div class="row" style="border-left: 30px;">
                                    <asp:GridView ID="gvDescriptors" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowCommand="gvDescriptors_RowCommand" DataKeyNames="CharacterAttributesBasicID"
                                        CssClass="table table-striped table-hover table-condensed">
                                        <EmptyDataRowStyle BackColor="Transparent" />
                                        <EmptyDataTemplate>
                                            <div class="row">
                                                <div class="text-center" style="background-color: transparent;">You have no descriptors selected. Please select from the boxes below.</div>
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="CharacterDescriptor" HeaderText="Character Descriptor" />
                                            <asp:BoundField DataField="DescriptorValue" HeaderText="Value" />
                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="40" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CommandName="DeleteDesc" CssClass="NoRightPadding"
                                                        ImageUrl="~/img/delete.png" CommandArgument='<%# Eval("CharacterAttributesBasicID") %>' Width="16px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-4 text-right NoRightPadding">
                                        <label for="ddlDescriptor" class="control-label">Character Descriptor</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlDescriptor" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDescriptor_SelectedIndexChanged" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-4 text-right NoRightPadding">
                                        <label for="ddlDescriptor" class="control-label">Name</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlName" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-4 text-right NoRightPadding">
                                        <label for="tbDateAdded" class="control-label" style="visibility: hidden">Date Added</label>
                                    </div>
                                    <div class="col-sm-1-5">
                                        <asp:TextBox ID="tbDateAdded" runat="server" CssClass="form-control" Visible="false" />
                                    </div>
                                    <div class="col-sm-0-5">
                                        &nbsp;
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnAddDesc" runat="server" Text="Add" class="StandardButton" OnClick="btnAddDesc_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="col-sm-12">
                        <div class="row">
                            <label for="tbDateAdded" class="control-label col-sm-10">&nbsp;</label>
                            <div class="col-sm-2 NoRightPadding">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="StandardButton" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-wrapper col-xs-3">
                    <div class="col-sm-12">
                        <div class="row">
                            To add a profile picture, use the browse button below.
                        </div>
                        <div class="row">
                            <asp:FileUpload ID="ulFile" runat="server" CssClass="control-label Col-sm-12" />
                        </div>
                        <div class="row">
                            <div class="col-sm-6">&nbsp;</div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="StandardButton" OnClick="btnSavePicture_Click" />
                            </div>
                        </div>
                        <br />
                        <asp:Panel ID="pnlCharacterPicture" runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Image ID="imgCharacterPicture" CssClass="col-sm-12" runat="server" Width="125" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5">&nbsp;</div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <asp:Button ID="btnClearPicture" runat="server" CssClass="StandardButton col-sm-12" Text="Clear Picture" OnClick="btnClearPicture_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblMessage" runat="server" />
</asp:Content>
