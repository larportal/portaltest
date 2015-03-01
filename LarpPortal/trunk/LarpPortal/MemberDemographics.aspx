<%@ Page Title="" Language="C#" MasterPageFile="~/MemberProfile.master" AutoEventWireup="true" CodeBehind="MemberDemographics.aspx.cs" Inherits="LarpPortal.MemberDemographics" %>

<asp:Content ID="MyProfileDemographics" ContentPlaceHolderID="Demographics" runat="server">
<asp:Label ID="WIP" runat="server" BackColor="Yellow"> Profile - Demographics - Placeholder page - in progress</asp:Label>
    <div class="mainContent tab-content">
        <section id="demographics" class="demographics tab-pane active">
            <div role="form" class="form-horizontal form-condensed">
                <div class="col-sm-12">
                    <h1 class="col-sm-4">My Profile - <span>Demographics</span></h1>
                    <div class="panel-wrapper col-sm-1">
                        <a href="#">Edit</a>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-7">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Label AssociatedControlID="txtFirstName" ID="lblFirstName" runat="server" CssClass="control-label col-sm-1">First</asp:Label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtMI" ID="lblMI" runat="server" CssClass="control-label col-sm-1">MI</asp:Label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtMI" runat="server" CssClass="form-control" MaxLength="1"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtLastName" ID="lblLastName" runat="server" CssClass="control-label col-sm-1">Last</asp:Label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="form-group">
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="col-sm-12" AutoPostBack="true" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtGenderOther" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtDOB" ID="lblDOB" runat="server" CssClass="control-label col-sm-1">DOB</asp:Label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="emergency col-sm-12">
                            <h4>Emergency Contact Information</h4>
                            <div class="form-group">
                                <asp:Label AssociatedControlID="txtEmergencyName" ID="lblEmergencyName" runat="server" CssClass="control-label col-sm-2">Name</asp:Label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtEmergencyName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label AssociatedControlID="txtEmergencyPhone" ID="lblEmergencyPhone" runat="server" CssClass="control-label col-sm-1">Phone</asp:Label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtEmergencyPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="userNames col-sm-3">
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtUsername" ID="lblUsername" runat="server" CssClass="col-sm-2">Username</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtNickname" ID="lblNickname" runat="server" CssClass="col-sm-2">Nickname</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtNickname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtPenname" ID="lblPenname" runat="server" CssClass="col-sm-2">Pen Name</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtPenname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtForumname" ID="lblForumname" runat="server" CssClass="col-sm-2">Forum Name</asp:Label>
                            <div class="userName col-sm-9">
                                <asp:TextBox ID="txtForumname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="uploadFile row col-sm-12">
                            <div class="input-group">
                                <input type="text" class="form-control" placeholder="Browse to add photo">
                                <span class="input-group-btn">
                                    <span class="btn btn-default btn-sm btnFile">Browse<input type="file" multiple>
                                    </span>
                                    <asp:FileUpload ID="fuMemberImage" runat="server" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="userPhoto col-sm-2">
                        <asp:Image ID="imgPlayerImage" runat="server" AlternateText="Player Picture" Width="150" ImageUrl="img/Player/PL-2-RickPierce.jpg" />
                    </div>
                </div>

<%--                <div class="col-sm-12">
                    <div class=" col-sm-10">
                        <div class="panel-wrapper">
                            <div class="panel">
                                <div class="panel-header">
                                    <h2>Addresses</h2>
                                </div>
                                <div class="panel-body">
                                    <div class="panel-container-table">
                                        <table class="table table-striped table-bordered table-hover table-condensed" data-toggle="table" data-height="100" data-sort-name="primary" data-sort-order="asc">
                                            <thead>
                                                <tr>
                                                    <th data-field="address-1" data-sortable="true">Address 1</th>
                                                    <th data-field="address-2" data-sortable="true">Address 2</th>
                                                    <th data-field="city" data-sortable="true">City</th>
                                                    <th data-field="address-state" data-sortable="true">State</th>
                                                    <th data-field="zip" data-sortable="true">Postal/Zip Code</th>
                                                    <th data-field="country" data-sortable="true">Country</th>
                                                    <th data-field="type" data-sortable="true">Type</th>
                                                    <th data-field="primary" data-sortable="true">Primary</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <input type="text" placeholder="address 1" value="483 Monroe Tpk">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="address 2" value="#161">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="city" value="Monroe">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="st" value="CT">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="zip" value="06468">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="country" value="USA">
                                                    </td>
                                                    <td>
                                                        <select class="">
                                                            <option value="home">home</option>
                                                            <option value="work">work</option>
                                                            <option value="cottage">cottage</option>
                                                            <option value="mailing" selected>mailing</option>
                                                            <option value="other">other</option>
                                                        </select>
                                                    </td>
                                                    <td>
                                                        <input type="radio" name="primary-address" value="" checked>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="text" placeholder="address 1" value="972 Monroe Tpk">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="address 2" value="">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="city" value="Monroe">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="st" value="CT">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="zip" value="06468">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="country" value="USA">
                                                    </td>
                                                    <td>
                                                        <select class="">
                                                            <option value="home" selected>home</option>
                                                            <option value="work">work</option>
                                                            <option value="cottage">cottage</option>
                                                            <option value="mailing">mailing</option>
                                                            <option value="other">other</option>
                                                        </select>
                                                    </td>
                                                    <td>
                                                        <input type="radio" name="primary-address" value="">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="text" placeholder="address 1" value="">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="address 2" value="">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="city" value="">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="st" value="">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="zip" value="">
                                                    </td>
                                                    <td>
                                                        <input type="text" placeholder="country" value="">
                                                    </td>
                                                    <td>
                                                        <select class="">
                                                            <option value="home">home</option>
                                                            <option value="work">work</option>
                                                            <option value="cottage">cottage</option>
                                                            <option value="mailing">mailing</option>
                                                            <option value="other">other</option>
                                                        </select>
                                                    </td>
                                                    <td>
                                                        <input type="radio" name="primary-address" value="">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- .addresses -->
                </div>--%>

<%--                <div class="col-sm-12">
                    <div class="col-sm-6 panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <h2>Phone Numbers</h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container-table">
                                    <table class="table table-striped table-bordered table-hover table-condensed" data-toggle="table" data-height="100" data-sort-name="phone-primary" data-sort-order="asc">
                                        <thead>
                                            <tr>
                                                <th data-field="phone-area-code" data-sortable="true">Area Code</th>
                                                <th data-field="phone-number" data-sortable="true">Phone #</th>
                                                <th data-field="phone-extension" data-sortable="true">Extension</th>
                                                <th data-field="phone-type" data-sortable="true">Type</th>
                                                <th data-field="phone-primary" data-sortable="true">Primary</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <input type="text" placeholder="area" value="203">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="phone" value="260-4281">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="ext" value="">
                                                </td>
                                                <td>
                                                    <select class="">
                                                        <option value="home" selected>home</option>
                                                        <option value="work">work</option>
                                                        <option value="emergency">emergency</option>
                                                        <option value="other">other</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="radio" name="primary-phone" value="">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" placeholder="area" value="203">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="phone" value="260-4282">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="ext" value="">
                                                </td>
                                                <td>
                                                    <select class="">
                                                        <option value="home" selected>home</option>
                                                        <option value="work">work</option>
                                                        <option value="emergency">emergency</option>
                                                        <option value="other">other</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="radio" name="primary-phone" value="">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" placeholder="area" value="203">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="phone" value="260-4283">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="ext" value="">
                                                </td>
                                                <td>
                                                    <select class="">
                                                        <option value="home">home</option>
                                                        <option value="work">work</option>
                                                        <option value="emergency selected">emergency</option>
                                                        <option value="other">other</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="radio" name="primary-phone" value="" checked>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" placeholder="area" value="203">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="phone" value="260-4284">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="ext" value="">
                                                </td>
                                                <td>
                                                    <select class="">
                                                        <option value="home">home</option>
                                                        <option value="work" selected>work</option>
                                                        <option value="emergency">emergency</option>
                                                        <option value="other">other</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="radio" name="primary-phone" value="">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" placeholder="area" value="">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="phone" value="">
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="ext" value="">
                                                </td>
                                                <td>
                                                    <select class="">
                                                        <option value="home">home</option>
                                                        <option value="work">work</option>
                                                        <option value="emergency">emergency</option>
                                                        <option value="other">other</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="radio" name="primary-phone" value="">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- .userPhoneNumbers -->
                    <div class="col-sm-5 panel-wrapper">
                        <div class="panel">
                            <div class="panel-header">
                                <h2>Email Addresses</h2>
                            </div>
                            <div class="panel-body">
                                <div class="panel-container-table">
                                    <table class="table table-striped table-bordered table-hover table-condensed" data-toggle="table" data-height="100" data-sort-name="email-primary" data-sort-order="asc">
                                        <thead>
                                            <tr>
                                                <th data-field="email-address" data-sortable="true">Email Address</th>
                                                <th data-field="email-type" data-sortable="true">Type</th>
                                                <th data-field="email-primary" data-sortable="true">Primary</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <input type="text" placeholder="email address" value="rgpierce@earthlink.net">
                                                </td>
                                                <td>
                                                    <select class="">
                                                        <option value="home" selected>home</option>
                                                        <option value="work">work</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="radio" name="primary-email" value="" checked>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>owner@LARPportal.com
                                                </td>
                                                <td>
                                                    <select>
                                                        <option value="home">home</option>
                                                        <option value="work" selected>work</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="radio" name="primary-email" value="">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>rpierce@themarlincompany.com
                                                </td>
                                                <td>
                                                    <select>
                                                        <option value="home">home</option>
                                                        <option value="work" selected>work</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="radio" name="primary-email" value="">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="text" placeholder="email address" value="">
                                                </td>
                                                <td>
                                                    <select>
                                                        <option value="home">home</option>
                                                        <option value="work">work</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="radio" name="primary-email" value="">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- .userEmailAddress -->
                    <div class="edit-save col-sm-1">
                        <button type="submit" class="btn btn-default">Save</button>
                    </div>
                </div>--%>
                <!-- .row -->
            </div>
        </section>
        <!-- demographics -->
    </div>
</asp:Content>
