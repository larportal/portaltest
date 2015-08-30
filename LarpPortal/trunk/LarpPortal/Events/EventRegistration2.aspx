<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="EventRegistration2.aspx.cs" Inherits="LarpPortal.Events.EventRegistration2" %>

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

        .PnlDesign
        {
            border: solid 1px #000000;
            height: 150px;
            width: 330px;
            overflow-y: scroll;
            background-color: #EAEAEA;
            font-size: 15px;
            font-family: Arial;
        }

        .TextEntry
        {
            border: 1px solid black;
            padding: 0px;
        }
    </style>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
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

    </script>

    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#<%= tbArriveDate.ClientID %>").datepicker();
        });
        $(function () {
            $("#<%= tbDepartDate.ClientID %>").datepicker();
        });
    </script>

</asp:Content>

<asp:Content ID="CampaignInfoSetup" ContentPlaceHolderID="MemberCampaignsContent" runat="server">
    <div class="mainContent tab-content col-lg-12">
        <section id="character-info" class="character-info tab-pane active">
            <%--        <section id="campaign-info" class="campaign-info tab-pane active">--%>
            <div role="form" class="form-horizontal form-condensed">
                <div class="col-lg-12">
                    <h1 class="col-lg-12">Event Registration</h1>
                </div>
                <div class="row col-sm-12" style="padding-left: 25px;">
                    <asp:MultiView ID="mvDisplay" runat="server" ActiveViewIndex="1">
                        <asp:View ID="vwNotOpenYet" runat="server">
                            <br />
                            <br />
                            <br />
                            <h2 style="text-align: center; color: black;" class="col-sm-12">Registration for the Silverfire Sept 4th event will be open from<br />
                                Thursday July 30th @ 8:00PM to Saturday August 1st @ 8:00PM.</h2>
                        </asp:View>
                        <asp:View ID="vwRegistrationOpen" runat="server">
                            <asp:MultiView ID="mvPlayerInfo" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vwPlayerInfo" runat="server">
                                    <div class="row col-lg-12">
                                        <div class="col-lg-6">
                                            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                                                <div class="panelheader">
                                                    <h2>Event Registration</h2>
                                                    <div class="panel-body">
                                                        <div class="panel-container" style="padding: 5px 5px 5px 5px;">
                                                            <div class="row PrePostPadding">
                                                                <div class="TableLabel col-sm-3">Select Event Date: </div>
                                                                <div class="col-sm-4 NoPadding">
                                                                    <asp:DropDownList ID="ddlEventDate" CssClass="NoPadding" runat="server" OnSelectedIndexChanged="ddlEventDate_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Text="9/4/2015" Value="9/4/2015" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-lg-5 text-right">
                                                                    <b>My Current Status: </b>
                                                                    <asp:Label ID="lblRegistrationStatus" runat="server" />&nbsp;&nbsp;&nbsp;
                                                                </div>
                                                            </div>
                                                            <div class="row PrePostPadding">
                                                                <div class="TableLabel col-lg-3">Event Status: </div>
                                                                <div class="col-lg-4 NoPadding">
                                                                    <asp:Label ID="lblEventStatus" runat="server" />
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
                                                                            <asp:TextBox ID="tbArriveTime" runat="server" CssClass="TextEntry" Width="100px" TextMode="Time"  />
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
                                                                <div class="col-lg-3 NoPadding">
                                                                    <asp:DropDownList ID="ddlMealPlan" runat="server">
                                                                        <asp:ListItem Text="Friday Dinner Only" Value="Friday" />
                                                                        <asp:ListItem Text="Saturday" Value="Saturday" />
                                                                    </asp:DropDownList>
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
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                                                <div class="panelheader">
                                                    <h2>Event Details</h2>
                                                    <div class="panel-body">
                                                        <div class="panel-container search-criteria">
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
                                                                <div class="TableLabel col-lg-3">
                                                                    PC Meal Combo:
                                                                </div>
                                                                <div class="col-lg-3 NoPadding">
                                                                    <asp:Label ID="lblPCMealComboPrice" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row PrePostPadding">
                                                                <div class="TableLabel col-lg-3">
                                                                    PEL Due:
                                                                </div>
                                                                <div class="col-lg-3 NoPadding">
                                                                    <asp:Label ID="lblPELDueDate" runat="server" />
                                                                </div>
                                                                <div class="TableLabel col-lg-3">
                                                                    NPC Meal Combo:
                                                                </div>
                                                                <div class="col-lg-3 NoPadding">
                                                                    <asp:Label ID="lblNPCMealComboPrice" runat="server" />
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
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row col-lg-12" style="vertical-align: middle; padding-top: 15px;" runat="server" id="divRSVP">
                                                <div class="TableLabel col-lg-2"></div>
                                                <div class="col-lg-4">
                                                    <asp:Button ID="btnRSVPYes" runat="server" CssClass="StandardButton" Width="150px" Text="I Plan to Attend" />
                                                </div>
                                                <div class="TableLabel col-lg-2"></div>
                                                <div class="col-lg-4">
                                                    <asp:Button ID="btnICannotAttend" runat="server" CssClass="StandardButton" Width="150px" Text="I Cannot Attend" />
                                                </div>
                                            </div>
                                            <div class="row col-lg-12" style="padding-top: 15px;" runat="server" id="divRegister">
                                                <div class="col-lg-2">&nbsp;</div>
                                                <div class="col-lg-4">
                                                    <asp:Button ID="btnUnregister" runat="server" CssClass="CancelButton" Width="150px" Text="Unregister" />
                                                </div>
                                                <div class="col-lg-2">&nbsp;</div>
                                                <div class="col-lg-4">
                                                    <asp:Button ID="btnRegister" runat="server" CssClass="StandardButton" Width="150px" Text="Register" OnClick="btnRegister_Click" />
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
                                </asp:View>
                                <asp:View ID="vwNoPlayer" runat="server">
                                    <br />
                                    <br />
                                    <br />
                                    <h2 style="text-align: center; color: black; font-size: 24pt;">You do not have any Silverfire Characters.</h2>
                                </asp:View>
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
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
