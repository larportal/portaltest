<%@ Page Title="" Language="C#" MasterPageFile="~/MemberCampaignsAdmin.master" AutoEventWireup="true" CodeBehind="CampaignInfoSetup.aspx.cs" Inherits="LarpPortal.CampaignInfoSetup" %>
<asp:Content ID="CampaignInfoSetup" ContentPlaceHolderID="MemberCampaignsAdminContent" runat="server">
    <asp:Label ID="WIP" runat="server" BackColor="Yellow">Campaign Info Setup - Placeholder page - in progress</asp:Label>
<%--    			<div class="mainContent tab-content">

				<section id="info" class="campaign-info tab-pane active">
					<div role="form" class="form-horizontal form-condensed">
						<div class="col-sm-12">
							<h1 class="col-sm-4">Campaign Information</h1>
							<div class="panel-wrapper col-sm-1">
								<a href="#">Edit</a>
							</div>
							<div class="panel-wrapper">
								<div class="uploadFile col-sm-4">
									<div class="input-group">
										<input type="text" class="form-control" placeholder="Browse to add photo">
										<span class="input-group-btn">
											<span class="btn btn-default btn-sm btnFile">Browse<input type="file" multiple></span>
										</span>
									</div>
								</div><!-- .uploadFile -->
							</div>
						</div>
						<div class="col-sm-12">
							<div class="col-sm-5">
								<div class="panel-wrapper">
									<div class="panel">
										<div class="panel-header">
											<h2>Demographics</h2>
										</div>
										<div class="panel-body">
											<div class="panel-container scroll-200">
												<div class="form-group">
													<label for="campaign-name" class="col-sm-3 control-label">Name</label>
													<div class="col-sm-9">
														<input type="email" class="form-control" id="campaign-name" placeholder="Campaign Name">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-started" class="col-sm-3 control-label">Started</label>
													<div class="col-sm-9">
														<input type="text" class="form-control" id="campaign-started" placeholder="yyyy">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-end" class="col-sm-3 control-label">Expected End</label>
													<div class="col-sm-3">
														<input type="text" class="form-control" id="campaign-end" placeholder="yyyy">
													</div>
													<label for="campaign-num-events" class="col-sm-3 control-label"># Events</label>
													<div class="col-sm-3">
														<input type="text" class="form-control" id="campaign-num-events" placeholder="# Events">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-game-system" class="col-sm-3 control-label">Game System</label>
													<div class="col-sm-9">
														<input type="text" class="form-control" id="campaign-game-system" placeholder="Game System">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-genre-1" class="col-sm-3 control-label">Genre 1</label>
													<div class="col-sm-3">
														<input type="text" class="form-control" id="campaign-genre-1" placeholder="Genre 1">
													</div>
													<label for="campaign-genre-2" class="col-sm-3 control-label">Genre 2</label>
													<div class="col-sm-3">
														<input type="text" class="form-control" id="campaign-genre-2" placeholder="Genre 2">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-style" class="col-sm-3 control-label">Style</label>
													<div class="col-sm-9">
														<input type="text" class="form-control" id="campaign-style" placeholder="Style">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-tech-level" class="col-sm-3 control-label">Tech Level</label>
													<div class="col-sm-9">
														<input type="text" class="form-control" id="campaign-tech-level" placeholder="Tech Level">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-size" class="col-sm-3 control-label">Size</label>
													<div class="col-sm-9">
														<input type="text" class="form-control" id="campaign-size" placeholder="Size">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-avg-events" class="col-sm-3 control-label">Average # Events</label>
													<div class="col-sm-9">
														<input type="text" class="form-control" id="campaign-avg-events" placeholder="AVG # Events">
													</div>
												</div>
											</div>
										</div><!-- .panle-body -->
									</div><!-- .panel -->
								</div><!-- panel-wrapper -->
								<div class="panel-wrapper">
									<div class="panel">
										<div class="panel-header">
											<h2>Contacts</h2>
										</div>
										<div class="panel-body">
											<div class="panel-container scroll-150">
												<ul class="col-sm-12">
													<li>
														<ul class="list-inline">
															<li class="col=sm-4">Campaign GM</li>
															<li class="col=sm-4"><a href="#">Campaign URL</a></li>
															<li class="col=sm-4"><a href="mailto:#">GM@Campaign.com</a></li>
														</ul>
													</li>
													<li>
														<ul class="list-inline">
															<li class="col=sm-4">Character</li>
															<li class="col=sm-4"><a href="#">Character URL</a></li>
															<li class="col=sm-4"><a href="mailto:#">Character@campaign.com</a></li>
														</ul>
														<ul class="list-inline">
															<li class="col=sm-4">Character History</li>
															<li class="col=sm-4"><a href="#">Character History URL</a></li>
															<li class="col=sm-4"><a href="mailto:#">Ch_History@campaign.com</a></li>
														</ul>
													</li>
													<li>
														<ul class="list-inline">
															<li>Info Skills</li>
															<li><a href="#">Info Skills URL</a></li>
															<li><a href="mailto:#">InfoSkills@campaign.com</a></li>
														</ul>
													</li>
													<li>
														<ul class="list-inline">
															<li>Production Skills</li>
															<li><a href="#">Production Skills URL</a></li>
															<li><a href="mailto:#">ProductionSkills@campaign.com</a></li>
														</ul>
													</li>
													<li>
														<ul class="list-inline">
															<li>PEL</li>
															<li><a href="#">PEL URL</a></li>
															<li><a href="mailto:#">PEL@campaign.com</a></li>
														</ul>
													</li>
												</ul>
											</div><!-- .panel-container -->
										</div><!-- .panel-body -->
									</div><!-- .panel -->
								</div><!-- wrapper -->
							</div><!-- .col-sm-4 -->
							<div class="col-sm-5">
								<div class="panel-wrapper">
									<div class="panel">
										<div class="panel-header">
											<h2>Description</h2>
										</div>
										<div class="panel-body">
											<div class="panel-container scroll-200">
												<h3>The World of Aerune</h3>
												<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Illo, esse, incidunt, officiis voluptate at doloremque maxime maiores minus dolorum dicta sequi ab labore odio omnis suscipit dolor numquam. Doloremque, doloribus.</p>
												<p>Fugiat, expedita molestiae aut eius modi facilis ipsam sint omnis iure ut quae nesciunt accusamus perferendis quia recusandae nemo rem consectetur dolores animi illum porro culpa exercitationem voluptas ad blanditiis.</p>
												<p>Quis aut fugit esse voluptate inventore! Sed, ab, suscipit, perferendis in eum laboriosam nulla voluptate nesciunt nisi praesentium harum esse ratione consectetur nemo pariatur ipsa magnam accusamus quisquam labore soluta.</p>
												<p>Consequatur, a odio reiciendis nulla hic non expedita. Dolore, explicabo beatae tempora neque laboriosam cum reprehenderit. Unde labore nam nihil voluptatum laborum quasi delectus pariatur laboriosam. Aliquam similique eveniet alias.</p>
											</div>
										</div><!-- .panle-body -->
									</div><!-- .panel -->
								</div>
								<div class="panel-wrapper">
									<div class="panel">
										<div class="panel-header">
											<h2>Requirements</h2>
										</div>
										<div class="panel-body">
											<div class="panel-container scroll-150">
												<div class="form-group">
													<label for="campaign-req-fee" class="col-sm-4 control-label">Membership Fee</label>
													<div class="col-sm-4">
														<input type="email" class="form-control" id="campaign-req-fee" placeholder="Fee">
													</div>
													<div class="col-sm-4">
														<input type="email" class="form-control" id="campaign-req-fee-freq" placeholder="Frequency">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-req-age" class="col-sm-4 control-label">Minimum Age</label>
													<div class="col-sm-4">
														<input type="email" class="form-control" id="campaign-req-age" placeholder="Min Age">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-req-sup-age" class="col-sm-4 control-label">Supervised Age</label>
													<div class="col-sm-4">
														<input type="email" class="form-control" id="campaign-req-sup-age" placeholder="Sup Age">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-req-waiver" class="col-sm-4 control-label">Waiver</label>
													<div class="col-sm-4">
														<input type="email" class="form-control" id="campaign-req-waiver" placeholder="Req. Waiver">
													</div>
													<p  class="form-control-static"><a href="#">Waiver Link</a></p>
												</div>
												<div class="form-group">
													<div class="col-sm-4 col-sm-offset-4">
														<input type="email" class="form-control" id="campaign-req-sup-waiver-2" placeholder="Waiver 2">
													</div>
												</div>
												<div class="form-group">
													<label for="campaign-req-consent" class="col-sm-4 control-label">Consent</label>
													<div class="col-sm-4">
														<input type="email" class="form-control" id="campaign-req-consent" placeholder="Consent">
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div><!-- col-sm-5 -->
							<div class="col-sm-2 scroll-500">
								<div class="panel-wrapper">
									<img src="http://placehold.it/125x125" alt="Photo of inventory item">
								</div>
								<div class="panel-wrapper">
									<img src="http://placehold.it/125x125" alt="Photo of inventory item">
								</div>
								<div class="panel-wrapper">
									<img src="http://placehold.it/125x125" alt="Photo of inventory item">
								</div>
								<div class="panel-wrapper">
									<img src="http://placehold.it/125x125" alt="Photo of inventory item">
								</div>
							</div>
						</div><!-- col-sm-12 -->
					</div>
				</section><!-- #info -->
                </div>--%>
</asp:Content>