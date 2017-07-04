<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Profile.Master" AutoEventWireup="true" CodeBehind="Waivers.aspx.cs" Inherits="LarpPortal.Profile.Waivers" %>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="ProfileHeaderStyles">
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

        .checkbox label {
            padding-left: 0px !important;
        }

        .TableTextBoxWithPadding {
            border: 1px solid black;
            padding: 2px;
        }

        .form-group input[type="checkbox"] {
            display: none;
        }

            .form-group input[type="checkbox"] + .btn-group > label span {
                width: 20px;
            }

                .form-group input[type="checkbox"] + .btn-group > label span:first-child {
                    display: none;
                }

                .form-group input[type="checkbox"] + .btn-group > label span:last-child {
                    display: inline-block;
                }

            .form-group input[type="checkbox"]:checked + .btn-group > label span:first-child {
                display: inline-block;
            }

            .form-group input[type="checkbox"]:checked + .btn-group > label span:last-child {
                display: none;
            }

        .NoBottomMargins {
            margin-bottom: 0px;
        }
    </style>
</asp:Content>

<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="ProfileHeaderScripts">
    <script src="../Scripts/jquery-1.11.3.js"></script>

    <script type="text/javascript">
        function openModalMessage() {
            $('#ModalMessage').modal('show');
        }

        function DisagreeChecked(DisagreeCheckBox) {
            var cbxAgree = document.getElementById('<%= cbxAgree.ClientID %>');
            if (cbxAgree)
                if (DisagreeCheckBox.checked)
                    cbxAgree.checked = false;
        }

        function AgreeChecked(AgreeCheckBox) {
            var cbxDisagree = document.getElementById('<%= cbxDisagree.ClientID %>');
            if (cbxDisagree)
                if (AgreeCheckBox.checked)
                    cbxDisagree.checked = false;
        }

        function setSelectedValue(selectObj, valueToSet) {
            for (var i = 0; i < selectObj.options.length; i++) {
                if (selectObj.options[i].value == valueToSet) {
                    selectObj.options[i].selected = true;
                    return;
                }
            }
        }

    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="MainPage" ContentPlaceHolderID="ProfileMain" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-top: 10px; padding-bottom: 15px;">
            <div class="col-xs-4">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Waivers &amp; Consent" />
            </div>
        </div>

        <section id="character-info" class="character-info tab-pane active">
            <div class="row">
                <div class="col-xs-12 col-lg-6">
                    <div class="panel NoGutters" style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 15px;">
                        <div class="panelheader NoGutters">
                            <h2>Waivers</h2>
                            <div class="panel-body NoGutters">
                                <div class="panel-container" style="padding-bottom: 15px; padding-left: 15px;">
                                    <div class="row" style="padding-left: 15px; padding-right: 15px; padding-bottom: 0px; margin-bottom: 0px; max-height: 125px; overflow-y: auto;">
                                        <asp:GridView runat="server" ID="gvWaivers" AutoGenerateColumns="false" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="1" DataKeyNames="PlayerWaiverID" OnRowCreated="gvWaivers_RowCreated" EnableViewState="false"
                                            CssClass="table table-striped table-hover table-condensed col-sm-12 NoBottomMargins">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hidPlayerWaiverID" runat="server" Value='<%# Eval("PlayerWaiverID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CampaignName" HeaderText="Campaign" />
                                                <asp:BoundField DataField="WaiverType" HeaderText="Waiver Type" />
                                                <asp:BoundField DataField="WaiverStatus" HeaderText="Status" />
                                                <asp:BoundField DataField="WaiverStatusDate" HeaderText="Status Date" DataFormatString="{0:MM/dd/yyyy}" />
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="col-xs-12" Font-Bold="true" Font-Size="XX-Large" HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                There are no waivers you need to review.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-9">
                    <div class="panel NoGutters" style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 15px;">
                        <div class="panelheader">
                            <h2>Waiver Text</h2>
                            <div class="panel-body">
                                <div class="panel-container" style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px;">
                                    <div class="row">
                                        <div class="col-xs-12" style="max-height: 150px; overflow-y: auto;">
                                            <asp:Label ID="lblWaiverText" runat="server" CssClass="col-xs-12" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="col-xs-12 form-group" style="padding-left: 0px; margin-bottom: 0px;">
                        <input type="checkbox" name="cbxAgree" id="cbxAgree" runat="server" onclick="AgreeChecked(this);" />
                        <div class="btn-group col-xs-12">
                            <label for="<%= cbxAgree.ClientID%>" class="btn btn-default">
                                <span class="glyphicon glyphicon-ok"></span>
                                <span class="glyphicon glyphicon-unchecked"></span>
                            </label>
                            <label for="<%= cbxAgree.ClientID %>" class="btn btn-default active">
                                I Agree
                            </label>
                        </div>
                    </div>
                    <div class="col-xs-12 form-group" style="padding-left: 0px;">
                        <input type="checkbox" name="cbxDisagree" id="cbxDisagree" runat="server" onclick="DisagreeChecked(this);" />
                        <div class="btn-group col-xs-12">
                            <label for="<%= cbxDisagree.ClientID %>" class="btn btn-default">
                                <span class="glyphicon glyphicon-ok"></span>
                                <span class="glyphicon glyphicon-unchecked"></span>
                            </label>
                            <label for="<%= cbxDisagree.ClientID %>" class="btn btn-default active">
                                I do not Agree
                            </label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Player Comments</h2>
                            <div class="panel-body">
                                <div class="panel-container" style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px;">
                                    <div class="row" style="padding-right: 15px;">
                                        <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                            <asp:TextBox ID="tbWaiverComment" runat="server" TextMode="MultiLine" CssClass="TableTextBoxWithPadding col-xs-12" Rows="4" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 text-right" style="margin-top: 20px; padding-left: 15px; padding-right: 15px;">
                    <asp:Button ID="btnSaveComments" runat="server" Text="Save" CssClass="StandardButton" Width="100" OnClick="btnSaveComments_Click" />
                </div>
            </div>
        </section>
    </div>

    <div class="modal" id="ModalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Medical Information
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblModalMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="StandardButton" style="width: 125px;">Close</button>
                </div>
            </div>
        </div>
    </div>

    <%--    <asp:HiddenField ID="hidPlayerProfileID" runat="server" />--%>
    <asp:HiddenField ID="hidPlayerWaiverID" runat="server" />
    <asp:HiddenField ID="hidRowSelected" runat="server" />
</asp:Content>
