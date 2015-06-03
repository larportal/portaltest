<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CharSkill.aspx.cs" Inherits="LarpPortal.Character.CharSkill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.10.2.min.js"></script>

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
                data: "{'CampaignID':'" + d.toString() + "'}",
                url: "/CampaignInfo.asmx/GetCampaignInfo",
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
        //$.ajax({
        //    type: "get",
        //    url: "/CampaignInfo.aspx/GetCampaignInfo",
        //    data: "{ CampaignID: " + d + "}",
        //    contentType: "application/json",
        //    dataType: "json",
        //    success: OnSuccessCall,
        //    error: OnErrorCall
        //});
        //}

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

    </script>

    <style type="text/css">
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

    </style>
</head>
<body style="width: auto; height: 560px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server" />

        <asp:UpdatePanel ID="upSkill" runat="server">
            <ContentTemplate>
                <table style="width: 100%;" border="0">
                    <tr class="TableItems" style="vertical-align: top;">
                        <td style="width: 40%;" class="TableItems">
                            <asp:Panel ID="pnlTreeView" runat="server" ScrollBars="Vertical" Height="500px">
                                <asp:TreeView ID="tvSkills" runat="server" SkipLinkText="" BorderColor="Black" BorderStyle="Solid" BorderWidth="0" ShowCheckBoxes="All"
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
                                <asp:GridView ID="gvCostList" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvCostList_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Skill" HeaderText="Skill" />
                                        <asp:BoundField DataField="Cost" HeaderText="Cost" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField>
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
                        </td>
                        <td align="right">
                            <asp:Button ID="btnSave" runat="server" Width="100px" CssClass="StandardButton" Text="Save" OnClick="btnSave_Click" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
