<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="EventEdit.aspx.cs" Inherits="LarpPortal.Events.EventEdit" %>

<asp:Content ID="EventStyles" runat="server" ContentPlaceHolderID="MemberStyles">
    <style type="text/css">
        th, tr:nth-child(even) > td
        {
            background-color: transparent;
        }

        .CharInfoTable
        {
            border-collapse: collapse;
        }

            .CharInfoTable td
            {
                padding: 4px;
            }

        .NoPadding
        {
            padding-left: 0px;
            padding-right: 0px;
        }

        .TextEntry
        {
            border: 1px solid black;
            padding: 0px;
        }

        .PnlDesign
        {
            border: solid 1px #000000;
            height: 150px;
            width: 330px;
            overflow-y: scroll;
            background-color: white;
            font-size: 15px;
            font-family: Arial;
        }

        .txtbox
        {
            background-image: url(../img/download.png);
            background-position: right top;
            background-repeat: no-repeat;
            cursor: pointer;
            border: 1px solid #A9A9A9;
        }

        .container
        {
            display: table;
            vertical-align: middle;
        }

        /*.vertical-center-row
        {
            display: table-cell;
            vertical-align: middle;
        }*/

        div
        {
            border: solid 0px black;
        }

        .vertical-center-row
        {
            display: inline-block;
            vertical-align: middle;
            float: none;
        }

        label
        {
            margin: 0px 0px 0px 0px;
        }

        .ErrorDisplay
        {
            font-weight: bold;
            font-style: italic;
            color: red;
            margin-left: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="EventScripts" ContentPlaceHolderID="MemberScripts" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function closeModal() {
            $('#myModal').hide();
        }

        function CalcDates() {
            var tbStartDate = document.getElementById("<%= tbStartDate.ClientID %>");
            var StartDate = new Date(tbStartDate.value);
            tbStartDate.value = (StartDate.getMonth() + 1) + '/' + StartDate.getDate() + '/' + StartDate.getFullYear();

            var tbCloseDate = document.getElementById("<%= tbCloseRegDate.ClientID %>");
            tbCloseDate.value = (StartDate.getMonth() + 1) + '/' + StartDate.getDate() + '/' + StartDate.getFullYear();

            var InfoDueDate = parseInt(document.getElementById("<%= hidDaysToInfoSkillDeadlineDate.ClientID %>").value);
            if (!isNaN(InfoDueDate)) {
                var InfoSkillDate = new Date(StartDate);
                InfoSkillDate.setDate(InfoSkillDate.getDate() - InfoDueDate);
                var tbInfoDue = document.getElementById("<%= tbInfoSkillDue.ClientID %>");
                tbInfoDue.value = (InfoSkillDate.getMonth() + 1) + '/' + InfoSkillDate.getDate() + '/' + InfoSkillDate.getFullYear();
            }

            var PreregOpenDays = parseInt(document.getElementById("<%= hidDaysToRegistrationOpenDate.ClientID %>").value);
            if (!isNaN(PreregOpenDays)) {
                var PreregOpenDate = new Date(StartDate);
                PreregOpenDate.setDate(PreregOpenDate.getDate() - PreregOpenDays);
                var tbOpenRegDate = document.getElementById("<%= tbOpenRegDate.ClientID %>");
                tbOpenRegDate.value = (PreregOpenDate.getMonth() + 1) + '/' + PreregOpenDate.getDate() + '/' + PreregOpenDate.getFullYear();
            }

            var DaysToPELDeadlineDate = parseInt(document.getElementById("<%= hidDaysToPELDeadlineDate.ClientID %>").value);
            if (!isNaN(DaysToPELDeadlineDate)) {
                var PELDueDate = new Date(StartDate);
                PELDueDate.setDate(PELDueDate.getDate() + DaysToPELDeadlineDate);
                var tbPELDue = document.getElementById("<%= tbPELDue.ClientID %>");
                tbPELDue.value = (PELDueDate.getMonth() + 1) + '/' + PELDueDate.getDate() + '/' + PELDueDate.getFullYear();
            }

            var DaysToPreregistrationDeadline = parseInt(document.getElementById("<%= hidDaysToPreregistrationDeadline.ClientID %>").value);
            if (!isNaN(DaysToPreregistrationDeadline)) {
                var PreRegDate = new Date(StartDate);
                PreRegDate.setDate(PreRegDate.getDate() - DaysToPreregistrationDeadline);
                var tbPreReg = document.getElementById("<%= tbPreRegDeadline.ClientID %>");
                tbPreReg.value = (PreRegDate.getMonth() + 1) + '/' + PreRegDate.getDate() + '/' + PreRegDate.getFullYear();
            }

            var DaysToPaymentDue = parseInt(document.getElementById("<%= hidDaysToPaymentDue.ClientID %>").value);
            if (!isNaN(DaysToPaymentDue)) {
                var PaymentDueDate = new Date(StartDate);
                PaymentDueDate.setDate(PaymentDueDate.getDate() - DaysToPaymentDue);
                var tbPaymentDue = document.getElementById("<%= tbPaymentDate.ClientID %>");
                tbPaymentDue.value = (PaymentDueDate.getMonth() + 1) + '/' + PaymentDueDate.getDate() + '/' + PaymentDueDate.getFullYear();
            }

            Page_ClientValidate();
        }

    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="CampaignInfoSetup" ContentPlaceHolderID="MemberCampaignsContent" runat="server">

    <style>
        select
        {
            font-size: 12px;
        }

        .NoMargins
        {
            margin: 0px 0px 0px 0px;
        }

        .row
        {
            vertical-align: middle;
            padding-bottom: 2px;
        }
    </style>
    <div class="mainContent tab-content col-lg-12 input-group">
        <section id="character-info" class="character-info tab-pane active">
            <div role="form" class="form-horizontal panel">
                <div class="col-lg-12 NoPadding">
                    <h1 class="col-lg-12">Schedule An Event</h1>
                </div>
                <div class="col-lg-12 NoPadding row">
                    <div class="col-lg-8 NoPadding">
                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">Start Date/Time</div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbStartDate" runat="server" CssClass="col-sm-6 NoPadding" />
                                <asp:TextBox ID="tbStartTime" runat="server" CssClass="col-sm-5 NoPadding" Style="margin-left: 10px;" TextMode="Time" />
                                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="tbStartDate"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                                <asp:CompareValidator ID="cvStartDate" runat="server" ControlToValidate="tbStartDate" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Enter A Valid Date" Type="Date" Operator="DataTypeCheck" />
                            </div>
                            <div class="TableLabel col-lg-3">End Date/Time</div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbEndDate" runat="server" CssClass="col-sm-6 NoPadding" />
                                <asp:TextBox ID="tbEndTime" runat="server" CssClass="col-sm-5 NoPadding" Style="margin-left: 10px;" TextMode="Time" />
                                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="tbEndDate"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                                <asp:CompareValidator ID="cvEndDate" runat="server" ControlToValidate="tbEndDate" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Enter A Valid Date" Type="Date" Operator="DataTypeCheck" />
                            </div>
                        </div>
                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">Site</div>
                            <div class="col-lg-9 NoPadding">
                                <asp:DropDownList ID="ddlSiteList" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvSiteList" runat="server" ControlToValidate="ddlSiteList" InitialValue=""
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">Event Name</div>
                            <div class="col-lg-9 NoPadding">
                                <asp:TextBox ID="tbEventName" runat="server" CssClass="col-sm-10" />
                                <asp:RequiredFieldValidator ID="rfvEventName" runat="server" ControlToValidate="tbEventName"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">Event Description</div>
                            <div class="col-lg-9 NoPadding">
                                <asp:TextBox ID="tbEventDescription" runat="server" CssClass="col-sm-10" />
                                <asp:RequiredFieldValidator ID="rfvEventDescription" runat="server" ControlToValidate="tbEventDescription"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">In Game Location</div>
                            <div class="col-lg-9 NoPadding">
                                <asp:TextBox ID="tbGameLocation" runat="server" CssClass="col-lg-10" />
                                <asp:RequiredFieldValidator ID="rfvGameLocation" runat="server" ControlToValidate="tbGameLocation"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Maximum PC Count
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbMaxPCCount" runat="server" Columns="4" MaxLength="4" />
                                <asp:CompareValidator ID="cvMaxPCCount" runat="server" ControlToValidate="tbMaxPCCount" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Numbers Only" Type="Integer" Operator="DataTypeCheck" />
                                <asp:RequiredFieldValidator ID="rfvMaxPCCount" runat="server" ControlToValidate="tbMaxPCCount"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                BaseNPC Count
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbBaseNPCCount" runat="server" Columns="4" MaxLength="4" />
                                <asp:CompareValidator ID="cvBaseNPCCount" runat="server" ControlToValidate="tbBaseNPCCount" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Numbers Only" Type="Integer" Operator="DataTypeCheck" />
                                <asp:RequiredFieldValidator ID="rfvBaseNPCCount" runat="server" ControlToValidate="tbBaseNPCCount"
                                    CssClass="ErrorDisplay" Text="* Enter Date" Display="Dynamic" />
                            </div>
                            <div class="col-lg-3 NoPadding">
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Default Reg Status
                            </div>

                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlDefaultRegStatus" runat="server" />
                                <asp:RequiredFieldValidator ID="rvDefaultRegStatus" runat="server" ControlToValidate="ddlDefaultRegStatus" InitialValue="" CssClass="ErrorDisplay"
                                    Text="* Choose Def Reg Status" Display="Dynamic" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                NPC Override Ratio
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbOverrideRatio" runat="server" Columns="4" MaxLength="4" />
                                <asp:CompareValidator ID="cvOverrideRatio" runat="server" ControlToValidate="tbOverrideRatio" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Numbers Only" Type="Integer" Operator="DataTypeCheck" />
                                <asp:RequiredFieldValidator ID="rfvOverrideRation" runat="server" ControlToValidate="tbOverrideRatio"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                            <div class="col-lg-3 NoPadding">
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Open Reg Date
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbOpenRegDate" runat="server" CssClass="col-sm-6 NoPadding" />
                                <asp:TextBox ID="tbOpenRegTime" runat="server" CssClass="col-sm-5 NoPadding" Style="margin-left: 10px;" TextMode="Time" />
                                <asp:CompareValidator ID="cvOpenRegDate" runat="server" ControlToValidate="tbOpenRegDate" Operator="DataTypeCheck"
                                    CssClass="ErrorDisplay" Text="* Enter A Valid Date" Type="Date" Display="Dynamic" />
                                <asp:RequiredFieldValidator ID="rvOpenRegDate" runat="server" ControlToValidate="tbOpenRegDate"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                Cap New Notification
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlCapNearNotification" runat="server" Style="vertical-align: middle;">
                                    <asp:ListItem Text="Choose Value" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="true" />
                                    <asp:ListItem Text="No" Value="false" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCapNearNotification" runat="server" ControlToValidate="ddlCapNearNotification" InitialValue=""
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                            <div class="col-lg-3 NoPadding">
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Close Reg Date
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbCloseRegDate" runat="server" CssClass="col-sm-6 NoPadding" />
                                <asp:TextBox ID="tbCloseRegTime" runat="server" CssClass="col-sm-5 NoPadding" Style="margin-left: 10px;" TextMode="Time" />
                                <asp:CompareValidator ID="cvCloseRegDate" runat="server" ControlToValidate="tbCloseRegDate" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Enter A Valid Date" Type="Date" Operator="DataTypeCheck" />
                                <asp:RequiredFieldValidator ID="rfvCloseRegDate" runat="server" ControlToValidate="tbCloseRegDate"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                Cap Near Notification
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbCapThresholdNotification" runat="server" Columns="4" MaxLength="4" />
                                <asp:CompareValidator ID="cvCapThresholdNotification" runat="server" ControlToValidate="tbCapThresholdNotification" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Numbers Only" Type="Integer" Operator="DataTypeCheck" />
                                <asp:RequiredFieldValidator ID="rfvCapThresholdNotification" runat="server" ControlToValidate="tbCapThresholdNotification"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                            <div class="col-lg-3 NoPadding">
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Pre Reg Deadline
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbPreRegDeadline" runat="server" Columns="16" MaxLength="16" />
                                <asp:CompareValidator ID="cvPreRegDeadline" runat="server" ControlToValidate="tbPreRegDeadline" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Enter A Valid Date" Type="Date" Operator="DataTypeCheck" />
                                <asp:RequiredFieldValidator ID="rfvPreRegDeadline" runat="server" ControlToValidate="tbPreRegDeadline"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                Auto Approve Waitlist
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlAutoApproveWaitlist" runat="server" Style="vertical-align: middle;">
                                    <asp:ListItem Text="Choose Value" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="true" />
                                    <asp:ListItem Text="No" Value="false" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAutoApproveWaitlist" runat="server" ControlToValidate="ddlAutoApproveWaitlist" InitialValue=""
                                    CssClass="ErrorDisplay" Text="* Enter Date" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Payment Date
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbPaymentDate" runat="server" Columns="16" MaxLength="16" />
                                <asp:RequiredFieldValidator ID="rfvPaymentDate" runat="server" ControlToValidate="tbPaymentDate"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                                <asp:CompareValidator ID="cvPaymentDate" runat="server" ControlToValidate="tbPaymentDate" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Enter A Valid Date" Type="Date" Operator="DataTypeCheck" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                PC Food Service
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlPCFoodService" runat="server">
                                    <asp:ListItem Text="Choose Value" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="true" />
                                    <asp:ListItem Text="No" Value="false" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPCFoodService" runat="server" ControlToValidate="ddlPCFoodService" InitialValue=""
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Pre Registration Price
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbPreRegistrationPrice" runat="server" Columns="8" MaxLength="8" />
                                <asp:RequiredFieldValidator ID="rfvPreRegistrationPrice" runat="server" ControlToValidate="tbPreRegistrationPrice"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                                <asp:CompareValidator ID="cvPreRegistrationPrice" runat="server" ControlToValidate="tbPreRegistrationPrice" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Numbers Only" Type="Double" Operator="DataTypeCheck" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                NPC Food Service
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlNPCFoodService" runat="server">
                                    <asp:ListItem Text="Choose Value" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="true" />
                                    <asp:ListItem Text="No" Value="false" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvNPCFoodService" runat="server" ControlToValidate="ddlNPCFoodService" InitialValue=""
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Reg Price
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbRegPrice" runat="server" Columns="8" MaxLength="8" />
                                <asp:RequiredFieldValidator ID="rfvRegPrice" runat="server" ControlToValidate="tbRegPrice"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                At Door Price
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbAtDoorPrice" runat="server" Columns="8" MaxLength="8" />
                                <asp:RequiredFieldValidator ID="rfvAtDoorPrice" runat="server" ControlToValidate="tbAtDoorPrice"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                                <asp:CompareValidator ID="cvAtDoorPrice" runat="server" ControlToValidate="tbAtDoorPrice" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Numbers Only" Type="Double" Operator="DataTypeCheck" />
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Payment Instructions
                            </div>
                            <div class="col-lg-9 NoPadding">
                                <asp:TextBox ID="tbPaymentInstructions" runat="server" TextMode="MultiLine" Rows="3" CssClass="col-sm-11 NoLeftPadding" />
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Auto Cancel Reg
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlAutoCancelReg" runat="server" Style="vertical-align: middle;">
                                    <asp:ListItem Text="Choose Value" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="true" />
                                    <asp:ListItem Text="No" Value="false" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAutoCancelReg" runat="server" ControlToValidate="ddlAutoCancelReg" InitialValue=""
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Info Skill Due
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbInfoSkillDue" runat="server" Columns="16" MaxLength="16" />
                                <asp:RequiredFieldValidator ID="rfvInfoSkillDue" runat="server" ControlToValidate="tbInfoSkillDue"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                                <asp:CompareValidator ID="cvInfoSkillDue" runat="server" ControlToValidate="tbInfoSkillDue" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Enter A Valid Date" Type="Date" Operator="DataTypeCheck" />
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                PEL Due
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbPELDue" runat="server" Columns="16" MaxLength="16" />
                                <asp:RequiredFieldValidator ID="rfvPELDue" runat="server" ControlToValidate="tbPELDue"
                                    CssClass="ErrorDisplay" Text="* Required" Display="Dynamic" />
                                <asp:CompareValidator ID="cvPELDue" runat="server" ControlToValidate="tbPELDue" Display="Dynamic"
                                    CssClass="ErrorDisplay" Text="* Enter A Valid Date" Type="Date" Operator="DataTypeCheck" />
                            </div>
                            <%--                    <div class="col-sm-7 text-right">
                        <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Text="Save" Width="125px" OnClick="btnSave_Click" />
                    </div>--%>
                        </div>
                    </div>
                    <div class="TableLabel col-lg-4 text-left">
                        <asp:Repeater ID="rptPELTypes" runat="server" OnItemDataBound="rptPELTypes_ItemDataBound">
                            <ItemTemplate>
                                <div class="text-left">
                                    <span style="font: bold; font-size: 25px;"><%#Eval("TemplateTypeDescription") %></span><br />
                                    <asp:RadioButtonList ID="rblPELs" runat="server" RepeatLayout="flow" />
                                    <br />
                                    <br />
                                    <asp:HiddenField ID="hidEventPELID" runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="col-lg-11 text-right">
                    <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Text="Save" Width="125px" OnClick="btnSave_Click" />
                </div>
        </section>
    </div>


    <div class="modal" id="myModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Registration
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblRegistrationMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnClose_Click" />
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidEventID" runat="server" />
    <asp:HiddenField ID="hidCampaignID" runat="server" />
    <asp:HiddenField ID="hidDaysToPreregistrationDeadline" runat="server" />
    <asp:HiddenField ID="hidDaysToPaymentDue" runat="server" />
    <asp:HiddenField ID="hidDaysToPELDeadlineDate" runat="server" />
    <asp:HiddenField ID="hidDaysToInfoSkillDeadlineDate" runat="server" />
    <asp:HiddenField ID="hidDaysToRegistrationOpenDate" runat="server" />
</asp:Content>
