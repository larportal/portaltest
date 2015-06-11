<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharCardCustomization.aspx.cs" Inherits="LarpPortal.Character.CharCardCustomization" EnableViewState="true" %>

<asp:Content ID="ScriptSection" ContentPlaceHolderID="CharHeaderScripts" runat="server">

    <style type="text/css">
        th, tr:nth-child(even) > td
        {
            background-color: #ffffff;
        }

        .GridViewItem
        {
            padding-left: 5px;
            padding-right: 5px;
        }

        .SpaceBelow
        {
            padding-bottom: 20px;
        }

        .RelationPanel
        {
            max-height: 250px;
            overflow: auto;
            width: auto;
            margin-bottom: 10px;
        }

        .NoPadding
        {
            padding: 0px 0px 0px 0px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Card Customization" />
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
                    <td style="padding-left: 20px;">
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
                    </td>
                </tr>
            </table>
        </div>

        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
                <%-- class="form-horizontal form-condensed">--%>
                <div class="form-horizontal col-sm-12">
                    <div class="row">
                        <div id="Div1" class="panel-wrapper" runat="server">
                            <div class="panel">
                                <div class="panelheader">
                                    <h2>Character Card Customization</h2>
                                    <div class="panel-body">
                                        <div class="panel-container" style="height: 500px; overflow:auto;">
                                            <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="false" OnRowCancelingEdit="gvSkills_RowCancelingEdit"
                                                OnRowEditing="gvSkills_RowEditing" OnRowUpdating="gvSkills_RowUpdating" OnRowDataBound="gvSkills_RowDataBound" GridLines="None"
                                                HeaderStyle-Wrap="false" CssClass="table table-striped table-hover table-condensed">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidSkillID" runat="server" Value='<%# Eval("CharacterSkillsStandardID") %>' />
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

                                                    <asp:TemplateField HeaderText="Skill Card Description" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSkillDesc" runat="server" Text='<%# Eval("SkillCardDescription") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Display Desc" ItemStyle-HorizontalAlign="center" ItemStyle-Wrap="false" ItemStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgDisplay" runat="server" ImageUrl='<%# Boolean.Parse(Eval("CardDisplayDescription").ToString()) ? "../img/checkbox.png" : "../img/delete.png" %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="cbDisplayDesc" runat="server" Text="" CssClass="NoPadding" Checked='<%# Eval("CardDisplayDescription") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Skill Incant" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIncantDesc" runat="server" Text='<%# Eval("SkillIncant") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Display Incant" ItemStyle-HorizontalAlign="center" ItemStyle-Wrap="false" ItemStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgIncant" runat="server" ImageUrl='<%# Boolean.Parse(Eval("CardDisplayIncant").ToString()) ? "../img/checkbox.png" : "../img/delete.png" %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="cbDisplayIncant" runat="server" Text="" CssClass="NoPadding" Checked='<%# Eval("CardDisplayIncant") %>' />
                                                        </EditItemTemplate>
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
                                            </asp:GridView>
                                        </div>
                                        <div class="panel-container">
                                            <div class="row">
                                                <div align="right" style="padding-right: 20px;">
                                                    <asp:Button ID="btnSaveCharacter" runat="server" CssClass="StandardButton" Width="150" Text="Save Changes" OnClick="btnSaveCharacter_Click" />
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

</asp:Content>
