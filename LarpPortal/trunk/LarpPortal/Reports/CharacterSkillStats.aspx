<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reporting.master" AutoEventWireup="true" CodeBehind="CharacterSkillStats.aspx.cs" Inherits="LarpPortal.Reports.CharacterSkillStats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ReportsContent" runat="server">
    <aside></aside>
    <div role="form" class="form-horizontal form-condensed">
        <div class="col-sm-12">
            <h3 class="col-sm-12">Character Skill Statistics Reports</h3>
        </div>
        <asp:Panel ID="pnlParameters" runat="server" Visible="true">
            <div class="col-sm-12">
                <div class="row">
                    <label for="ddlEvent" runat="server" visible="true">Event</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlEvent" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="All events"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="row">
                        <asp:RadioButton ID="rdoSkillCount" runat="server" Visible="false" AutoPostBack="true" GroupName="grpPrintBy" Text=" Skill Count" OnCheckedChanged="grpPrintBy_CheckedChanged" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoSkillDetail" runat="server" Visible="false" AutoPostBack="true" GroupName="grpPrintBy" Text=" Skill Detail" OnCheckedChanged="grpPrintBy_CheckedChanged" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoSkillTypeCount" runat="server" Visible="false" AutoPostBack="true" GroupName="grpPrintBy" Text=" Type Count" OnCheckedChanged="grpPrintBy_CheckedChanged" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoSkillTypeDetail" runat="server" Visible="false" AutoPostBack="true" GroupName="grpPrintBy" Text=" Type Detail" OnCheckedChanged="grpPrintBy_CheckedChanged" />
                </div>
                <div class="row">
                    <div style="padding-right: 20px; padding-top: 2px">
                        <asp:Button ID="btnRunReport" runat="server" CssClass="StandardButton" Visible="false" Width="77px" Text="Run" OnClick="btnRunReport_Click" />
                    </div>
                </div>
                <div class="row">
                    <div style="padding-right: 20px; padding-top: 2px">
                        <asp:Button ID="btnExportExcel" runat="server" CssClass="StandardButton" Width="77px" Text="Excel" OnClick="btnExportExcel_Click" Visible="false" />
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div id="character-info" class="character-info tab-pane active">
            <section role="form">
                <asp:Panel ID="pnlReportOutputSkillCount" runat="server" Visible="false">
                    <%--Skill Count--%>
                    <div class="form-horizontal col-sm-12">
                        <div class="row">
                            <div id="Div1" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Character Skill Count </h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvSkillCount" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvSkillCount_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed">
                                                    <Columns>
                                                        <asp:BoundField DataField="ParentName" HeaderText="Parent Name" />
                                                        <asp:BoundField DataField="SkillName" HeaderText="Skill Name" />
                                                        <asp:BoundField DataField="HowMany" HeaderText="How Many?" />
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

                <asp:Panel ID="pnlReportOutputSkillDetail" runat="server" Visible="false">
                    <%--Skill Detail--%>
                    <div class="form-horizontal col-sm-12">
                        <div class="row">
                            <div id="Div2" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Character Skill Detail </h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvSkillDetail" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvSkillDetail_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed">
                                                    <Columns>
                                                        <asp:BoundField DataField="ParentName" HeaderText="Parent Name" />
                                                        <asp:BoundField DataField="SkillName" HeaderText="Skill Name" />
                                                        <asp:BoundField DataField="SkillType" HeaderText="Skill Type" />
                                                        <asp:BoundField DataField="Character" HeaderText="Character" />
                                                        <asp:BoundField DataField="Player" HeaderText="Player" />
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

                <asp:Panel ID="pnlReportOutputSkillTypeCount" runat="server" Visible="false">
                    <%--Skill Type Count--%>
                    <div class="form-horizontal col-sm-12">
                        <div class="row">
                            <div id="Div3" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Character Skill Type Count </h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvSkillTypeCount" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvSkillTypeCount_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed">
                                                    <Columns>
                                                        <asp:BoundField DataField="SkillType" HeaderText="Skill Type" />
                                                        <asp:BoundField DataField="HowMany" HeaderText="How Many?" />
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

                <asp:Panel ID="pnlReportOutputSkillTypeDetail" runat="server" Visible="false">
                    <%--Skill Type Detail--%>
                    <div class="form-horizontal col-sm-12">
                        <div class="row">
                            <div id="Div4" class="panel-wrapper" runat="server">
                                <div class="panel">
                                    <div class="panelheader">
                                        <h2>Character Skill Type Detail </h2>
                                        <div class="panel-body">
                                            <div class="panel-container" style="height: 500px; overflow: auto;">
                                                <asp:GridView ID="gvSkillTypeDetail" runat="server"
                                                    AutoGenerateColumns="false"
                                                    OnRowDataBound="gvSkillTypeDetail_RowDataBound"
                                                    GridLines="None"
                                                    HeaderStyle-Wrap="false"
                                                    CssClass="table table-striped table-hover table-condensed">
                                                    <Columns>
                                                        <asp:BoundField DataField="SkillType" HeaderText="Skill Type" />
                                                        <asp:BoundField DataField="SkillName" HeaderText="Skill Name" />
                                                        <asp:BoundField DataField="ParentSkill" HeaderText="Parent Skill" />
                                                        <asp:BoundField DataField="Character" HeaderText="Character" />
                                                        <asp:BoundField DataField="Player" HeaderText="Player" />
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
        </div>
    </div>
</asp:Content>
