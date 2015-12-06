<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="EventDefaults.aspx.cs" Inherits="LarpPortal.Events.EventDefaults" %>

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
                    <h1 class="col-lg-12">Event Setup Campaign Defaults</h1>
                </div>
                <div class="col-lg-12 NoPadding row">
                    <div class="col-lg-8 NoPadding">
                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-sm-3">Start/End Time</div>
                            <div class="col-sm-3 NoPadding">
                                <asp:TextBox ID="tbStartTime" runat="server" CssClass="col-sm-5 NoPadding" TextMode="Time" />
                                <asp:TextBox ID="tbEndTime" runat="server" CssClass="col-sm-5 NoPadding" Style="margin-left: 10px;" TextMode="Time" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                Maximum PC Count
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbMaxPCCount" runat="server" Columns="4" MaxLength="4" /><asp:RangeValidator ID="rvMaxPCCount" runat="server"
                                    ForeColor="Red" MaximumValue="999" MinimumValue="-999" Font-Bold="true" Font-Italic="true" Text="* Numbers Only" Type="Integer"
                                    ControlToValidate="tbMaxPCCount" Style="margin-left: 10px;" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Primary Site
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlSiteList" runat="server" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                BaseNPC Count
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbBaseNPCCount" runat="server" Columns="4" MaxLength="4" /><asp:RangeValidator ID="rvBaseNPCCount" runat="server"
                                    ForeColor="Red" MaximumValue="999" MinimumValue="-999" Font-Bold="true" Font-Italic="true" Text="* Numbers Only" Type="Integer"
                                    ControlToValidate="tbBaseNPCCount" Style="margin-left: 10px;" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Default Reg Status
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlDefaultRegStatus" runat="server" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                NPC Override Ratio
                            </div>
                            <div class="col-lg3 NoPadding">
                                <asp:TextBox ID="tbOverrideRatio" runat="server" Columns="4" MaxLength="4" /><asp:RangeValidator ID="rvOverrideRatio" runat="server"
                                    ForeColor="Red" MaximumValue="999" MinimumValue="-999" Font-Bold="true" Font-Italic="true" Text="* Numbers Only" Type="Integer"
                                    ControlToValidate="tbOverrideRatio" Style="margin-left: 10px;" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Open Reg Date
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbOpenRegDate" runat="server" CssClass="TableTextBox" Columns="16" MaxLength="16" /><asp:RangeValidator ID="rvOpenRegDate" runat="server"
                                    ForeColor="Red" MaximumValue="999" MinimumValue="-999" Font-Bold="true" Font-Italic="true" Text="* Numbers Only" Type="Integer"
                                    ControlToValidate="tbOpenRegDate" Style="margin-left: 10px;" Display="Dynamic" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                Cap New Notification
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlCapNearNotification" runat="server" Style="vertical-align: middle;">
                                    <asp:ListItem Text="No default" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Open Reg Time
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbOpenRegTime" runat="server" TextMode="Time" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                Cap Near Notification
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbCapThresholdNotification" runat="server" Columns="4" MaxLength="4" /><asp:RangeValidator ID="rvCapThresholdNotification" runat="server"
                                    ForeColor="Red" MaximumValue="999" MinimumValue="-999" Font-Bold="true" Font-Italic="true" Text="* Numbers Only" Type="Integer"
                                    ControlToValidate="tbCapThresholdNotification" Style="margin-left: 10px;" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Pre Reg Deadline
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbPreRegDeadline" runat="server" Columns="16" MaxLength="16" /><asp:RangeValidator ID="rvPreRegDeadline" runat="server"
                                    ForeColor="Red" MaximumValue="999" MinimumValue="-999" Font-Bold="true" Font-Italic="true" Text="* Numbers Only" Type="Integer"
                                    ControlToValidate="tbPreRegDeadline" Style="margin-left: 10px;" Display="Dynamic" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                Auto Approve Waitlist
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlAutoApproveWaitlist" runat="server" Style="vertical-align: middle;">
                                    <asp:ListItem Text="No default" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row col-sm-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Payment Date
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbPaymentDate" runat="server" Columns="16" MaxLength="16" /><asp:RangeValidator ID="rvPaymentDate" runat="server"
                                    ForeColor="Red" MaximumValue="999" MinimumValue="-999" Font-Bold="true" Font-Italic="true" Text="* Numbers Only" Type="Integer"
                                    ControlToValidate="tbPaymentDate" Style="margin-left: 10px;" Display="Dynamic" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                PC Food Service
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlPCFoodService" runat="server">
                                    <asp:ListItem Text="No default" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Pre Registration Price
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbPreRegistrationPrice" runat="server" Columns="8" MaxLength="8" /><asp:RegularExpressionValidator ID="rePreRegistrationPrice" runat="server"
                                    ControlToValidate="tbPreRegistrationPrice" ErrorMessage="* Enter Currency" ValidationExpression="^\d+(\.\d\d)?$" Font-Bold="true" Font-Italic="true"
                                    ForeColor="red" Display="Dynamic" />
                            </div>
                            <div class="TableLabel col-lg-3">
                                NPC Food Service
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlNPCFoodService" runat="server">
                                    <asp:ListItem Text="No default" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Reg Price
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbRegPrice" runat="server" Columns="8" MaxLength="8" /><asp:RegularExpressionValidator ID="reRegPrice" runat="server" ControlToValidate="tbRegPrice"
                                    ErrorMessage="* Enter Currency" ValidationExpression="^\d+(\.\d\d)?$" Font-Bold="true" Font-Italic="true" ForeColor="red" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                At Door Price
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbAtDoorPrice" runat="server" Columns="8" MaxLength="8" /><asp:RegularExpressionValidator ID="reAtDoorPrice" runat="server" ControlToValidate="tbAtDoorPrice"
                                    ErrorMessage="* Enter Currency" ValidationExpression="^\d+(\.\d\d)?$" Font-Bold="true" Font-Italic="true" ForeColor="red" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Payment Instructions
                            </div>
                            <div class="col-lg-9 NoPadding">
                                <asp:TextBox ID="tbPaymentInstructions" runat="server" TextMode="MultiLine" Rows="3" CssClass="col-sm-11 NoLeftPadding" />
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Auto Cancel Reg
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:DropDownList ID="ddlAutoCancelReg" runat="server" Style="vertical-align: middle;">
                                    <asp:ListItem Text="No default" Value="" Selected="true" />
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                Info Skill Due
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbInfoSkillDue" runat="server" Columns="16" MaxLength="16" /><asp:RangeValidator ID="rvInfoSkillDue" runat="server"
                                    ForeColor="Red" MaximumValue="999" MinimumValue="-999" Font-Bold="true" Font-Italic="true" Text="* Numbers Only" Type="Integer"
                                    ControlToValidate="tbInfoSkillDue" Style="margin-left: 10px;" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="row col-lg-12 NoPadding">
                            <div class="TableLabel col-lg-3">
                                PEL Due
                            </div>
                            <div class="col-lg-3 NoPadding">
                                <asp:TextBox ID="tbPELDue" runat="server" Columns="16" MaxLength="16" /><asp:RangeValidator ID="rvPELDue" runat="server"
                                    ForeColor="Red" MaximumValue="999" MinimumValue="-999" Font-Bold="true" Font-Italic="true" Text="* Numbers Only" Type="Integer"
                                    ControlToValidate="tbPELDue" Style="margin-left: 10px;" Display="Dynamic" />
                            </div>
                        </div>
                    </div>

                    <div class="TableLabel col-lg-4 text-left">
                        <div class="row">
                            <div class="col-lg-12 NoPadding text-left" style="font-size: 16px; padding-bottom: 20px;">
                                <b>Default PEL Selection</b>
                            </div>
                        </div>
                        <asp:Repeater ID="rptPELTypes" runat="server" OnItemDataBound="rptPELTypes_ItemDataBound">
                            <ItemTemplate>
                                <div class="text-left">
                                    <span style="font: bold;"><%#Eval("TemplateTypeDescription") %></span><br />
                                    <asp:RadioButtonList ID="rblPELs" runat="server" RepeatLayout="flow" />
                                    <br />
                                    <br />
                                </div>
                                <asp:HiddenField ID="hidTemplateTypeID" runat="server" Value='<%#Eval("PELTemplateTypeID") %>' />
                            </ItemTemplate>


                            <%--                            <ItemTemplate>
                                <div class="row" style="margin-bottom: 20px">
                                    <div class="panel text-left" style="padding-top: 0px; padding-bottom: 0px;">
                                        <div class="panelheader">
                                    <h2><%#Eval("TemplateTypeDescription") %></h2>
                                            <div class="panel-body">
                                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                                    <asp:RadioButtonList ID="rblPELs" runat="server" RepeatLayout="flow" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>--%>
                        </asp:Repeater>
                    </div>

                </div>

                <div class="col-lg-11 text-right">
                    <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Text="Save" Width="125px" OnClick="btnSave_Click" />
                </div>
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

    <asp:HiddenField ID="hidDefaultID" runat="server" />
</asp:Content>
