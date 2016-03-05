<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="SkillTable.aspx.cs" Inherits="LarpPortal.SkillTable" %>

<html>
    <body>

        <form runat="server">

                                                    <asp:TreeView ID="tvSkills" runat="server" SkipLinkText="" BorderColor="Black" BorderStyle="Solid" BorderWidth="0" ShowCheckBoxes="All"
                                                        ShowLines="false" Font-Underline="false" CssClass="TreeItems" EnableClientScript="false"
                                                        LeafNodeStyle-CssClass="TreeItems" NodeStyle-CssClass="TreeItems">
                                                        <LevelStyles>
                                                            <asp:TreeNodeStyle Font-Underline="false" />
                                                        </LevelStyles>
                                                    </asp:TreeView>

        </form>
    </body>
</html>

