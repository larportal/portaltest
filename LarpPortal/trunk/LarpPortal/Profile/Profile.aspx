<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Profile.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LarpPortal.Profile.Profile" %>

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


        div {
            border: 0px solid black;
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

    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>

    <script type="text/javascript">
        function DisplaySexOther(SexDropDownList) {
            var Gender = SexDropDownList.options[SexDropDownList.selectedIndex].value;
            if (Gender != null) {
                var OtherBox = document.getElementById('<%= tbGenderOther.ClientID %>');
                if (Gender == 'O') {
                    OtherBox.style.visibility = 'visible';
                    OtherBox.focus();
                }
                else
                    OtherBox.style.visibility = 'hidden';
            }
        }

        function DisplayPhoneProvider(ddlPhoneType) {
            var PhoneType = ddlPhoneType.options[ddlPhoneType.selectedIndex].text;
            if (PhoneType != null) {
                var ddlEnterProvider = document.getElementById('<%= ddlEnterProvider.ClientID %>');
                var lblEnterProvider = document.getElementById("lblEnterProvider");
                if (PhoneType.startsWith("Mo")) {
                    ddlEnterProvider.style.visibility = 'visible';
                    lblEnterProvider.style.visibility = 'visible';
                    ddlEnterProvider.focus();
                }
                else {
                    ddlEnterProvider.style.visibility = 'hidden';
                    lblEnterProvider.style.visibility = 'hidden';
                }
            }
        }

        function openPhoneModal(PhoneID, AreaCode, PhoneNumber, Extension, Primary, PhoneType, PhoneProvider) {
            $('#modalPhoneNumber').modal('show');
            try {
                var hidEnterPhoneID = document.getElementById('<%= hidEnterPhoneID.ClientID %>');
                if (hidEnterPhoneID) {
                    hidEnterPhoneID.value = "-1";
                    if (PhoneID)
                        hidEnterPhoneID.value = PhoneID;
                }

                var tbEnterAreaCode = document.getElementById('<%= tbEnterAreaCode.ClientID %>');
                if (tbEnterAreaCode) {
                    tbEnterAreaCode.value = "";
                    if (AreaCode)
                        tbEnterAreaCode.value = AreaCode;
                    tbEnterAreaCode.focus();
                }

                var tbEnterPhoneNumber = document.getElementById('<%= tbEnterPhoneNumber.ClientID %>');
                if (tbEnterPhoneNumber) {
                    tbEnterPhoneNumber.value = "";
                    if (PhoneNumber)
                        tbEnterPhoneNumber.value = PhoneNumber;
                }

                var tbEnterExtension = document.getElementById('<%= tbEnterExtension.ClientID %>');
                if (tbEnterExtension) {
                    tbEnterExtension.value = "";
                    if (Extension)
                        tbEnterExtension.value = Extension;
                }

                var cbxEnterPrimary = document.getElementById('<%= cbxEnterPrimary.ClientID %>');
                if (cbxEnterPrimary) {
                    cbxEnterPrimary.checked = false;
                    if (Primary == "True")
                        cbxEnterPrimary.checked = true;
                }

                var ddlEnterPhoneType = document.getElementById('<%= ddlEnterPhoneType.ClientID %>');
                if (ddlEnterPhoneType) {
                    ddlEnterPhoneType.options[0].selected = true
                    if (PhoneType)
                        setSelectedValue(ddlEnterPhoneType, PhoneType);
                }

                var ddlEnterProvider = document.getElementById('<%= ddlEnterProvider.ClientID %>');
                if (ddlEnterProvider) {
                    ddlEnterProvider.options[0].selected = true;
                    if (PhoneProvider)
                        setSelectedValue(ddlEnterProvider, PhoneProvider);
                }

                DisplayPhoneProvider(ddlEnterPhoneType)
            }
            catch (err) {
                var t = err.message;
            }
            return false;
        }

        function openPhoneDeleteModal(PhoneID, AreaCode, PhoneNumber) {
            $('#modalPhoneNumberDelete').modal('show');
            try {
                var hidDeletePhoneID = document.getElementById('<%= hidDeletePhoneID.ClientID %>');
                if (hidDeletePhoneID) {
                    hidDeletePhoneID.value = "-1";
                    if (PhoneID)
                        hidDeletePhoneID.value = PhoneID;
                }

                var lblDeletePhoneNumber = document.getElementById('lblDeletePhoneNumber');
                if (lblDeletePhoneNumber) {
                    lblDeletePhoneNumber.value = "";
                    lblDeletePhoneNumber.innerText = "(" + AreaCode + ") " + PhoneNumber.substring(0, 3) + "-" + PhoneNumber.substring(3, 7);
                }
            }
            catch (err) {
                var t = err.message;
            }

            return false;
        }


        function openEMailModal(EMailID, EMailAddress, EMailType, Primary) {
            $('#modalEMail').modal('show');
            try {
                var hidEnterEMailID = document.getElementById('<%= hidEnterEMailID.ClientID %>');
                if (hidEnterEMailID) {
                    hidEnterEMailID.value = "-1";
                    if (EMailID)
                        hidEnterEMailID.value = EMailID;
                }

                var tbEnterEMailAddress = document.getElementById('<%= tbEnterEMailAddress.ClientID %>');
                if (tbEnterEMailAddress) {
                    tbEnterEMailAddress.value = "";
                    if (EMailAddress)
                        tbEnterEMailAddress.value = EMailAddress;
                    tbEnterEMailAddress.focus();
                }

                var ddlEnterEMailType = document.getElementById('<%= ddlEnterEMailType.ClientID %>');
                if (ddlEnterEMailType) {
                    ddlEnterEMailType.options[0].selected = true
                    if (EMailType)
                        setSelectedValue(ddlEnterEMailType, EMailType);
                }

                var cbxEnterEMailPrimary = document.getElementById('<%= cbxEnterEMailPrimary.ClientID %>');
                if (cbxEnterEMailPrimary) {
                    cbxEnterEMailPrimary.checked = false;
                    if (Primary == "True")
                        cbxEnterEMailPrimary.checked = true;
                }
            }
            catch (err) {
                var t = err.message;
            }
            return false;
        }

        function openEmailDeleteModal(EMailID, EMail) {
            $('#modalEMailDelete').modal('show');
            try {
                var hidDeleteEMailID = document.getElementById('<%= hidDeleteEMailID.ClientID %>');
                if (hidDeleteEMailID) {
                    hidDeleteEMailID.value = "-1";
                    if (EMailID)
                        hidDeleteEMailID.value = EMailID;
                }

                var lblDeleteEMail = document.getElementById('lblDeleteEMail');
                if (lblDeleteEMail) {
                    lblDeleteEMail.value = "";
                    lblDeleteEMail.innerText = EMail;
                }
            }
            catch (err) {
                var t = err.message;
            }

            return false;
        }

        function openAddressModal(AddressID, Address1, Address2, City, StateID, PostalCode, Country, AddressType, Primary) {
            $('#modalAddress').modal('show');
            try {
                var hidAddressID = document.getElementById('<%= hidAddressID.ClientID %>');
                if (hidAddressID) {
                    hidAddressID.value = "-1";
                    if (AddressID)
                        hidAddressID.value = AddressID;
                }

                var tbEnterAddress1 = document.getElementById('<%= tbEnterAddress1.ClientID %>');
                if (tbEnterAddress1) {
                    tbEnterAddress1.value = "";
                    if (Address1)
                        tbEnterAddress1.value = Address1;
                    tbEnterAddress1.focus();
                }

                var tbEnterAddress2 = document.getElementById('<%= tbEnterAddress2.ClientID %>');
                if (tbEnterAddress2) {
                    tbEnterAddress2.value = "";
                    if (Address2)
                        tbEnterAddress2.value = Address2;
                }

                var tbEnterCity = document.getElementById('<%= tbEnterCity.ClientID %>');
                if (tbEnterCity) {
                    tbEnterCity.value = "";
                    if (City)
                        tbEnterCity.value = City;
                }

                var ddlEnterState = document.getElementById('<%= ddlEnterState.ClientID %>');
                if (ddlEnterState) {
                    ddlEnterState.options[0].selected = true
                    if (StateID)
                        setSelectedValue(ddlEnterState, StateID);
                }

                var tbEnterZipCode = document.getElementById('<%= tbEnterZipCode.ClientID %>');
                if (tbEnterZipCode) {
                    tbEnterZipCode.value = "";
                    if (PostalCode)
                        tbEnterZipCode.value = PostalCode;
                }

                var tbEnterCountry = document.getElementById('<%= tbEnterCountry.ClientID %>');
                if (tbEnterCountry) {
                    tbEnterCountry.value = "";
                    if (Country)
                        tbEnterCountry.value = Country;
                }

                var ddlEnterState = document.getElementById('<%= ddlEnterState.ClientID %>');
                if (ddlEnterAddressType) {
                    ddlEnterAddressType.options[0].selected = true
                    if (AddressType)
                        setSelectedValue(ddlEnterAddressType, AddressType);
                }

                var cbxEnterAddressPrimary = document.getElementById('<%= cbxEnterAddressPrimary.ClientID %>');
                if (cbxEnterAddressPrimary) {
                    cbxEnterAddressPrimary.checked = false;
                    if (Primary == "True")
                        cbxEnterAddressPrimary.checked = true;
                }
            }
            catch (err) {
                var t = err.message;
            }

            return false;
        }

        function openAddressDeleteModal(AddressID, Address1, Address2, City, StateID, PostalCode, Country, AddressType, Primary) {
            $('#modalAddressDelete').modal('show');
            try {
                var hidDeleteAddressID = document.getElementById('<%= hidDeleteAddressID.ClientID %>');
                if (hidDeleteAddressID) {
                    hidDeleteAddressID.value = "-1";
                    if (AddressID)
                        hidDeleteAddressID.value = AddressID;
                }

                var DisplayMessage = Address1 + "<br>";
                if (Address2.length > 0)
                    DisplayMessage += Address2 + "<br>";
                DisplayMessage += City + " " + StateID + ", " + PostalCode;

                var lblDeleteAddress = document.getElementById('lblDeleteAddress');
                if (lblDeleteAddress) {
                    lblDeleteAddress.value = "";
                    lblDeleteAddress.innerHTML = DisplayMessage;
                }
            }
            catch (err) {
                var t = err.message;
            }

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

</asp:Content>
<asp:Content ID="MainPage" ContentPlaceHolderID="ProfileMain" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <div class="col-sm-4">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="My Profile - Demographics" />
            </div>
            <div class="col-sm-8 text-right">
                <asp:Label ID="lblErrorMessage1" runat="server" Text="" Visible="true"
                    Style="margin-right: 10px;" ForeColor="Red" Font-Italic="true" Font-Size="Larger" Font-Bold="true" />
                <asp:Button ID="btnSave1" runat="server" Text="Save" CssClass="StandardButton" Width="100px" OnClick="btnSave_Click" />
            </div>
        </div>

        <section id="character-info" class="character-info tab-pane active">
            <div class="row" style="padding-left: 15px;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2>Personal Information</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria">
                                <table class="CharInfoTable" border="0">
                                    <tr>
                                        <td class="TableLabel col-sm-1">Name</td>
                                        <td class="col-sm-2" colspan="3">
                                            <asp:TextBox ID="tbFirstName" runat="server" CssClass="TableTextBox col-sm-12" /></td>
                                        <td class="col-sm-1">
                                            <asp:TextBox ID="tbMiddleInit" runat="server" CssClass="TableTextBox" /></td>
                                        <td class="col-sm-2" colspan="2">
                                            <asp:TextBox ID="tbLastName" runat="server" CssClass="TableTextBox" /></td>
                                        <td rowspan="4" style="width: 35px;">&nbsp;</td>
                                        <td>To add a profile picture, use the browse button below.
                                        </td>
                                        <td rowspan="4" style="text-align: center;">
                                            <asp:Image ID="imgPlayerImage" runat="server" Width="150" /></td>
                                    </tr>

                                    <tr>
                                        <td class="TableLabel">Gender</td>
                                        <td colspan="1">
                                            <asp:DropDownList ID="ddlGender" runat="server">
                                                <asp:ListItem Text="" Value="" />
                                                <asp:ListItem Text="Male" Value="M" />
                                                <asp:ListItem Text="Female" Value="F" />
                                                <asp:ListItem Text="Other" Value="O" />
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbGenderOther" runat="server" CssClass="TableTextBox" /></td>
                                        <td class="TableLabel">DOB</td>
                                        <td>
                                            <asp:TextBox ID="tbDOB" runat="server" CssClass="TableTextBox" /></td>
                                        <td colspan="2">&nbsp;
                                        </td>
                                        <%--                                        <td class="TableLabel">Birthplace</td>
                                        <td>
                                            <asp:TextBox ID="tbBirthPlace" runat="server" CssClass="TableTextBox" />
                                        </td>--%>
                                        <td>
                                            <asp:FileUpload ID="ulFile" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TableLabel">User Name</td>
                                        <td colspan="2">
                                            <asp:TextBox ID="tbUserName" runat="server" CssClass="TableTextBox" /></td>
                                        <td class="TableLabel">Nick Name</td>
                                        <td>
                                            <asp:TextBox ID="tbNickName" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td class="TableLabel">Pen Name</td>
                                        <td>
                                            <asp:TextBox ID="tbPenName" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="StandardButton" OnClick="btnSavePicture_Click" Width="100" /></td>
                                    </tr>

                                    <tr style="vertical-align: top;">
                                        <td class="TableLabel">Forum Name</td>
                                        <td colspan="2">
                                            <asp:TextBox ID="tbForumName" runat="server" CssClass="TableTextBox" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="3" class="text-left TableLabel" style="text-align: left;">Emergency Contact Info</td>
                                        <td colspan="6"></td>
                                        <td style="text-align: center;">
                                            <asp:Button ID="btnClearPicture" runat="server" CssClass="StandardButton" Width="100" Text="Clear Picture" OnClick="btnClearPicture_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableLabel">Name</td>
                                        <td class="col-sm-2" colspan="3">
                                            <asp:TextBox ID="tbEmergencyName" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td class="TableLabel">Phone</td>
                                        <td colspan="4">
                                            <asp:TextBox ID="tbEmergencyPhone" runat="server" CssClass="TableTextBox col-sm-2" Columns="15" />
                                            <ajaxToolkit:MaskedEditExtender ID="meeEmergencyPhone" runat="server" TargetControlID="tbEmergencyPhone" ClearMaskOnLostFocus="false" Mask="(999) 999-9999" />
                                            <%--                                            <asp:RegularExpressionValidator ID="revEmergencyPhone" runat="server" ValidationExpression="^[\s\S]{10}$" ControlToValidate="tbEmergencyPhone"
                                                ErrorMessage="* Enter all 10 digits of the phone number." Font-Italic="true" ForeColor="Red" Font-Bold="true" Display="Dynamic" />--%>
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
                        <h2>Addresses</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria" style="padding-bottom: 0px; margin-bottom: 0px;">
                                <asp:GridView ID="gvAddresses" runat="server" AutoGenerateColumns="false" Width="100%" TabIndex="14" GridLines="none" Style="padding-bottom: 0px; margin-bottom: 0px;"
                                    OnRowDataBound="gvAddresses_RowDataBound" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" CssClass="table table-striped table-hover table-condensed">
                                    <RowStyle VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:BoundField DataField="Address1" HeaderText="Address 1" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="Address2" HeaderText="Address 2" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="StateID" HeaderText="State" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="PostalCode" HeaderText="Postal/Zip Code" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="Country" HeaderText="Country" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="AddressType" HeaderText="Address Type" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="Primary" ItemStyle-Width="30px" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("IsPrimary").ToString() == "True" ? "Yes" : "No" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right" ItemStyle-Width="46px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                    ImageUrl="~/img/Edit.gif" Width="16px" Height="16px"
                                                    OnClientClick='<%# string.Format("return openAddressModal(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\"); return false;",
                                                        Eval("AddressID"), Eval("Address1"), Eval("Address2"), Eval("City"), Eval("StateID"), Eval("PostalCode"),
                                                        Eval("Country"), Eval("AddressType"), Eval("IsPrimary")) %>' />
                                                <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                    ImageUrl="~/img/delete.png" Width="16px" Height="16px"
                                                    OnClientClick='<%# string.Format("return openAddressDeleteModal(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\"); return false;",
                                                        Eval("AddressID"), Eval("Address1"), Eval("Address2"), Eval("City"), Eval("StateID"), Eval("PostalCode"),
                                                        Eval("Country"), Eval("AddressType"), Eval("IsPrimary")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="width: 100%; text-align: right; padding-top: 10px; padding-bottom: 0px; margin-bottom: 10px; padding-right: 0px; margin-right: 0px;">
                                    <asp:Button ID="btnAddAddress" runat="server" Text="Add Address" Width="150px" CssClass="StandardButton" OnClientClick="openAddressModal(); return false;"
                                        Style="padding-right: 0px; margin-right: 0px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <br />

            <div class="row" style="padding-left: 15px;">
                <div class="panel col-sm-6" style="padding-top: 0px; padding-bottom: 0px; padding-right: 0px; padding-left: 0px;">
                    <div class="panelheader col-sm-12" style="padding-top: 0px; padding-bottom: 0px; padding-right: 0px; padding-left: 0px;">
                        <h2>Phone Numbers</h2>
                        <div class="panel-body col-sm-12" style="padding-top: 0px; padding-bottom: 0px; padding-right: 0px; padding-left: 0px;">
                            <div class="panel-container search-criteria col-sm-12" style="padding-top: 10px; padding-bottom: 0px; padding-right: 15px; padding-left: 15px; margin-bottom: 0px;">
                                <asp:GridView ID="gvPhoneNumbers" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="none" Style="padding-bottom: 0px; margin-bottom: 0px;"
                                    OnRowDataBound="gvPhoneNumbers_RowDataBound"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" DataKeyNames="PhoneNumberID" CssClass="table table-striped table-hover table-condensed">
                                    <RowStyle VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:BoundField DataField="AreaCode" HeaderText="Area Code" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="Extension" HeaderText="Extension" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="PhoneType" HeaderText="Type" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:TemplateField HeaderText="Primary" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("IsPrimary").ToString() == "True" ? "Yes" : "No" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Provider" HeaderText="Provider" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right" ItemStyle-Width="46px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                    ImageUrl="~/img/Edit.gif" Width="16px" Height="16px"
                                                    OnClientClick='<%# string.Format("return openPhoneModal(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\"); return false;",
                                                        Eval("PhoneNumberID"), Eval("AreaCode"), Eval("PhoneNumber"), Eval("Extension"), Eval("IsPrimary"), Eval("PhoneTypeID"),
                                                        Eval("ProviderID")) %>' />
                                                <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                    ImageUrl="~/img/delete.png" Width="16px" Height="16px"
                                                    OnClientClick='<%# string.Format("return openPhoneDeleteModal(\"{0}\", \"{1}\", \"{2}\"); return false;",
                                                        Eval("PhoneNumberID"), Eval("AreaCode"), Eval("PhoneNumber")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="width: 100%; text-align: right; padding-top: 10px; padding-bottom: 0px; margin-bottom: 10px; padding-right: 0px; margin-right: 0px;">
                                    <asp:Button ID="btnAddPhoneNumber" runat="server" Text="Add Phone Number" Width="150px" CssClass="StandardButton" OnClientClick="openPhoneModal(); return false;"
                                        Style="padding-right: 0px; margin-right: 0px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-0-5">
                    &nbsp;
                </div>

                <div class="panel col-sm-5-5" style="padding-top: 0px; padding-bottom: 0px; padding-right: 0px; padding-left: 0px;">
                    <div class="panelheader col-sm-12" style="padding-top: 0px; padding-bottom: 0px; padding-right: 0px; padding-left: 0px;">
                        <h2>Email Addresses</h2>
                        <div class="panel-body col-sm-12" style="padding-top: 0px; padding-bottom: 0px; padding-right: 0px; padding-left: 0px;">
                            <div class="panel-container search-criteria col-sm-12" style="padding-top: 10px; padding-bottom: 0px; padding-right: 15px; padding-left: 15px; margin-bottom: 0px;">
                                <asp:GridView ID="gvEmails" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="none" Style="padding-bottom: 0px; margin-bottom: 0px;"
                                    OnRowDataBound="gvEmails_RowDataBound" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" CssClass="table table-striped table-hover table-condensed">
                                    <RowStyle VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:BoundField DataField="EMailAddress" HeaderText="Email Address" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:BoundField DataField="EMailType" HeaderText="Type" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" />
                                        <asp:TemplateField HeaderText="Primary" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("IsPrimary").ToString() == "True" ? "Yes" : "No" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                    ImageUrl="~/img/Edit.gif" Width="16px" Height="16px"
                                                    OnClientClick='<%# string.Format("return openEMailModal(\"{0}\", \"{1}\", \"{2}\", \"{3}\"); return false;",
                                                        Eval("EMailID"), Eval("EmailAddress"), Eval("EmailTypeID"), Eval("IsPrimary")) %>' />
                                                <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                    ImageUrl="~/img/delete.png" Width="16px" Height="16px"
                                                    OnClientClick='<%# string.Format("return openEmailDeleteModal(\"{0}\", \"{1}\"); return false;",
                                                        Eval("EMailID"), Eval("EMailAddress")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="width: 100%; text-align: right; padding-top: 10px; padding-bottom: 0px; margin-bottom: 10px; padding-right: 0px; margin-right: 0px;">
                                    <asp:Button ID="btnAddEmail" runat="server" Text="Add EMail" Width="125px" CssClass="StandardButton" OnClientClick='openEMailModal("-1"); return false;'
                                        Style="padding-right: 0px; margin-right: 0px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" style="padding-left: 15px; text-align: right; padding-top: 10px;">
                <asp:Label ID="lblErrorMessage2" runat="server" Text="" Visible="true"
                    Style="margin-right: 10px;" ForeColor="Red" Font-Italic="true" Font-Size="Larger" Font-Bold="true" />
                <asp:Button ID="btnSave2" runat="server" Text="Save" CssClass="StandardButton" Width="100px" OnClick="btnSave_Click" />
            </div>
        </section>
    </div>

    <div class="modal" id="myModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Profile Demographics
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnClose_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalPhoneNumber" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Phone Number
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="row">
                        <div class="col-lg-3 form-group">
                            <label for="<%= tbEnterAreaCode.ClientID %>">Area Code</label>
                            <asp:TextBox ID="tbEnterAreaCode" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-lg-6 form-group">
                            <label for="<%= tbEnterPhoneNumber.ClientID %>">Phone Number</label>
                            <asp:TextBox ID="tbEnterPhoneNumber" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-lg-3 form-group">
                            <label for="<%= tbEnterExtension.ClientID %>">Extension</label>
                            <asp:TextBox ID="tbEnterExtension" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 form-group" style="padding-top: 23px; padding-left: 0px; padding-right: 0px;">
                            <input type="checkbox" name="cbxEnterPrimary" id="cbxEnterPrimary" runat="server" />
                            <div class="btn-group col-xs-12" style="padding-right: 0px;">
                                <label for="<%= cbxEnterPrimary.ClientID%>" class="btn btn-default">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <span class="glyphicon glyphicon-unchecked"></span>
                                </label>
                                <label for="<%= cbxEnterPrimary.ClientID%>" class="btn btn-default active">
                                    Primary
                                   
                                </label>
                            </div>
                        </div>
                        <div class="col-lg-3 form-group">
                            <label for="<%= ddlEnterPhoneType.ClientID %>">Phone Type</label>
                            <asp:DropDownList ID="ddlEnterPhoneType" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-xs-6 form-group">
                            <label id="lblEnterProvider" for="<%= ddlEnterProvider.ClientID %>">Provider</label>
                            <asp:DropDownList ID="ddlEnterProvider" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnClosePhoneNumber" runat="server" CssClass="StandardButton" Width="100" Text="Close" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidEnterPhoneID" runat="server" />
                        <asp:Button ID="btnSavePhone" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSavePhone_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalPhoneNumberDelete" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Phone Number - Delete
                </div>

                <div class="modal-body" style="background-color: white;">
                    <div class="container-fluid" style="margin-left: -30px; margin-right: -30px">
                        <label class="col-xs-12">Phone Number</label>
                        <span class="col-xs-12" id="lblDeletePhoneNumber"></span>
                        <span class="col-xs-12" style="padding-top: 40px">Are you sure you want to delete this record ?</span>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCancelDeletePhoneNumber" runat="server" CssClass="StandardButton" Width="100" Text="Cancel" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidDeletePhoneID" runat="server" />
                        <asp:Button ID="btnDeletePhone" runat="server" Text="Delete" Width="100px" CssClass="StandardButton" OnClick="btnDeletePhone_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal" id="modalEMail" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    EMail
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="row">
                        <div class="col-lg-12 form-group">
                            <label for="<%= tbEnterEMailAddress.ClientID %>">EMail Address</label>
                            <asp:TextBox ID="tbEnterEMailAddress" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 form-group">
                            <label for="<%= ddlEnterEMailType.ClientID %>">EMail Type</label>
                            <asp:DropDownList ID="ddlEnterEMailType" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 form-group" style="padding-left: 0px; padding-right: 0px;">
                            <input type="checkbox" name="cbxEnterEMailPrimary" id="cbxEnterEMailPrimary" runat="server" />
                            <div class="btn-group col-xs-12" style="padding-right: 0px;">
                                <label for="<%= cbxEnterEMailPrimary.ClientID%>" class="btn btn-default">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <span class="glyphicon glyphicon-unchecked"></span>
                                </label>
                                <label for="<%= cbxEnterEMailPrimary.ClientID%>" class="btn btn-default active">
                                    Primary
                                   
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCloseEnterEMail" runat="server" CssClass="StandardButton" Width="100" Text="Close" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidEnterEMailID" runat="server" />
                        <asp:Button ID="btnSaveEMail" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSaveEMail_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalEMailDelete" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    EMail - Delete
                </div>

                <div class="modal-body" style="background-color: white;">
                    <div class="container-fluid" style="margin-left: -30px; margin-right: -30px">
                        <label class="col-xs-12">EMail Address</label>
                        <span class="col-xs-12" id="lblDeleteEMail"></span>
                        <span class="col-xs-12" style="padding-top: 40px">Are you sure you want to delete this record ?</span>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCancelDeleteEMail" runat="server" CssClass="StandardButton" Width="100" Text="Cancel" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidDeleteEMailID" runat="server" />
                        <asp:Button ID="btnDeleteEMail" runat="server" Text="Delete" Width="100px" CssClass="StandardButton" OnClick="btnDeleteEMail_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalAddress" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Address
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="row">
                        <div class="col-lg-12 form-group">
                            <label for="<%= tbEnterAddress1.ClientID %>">Address 1</label>
                            <asp:TextBox ID="tbEnterAddress1" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12 form-group">
                            <label for="<%= tbEnterAddress2.ClientID %>">Address 2</label>
                            <asp:TextBox ID="tbEnterAddress2" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-4 form-group">
                            <label for="<%= tbEnterCity.ClientID %>">City</label>
                            <asp:TextBox ID="tbEnterCity" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-lg-4 form-group">
                            <label for="<%= ddlEnterState.ClientID %>">State</label>
                            <asp:DropDownList ID="ddlEnterState" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-lg-4 form-group">
                            <label for="<%= tbEnterZipCode.ClientID %>">Zip Code</label>
                            <asp:TextBox ID="tbEnterZipCode" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 form-group">
                            <label for="<%= tbEnterCountry.ClientID %>">Country</label>
                            <asp:TextBox ID="tbEnterCountry" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-lg-4 form-group">
                            <label for="<%= ddlEnterAddressType.ClientID %>">Type</label>
                            <asp:DropDownList ID="ddlEnterAddressType" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-lg-4 form-group" style="padding-top: 23px; padding-left: 0px; padding-right: 0px;">
                            <input type="checkbox" name="cbxEnterAddressPrimary" id="cbxEnterAddressPrimary" runat="server" />
                            <div class="btn-group col-xs-12" style="padding-right: 0px;">
                                <label for="<%= cbxEnterAddressPrimary.ClientID%>" class="btn btn-default">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <span class="glyphicon glyphicon-unchecked"></span>
                                </label>
                                <label for="<%= cbxEnterAddressPrimary.ClientID%>" class="btn btn-default active">
                                    Primary
                                   
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCloseEnterAddress" runat="server" CssClass="StandardButton" Width="100" Text="Close" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidAddressID" runat="server" />
                        <asp:Button ID="btnSaveAddress" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSaveAddress_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalAddressDelete" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Address - Delete
                </div>

                <div class="modal-body" style="background-color: white;">
                    <div class="container-fluid" style="margin-left: -30px; margin-right: -30px">
                        <label class="col-xs-12">Address</label>
                        <span class="col-xs-12" id="lblDeleteAddress"></span>
                        <span class="col-xs-12" style="padding-top: 40px">Are you sure you want to delete this record ?</span>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCancelDeleteAddress" runat="server" CssClass="StandardButton" Width="100" Text="Cancel" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidDeleteAddressID" runat="server" />
                        <asp:Button ID="btnDeleteAddress" runat="server" Text="Delete" Width="100px" CssClass="StandardButton" OnClick="btnDeleteAddress_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:HiddenField ID="hidNumOfPhones" runat="server" />
    <asp:HiddenField ID="hidNumOfEMails" runat="server" />
    <asp:HiddenField ID="hidNumOfAddresses" runat="server" />
</asp:Content>
