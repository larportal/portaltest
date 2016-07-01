<%@ Page Title="" Language="C#" MasterPageFile="~/LARPortal.master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="LarpPortal.TestPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPage" runat="server">

    <style type="text/css">
/* Fx. set the header height to 100px */
.ajax__tab .ajax__tab_tab {
	height:100px; /* here */
	margin:0;
	padding:10px 4px;
}


/* Then increase the body's top attribute in order to expose the rest of the tab header */
.ajax__tab .ajax__tab_body
{
	position:absolute;
	top:100px; /* here */
	width:205px;
}</style>
    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>
    <ajaxToolkit:TabContainer ID="tc" runat="server" Height="500">
        <ajaxToolkit:TabPanel runat="server" HeaderText="Pan1" ID="Panel1" Height="450">
            <HeaderTemplate>
                Campaign Skills
            </HeaderTemplate>
            <ContentTemplate>
                        Master Skills<br />
                <asp:ListBox ID="lb" Rows="10" runat="server" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Second Pan" ID="panel2" Height="450">
            <HeaderTemplate>
                Node Placement
            </HeaderTemplate>
            <ContentTemplate>
                Here's another panel.
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
<asp:Button ID="btnBack" runat="server" Text=" Prev " OnClick="btnBack_Click" />&nbsp;<asp:Button ID="btnNext" runat="server" Text=" Next " OnClick="btnNext_Click"  />
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
