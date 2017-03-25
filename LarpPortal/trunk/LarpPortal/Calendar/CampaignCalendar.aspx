<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CampaignCalendar.aspx.cs" Inherits="LarpPortal.Calendars.CampaignCalendar" MasterPageFile="~/Calendar/Calendars.master" %>

<asp:Content ID="ScriptSection" runat="server" ContentPlaceHolderID="CalendarHeaderScripts">
    <script type="text/javascript">

        function openMessage() {
            $('#modalMessage').modal('show');
            return false;
        }
        function closeMessage() {
            $('#modalMessage').hide();
        }

        function ScrollToDate() {
            location.hash = '#ScrollToDate';
        }

    </script>

    <script src="../Scripts/jquery-1.11.3.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</asp:Content>

<asp:Content ID="StyleSection" runat="server" ContentPlaceHolderID="CalendarHeaderStyles">
    <style>
        .EventHeader {
            color: blue;
        }

        .cellHeader {
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-top: 1px solid black;
            height: 20px;
            width: 12%;
            font-size: larger;
        }

        .cellContents {
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-bottom: 1px solid black;
            height: 80px;
            width: 12%;
            text-align: left;
            vertical-align: top;
            padding-left: 2px;
            padding-right: 2px;
        }

        .cellColumnHeader {
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-top: 1px solid black;
            height: 20px;
            width: 12%;
            font-size: larger;
            text-align: center;
            padding-left: 2px;
            padding-right: 2px;
        }

        .CalendarMonth {
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-top: 1px solid black;
            height: 20px;
            /*width: 12%;*/
            font-size: larger;
            text-align: center;
            background-color: lightgray;
        }

        th, tr:nth-child(even) > td {
            background-color: #ffffff;
        }

        .Calendar {
            border: 1px solid black;
            width: 99%;
        }

        .tooltip-inner {
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
            margin-bottom: 0px;
            background-color: white;
            color: black;
            font-size: 14px;
            box-shadow: 5px 5px 10px #888888;
            width: auto;
            min-width: 200px;
            opacity: 1;
            filter: alpha(opacity=100);
            white-space: nowrap;
        }

        .eventHighlight {
            background-color: blue;
            color: white;
        }

        div {
            border: 0px solid black;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="ReportsContent" ID="PELList" runat="server">
    <div class="mainContent tab-content col-sm-12">
        <div class="row" style="padding-left: -15px; padding-top: 10px; padding-bottom: 10px; vertical-align: bottom;">
            <div class="col-sm-6 NoGutters">
                <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Campaign Calendar" />
            </div>
        </div>
<%--        <div class="row" style="padding-bottom: 20px;">
            Campaign:
            <asp:DropDownList ID="ddlCampList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampList_SelectedIndexChanged" />
        </div>--%>
        <div class="row" style="padding-left: -15px; padding-right: 0px;">
            <asp:Label ID="lblCalendar" runat="server" />
            <div class="panel" style="padding-top: 0px; padding-bottom: 0px;" id="divCalendar" runat="server">
                <div class="panelheader">
                    <h2>Event List</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria" style="padding-bottom: 10px;">
                            <div style="max-height: 500px; overflow-y: auto; margin-right: 10px;">
                                <asp:Table ID="tabCalendar" runat="server" CssClass="Calendar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalMessage" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <a class="close" data-dismiss="modal" style="color: white;">×</a>
                    LARPortal Registration
                </div>
                <div class="modal-body" style="background-color: white;">
                    <p>
                        <asp:Label ID="lblMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer ">
                    <asp:Button ID="btnCloseMessage" runat="server" Text="Close" Width="150px" CssClass="StandardButton" OnClientClick="closeMessage(); return false;" />
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $('[data-toggle="popover"]').popover({
            placement: 'top',
            trigger: 'hover'
        });



    </script>

</asp:Content>
