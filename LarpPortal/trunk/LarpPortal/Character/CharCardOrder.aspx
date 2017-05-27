<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharCardOrder.aspx.cs" Inherits="LarpPortal.Character.CharCardOrder" EnableViewState="true" %>

<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>

<asp:Content ID="ScriptSection" runat="server" ContentPlaceHolderID="CharHeaderScripts">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="StyleSection" ContentPlaceHolderID="CharHeaderStyles" runat="server">

    <style type="text/css">
        th, tr:nth-child(even) > td {
            background-color: #ffffff;
        }

        .GridViewItem {
            padding-left: 5px;
            padding-right: 5px;
        }

        .SpaceBelow {
            padding-bottom: 20px;
        }

        .RelationPanel {
            max-height: 250px;
            overflow: auto;
            width: auto;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CharHeaderMain" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Card Sort Order" />
        </div>

        <div class="row col-sm-12" style="padding-bottom: 0px; padding-left: 15px; padding-right: 0px;">
            <%-- border: 1px solid black;">--%>
            <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">
                <CharSelecter:CSelect ID="oCharSelect" runat="server" />
            </div>
            <div class="col-sm-2 text-right">
                <asp:Button ID="btnTopSave" runat="server" CssClass="StandardButton" Width="100" Text="Save Changes" OnClick="btnSaveCharacter_Click" />
            </div>
        </div>

        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
                <div class="form-horizontal col-sm-12">
                    <div class="row">
                        <div id="Div1" class="panel-wrapper" runat="server">
                            <div class="panel">
                                <div class="panelheader">
                                    <h2>Character Card Display Order</h2>
                                    <div class="panel-body">
                                        <div class="panel-container" style="height: 500px; overflow: auto;">
                                            <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="false" GridLines="None"
                                                HeaderStyle-Wrap="false" CssClass="table table-striped table-hover table-condensed">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidSkillID" runat="server" Value='<%# Eval("CharacterSkillID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skill Name">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("SkillName") %>' ToolTip='<%# Eval("DisplayOrder") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skill Card Description" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSkillDesc" runat="server" Text='<%# Eval("SkillCardDescription") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sort Order">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="tbSortOrder" Text='<%# Eval("SortOrder") %>' CssClass="form-control" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <span style="color: red; font-weight: bold; font-size: 24px;">The character has no skills defined.</span>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                        <div class="panel-container">
                                            <div class="row">
                                                <div class="text-right" style="padding-right: 20px;">
                                                    <asp:Button ID="btnSaveCharacter" runat="server" CssClass="StandardButton" Width="100" Text="Save Changes" OnClick="btnSaveCharacter_Click" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                &nbsp;
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Card Order
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblmodalMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
