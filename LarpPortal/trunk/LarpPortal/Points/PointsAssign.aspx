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
                                        <asp:HiddenField ID="hidCampaignPlayerUserID" runat="server" />
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

            <%--Add new points panel--%>
            <section role="form">
                <asp:Panel ID="pnlAddNewCP" runat="server" Visible="false">
                    <div class="form-horizontal col-lg-6">
                        <div class="row">
                            <div id="Div2" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader NoPadding">
                                        <h2>Add New Points</h2>
                                        <div class="panel-body NoPadding">
                                            <asp:HiddenField ID="hidInsertCampaignPlayerID" runat="server" />
                                            <asp:HiddenField ID="hidInsertCharacterID" runat="server" />
                                            <asp:HiddenField ID="hidInsertCampaignCPOpportunityDefaultID" runat="server" />
                                            <asp:HiddenField ID="hidInsertCampaignCPOpportunityDefaultIDNPCEvent" runat="server" />
                                            <asp:HiddenField ID="hidInsertCampaignCPOpportunityDefaultIDNPCSetup" runat="server" />
                                            <asp:HiddenField ID="hidInsertCampaignCPOpportunityDefaultIDNPCPEL" runat="server" />
                                            <asp:HiddenField ID="hidInsertEventID" runat="server" />
                                            <asp:HiddenField ID="hidInsertCampaignID" runat="server" />
                                            <asp:HiddenField ID="hidInsertDescription" runat="server" />
                                            <asp:HiddenField ID="hidInsertDescriptionNPCEvent" runat="server" />
                                            <asp:HiddenField ID="hidInsertDescriptionNPCSetup" runat="server" />
                                            <asp:HiddenField ID="hidInsertDescriptionNPCPEL" runat="server" />
                                            <asp:HiddenField ID="hidInsertOpportunityNotes" runat="server" />
                                            <asp:HiddenField ID="hidInsertExampleURL" runat="server" />
                                            <asp:HiddenField ID="hidInsertReasonID" runat="server" />
                                            <asp:HiddenField ID="hidInsertReasonIDNPCEvent" runat="server" />
                                            <asp:HiddenField ID="hidInsertReasonIDNPCSetup" runat="server" />
                                            <asp:HiddenField ID="hidInsertReasonIDNPCPEL" runat="server" />
                                            <asp:HiddenField ID="hidInsertStatusID" runat="server" />
                                            <asp:HiddenField ID="hidInsertAddedByID" runat="server" />
                                            <asp:HiddenField ID="hidInsertCPValue" runat="server" />
                                            <asp:HiddenField ID="hidInsertApprovedByID" runat="server" />
                                            <asp:HiddenField ID="hidInsertReceiptDate" runat="server" />
                                            <asp:HiddenField ID="hidInsertReceivedByID" runat="server" />
                                            <asp:HiddenField ID="hidInsertCPAssignmentDate" runat="server" />
                                            <asp:HiddenField ID="hidInsertStaffComments" runat="server" />
                                            <asp:HiddenField ID="hidLastAddCPStep" runat="server" />
                                            <div class="panel-container">
                                                <div class="row PrePostPadding">
                                                    <div class="TableLabel col-sm-3">Player: <%--A--%></div>
                                                    <div class="col-sm-7 NoPadding">
                                                        <asp:DropDownList ID="ddlCampaignPlayer" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaignPlayer_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 NoPadding">
                                                        <asp:Button ID="btnSaveNewOpportunity" runat="server" CssClass="StandardButton" Width="77px" Text="Save" OnClick="btnSaveNewOpportunity_Click" />
                                                    </div>
                                                </div>
                                                <asp:Panel ID="pnlAddSourceCampaign" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Source Campaign: <%--B--%></div>
                                                        <div class="col-sm-5 NoPadding">
                                                            <asp:DropDownList ID="ddlAddSourceCampaign" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlAddSourceCampaign_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3 NoPadding">
                                                            <asp:Label ID="lblAddMessage" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlAddOpportunityDefault" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Earn Description: <%--C1--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlAddOpportunityDefaultID" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlAddOpportunityDefaultID_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlAddOpportunityDefaultC6" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Earn Description: <%--C6--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlAddOpportunityDefaultIDC6" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlAddOpportunityDefaultIDC6_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlCPDestinationD3" runat="server" Visible="false">
                                                    <%--D3
                                                        Choose event
                                                        Choose destination for CP--%>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Send Points To: <%--D3--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlDestinationCampaign" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlDestinationCampaign_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlCPDestinationD6" runat="server" Visible="false">
                                                    <%--D6
                                                        Choose destination for CP--%>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlNPCCheckboxes" runat="server" Visible="false">
                                                    <%--E3, E4 or E6--%>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Event: </div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlSourceEvent" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlSourceEvent_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">
                                                        </div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:CheckBox ID="chkNPCEvent" Text="NPC Event" runat="server" Checked="true" OnCheckedChanged="chkNPCEvent_CheckedChanged" />
                                                            <%--Conditional Value--%>
                                                            <asp:TextBox ID="txtNPCEvent" runat="server" Width="35px" Text="1"></asp:TextBox> Points
                                                            <br />
                                                            <asp:CheckBox ID="chkSetupCleanup" Text="Setup/Cleanup" runat="server" Checked="true" OnCheckedChanged="chkSetupCleanup_CheckedChanged"  />
                                                            <%--Conditional Value--%>
                                                            <asp:TextBox ID="txtSetupCleanup" runat="server" Width="35px" Text="0.5"></asp:TextBox> Points
                                                            <br />
                                                            <asp:CheckBox ID="chkPEL" Text="PEL" runat="server" Checked="true" OnCheckedChanged="chkPEL_CheckedChanged"  />
                                                            <%--Conditional Value--%>
                                                            <asp:TextBox ID="txtPEL" runat="server" Width="55px" Text="0.5"></asp:TextBox> Points
                                                            <br />
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlAddDonationCP" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Donation Type: <%--F0--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlDonationTypes" runat="server" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlDonationTypes_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Donation"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Art"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Costume"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Effects"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Hats"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Labor"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Mask"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Prop"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Shield"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Water"></asp:ListItem>
                                                                <asp:ListItem Text="Donation, Weapon"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Donation Notes: <%--F0--%></div>
                                                        <div class="col-sm-9 NoPadding">
                                                            <asp:TextBox ID="txtOpportunityNotes" runat="server" Width="95%" OnTextChanged="txtOpportunityNotes_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Receipt Date: <%--F0--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:TextBox ID="txtReceiptDate" runat="server" Width="100px"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtReceiptDate" Format="MM/dd/yyyy" />
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Points: <%--F0--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:TextBox ID="txtCPF0" runat="server" Width="55px" Text="0" OnTextChanged="txtCPF0_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Character: <%--F0--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlSelectCharacterOrBankF0" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCharacterOrBankF0_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlAddNonEventCP" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Points: <%--F1--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:TextBox ID="txtCPF1" runat="server" Width="55px" Text="0" OnTextChanged="txtCPF1_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Character: <%--F1--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlSelectCharacterOrBankF1" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCharacterOrBankF1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlAddPCLocalCP" runat="server" Visible="false">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Event: </div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlSourceEventPC" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlSourceEventPC_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Points: <%--F2--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:TextBox ID="txtCPF2" runat="server" Width="55px" Text="0" OnTextChanged="txtCPF2_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="TableLabel col-sm-3">Character: <%--F2--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlSelectCharacterOrBankF2" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCharacterOrBankF2_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                </asp:Panel> 
                                                <asp:Panel ID="pnlAddNPCLocalCPStaying" runat="server" Visible="false">
                                                    <div class="TableLabel col-sm-3">Character: <%--F3--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlSelectCharacterOrBankF3" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCharacterOrBankF3_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>                                                
                                                </asp:Panel>
                                                <asp:Panel ID="pnlAddNPCLocalCPGoingToLARPPortalCampaign" runat="server" Visible="false">
                                                    <%--F4--%>
                                                    <div class="TableLabel col-sm-3">Character: <%--F4--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlSelectCharacterOrBankF4" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCharacterOrBankF4_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlAddNPCLocalCPGoingToNonLARPPortalCampaign" runat="server" Visible="false">
                                                    <%--F5 Different from others. Set up email--%>

                                                </asp:Panel>
                                                <asp:Panel ID="pnlAddNPCIncoming" runat="server" Visible="false">
                                                    <%--F6--%>
                                                    <div class="TableLabel col-sm-3">Character: <%--F6--%></div>
                                                        <div class="col-sm-6 NoPadding">
                                                            <asp:DropDownList ID="ddlSelectCharacterOrBankF6" runat="server" CssClass="NoPadding" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCharacterOrBankF6_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                </asp:Panel>

                                                <br />
                                                <asp:Panel ID="pnlAddStaffComments" runat="server" Visible="true">
                                                    <div class="row PrePostPadding">
                                                        <div class="TableLabel col-sm-3">Staff Comments: </div>
                                                        <div class="col-sm-9 NoPadding">
                                                            <asp:TextBox ID="txtStaffComments" runat="server" Width="90%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlCharacterPointDisplay" runat="server" Visible="false">
                    <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
                        <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="" />
                    </div>
                    <div class="panel">
                        <div class="panel-header">
                            <h2>Total Points<asp:Label ID="lblGridHeader" runat="server"></asp:Label></h2>
                        </div>
                        <div class="panel-body search-criteria" style="padding-bottom: 5px;">
                            <div style="max-height: 500px; overflow-y: auto;">
                                <asp:GridView ID="gvPointsList" runat="server" AutoGenerateColumns="false" GridLines="None"
                                    CssClass="table table-striped table-hover table-condensed" BorderColor="Black" BorderStyle="Solid" BorderWidth="1">
                                    <RowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="TransactionDate" HeaderText=" Earn Date" DataFormatString="{0: MM/dd/yyyy}" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="ReasonDescription" HeaderText="Type" ItemStyle-Wrap="false"
                                            HeaderStyle-Wrap="false" ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="AdditionalNotes" HeaderText="Description" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="CPAmount" HeaderText="Points" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="StatusName" HeaderText="Status" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="CPApprovedDate" HeaderText="Spend Date" DataFormatString="{0: MM/dd/yyyy}" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="RecvFromCampaign" HeaderText="Earned At" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="OwningPlayer" HeaderText="Earned By" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="ReceivingCampaign" HeaderText="Spent At" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="Character" HeaderText="Spent On" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="ReceivingPlayer" HeaderText="Transfer To" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                        <asp:BoundField DataField="CPApprovedDate" HeaderText="Approved" ItemStyle-Wrap="true" HeaderStyle-Wrap="false"
                                            ItemStyle-CssClass="LeftRightPadding" HeaderStyle-CssClass="LeftRightPadding" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </section>
        </div>
    </div>
</asp:Content>
