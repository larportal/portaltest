<%@ Page Title="" Language="C#" MasterPageFile="~/MemberProfile.master" AutoEventWireup="true" CodeBehind="MemberDemographics.aspx.cs" Inherits="LarpPortal.MemberDemographics" %>

<asp:Content ID="MyProfileDemographics" ContentPlaceHolderID="Demographics" runat="server">
    My Profile - Demographics - Placeholder page - in progress
    				<div class="mainContent tab-content">

                        <!-- DEMOGRAPHICS -->
                        <section id="demographics" class="demographics tab-pane active">
                            <form role="form" class="form-horizontal form-condensed">

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
                                                <label for="user-demo-first-name" class="control-label col-sm-1">First</label>
                                                <div class="col-sm-3">
                                                    <input type="text" name="user-demo-first-name" id="user-demo-first-name" placeholder="First Name" value="Richard" class="form-control">
                                                </div>
                                                <label for="user-demo-m-name" class="control-label col-sm-1">MI</label>
                                                <div class="col-sm-2">
                                                    <input type="text" name="user-demo-m-name" id="user-demo-m-name" maxlength="1" placeholder="" value="M" class="form-control">
                                                </div>
                                                <label for="user-demo-last-name" class="control-label col-sm-1">Last</label>
                                                <div class="col-sm-4">
                                                    <input type="text" name="user-demo-last-name" id="user-demo-last-name" placeholder="Last Name" value="Pierce" class="form-control">
                                                </div>
                                            </div>
                                            <!-- .formGroup -->
                                        </div>
                                        <!-- .contactInfoName -->
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <div class="col-sm-3">
                                                    <select class="col-sm-12">
                                                        <option selected disabled>Gender</option>
                                                        <option value="m">Male</option>
                                                        <option value="f">Female</option>
                                                        <option value="o">Other</option>
                                                    </select>
                                                </div>
                                                <!-- .gender -->
                                                <label for="user-demo-other-descrip" class="control-label sr-only">Other Description</label>
                                                <div class="col-sm-4">
                                                    <input type="text" name="user-demo-other-descrip" id="user-demo-other-descrip" placeholder="Other Description" value="" class="form-control">
                                                </div>
                                                <label for="dob" class="control-label col-sm-1">DOB</label>
                                                <div class="col-sm-4">
                                                    <input type="text" name="dob" id="dob" placeholder="mm-yyyy" value="" class="form-control">
                                                </div>
                                            </div>
                                            <!-- .formGroup -->
                                        </div>
                                        <!-- .contactInfoDemo -->
                                        <div class="emergency col-sm-12">
                                            <h2>Emergency Contact Information</h2>
                                            <div class="form-group">
                                                <label for="user-demo-emergency" class="control-label col-sm-2">Name</label>
                                                <div class="col-sm-5">
                                                    <input type="text" name="user-demo-emergency" id="user-demo-emergency" placeholder="Emergency Contact Name" value="Monique Pierce" class="form-control">
                                                </div>
                                                <label for="user-demo-emergency-phone" class="control-label col-sm-1">Phone</label>
                                                <div class="col-sm-4">
                                                    <input type="text" name="user-demo-emergency-phone" id="user-demo-emergency-phone" placeholder="Phone #" value="203-414-3751" class="form-control">
                                                </div>
                                            </div>
                                            <!-- .formGroup -->
                                        </div>
                                        <!-- .emergency -->
                                    </div>
                                    <!-- .contactInfo -->

                                    <div class="userNames col-sm-3">
                                        <div class="form-group">
                                            <label for="userName" class="col-sm-2">User</label>
                                            <div class="userName col-sm-9">
                                                <input type="text" name="userName" id="userName" placeholder="User Name" value="rgpierce@earthlink.net" class="form-control">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="user-profile-nick-name" class="col-sm-2">Nick</label>
                                            <div class="userName col-sm-9">
                                                <input type="text" name="user-profile-nick-name" id="user-profile-nick-name" placeholder="First Name" value="Rick" class="form-control">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="penName" class="col-sm-2">Pen</label>
                                            <div class="userName col-sm-9">
                                                <input type="text" name="penName" id="penName" placeholder="Pen Name" value="" class="form-control">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="forumName" class="col-sm-2">Forum</label>
                                            <div class="userName col-sm-9">
                                                <input type="text" name="forumName" id="forumName" placeholder="Forum Name" value="" class="form-control">
                                            </div>
                                            <!-- .userName -->
                                        </div>
                                        <!-- .formGroup -->

                                        <div class="uploadFile row col-sm-12">
                                            <div class="input-group">
                                                <input type="text" class="form-control" placeholder="Browse to add photo">
                                                <span class="input-group-btn">
                                                    <span class="btn btn-default btn-sm btnFile">Browse<input type="file" multiple>
                                                    </span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="userPhoto col-sm-2">
                                        <img src="img/rick.jpg" alt="User photo" width="150">
                                    </div>
                                    <!-- .userPhoto -->
                                </div>

                                <div class="col-sm-12">
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
                                                                        <input type="text" placeholder="address 1" value="483 Monroe Tpk"></td>
                                                                    <td>
                                                                        <input type="text" placeholder="address 2" value="#161"></td>
                                                                    <td>
                                                                        <input type="text" placeholder="city" value="Monroe"></td>
                                                                    <td>
                                                                        <input type="text" placeholder="st" value="CT"></td>
                                                                    <td>
                                                                        <input type="text" placeholder="zip" value="06468"></td>
                                                                    <td>
                                                                        <input type="text" placeholder="country" value="USA"></td>
                                                                    <td>
                                                                        <select class="">
                                                                            <option value="home">home</option>
                                                                            <option value="work">work</option>
                                                                            <option value="cottage">cottage</option>
                                                                            <option value="mailing" selected>mailing</option>
                                                                            <option value="other">other</option>
                                                                        </select></td>
                                                                    <td>
                                                                        <input type="radio" name="primary-address" value="" checked></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input type="text" placeholder="address 1" value="972 Monroe Tpk"></td>
                                                                    <td>
                                                                        <input type="text" placeholder="address 2" value=""></td>
                                                                    <td>
                                                                        <input type="text" placeholder="city" value="Monroe"></td>
                                                                    <td>
                                                                        <input type="text" placeholder="st" value="CT"></td>
                                                                    <td>
                                                                        <input type="text" placeholder="zip" value="06468"></td>
                                                                    <td>
                                                                        <input type="text" placeholder="country" value="USA"></td>
                                                                    <td>
                                                                        <select class="">
                                                                            <option value="home" selected>home</option>
                                                                            <option value="work">work</option>
                                                                            <option value="cottage">cottage</option>
                                                                            <option value="mailing">mailing</option>
                                                                            <option value="other">other</option>
                                                                        </select></td>
                                                                    <td>
                                                                        <input type="radio" name="primary-address" value=""></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <input type="text" placeholder="address 1" value=""></td>
                                                                    <td>
                                                                        <input type="text" placeholder="address 2" value=""></td>
                                                                    <td>
                                                                        <input type="text" placeholder="city" value=""></td>
                                                                    <td>
                                                                        <input type="text" placeholder="st" value=""></td>
                                                                    <td>
                                                                        <input type="text" placeholder="zip" value=""></td>
                                                                    <td>
                                                                        <input type="text" placeholder="country" value=""></td>
                                                                    <td>
                                                                        <select class="">
                                                                            <option value="home">home</option>
                                                                            <option value="work">work</option>
                                                                            <option value="cottage">cottage</option>
                                                                            <option value="mailing">mailing</option>
                                                                            <option value="other">other</option>
                                                                        </select></td>
                                                                    <td>
                                                                        <input type="radio" name="primary-address" value=""></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- .addresses -->
                                </div>

                                <div class="col-sm-12">
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
                                                                    <input type="text" placeholder="area" value="203"></td>
                                                                <td>
                                                                    <input type="text" placeholder="phone" value="260-4281"></td>
                                                                <td>
                                                                    <input type="text" placeholder="ext" value=""></td>
                                                                <td>
                                                                    <select class="">
                                                                        <option value="home" selected>home</option>
                                                                        <option value="work">work</option>
                                                                        <option value="emergency">emergency</option>
                                                                        <option value="other">other</option>
                                                                    </select></td>
                                                                <td>
                                                                    <input type="radio" name="primary-phone" value=""></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input type="text" placeholder="area" value="203"></td>
                                                                <td>
                                                                    <input type="text" placeholder="phone" value="260-4282"></td>
                                                                <td>
                                                                    <input type="text" placeholder="ext" value=""></td>
                                                                <td>
                                                                    <select class="">
                                                                        <option value="home" selected>home</option>
                                                                        <option value="work">work</option>
                                                                        <option value="emergency">emergency</option>
                                                                        <option value="other">other</option>
                                                                    </select></td>
                                                                <td>
                                                                    <input type="radio" name="primary-phone" value=""></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input type="text" placeholder="area" value="203"></td>
                                                                <td>
                                                                    <input type="text" placeholder="phone" value="260-4283"></td>
                                                                <td>
                                                                    <input type="text" placeholder="ext" value=""></td>
                                                                <td>
                                                                    <select class="">
                                                                        <option value="home">home</option>
                                                                        <option value="work">work</option>
                                                                        <option value="emergency selected">emergency</option>
                                                                        <option value="other">other</option>
                                                                    </select></td>
                                                                <td>
                                                                    <input type="radio" name="primary-phone" value="" checked></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input type="text" placeholder="area" value="203"></td>
                                                                <td>
                                                                    <input type="text" placeholder="phone" value="260-4284"></td>
                                                                <td>
                                                                    <input type="text" placeholder="ext" value=""></td>
                                                                <td>
                                                                    <select class="">
                                                                        <option value="home">home</option>
                                                                        <option value="work" selected>work</option>
                                                                        <option value="emergency">emergency</option>
                                                                        <option value="other">other</option>
                                                                    </select></td>
                                                                <td>
                                                                    <input type="radio" name="primary-phone" value=""></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input type="text" placeholder="area" value=""></td>
                                                                <td>
                                                                    <input type="text" placeholder="phone" value=""></td>
                                                                <td>
                                                                    <input type="text" placeholder="ext" value=""></td>
                                                                <td>
                                                                    <select class="">
                                                                        <option value="home">home</option>
                                                                        <option value="work">work</option>
                                                                        <option value="emergency">emergency</option>
                                                                        <option value="other">other</option>
                                                                    </select></td>
                                                                <td>
                                                                    <input type="radio" name="primary-phone" value=""></td>
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
                                                                    <input type="text" placeholder="email address" value="rgpierce@earthlink.net"></td>
                                                                <td>
                                                                    <select class="">
                                                                        <option value="home" selected>home</option>
                                                                        <option value="work">work</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="radio" name="primary-email" value="" checked></td>
                                                            </tr>
                                                            <tr>
                                                                <td>owner@LARPportal.com</td>
                                                                <td>
                                                                    <select>
                                                                        <option value="home">home</option>
                                                                        <option value="work" selected>work</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="radio" name="primary-email" value=""></td>
                                                            </tr>
                                                            <tr>
                                                                <td>rpierce@themarlincompany.com</td>
                                                                <td>
                                                                    <select>
                                                                        <option value="home">home</option>
                                                                        <option value="work" selected>work</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="radio" name="primary-email" value=""></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input type="text" placeholder="email address" value=""></td>
                                                                <td>
                                                                    <select>
                                                                        <option value="home">home</option>
                                                                        <option value="work">work</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input type="radio" name="primary-email" value=""></td>
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

                                </div>
                                <!-- .row -->
                            </form>
                        </section>
                        <!-- demographics -->
                    </div>
</asp:Content>
