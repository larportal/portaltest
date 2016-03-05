<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharSkillsFull.aspx.cs" Inherits="LarpPortal.Character.CharSkillsFull" %>

<asp:Content ID="Scripts" ContentPlaceHolderID="CharHeaderScripts" runat="server">
    <script type="text/javascript">
        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }
        }

        function ShowContent(d) {
            if (d.length < 1) { return; }
            var dd = document.getElementById(d);
            dd.style.display = "block";
        }

        function GetContent(d) {
            $.ajax({
                contentType: "application/json; charset=utf-8",
                data: "{'SkillNodeID':'" + d.toString() + "'}",
                url: "/SkillNodeInfo.asmx/getSkillNodeInfo",
                type: "POST",
                dataType: 'json',
                success: function (result) {
                    divDesc.innerHTML = result.d;
                },
                error: function (xhr, msg) {
                    alert("error: " + xhr.responseText + "   " + msg.toString());
                }
            });
        }

        function Callback(result) {
            var outDiv = document.getElementById("outputDiv");
            outDiv.innerText = result;
        }
        function OnSuccessCall(response) {
            alert(response.d);
        }

        function OnErrorCall(response) {
            alert(response.status + " " + response.statusText);
        }

        function DisableButton() {
            document.forms[0].submit();
            window.setTimeout("disableButton('" +
               window.event.srcElement.id + "')", 0);
        }

        function disableButton(buttonID) {
            document.getElementById(buttonID).disabled = true;
            document.getElementById(buttonID).value = "...Saving";
        }

        var xPos, yPos;

        function BeginRequestHandler(sender, args) {
            if ($get('<%=pnlTreeView.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos = $get('<%=pnlTreeView.ClientID%>').scrollLeft;
                yPos = $get('<%=pnlTreeView.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=pnlTreeView.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=pnlTreeView.ClientID%>').scrollLeft = xPos;
                $get('<%=pnlTreeView.ClientID%>').scrollTop = yPos;
            }
        }

        function openModal() {
            $('#myModal').modal('show');
        }
        function closeModal() {
            $('#myModal').hide();
        }

    </script>

    <%--In order to make the modal work you need to include these scripts.--%>
    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>


    <script>
    </script>

</asp:Content>

<asp:Content ContentPlaceHolderID="CharHeaderStyles" runat="server" ID="Styles">
    <style type="text/css">
        .TreeItems
        {
            text-decoration: none;
            height: 14px;
            display: inline-block;
        }

            .TreeItems td
            {
                height: 14px;
            }

            .TreeItems input
            {
                height: 14px;
                margin: 3px 3px 3px 4px;
            }

            .TreeItems label
            {
                display: inline-block;
                margin-bottom: 5px;
                font-weight: bold;
            }

        body
        {
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
            font-size: 14px;
            line-height: 1.42857;
            /*color: #333333;
            background-color: #b2c68d;*/
        }

        .form-control
        {
            display: block;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857;
            color: #555555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            -webkit-transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
            transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
        }

        .form-horizontal .control-label
        {
            text-align: right;
            margin-bottom: 0;
            padding-top: 7px;
        }

        .label
        {
            display: inline-block;
            /*max-width: 100%;*/
            margin-bottom: 5px;
            font-weight: bold;
        }

        .TreeItems
        {
            text-decoration: none;
        }

        .TableItems
        {
            text-wrap: none;
            vertical-align: top;
        }

            .TableItems th
            {
                background-color: #ffffff;
            }

        .TablesItems tr
        {
            background-color: white;
        }










        .PointsLabel
        {
            text-align: right;
            padding-right: 10px;
            font-weight: bold;
        }

        .TextAlignRight
        {
            text-align: right;
        }

        .StandardButton
        {
            background-color: darkblue;
            border-color: black;
            border-width: 1px;
            border-style: solid;
            color: white;
        }

            .StandardButton:hover
            {
                background-color: lightblue;
            }

        .SkillsLocked
        {
            color: red;
            font-weight: bold;
        }

        .tv table tbody tr
        {
            display: inline-block;
            padding: 0px;
            margin-left: 5px;
            width: 100%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainContent tab-content col-lg-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Skills" />
        </div>
        <div class="row" style="padding-left: 15px; padding-bottom: 10px;">
            <table>
                <tr style="vertical-align: middle;">
                    <td style="width: 10px"></td>
                    <td>
                        <b>Selected Character:</b>
                    </td>
                    <td style="padding-left: 10px;">
                        <asp:DropDownList ID="ddlCharacterSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCharacterSelector_SelectedIndexChanged" />
                    </td>
                    <td style="padding-left: 20px;">
                        <b>Campaign:</b>
                    </td>
                    <td style="padding-left: 10px;">
                        <asp:Label ID="lblCampaign" runat="server" Text="" />
                    </td>
                    <td style="padding-left: 20px;">
                        <b>Last Update:</b>
                    </td>
                    <td style="padding-left: 10px;">
                        <asp:Label ID="lblUpdateDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: transparent; padding-top: 10px;"></td>
                    <td colspan="6" style="background-color: transparent; padding-top: 10px;">Skill validation may take a few moments.  Please wait while screen refreshes <u>BEFORE</u> Saving.</td>
                </tr>
            </table>
        </div>

        <div class="row" style="padding-left: 15px;">
            <div class="plain-panel" style="padding-top: 5px;">
                <div class="plain-panelheader">
                    <h2>Character Skills</h2>
                    <div class=" plain-panel-body">
                        <div class="plain-panel-container search-criteria">


                            <script type="text/javascript">
                                // It is important to place this JavaScript code after ScriptManager1
                            </script>

                            <asp:UpdatePanel ID="upSkill" runat="server">
                                <ContentTemplate>
                                    <table style="width: 100%;" border="0">
                                        <tr class="TableItems" style="vertical-align: top;">
                                            <td style="width: 40%;" class="TableItems">
                                                <asp:Panel ID="pnlTreeView" runat="server" ScrollBars="Vertical" Height="500px">
                                                    <asp:TreeView ID="tvSkills" runat="server"
                                                        SkipLinkText="" BorderColor="Black" BorderStyle="Solid" BorderWidth="0" ShowCheckBoxes="All"
                                                        ShowLines="false" OnTreeNodeCheckChanged="tvSkills_TreeNodeCheckChanged" Font-Underline="false" CssClass="TreeItems" EnableClientScript="false"
                                                        LeafNodeStyle-CssClass="TreeItems" NodeStyle-CssClass="TreeItems">
                                                        <LevelStyles>
                                                            <asp:TreeNodeStyle Font-Underline="false" />
                                                        </LevelStyles>
                                                    </asp:TreeView>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 40%; padding-right: 20px;" class="TableItems">
                                                <asp:Panel ID="pnlDescription" runat="server" ScrollBars="Vertical" Height="500px">
                                                    <div id="divDesc" />
                                                    <br />
                                                    <asp:TextBox ID="tbPlayerComments" runat="server" Visible="false" />
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 20%;" class="TableItems" align="right">
                                                <asp:Panel ID="pnlCostList" runat="server" ScrollBars="Vertical" Height="500px">
                                                    <asp:GridView ID="gvCostList" AlternatingRowStyle-BackColor="Transparent" runat="server" RowStyle-BackColor="Transparent"
                                                        AutoGenerateColumns="false" GridLines="None" HeaderStyle-Font-Size="14pt" OnRowDataBound="gvCostList_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="Skill" HeaderText="Skill" />
                                                            <asp:TemplateField ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    &nbsp;&nbsp;
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Cost" HeaderText="Cost" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hidColor" runat="server" Value='<%# Eval("Color") %>' />
                                                                    <asp:HiddenField ID="hidSortOrder" runat="server" Value='<%# Eval("SortOrder") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="5px">
                                                                <ItemTemplate>
                                                                    &nbsp;&nbsp;
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblMessage" runat="server" />
                                                <asp:Label ID="lblSkillsLocked" runat="server" Text="Changes not allowed after Save" CssClass="SkillsLocked" Visible="false" />
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnSave" runat="server" Width="100px" CssClass="StandardButton" Text="Save" OnClick="btnSave_Click" /></td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hidAllowCharacterRebuild" runat="server" Value="0" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="myModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    Character Save
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:UpdatePanel ID="upMessage" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblPopMessage" runat="server" Text="There is no text for this object." />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClick="btnClose_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    </script>
</asp:Content>
