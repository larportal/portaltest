<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="EventRegistration.aspx.cs" Inherits="LarpPortal.Events.EventRegistration" %>

<asp:Content ID="EventStyles" runat="server" ContentPlaceHolderID="MemberStyles">
    <style type="text/css">
        .TableTextBox {
            border: 1px solid black;
            padding-left: 5px;
            padding-right: 5px;
        }

        th, tr:nth-child(even) > td {
            background-color: transparent;
        }

        .CharInfoTable {
            border-collapse: collapse;
        }

            .CharInfoTable td {
                padding: 4px;
            }

        div {
            border: 0px solid black;
        }

        .NoPadding {
            padding-left: 0px;
            padding-right: 0px;
        }

        .TextEntry {
            border: 1px solid black;
            padding: 0px;
        }

        .PnlDesign {
            border: solid 1px #000000;
            height: 150px;
            width: 330px;
            overflow-y: scroll;
            background-color: white;
            font-size: 15px;
            font-family: Arial;
        }

        .txtbox {
            background-image: url(../img/download.png);
            background-position: right top;
            background-repeat: no-repeat;
            cursor: pointer;
            border: 1px solid #A9A9A9;
        }

        .container {
            display: table;
            vertical-align: middle;
        }

        .vertical-center-row {
            display: table-cell;
            vertical-align: middle;
        }

        .contentArea {
            height: auto;
        }

        .MarginLeftRight {
            margin-left: 10px;
            margin-right: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="EventScripts" ContentPlaceHolderID="MemberScripts" runat="server">
    <script type="text/javascript">
        function ddl_changed(ddl) {
            var panel = document.getElementById("<%= divFullEventNo.ClientID %>");
            if (panel) {
                if (ddl.value == "Y")
                    panel.style.display = "none";
                else
                    panel.style.display = "block";
            }
        }

        function ddlSendToCampaign(ddl) {
            var panel = document.getElementById("<%= tbSendToCPOther.ClientID %>");
            if (panel) {
                if (ddl.value == "-1")
                    panel.style.display = "block";
                else
                    panel.style.display = "none";
            }
        }

        $(function () {
            $("#<%= tbArriveDate.ClientID %>").datepicker();
            $("#<%= tbDepartDate.ClientID %>").datepicker();
        });

        function openModal() {
            $('#myModal').modal('show');
        }
        function closeModal() {
            $('#myModal').hide();
        }

        function openPayPalWindow() {
            var win = window.open('/Events/EventPayment.aspx', '_blank');
            win.focus();
        }

        function enablePayNowButton(ddlId) {
            var PayPalID = document.getElementById('<%= hidPayPalTypeID.ClientID%>').value;
            var ControlName = document.getElementById(ddlId.id);

            if (ControlName.value == PayPalID) {
                document.getElementById('<%= btnPayNow.ClientID %>').style.display = '';
            }
            else {
                document.getElementById('<%= btnPayNow.ClientID %>').style.display = 'none';
            }
        }

        function openNoPCChar() {
            $('#modalNoPCChar').modal('show');
        }
        function closeNoPCChar() {
            $('#modalNoPCChar').hide();
        }

    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="CampaignInfoSetup" ContentPlaceHolderID="MemberCampaignsContent" runat="server">

    <div class="mainContent tab-content col-lg-12 input-group">
        <section id="character-info" class="character-info tab-pane active">
            <div role="form" class="form-horizontal">
                <div class="row col-lg-12">
                    <h1>Event Registration/RSVP</h1>
                </div>

                <div class="col-xs-12 NoGutters" runat="server" id="divEvents">
                    <div class="row col-lg-6 col-md-9 col-sm-9 col-xs-12 text-left">
                        <div>
                            Select Event Date: 
                        <asp:DropDownList ID="ddlEventDate" CssClass="NoPadding" runat="server" Style="z-index: 500; position: relative;" OnSelectedIndexChanged="ddlEventDate_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="9/4/2015" Value="9/4/2015" />
                        </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row col-xs-12 col-sm-12 col-md-12 col-lg-12" style="padding-top: 20px; padding-bottom: 20px;">
                        <%--                    <asp:MultiView ID="mvPlayerInfo" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vwPlayerInfo" runat="server">--%>
                        <div class="row col-lg-12 col-sm-12 col-md-12 col-xs-12" style="max-width: 1000px;">
                            <div class="panel NoPadding" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="panelheader NoPadding">
                                    <h2>Event Registration</h2>
                                    <div class="panel-body NoPadding">
                                        <div class="panel-container NoPadding" style="padding: 5px 5px 5px 5px;" runat="server">
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    My Current Status:
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblRegistrationStatus" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Payment Status:
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblPaymentStatus" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">Role:</div>
                                                <div class="col-xs-5 NoPadding">
                                                    <asp:DropDownList ID="ddlRoles" runat="server" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="PC" Value="PC" />
                                                        <asp:ListItem Text="NPC" Value="NPC" />
                                                        <asp:ListItem Text="Staff" Value="Staff" />
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblRole" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">Player: </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblPlayerName" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">EMail: </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblEMail" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding" id="divCharacters" runat="server">
                                                <div class="TableLabel col-xs-3">Character: </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblCharacter" runat="server" />
                                                    <asp:DropDownList ID="ddlCharacterList" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding" id="divSendCPTo" runat="server">
                                                <div class="TableLabel col-xs-3">
                                                    Send CP to 
                                                </div>
                                                <div class="col-xs-9">
                                                    <div class="row">
                                                        <asp:DropDownList ID="ddlSendToCampaign" runat="server" />
                                                    </div>
                                                    <div class="row">
                                                        <asp:TextBox ID="tbSendToCPOther" runat="server" CssClass="col-xs-10" MaxLength="500" Style="display: none;" TextMode="MultiLine" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Full Event: 
                                                </div>
                                                <div class="col-xs-1 NoPadding">
                                                    <asp:DropDownList ID="ddlFullEvent" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Y" Selected="True" />
                                                        <asp:ListItem Text="No" Value="N" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-xs-7" id="divFullEventNo" style="display: none;" runat="server">
                                                    <div class="row NoPadding">
                                                        <div class="col-xs-6 NoPadding">Arrival Date/Time</div>
                                                        <div class="col-xs-6 NoPadding">Depart Date/Time</div>
                                                    </div>
                                                    <div class="row NoPadding">
                                                        <div class="col-xs-6 NoPadding">
                                                            <asp:TextBox ID="tbArriveDate" runat="server" CssClass="TextEntry" Width="100px" />
                                                            <asp:TextBox ID="tbArriveTime" runat="server" CssClass="TextEntry" Width="100px" TextMode="Time" />
                                                        </div>
                                                        <div class="col-xs-6 NoPadding">
                                                            <asp:TextBox ID="tbDepartDate" runat="server" CssClass="TextEntry" Width="100px" />
                                                            <asp:TextBox ID="tbDepartTime" runat="server" CssClass="TextEntry" Width="100px" TextMode="Time" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding" runat="server" id="divTeams">
                                                <div class="TableLabel col-xs-3">Team Name: </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:DropDownList ID="ddlTeams" runat="server">
                                                        <asp:ListItem Text="Team 1" Value="Team1" />
                                                        <asp:ListItem Text="Team 2" Value="Team2" />
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblNoTeams" runat="server" Text="No Teams" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding" runat="server" id="divHousePref">
                                                <div class="TableLabel col-xs-3">Housing Preference: </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:TextBox ID="tbReqstdHousing" runat="server" CssClass="TextEntry col-sm-11" />
                                                    <asp:Label ID="lblReqstdHousing" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding" runat="server" id="divHouseAssign">
                                                <div class="TableLabel col-xs-3">Assign Housing:</div>
                                                <div class="col-sm-9 NoPadding">
                                                    <asp:Label ID="lblAssignHousing" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">Meal Plan: </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:MultiView ID="mvMenu" runat="server" ActiveViewIndex="0">
                                                        <asp:View ID="vwFoodAvail" runat="server">
                                                            <asp:UpdatePanel ID="upMealSelection" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbSelectedMeals" Text="" runat="server" CssClass="txtbox"
                                                                        Height="20px" Width="322px"></asp:TextBox>
                                                                    <asp:Panel ID="PnlMealList" runat="server" CssClass="PnlDesign">
                                                                        <asp:CheckBoxList ID="cbMealList" runat="server" Width="200px" RepeatLayout="flow" CssClass="CheckBoxList" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="cbFoodList_SelectedIndexChanged">
                                                                            <asp:ListItem Text="One" Value="one" />
                                                                            <asp:ListItem Text="Two" Value="two" />
                                                                        </asp:CheckBoxList>
                                                                    </asp:Panel>
                                                                    <ajaxToolkit:PopupControlExtender ID="PceSelectCustomer" runat="server" TargetControlID="tbSelectedMeals"
                                                                        PopupControlID="PnlMealList" Position="Bottom">
                                                                    </ajaxToolkit:PopupControlExtender>
                                                                    <asp:Label ID="Label1" runat="server" Text="" Style="visibility: hidden; border: 1px solid black;"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </asp:View>
                                                        <asp:View ID="vwNoFood" runat="server">
                                                            This event has not been set up with food services.
                                                        </asp:View>
                                                    </asp:MultiView>
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">Payment Instructions: </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblPaymentInstructions2" runat="server" CssClass="col-xs-11 NoPadding" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">Payment Choice: </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:DropDownList ID="ddlPaymentChoice" runat="server" />
                                                    <asp:Button ID="btnPayNow" runat="server" CssClass="StandardButton" Width="125" Text="Pay Now"
                                                        OnClientClick="openPayPalWindow(); return false;" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">Comments: </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" CssClass="col-xs-11 NoPadding" Rows="4" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row col-xs-12 text-center" style="padding-top: 20px;">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger MarginLeftRight" Width="125" OnCommand="CancelReg" />
                                <%--<asp:Button ID="btnCancelRSVP" runat="server" Text="I Cannot Attend" CssClass="btn btn-danger MarginLeftRight" Width="125" OnCommand="CancelReg" />--%>
                                <asp:Button ID="btnChange" runat="server" Text="Change Reg" CssClass="StandardButton MarginLeftRight" Width="125" OnCommand="Register" />
                                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="StandardButton MarginLeftRight" Width="125" OnCommand="Register" />
                                <asp:Button ID="btnRSVPNo" runat="server" Text="I Cannot Attend" CssClass="btn btn-danger MarginLeftRight" Width="125" OnCommand="RSVPEvent" CommandName="RSVPNO" />
                                <asp:Button ID="btnRSVP" runat="server" Text="I Will Attend" CssClass="StandardButton MarginLeftRight" Width="125" OnCommand="RSVPEvent" CommandName="RSVP" />
                            </div>

                            <div class="row col-xs-12 text-center">
                                <asp:Label ID="lblWhyRSVP" runat="server" Visible="false" Text="Currently this event is not yet open for registration.<br />By letting the owners know whether you plan to attend an event you will help in planning.<br />" />
                                <asp:Label ID="lblAlreadyHappened" runat="server" Visible="false" Text="This event has already happened. You cannot change your registration after the event." />
                                <asp:Label ID="lblClosedToPC" runat="server" Visible="false" Text="Event has been closed to new PC registration.<br />" />
                                <asp:Label ID="lblPastEvent" runat="server" Text="Event  has already happened." />
                            </div>

                            <%--                                <asp:MultiView ID="mvButtons" runat="server">
                                    <asp:View ID="vwAlreadyHappened" runat="server">
                                        <div class="row col-xs-12 NoPadding" style="padding-top: 15px;">
                                            <div class="col-xs-12 text-center">
                                                This event has already happened. You cannot change your registration after the event.
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwRSVPButtons" runat="server">
                                        <div class="row col-xs-12 NoPadding" style="padding-top: 15px;">
                                            <div class="col-xs-12 text-center">
                                                <asp:Label ID="lblWhyRSVP" runat="server" Text="Currently this event is not yet open for registration.<br />By letting the owners know whether you plan to attend an event you will help in planning.<br />" />
                                                <asp:Label ID="lblClosedToPC" runat="server" Text="Event has been closed to new PC registration.<br />" />
                                                <br />
                                            </div>
                                        </div>
                                        <div class="row col-xs-12 NoPadding">
                                            <div class="col-xs-2 NoPadding">&nbsp;</div>
                                            <div class="col-xs-4 NoPadding" style="float: left;">
                                                <asp:Button ID="btnRSVPYes" runat="server" CssClass="StandardButton" Width="150px" Text="I Plan to Attend"
                                                    OnCommand="btnRegister_Command" CommandName="RSVPATTEND" />
                                            </div>
                                            <div class="col-xs-4 NoPadding" style="float: right;">
                                                <asp:Button ID="btnICannotAttend" runat="server" CssClass="StandardButton" Width="150px" Text="I Cannot Attend"
                                                    OnCommand="btnRegister_Command" CommandName="RSVPCANNOTATTEND" />
                                            </div>
                                            <div class="col-xs-2 NoPadding" style="clear: both;">&nbsp;</div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwRegisterButtons" runat="server">
                                        <div class="row col-xs-12 NoPadding" style="padding-top: 15px;">
                                            <div class="col-xs-2">&nbsp;</div>
                                            <div class="col-xs-4 NoPadding">
                                                <asp:Button ID="btnUnregister" runat="server" CssClass="CancelButton" Width="150px" Text="Unregister"
                                                    OnCommand="btnRegister_Command" CommandName="UNREGISTER" />
                                            </div>
                                            <div class="col-xs-4 NoPadding" style="text-align: right;">
                                                <asp:Button ID="btnRegister" runat="server" CssClass="StandardButton" Width="150px" Text="Register"
                                                    OnCommand="btnRegister_Command" CommandName="REGISTER" />
                                            </div>
                                            <div class="col-xs-2">&nbsp;</div>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>--%>
                        </div>
                        <br />
                        <div class="row col-lg-12 col-sm-12 col-md-12 col-xs-12" style="max-width: 1000px; margin-top: 25px;">
                            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="panelheader NoPadding">
                                    <h2>Event Details</h2>
                                    <div class="panel-body NoPadding">
                                        <div class="panel-container">
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Event Status:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblEventStatus" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Event Name: 
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblEventName" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Description: 
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblEventDescription" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    In Game Location: 
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblInGameLocation" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Start Date: 
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblEventStartDate" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    End Date:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblEventEndDate" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Site Location: 
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Label ID="lblSiteLocation" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Reg Open Date:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblEventOpenDate" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Reg Close Date:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblEventCloseDate" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Pre Reg Price:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblPreRegPrice" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Pre Reg:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblPreRegDate" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Reg Price:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblRegPrice" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Payment Due:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblPaymentDue" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    At Door Price:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblDoorPrice" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Info Skill Due:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblInfoSkillDueDate" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    PEL Due:
                                                </div>
                                                <div class="col-xs-3 NoPadding">
                                                    <asp:Label ID="lblPELDueDate" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    Available: 
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Image ID="imgPCFoodService" runat="server" ImageUrl="~/img/Checked-Checkbox-icon.png" />
                                                    PC Food Services
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    &nbsp;
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Image ID="imgNPCFoodService" runat="server" ImageUrl="~/img/Unchecked-Checkbox-icon.png" />
                                                    NPC Food Services
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    &nbsp;
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Image ID="imgCookingAllowed" runat="server" ImageUrl="~/img/Unchecked-Checkbox-icon.png" />
                                                    Cooking Allowed
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    &nbsp;
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Image ID="imgRefrigerator" runat="server" ImageUrl="~/img/Unchecked-Checkbox-icon.png" />
                                                    Refigerator
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    &nbsp;
                                                </div>
                                                <div class="col-xs-9 NoPadding">
                                                    <asp:Image ID="imgMenu" runat="server" ImageUrl="~/img/Unchecked-Checkbox-icon.png" />
                                                    Menu Prices 
                                                </div>
                                            </div>
                                            <div class="row PrePostPadding">
                                                <div class="TableLabel col-xs-3">
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            &nbsp;
                        </div>
                        <asp:HiddenField ID="hidRegistrationID" runat="server" />
                        <asp:HiddenField ID="hidCharacterID" runat="server" />
                        <asp:HiddenField ID="hidTeamMember" runat="server" Value="0" />
                        <div class="row" style="padding-bottom: 25px;">&nbsp;</div>
                    </div>
            </div>
            <div class="col-xs-12 NoGutters" id="divNoEvents" runat="server">
                There are no upcoming events for this campaign.
            </div>
            <div class="col-xs-12 NoGutters" id="divNoPriv" runat="server">
                You do not have the privileges required to register for an event for this campaign.
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
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" CssClass="StandardButton" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalNoPCChar" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: red;">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Registration
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        You do not have any PC Characters. In order to register for an event as a PC, you must first create one.<br />
                        <br />
                        Do you want to create a character now?
                </div>
                <div class="modal-footer">
                    <div class="text-left col-lg-6">
                        <asp:Button ID="btnStayAsNPC" runat="server" Text="Stay as NPC" Width="150px" CssClass="StandardButton" OnClick="btnStayAsNPC_Click" />
                    </div>
                    <div class="text-right col-lg-6">
                        <asp:Button ID="btnCreateACharacter" runat="server" Text="Create Character" Width="150px" CssClass="StandardButton" OnClick="btnCreateACharacter_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidCampaignName" runat="server" />
    <asp:HiddenField ID="hidCampaignEMail" runat="server" />
    <asp:HiddenField ID="hidCharAKA" runat="server" />
    <asp:HiddenField ID="hidPlayerEMail" runat="server" />
    <asp:HiddenField ID="hidRegistrationStatusID" runat="server" />
    <asp:HiddenField ID="hidPayPalTypeID" runat="server" />

    <asp:HiddenField ID="hidHasPCChar" runat="server" />
    <asp:HiddenField ID="hidRegOpen" runat="server" />
    <asp:HiddenField ID="hidRegClose" runat="server" />
    <asp:HiddenField ID="hidEventClose" runat="server" />
    <asp:HiddenField ID="hidPELClose" runat="server" />
    <asp:HiddenField ID="hidCurrentRegStatus" runat="server" />
</asp:Content>
