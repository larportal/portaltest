<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharPlaces.aspx.cs" Inherits="LarpPortal.Character.CharPlaces" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mainContent tab-content col-sm-12">
        <div id="character-info" class="character-info tab-pane active">
            <section role="form" class="form-horizontal form-condensed">
                <div class="row">
                    <h1 class="col-xs-10" style="padding-top: 0px;">
                        <asp:Label ID="lblHeader" runat="server" /></h1>
                </div>

                <div class="form-horizontal">
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
            </section>
        </div>
    </div>
</asp:Content>
