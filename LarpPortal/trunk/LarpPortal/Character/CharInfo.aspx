<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharInfo.aspx.cs" Inherits="LarpPortal.Character.CharInfo" %>

<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="CharHeaderStyles">
    <style type="text/css">
        .TableTextBox {
            border: 1px solid black;
        }

        .TableTextBoxWithPadding {
            border: 1px solid black;
            padding: 2px;
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

        .NoMargins {
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
        }

        .SmallText {
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            font-size: 12px !important;
            /*    border-radius: 3px  */
        }

        /*div {
            border: 1px solid black;
        }*/

        div.tooltip-inner {
            text-align: center;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
            margin-bottom: 0px;
            background-color: white;
            color: black;
            /*    background-color: #505050; */
            font-size: 14px;
            width: 350px;
            box-shadow: 5px 5px 10px #888888;
            max-width: 350px;
            opacity: 1;
            opacity: 1 !important;
            /* If max-width does not work, try using width instead */
            width: 350px;
        }

        .tooltip.in {
            opacity: 1.0;
        }
    </style>
</asp:Content>

<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="CharHeaderScripts">
    <script type="text/javascript">

        function ddlRebuildSetVisible() {
            var ddlAllowRebuild = document.getElementById("<%=ddlAllowRebuild.ClientID %>");
            var tbRebuildToDate = document.getElementById("<%=tbRebuildToDate.ClientID %>");
            var lblExpiresOn = document.getElementById("<%= lblExpiresOn.ClientID %>");
            tbRebuildToDate.style.display = "none";
            lblExpiresOn.style.display = "none";
            if (ddlAllowRebuild)
                if (ddlAllowRebuild.options[ddlAllowRebuild.selectedIndex].value == "Y") {
                    tbRebuildToDate.style.display = "inline";
                    lblExpiresOn.style.display = "inline";
                }
            return false;
        }

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

        function openDeath(CharacterDeathID, DeathDate, Permed, Comments, StaffComments) {
            $('#modalEditUpdateDeath').modal('show');

            var tbDeathDate = document.getElementById('<%= tbDeathDate.ClientID %>');
            tbDeathDate.value = DeathDate;

            var cbxPerm = document.getElementById('<%= cbxDeathPerm.ClientID %>');
            var txtPermed = Permed.charAt(0);
            if (txtPermed == 'T') {
                cbxDeathPerm.checked = true;
            }

            var tbComments = document.getElementById('<%= tbDeathComments.ClientID %>');
            if (Comments)
                tbComments.value = Comments;
            else
                tbComments.value = "";

            var tbStaffComments = document.getElementById('<%= tbDeathStaffComments.ClientID %>');
            if (StaffComments)
                tbStaffComments.value = StaffComments;
            else
                tbStaffComments.value = "";

            var hidDeathID = document.getElementById('<%= hidDeathID.ClientID %>');

            if (hidDeathID) {
                hidDeathID.value = CharacterDeathID;
            }

            return false;
        }
        function closeDeath() {
            $('#modalEditUpdateDeath').hide();
            return false;
        }


        function openActor(CharacterActorID, UserName, Comments, StaffComments, StartDate, EndDate) {
            $('#modalEditUpdateActor').modal('show');

            var tbActorStartDate = document.getElementById('<%= tbActorStartDate.ClientID %>');
            if (StartDate) {
                tbActorStartDate.value = StartDate;
            }
            else {
                tbActorStartDate.value = "";
            }

            var tbActorEndDate = document.getElementById('<%= tbActorEndDate.ClientID %>');
            if (EndDate) {
                tbActorEndDate.value = EndDate;
            }
            else {
                tbActorEndDate.value = "";
            }

            var tbComments = document.getElementById('<%= tbActorComments.ClientID %>');
            tbComments.value = Comments;

            var tbStaffComments = document.getElementById('<%= tbActorStaffComments.ClientID %>');
            tbStaffComments.value = StaffComments;

            var hidActorID = document.getElementById('<%= hidActorID.ClientID %>');

            var ddlActorPlayer = document.getElementById('<%= ddlActorName.ClientID %>');
            if (UserName)
                setSelectedValue(ddlActorPlayer, UserName);

            if (hidActorID) {
                hidActorID.value = CharacterActorID;
            }

            return false;
        }
        function closeActor() {
            $('#modalEditUpdateActor').hide();
            return false;
        }

        function openDeleteDeath(CharacterDeathID, DeathDate) {
            $('#modalDeleteDeath').modal('show');

            var hidDeleteDeathID = document.getElementById('<%= hidDeleteDeathID.ClientID %>');
            if (hidDeleteDeathID)
                hidDeleteDeathID.value = CharacterDeathID;

            var lblDeleteDeathMessage = document.getElementById('<%= lblDeleteDeathMessage.ClientID %>');
            if (lblDeleteDeathMessage)
                if (DeathDate)
                    lblDeleteDeathMessage.innerText = "Are you sure you want to delete death on " + DeathDate + " ?";
                else
                    lblDeleteDeathMessage.innerText = "Are you sure you want to delete this death ?";
            return false;
        }


        function openDeleteActor(CharacterActorID, ActorName) {
            $('#modalDeleteActor').modal('show');

            var hidDeleteActorID = document.getElementById('<%= hidDeleteActorID.ClientID %>');
            if (hidDeleteActorID)
                hidDeleteActorID.value = CharacterActorID;

            var lblDeleteActorMessage = document.getElementById('<%= lblDeleteActorMessage.ClientID %>');
            if (lblDeleteActorMessage)
                if (ActorName)
                    lblDeleteActorMessage.innerText = "Are you sure you want to delete actor " + ActorName + " ?";
                else
                    lblDeleteActorMessage.innerText = "Are you sure you want to delete this actor ?";
            return false;
        }

        function openDeleteDesc(DescID, Descriptor, DescValue) {
            $('#modalDeleteDesc').modal('show');

            var hidDescID = document.getElementById('<%= hidDescID.ClientID %>');
            if (hidDescID)
                hidDescID.value = DescID;

            var lblDeleteDescMessage = document.getElementById('<%= lblDeleteDescMessage.ClientID %>');
            if (lblDeleteDescMessage)
                lblDeleteDescMessage.innerText = "Are you sure you want to delete " + Descriptor + " - " + DescValue + " ?";
            return false;
        }

        function setSelectedValue(selectObj, valueToSet) {
            for (var i = 0; i < selectObj.options.length; i++) {
                if (selectObj.options[i].text == valueToSet) {
                    selectObj.options[i].selected = true;
                    return;
                }
            }
        }

    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>

</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="CharHeaderMain" runat="server">
    <div class="mainContent tab-content col-sm-12" style="padding-right: 0px;">
        <div class="row col-sm-12" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Info" CssClass="HeaderFloat" />
        </div>
        <div class="row col-sm-12" style="padding-bottom: 0px; padding-left: 15px; padding-right: 0px;">
            <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">
                <CharSelecter:CSelect ID="oCharSelect" runat="server" />
            </div>
            <div class="col-sm-2 text-right" style="padding-right: 0px;">
                <asp:Button ID="btnSaveTop" runat="server" Text="Save" CssClass="StandardButton" Width="100px" OnClick="btnSave_Click" />
            </div>
        </div>

        <section id="character-info" class="character-info tab-pane active col-sm-12" style="padding-right: 0px;">
            <div class="row col-sm-12" style="padding-left: 0px; padding-right: 0px;">
                <div class="col-sm-12" style="padding-left: 0px; padding-right: 0px;">
                    <asp:Label ID="lblHelp" runat="server" Text="Fill in Information to describe your character. Some items are automatically updated after events." />
                </div>
            </div>

            <div class="row col-sm-12" style="padding-left: 0px; padding-right: 0px;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2><span class="CharInfoHeader">Character Information</span></h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria">
                                <table class="CharInfoTable" border="0">
                                    <tr>
                                        <td class="TableLabel">Character</td>
                                        <td>
                                            <asp:TextBox ID="tbFirstName" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbMiddleName" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbLastName" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td class="TableLabel">Birthplace</td>
                                        <td>
                                            <asp:TextBox ID="tbOrigin" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td class="TableLabel">Status</td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" /><asp:DropDownList ID="ddlStatus" runat="server" Visible="false" />
                                        </td>
                                        <td rowspan="4" style="width: 35px;">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblProfilePictureText" runat="server" Text="To add a profile picture, use the browse button below." />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TableLabel">AKA</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="tbAKA" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td class="TableLabel">Home</td>
                                        <td>
                                            <asp:TextBox ID="tbHome" runat="server" CssClass="TableTextBox" /></td>
                                        <td class="TableLabel">Last Event</td>
                                        <td>
                                            <asp:Label ID="lblDateLastEvent" runat="server" /></td>
                                        <td>
                                            <asp:FileUpload ID="ulFile" runat="server" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TableLabel">Type</td>
                                        <td colspan="1">
                                            <asp:TextBox ID="tbType" runat="server" Enabled="false" />
                                            <asp:DropDownList ID="ddlCharType" runat="server" Visible="false">
                                                <asp:ListItem Text="PC" Value="1" />
                                                <asp:ListItem Text="NPC" Value="2" />
                                            </asp:DropDownList></td>
                                        <td colspan="1" class="TableLabel">
                                            <asp:Label ID="lblVisibleRelationship" runat="server" Text="Visible On Relationships" /></td>
                                        <td colspan="1">
                                            <div style="float: left; width: 50%;">
                                                <asp:DropDownList ID="ddlVisible" runat="server">
                                                    <asp:ListItem Text="Yes" Value="1" />
                                                    <asp:ListItem Text="No" Value="0" />
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: right; width: 50%; text-align: right;">
                                                <asp:Label ID="lblExpiresOn" runat="server" Text="Expires On" />
                                            </div>
                                        </td>
                                        <td class="TableLabel">Primary Team</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTeamList" runat="server" />
                                            <asp:TextBox ID="tbTeam" runat="server" CssClass="col-sm-12" Enabled="false" />
                                        </td>
                                        <td class="TableLabel" style="visibility: hidden;"># of Deaths</td>
                                        <td>
                                            <asp:TextBox ID="tbNumOfDeaths" runat="server" Enabled="false" Visible="false" />
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="StandardButton" OnClick="btnSavePicture_Click" Width="100" /></td>
                                    </tr>

                                    <tr style="vertical-align: top;">
                                        <td class="TableLabel">DOB</td>
                                        <td>
                                            <asp:TextBox ID="tbDOB" runat="server" CssClass="TableTextBox" />
                                        </td>
                                        <td class="TableLabel">
                                            <asp:Label ID="lblAllowSkillRebuild" runat="server" Text="Allow Skill Rebuid" />
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:DropDownList ID="ddlAllowRebuild" runat="server">
                                                    <asp:ListItem Text="No" Value="N" Selected="True" />
                                                    <asp:ListItem Text="Yes" Value="Y" />
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: right;">
                                                <asp:TextBox ID="tbRebuildToDate" runat="server" Columns="10" MaxLength="10" CssClass="TableTextBox" Width="80" Style="margin-left: 10px;" />
                                            </div>
                                        </td>
                                        <td class="TableLabel">Race</td>
                                        <td>
                                            <asp:DropDownList ID="ddlRace" runat="server" /><asp:TextBox ID="tbRace" runat="server" Enabled="false" /></td>
                                        <td class="TableLabel"><%--Date Last Death--%></td>
                                        <td>
                                            <asp:TextBox ID="tbDOD" runat="server" Enabled="false" Visible="false" />
                                            <asp:Label ID="lblDOD" runat="server" Visible="false" />
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

            <div class="row col-sm-12" style="padding-left: 0px; padding-right: 0px; padding-top: 15px;">
                <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;" runat="server" id="divNonCost">
                    <div class="panel" style="padding: 0px;">
                        <div class="panelheader">
                            <h2>Non Cost Character Descriptors</h2>
                            <div class="panel-body">
                                <!-- OnRowCommand="gvDescriptors_RowCommand" -->
                                <div class="panel-container search-criteria">
                                    <div style="margin-bottom: 2px; font-size: larger;">Select criteria that describes your character.</div>
                                    <asp:GridView ID="gvDescriptors" runat="server" AutoGenerateColumns="false" GridLines="None" 
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
                                                    <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                        ImageUrl="~/img/delete.png" Width="16px"
                                                        OnClientClick='<%# string.Format("return openDeleteDesc({0}, \"{1}\", \"{2}\"); return false;",
                                                            Eval("CharacterAttributesBasicID"), Eval("CharacterDescriptor"), Eval("DescriptorValue")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <table class="CharInfoTable" id="divCharDev" runat="server">
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

                <div class="col-sm-6" style="padding-left: 20px; padding-right: 0px;" runat="server" id="divDeaths">
                    <div class="panel" style="padding: 0px;">
                        <div class="panelheader">
                            <h2>Character Deaths</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px; padding-right: 30px; padding-left: 30px;">
                                    <div class="row">
<%--                                        OnRowCommand="gvDeaths_RowCommand" --%>
                                        <asp:GridView runat="server" ID="gvDeaths" AutoGenerateColumns="false" GridLines="None" OnDataBound="gvDeaths_DataBound"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                                            DataKeyNames="CharacterDeathID" CssClass="table table-striped table-hover table-condensed col-sm-12">
                                            <Columns>
                                                <asp:BoundField DataField="DeathDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date of Death" />
                                                <asp:BoundField DataField="Comments" HeaderText="Comments" />
                                                <asp:CheckBoxField DataField="DeathPermanent" HeaderText="Permed" ReadOnly="true" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" />
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/Edit.gif" CommandArgument='<%# Eval("CharacterDeathID") %>' Width="16px" Height="16px"
                                                            OnClientClick='<%# string.Format("return openDeath({0}, \"{1:MM/dd/yyyy}\", \"{2}\", \"{3}\", \"{4}\");", 
                                                                    Eval("CharacterDeathID"), Eval("DeathDate"), Eval("DeathPermanent"), Eval("Comments"), Eval("StaffComments")) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CommandName="DeleteDeathStaff" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/delete.png" CommandArgument='<%# Eval("CharacterDeathID") %>' Width="16px" Height="16px" 
                                                            OnClientClick='<%# string.Format("return openDeleteDeath({0}, \"{1:MM/dd/yyyy}\"); return false;", 
                                                                    Eval("CharacterDeathID"), Eval("DeathDate")) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="col-sm-12 text-right" style="padding-left: 0px; padding-right: 0px;" runat="server" id="divAddDeath">
                                            <asp:Button ID="btnAddNewDeath" runat="server" Text="Add" CssClass="StandardButton" Width="100"
                                                OnClientClick="return openDeath(-1, '', '', '');" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6" style="padding-left: 20px; padding-right: 0px;" runat="server" id="divActors">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Character Actors</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px; padding-right: 30px; padding-left: 30px;">
                                    <div class="row">
<%--                                        OnRowDataBound="gvActors_RowDataBound" OnRowCommand="gvActors_RowCommand"--%>
                                        <asp:GridView runat="server" ID="gvActors" AutoGenerateColumns="false" GridLines="None" OnDataBound="gvActors_DataBound"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" 
                                            DataKeyNames="CharacterActorID" CssClass="table table-striped table-hover table-condensed col-sm-12">
                                            <Columns>
                                                <asp:BoundField DataField="PlayerName" HeaderText="Player Name" HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="StartDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Start Date" />
                                                <asp:BoundField DataField="EndDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="End Date" />

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>' ToolTip='<%# Eval("StaffComments") %>' />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>Comments</HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/Edit.gif" Width="16px"
                                                            OnClientClick='<%# string.Format("return openActor({0}, \"{1}\", \"{2}\", \"{3}\", \"{4:MM/dd/yyyy}\", \"{5:MM/dd/yyyy}\");",
                                                                    Eval("CharacterActorID"), Eval("PlayerName"), Eval("Comments"), Eval("StaffComments"), Eval("StartDate"), Eval("EndDate")) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/delete.png" Width="16px"
                                                            OnClientClick='<%# string.Format("return openDeleteActor({0}, \"{1}\"); return false;",
                                                                    Eval("CharacterActorID"), Eval("loginUserName")) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="col-lg-8 col-xs-12">
                                            <asp:Label ID="lblDateProblem" runat="server" Text="There is a problem with the dates." Font-Bold="true" Font-Italic="true" ForeColor="Red" />
                                        </div>
                                        <div class="col-sm-4 text-right" style="padding-left: 0px; padding-right: 0px;" runat="server" id="divAddActor">
                                            <asp:Button ID="Button1" runat="server" Text="Add" CssClass="StandardButton" Width="100"
                                                OnClientClick="return openActor(-1, '', '', '');" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6" style="padding-left: 20px; padding-right: 0px;" runat="server" id="divPlayer">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Player</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px; padding-right: 30px; padding-left: 30px;">
                                    <div class="row">
                                        <asp:Label ID="lblPlayer" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divStaffComments" class="row col-sm-12" runat="server" style="padding-right: 0px; margin-top: 20px; padding-left: 0px;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px; padding-right: 0px;">
                    <div class="panelheader">
                        <h2>Staff Comments</h2>
                        <div class="panel-body" style="height: 100px;">
                            <div class="panel-container search-criteria">
                                <asp:TextBox ID="tbStaffComments" runat="server" TextMode="MultiLine" Rows="4" CssClass="col-sm-12" Height="75px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row col-sm-12" style="padding-left: 15px; padding-right: 0px; text-align: right; padding-top: 10px;">
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
                    <asp:Button ID="btnCloseError" runat="server" Text="Close" Width="150px" CssClass="StandardButton" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalDeleteDeath" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: red;">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Info - Delete Death
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblDeleteDeathMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <div class="col-xs-6 NoGutters text-left">
                        <asp:Button ID="btnDeleteDeathCancel" runat="server" Text="Cancel" Width="125px" CssClass="CancelButton" />
                    </div>
                    <div class="col-xs-6 NoGutters text-right">
                        <asp:HiddenField ID="hidDeleteDeathID" runat="server" />
                        <asp:Button ID="btnDeleteDeath" runat="server" Text="Delete" Width="125px" CssClass="StandardButton" OnClick="btnDeleteDeath_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalDeleteActor" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: red;">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Info - Delete Actor
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblDeleteActorMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <div class="col-xs-6 NoGutters text-left">
                        <asp:Button ID="btnDeleteActorCancel" runat="server" Text="Cancel" Width="125px" CssClass="CancelButton" />
                    </div>
                    <div class="col-xs-6 NoGutters text-right">
                        <asp:HiddenField ID="hidDeleteActorID" runat="server" />
                        <asp:Button ID="btnDeleteActor" runat="server" Text="Delete" Width="125px" CssClass="StandardButton" OnClick="btnDeleteActor_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalDeleteDesc" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: red;">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Character Info - Delete Descriptor
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblDeleteDescMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <div class="col-xs-6 NoGutters text-left">
                        <asp:Button ID="btnDeleteDescCancel" runat="server" Text="Cancel" Width="125px" CssClass="CancelButton" />
                    </div>
                    <div class="col-xs-6 NoGutters text-right">
                        <asp:HiddenField ID="hidDescID" runat="server" />
                        <asp:Button ID="btnDeleteDesc" runat="server" Text="Delete" Width="125px" CssClass="StandardButton" OnClick="btnDeleteDesc_Click" />
                    </div>
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
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalEditUpdateDeath" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Character Death
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Death Date:</div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbDeathDate" runat="server" CssClass="TableTextBoxWithPadding col-sm-6" />
                            <ajaxToolkit:CalendarExtender ID="ceDeathDate" runat="server" TargetControlID="tbDeathDate" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Perm:</div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:CheckBox ID="cbxDeathPerm" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">
                            Comments:<br />
                            (Visible<br />
                            to player)
                        </div>
                        <div class="col-sm-9" style="padding-left: 0px; margin-left: 0px;">
                            <asp:TextBox ID="tbDeathComments" runat="server" CssClass="TableTextBoxWithPadding col-sm-12" TextMode="MultiLine" MaxLength="500" Rows="5" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">
                            Staff Comments:<br />
                            (Not visible to player)
                        </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbDeathStaffComments" runat="server" CssClass="TableTextBoxWithPadding col-sm-12" TextMode="MultiLine" MaxLength="500" Rows="5" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCancelCharDeath" OnClientClick="closeDeath(); return false;" runat="server" Text="Cancel" Width="100px" CssClass="StandardButton" />
                    </div>
                    <div class="col-sm-6 text-right padding-right-0">
                        <asp:HiddenField ID="hidDeathID" runat="server" />
                        <asp:Button ID="btnSaveCharDeath" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSaveCharDeath_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal" id="modalEditUpdateActor" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Character Actor
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Actor Name:</div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:DropDownList ID="ddlActorName" runat="server" CssClass="SmallText" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Start Date:</div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbActorStartDate" runat="server" CssClass="TableTextBoxWithPadding col-lg-6" />
                            <ajaxToolkit:CalendarExtender ID="ceActorStartDate" runat="server" TargetControlID="tbActorStartDate" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">End Date:</div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbActorEndDate" runat="server" CssClass="TableTextBoxWithPadding col-sm-6" />
                            <ajaxToolkit:CalendarExtender ID="ceActorEndDate" runat="server" TargetControlID="tbActorEndDate" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">
                            Comments:<br />
                            (Visible<br />
                            to player)
                        </div>
                        <div class="col-sm-9" style="padding-left: 0px; margin-left: 0px;">
                            <asp:TextBox ID="tbActorComments" runat="server" CssClass="TableTextBoxWithPadding col-sm-12" TextMode="MultiLine" MaxLength="500" Rows="5" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">
                            Staff Comments:<br />
                            (Not visible to player)
                        </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbActorStaffComments" runat="server" CssClass="TableTextBoxWithPadding col-sm-12" TextMode="MultiLine" MaxLength="500" Rows="5" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-lg-12 text-center">
                        <asp:RequiredFieldValidator ID="rfvActorRequired" runat="server" ControlToValidate="ddlActorName" InitialValue="-1" ErrorMessage="You must choose an actor.<br>"
                            Font-Bold="true" Font-Italic="true" Font-Size="20px" ForeColor="Red" Display="Dynamic" ValidationGroup="ActorEntry" />
                        <asp:RequiredFieldValidator ID="rvActorStartDate" runat="server" ControlToValidate="tbActorStartDate" InitialValue="" ErrorMessage="You must include a start date.<br>"
                            Font-Bold="true" Font-Italic="true" Font-Size="20px" ForeColor="Red" Display="Dynamic" ValidationGroup="ActorEntry" />
                        <asp:CompareValidator ID="cvActorStartEndDate" runat="server" ControlToValidate="tbActorStartDate" ControlToCompare="tbActorEndDate" Type="Date" Operator="LessThanEqual"
                            ErrorMessage="The start date must be less than the end date.<br>" Font-Bold="true" Font-Italic="true" Font-Size="20px" ForeColor="Red" Display="Dynamic" />
                    </div>
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCancelActor" OnClientClick="closeActor(); return false;" runat="server" Text="Cancel" Width="100px" CssClass="StandardButton" CausesValidation="false" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidActorID" runat="server" />
                        <asp:Button ID="btnSaveActor" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSaveActor_Click" ValidationGroup="ActorEntry" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidCharacterID" runat="server" />
    <asp:HiddenField ID="hidActorDateProblems" runat="server" Value="" />

    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(document).ready(function () {
            //$('.HeaderFloat').tooltip({ title: "<h1><strong>HTML</strong> inside <code>the</code> <em>tooltip</em></h1>", html: true, placement: "bottom" });
            $('.CharInfoHeader').tooltip({ title: "Fill in Information to describe your character. Some items are automatically updated after events.", html: true, offset: '0 0' });
        });
    </script>
</asp:Content>
