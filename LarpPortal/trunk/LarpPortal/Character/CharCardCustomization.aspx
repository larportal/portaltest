<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharCardCustomization.aspx.cs" Inherits="LarpPortal.Character.CharCardCustomization" EnableViewState="true" ValidateRequest="false" %>

<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>

<asp:Content ID="Scripts" ContentPlaceHolderID="CharHeaderScripts" runat="server">
    <script type="text/javascript">
        function openMessage() {
            $('#modalMessage').modal('show');
        }
    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="Styles" ContentPlaceHolderID="CharHeaderStyles" runat="server">

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

        .tooltip-inner {
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
            margin-bottom: 0px;
            background-color: white;
            color: black;
            font-size: 14px;
            box-shadow: 5px 5px 10px #888888;
            width: auto;
            opacity: 1;
            filter: alpha(opacity=100);
            white-space: nowrap;
            max-width: 350px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CharHeaderMain" runat="server">
    <div class="mainContent tab-content col-sm-12 row">
        <div class="row" style="padding-left: 15px; padding-top: 5px; padding-bottom: 1px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Card Customization" />
        </div>

        <div class="row col-sm-12" style="padding-bottom: 0px; padding-left: 15px; padding-right: 0px;">
            <%-- border: 1px solid black;">--%>
            <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">
                <CharSelecter:CSelect ID="oCharSelect" runat="server" />
            </div>
            <div class="col-sm-2 text-right" style="padding-right: 0px;">
                <asp:Button ID="btnSaveTop" runat="server" CssClass="StandardButton" Width="100" Text="Save Changes" OnClick="btnSave_Click" />
            </div>
        </div>

        <div id="character-info" class="character-info tab-pane active row">
            <section role="form">
                <%-- class="form-horizontal form-condensed">--%>
                <div class="form-horizontal col-sm-12">
                    <div class="row">
                        <div id="Div1" class="panel-wrapper" runat="server">
                            <div class="panel">
                                <div class="panelheader">
                                    <h2>Character Card Customization</h2>
                                    <div class="panel-body">
                                        <div class="panel-container" style="height: 500px; overflow: auto;">
                                            <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="false" OnRowCancelingEdit="gvSkills_RowCancelingEdit" OnDataBound="gvSkills_DataBound"
                                                OnRowEditing="gvSkills_RowEditing" OnRowUpdating="gvSkills_RowUpdating" OnRowDataBound="gvSkills_RowDataBound" GridLines="None"
                                                HeaderStyle-Wrap="false" CssClass="table table-striped table-hover table-condensed">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidSkillID" runat="server" Value='<%# Eval("CharacterSkillID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Skill Name">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("SkillName") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("SkillName") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="right" ItemStyle-Wrap="false" ItemStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <a href="#" data-toggle="tooltip" title="Should the standard description be displayed?">
                                                            <asp:Image ID="imgDisplay" runat="server" ImageUrl='<%# Boolean.Parse(Eval("CardDisplayDescription").ToString()) ? "../img/checkbox.png" : "../img/delete.png" %>' ToolTip="Should the Skill Card Description Be Displayed" />
                                                                </a>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="cbDisplayDesc" runat="server" Text="" CssClass="NoPadding" Checked='<%# Eval("CardDisplayDescription") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Skill Card Description" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSkillDesc" runat="server" Text='<%# Eval("SkillCardDescription") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="right" ItemStyle-Wrap="false" ItemStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <a href="#" data-toggle="tooltip" title="Should the standard incant be displayed?">
                                                                <asp:Image ID="imgIncant" runat="server" ImageUrl='<%# Boolean.Parse(Eval("CardDisplayIncant").ToString()) ? "../img/checkbox.png" : "../img/delete.png" %>' />
                                                            </a>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="cbDisplayIncant" runat="server" Text="" CssClass="NoPadding" Checked='<%# Eval("CardDisplayIncant") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Skill Incant" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIncantDesc" runat="server" Text='<%# Eval("SkillIncant") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Card Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCardDesc" runat="server" Text='<%# Eval("PlayerDescription") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbPlayDesc" runat="server" Text='<%# Eval("PlayerDescription") %>' BorderColor="Black" BorderStyle="Solid" BorderWidth="1" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Card Incant">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCardIncant" runat="server" Text='<%# Eval("PlayerIncant") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbPlayIncant" runat="server" Text='<%# Eval("PlayerIncant") %>' BorderColor="Black" BorderStyle="Solid" BorderWidth="1" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" Width="100px" CssClass="StandardButton" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Button ID="btnupdate2" runat="server" CommandName="Update" Text="Update" Width="100px" CssClass="StandardButton" />
                                                            <asp:Button ID="btncancel2" runat="server" CommandName="Cancel" Text="Cancel" Width="100px" CssClass="StandardButton" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <span style="color: red; font-weight: bold; font-size: 24px;">The character has no skills defined.</span>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
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

    <br />
    <div class="row text-right" style="padding-right: 20px; padding-top: 20px;">
    </div>
    <div class="row text-right" style="padding-top: 10px; padding-right: 45px; padding-bottom: 20px;">
        <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Width="100" Text="Save Changes" OnClick="btnSave_Click" />
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Card Customization
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

    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $('[data-toggle="popover"]').popover({
            placement: 'top',
            trigger: 'hover'
        });
    </script>

</asp:Content>
