<%@ Page Title="" Language="C#" MasterPageFile="~/MemberProfile.master" AutoEventWireup="true" CodeBehind="MemberDemographics.aspx.cs" Inherits="LarpPortal.MemberDemographics" %>

<asp:Content ID="MyProfileDemographics" ContentPlaceHolderID="Demographics" runat="server">
   
<asp:Label ID="WIP" runat="server" BackColor="Yellow"> Profile - Demographics - Placeholder page - in progress</asp:Label>
    <div class="mainContent tab-content">
        <section id="demographics" class="demographics tab-pane active">
            <div role="form" class="form-horizontal form-condensed">
                <div class="col-sm-12">
                    <h1 class="col-sm-4">My Profile - <span>Demographics</span></h1>
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-7">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Label AssociatedControlID="txtFirstName" ID="lblFirstName" runat="server" CssClass="control-label col-sm-1">First</asp:Label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtMI" ID="lblMI" runat="server" CssClass="control-label col-sm-1">MI</asp:Label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtMI" runat="server" placeholder="M" MaxLength="1" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtLastName" ID="lblLastName" runat="server" CssClass="control-label col-sm-1">Last</asp:Label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                                </div>
                            </div>
                        </div><!--.ContactInfoName  -->
                        <div class="col-sm-12">
                            <div class="form-group">
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="col-sm-12" AutoPostBack="true"                                         
                                        OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
                                        <asp:ListItem Text="Gender" Value="" disabled="disabled"></asp:ListItem>
                                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtGenderOther" placeholder="Other Description" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtDOB" ID="lblDOB" runat="server" CssClass="control-label col-sm-1">DOB</asp:Label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div><!-- contactInfo -->
                        <div class="emergency col-sm-12">
                            <h2>Emergency Contact Information</h2>
                            <div class="form-group">
                                <asp:Label AssociatedControlID="txtEmergencyName" ID="lblEmergencyName" runat="server" CssClass="control-label col-sm-2">Name</asp:Label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtEmergencyName" runat="server" CssClass="form-control" placeholder="Emergency Contact Name"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtEmergencyPhone" ID="lblEmergencyPhone" runat="server" CssClass="control-label col-sm-1">Phone</asp:Label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtEmergencyPhone" runat="server" CssClass="form-control" placeholder="Phone"></asp:TextBox>
                                </div>
                            </div>
                        </div><!-- Emergency Info -->
                    </div>

                    <div class="userNames col-sm-3">
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtUsername" ID="lblUsername" runat="server" CssClass="col-sm-2">User</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ReadOnly="true" placeholder="User Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtNickname" ID="lblNickname" runat="server" CssClass="col-sm-2">Nick</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtNickname" runat="server" CssClass="form-control" placeholder="Nick Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtPenname" ID="lblPenname" runat="server" CssClass="col-sm-2">Pen</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtPenname" runat="server" CssClass="form-control" placeholder="Pen Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtForumname" ID="lblForumname" runat="server" CssClass="col-sm-2">Forum</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtForumname" runat="server" CssClass="form-control" placeholder="Forum Name"></asp:TextBox>
                            </div>
                        </div>

                        <div class="uploadFile row col-sm-12">
                            <div class="input-group">
                                <input type="text" class="form-control" placeholder="Browse to add photo">
                                <span class="input-group-btn">
                                    <%--<span class="btn btn-default btn-sm btnFile">Browse<input type="file" multiple>
                                    </span>--%>
                                    <asp:FileUpload ID="fuMemberImage" runat="server" CssClass="btn btn-default btn-sm btnFile" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="userPhoto col-sm-2">
                        <asp:Image ID="imgPlayerImage" runat="server" AlternateText="Player Picture" Height="150" Width="150" />
                    </div>
                </div>

                 <div class="col-sm-12">
                     <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
                </div>

                <div class="col-sm-12">
                    <div class="col-sm-10">
                        <div class="panel-wrapper">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Addresses</h2>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="gvw_Address" runat="server" CssClass="panel-container-table"></asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12">
                    <div class="col-sm-6 panel-wrapper">
                        <div class="panel-wrapper">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Phone Numbers</h2>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="gvw_PhoneNums" runat="server" CssClass="panel-container-table"></asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-5 panel-wrapper">
                        <div class="panel-wrapper">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Email Addresses</h2>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="gvw_Emails" runat="server" CssClass="panel-container-table"></asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="edit-save col-sm-1">
                        <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
        </section>
        <!-- demographics -->
    </div>
    <%--<script src="Scripts/jquery-2.1.3.js"></script>
    <script src="Scripts/jquery.maskedinput.js"></script>            
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $("#txtDOB").mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
            $("#txtEmergencyPhone").mask("999-999-9999");
        });
    </script>--%>

</asp:Content>
