<%@ Page Title="" Language="C#" MasterPageFile="~/MemberPoints.master" AutoEventWireup="true" CodeBehind="MemberPointsView.aspx.cs" Inherits="LarpPortal.MemberPointsView" %>
<asp:Content ID="MemberPoints" ContentPlaceHolderID="PointsContent" runat="server">
			<div class="mainContent tab-content">

				<section id="view-points" class="character-build-points tab-pane active">
					<div role="form" class="form-horizontal form-inline">
					<div class="row">
						<h1 class="col-sm-12">View Character Build Value / Points</h1>
					</div>
					<div class="row">
						<div class="panel-wrapper col-sm-12">
							<div class="panel">
								<div class="panel-header">
									<h2>Total Points <asp:Label ID="lblGridHeader" runat="server"></asp:Label></h2>
								</div>
								<div class="panel-body">
									<div class="panel-container-table">
                                        <asp:Label ID="lblCPAuditTableCode" runat="server" >Build the table here programatically.  If you see this code the build failed.</asp:Label>
<%--                                        <asp:GridView ID="gvCP" runat="server" Width="100%" AutoGenerateColumns="false" 
                                            OnPageIndexChanged="gvCP_SelectedIndexChanged" OnRowDataBound="gvCP_RowDataBound" OnSorting="gvCP_Sorting">
                                            <Columns>--%>
<%--                                                <asp:BoundField DataField="PlayerCPAuditID" HeaderText="CPID" ReadOnly="true" SortExpression="PlayerCPAuditID" />
                                                        <asp:TemplateField HeaderText="TransactionDate" SortExpression="TransactionDate"> 
                                                            <EditItemTemplate> 
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox> 
                                                            </EditItemTemplate> 
                                                            <ItemTemplate> 
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("LastName") %>'></asp:Label> 
                                                            </ItemTemplate> 
                                                         </asp:TemplateField> 

                                                <asp:BoundField DataField="TransactionDate" HeaderText="Earn Date" ReadOnly="true" SortExpression="TransactionDate" />
                                                <asp:BoundField DataField="ReasonDescription" HeaderText="Type" ReadOnly="true" SortExpression="ReasonDescription" />
                                                <asp:BoundField DataField="AdditionalNotes" HeaderText="Description" ReadOnly="true" SortExpression="AdditionalNotes" />
                                                <asp:BoundField DataField="CPAmount" HeaderText="Point Value" ReadOnly="true" SortExpression="CPAmount" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="true" SortExpression="Status" />
                                                <asp:BoundField DataField="SpendDate" HeaderText="Spend Date" ReadOnly="true" SortExpression="Spend Date" />
                                                <asp:BoundField DataField="RecvFromCampaign" HeaderText="Earned At" ReadOnly="true" SortExpression="RecvFromCampaign" />
                                                <asp:BoundField DataField="OwningPlayer" HeaderText="Earned By" ReadOnly="true" SortExpression="OwningPlayer" />
                                                <asp:BoundField DataField="ReceivingCampaign" HeaderText="Spend At" ReadOnly="true" SortExpression="ReceivingCampaign" />
                                                <asp:BoundField DataField="Character" HeaderText="Spent On" ReadOnly="true" SortExpression="Character" />
                                                <asp:BoundField DataField="ReceivingPlayer" HeaderText="Transfer To" ReadOnly="true" SortExpression="ReceivingPlayer" />
                                                <asp:BoundField DataField="CPApprovedDate" HeaderText="Approved" ReadOnly="true" SortExpression="CPApprovedDate" />--%>                                               
<%--                                            </Columns>
                                        </asp:GridView>--%>
									</div>
								</div><!-- .panel-body -->
							</div><!-- .panel -->
						</div><!-- .panel-wrapper -->
					</div><!-- .row -->
					<div class="row panel-wrapper">
                            <%--Column 1--%>
						<div class="col-sm-12">
							<div class="input-group col-sm-12">
								<label for="date-earned" class="control-label col-sm-12"><%--Earn Date--%>.</label>
								<div class="col-sm-8">
									<%--<input type="text" name="date-earned" id="date-earned" placeholder="Earn Date" value="" class="form-control">--%>
								</div>
							</div>
							<%--<div class="input-group col-sm-12">
								<label for="points-earn-type" class="control-label col-sm-4">Earn Type</label>
								<div class="col-sm-8">
									<input type="text" name="points-earn-type" id="points-earn-type" value="" placeholder="Earn Type" class="form-control">
								</div>
							</div>
							<div class="input-group col-sm-12">
								<label for="points-earn-description" class="control-label col-sm-4">Description</label>
								<div class="col-sm-8">
									<input type="text" name="points-earn-description" id="points-earn-description" value="" placeholder="Earn Description" class="form-control">
								</div>
							</div>
							<div class="input-group col-sm-12">
								<label for="source-value" class="control-label col-sm-4">Source Value</label>
								<div class="col-sm-8">
									<input type="text" name="source-value" id="source-value" value="" placeholder="Source Value" class="form-control">
								</div>
							</div>
							<div class="input-group col-sm-12">
								<label for="points-points" class="control-label col-sm-4">Points</label>
								<div class="col-sm-8">
									<input type="text" name="points-points" id="points-points" placeholder="Points" value="" class="form-control">
								</div>
							</div>
							<div class="input-group col-sm-12">
								<label for="points-status" class="control-label col-sm-4">Status</label>
								<div class="col-sm-8">
									<input type="text" name="points-status" id="points-status" value="" placeholder="Status" class="form-control">
								</div>
							</div>--%>
						</div>
                            <%--Column 2--%>
						<div class="col-sm-12">
							<div class="input-group col-sm-12">
								<label for="date-spent" class="control-label col-sm-12"><%--Spend Date--%>.</label>
								<div class="col-sm-8">
									<%--<input type="text" name="date-spent" id="date-spent" placeholder="Spend Date" value="" class="form-control">--%>
								</div>
							</div>
							<%--<div class="input-group col-sm-12">
								<label for="points-earn-at" class="control-label col-sm-4">Earned at</label>
								<div class="col-sm-8">
									<input type="text" name="points-earn-at" id="points-earn-at" value="" placeholder="Earned at Campaign" class="form-control">
								</div>
							</div>
							<div class="input-group col-sm-12">
								<label for="points-earn-by-character" class="control-label col-sm-4">Earned by</label>
								<div class="col-sm-8">
									<input type="text" name="points-earn-by-character" id="points-earn-by-character" value="" placeholder="Earn Description" class="form-control">
								</div>
							</div>
							<div class="input-group col-sm-12">
								<label for="points-spect-at-campaign" class="control-label col-sm-4">Spent at</label>
								<div class="col-sm-8">
									<input type="text" name="points-spect-at-campaign" id="points-spect-at-campaign" value="" placeholder="Spent at Campaign" class="form-control">
								</div>
							</div>
							<div class="input-group col-sm-12">
								<label for="points-spent-on" class="control-label col-sm-4">Spent on</label>
								<div class="col-sm-8">
									<input type="text" name="points-spent-on" id="points-spent-on" placeholder="Spent on Character" value="" class="form-control">
								</div>
							</div>
							<div class="input-group col-sm-12">
								<label for="points-transfer-to" class="control-label col-sm-4">Transfer to</label>
								<div class="col-sm-8">
									<input type="text" name="points-transfer-to" id="points-transfer-to" value="" placeholder="Transfer to Player" class="form-control">
								</div>
							</div>--%>
						</div>
                            <%--Column 3--%>
						<div class="col-sm-12">
							<div class="input-group col-sm-12">
								<label for="points-date-approved" class="control-label col-sm-12"><%--Approved Date--%>.</label>
								<div class="col-sm-8">
									<%--<input type="text" name="points-date-approved" id="points-date-approved" value="" placeholder="Approved Date" class="form-control">--%>
								</div>
							</div>
<%--							<div class="input-group col-sm-12">
								<label for="points-reason-unapproved" class="control-label col-sm-4">Reason Unapproved</label>
								<div class="col-sm-8">
									<input type="text" name="points-reason-unapproved" id="points-reason-unapproved" value="" placeholder="Reason if not approved" class="form-control">
								</div>
							</div>--%>
						</div>
					</div>
					<div class="edit-save col-sm-1 col-sm-offset-11">
						<%--<button type="submit" class="btn btn-default">Save</button>--%>
					</div>
					</div>
				</section>
            </div>
</asp:Content>
