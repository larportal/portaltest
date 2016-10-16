<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharInfo.aspx.cs" Inherits="LarpPortal.Character.CharInfo" %>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="CharHeaderStyles">
    <style type="text/css">
        .TableTextBox {
            border: 1px solid black;
        }

        th, tr:nth-child(even) > td {
            background-color: #ffffff;
        }

        .CharInfoTable {
            border-collapse: collapse;
        }

            .CharInfoTable td {
                padding: 4px;
            }
    </style>
</asp:Content>

<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="CharHeaderScripts">
    <script type="text/javascript">

        //  JBradshaw  7/11/2016    Request #1286     Changed over to bootstrap popup.
        function openMessage() {
            $('#modalMessage').modal('show');
        }
        function closeMessage() {
            $('#modelMessage').hide();
        }

        function openError() {
            $('#modalError').modal('show');
        }
        function closeError() {
            $('#modelError').hide();
        }
    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>

</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Info" />
        </div>
        <div class="row" style="padding-bottom: 10px; padding-left: 15px;">
            <table>
                <tr style="vertical-align: middle;">
                    <%--<td style="width: 10px"></td>--%>
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
                        <asp:Label ID="lblCampaign" runat="server" Text="Fifth Gate" />
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

        <section id="character-info" class="character-info tab-pane active">

            <div class="row">
                <div class="col-sm-12">
                    <asp:Label ID="lblHelp" runat="server" Text="Fill in Information to describe your character. Some items are automatically updated after events." />
                </div>
            </div>


            <div class="row" style="padding-left: 15px;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2>Character Information</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria">
                                <table class="CharInfoTable" border="0">
                                    <tr>
                                        <td class="TableLabel">Character</td>
                                        <td>
                                            <asp:TextBox ID="tbFirstName" runat="server" CssClass="TableTextBox" /></td>
                                        <td>
                                            <asp:TextBox ID="tbMiddleName" runat="server" CssClass="TableTextBox" /></td>
                                        <td>
                                            <asp:TextBox ID="tbLastName" runat="server" CssClass="TableTextBox" /></td>
                                        <td class="TableLabel">Birthplace</td>
                                        <td>
                                            <asp:TextBox ID="tbOrigin" runat="server" CssClass="TableTextBox" /></td>
                                        <td class="TableLabel">Status</td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" /><asp:DropDownList ID="ddlStatus" runat="server" Visible="false" /></td>
                                        <td rowspan="4" style="width: 35px;">&nbsp;</td>
                                        <td>To add a profile picture, use the browse button below.
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TableLabel">AKA</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="tbAKA" runat="server" CssClass="TableTextBox" /></td>
                                        <td class="TableLabel">Home</td>
                                        <td>
                                            <asp:TextBox ID="tbHome" runat="server" CssClass="TableTextBox" /></td>
                                        <td class="TableLabel">Last Event</td>
                                        <td>
                                            <asp:TextBox ID="tbDateLastEvent" runat="server" Enabled="false" /><asp:Label ID="lblDateLastEvent" runat="server" /></td>
                                        <td>
                                            <asp:FileUpload ID="ulFile" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TableLabel">Type</td>
                                        <td colspan="1">
                                            <asp:TextBox ID="tbType" runat="server" Enabled="false" /></td>
                                        <td colspan="2"></td>
                                        <td class="TableLabel">Primary Team</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTeamList" runat="server" />
                                            <asp:TextBox ID="tbTeam" runat="server" CssClass="col-sm-12" Enabled="false" />
                                        </td>
                                        <td class="TableLabel"># of Deaths</td>
                                        <td>
                                            <asp:TextBox ID="tbNumOfDeaths" runat="server" Enabled="false" /></td>
                                        <td style="text-align: right;">
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="StandardButton" OnClick="btnSavePicture_Click" Width="100" /></td>
                                    </tr>

                                    <tr style="vertical-align: top;">
                                        <td class="TableLabel">DOB</td>
                                        <td>
                                            <asp:TextBox ID="tbDOB" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td class="TableLabel">Race</td>
                                        <td>
                                            <asp:DropDownList ID="ddlRace" runat="server" /></td>
                                        <td class="TableLabel">DOD</td>
                                        <td>
                                            <asp:TextBox ID="tbDOD" runat="server" Enabled="false" />
                                            <asp:Label ID="lblDOD" runat="server" />
                                        </td>
                                        <th>
                                            <asp:Image ID="imgCharacterPicture" runat="server" Width="125" /></th>
                                    </tr>

                                    <tr>
                                        <td colspan="9"></td>
                                        <td style="text-align: center;">
                                            <asp:Button ID="btnClearPicture" runat="server" CssClass="StandardButton" Width="100" Text="Clear Picture" OnClick="btnClearPicture_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <br />

            <div class="row" style="padding-left: 15px;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2>Non Cost Character Descriptors</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria">
                                <div style="margin-bottom: 2px; font-size: larger;">Select criteria that describes your character.</div>
                                <asp:GridView ID="gvDescriptors" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowCommand="gvDescriptors_RowCommand"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" DataKeyNames="CharacterAttributesBasicID"
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
                                <table class="CharInfoTable">
                                    <tr>
                                        <td><b>Character Descriptor</b></td>
                                        <td>
                                            <asp:DropDownList ID="ddlDescriptor" runat="server" Style="min-width: 150px;" AutoPostBack="true" OnSelectedIndexChanged="ddlDescriptor_SelectedIndexChanged" /></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;"><b>Name</b></td>
                                        <td>
                                            <asp:DropDownList ID="ddlName" runat="server" Style="min-width: 150px;" /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="text-align: right">
                                            <asp:Button ID="btnAddDesc" runat="server" Width="100px" Text="Add" CssClass="StandardButton" OnClick="btnAddDesc_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" style="padding-left: 15px; text-align: right; padding-top: 10px;">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="StandardButton" Width="100px" OnClick="btnSave_Click" />
            </div>


        </section>
    </div>
    <asp:Label ID="lblMessage" runat="server" />


    <!-- JBradshaw  7/11/2016    Request #1286     Changed over to bootstrap popup.  -->
    <div class="modal" id="modalError" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: red;">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Info
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblmodalError" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseError" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnCloseError_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Info
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblmodalMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnCloseMessage_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
