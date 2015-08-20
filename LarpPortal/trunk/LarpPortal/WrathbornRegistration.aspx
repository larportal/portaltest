<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="WrathbornRegistration.aspx.cs" Inherits="LarpPortal.WrathbornRegistration" %>

<asp:Content ID="CampaignInfoSetup" ContentPlaceHolderID="MemberCampaignsContent" runat="server">

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
    </style>

    <div class="mainContent tab-content">
        <section id="campaign-info" class="campaign-info tab-pane active">
            <div role="form" class="form-horizontal form-condensed">
                <div class="col-sm-12">
                    <h1 class="col-sm-12">Register for Wrathborn Event 10-2-2015</h1>
                </div>
                <div class="row col-sm-12" style="padding-left: 25px;">

                    <asp:MultiView ID="mvDisplay" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vwNotOpenYet" runat="server">
                            <br />
                            <br />
                            <br />
                            <h2 style="text-align: center; color: black;" class="col-sm-12">Registration for the Wrathborn Oct 2nd event will open on<br />
                                <asp:Label ID="lblStartReg" runat="server" /> <asp:Label ID="lblEndReg" runat="server" /></h2>
                        </asp:View>
                        <asp:View ID="vwRegistrationOpen" runat="server">
                            <asp:MultiView ID="mvPlayerInfo" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vwPlayerInfo" runat="server">
                                    <table class="CharInfoTable" border="0">
                                        <tr>
                                            <td class="TableLabel">Player</td>
                                            <td>
                                                <asp:Label ID="lblPlayerName" runat="server" /></td>
                                        </tr>

                                        <tr>
                                            <td class="TableLabel">Email</td>
                                            <td>
                                                <asp:Label ID="lblPlayerEmail" runat="server" /></td>
                                        </tr>

                                        <tr>
                                            <td class="TableLabel">Role</td>
                                            <td>
                                                <asp:Label ID="lblRole" runat="server" Text="PC" /></td>
                                        </tr>

                                        <tr>
                                            <td class="TableLabel">Character</td>
                                            <td>
                                                <asp:Label ID="lblCharacterAKA" runat="server" /></td>
                                        </tr>

                                        <tr>
                                            <td class="TableLabel">Team</td>
                                            <td>
                                                <asp:DropDownList ID="ddlTeams" runat="server" /><asp:Label ID="lblNoTeamMember" runat="server" Text="Not a member of any team." Visible="false" /></td>
                                        </tr>

                                        <tr style="vertical-align: top;">
                                            <td class="TableLabel">Payment Choice</td>
                                            <td>Describe how you plan to pay for the event. ( Payment is not being collected through this site at this time )</td>
                                        </tr>

                                        <tr style="vertical-align: top;">
                                            <td class="TableLabel"></td>
                                            <td>
                                                <asp:DropDownList ID="ddlPaymentType" runat="server">
                                                    <asp:ListItem Text="Payment" Value="" />
                                                    <asp:ListItem Text="PayPal" Value="1" />
                                                    <asp:ListItem Text="Check" Value="3" />
                                                    <asp:ListItem Text="Kick Starter" Value="9" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPaymentType" runat="server" ErrorMessage="* Please choose a payment method." ControlToValidate="ddlPaymentType"
                                                    InitialValue="" ForeColor="Red" Font-Bold="true" Font-Italic="true" />
                                            </td>
                                        </tr>

                                        <tr style="vertical-align: top;">
                                            <td class="TableLabel">Comments</td>
                                            <td>Describe any special arrangements or considerations (including team changes).</td>
                                        </tr>

                                        <tr style="vertical-align: top;">
                                            <td class="TableLabel"></td>
                                            <td>
                                                <asp:TextBox ID="tbComment" runat="server" TextMode="MultiLine" Rows="3" CssClass="col-sm-12 TableTextBox" /></td>
                                        </tr>

                                        <tr style="vertical-align: top;">
                                            <td class="TableLabel"></td>
                                            <td style="text-align: center; text-align: right;">
                                                <span style="text-align: center;">
                                                    <asp:Label ID="lblAlreadyRegistered" runat="server" Text="You have been added to the wait list for the event." Visible="false" /></span>
                                                <span style="text-align: right;">
                                                    <asp:Button ID="btnRegister" runat="server" CssClass="StandardButton" Width="100" Text="Register" OnClick="btnRegister_Click" /></span>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hidEventID" runat="server" />
                                    <asp:HiddenField ID="hidCharacterID" runat="server" />
                                    <asp:HiddenField ID="hidTeamMember" runat="server" Value="0" />
                                </asp:View>
                                <asp:View ID="vwNoPlayer" runat="server">
                                    <br />
                                    <br />
                                    <br />
                                    <h2 style="text-align: center; color: black; font-size: 24pt;">You do not have any Wraithborne Characters.</h2>
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
                        <asp:View ID="vwRegistrationClosed" runat="server">
                            <br />
                            <br />
                            <br />
                            <h2 style="text-align: center; color: black; font-size: 24pt;">This event is now closed and not accepting any more registrations.</h2>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
