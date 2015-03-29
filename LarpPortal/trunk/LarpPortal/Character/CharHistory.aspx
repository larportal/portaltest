<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharHistory.aspx.cs" Inherits="LarpPortal.Character.CharHistory" EnableViewState="true" %>

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
                        <div class="panel-wrapper col-xs-10">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Character History</h2>
                                </div>
                                <textarea name="taHistory" id="taHistory" rows="10" class="col-xs-12" runat="server" placeholder="Type your characters history here" />
                            </div>
                        </div>
<%--                        <div class="col-xs-10 panel-wrapper">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Comments</h2>
                                </div>
                                <textarea name="taComments" id="taComments" rows="2" class="col-xs-12" runat="server" />
                            </div>
                        </div>--%>
                    </div>
                    <span class="col-xs-9 panel-wrapper text-right">&nbsp;
                    </span>
                    <span class="col-xs-1" style="float: right;">
                        <asp:Button ID="btnSave" runat="server" CssClass="StandardButton" Style="height: 30px;" Text="Save" OnClick="btnSave_Click" />
                    </span>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
