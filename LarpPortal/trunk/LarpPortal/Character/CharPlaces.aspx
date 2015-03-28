<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharPlaces.aspx.cs" Inherits="LarpPortal.Character.CharPlaces" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-sm-12">
        <div class="row">
            <h1 class="col-sm-10">
                <asp:Label ID="lblHeader" runat="server" Text="Places" /></h1>
        </div>
    </div>

    <div class="mainContent tab-content col-sm-12">
        <div class="row">
            <div id="Div1" class="panel-wrapper" runat="server">
                <div class="panel">
                    <div class="panelheader">
                        <h2>Places</h2>
                        <div class="panel-body">
                            <div class="panel-container search-criteria">
                                <asp:Panel ID="pnlFrame" runat="server" Height="600px">
                                    <iframe id="FrameSkills" name="FrameSkills" src="CharPlace.aspx"
                                        style="border: 0px solid green; width: 100%; height: 1580px;" />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
