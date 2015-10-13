<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="PointsAssign.aspx.cs" Inherits="LarpPortal.Points.PointsAssign" %>

<asp:Content ID="PointsStyles" runat="server" ContentPlaceHolderID="MemberStyles">
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
    </style>

    <link rel="stylesheet" href="http://localhost:49282/code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MemberCampaignsContent" runat="server">
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-5">Assign Character Points </h3>
        </div>
        <asp:Panel ID="pnlAssignHeader" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="col-sm-5">
                    <div class="col-sm-12 form-group">
                        <label for="ddlAttenance" class="col-sm-5 control-label">Attendance</label>
                        <asp:DropDownList ID="ddlAttendance" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAttendance_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Select Event/Date"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 form-group">
                        <label for="ddlEarnType" class="col-sm-5 control-label">Earn Type</label>
                        <asp:DropDownList ID="ddlEarnType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEarnType_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Select Earn Type"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-sm-12 form-group">
                        <label for="ddlEarnReason" class="col-sm-5 control-label">Earn Reason</label>
                        <asp:DropDownList ID="ddlEarnReason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEarnReason_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Select Earn Reason"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="col-sm-12 form-group">
                        <label for="ddlPlayer" class="col-sm-3 control-label">Player</label>
                        <asp:DropDownList ID="ddlPlayer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPlayer_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Select Player"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 form-group">
                        <label for="ddlCharacters" class="col-sm-3 control-label">Character</label>
                        <asp:DropDownList ID="ddlCharacters" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacters_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Select Player"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-1">
                    <div class="row">
                        <div style="padding-right: 20px;">
                            <asp:Button ID="btnAddNewOpportunity" runat="server" CssClass="StandardButton" Width="77px" Text="Add View" ToolTip="Switch to add view to add new points" OnClick="btnAddNewOpportunity_Click" />
                        </div>
                    </div>
                    <div class="row">
                        &nbsp;
                    </div>
                    <div class="row">
                        <div style="padding-right: 20px;">
                            <asp:Button ID="btnAssignAll" runat="server" CssClass="StandardButton" Width="75px" Text="Assign All" OnClick="btnAssignAll_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAddHeader" runat="server" Visible="false">
            <div class="col-sm-12">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-6">
                </div>
                <div class="col-sm-1">
                    <div class="row">
                        <div style="padding-right: 20px;">
                            <asp:Button ID="btnAssignExisting" runat="server" CssClass="StandardButton" Width="77px" Text="Assign View" ToolTip="Switch to assign view to assign existing point opportunities" OnClick="btnAssignExisting_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
                <asp:Panel ID="pnlAssignExisting" runat="server">
                    <div class="form-horizontal col-sm-12">
                        <div class="row">
                            <div id="Div1" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Points Awaiting Assignment</h2>
                                        <asp:HiddenField ID="hidUserName" runat="server" />
                                        <asp:HiddenField ID="hidCampaignID" runat="server" />
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvPoints" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowCancelingEdit="gvPoints_RowCancelingEdit"
                                                    OnRowEditing="gvPoints_RowEditing"
                                                    OnRowUpdating="gvPoints_RowUpdating"
                                                    OnRowUpdated="gvPoints_RowUpdated"
                                                    OnRowDeleting="gvPoints_RowDeleting"
                                                    OnRowDataBound="gvPoints_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("EventDate") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Event">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("EventName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Player Name" ItemStyle-Wrap="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPlayerName" runat="server" Text='<%# Eval("PlayerName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Character Name" ItemStyle-HorizontalAlign="center" ItemStyle-Wrap="false" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("CharacterAKA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Earn Description" ItemStyle-Wrap="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEarnDescription" runat="server" Text='<%# Eval("Description") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Value" ItemStyle-Wrap="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCPValue" runat="server" Text='<%# Eval("CPValue") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCPValue" runat="server" Text='<%# Eval("CPValue") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Staff Comments">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStaffComments" runat="server" Visible="false" Text='<%# Eval("StaffComments") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="tbStaffComments" runat="server" Visible="true" Text='<%# Eval("StaffComments") %>' BorderColor="Black" BorderStyle="Solid" BorderWidth="1" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidReceiptDate" runat="server" Value='<%# Eval("ReceiptDate") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidReceivedBy" runat="server" Value='<%# Eval("ReceiversName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidPointID" runat="server" Value='<%# Eval("CampaignCPOpportunityID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidCampaignPlayer" runat="server" Value='<%# Eval("CampaignPlayerID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidCharacterID" runat="server" Value='<%# Eval("CharacterID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidEventID" runat="server" Value='<%# Eval("EventID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidOpportunityNotes" runat="server" Value='<%# Eval("OpportunityNotes") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidExampleURL" runat="server" Value='<%# Eval("ExampleURL") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidCPOpportunityDefaultID" runat="server" Value='<%# Eval("CampaignCPOpportunityDefaultID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidReasonID" runat="server" Value='<%# Eval("ReasonID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidAddedByID" runat="server" Value='<%# Eval("AddedByID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidCPAssignmentDate" runat="server" Value='<%# Eval("CPAssignmentDate") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" Width="75px" CssClass="StandardButton" />
                                                                <asp:Button ID="btnAssign" runat="server" CommandName="Update" Text="Assign" Width="75px" CssClass="StandardButton" />
                                                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" Width="75px" CssClass="StandardButton" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Button ID="btnupdate2" runat="server" CommandName="Update" Text="Assign" Width="75px" CssClass="StandardButton" />
                                                                <asp:Button ID="btncancel2" runat="server" CommandName="Cancel" Text="Cancel" Width="75px" CssClass="StandardButton" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </section>
            <section role="form">
                <asp:Panel ID="pnlAddNewCP" runat="server" Visible="false">
                    <div class="form-horizontal col-lg-6">
                        <div class="row">
                            <div id="Div2" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader NoPadding">
                                        <h2>Add New Points</h2>
                                        <div class="panel-body NoPadding">
                                            <div class="panel-container">
                                                <div class="row PrePostPadding">
                                                    <div class="TableLabel col-sm-3">Player: </div>
                                                    <div class="col-sm-7 NoPadding">
                                                        <asp:DropDownList ID="ddlCampaignPlayer" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaignPlayer_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 NoPadding">
                                                        <asp:Button ID="btnSaveNewOpportunity" runat="server" CssClass="StandardButton" Width="77px" Text="Save" OnClick="btnSaveNewOpportunity_Click" />
                                                    </div>
                                                </div>
                                                <div class="row PrePostPadding">
                                                    <div class="TableLabel col-sm-3">Point Type:</div>
                                                    <div class="col-sm-3 NoPadding">
                                                        <asp:DropDownList ID="ddlAddOpportunityType" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlAddOpportunityType_SelectedIndexChanged">
                                                            <asp:ListItem Text="Event Points" Value="E" Selected="False"></asp:ListItem>
                                                            <asp:ListItem Text="Non-Event Points" Value="N" Selected="True"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <asp:Panel ID="pnlddlAddEvent" runat="server" Visible="false">
                                                        <div>
                                                            <div class="TableLabel col-sm-1">Event: </div>
                                                            <asp:DropDownList ID="ddlAddEvent" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlAddEvent_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <asp:Panel ID="pnlddlPCorNPC" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">PC / NPC: </div>
                                                        <asp:DropDownList ID="ddlPCorNPC" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlPCorNPC_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Text="PC" Value="PC"></asp:ListItem>
                                                            <asp:ListItem Selected="False" Text="NPC/Staff" Value="NPC"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </asp:Panel>
                                                <div class="row PrePostPadding">
                                                    <div class="TableLabel col-sm-3">Earn Description: </div>
                                                    <div class="col-sm-6 NoPadding">
                                                        <asp:DropDownList ID="ddlAddOpportunityDefaultID" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlAddOpportunityDefaultID_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="pnlddlSendPoints" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Send points to:</div>
                                                        <asp:Label ID="lblPlaceholder" runat="server" Text="We'll do some cool stuff here"></asp:Label>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlddlAddCharacter" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Character: </div>
                                                        <div class="col-sm-4 NoPadding">
                                                            <asp:DropDownList ID="ddlAddCharacter" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlAddCharacter_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlFinalChoices" runat="server" Visible="true">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">CPValue: x</div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">StaffComments:</div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlHiddenStuff" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">CampaignID:</div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Description of Earning: </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Opportunity Notes:</div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">ReasonID:</div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">StatusID:</div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">AddedByID:</div>
                                                    </div>

                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">ApprovedByID</div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">ReceiptDate:</div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">ReceivedByID:</div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">CPAssignmentDate:</div>
                                                    </div>

                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">ReceivedFromCampaignID:</div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--                            <div class="col-sm-1">
                                <asp:Button ID="btnSaveNewOpportunity" runat="server" CssClass="StandardButton" Width="77px" Text="Save" OnClick="btnSaveNewOpportunity_Click" />
                            </div>--%>
                    </div>
                </asp:Panel>
            </section>
        </div>
    </div>
</asp:Content>
