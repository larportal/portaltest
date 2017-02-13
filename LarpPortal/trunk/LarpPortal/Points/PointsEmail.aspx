<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaigns.master" AutoEventWireup="true" CodeBehind="PointsEmail.aspx.cs" Inherits="LarpPortal.Points.PointsEmail" %>
<asp:Content ID="PointsStyles" runat="server" ContentPlaceHolderID="MemberStyles">
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
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
            <h3 class="col-sm-5">Send Character Points Via Email</h3>
        </div>
        <asp:Panel ID="pnlAssignHeader" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="col-sm-4">
                    <div class="col-sm-12 form-group">
                        <label for="ddlCampaign" class="col-sm-5 control-label">Send To Campaign</label>
                        <asp:DropDownList ID="ddlCampaign" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaign_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Select Campaign"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-7">
                </div>
                <div class="col-sm-1">
                    <div class="row">
                        <div style="padding-right: 20px;">
                            <%--<asp:Button ID="btnDeleteAll" runat="server" CssClass="StandardButton" Width="77px" Text="Delete All" OnClick="btnDeleteAll_Click" />--%>
                        </div>
                    </div>
                    <div class="row">
                        &nbsp;
                    </div>
                    <div class="row">
                        <div style="padding-right: 20px;">
                            <asp:UpdatePanel ID="upnlAssignAll" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnPreview2" runat="server" Visible="false" CssClass="StandardButton" Width="75px" Text="Preview2" OnClick="btnPreview2_Click" />
                                    <asp:Button ID="btnPreview" runat="server" Visible="false" CssClass="StandardButton" Width="75px" Text="Preview" OnClick="btnPreview_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
                <asp:Panel ID="pnlAwaitingSending" runat="server">
                    <div class="form-horizontal col-sm-12">
                        <div class="row">
                            <div id="Div1" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Points Awaiting Sending</h2>
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
                                                        <asp:TemplateField HeaderText="Player Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("PlayerName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Event">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("EventName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Description" ItemStyle-Wrap="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPlayerName" runat="server" Text='<%# Eval("Description") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="To Campaign" ItemStyle-Wrap="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEarnDescription" runat="server" Text='<%# Eval("CampaignName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Points" ItemStyle-Wrap="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCPValue" runat="server" Text='<%# Eval("CPValue") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCPValue" runat="server" Visible="true" Text='<%# Eval("CPValue") %>' BorderColor="Black" BorderStyle="Solid" BorderWidth="1" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Staff Comments">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStaffComments" runat="server" Text='<%# Eval("OpportunityStaffComments") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="tbStaffComments" runat="server" Visible="true" Text='<%# Eval("OpportunityStaffComments") %>' BorderColor="Black" BorderStyle="Solid" BorderWidth="1" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidReceiptDate" runat="server" Value='<%# Eval("ReceiptDate") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidReceivedBy" runat="server" Value='<%# Eval("ReceiversName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

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

                                                        <%--<asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidRole" runat="server" Value='<%# Eval("Role") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidNPCCampaignID" runat="server" Value='<%# Eval("NPCCampaignID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hidRegistrationID" runat="server" Value='<%# Eval("RegistrationID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" Width="75px" CssClass="StandardButton" />
                                                                <%--<asp:Button ID="btnAssign" runat="server" CommandName="Update" Text="Assign" Width="75px" CssClass="StandardButton" />--%>
                                                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" Width="75px" CssClass="StandardButton" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Button ID="btnupdate2" runat="server" CommandName="Update" Text="Save" Width="75px" CssClass="StandardButton" />
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

            <%--Email preview panel--%>
            <section role="form">
                <asp:Panel ID="pnlPreviewEmail" runat="server">  <%--Need to make this visible="false" by default--%>
                    <div class="form-horizontal col-lg-6">
                        <div class="row">
                            <div id="Div2" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader NoPadding">
                                        <h2>Preview Email</h2>
                                        <div class="panel-body NoPadding">
                                            <asp:HiddenField ID="hidTo" runat="server" />
                                            <asp:HiddenField ID="hidSubject" runat="server" />
                                            <asp:HiddenField ID="hidBcc" runat="server" />
                                            <asp:HiddenField ID="hidBody" runat="server" />
                                            <asp:HiddenField ID="hidBodyAdditionalText" runat="server" />
                                            <div class="panel-container">
                                                <div>
                                                    <asp:Label ID="lblTest" runat="server" Text="Initial Text"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </section>
        </div>
    </div>
</asp:Content>
