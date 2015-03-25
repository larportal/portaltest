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

        div
        {
            border: 0px solid black;
        }
    </style>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="CharHeaderScripts" runat="server">

    <script type="text/javascript">
        function resizeIframe(obj) {
            obj.style.height = (obj.contentWindow.document.body.scrollHeight - 150) + 'px';
            var myHidden = document.getElementById('<%= hidFrameHeight.ClientID %>');

            if (myHidden)//checking whether it is found on DOM, but not necessary
            {
                myHidden.value = (obj.contentWindow.document.body.scrollHeight - 150).toString();
                //alert(myHidden.value);
            }
            document.getElementById('<%= lblFrameHeight.ClientID %>').value = (obj.contentWindow.document.body.scrollHeight - 150).toString();
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblFrameHeight" runat="server" />
    <asp:HiddenField ID="hidFrameHeight" runat="server" />
    <%--    <div class="mainContent tab-content col-sm-12">
        <div class="form-horizontal col-sm-12">
            <div class="row col-sm-12 NoRightPadding">
                <div class="col-sm-12">
   <asp:Image ID="imgBlank" runat="server" ImageUrl="~/img/blank.gif" Width="1000" Height="1" BorderStyle="Solid" BorderColor="Black" BorderWidth="1" />
                </div>
            </div>
        </div>
    </div>--%>
    <div class="mainContent tab-content col-sm-12">
        <div class="row">
            <div class="panel-wrapper" runat="server">
                <div class="panel">
                    <div class="panelheader">
                        <h2>Skills</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria">
                                <asp:Panel ID="pnlFrame" runat="server" Height="600px">
                                    <iframe id="FrameSkills" name="FrameSkills" src="CharSkill.aspx"
                                        style="border: 0px solid green; width: 100%; height: 1580px;" />
                                    Here's another.
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="row" style="border: solid 1px red;">
                <div class="col-sm-10">This is a spot.</div>
                <div class="col-sm-2 NoRightPadding">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default input-group button form-control" Style="height: 30px;" Text="Save" OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </div>
    <%--    <br />
    <br />
    <br />
    <div class="mainContent tab-content">
        <section>
            <div class="row">
                <div class="panel-wrapper" style="width: 1000px;">
                    <div class="panel">
                        <div class="panelheader">
                            <h2>Skills</h2>
                            <div class="panel-body">
                                <div class="panel-container  search-criteria">
                                    <asp:TreeView ID="tvSkills" runat="server" SkipLinkText="" BorderWidth="0px" NodeWrap="True" ExpandDepth="1" PopulateNodesFromClient="False" ShowLines="True" ImageSet="Simple" NodeIndent="10">
                                        <SelectedNodeStyle Font-Bold="True" Font-Underline="True" ForeColor="#DD5555" VerticalPadding="0px" Width="500px" />
                                        <ParentNodeStyle Font-Bold="False" />
                                        <%--                                        <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
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
