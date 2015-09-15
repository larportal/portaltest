<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="PointsAssign.aspx.cs" Inherits="LarpPortal.Points.PointsAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MemberCampaignsContent" runat="server">
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-5">Assign Character Build Value&#47;Points </h3>
        </div>
        <div class="col-sm-12">
            <div class="col-sm-1">
                <h4>Assign By:</h4>
            </div>
            <div class="col-sm-6">
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
            <div class="col-sm-4">
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
                    <div align="right" style="padding-right: 20px;">
                        <asp:Button ID="btnAssignAll" runat="server" CssClass="StandardButton" Width="75px" Text="Assign All" OnClick="btnAssignAll_Click" />
                    </div>
                </div>
                <div class="row">
                    &nbsp;
                </div>
            </div>

        </div>

        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
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
                                                            <asp:Label ID="lblStaffComments" runat="server" Text='<%# Eval("StaffComments") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbStaffComments" runat="server" Text='<%# Eval("StaffComments") %>' BorderColor="Black" BorderStyle="Solid" BorderWidth="1" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:HiddenFIeld ID="hidReceiptDate" runat="server" Value='<%# Eval("ReceiptDate") %>' />
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
            </section>
            <section role="form">
                <div class="form-horizontal col-sm-12">
                    <div class="row">
                        <div id="Div2" class="panel-wrapper" runat="server">
                            <div class="panel">
                                <div class="panelheader">
                                    <h2>Add Points Here</h2>
                                    <div class="panel-body">
                                        <div class="panel-container" style="height: 500px; overflow: auto;">
                                            <asp:Label ID="lblPoints" runat="server" Text="Player: "></asp:Label>
                                            <asp:DropDownList ID="ddlCampaignPlayer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaignPlayer_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
