<%@ Page Title="" Language="C#" MasterPageFile="~/MemberMessages.master" AutoEventWireup="true" CodeBehind="AllMessages.aspx.cs" Inherits="LarpPortal.AllMessages" %>

<asp:Content ID="AllMessages" ContentPlaceHolderID="MemberMessageContent" runat="server">
All messages - Placeholder page - in progress
<div class="mainContent tab-content">
    <section id="total" class="tab-pane active">
        <form role="form" class="form-horizontal form-condensed">
            <div class="row">
                <h1 class="col-sm-3">My Messages</h1>
                <div class="panel-wrapper col-sm-1">
                    <a href="#">Edit</a>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-header">
                            <h2>All Messages</h2>
                        </div>
                        <div class="panel-body">
                            <div class="panel-container-table">
                                <asp:Table ID="eventstable3" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed" data-toggle="table" data-height="250" data-sort-name="id" data-sort-order="desc" data-click-to-select="true" data-select-item-name="message-selector">
                                    <asp:TableHeaderRow ID="eventstable3th" runat="server">                                    
										<asp:TableHeaderCell ID="state" runat="server" data-radio="true"></asp:TableHeaderCell>
										<asp:TableHeaderCell ID="id" runat="server" data-sortable="true">ID</asp:TableHeaderCell>
										<asp:TableHeaderCell ID="date" runat="server" data-sortable="true">Date</asp:TableHeaderCell>
										<asp:TableHeaderCell ID="campaign" runat="server" data-sortable="true">Campaign</asp:TableHeaderCell>
										<asp:TableHeaderCell ID="notificationreason" runat="server" data-sortable="true">Notification Reason</asp:TableHeaderCell>
										<asp:TableHeaderCell ID="sender" runat="server" data-sortable="true">Sender</asp:TableHeaderCell>
										<asp:TableHeaderCell ID="rsvp" runat="server" data-sortable="true">RSVP?</asp:TableHeaderCell>
										<asp:TableHeaderCell ID="dateread" runat="server" data-sortable="true">Date Read</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>

                                    <tbody>
                                        <tr>
                                            <td></td>
                                            <td>11222</td>
                                            <td>1/15/2014</td>
                                            <td>Madrigal</td>
                                            <td>Event Teaser</td>
                                            <td>Campaign GM</td>
                                            <td>no</td>
                                            <td>1/16/2014</td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>11300</td>
                                            <td>1/24/2014</td>
                                            <td>Mirror Mirror</td>
                                            <td>Housing Notification</td>
                                            <td>Logistics Staff</td>
                                            <td>no</td>
                                            <td>1/24/2014</td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>15454</td>
                                            <td>5/29/2014</td>
                                            <td>7 Virtues</td>
                                            <td>Staff Notice 1 Day Event 8/2014</td>
                                            <td>Campaign Owner</td>
                                            <td>Yes</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row panel-wrapper">
                <div class="col-sm-3">
                    <div class="form-group">
                        <div class="input-group col-sm-12">
                            <label for="messages-total-id" class="control-label col-sm-4">ID</label>
                            <div class="col-sm-8">
                                <input type="text" name="messages-total-id" id="messages-total-id" value="" class="form-control">
                            </div>
                        </div>
                        <div class="input-group col-sm-12">
                            <label for="messages-total-date" class="control-label col-sm-4">Date</label>
                            <div class="col-sm-8">
                                <input type="text" name="messages-total-date" id="messages-total-date" value="" class="form-control">
                            </div>
                        </div>
                        <div class="input-group col-sm-12">
                            <label for="messages-total-campaign" class="control-label col-sm-4">Campaign</label>
                            <div class="col-sm-8">
                                <input type="text" name="messages-total-campaign" id="messages-total-campaign" value="" class="form-control">
                            </div>
                        </div>
                        <div class="input-group col-sm-12">
                            <label for="messages-total-sender" class="control-label col-sm-4">Sender</label>
                            <div class="col-sm-8">
                                <input type="text" name="messages-total-sender" id="messages-total-sender" value="" class="form-control">
                            </div>
                        </div>
                        <div class="input-group col-sm-12">
                            <label for="messages-total-rsvp" class="control-label col-sm-4">RSVP</label>
                            <div class="col-sm-8">
                                <input type="text" name="messages-total-rsvp" id="messages-total-rsvp" value="" class="form-control">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-8">
                    <div class="col-sm-11">
                        <div class="form-group">
                            <label for="messages-total-notification-reason" class="control-label col-sm-3">Notification Reason</label>
                            <div class="col-sm-5">
                                <input type="text" name="messages-total-notification-reason" id="messages-total-notification-reason" value="" class="form-control">
                            </div>
                            <label for="messages-rsvp-date-read" class="control-label col-sm-2">Date Read</label>
                            <div class="col-sm-2">
                                <input type="text" name="messages-total-date-read" id="messages-total-date-read" value="" class="form-control">
                            </div>
                        </div>
                        <div class="panel-wrapper">
                            <textarea name="messages-total-notification-content" class="form-control" rows="2">Notification Content</textarea>
                        </div>
                    </div>
                    <div class="panel-wrapper col-sm-11">
                        <textarea name="messages-total-notification-content" class="form-control" rows="2">RSVP Content</textarea>
                    </div>
                </div>
            </div>
        </form>
    </section>
</div>
</asp:Content>
