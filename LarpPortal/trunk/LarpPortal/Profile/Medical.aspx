<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Profile.Master" AutoEventWireup="true" CodeBehind="Medical.aspx.cs" Inherits="LarpPortal.Profile.Medical" %>

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
    </style>
</asp:Content>

<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="ProfileHeaderScripts">
    <script src="../Scripts/jquery-1.11.3.js"></script>

    <script type="text/javascript">
        function openModalMessage() {
            $('#ModalMessage').modal('show');
        }

        function openMedical(PlayerMedicalID, Condition, Medications, ShareInfo, PrintOnCard, StartDate, EndDate) {
            $('#modalMedical').modal('show');

            var tbCondition = document.getElementById('<%= tbCondition.ClientID %>');
            if (tbCondition) {
                tbCondition.value = Condition;
                tbCondition.focus();
            }

            var tbMedication = document.getElementById('<%= tbMedication.ClientID %>');
            if (tbMedication)
                tbMedication.value = Medications;

            var hidPlayerMedicalID = document.getElementById('<%= hidPlayerMedicalID.ClientID %>');
            if (hidPlayerMedicalID)
                hidPlayerMedicalID.value = PlayerMedicalID;

            var cbxShareWithStaff = document.getElementById('<%= cbxShareWithStaff.ClientID %>');
            if (cbxShareWithStaff) {
                cbxShareWithStaff.checked = false;
                if (ShareInfo == "True")
                    cbxShareWithStaff.checked = true;
            }

            var cbxPrintOnCard = document.getElementById('<%= cbxPrintOnCard.ClientID %>');
            if (cbxPrintOnCard) {
                cbxPrintOnCard.checked = false;
                if (PrintOnCard == "True")
                    cbxPrintOnCard.checked = true;
            }

            var tbMedicalStartDate = document.getElementById('<%= tbMedicalStartDate.ClientID %>');
            if (tbMedicalStartDate) {
                tbMedicalStartDate.value = "";
                if (StartDate)
                    tbMedicalStartDate.value = StartDate;
            }

            var tbMedicalEndDate = document.getElementById('<%= tbMedicalEndDate.ClientID %>');
            if (tbMedicalEndDate) {
                tbMedicalEndDate.value = "";
                if (EndDate)
                    tbMedicalEndDate.value = EndDate;
            }

            return false;
        }

        function openMedicalDelete(PlayerMedicalID, Condition, Medications) {
            $('#modalMedicalDelete').modal('show');

            var lblDeleteCondition = document.getElementById('lblDeleteCondition');
            if (lblDeleteCondition)
                lblDeleteCondition.innerText = Condition;

            var lblDeleteMedication = document.getElementById('lblDeleteMedication');
            if (lblDeleteMedication)
                lblDeleteMedication.innerText = Medications;

            var hidDeleteMedicalID = document.getElementById('<%= hidDeleteMedicalID.ClientID %>');
            if (hidDeleteMedicalID)
                hidDeleteMedicalID.value = PlayerMedicalID;

            return false;
        }

        function openLimitations(PlayerLimitID, Description, ShareInfo, PrintOnCard, StartDate, EndDate) {
            $('#modalLimitation').modal('show');

            var tbLimitation = document.getElementById('<%= tbLimitation.ClientID %>');
            if (tbLimitation) {
                tbLimitation.value = Description;
                tbLimitation.focus();
            }

            var hidPlayerLimitID = document.getElementById('<%= hidPlayerLimitID.ClientID %>');
            if (hidPlayerLimitID)
                hidPlayerLimitID.value = PlayerLimitID;

            var cbxLimitShareWithStaff = document.getElementById('<%= cbxLimitShareWithStaff.ClientID %>');
            if (cbxLimitShareWithStaff) {
                cbxLimitShareWithStaff.checked = false;
                if (ShareInfo == "True")
                    cbxLimitShareWithStaff.checked = true;
            }

            var cbxLimitPrintOnCard = document.getElementById('<%= cbxLimitPrintOnCard.ClientID %>');
            if (cbxLimitPrintOnCard) {
                cbxLimitPrintOnCard.checked = false;
                if (PrintOnCard == "True")
                    cbxLimitPrintOnCard.checked = true;
            }

            var tbLimitationStartDate = document.getElementById('<%= tbLimitationStartDate.ClientID %>');
            if (tbLimitationStartDate) {
                tbLimitationStartDate.value = "";
                if (StartDate)
                    tbLimitationStartDate.value = StartDate;
            }

            var tbLimitationEndDate = document.getElementById('<%= tbLimitationEndDate.ClientID %>');
            if (tbLimitationEndDate) {
                tbLimitationEndDate.value = "";
                if (EndDate)
                    tbLimitationEndDate.value = EndDate;
            }

            return false;
        }

        function openLimitationsDelete(PlayerLimitID, Description) {
            $('#modalLimitationsDelete').modal('show');

            var lblDeletePlayerLimitation = document.getElementById('lblDeletePlayerLimitation');
            if (lblDeletePlayerLimitation)
                lblDeletePlayerLimitation.innerText = Description;

            var hidDeleteLimitID = document.getElementById('<%= hidDeleteLimitID.ClientID %>');
            if (hidDeleteLimitID)
                hidDeleteLimitID.value = PlayerLimitID;

            return false;
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
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Medical Info" />
            </div>
        </div>

        <section id="character-info" class="character-info tab-pane active">
            <div class="row">
                <div class="col-xs-12 col-lg-6">
                    <div class="panel NoGutters" style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 15px;">
                        <div class="panelheader NoGutters">
                            <h2>Medical Conditions</h2>
                            <div class="panel-body NoGutters">
                                <div class="panel-container" style="padding-bottom: 15px; padding-left: 15px;">
                                    <div class="row" style="padding-left: 15px; padding-right: 15px;">
                                        <asp:GridView runat="server" ID="gvMedical" AutoGenerateColumns="false" GridLines="None"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                                            CssClass="table table-striped table-hover table-condensed col-sm-12">
                                            <Columns>
                                                <asp:BoundField DataField="Description" HeaderText="Condition" />
                                                <asp:BoundField DataField="Medication" HeaderText="Medication" />
                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}" />

                                                <asp:TemplateField HeaderText="Show Staff" ItemStyle-HorizontalAlign="center" ItemStyle-Wrap="false" ItemStyle-Width="30px" HeaderStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <a href="#" data-toggle="tooltip" title="Should this be displayed to staff ?">
                                                            <asp:Image ID="imgDisplayShowStaff" runat="server"
                                                                ImageUrl='<%# Boolean.Parse(Eval("ShareInfo").ToString()) ? "../img/checkbox.png" : "../img/uncheckbox.png" %>' ToolTip="Should this be displayed to staff ?" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="60px" HeaderText="&nbsp;&nbsp;&nbsp;Print&nbsp; On Card" HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="center" ItemStyle-Wrap="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <a href="#" data-toggle="tooltip" title="Should this be displayed to staff ?">
                                                            <asp:Image ID="imgDisplayPrintOnCard" runat="server"
                                                                ImageUrl='<%# Boolean.Parse(Eval("PrintOnCard").ToString()) ? "../img/checkbox.png" : "../img/uncheckbox.png" %>' ToolTip="Should this be displayed to staff ?" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/Edit.gif" Width="16px"
                                                            OnClientClick='<%# Eval("JavaScriptEdit") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/delete.png" Width="16px"
                                                            OnClientClick='<%# Eval("JavaScriptDelete") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                            <asp:Button ID="btnAddResumeItem" runat="server" Text="Add" CssClass="StandardButton" Width="100"
                                                OnClientClick='openMedical(-1, "", "", "", ""); return false;' />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xs-12 col-lg-6">
                    <div class="panel NoGutters" style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 15px;">
                        <div class="panelheader">
                            <h2>Limitations</h2>
                            <div class="panel-body">
                                <div class="panel-container" style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px;">
                                    <div class="row" style="padding-left: 15px; padding-right: 15px;">
                                        <asp:GridView runat="server" ID="gvLimitations" AutoGenerateColumns="false" GridLines="None"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                                            CssClass="table table-striped table-hover table-condensed col-sm-12">
                                            <Columns>
                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:TemplateField HeaderText="Show Staff" ItemStyle-HorizontalAlign="center" ItemStyle-Wrap="false" ItemStyle-Width="30px" HeaderStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <a href="#" data-toggle="tooltip" title="Should this be displayed to staff ?">
                                                            <asp:Image ID="imgDisplayShowStaff" runat="server"
                                                                ImageUrl='<%# Boolean.Parse(Eval("ShareInfo").ToString()) ? "../img/checkbox.png" : "../img/uncheckbox.png" %>' ToolTip="Should this be displayed to staff ?" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="60px" HeaderText="&nbsp;&nbsp;&nbsp;Print&nbsp; On Card" HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="center" ItemStyle-Wrap="false" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <a href="#" data-toggle="tooltip" title="Should this be displayed to staff ?">
                                                            <asp:Image ID="imgDisplayPrintOnCard" runat="server"
                                                                ImageUrl='<%# Boolean.Parse(Eval("PrintOnCard").ToString()) ? "../img/checkbox.png" : "../img/uncheckbox.png" %>' ToolTip="Should this be displayed to staff ?" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/Edit.gif" Width="16px"
                                                            OnClientClick='<%# Eval("JavaScriptEdit") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/delete.png" Width="16px"
                                                            OnClientClick='<%# Eval("JavaScriptDelete") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                            <asp:Button ID="btnAddAffiliation" runat="server" Text="Add" CssClass="StandardButton" Width="100"
                                                OnClientClick='openLimitations(-1, "", "", "", "", ""); return false;' />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 15px;">
                        <div class="panelheader">
                            <h2>Allergies</h2>
                            <div class="panel-body">
                                <div class="panel-container" style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px;">
                                    <div class="row" style="padding-right: 15px;">
                                        <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                            <asp:TextBox ID="tbAllergies" runat="server" TextMode="MultiLine" CssClass="TableTextBoxWithPadding col-xs-12" Rows="4" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Comments</h2>
                            <div class="panel-body">
                                <div class="panel-container" style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px;">
                                    <div class="row" style="padding-right: 15px;">
                                        <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                            <asp:TextBox ID="tbMedicalComments" runat="server" TextMode="MultiLine" CssClass="TableTextBoxWithPadding col-xs-12" Rows="4" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 text-right" style="margin-top: 20px; padding-left: 15px; padding-right: 30px;">
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

    <div class="modal" id="modalMedical" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Medical Information
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="form-group">
                        <label for="tbCondition">Medical Condition</label>
                        <asp:TextBox ID="tbCondition" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="tbMedication">Medication</label>
                        <asp:TextBox ID="tbMedication" runat="server" CssClass="form-control" />
                    </div>
                    <div class="row">
                        <div class="col-lg-6 form-group">
                            <label for="<%= tbMedicalStartDate.ClientID %>">Start Date</label>
                            <asp:TextBox ID="tbMedicalStartDate" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-lg-6 form-group">
                            <label for="<%= tbMedicalEndDate.ClientID %>">End Date</label>
                            <asp:TextBox ID="tbMedicalEndDate" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="row" style="padding-left: 0px;">
                        <div class="col-xs-6 form-group" style="padding-left: 0px;">
                            <input type="checkbox" name="cbxShareWithStaff" id="cbxShareWithStaff" runat="server" />
                            <div class="btn-group col-xs-12">
                                <label for="<%= cbxShareWithStaff.ClientID%>" class="btn btn-default">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <span class="glyphicon glyphicon-unchecked"></span>
                                </label>
                                <label for="<%= cbxShareWithStaff.ClientID%>" class="btn btn-default active">
                                    Show Staff
                                   
                                </label>
                            </div>
                        </div>
                        <div class="col-xs-6 form-group" style="padding-left: 0px;">
                            <input type="checkbox" name="cbxPrintOnCard" id="cbxPrintOnCard" runat="server" />
                            <div class="btn-group col-xs-12">
                                <label for="<%= cbxPrintOnCard.ClientID %>" class="btn btn-default">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <span class="glyphicon glyphicon-unchecked"></span>
                                </label>
                                <label for="<%= cbxPrintOnCard.ClientID %>" class="btn btn-default active">
                                    Print On Card
                                   
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCloseMedical" runat="server" CssClass="StandardButton" Width="100" Text="Close" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidPlayerMedicalID" runat="server" />
                        <asp:Button ID="btnSaveMedical" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSaveMedical_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalMedicalDelete" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Medical Information - Delete
                </div>

                <div class="modal-body" style="background-color: white;">
                    <div class="container-fluid" style="margin-left: -30px; margin-right: -30px">
                        <label class="col-xs-12">Medical Condition</label>
                        <span class="col-xs-12" id="lblDeleteCondition"></span>
                        <label class="col-xs-12" style="padding-top: 20px;">Medication</label>
                        <span class="col-xs-12" id="lblDeleteMedication"></span>
                        <span class="col-xs-12" style="padding-top: 40px">Are you sure you want to delete this record ?</span>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCancelDeleteMedical" runat="server" CssClass="StandardButton" Width="100" Text="Cancel" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidDeleteMedicalID" runat="server" />
                        <asp:Button ID="btnDeleteMedical" runat="server" Text="Delete" Width="100px" CssClass="StandardButton" OnClick="btnDeleteMedical_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalLimitation" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Limitations
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="form-group">
                        <label for="tbLimitation">Player Limitation</label>
                        <asp:TextBox ID="tbLimitation" runat="server" CssClass="form-control" />
                    </div>

                    <div class="row">
                        <div class="col-lg-6 form-group">
                            <label for="<%= tbLimitationStartDate.ClientID %>">Start Date</label>
                            <asp:TextBox ID="tbLimitationStartDate" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-lg-6 form-group">
                            <label for="<%= tbLimitationEndDate.ClientID %>">End Date</label>
                            <asp:TextBox ID="tbLimitationEndDate" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="row" style="padding-left: 0px;">
                        <div class="col-xs-6 form-group" style="padding-left: 0px;">
                            <input type="checkbox" name="cbxLimitShareWithStaff" id="cbxLimitShareWithStaff" runat="server" />
                            <div class="btn-group col-xs-12">
                                <label for="<%= cbxLimitShareWithStaff.ClientID%>" class="btn btn-default">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <span class="glyphicon glyphicon-unchecked"></span>
                                </label>
                                <label for="<%= cbxLimitShareWithStaff.ClientID%>" class="btn btn-default active">
                                    Show Staff
                                   
                                </label>
                            </div>
                        </div>
                        <div class="col-xs-6 form-group" style="padding-left: 0px;">
                            <input type="checkbox" name="cbxLimitPrintOnCard" id="cbxLimitPrintOnCard" runat="server" />
                            <div class="btn-group col-xs-12">
                                <label for="<%= cbxLimitPrintOnCard.ClientID %>" class="btn btn-default">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <span class="glyphicon glyphicon-unchecked"></span>
                                </label>
                                <label for="<%= cbxLimitPrintOnCard.ClientID %>" class="btn btn-default active">
                                    Print On Card
                                   
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCloseLimit" runat="server" CssClass="StandardButton" Width="100" Text="Close" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidPlayerLimitID" runat="server" />
                        <asp:Button ID="btnSaveLimitation" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSaveLimit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalLimitationsDelete" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Limitation - Delete
                </div>

                <div class="modal-body" style="background-color: white;">
                    <div class="container-fluid" style="margin-left: -30px; margin-right: -30px">
                        <label class="col-xs-12">PlayerLimitation</label>
                        <span class="col-xs-12" id="lblDeletePlayerLimitation"></span>
                        <span class="col-xs-12" style="padding-top: 40px">Are you sure you want to delete this record ?</span>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCancelDeleteLimit" runat="server" CssClass="StandardButton" Width="100" Text="Cancel" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidDeleteLimitID" runat="server" />
                        <asp:Button ID="btnDeleteLimit" runat="server" Text="Delete" Width="100px" CssClass="StandardButton" OnClick="btnDeleteLimit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidPlayerProfileID" runat="server" />

    <script type="text/javascript">
        $(function () {
            $('#<%= tbMedicalStartDate.ClientID %>').datepicker();
            $('#<%= tbMedicalEndDate.ClientID %>').datepicker();
            $('#<%= tbLimitationStartDate.ClientID %>').datepicker();
            $('#<%= tbLimitationEndDate.ClientID %>').datepicker();
        });
    </script>

</asp:Content>
