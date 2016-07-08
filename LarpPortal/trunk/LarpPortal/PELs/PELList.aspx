<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PELList.aspx.cs" Inherits="LarpPortal.PELs.PELList" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ID="ScriptSection" runat="server" ContentPlaceHolderID="MemberScripts">

    <script type="text/javascript">
        function DisplayRoles(DropDownListRoles) {
            var Role = DropDownListRoles.options[DropDownListRoles.selectedIndex].text;
            if (Role != null) {
                var trPCStaff = document.getElementById('<%= divPCStaff.ClientID %>');
                var trNPC = document.getElementById('<%= divNPC.ClientID %>');
                var trSendCPOther = document.getElementById('<%= divSendOther.ClientID %>');
                if ((Role == 'PC') || (Role == 'Staff')) {
                    trPCStaff.style.display = 'block';
                    trNPC.style.display = 'none';
                    trSendCPOther.style.display = 'none';
                }
                else {
                    trPCStaff.style.display = 'none';
                    trNPC.style.display = 'block';
                    trSendCPOther.style.display = 'block';
                }
            }
        }

        function closeRegistration() {
            $('#modalRegistration').hide();
        }

        function openMessage() {
            $('#modalMessage').modal('show');
        }
        function closeMessage() {
            $('#modalMessage').hide();
        }


    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px; vertical-align: bottom;">
            <div class="col-sm-6">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="PEL (Post Event Letter)" />
            </div>
            <div class="col-sm-6 text-right">
                <button type="button" data-toggle="modal" data-target="#modalRegistration" class="StandardButton" style="width: 125px;">Missing Event?</button>
            </div>
        </div>
        <div class="row" style="padding-left: 15px;">
            <asp:Label ID="lblCharInfo" runat="server" />
        </div>
        <div class="row" style="padding-left: 15px;">
            <asp:MultiView ID="mvPELList" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwPELList" runat="server">
                    <div class="panel" style="padding-top: 0px; padding-bottom: 0px;">
                        <div class="panelheader">
                            <h2>Event List</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                                    <div style="max-height: 500px; overflow-y: auto; margin-right: 10px;">
                                        <asp:GridView ID="gvPELList" runat="server" AutoGenerateColumns="false" OnRowCommand="gvPELList_RowCommand" GridLines="None"
                                            CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" BorderWidth="1">
                                            <Columns>
                                                <asp:BoundField DataField="CampaignName" HeaderText="Campaign" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventStartDate" HeaderText="Event Date" DataFormatString="{0: MM/dd/yyyy}" ItemStyle-Wrap="false"
                                                    HeaderStyle-Wrap="false" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="CharacterAKA" HeaderText="Character" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventName" HeaderText="Event Name" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="EventDescription" HeaderText="Event Description" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:BoundField DataField="PELStatus" HeaderText="Status" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                    ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAddendum" runat="server" CommandName="Addendum" Text="&nbsp;&nbsp;Add Addendum&nbsp;&nbsp;" Width="100px" CssClass="StandardButton"
                                                            Visible='<%# Eval("DisplayAddendum") %>' CommandArgument='<%# Eval("RegistrationID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="LeftRightPadding">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCommand" Width="100px" runat="server" CommandArgument='<%# Eval("RegistrationID") %>' CommandName='<%# Eval("ButtonText") %>Item'
                                                            Style="" Text='<%# Eval("ButtonText") %>' CssClass="StandardButton" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vwNoPELs" runat="server">
                    <p>
                        <strong>You do not have any open PEL for the campaign
                            <asp:Label ID="lblCampaignName" runat="server" />.
                        </strong>
                    </p>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>

    <style type="text/css">
        .Padding5 {
            padding-top: 5px;
        }
    </style>

    <div class="modal fade" id="modalRegistration" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Enter Event Information
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="row">
                        <div class="TableLabel col-sm-4">
                            Event List:
                        </div>
                        <div class="col-sm-8 NoLeftPadding">
                            <asp:DropDownList ID="ddlMissedEvents" runat="server" OnSelectedIndexChanged="ddlMissedEvents_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                    </div>
                    <div class="row Padding5">
                        <div class="TableLabel col-sm-4">Role: </div>
                        <div class="col-sm-8 NoLeftPadding">
                            <asp:DropDownList ID="ddlRoles" runat="server" /><asp:Label ID="lblRole" runat="server" />
                        </div>
                    </div>
                    <div class="row Padding5" id="divPCStaff" runat="server">
                        <div class="col-sm-4 TableLabel">
                            Character:
                        </div>
                        <div class="col-sm-8 NoLeftPadding">
                            <asp:DropDownList ID="ddlCharacterList" runat="server" /><asp:Label ID="lblCharacter" runat="server" />
                        </div>
                    </div>

                    <div class="row Padding5" id="divNPC" runat="server">
                        <div class="col-sm-4 TableLabel">
                            Send CP to:
                        </div>
                        <div class="col-sm-8 NoLeftPadding">
                            <asp:DropDownList ID="ddlSendToCampaign" runat="server" />
                        </div>
                    </div>

                    <div class="row Padding5" id="divSendOther" runat="server">
                        <div class="col-sm-4">&nbsp;</div>
                        <div class="col-sm-8 NoLeftPadding">
                            <asp:TextBox ID="tbSendToCPOther" runat="server" CssClass="form-control col-lg-10" MaxLength="500" TextMode="MultiLine" />
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnCloseRegisterForEvent" runat="server" Text="Register For Event" Width="150px" CssClass="StandardButton" OnClick="btnCloseRegisterForEvent_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Registration
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblMessage" runat="server" /></p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnCloseMessage_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
