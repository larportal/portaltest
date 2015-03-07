<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharSkills.aspx.cs" Inherits="LarpPortal.Character.CharSkills" %>

<asp:Content ID="Styles" runat="server" ContentPlaceHolderID="CharHeaderStyles">
    <style type="text/css">
        .NoRightPadding
        {
            padding-right: 0px;
        }

        .NoLeftPadding
        {
            padding-left: 0px;
        }

        /*div
        {
            border: 1px solid black;
        }*/

        table.myformat th
        {
            text-align: left;
            border-collapse: initial;
        }

        table.myformat td
        {
            text-align: left;
            border-collapse: initial;
        }

        input[type="checkbox"]
        {
            display: inline-block;
            text-align: left;
        }

        table input
        {
            width: auto;
        }

        table th
        {
            width: auto;
        }

        table td
        {
            width: auto;
            background-color: inherit;
        }

        div
        {
            width: auto;
        }

        .treeNode
        {
            color: blue;
            font: 14px Arial, Sans-Serif;
        }

        .rootNode
        {
            font-size: 18px;
            width: 100%;
            border-bottom: Solid 1px black;
        }

        .leafNode
        {
            border: Dotted 2px black;
            padding: 4px;
            background-color: #eeeeee;
            font-weight: bold;
        }
    </style>


    <script type="text/javascript">
        function resizeIframe(obj) {
            obj.style.height = (obj.contentWindow.document.body.scrollHeight + 25) + 'px';
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active">
            <div class="form-horizontal">
                <div class="row col-sm-12 NoRightPadding">
                    <div class="col-sm-6">
                        <h1>Character Skills</h1>
                    </div>
                    <div class="col-sm-6 NoRightPadding">
                        <div class="row col-sm-12 NoRightPadding">
                            <div class="col-sm-9 text-right NoRightPadding">
                                <label for="tbCharacter" class="control-label">Character</label>
                            </div>
                            <div class="col-sm-3 NoRightPadding">
                                <asp:TextBox ID="tbFirstName" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="row col-sm-12 NoRightPadding">
                            <div class="col-sm-9 text-right NoRightPadding">
                                <label for="tbBuildPoints" class="control-label">Character Total Build Point</label>
                            </div>
                            <div class="col-sm-3 text-right NoRightPadding">
                                <asp:TextBox ID="tbBuildPoints" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="row col-sm-12 NoRightPadding">
                            <div class="col-sm-9 text-right NoRightPadding">
                                <label for="tbSpentPoints" class="control-label">Character Spent Points</label>
                            </div>
                            <div class="col-sm-3 text-right NoRightPadding">
                                <asp:TextBox ID="tbSpentPoints" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnl" runat="server" Visible="false">
                    <div class="row col-sm-12 NoRightPadding" style="display: none;">
                        <div class="col-sm-6">
                            <div class="control-label pull-left"><b>Maintain Character Skills. Note Campaigns may have Custom Character Generator in addition.</b></div>
                        </div>
                        <div class="col-sm-6 NoRightPadding">
                            <div class="row col-sm-12 NoRightPadding">
                                <div class="col-sm-9 text-right NoRightPadding">
                                    <label for="tbAvailable" class="control-label">Available Points</label>
                                </div>
                                <div class="col-sm-3 NoRightPadding">
                                    <asp:TextBox ID="tbAvailable" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 form-control-static" style="display: none;">
                        <asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="false" GridLines="Vertical" CssClass="table table-striped table-bordered table-hover table-condensed">
                            <Columns>
                                <asp:BoundField DataField="CharacterSkillsStandardID" HeaderText="ID" />
                                <asp:BoundField DataField="SkillSetName" HeaderText="Skill Set Name" />
                                <asp:BoundField DataField="StatusName" HeaderText="StatusName" />
                                <asp:BoundField DataField="SkillSetTypeDescription" HeaderText="SkillSetTypeDescription" />
                                <asp:BoundField DataField="SkillName" HeaderText="SkillName" />
                                <asp:BoundField DataField="SkillShortDescription" HeaderText="SkillShortDesc" />
                                <asp:BoundField DataField="SkillLongDescription" HeaderText="SkillLongDesc" />
                                <asp:BoundField DataField="CampaignSkillsStandardComments" HeaderText="Comments" />
                                <asp:BoundField DataField="SkillTypeDescription" HeaderText="TypeDesc" />
                                <asp:BoundField DataField="SkillTypeComments" HeaderText="TypeComments" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="row col-sm-12">
                        <label for="ddlHeaderType" class="control-label col-sm-1-5">Header Type</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlHeaderType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDescriptor_SelectedIndexChanged" />
                        </div>
                        <div class="col-sm-0-5">
                        </div>
                        <div class="col-sm-6">
                            <asp:Label ID="lblSkillShortDesc" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="row col-sm-12">
                        <label for="ddlHeader" class="control-label col-sm-1-5">Header</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlHeader" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDescriptor_SelectedIndexChanged" />
                        </div>
                        <div class="col-sm-0-5">
                        </div>
                        <div class="col-sm-6">
                            <asp:Label ID="lblSkillLongDesc" runat="server" CssClass="col-sm-6 form-control" Text="Here's some text." />
                        </div>
                    </div>

                    <div class="row col-sm-12">
                        <label for="ddlSkillType" class="control-label col-sm-1-5">Skill Type</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlSkillType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDescriptor_SelectedIndexChanged" />
                        </div>
                    </div>

                    <div class="row col-sm-12">
                        <label for="ddlSkillName" class="control-label col-sm-1-5">Skill Name</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlSkillName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDescriptor_SelectedIndexChanged" />
                        </div>
                    </div>

                    <div class="row col-sm-12">
                        <label for="ddlHeader" class="control-label col-sm-1-5">Prerequisite</label>
                        <div class="col-sm-3 control-label" style="text-align: left;">
                            <asp:Label ID="lblPrereq" runat="server">Here's the prerequisite.</asp:Label>
                        </div>
                        <div class="col-sm-0-5">
                        </div>
                        <div class="col-sm-6">
                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-6 form-control" />
                        </div>
                    </div>

                    <div class="row col-sm-12">
                        <label class="control-label col-sm-1-5">
                            Standard Cost
                        </label>
                        <div class="col-sm-3 control-label" style="text-align: left;">
                            <asp:Label ID="lblStandardCost" runat="server" Text="Here's the standard cost." />
                        </div>
                        <div class="col-sm-0-5">
                        </div>
                        <div class="col-sm-6">
                            <asp:Label ID="Label2" runat="server" CssClass="col-sm-6 form-control" />
                        </div>
                    </div>

                    <div class="row col-sm-12">
                        <label class="control-label col-sm-1-5">
                            Cost Modifier
                        </label>
                        <div class="col-sm-3 control-label" style="text-align: left;">
                            <asp:Label ID="lblCostModifier" runat="server" Text="Cost Modifier.." />
                        </div>
                        <div class="col-sm-0-5">
                        </div>
                        <div class="col-sm-6">
                            <asp:Label ID="Label3" runat="server" CssClass="col-sm-6 form-control" />
                        </div>
                    </div>
                </asp:Panel>
            </div>

        </div>

    </div>--%>
    <div class="mainContent tab-content col-sm-12">
        <div class="form-horizontal">
            <div class="row col-sm-12 NoRightPadding">
                <div class="col-sm-6">
                    &nbsp;
                </div>
            </div>
        </div>
    </div>
    <iframe id="FrameSkills" name="FrameSkills" src="CharSkill.aspx" onload='javascript:resizeIframe(this);' style="border: 0px solid black; width: 1000px;" />
    <br />
    <br />
    <br />
    <%--    <div class="mainContent tab-content">
        <section id="public-campaigns">
            <div class="row">
                <div class="panel-wrapper col-sm-10">
                    <div class="panel">
                        <div class="panelheader">
                            <h2>Campaign Search</h2>
                            <div class="panel-body">
                                <div class="panel-container  search-criteria">
                                    <asp:TreeView ID="tvSkills" runat="server" SkipLinkText="" BorderWidth="0px" NodeWrap="True" ExpandDepth="1" PopulateNodesFromClient="False" ShowLines="True" ImageSet="Simple" NodeIndent="10">
                                        <SelectedNodeStyle Font-Bold="True" Font-Underline="True" ForeColor="#DD5555" VerticalPadding="0px" />
                                        <ParentNodeStyle Font-Bold="False" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="10px" VerticalPadding="0px" />
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>--%>
</asp:Content>
