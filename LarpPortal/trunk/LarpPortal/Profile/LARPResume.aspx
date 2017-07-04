<%@ Page Title="" Language="C#" MasterPageFile="~/Profile/Profile.Master" AutoEventWireup="true" CodeBehind="LARPResume.aspx.cs" Inherits="LarpPortal.Profile.LARPResume" %>

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

        function openResumeItem(PlayerLARPResumeID, PlayerProfileID, GameSystem, Campaign, AuthorGM, StyleID, GenreID, RoleID, StartDate, EndDate, PlayerComments) {
            $('#modalResumeItem').modal('show');

            var tbStartDate = document.getElementById('<%= tbStartDate.ClientID %>');
            if (tbStartDate)
                tbStartDate.value = StartDate;

            var tbEndDate = document.getElementById('<%= tbEndDate.ClientID %>');
            if (tbEndDate)
                tbEndDate.value = EndDate;

            var tbGameSystem = document.getElementById('<%= tbGameSystem.ClientID %>');
            if (tbGameSystem)
                tbGameSystem.value = GameSystem;

            var tbCampaign = document.getElementById('<%= tbCampaign.ClientID %>');
            if (tbCampaign)
                tbCampaign.value = Campaign;

            var tbAuthorGM = document.getElementById('<%= tbAuthorGM.ClientID %>');
            if (tbAuthorGM)
                tbAuthorGM.value = AuthorGM;

            var hidPlayerResumeID = document.getElementById('<%= hidPlayerLARPResumeID.ClientID %>');
            if (hidPlayerResumeID)
                hidPlayerResumeID.value = PlayerLARPResumeID;

            var ddlStyle = document.getElementById('<%= ddlStyle.ClientID %>');
            setSelectedValue(ddlStyle, StyleID);

            var ddlGenre = document.getElementById('<%= ddlGenre.ClientID %>');
            setSelectedValue(ddlGenre, GenreID);

            var ddlRole = document.getElementById('<%= ddlRole.ClientID %>');
            setSelectedValue(ddlRole, RoleID);

            var tbComments = document.getElementById('<%= tbComments.ClientID %>');
            if (tbComments)
                tbComments.value = PlayerComments;

            return false;
        }

        function openDeleteResumeItem(PlayerLARPResumeID, GameSystem, Campaign, AuthorGM, StartDate, EndDate) {
            $('#modalDeleteResumeItem').modal('show');

            var hidDeleteResumeItemID = document.getElementById('<%= hidDeleteResumeItemID.ClientID %>');
            if (hidDeleteResumeItemID)
                hidDeleteResumeItemID.value = PlayerLARPResumeID;

            var lblDeleteMessage = document.getElementById('<%= lblDeleteMessage.ClientID %>');
            if (lblDeleteMessage) {
                lblDeleteMessage.innerHTML = "";
                if (GameSystem.length > 0)
                    lblDeleteMessage.innerHTML += "Game System: " + GameSystem + "<br>";
                if (Campaign.length > 0)
                    lblDeleteMessage.innerHTML += "Campaign: " + Campaign + "<br>";
                if (AuthorGM.length > 0)
                    lblDeleteMessage.innerHTML += "Author/GM: " + AuthorGM + "<br>";
                if (StartDate.length > 0)
                    lblDeleteMessage.innerHTML += "Start Date: " + StartDate + "<br>";
                if (EndDate.length > 0)
                    lblDeleteMessage.innerHTML += "End Date: " + EndDate + "<br>";

                lblDeleteMessage.innerHTML += "<br>Are you sure you want to delete this record ?";
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

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="MainPage" ContentPlaceHolderID="ProfileMain" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-top: 10px; padding-bottom: 0px;">
            <div class="col-sm-4">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="LARP Resume" />
            </div>
        </div>

        <section id="character-info" class="character-info tab-pane active">
            <div class="row" style="padding-left: 15px; padding-bottom: 15px;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2>Player LARP Resume</h2>
                        <div class="panel-body">
                            <div class="panel-container" style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px;">
                                <div class="row" style="padding-left: 15px; padding-right: 15px;">
                                    <asp:GridView runat="server" ID="gvResumeItems" AutoGenerateColumns="false" GridLines="None"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                                        CssClass="table table-striped table-hover table-condensed col-sm-12">
                                        <Columns>
                                            <asp:BoundField DataField="StartDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Start Date" />
                                            <asp:BoundField DataField="EndDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="End Date" />
                                            <asp:BoundField DataField="GameSystem" HeaderText="Game System" />
                                            <asp:BoundField DataField="Campaign" HeaderText="Campaign" />
                                            <asp:BoundField DataField="AuthorGM" HeaderText="Author GM" />
                                            <asp:BoundField DataField="Style" HeaderText="Style" />
                                            <asp:BoundField DataField="Genre" HeaderText="Genre" />
                                            <asp:BoundField DataField="Role" HeaderText="Role" />
                                            <asp:BoundField DataField="PlayerComments" HeaderText="Comments" />
                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                        ImageUrl="~/img/Edit.gif" Width="16px"
                                                        OnClientClick='<%# string.Format("return openResumeItem({0}, {1}, \"{2}\", \"{3}\", \"{4}\", {5}, {6}, {7}, \"{8:MM/dd/yyyy}\", \"{9:MM/dd/yyyy}\", \"{10}\");",
                                                            Eval("PlayerLARPResumeID"), Eval("PlayerProfileID"), Eval("GameSystem"), Eval("Campaign"), Eval("AuthorGM"), Eval("StyleID"),
                                                            Eval("GenreID"), Eval("RoleID"), Eval("StartDate"), Eval("EndDate"), Eval("PlayerComments")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="20" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnDelete" runat="server" CausesValidation="false" CssClass="NoRightPadding"
                                                        ImageUrl="~/img/delete.png" Width="16px"
                                                        OnClientClick='<%# string.Format("return openDeleteResumeItem({0}, \"{1}\", \"{2}\", \"{3}\", \"{4:d}\", \"{5:d}\");",
                                                            Eval("PlayerLARPResumeID"), Eval("GameSystem"), Eval("Campaign"), Eval("AuthorGM"), Eval("StartDate"), Eval("EndDate")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                        <asp:Button ID="btnAddResumeItem" runat="server" Text="Add" CssClass="StandardButton" Width="100"
                                            OnClientClick="return openResumeItem(-1, '', '', '', '', '', '', '', '', '');" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row" style="padding-left: 15px;">
                <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                    <div class="panelheader">
                        <h2>Comments</h2>
                        <div class="panel-body">
                            <div class="panel-container" style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px;">
                                <div class="row" style="padding-right: 15px;">
                                    <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                        <asp:TextBox ID="tbLARPResumeComments" runat="server" TextMode="MultiLine" CssClass="TableTextBoxWithPadding col-xs-12" Rows="4" />
                                    </div>
                                </div>
                                <div class="row" style="padding-left: 15px; padding-right: 15px; padding-top: 15px;">
                                    <div class="col-xs-12 text-right" style="padding-left: 15px; padding-right: 0px;">
                                        <asp:Button ID="btnSaveComments" runat="server" Text="Save" CssClass="StandardButton" Width="100" OnClick="btnSaveComments_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
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
                    Player Resume Item
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

    <div class="modal" id="modalResumeItem" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Resume Item
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Start Date: </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbStartDate" runat="server" CssClass="TableTextBoxWithPadding col-sm-6" />
                            <ajaxToolkit:CalendarExtender ID="ceStartMonthYear" runat="server" TargetControlID="tbStartDate" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">End Date: </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbEndDate" runat="server" CssClass="TableTextBoxWithPadding col-lg-6" />
                            <ajaxToolkit:CalendarExtender ID="ceEndMonthYear" runat="server" TargetControlID="tbEndDate" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Game System: </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbGameSystem" runat="server" CssClass="TableTextBoxWithPadding col-sm-6" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Campaign: </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbCampaign" runat="server" CssClass="TableTextBoxWithPadding col-sm-12" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Author GM: </div>
                        <div class="col-sm-9" style="padding-left: 0px; margin-left: 0px;">
                            <asp:TextBox ID="tbAuthorGM" runat="server" CssClass="TableTextBoxWithPadding col-sm-12" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Style: </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:DropDownList ID="ddlStyle" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Genre: </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:DropDownList ID="ddlGenre" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Role: </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:DropDownList ID="ddlRole" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 5px;">
                        <div class="col-sm-3 TableLabel">Comments: </div>
                        <div class="col-sm-9" style="padding-left: 0px;">
                            <asp:TextBox ID="tbComments" runat="server" CssClass="TableTextBoxWithPadding col-sm-12" TextMode="MultiLine" Rows="4" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-sm-6 text-left" style="padding-left: 0px;">
                        <button type="button" data-dismiss="modal" class="StandardButton" style="width: 125px;">Close</button>
                    </div>
                    <div class="col-sm-6 text-right  padding-right-0">
                        <asp:HiddenField ID="hidPlayerLARPResumeID" runat="server" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" CssClass="StandardButton" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalDeleteResumeItem" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: red;">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Player Resume - Delete
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblDeleteMessage" runat="server" Text="" />
                    </p>
                </div>
                <div class="modal-footer">
                    <div class="col-xs-6 NoGutters text-left">
                        <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" Width="125px" CssClass="CancelButton" />
                    </div>
                    <div class="col-xs-6 NoGutters text-right">
                        <asp:HiddenField ID="hidDeleteResumeItemID" runat="server" />
                        <asp:Button ID="btnDeleteResumeItem" runat="server" Text="Delete" Width="125px" CssClass="StandardButton" OnClick="btnDeleteResumeItem_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidPlayerProfileID" runat="server" />
</asp:Content>
