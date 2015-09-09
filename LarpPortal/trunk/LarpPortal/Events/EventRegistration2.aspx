﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="EventRegistration2.aspx.cs" Inherits="LarpPortal.Events.EventRegistration2" %>

<asp:Content ID="EventStyles" runat="server" ContentPlaceHolderID="MemberStyles">
    <style type="text/css">
        .TableTextBox
        {
            border: 1px solid black;
            padding-left: 5px;
            padding-right: 5px;
        }

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

        div
        {
            border: 0px solid black;
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
            cursor: hand;
        }
    </style>

    <link rel="stylesheet" href="http://localhost:49282/code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
</asp:Content>

<asp:Content ID="EventScripts" ContentPlaceHolderID="MemberScripts" runat="server">
    <script type="text/javascript">
        $('.table > tr').click(function () {
            alert("Row Clicked");
            // row was clicked
        });

        function ddl_changed(ddl) {
            var panel = document.getElementById("<%= divFullEventNo.ClientID %>");
            if (panel) {
                if (ddl.value == "Y")
                    panel.style.display = "none";
                else
                    panel.style.display = "block";
            }
        }

        $(function () {
            $("#<%= tbArriveDate.ClientID %>").datepicker();
            $("#<%= tbDepartDate.ClientID %>").datepicker();
        });
    </script>

    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    <script>
    </script>

</asp:Content>

<asp:Content ID="CampaignInfoSetup" ContentPlaceHolderID="MemberCampaignsContent" runat="server">

    <div class="mainContent tab-content col-lg-12 input-group">
        <section id="character-info" class="character-info tab-pane active">
            <div role="form" class="form-horizontal">
                <div class="col-lg-12 NoPadding">
                    <h1 class="col-lg-12">Event Registration/RSVP</h1>
                </div>
                <div class="row col-sm-12 NoPadding" style="padding-left: 25px;">
                    <asp:MultiView ID="mvPlayerInfo" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vwPlayerInfo" runat="server">
                            <div class="row col-lg-12 NoPadding">
                                <div class="col-lg-6 NoPadding">
                                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                                        <div class="panelheader NoPadding">
                                            <h2>Event Details</h2>
                                            <div class="panel-body NoPadding">
                                                <div class="panel-container">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Select Event Date: </div>
                                                        <div class="col-sm-4 NoPadding">
                                                            <asp:DropDownList ID="ddlEventDate" CssClass="NoPadding" runat="server" OnSelectedIndexChanged="ddlEventDate_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="9/4/2015" Value="9/4/2015" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="TableLabel col-lg-2">Event Status: </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblEventStatus" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Event Name: 
                                                        </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:Label ID="lblEventName" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Description: 
                                                        </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:Label ID="lblEventDescription" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            In Game Location: 
                                                        </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:Label ID="lblInGameLocation" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Start Date: 
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblEventStartDate" runat="server" />
                                                        </div>
                                                        <div class="TableLabel col-lg-3">
                                                            End Date:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblEventEndDate" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Site Location: 
                                                        </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:Label ID="lblSiteLocation" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Open Date:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblEventOpenDate" runat="server" />
                                                        </div>
                                                        <div class="TableLabel col-lg-3">
                                                            Pre Reg Price:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblPreRegPrice" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Pre Reg:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblPreRegDate" runat="server" />
                                                        </div>
                                                        <div class="TableLabel col-lg-3">
                                                            Reg Price:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblRegPrice" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Payment Due:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblPaymentDue" runat="server" />
                                                        </div>
                                                        <div class="TableLabel col-lg-3">
                                                            At Door Price:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblDoorPrice" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Info Skill Due:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblInfoSkillDueDate" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            PEL Due:
                                                        </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:Label ID="lblPELDueDate" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Available: 
                                                        </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:Image ID="imgPCFoodService" runat="server" ImageUrl="~/img/Checked-Checkbox-icon.png" />
                                                            PC Food Services
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:Image ID="imgNPCFoodService" runat="server" ImageUrl="~/img/Unchecked-Checkbox-icon.png" />
                                                            NPC Food Services
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:Image ID="imgCookingAllowed" runat="server" ImageUrl="~/img/Unchecked-Checkbox-icon.png" />
                                                            Cooking Allowed
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:Image ID="imgRefrigerator" runat="server" ImageUrl="~/img/Unchecked-Checkbox-icon.png" />
                                                            Refigerator
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:Image ID="imgMenu" runat="server" ImageUrl="~/img/Unchecked-Checkbox-icon.png" />
                                                            Menu Prices 
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 NoPadding" style="padding-left: 25px;">
                                    <div class="panel NoPadding" style="padding-top: 0px; padding-bottom: 0px;">
                                        <div class="panelheader NoPadding">
                                            <h2>Event Registration</h2>
                                            <div class="panel-body NoPadding">
                                                <div class="panel-container NoPadding" style="padding: 5px 5px 5px 5px;">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            My Current Status:
                                                        </div>
                                                        <div class="col-lg-5 NoPadding">
                                                            <asp:Label ID="lblRegistrationStatus" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">Role:</div>
                                                        <div class="col-lg-5 NoPadding">
                                                            <asp:DropDownList ID="ddlRoles" runat="server">
                                                                <asp:ListItem Text="PC" Value="PC" />
                                                                <asp:ListItem Text="NPC" Value="NPC" />
                                                                <asp:ListItem Text="Staff" Value="Staff" />
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblRole" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">Player: </div>
                                                        <div class="col-lg-5 NoPadding">
                                                            <asp:Label ID="lblPlayerName" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">EMail: </div>
                                                        <div class="col-lg-5 NoPadding">
                                                            <asp:Label ID="lblEMail" runat="server" />
                                                        </div>
                                                    </div>
                                                    <asp:MultiView ID="mvCharacters" runat="server" ActiveViewIndex="0">
                                                        <asp:View ID="vwCharacter" runat="server">
                                                            <div class="row PrePostPadding" id="divCharacters" runat="server">
                                                                <div class="TableLabel col-lg-3">Character: </div>
                                                                <div class="col-lg-5 NoPadding">
                                                                    <asp:Label ID="lblCharacter" runat="server" />
                                                                    <asp:DropDownList ID="ddlCharacterList" runat="server" />
                                                                </div>
                                                            </div>
                                                        </asp:View>
                                                        <asp:View ID="vwSendCPTo" runat="server">
                                                            <div class="row PrePostPadding" id="divSendCPTo" runat="server">
                                                                <div class="TableLabel col-lg-2">
                                                                    Send CP to 
                                                                </div>
                                                                <div class="col-lg-2 NoPadding">
                                                                    <asp:TextBox ID="tbSendCPTo" runat="server" />
                                                                </div>
                                                            </div>
                                                        </asp:View>
                                                    </asp:MultiView>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">
                                                            Full Event: 
                                                        </div>
                                                        <div class="col-lg-1 NoPadding">
                                                            <asp:DropDownList ID="ddlFullEvent" runat="server" onchange="ddl_changed(this);">
                                                                <asp:ListItem Text="Yes" Value="Y" Selected="True" />
                                                                <asp:ListItem Text="No" Value="N" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-7" id="divFullEventNo" style="display: none;" runat="server">
                                                            <div class="row NoPadding">
                                                                <div class="col-lg-6 NoPadding">Arrival Date/Time</div>
                                                                <div class="col-lg-6 NoPadding">Depart Date/Time</div>
                                                            </div>
                                                            <div class="row NoPadding">
                                                                <div class="col-lg-6 NoPadding">
                                                                    <asp:TextBox ID="tbArriveDate" runat="server" CssClass="TextEntry" Width="100px" />
                                                                    <asp:TextBox ID="tbArriveTime" runat="server" CssClass="TextEntry" Width="100px" TextMode="Time" />
                                                                </div>
                                                                <div class="col-lg-6 NoPadding">
                                                                    <asp:TextBox ID="tbDepartDate" runat="server" CssClass="TextEntry" Width="100px" />
                                                                    <asp:TextBox ID="tbDepartTime" runat="server" CssClass="TextEntry" Width="100px" TextMode="Time" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">Team Name: </div>
                                                        <div class="col-lg-4 NoPadding">
                                                            <asp:DropDownList ID="ddlTeams" runat="server">
                                                                <asp:ListItem Text="Team 1" Value="Team1" />
                                                                <asp:ListItem Text="Team 2" Value="Team2" />
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblNoTeams" runat="server" Text="No Teams" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">Housing Plan: </div>
                                                        <div class="col-lg-3 NoPadding">
                                                            <asp:DropDownList ID="ddlHousing" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">Meal Plan: </div>
                                                        <div class="col-lg-6 NoPadding">
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
                                                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>


                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">Payment Instructions: </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:TextBox ID="tbPayment" runat="server" TextMode="MultiLine" CssClass="col-lg-11 NoPadding" Rows="4" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">Payment Choice: </div>
                                                        <div class="col-lg-4 NoPadding">
                                                            <asp:DropDownList ID="ddlPaymentChoice" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-lg-3">Comments: </div>
                                                        <div class="col-lg-9 NoPadding">
                                                            <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" CssClass="col-lg-11 NoPadding" Rows="4" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:MultiView ID="mvButtons" runat="server">
                                        <asp:View ID="vwRSVPButtons" runat="server">
                                            <div class="row col-lg-12 NoPadding" style="padding-top: 15px;">
                                                <div class="col-lg-12 text-center">
                                                    Currently this event is not yet open for registration.<br />
                                                    By letting the owners know whether you plan to attend an event you will help in planning.<br />
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row col-lg-12 NoPadding">
                                                <div class="col-lg-2 NoPadding">&nbsp;</div>
                                                <div class="col-lg-4 NoPadding" style="float: left;">
                                                    <asp:Button ID="btnRSVPYes" runat="server" CssClass="StandardButton" Width="150px" Text="I Plan to Attend"
                                                        OnCommand="btnRegister_Command" CommandName="RSVPATTEND" />
                                                </div>
                                                <div class="col-lg-4 NoPadding" style="float: right;">
                                                    <asp:Button ID="btnICannotAttend" runat="server" CssClass="StandardButton" Width="150px" Text="I Cannot Attend"
                                                        OnCommand="btnRegister_Command" CommandName="RSVPCANNOTATTEND" />
                                                </div>
                                                <div class="col-lg-2 NoPadding" style="clear: both;">&nbsp;</div>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="vwRegisterButtons" runat="server">
                                            <div class="row col-lg-12 NoPadding" style="padding-top: 15px;">
                                                <div class="col-lg-2">&nbsp;</div>
                                                <div class="col-lg-4 NoPadding">
                                                    <asp:Button ID="btnUnregister" runat="server" CssClass="CancelButton" Width="150px" Text="Unregister"
                                                        OnCommand="btnRegister_Command" CommandName="UNREGISTER" />
                                                </div>
                                                <div class="col-lg-4 NoPadding" style="text-align: right;">
                                                    <asp:Button ID="btnRegister" runat="server" CssClass="StandardButton" Width="150px" Text="Register"
                                                        OnCommand="btnRegister_Command" CommandName="REGISTER" />
                                                </div>
                                                <div class="col-lg-2">&nbsp;</div>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                            <div class="row">
                                &nbsp;
                            </div>
                            <asp:HiddenField ID="hidRegistrationID" runat="server" />
                            <asp:HiddenField ID="hidCharacterID" runat="server" />
                            <asp:HiddenField ID="hidTeamMember" runat="server" Value="0" />
                        </asp:View>
                        <%--                                <asp:View ID="vwNoPlayer" runat="server">
                                    <br />
                                    <br />
                                    <br />
                                    <h2 style="text-align: center; color: black; font-size: 24pt;">You do not have any Silverfire Characters.</h2>
                                </asp:View>--%>
                        <asp:View ID="vwRegistered" runat="server">
                            <br />
                            <br />
                            <br />
                            <h2 style="text-align: center; color: black; font-size: 24pt;">You have been added to the wait list for this event.</h2>
                            <br />
                            <h2 style="text-align: center; color: black; font-size: 24pt;">You will be notified via email if you are registered.</h2>
                        </asp:View>
                        <asp:View ID="vwError" runat="server">
                            <br />
                            <br />
                            <br />
                            <h2 style="text-align: center; color: red; font-size: 24pt;">There was an issue registering for the event.<br />
                                Information has been sent to the LARPortal staff.</h2>
                        </asp:View>

                    </asp:MultiView>
                    <%--                        </asp:View>
                    </asp:MultiView>--%>
                </div>
            </div>
        </section>
    </div>
</asp:Content>