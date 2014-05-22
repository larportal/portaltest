<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LarpPortal._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-4">
            <p class="site-title"><a runat="server" href="~/"><asp:Image ID="LARPortal" runat="server" Width="600" ImageUrl="~/Images/Portal_2010_by_Palpatine.jpg" /></a></p>
        </div>
    </div>
        <div class="jumbotron">
        <h1>LARP Portal</h1>
        <p class="lead">The gateway to managing all your LARPs.</p>
    </div>


</asp:Content>
