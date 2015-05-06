﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.Master" AutoEventWireup="true" CodeBehind="CharSkills.aspx.cs" Inherits="LarpPortal.Character.CharSkills" %>

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
        <script src="/Scripts/jquery-1.10.2.min.js"></script>

    <script type="text/javascript">
        //var PanelID = document.getElementById('divMainArea');
        //Panel.setAttribute("style", "width:1500px");

        function resizeIframe(obj) {
            obj.style.height = (obj.contentWindow.document.body.scrollHeight - 150) + 'px';
            var myHidden = document.getElementById('<%= hidFrameHeight.ClientID %>');

            if (myHidden)//checking whether it is found on DOM, but not necessary
            {
                myHidden.value = (obj.contentWindow.document.body.scrollHeight - 150).toString();
            }
            document.getElementById('<%= lblFrameHeight.ClientID %>').value = (obj.contentWindow.document.body.scrollHeight - 150).toString();
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblFrameHeight" runat="server" />
    <asp:HiddenField ID="hidFrameHeight" runat="server" />
    <div class="mainContent tab-content" id="divMainArea">
        <section role="form" class="form-horizontal form-condensed">
            <div class="row">
                <h1 class="col-xs-10" style="padding-top: 0px;">
                    <asp:Label ID="lblHeader" runat="server" /></h1>
            </div>
            <div class="row">
                <div class="col-xs-12">&nbsp;&nbsp;Skill validation may take a few moments.  Please wait while screen refreshes <u>BEFORE</u> Saving.</div>
            </div>
            <div class="row">
                <div class="panel-wrapper" runat="server">
                    <div class="panel">
                        <div class="panelheader">
                            <h2>Skills</h2>
                            <div class="panel-body">
                                <div class="panel-container search-criteria" style="padding: 0px;">
                                    <asp:Panel ID="pnlFrame" runat="server" Height="600px">
                                        <iframe id="FrameSkills" name="FrameSkills" src="CharSkill.aspx"
                                            style="width: 100%; height: 600px;" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
