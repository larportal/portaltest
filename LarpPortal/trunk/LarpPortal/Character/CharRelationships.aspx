<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharRelationships.aspx.cs" 
    Inherits="LarpPortal.Character.CharRelationships" EnableViewState="true" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="ScriptSection" ContentPlaceHolderID="CharHeaderScripts" runat="server">
    <script type="text/javascript">
        function CheckForOther() {
            var RelSelect = document.getElementById("<%= ddlRelationship.ClientID %>");
            var selectedText = RelSelect.options[RelSelect.selectedIndex].text;
            var tbOther = document.getElementById("<%= tbOther.ClientID %>");

            if (selectedText == "Other")
                tbOther.style.display = "block";
            else
                tbOther.style.display = "none";
        }

        function CheckForOtherNonChar() {
            var RelSelect = document.getElementById("<%= ddlRelationshipNonChar.ClientID %>");
            var selectedText = RelSelect.options[RelSelect.selectedIndex].text;
            var tbOther = document.getElementById("<%= tbOtherNonChar.ClientID %>");

            if (selectedText == "Other")
                tbOther.style.display = "block";
            else
                tbOther.style.display = "none";
        }
    </script>

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
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Relationships" />
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
            <section role="form" class="form-horizontal form-condensed">
<%--                <div class="row">
                    <h1 class="col-xs-10" style="padding-top: 0px;">
                        <asp:Label ID="lblHeader" runat="server" /></h1>
                </div>--%>
                <div class="row" style="padding-left: 20px;">
                    Select an in game PC or NPC character and the corresponding relationship  from the lists provided - or enter the name of a person that is not on the list in the field provided.
                </div>

                <div class="form-horizontal col-sm-12">
                    <div class="row">
                        <div id="Div1" class="panel-wrapper" runat="server">
                            <div class="panel">
                                <div class="panelheader">
                                    <h2>Relationships</h2>
                                    <div class="panel-body">
                                        <div class="panel-container" style="height: 520px;">
                                            <div class="col-sm-3 scroll-500">
                                                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover table-condensed" DataKeyNames="CharacterID"
                                                    AutoGenerateSelectButton="true" OnRowDataBound="gvList_RowDataBound" OnSelectedIndexChanged="gvList_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Character" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="Larger">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCharacterAKA" runat="server" Text='<%# Eval("CharacterAKA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-8" style="vertical-align: middle; padding-left: 5px;">
                                                <div class="row" style="padding-left: 20px;">
                                                    <asp:Panel ID="pnlRelation" runat="server" CssClass="RelationPanel">
                                                        <asp:GridView ID="gvRelationships" runat="server" AutoGenerateColumns="false" GridLines="none"
                                                            AlternatingRowStyle-BackColor="Linen" BorderColor="Black" BorderWidth="1px" BorderStyle="none" ShowFooter="true"
                                                            OnRowCommand="gvRelationships_RowCommand" Caption="<span style='font-size: larger; font-weight: bold;'>Character Relationships</span>">
                                                            <Columns>
                                                                <asp:BoundField DataField="Name" HeaderText="Character Name" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewItem" />
                                                                <asp:BoundField DataField="RelationDescription" HeaderText="Relationship" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewItem" />
                                                                <asp:BoundField DataField="PlayerComments" HeaderText="Player Comments" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewItem" />
                                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="16" ItemStyle-Wrap="false" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewItem">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CommandName="EditItem" CssClass="NoRightPadding"
                                                                            ImageUrl="~/img/edit.gif" CommandArgument='<%# Eval("CharacterRelationshipID") %>' Width="16px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="16" ItemStyle-Wrap="false" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewItem">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CommandName="DeleteItem" CssClass="NoRightPadding" OnClientClick="if (!confirm('Are you sure you want to delete?')) return false;"
                                                                            ImageUrl="~/img/delete.png" CommandArgument='<%# Eval("CharacterRelationshipID") %>' Width="16px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>

                                                <div class="row" style="padding-left: 20px;">
                                                    <asp:HiddenField ID="hidRelateID" runat="server" />

                                                    <asp:MultiView ID="mvAddingRelationship" runat="server" ActiveViewIndex="0">
                                                        <asp:View ID="vwNewRelateButton" runat="server">
                                                            <asp:Button ID="btnAddNewRelate" runat="server" CssClass="StandardButton col-sm-3" Text="Add Non-Character Relationship" OnClick="btnAddNewRelate_Click" />
                                                        </asp:View>
                                                        <asp:View ID="vwNewRelate" runat="server">
                                                            <table border="0">
                                                                <tr style="background-color: white">
                                                                    <td colspan="2"><b style="font-size: larger;">Add a new non-character relationship for this character.</b>
                                                                    </td>
                                                                </tr>
                                                                <tr style="background-color: white">
                                                                    <td><b>Name</b></td>
                                                                    <td style="padding-left: 10px;"><b>Relationship</b></td>
                                                                    <td style="padding-left: 10px;"><b>Player Comments</b></td>
                                                                </tr>
                                                                <tr style="background-color: white">
                                                                    <td style="vertical-align: top;">
                                                                        <asp:TextBox ID="tbCharacterName" runat="server" Columns="30" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" /></td>
                                                                    <td style="vertical-align: top; padding-left: 10px;">
                                                                        <asp:DropDownList ID="ddlRelationshipNonChar" runat="server" /><br />
                                                                        <asp:TextBox ID="tbOtherNonChar" runat="server" Columns="40" MaxLength="40" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" />
                                                                    </td>
                                                                    <td style="padding-left: 10px;">
                                                                        <asp:TextBox ID="tbPlayerCommentsNonChar" runat="server" Columns="50" Rows="6" TextMode="MultiLine" /></td>
                                                                </tr>
                                                                <tr style="background-color: white">
                                                                    <td>
                                                                        <asp:Button ID="btnCancelNonChar" CssClass="StandardButton" runat="server" Text="Cancel" Style="width: 75px;"
                                                                            OnClick="btnCancelAdding_Click" /></td>
                                                                    <td>&nbsp;</td>
                                                                    <td style="padding-left: 10px; text-align: right;">
                                                                        <asp:Button ID="btnSaveNonChar" CssClass="StandardButton" runat="server" Text="Save"
                                                                            Style="width: 75px;" OnClick="btnSaveNonChar_Click" /></td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </asp:View>

                                                        <asp:View ID="vwExistingCharacter" runat="server">
                                                            <table border="0">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblCharacter" runat="server" Font-Size="X-Large" Font-Bold="true" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Relationship</th>
                                                                    <th style="padding-left: 10px;">Player Comments</th>
                                                                </tr>
                                                                <tr style="vertical-align: top;">
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlRelationship" runat="server" /><br />
                                                                        <asp:TextBox ID="tbOther" runat="server" Columns="40" MaxLength="40" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" />
                                                                    </td>
                                                                    <td style="padding-left: 10px;">
                                                                        <asp:TextBox ID="tbPlayerComments" runat="server" Columns="50" Rows="6" TextMode="MultiLine" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnCancelAdding" runat="server" Text="Cancel" CssClass="StandardButton" Style="width: 75px;" OnClick="btnCancelAdding_Click" /></td>
                                                                    <td align="right">
                                                                        <asp:Button ID="btnSaveExistingRelate" runat="server" CssClass="StandardButton" Style="width: 75px;" Text="Save" OnClick="btnSaveExistingRelate_Click" /></td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <div class="col-sm-0-5">&nbsp;</div>
                                                        </asp:View>
                                                    </asp:MultiView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-container">
                                            <div class="row">
                                                <div align="right" style="padding-right: 20px;">
                                                    <asp:Button ID="btnSaveCharacter" runat="server" CssClass="StandardButton" Width="150" Text="Save All Changes" OnClick="btnSaveCharacter_Click" />
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
