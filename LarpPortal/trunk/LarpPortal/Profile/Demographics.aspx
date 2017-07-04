<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Profile.Master" AutoEventWireup="true" CodeBehind="Demographics.aspx.cs" Inherits="LarpPortal.Profile.Demographics" %>

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
                                        <td colspan="2">
                                            &nbsp;
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
                                            <asp:RegularExpressionValidator ID="revEmergencyPhone" runat="server" ValidationExpression="^[\s\S]{10}$" ControlToValidate="tbEmergencyPhone"
                                                ErrorMessage="* Enter all 10 digits of the phone number." Font-Italic="true" ForeColor="Red" Font-Bold="true" Display="Dynamic" />
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
                                    OnRowEditing="gvAddresses_RowEditing" OnRowUpdating="gvAddresses_RowUpdating" OnRowCancelingEdit="gvAddresses_RowCancelingEdit"
                                    OnRowDeleting="gvAddresses_RowDeleting" OnRowDataBound="gvAddresses_RowDataBound"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" DataKeyNames="AddressID" CssClass="table table-striped table-hover table-condensed">
                                    <RowStyle VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Address 1" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Address1") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbAddress1" CssClass="form-control col-sm-4" Text='<%# Eval("Address1") %>' />
                                                <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="tbAddress1" Font-Italic="true"
                                                    ForeColor="Red" Font-Bold="true" Text="* Required" Display="Dynamic" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address 2" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Address2") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbAddress2" CssClass="form-control col-sm-4" Text='<%# Eval("Address2") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("City") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbCity" CssClass="form-control col-sm-2" Text='<%# Eval("City") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="tbCity" Font-Italic="true"
                                                    ForeColor="Red" Font-Bold="true" Text="* Required" Display="Dynamic" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("StateID") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbState" CssClass="form-control col-sm-1" Text='<%# Eval("StateID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Postal/Zip Code" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("PostalCode") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbPostalCode" CssClass="form-control col-sm-2" Text='<%# Eval("PostalCode") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPostalCode" runat="server" ControlToValidate="tbPostalCode" Font-Italic="true"
                                                    ForeColor="Red" Font-Bold="true" Text="* Required" Display="Dynamic" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Country") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbCountry" CssClass="form-control col-sm-2" Text='<%# Eval("Country") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" ItemStyle-Width="100px" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("AddressType") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlAddressType" Width="100px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Primary" ItemStyle-Width="30px" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("IsPrimary").ToString() == "True" ? "Yes" : "No" %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:RadioButton runat="server" ID="rbPrimary" Checked='<%# Eval("IsPrimary") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" Width="100px" CssClass="StandardButton" />
                                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" Width="100px" CssClass="StandardButton" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" Width="100px" CssClass="StandardButton" />
                                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" Width="100px" CausesValidation="false" CssClass="StandardButton" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="width: 100%; text-align: right; padding-top: 10px; padding-bottom: 0px; margin-bottom: 10px; padding-right: 0px; margin-right: 0px;">
                                    <asp:Button ID="btnAddAddress" runat="server" Text="Add Address" Width="150px" CssClass="StandardButton" OnClick="btnAddAddress_Click"
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
                                    OnRowEditing="gvPhoneNumbers_RowEditing" OnRowUpdating="gvPhoneNumbers_RowUpdating" OnRowCancelingEdit="gvPhoneNumbers_RowCancelingEdit"
                                    OnRowDeleting="gvPhoneNumbers_RowDeleting" OnRowDataBound="gvPhoneNumbers_RowDataBound"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" DataKeyNames="PhoneNumberID" CssClass="table table-striped table-hover table-condensed">
                                    <RowStyle VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Area Code" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("AreaCode") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbAreaCode" CssClass="form-control col-sm-4" Text='<%# Eval("AreaCode") %>' />
                                                <asp:RequiredFieldValidator ID="rfvAreaCode" runat="server" ControlToValidate="tbAreaCode" Font-Italic="true"
                                                    ForeColor="Red" Font-Bold="true" Text="* Required" Display="Dynamic" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone Number" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("PhoneNumber") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbPhoneNumber" CssClass="form-control col-sm-4" Text='<%# Eval("PhoneNumber") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ControlToValidate="tbPhoneNumber" Font-Italic="true"
                                                    ForeColor="Red" Font-Bold="true" Text="* Required" Display="Dynamic" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Extension" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Extension") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbExtension" CssClass="form-control col-sm-2" Text='<%# Eval("Extension") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" ItemStyle-Width="100px" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("PhoneType") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlPhoneType" Width="100px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Primary" ItemStyle-Width="30px" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("IsPrimary").ToString() == "True" ? "Yes" : "No" %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:RadioButton runat="server" ID="rbPrimary" Checked='<%# Eval("IsPrimary") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Provider">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Provider") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlProviderList" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" Width="100px" CssClass="StandardButton" />
                                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" Width="100px" CssClass="StandardButton" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" Width="100px" CssClass="StandardButton" />
                                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" Width="100px" CausesValidation="false" CssClass="StandardButton" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="width: 100%; text-align: right; padding-top: 10px; padding-bottom: 0px; margin-bottom: 10px; padding-right: 0px; margin-right: 0px;">
                                    <asp:Button ID="btnAddPhoneNumber" runat="server" Text="Add Phone Number" Width="150px" CssClass="StandardButton" OnClick="btnAddPhoneNumber_Click"
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
                                    OnRowEditing="gvEmails_RowEditing" OnRowUpdating="gvEmails_RowUpdating" OnRowCancelingEdit="gvEmails_RowCancelingEdit"
                                    OnRowDeleting="gvEmails_RowDeleting" OnRowDataBound="gvEmails_RowDataBound"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" DataKeyNames="EMailID" CssClass="table table-striped table-hover table-condensed">
                                    <RowStyle VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Email Address" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("EmailAddress") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbEmailAddress" CssClass="form-control col-sm-4" Text='<%# Eval("EmailAddress") %>' />
                                                <asp:RequiredFieldValidator runat="server" ID="rfvEmailAddress" ControlToValidate="tbEmailAddress" ForeColor="Red"
                                                    Font-Italic="true" Font-Bold="true" Text="* Required" Display="Dynamic" />
                                                <asp:RegularExpressionValidator runat="server" ID="revEmailAddress" ControlToValidate="tbEmailAddress" ForeColor="Red"
                                                    Font-Italic="true" Font-Bold="true" Text="* Invalid email" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" ItemStyle-Width="100px" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("EmailType") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlEmailType" Width="100px" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Primary" ItemStyle-Width="30px" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("IsPrimary").ToString() == "True" ? "Yes" : "No" %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:RadioButton runat="server" ID="rbPrimary" Checked='<%# Eval("IsPrimary") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" Width="100px" CssClass="StandardButton" />
                                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" Width="100px" CssClass="StandardButton" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" Width="100px" CssClass="StandardButton" />
                                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" Width="100px" CausesValidation="false" CssClass="StandardButton" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="width: 100%; text-align: right; padding-top: 10px; padding-bottom: 0px; margin-bottom: 10px; padding-right: 0px; margin-right: 0px;">
                                    <asp:Button ID="btnAddEmail" runat="server" Text="Add EMail" Width="150px" CssClass="StandardButton" OnClick="btnAddEmail_Click"
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
</asp:Content>
