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
                                    <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtMI" ID="lblMI" runat="server" CssClass="control-label col-sm-1">MI</asp:Label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtMI" runat="server" placeholder="M" MaxLength="1" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtLastName" ID="lblLastName" runat="server" CssClass="control-label col-sm-1">Last</asp:Label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtLastName" runat="server" TabIndex="3" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                                </div>
                            </div>
                        </div><!--.ContactInfoName  -->
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Label AssociatedControlID="ddlGender" ID="lblGender" runat ="server" CssClass="control-label col-sm-1">Gender</asp:Label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="col-sm-12" TabIndex="5" AutoPostBack="true"                                         
                                        OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
                                        <asp:ListItem Text="Gender" Value="" disabled="disabled"></asp:ListItem>
                                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGenderOther" placeholder="Other Description" TabIndex="6" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtDOB" ID="lblDOB" runat="server" CssClass="control-label col-sm-1">DOB</asp:Label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDOB" runat="server" TabIndex="7" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div><!-- contactInfo -->
                       <div class="col-sm-12">
                           <div class="form-group">
                               <div class="emptyrow col-sm-12">
                                   <br />
                               </div>
                           </div>
                       </div>
                       <div class="col-sm-12">
                        <div class="form-group">
                            <div class="emergency col-sm-12">
                                <h3>Emergency Contact Information</h3>
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="txtEmergencyName" ID="lblEmergencyName" runat="server" CssClass="control-label col-sm-1">Name</asp:Label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtEmergencyName" runat="server" TabIndex="11" CssClass="form-control" placeholder="Emergency Contact Name"></asp:TextBox>
                                    </div>
                                    <asp:Label AssociatedControlID="txtEmergencyPhone" ID="lblEmergencyPhone" runat="server" CssClass="control-label col-sm-1">Phone</asp:Label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtEmergencyPhone" runat="server" TabIndex="12" CssClass="form-control" placeholder="Phone"></asp:TextBox>
                                    </div>
                                </div>
                            </div><!-- Emergency Info -->
                        </div>
                       </div> 
                    </div>

                    <div class="userNames col-sm-3">
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtUsername" ID="lblUsername" runat="server" CssClass="col-sm-2">User Name</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtUsername" runat="server" TabIndex="4" CssClass="form-control" placeholder="User Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtNickname" ID="lblNickname" runat="server" CssClass="col-sm-2">Nick Name</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtNickname" runat="server" TabIndex="8" CssClass="form-control" placeholder="Nick Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtPenname" ID="lblPenname" runat="server" CssClass="col-sm-2">Pen Name</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtPenname" runat="server" TabIndex="9" CssClass="form-control" placeholder="Pen Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtForumname" ID="lblForumname" runat="server" CssClass="col-sm-2">Forum Name</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtForumname" runat="server" TabIndex="10" CssClass="form-control" placeholder="Forum Name"></asp:TextBox>
                            </div>
                        </div>

                        <div class="uploadFile row col-sm-12">
                            <div class="input-group">                                
                                <span class="input-group-btn">                                    
                                    <asp:FileUpload ID="fuMemberImage" runat="server" TabIndex="13" Width="90px" CssClass="btn btn-default btn-sm btnFile col-sm-1" />
                                </span>
                                <input type="text" class="form-control col-sm-1" readonly="readonly" placeholder="Browse to add photo">
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
                                    <!-- AlternatingRowStyle-BackColor="Linen" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid"
                                         Caption="<span style='font-size: larger; font-weight: bold;'>Addresses</span>"
                                       -->  
        <%--<asp:UpdatePanel ID="gvAddressPanel" runat="server">
            <ContentTemplate>
        --%>                                       

                                    <asp:GridView ID="gv_Address" runat="server" AutoGenerateColumns="false"  Width="100%" TabIndex="14"
                                        DataKeyNames="IntAddressID"
                                        OnRowCommand="gv_Address_RowCommand" OnRowDataBound="gv_Address_RowDataBound"                                     
                                        OnRowEditing="gv_Address_RowEditing" OnRowUpdating="gv_Address_RowUpdating" OnRowCancelingEdit="gv_Address_RowCancelingEdit"
                                        OnRowDeleting="gv_Address_RowDeleting"
                                        CssClass="panel-container-table">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Address 1">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Address1" runat="server" Text='<%# Bind("StrAddress1") %>'></asp:TextBox>
                                                        
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:BoundField DataField="StrAddress1" HeaderText="Address 1" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewHeader" />
                                            <asp:BoundField DataField="StrAddress2" HeaderText="Address 2" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewHeader" />
                                            <asp:BoundField DataField="StrCity" HeaderText="City" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewHeader" />
                                            <asp:BoundField DataField="StrStateID" HeaderText="State" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewHeader" />
                                            <asp:BoundField DataField="StrPostalCode" HeaderText="Postal/Zip Code" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewHeader" />
                                            <asp:BoundField DataField="StrCountry" HeaderText="Country" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewHeader" />                                            
                                            <asp:TemplateField HeaderText="Type" ItemStyle-Width="80px" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <!-- At some point I need to disable this two custom controls and enable only when editing to avoid confusions on the save -->
                                                    <asp:DropDownList runat="server" ID="ddAddressType" DataTextField="strAddressTypeId" Width="50px"  >
                                                        <asp:ListItem Text="None" Value="0" disabled="disabled"></asp:ListItem>
                                                        <asp:ListItem Text="Home" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Mailing" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Work" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Site" Value="4"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField  HeaderText="Primary" ItemStyle-Width="30px" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:RadioButton runat="server" ID="rbtnPrimary" DataTextField="IsPrimary" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:CommandField ShowEditButton="true" ButtonType="Image" EditImageUrl="~/img/edit.gif" ItemStyle-Width="22px" 
                                                UpdateImageUrl="~/img/add22x22.png" CancelImageUrl="~/img/delete.png" />

                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/img/delete.png" ItemStyle-Width="22px" />

                                        </Columns>
                                         
                                    </asp:GridView>
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
                                   
                                    <asp:GridView ID="gv_PhoneNums" runat="server" AutoGenerateColumns="false" Width="100%" TabIndex="15" 
                                        OnRowCommand="gv_PhoneNums_RowCommand" OnRowDataBound="gv_PhoneNums_RowDataBound"
                                        OnRowEditing="gv_PhoneNums_RowEditing" OnRowUpdating="gv_PhoneNums_RowUpdating" OnRowCancelingEdit="gv_PhoneNums_RowCancelingEdit"
                                        OnRowDeleting="gv_PhoneNums_RowDeleting"
                                        CssClass="panel-container-table">
                                        <Columns>
                                            <%--<asp:BoundField DataField="AreaCode" HeaderText="Area Code" ItemStyle-CssClass="GridViewItem" HeaderStyle-CssClass="GridViewHeader" ItemStyle-Width="50px" />
                                            --%>
                                            <asp:TemplateField HeaderText="Area Code" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="gv_txtAreaCode" CssClass="form-control col-sm-1"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone Number" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="gv_txtPhoneNumber" CssClass="form-control col-sm-1"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Extension" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="gv_txtExtension" CssClass="form-control col-sm-1"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" ItemStyle-Width="80px">
                                               <ItemTemplate>
                                                   <asp:DropDownList runat="server" ID="ddPhoneNumber" DataTextField="PhoneTypeID">
                                                       <asp:ListItem Text="None" Value="0" disabled="disabled"></asp:ListItem>
                                                       <asp:ListItem Text="Home" Value="1"></asp:ListItem>
                                                       <asp:ListItem Text="Mobile" Value="2"></asp:ListItem>
                                                       <asp:ListItem Text="Fax" Value="3"></asp:ListItem>
                                                       <asp:ListItem Text="Work" Value="4"></asp:ListItem>
                                                       <asp:ListItem Text="Business" Value="5"></asp:ListItem>
                                                   </asp:DropDownList>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Primary" ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:RadioButton runat="server" ID="rbtnPrimary1"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:CommandField ShowEditButton="true" ButtonType="Image" EditImageUrl="~/img/edit.gif" ItemStyle-Width="22px" 
                                                UpdateImageUrl="~/img/add22x22.png" CancelImageUrl="~/img/delete.png" />

                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/img/delete.png" ItemStyle-Width="22px" />

                                            
                                        </Columns>
                                    </asp:GridView>
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
                                    
                                    <asp:GridView ID="gv_Emails" runat="server" AutoGenerateColumns="false" Width="100%" TabIndex="16" 
                                        OnRowCommand="gv_Emails_RowCommand" OnRowDataBound="gv_Emails_RowDataBound"
                                        OnRowEditing="gv_Emails_RowEditing" OnRowUpdating="gv_Emails_RowUpdating" OnRowCancelingEdit="gv_Emails_RowCancelingEdit"
                                        OnRowDeleting="gv_Emails_RowDeleting"
                                        CssClass="panel-container-table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Email Address" HeaderStyle-CssClass="GridViewHeader" ItemStyle-CssClass="GridViewItem" ItemStyle-Width="180px">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="gv_txtEmailAddress" CssClass="form-control col-sm-4"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddEmailTypeId" DataTextField="EmailTypeId">
                                                        <asp:ListItem Text="None" Value="0" disabled="disabled"></asp:ListItem>
                                                        <asp:ListItem Text="Home" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Work" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Primary" ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:RadioButton runat="server" ID="rbtnPrimary2" />
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="true" ButtonType="Image" EditImageUrl="~/img/edit.gif" ItemStyle-Width="22px"
                                                UpdateImageUrl="~/img/add22x22.png" CancelImageUrl="~/img/delete.png" />
                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/img/delete.png" ItemStyle-Width="22px" />
                                        </Columns>
                                    </asp:GridView>
                                
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
