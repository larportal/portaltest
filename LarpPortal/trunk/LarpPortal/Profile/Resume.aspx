<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Profile.Master" AutoEventWireup="true" CodeBehind="Resume.aspx.cs" Inherits="LarpPortal.Profile.Resume" %>

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
    </style>
</asp:Content>

<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="ProfileHeaderScripts">
    <script type="text/javascript">
        function openModalMessage() {
            $('#ModalMessage').modal('show');
        }

        function openPlayerSkill(PlayerResumeID, SkillName, SkillLevel, PlayerComments) {
            $('#modalPlayerSkill').modal('show');

            var tbSkillName = document.getElementById('<%= tbSkillName.ClientID %>');
            if (tbSkillName) {
                tbSkillName.value = SkillName;
                tbSkillName.focus();
            }

            var hidPlayerResumeID = document.getElementById('<%= hidPlayerResumeID.ClientID %>');
            if (hidPlayerResumeID)
                hidPlayerResumeID.value = PlayerResumeID;

            var ddlSkillLevel = document.getElementById('<%= ddlSkillLevel.ClientID %>');
            if (ddlSkillLevel)
                setSelectedValue(ddlSkillLevel, SkillLevel);

            var tbSkillComments = document.getElementById('<%= tbSkillComments.ClientID %>');
            if (tbSkillComments)
                tbSkillComments.value = PlayerComments;

            return false;
        }

        function openDeletePlayerSkill(PlayerResumeID, SkillName, SkillLevel, PlayerComments) {
            $('#modalDeletePlayerSkill').modal('show');

            var lblDeletePlayerSkillName = document.getElementById('lblDeletePlayerSkillName');
            if (lblDeletePlayerSkillName)
                lblDeletePlayerSkillName.innerText = SkillName;

            var lblDeletePlayerSkillLevel = document.getElementById('lblDeletePlayerSkillLevel');
            if (lblDeletePlayerSkillLevel)
                lblDeletePlayerSkillLevel.innerText = SkillLevel;

            var lblDeletePlayerSkillComments = document.getElementById('lblDeletePlayerSkillComments');
            if (lblDeletePlayerSkillComments)
                lblDeletePlayerSkillComments.innerText = PlayerComments;

            var hidDeleteSkillID = document.getElementById('<%= hidDeleteSkillID.ClientID %>');
            if (hidDeleteSkillID)
                hidDeleteSkillID.value = PlayerResumeID;

            return false;
        }

        function openAffiliation(PlayerAffilID, AffilName, AffilRole, PlayerComments) {
            $('#modalPlayerAffil').modal('show');

            var tbAffiliationName = document.getElementById('<%= tbAffiliationName.ClientID %>');
            if (tbAffiliationName) {
                tbAffiliationName.value = AffilName;
                tbAffiliationName.focus();
            }

            var hidPlayerAffilID = document.getElementById('<%= hidPlayerAffilID.ClientID %>');
            if (hidPlayerAffilID)
                hidPlayerAffilID.value = PlayerAffilID;

            var tbAffiliationRole = document.getElementById('<%= tbAffiliationRole.ClientID %>');
            if (tbAffiliationRole)
                tbAffiliationRole.value = AffilRole;

            var tbAffiliationComments = document.getElementById('<%= tbAffiliationComments.ClientID %>');
            if (tbAffiliationComments)
                tbAffiliationComments.value = PlayerComments;

            return false;
        }

        function openDeleteAffiliation(PlayerAffilID, AffilName, AffilRole, PlayerComments) {
            $('#modalDeletePlayerAffil').modal('show');

            var lblDeletePlayerAffilName = document.getElementById('lblDeletePlayerAffilName');
            if (lblDeletePlayerAffilName)
                lblDeletePlayerAffilName.innerText = AffilName;

            var lblDeletePlayerAffilRole = document.getElementById('lblDeletePlayerAffilRole');
            if (lblDeletePlayerAffilRole)
                lblDeletePlayerAffilRole.innerText = AffilRole;

            var lblDeletePlayerAffilComments = document.getElementById('lblDeletePlayerAffilComments');
            if (lblDeletePlayerAffilComments)
                lblDeletePlayerAffilComments.innerText = PlayerComments;

            var hidDeleteAffilID = document.getElementById('<%= hidDeleteAffilID.ClientID %>');
            if (hidDeleteAffilID)
                hidDeleteAffilID.value = PlayerAffilID;

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
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Work Resume" />
            </div>
        </div>

        <section id="character-info" class="character-info tab-pane active">

            <div class="row" style="padding-bottom: 20px;">
                <div class="col-xs-12">
                    <table class="col-xs-12">
                        <tr>
                            <td style="white-space: nowrap;">
                                <b>Linked In URL:&nbsp;&nbsp;</b>
                            </td>
                            <td style="width: 100%;">
                                <asp:TextBox ID="tbLinkedInURL" runat="server" CssClass="TableTextBoxWithPadding col-xs-5" BackColor="White" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 col-lg-6">
                    <div class="panel NoGutters" style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 15px;">
                        <div class="panelheader NoGutters">
                            <h2>Profession Skills</h2>
                            <div class="panel-body NoGutters">
                                <div class="panel-container" style="padding-bottom: 15px; padding-left: 15px;">
                                    <div class="row" style="padding-left: 15px; padding-right: 15px;">
                                        <asp:GridView runat="server" ID="gvSkills" AutoGenerateColumns="false" GridLines="None"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                                            CssClass="table table-striped table-hover table-condensed col-sm-12">
                                            <Columns>
                                                <asp:BoundField DataField="SkillName" HeaderText="Professional Skill" />
                                                <asp:BoundField DataField="SkillLevel" HeaderText="Skill Level" />
                                                <asp:BoundField DataField="PlayerComments" HeaderText="Comments" />
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/Edit.gif" Width="16px"
                                                            OnClientClick='<%# string.Format("return openPlayerSkill({0}, \"{1}\", \"{2}\", \"{3}\"); return false;",
                                                            Eval("PlayerSkillID"), Eval("SkillName"), Eval("SkillLevel"), Eval("PlayerComments")) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/delete.png" Width="16px"
                                                            OnClientClick='<%# string.Format("return openDeletePlayerSkill({0}, \"{1}\", \"{2}\", \"{3}\"); return false;",
                                                            Eval("PlayerSkillID"), Eval("SkillName"), Eval("SkillLevel"), Eval("PlayerComments")) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                            <asp:Button ID="btnAddResumeItem" runat="server" Text="Add" CssClass="StandardButton" Width="100"
                                                OnClientClick="return openPlayerSkill(-1, '', '', ''); return false;" />
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
                            <h2>Professional Affiliations</h2>
                            <div class="panel-body">
                                <div class="panel-container" style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px;">
                                    <div class="row" style="padding-left: 15px; padding-right: 15px;">
                                        <asp:GridView runat="server" ID="gvAffiliations" AutoGenerateColumns="false" GridLines="None"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                                            CssClass="table table-striped table-hover table-condensed col-sm-12">
                                            <Columns>
                                                <asp:BoundField DataField="AffiliationName" HeaderText="Organization Name & Affiliation" />
                                                <asp:BoundField DataField="AffiliationRole" HeaderText="Role" />
                                                <asp:BoundField DataField="PlayerComments" HeaderText="Comments" />
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/Edit.gif" Width="16px"
                                                            OnClientClick='<%# string.Format("return openAffiliation({0}, \"{1}\", \"{2}\", \"{3}\");",
                                                            Eval("PlayerAffiliationID"), Eval("AffiliationName"), Eval("AffiliationRole"), Eval("PlayerComments")) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                            ImageUrl="~/img/delete.png" Width="16px"
                                                            OnClientClick='<%# string.Format("return openDeleteAffiliation({0}, \"{1}\", \"{2}\", \"{3}\");",
                                                            Eval("PlayerAffiliationID"), Eval("AffiliationName"), Eval("AffiliationRole"), Eval("PlayerComments")) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                            <asp:Button ID="btnAddAffiliation" runat="server" Text="Add" CssClass="StandardButton" Width="100"
                                                OnClientClick="return openAffiliation(-1, '', '', '');" />
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
                                            <asp:TextBox ID="tbResumeComments" runat="server" TextMode="MultiLine" CssClass="TableTextBoxWithPadding col-xs-12" Rows="4" />
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
            </div>

        </section>
    </div>

    <div class="modal" id="ModalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Resume
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

    <div class="modal" id="modalPlayerSkill" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Professional Skill
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="form-group">
                        <label for="tbSkillName">Skill Name:</label>
                        <asp:TextBox ID="tbSkillName" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="ddlSkillLevel">Skill Level:</label>
                        <asp:DropDownList ID="ddlSkillLevel" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Beginner" Text="Beginner" />
                            <asp:ListItem Value="Intermediate" Text="Intermediate" />
                            <asp:ListItem Value="Expert" Text="Expert" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="tbSkillComments">Comments:</label>
                        <asp:TextBox ID="tbSkillComments" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnClosePlayerSkill" runat="server" CssClass="StandardButton" Width="100" Text="Close" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidPlayerResumeID" runat="server" />
                        <asp:Button ID="btnSaveSkill" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSaveSkill_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalDeletePlayerSkill" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Professional Skill - Delete
                </div>

                <div class="modal-body" style="background-color: white;">
                    <div class="container-fluid" style="margin-left: -30px; margin-right: -30px">
                        <label class="col-xs-12">Skill Name:</label>
                        <span class="col-xs-12" id="lblDeletePlayerSkillName"></span>
                        <label class="col-xs-12" style="padding-top: 20px;">Skill Level:</label>
                        <span class="col-xs-12" id="lblDeletePlayerSkillLevel"></span>
                        <label class="col-xs-12" style="padding-top: 20px;">Comments:</label>
                        <span class="col-xs-12" id="lblDeletePlayerSkillComments"></span>
                        <span class="col-xs-12" style="padding-top: 40px">Are you sure you want to delete this record ?</span>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCancelDeleteSkill" runat="server" CssClass="StandardButton" Width="100" Text="Cancel" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidDeleteSkillID" runat="server" />
                        <asp:Button ID="btnDeleteSkill" runat="server" Text="Delete" Width="100px" CssClass="StandardButton" OnClick="btnDeleteSkill_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalPlayerAffil" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Professional Affiliation
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="form-group">
                        <label for="tbAffiliation">Affiliation:</label>
                        <asp:TextBox ID="tbAffiliationName" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="tbAffiliationRole">Role:</label>
                        <asp:TextBox ID="tbAffiliationRole" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="tbAffilComments">Comments:</label>
                        <asp:TextBox ID="tbAffiliationComments" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCloseAffil" runat="server" CssClass="StandardButton" Width="100" Text="Close" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidPlayerAffilID" runat="server" />
                        <asp:Button ID="btnSaveAffiliation" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSaveAffiliation_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalDeletePlayerAffil" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Professional Affiliation - Delete
                </div>

                <div class="modal-body" style="background-color: white;">
                    <div class="container-fluid" style="margin-left: -30px; margin-right: -30px">
                        <label class="col-xs-12">Affiliation:</label>
                        <span class="col-xs-12" id="lblDeletePlayerAffilName"></span>
                        <label class="col-xs-12" style="padding-top: 20px;">Role:</label>
                        <span class="col-xs-12" id="lblDeletePlayerAffilRole"></span>
                        <label class="col-xs-12" style="padding-top: 20px;">Comments:</label>
                        <span class="col-xs-12" id="lblDeletePlayerAffilComments"></span>
                        <span class="col-xs-12" style="padding-top: 40px">Are you sure you want to delete this record ?</span>
                    </div>
                </div>

                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <asp:Button ID="btnCancelDeleteAffil" runat="server" CssClass="StandardButton" Width="100" Text="Cancel" />
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidDeleteAffilID" runat="server" />
                        <asp:Button ID="btnDeleteAffil" runat="server" Text="Delete" Width="100px" CssClass="StandardButton" OnClick="btnDeleteAffil_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidPlayerProfileID" runat="server" />
</asp:Content>
