<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharNoCampaign.aspx.cs" Inherits="LarpPortal.Character.CharNoCampaign" %>

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

        [disabled]
        { /* Text and background colour, medium red on light yellow */
            color: #933;
            background-color: #ffc;
        }

        .control-label
        {
            text-align: right;
        }

        .WithBorder
        {
            border: 1px solid black;
        }

        /*div, label
        {
            border: 1px solid black;
        }*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div id="Div1" class="character-info tab-pane active">
            <div class="col-sm-12">
                <div class="row">
                    <h1 class="col-sm-10" style="text-align: center;">There appears to be a problem with this character.<br />
                        Contact tech support with this problem.<br />
                        <br />
                    Please choose a different character.</h1>
                </div>
            </div>
        </div>
        </div>
</asp:Content>
