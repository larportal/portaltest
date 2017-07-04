<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Profile.Master" AutoEventWireup="true" CodeBehind="PlayerPreferences.aspx.cs" Inherits="LarpPortal.Profile.PlayerPreferences" %>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="ProfileHeaderStyles">
    <style type="text/css">
        .TableTextBox {
            border: 1px solid black;
        }

        .CharInfoTable {
            border-collapse: collapse;
        }

            .CharInfoTable td {
                padding: 4px;
            }

        .checkbox label {
            padding-left: 0px !important;
        }

        .WhiteRow {
            padding-top: 5px;
            padding-bottom: 5px;
            white-space: nowrap;
            background-color: #ffffff;
        }

        .GreyRow {
            padding-top: 5px;
            padding-bottom: 5px;
            white-space: nowrap;
            background-color: #eeeeee;
        }

        .BottomAlign {
            vertical-align: middle !important;
        }

        .DataRadioButton {
            font-weight: normal !important;
            width: 200px;
        }

            .DataRadioButton label {
                font-weight: normal !important;
                color: black;
            }

            .DataRadioButton input[type="radio"]:checked + label {
                font-weight: bold !important;
                /*font-size: larger !important;*/
            }

            .DataRadioButton input[type="radio"]:disabled + label {
                color: gray;
            }

        .RadioButtonCell {
            padding-left: 5px !important;
            padding-right: 5px !important;
            vertical-align: bottom !important;
        }

        .GridViewHeader {
            background-color: #093760 !important;
            color: white !important;
        }

            .GridViewHeader th {
                background-color: #093760 !important;
                color: white !important;
            }

        .DescPadding {
            /*padding-left: 25px;*/
        }

        .NoPaddingTopBottom {
            padding-top: 0px !important;
            padding-bottom: 0px !important;
        }

        .DescTopPadding {
            padding-top: 15px !important;
            vertical-align: bottom !important;
        }

        .fixWidth {
            width: 200px;
        }

        /*.fixWidth label {
            padding-left: 2px !important;
        }*/

        .fixWidth tr td {
            width: 33%;
        }
        .fixWidth tr th {
            width: 33%;
        }

    </style>
</asp:Content>

<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="ProfileHeaderScripts">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function closeModal() {
            $('#myModal').hide();
        }

        $('div .checkbox').click(function () {
            checkedState = $(this).attr('checked');
            $(this).parent('div').children('.checkbox:checked').each(function () {
                $(this).attr('checked', false);
            });
            $(this).attr('checked', checkedState);
        });
    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="MainPage" ContentPlaceHolderID="ProfileMain" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-top: 10px; padding-bottom: 0px;">
            <div class="col-sm-4">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Preferences" />
            </div>
        </div>

        <section id="character-info" class="character-info tab-pane active">
            <div class="row" style="padding-left: 15px;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2>Player Notifications</h2>
                        <div class="panel-body">
                            <div class="panel-container">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvPref" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvPref_RowDataBound"
                                            CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Width="99%">
                                            <RowStyle VerticalAlign="Bottom" />
                                            <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="2" ForeColor="White" BackColor="DarkBlue" CssClass="GridViewHeader" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemStyle Wrap="false" CssClass="BottomAlign" Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="rblOptions" runat="server" OnSelectedIndexChanged="rblOptions_SelectedIndexChanged" CssClass="fixWidth DataRadioButton"
                                                            AutoPostBack="true" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="None" Value="None" />
                                                            <asp:ListItem Text="EMail" Value="EMail" />
                                                            <asp:ListItem Text="Text" Value="Text" />
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="false" Width="200px" />
                                                    <HeaderTemplate>
                                                        <asp:RadioButtonList ID="rblHeaderOptions" runat="server" CssClass="fixWidth" Font-Bold="true" OnSelectedIndexChanged="rblHeaderOptions_SelectedIndexChanged"
                                                            AutoPostBack="true" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="None" Value="None" />
                                                            <asp:ListItem Text="EMail" Value="EMail" />
                                                            <asp:ListItem Text="Text" Value="Text" />
                                                        </asp:RadioButtonList>
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemStyle CssClass="BottomAlign" Wrap="false" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisplayDesc" runat="server" Text='<%# Eval("DisplayDesc") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="BottomAlign" Font-Size="larger" Wrap="false" />
                                                    <HeaderTemplate>
                                                        <b>Set all records.</b>
                                                        <asp:Label ID="lblHeaderEMail" runat="server" Style="padding-left: 50px;" Font-Bold="false" />
                                                        <asp:Label ID="lblHeaderText" runat="server" Style="padding-left: 50px;" Font-Bold="false" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
<%--                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hidPlayerProfileID" runat="server" Value='<%# Eval("PlayerProfileID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
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

    <div class="modal" id="myModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Preferences
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="StandardButton" style="width: 125px;">Close</button>
                    <%--                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnClose_Click" />--%>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidPlayerProfileID" runat="server" />
    <asp:HiddenField ID="hidMobileNumber" runat="server" />
    <asp:HiddenField ID="hidEMail" runat="server" />


</asp:Content>
