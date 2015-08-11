<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="PointsSetupNonStandard.aspx.cs" Inherits="LarpPortal.Points.PointsSetupNonStandard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MemberCampaignsContent" runat="server">
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-5">Assign Character Build Value&#47;Points </h3>
        </div>
        <div class="col-sm-12">
            <div class="col-sm-1">
                <h4>Assign By:</h4>
            </div>
            <div class="col-sm-4">
                <div class="col-sm-12 form-group">
                    <label for="ddlAttenance" class="col-sm-5 control-label">Attendance</label>
                    <asp:DropDownList ID="ddlAttendance" runat="server">
                        <asp:ListItem Value="0" Text="Select Event/Date"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-sm-12 form-group">
                    <label for="ddlEarnType" class="col-sm-5 control-label">Earn Type</label>
                    <asp:DropDownList ID="ddlEarnType" runat="server">
                        <asp:ListItem Value="0" Text="Select Earn Type"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-sm-12 form-group">
                    <label for="ddlEarnReason" class="col-sm-5 control-label">Earn Reason</label>
                    <asp:DropDownList ID="ddlEarnReason" runat="server">
                        <asp:ListItem Value="0" Text="Select Earn Reason"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="col-sm-12 form-group">
                    <label for="ddlPlayer" class="col-sm-3 control-label">Player</label>
                    <asp:DropDownList ID="ddlPlayer" runat="server">
                        <asp:ListItem Value="0" Text="Select Player"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-12 form-group">
                    <label for="ddlCharacters" class="col-sm-3 control-label">Character</label>
                    <asp:DropDownList ID="ddlCharacters" runat="server">
                        <asp:ListItem Value="0" Text="Select Player"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
                <%-- class="form-horizontal form-condensed">--%>
                <div class="form-horizontal col-sm-12">
                    <div class="row">
                        <div id="Div1" class="panel-wrapper" runat="server">
                            <div class="panel">
                                <div class="panelheader">
                                    <h2>Points Awaiting Assignment</h2>
                                    <div class="panel-body">
                                        <div class="panel-container" style="height: 500px; overflow: auto;">
                                            <%--OnRowEditing="gvPoints_RowEditing"--%><%--OnRowUpdating="gvPoints_RowUpdating"--%>
                                            <asp:GridView ID="gvPoints" runat="server"
                                                AutoGenerateColumns="false" 
                                                OnRowCancelingEdit="gvPoints_RowCancelingEdit"
                                                OnRowDataBound="gvPoints_RowDataBound" 
                                                GridLines="None"
                                                HeaderStyle-Wrap="false" 
                                                CssClass="table table-striped table-hover table-condensed">
                                                <Columns>

                                                   <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidPointID" runat="server" Value='<%# Eval("CampaignCPOpportunityID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Event">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("EventName") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblEvent" runat="server" Text='<%# Eval("EventID") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Player Name" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlayerName" runat="server" Text='<%# Eval("PlayerName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Character Name" ItemStyle-HorizontalAlign="center" ItemStyle-Wrap="false" ItemStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("PlayerName") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblCharacterName" runat="server" Text='<%# Eval("PlayerName") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Earn Description" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEarnDescription" runat="server" Text='<%# Eval("Description") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Value" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValue" runat="server" Text='<%# Eval("CPValue") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Assign CP">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAssignCPCurrent" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkAssignCPAction" runat="server" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reason Unassigned">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStaffComments" runat="server" Text='<%# Eval("StaffComments") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="tbStaffComments" runat="server" Text='<%# Eval("StaffComments") %>' BorderColor="Black" BorderStyle="Solid" BorderWidth="1" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" Width="100px" CssClass="StandardButton" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <%--                                                            <asp:Button ID="btnupdate2" runat="server" CommandName="Update" Text="Update" Width="100px" CssClass="StandardButton" />
                                                            <asp:Button ID="btncancel2" runat="server" CommandName="Cancel" Text="Cancel" Width="100px" CssClass="StandardButton" />--%>
                                                            <asp:Button ID="btnupdate2" runat="server" CommandName="Update" Text="Update" Width="100px" CssClass="StandardButton" />
                                                            <asp:Button ID="btncancel2" runat="server" CommandName="Cancel" Text="Cancel" Width="100px" CssClass="StandardButton" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <%--                                        <div class="panel-container">
                                            <div class="row">
                                                <div align="right" style="padding-right: 20px;">
                                                    <asp:Button ID="btnSaveCharacter" runat="server" CssClass="StandardButton" Width="150" Text="Save Changes" OnClick="btnSaveCharacter_Click" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                &nbsp;
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>


        <%--		<div class="col-sm-12">
			<div class="panel-wrapper">
				<div class="panel">
					<div class="panel-header">
						<h2>Assign entire list or deselect to exclude one or more entries.</h2>
					</div>
					<div class="panel-body">
						<div class="panel-container scroll-150">
							<table class="table table-striped table-bordered table-condensed table-hover">
								<thead>
									<tr>
										<th>Date</th>
										<th>Player Name</th>
										<th>Character Name</th>
										<th>Earn description</th>
										<th>Value</th>
										<th>Assign CP</th>
										<th>reason unassigned</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>12/12/2012</td>
										<td>Rick Pierce</td>
										<td>Zephyr</td>
										<td>PC Full Event</td>
										<td>1</td>
										<td><input type="checkbox" value="" checked></td>
										<td></td>
									</tr>
									<tr>
										<td>11/11/1111</td>
										<td>Sam Flam</td>
										<td>Zephyr</td>
										<td>PC Full Event</td>
										<td>.5</td>
										<td><input type="checkbox" value=""></td>
										<td>He's an idiot dunce</td>
									</tr>
									<tr>
										<td></td>
										<td></td>
										<td></td>
										<td></td>
										<td></td>
										<td><input type="checkbox" value="" checked></td>
										<td></td>
									</tr>
									<tr>
										<td></td>
										<td></td>
										<td></td>
										<td></td>
										<td></td>
										<td><input type="checkbox" value="" checked></td>
										<td></td>
									</tr>
									<tr>
										<td></td>
										<td></td>
										<td></td>
										<td></td>
										<td></td>
										<td><input type="checkbox" value="" checked></td>
										<td></td>
									</tr>
								</tbody>
							</table>
						</div><!-- .container -->
					</div><!-- body -->
				</div><!-- panel -->
			</div><!-- wrapper -->
		</div>

		<div class="col-sm-12 panel-wrapper">
			<div class="col-sm-4">
				<div class="col-sm-12 form-group">
					<label for="cp-setup-assign-event-attend-2" class="col-sm-5 control-label">Attendance</label>
					<div class="col-sm-7">
						<select id="cp-setup-assign-event-attend-2" class="col-sm-12">
							<option value="">Select Character</option>
							<option value="Skill Type 1">Event Date 1</option>
							<option value="Skill Type 2">Event Date 2</option>
							<option value="Skill Type 3">Event Date 3</option>
							<option value="Skill Type 4">Event Date 4</option>
						</select>
					</div>
				</div>
				<div class="col-sm-12 form-group">
					<label for="cp-setup-assign-player-2" class="col-sm-5 control-label">Player</label>
					<div class="col-sm-7">
						<select id="cp-setup-assign-player-2" class="col-sm-12">
							<option value="">Select Player</option>
							<option value="Skill Type 1">Player Name 1</option>
							<option value="Skill Type 2">Player Name 2</option>
							<option value="Skill Type 3">Player Name 3</option>
							<option value="Skill Type 4">Player Name 4</option>
						</select>
					</div>
				</div>
				<div class="col-sm-12 form-group">
					<label for="cp-setup-assign-character-2" class="col-sm-5 control-label">Character</label>
					<div class="col-sm-7">
						<select id="cp-setup-assign-character-2" class="col-sm-12">
							<option value="">Select Character</option>
							<option value="Skill Type 1">Character Name 1</option>
							<option value="Skill Type 2">Character Name 2</option>
							<option value="Skill Type 3">Character Name 3</option>
							<option value="Skill Type 4">Character Name 4</option>
						</select>
					</div>
				</div>
			</div>
			<div class="col-sm-4">
				<div class="col-sm-12 form-group">
					<label for="cp-setup-assign-earn-description-2" class="col-sm-5 control-label">Earn Desc</label>
					<div class="col-sm-7">
						<select id="cp-setup-assign-earn-description-2" class="col-sm-12">
							<option value="">Select Earn Description</option>
							<option value="Skill Type 1">Earn Description 1</option>
							<option value="Skill Type 2">Earn Description 2</option>
							<option value="Skill Type 3">Earn Description 3</option>
							<option value="Skill Type 4">Earn Description 4</option>
						</select>
					</div>
				</div>
				<div class="col-sm-12 form-group">
					<label for="cp-setup-modify-assign-value" class="col-sm-5 control-label">Value</label>
					<div class="col-sm-7">
						<input type="text" class="form-control" id="cp-setup-modify-assign-value" placeholder="Enter value&#47;points">
					</div>
				</div>
				<div class="col-sm-12 form-group">
					<label for="cp-setup-modify-assign-status" class="col-sm-5 control-label">Status</label>
					<div class="col-sm-7">
						<input type="text" class="form-control" id="cp-setup-modify-assign-status" placeholder="Assign Status">
					</div>
				</div>
			</div>
			<div class="col-sm-4">
				<div class="col-sm-12 form-group">
					<label for="cp-setup-modify-assign-comments" class="col-sm-5 control-label">Comments</label>
					<div class="col-sm-7">
						<input type="text" class="form-control" id="cp-setup-modify-assign-comments" placeholder="Comments if not approved">
					</div>
				</div>
			</div>
			<div class="col-sm-1 col-sm-offset-11">
				<button type="submit" class="btn btn-default">Assign</button>
			</div>
		</div>--%>
    </div>
</asp:Content>
