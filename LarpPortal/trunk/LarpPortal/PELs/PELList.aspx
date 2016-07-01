<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PELList.aspx.cs" Inherits="LarpPortal.PELs.PELList" MasterPageFile="~/MemberCampaigns.master" %>

<asp:Content ContentPlaceHolderID="MemberCampaignsContent" ID="PELList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <div class="col-sm-6">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="PEL (Post Event Letter)" />
            </div>
<%--            <div class="col-sm-6 text-right">
                <a href="#myModal" role="button" class="StandardButton col-sm-3" data-toggle="modal">Missing Event ?</a>
            </div>--%>
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

    <div id="myModal2" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 id="myModalLabel">We'd Love to Hear From You</h3>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Name</label><input class="form-control required" placeholder="Your name" data-placement="top" data-trigger="manual" data-content="Must be at least 3 characters long, and must only contain letters." type="text">
                    </div>
                    <div class="form-group">
                        <label>Message</label><textarea class="form-control" placeholder="Your message here.." data-placement="top" data-trigger="manual"></textarea>
                    </div>
                    <div class="form-group">
                        <label>E-Mail</label><input class="form-control email" placeholder="email@you.com (so that we can contact you)" data-placement="top" data-trigger="manual" data-content="Must be a valid e-mail address (user@gmail.com)" type="text">
                    </div>
                    <div class="form-group">
                        <label>Phone</label><input class="form-control phone" placeholder="999-999-9999" data-placement="top" data-trigger="manual" data-content="Must be a valid phone number (999-999-9999)" type="text">
                    </div>
                    <div class="form-group">
                        <button type="submit" class="btn btn-success pull-right">Send It!</button>
                        <p class="help-block pull-left text-danger hide" id="form-error">&nbsp; The form is not valid. </p>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
                </div>
            </div>
        </div>
    </div>

    <style type="text/css">
        .table tbody tr:hover td,
        .table tbody tr:hover th {
            background-color: transparent;
        }

        .table tbody tr:nth-child(even) td {
            background-color: transparent;
        }

        .table tbody td {
            background-color: transparent;
        }
    </style>

    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Enter Event Information
                </div>
                <div class="modal-body" style="background-color: white;">
                    <asp:Table ID="tblEventInfo" runat="server" CssClass="table">
                        <asp:TableRow ID="trEventList" runat="server" CssClass="PrePostPadding">
                            <asp:TableCell ID="tcEventLabel" runat="server" CssClass="TableLabel">
                                Event List:
                            </asp:TableCell>
                            <asp:TableCell ID="tcEventList" runat="server">
                                <asp:DropDownList ID="ddlListToSelect" runat="server" OnSelectedIndexChanged="ddlListToSelect_SelectedIndexChanged" AutoPostBack="true" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="trRole" runat="server" CssClass="PrePostPadding">
                            <asp:TableCell ID="tcRoleLabel" runat="server" CssClass="TableLabel">
                                Role:
                            </asp:TableCell>
                            <asp:TableCell ID="tcRoleList" runat="server">
                                <asp:DropDownList ID="ddlRoles" runat="server" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" /><asp:Label ID="lblRole" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="trPCStaff" runat="server" CssClass="PrePostPadding">
                            <asp:TableCell ID="tcCharLabel" runat="server" CssClass="TableLabel">
                                Character:
                            </asp:TableCell>
                            <asp:TableCell ID="tcCharList" runat="server">
                                <asp:DropDownList ID="ddlCharacterList" runat="server" /><asp:Label ID="lblCharacter" runat="server" />
                            </asp:TableCell>

                        </asp:TableRow>
                        <asp:TableRow ID="trNPC" runat="server" CssClass="PrePostPadding">
                            <asp:TableCell ID="tcSendCPToLabel" runat="server" CssClass="TableLabel">
                                    Send CP to 
                            </asp:TableCell>
                            <asp:TableCell ID="tcSendCPTo" runat="server">
                                <asp:DropDownList ID="ddlSendToCampaign" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="trSendCPOther" runat="server" CssClass="PrePostPadding">
                            <asp:TableCell ID="tcSendCPOtherLabel" runat="server">
                                &nbsp;
                            </asp:TableCell>
                            <asp:TableCell ID="tcSendCPOther" runat="server">
                                <asp:TextBox ID="tbSendToCPOther" runat="server" CssClass="col-lg-10" MaxLength="500" Style="display: none;" TextMode="MultiLine" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>



                    <%--                    <asp:MultiView ID="mvCharacters" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vwCharacter" runat="server">
                            <div class="row PrePostPadding">
                                <div>
                                    Character:
                                </div>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlCharacterList" runat="server" /><asp:Label ID="lblCharacter" runat="server" />
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="vwSendCPTo" runat="server">
                            <div class="row PrePostPadding" id="divSendCPTo" runat="server">
                                <div class="TableLabel col-lg-4">
                                    Send CP to 
                                </div>
                                <div class="col-lg-6">
                                    <div class="row PrePostPadding">
                                        <asp:DropDownList ID="ddlSendToCampaign" runat="server" />
                                    </div>
                                    <div class="row PrePostPadding">
                                        <asp:TextBox ID="tbSendToCPOther" runat="server" CssClass="col-lg-10" MaxLength="500" Style="display: none;" TextMode="MultiLine" />
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>--%>

                    <div class="modal-footer">
                        <asp:Button ID="btnClose2" runat="server" Text="Save Event Info" Width="150px" CssClass="StandardButton" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
