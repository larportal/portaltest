<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharCardOrder.aspx.cs" Inherits="LarpPortal.Character.CharCardOrder" EnableViewState="true" %>

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
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Card Sort Order" />
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
                <div class="form-horizontal col-sm-12">
                    <div class="row">
                        <div id="Div1" class="panel-wrapper" runat="server">
                            <div class="panel">
                                <div class="panelheader">
                                    <h2>Character Card Display Order</h2>
                                    <div class="panel-body">
                                        <div class="panel-container" style="height: 500px; overflow:auto;">
                                            <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvSkills_RowDataBound" GridLines="None"
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
