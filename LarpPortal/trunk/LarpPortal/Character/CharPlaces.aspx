<%@ Page Title="" Language="C#" MasterPageFile="~/Character/Character.master" AutoEventWireup="true" CodeBehind="CharPlaces.aspx.cs" Inherits="LarpPortal.Character.CharPlaces" EnableEventValidation="false" %>
<%@ Register TagPrefix="CharSelecter" TagName="CSelect" Src="~/controls/CharacterSelect.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CharHeaderMain" runat="server">
    <div class="mainContent tab-content col-lg-12">
        <div class="row" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Size="24px" Style="font-weight: 500" Text="Character Places" />
        </div>

        <div class="row col-sm-12" style="padding-bottom: 20px; padding-left: 15px; padding-right: 0px;">
            <%-- border: 1px solid black;">--%>
            <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">
                <CharSelecter:CSelect ID="oCharSelect" runat="server" />
            </div>
            <div class="col-sm-2">
<%--                Here.--%>
            </div>
        </div>

        <br />
        <div class="row" style="padding-left: 15px; padding-top: 20px;">
            <div class="panel" style="padding-top: 5px;">
                <div class="panelheader">
                    <h2>Places</h2>
                    <div class="panel-body">
                        <div class="panel-container search-criteria">
                            <iframe id="FrameSkills" name="FrameSkills" src="CharPlace.aspx"
                                style="border: 0px solid green; width: 100%; height: 600px" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
