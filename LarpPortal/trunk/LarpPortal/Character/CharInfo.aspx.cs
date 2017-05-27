using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharInfo : System.Web.UI.Page
    {
        public bool _Reload = false;
        private string _UserName = "";
        private int _UserID = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnCloseError.Attributes.Add("data-dismiss", "modal");
            btnCloseMessage.Attributes.Add("data-dismiss", "modal");
            ddlAllowRebuild.Attributes.Add("onchange", "ddlRebuildSetVisible();");
            tbRebuildToDate.Attributes.Add("placeholder", "MM/DD/YYYY");

            if (!IsPostBack)
            {
                tbRebuildToDate.Style["display"] = "none";
                lblExpiresOn.Style["display"] = "none";
                tbFirstName.Attributes.Add("Placeholder", "First Name");
                tbLastName.Attributes.Add("Placeholder", "Last Name");
                lblMessage.Text = "";

                ViewState["NewRecCounter"] = (int)-1;

                oCharSelect.WhichSelected = controls.CharacterSelect.Selected.MyCharacters;
            }
            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
            oCharSelect.CharacterChanged += oCharSelect_CharacterChanged;
            btnCancelActor.Attributes.Add("data-dismiss", "modal");
            btnCancelCharDeath.Attributes.Add("data-dismiss", "modal");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (!IsPostBack)
            {
                SortedList sParams = new SortedList();
                sParams.Add("@StatusType", "Character");
                Classes.cUtilities.LoadDropDownList(ddlStatus, "uspGetStatus", sParams, "StatusName", "StatusID", "LARPortal", _UserName, lsRoutineName);
                lblStatus.Visible = false;
                ddlStatus.Visible = true;
            }

            oCharSelect.LoadInfo();

            if (hidActorDateProblems.Value.Length > 0)
                lblDateProblem.Visible = true;
            else
                lblDateProblem.Visible = false;

            if ((oCharSelect.CharacterID != null) && (oCharSelect.CharacterInfo != null))
            {
                int iSelectedCharID = oCharSelect.CharacterID.Value;
                if ((!IsPostBack) || (_Reload))
                {
                    DisplayCharacter(oCharSelect.CharacterInfo);
                }
            }
        }

        protected void btnSavePicture_Click(object sender, EventArgs e)
        {
            if (ulFile.HasFile)
            {
                try
                {
                    string sUser = Session["LoginName"].ToString();
                    Classes.cPicture NewPicture = new Classes.cPicture();
                    NewPicture.PictureType = Classes.cPicture.PictureTypes.Profile;
                    NewPicture.CreateNewPictureRecord(sUser);
                    string sExtension = Path.GetExtension(ulFile.FileName);
                    NewPicture.PictureFileName = "CP" + NewPicture.PictureID.ToString("D10") + sExtension;

                    NewPicture.CharacterID = oCharSelect.CharacterID.Value;

                    string LocalName = NewPicture.PictureLocalName;

                    if (!Directory.Exists(Path.GetDirectoryName(NewPicture.PictureLocalName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(NewPicture.PictureLocalName));

                    ulFile.SaveAs(NewPicture.PictureLocalName);
                    NewPicture.Save(sUser);

                    ViewState["UserIDPicture"] = NewPicture;
                    ViewState.Remove("PictureDeleted");

                    imgCharacterPicture.ImageUrl = NewPicture.PictureURL;
                    imgCharacterPicture.Visible = true;
                    btnClearPicture.Visible = true;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message + "<br>" + ex.StackTrace;
                }
            }
        }

        protected void btnClearPicture_Click(object sender, EventArgs e)
        {
            if (ViewState["UserIDPicture"] != null)
                ViewState["PictureDeleted"] = "Y";
            imgCharacterPicture.Visible = false;
            btnClearPicture.Visible = false;

            SortedList sParam = new SortedList();
            sParam.Add("@CharacterID", oCharSelect.CharacterID.Value);
            Classes.cUtilities.LoadDataTable("uspClearCharacterProfilePicture", sParam, "LARPortal", _UserName, "CharInfo.btnClearPicture");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int iTemp;

            oCharSelect.LoadInfo();

            if ((oCharSelect.CharacterID != null) && (oCharSelect.CharacterInfo != null))
            {
                if ((tbAKA.Text.Length == 0) &&
                    (tbFirstName.Text.Length == 0))
                {
                    // JBradshaw  7/11/2016    Request #1286     Must have at least first name or last name.
                    lblmodalError.Text = "You must fill in at least the first name or the character AKA.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openError();", true);
                    return;
                }

                Classes.cCharacter cChar = new Classes.cCharacter();
                cChar.LoadCharacter(oCharSelect.CharacterID.Value);

                cChar.FirstName = tbFirstName.Text;
                cChar.MiddleName = tbMiddleName.Text;
                cChar.LastName = tbLastName.Text;

                cChar.CurrentHome = tbOrigin.Text;

                cChar.AllowCharacterRebuildToDate = null;

                if (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.CampaignCharacters)
                {
                    cChar.CharacterStatusID = Convert.ToInt32(ddlStatus.SelectedValue);
                    cChar.CharacterType = Convert.ToInt32(ddlCharType.SelectedValue);

                    if (ddlAllowRebuild.SelectedValue == "Y")
                    {
                        tbRebuildToDate.Style["display"] = "inline";
                        lblExpiresOn.Style["display"] = "inline";
                        DateTime dtTemp;
                        if (DateTime.TryParse(tbRebuildToDate.Text, out dtTemp))
                            cChar.AllowCharacterRebuildToDate = dtTemp;
                    }
                    else
                    {
                        tbRebuildToDate.Style["display"] = "none";
                        lblExpiresOn.Style["display"] = "none";
                    }
                }

                if (ddlVisible.SelectedValue == "1")
                    cChar.VisibleToPCs = true;
                else
                    cChar.VisibleToPCs = false;

                cChar.AKA = tbAKA.Text;
                cChar.CurrentHome = tbHome.Text;
                cChar.WhereFrom = tbOrigin.Text;

                // If the drop down list is visible, it means they belong to multiple teams so we need to check it.
                if (ddlTeamList.Visible)
                {
                    if (int.TryParse(ddlTeamList.SelectedValue, out iTemp))
                        cChar.TeamID = iTemp;
                }

                cChar.DateOfBirth = tbDOB.Text;
                if (ViewState["UserIDPicture"] != null)
                {
                    cChar.ProfilePicture = ViewState["UserIDPicture"] as Classes.cPicture;
                    if (ViewState["PictureDeleted"] != null)
                        cChar.ProfilePicture.RecordStatus = Classes.RecordStatuses.Delete;
                }
                else
                    cChar.ProfilePicture = null;

                if (ddlRace.SelectedIndex > -1)
                {
                    cChar.Race = new Classes.cRace();
                    int.TryParse(ddlRace.SelectedValue, out iTemp);
                    cChar.Race.CampaignRaceID = iTemp;
                }

                if (ddlAllowRebuild.SelectedValue == "Y")
                {
                    DateTime dtTemp;
                    if (DateTime.TryParse(tbRebuildToDate.Text, out dtTemp))
                        cChar.AllowCharacterRebuildToDate = dtTemp;
                }
                else
                    cChar.AllowCharacterRebuildToDate = new DateTime(1900, 1, 1);

                if (cChar.CharacterType != 1)       // Means it's not a PC so we need to check who is the current actor.
                {
                    var LastActor = cChar.Actors.OrderByDescending(x => x.StartDate).ToList();
                    if (LastActor == null)
                        cChar.CurrentUserID = _UserID;
                    else
                    {
                        if (LastActor.Count > 0)
                        {
                            cChar.CurrentUserID = LastActor[0].UserID;
                        }
                    }
                }

                cChar.StaffComments = tbStaffComments.Text;

                cChar.SaveCharacter(_UserName, _UserID);
                // JBradshaw  7/11/2016    Request #1286     Changed over to bootstrap popup.
                lblmodalMessage.Text = "Character " + cChar.AKA + " has been saved.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
            }
        }

        protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (oCharSelect.CharacterInfo != null)
            {
                if (oCharSelect.CharacterID.HasValue)
                {
                    Classes.cUser UserInfo = new Classes.cUser(Session["UserName"].ToString(), "PasswordNotNeeded");
                    UserInfo.LastLoggedInCampaign = oCharSelect.CharacterInfo.CampaignID;
                    UserInfo.LastLoggedInCharacter = oCharSelect.CharacterID.Value;
                    UserInfo.LastLoggedInMyCharOrCamp = (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters ? "M" : "C");
                    UserInfo.Save();
                }
                _Reload = true;
            }
        }

        protected void oCharSelect_CharacterChanged(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (oCharSelect.CharacterInfo != null)
            {
                if (oCharSelect.CharacterID.HasValue)
                {
                    Classes.cUser UserInfo = new Classes.cUser(_UserName, "PasswordNotNeeded");
                    UserInfo.LastLoggedInCampaign = oCharSelect.CharacterInfo.CampaignID;
                    UserInfo.LastLoggedInCharacter = oCharSelect.CharacterID.Value;
                    UserInfo.LastLoggedInMyCharOrCamp = (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters ? "M" : "C");
                    UserInfo.Save();
                }
                _Reload = true;
            }
        }

        private void DisplayCharacter(Classes.cCharacter CharInfo)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            hidCharacterID.Value = CharInfo.CharacterID.ToString();
            ViewState["ProfilePictureID"] = CharInfo.ProfilePictureID;

            tbFirstName.Text = CharInfo.FirstName;
            tbMiddleName.Text = CharInfo.MiddleName;
            tbLastName.Text = CharInfo.LastName;

            tbOrigin.Text = CharInfo.CurrentHome;
            tbAKA.Text = CharInfo.AKA;
            tbHome.Text = CharInfo.CurrentHome;
            if (CharInfo.LatestEventDate.HasValue)
                lblDateLastEvent.Text = CharInfo.LatestEventDate.Value.ToString("MM/dd/yyyy");
            else
                lblDateLastEvent.Text = "";

            tbType.Text = CharInfo.CharType.Description;

            if (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.CampaignCharacters)
            {
                ddlVisible.Visible = true;
                ddlAllowRebuild.Visible = true;
                lblAllowSkillRebuild.Visible = true;
                lblVisibleRelationship.Visible = true;
                lblExpiresOn.Visible = true;
                tbType.Visible = false;
                ddlCharType.SelectedValue = CharInfo.CharType.CharacterTypeID.ToString();
                ddlCharType.Visible = true;

                divAddDeath.Attributes["class"] = "show text-right";
                divAddActor.Attributes["class"] = "show text-right";
                lblStatus.Visible = false;
                ddlStatus.Visible = true;
                ddlAllowRebuild.Visible = true;
                lblAllowSkillRebuild.Visible = true;

                if (CharInfo.AllowCharacterRebuild)
                {
                    ddlAllowRebuild.SelectedValue = "Y";
                    tbRebuildToDate.Text = CharInfo.AllowCharacterRebuildToDate.Value.ToShortDateString();
                    tbRebuildToDate.Style["display"] = "inline";
                    lblExpiresOn.Style["display"] = "inline";
                }
                else
                {
                    ddlAllowRebuild.SelectedValue = "N";
                    tbRebuildToDate.Text = "";
                    tbRebuildToDate.Style["display"] = "none";
                    lblExpiresOn.Style["display"] = "none";
                }

                // This is me being overly cautious. If the status is not found I'm not sure what will happen.
                try
                {
                    ddlStatus.SelectedValue = CharInfo.Status.StatusID.ToString();
                }
                catch
                {
                    ddlStatus.ClearSelection();
                    foreach (ListItem litem in ddlStatus.Items)
                    {
                        if (litem.Text.ToUpper() == "ACTIVE")
                            litem.Selected = true;
                    }
                }
            }
            else
            {
                ddlStatus.Visible = false;
                lblStatus.Visible = true;
                tbType.Visible = true;
                ddlCharType.Visible = false;
                lblStatus.Text = CharInfo.Status.StatusName;
                ddlVisible.Visible = false;
                lblVisibleRelationship.Visible = false;
                ddlAllowRebuild.Visible = false;
                lblAllowSkillRebuild.Visible = false;
                lblExpiresOn.Visible = false;
                divAddDeath.Attributes["class"] = "hide";
                divAddActor.Attributes["class"] = "hide";
            }


            if (oCharSelect.CharacterInfo.CharacterType == 1)         // 1 = PC.
            {
                if (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters)
                {
                    divNonCost.Attributes["class"] = "col-sm-6";
                    divDeaths.Attributes["class"] = "col-sm-6";
                    divPlayer.Attributes["class"] = "hide";
                    divActors.Attributes["class"] = "hide";
                }
                else
                {
                    divNonCost.Attributes["class"] = "col-sm-4";
                    divDeaths.Attributes["class"] = "col-sm-4";
                    divActors.Attributes["class"] = "hide";
                    divPlayer.Attributes["class"] = "col-sm-4";
                }
            }
            else
            {
                divNonCost.Attributes["class"] = "col-sm-4";
                divDeaths.Attributes["class"] = "col-sm-4";
                divActors.Attributes["class"] = "col-sm-4";
                divPlayer.Attributes["class"] = "hide";

                SortedList sParams = new SortedList();
                sParams.Add("@CampaignID", oCharSelect.CharacterInfo.CampaignID);
                DataTable dtPlayers = new DataTable();
                dtPlayers = Classes.cUtilities.LoadDataTable("uspGetCampaignPlayers", sParams, "LARPortal", _UserName, lsRoutineName);

                DataView dvPlayers = new DataView(dtPlayers, "", "PlayerFirstLastName", DataViewRowState.CurrentRows);
                ddlActorName.DataSource = dvPlayers;
                ddlActorName.DataTextField = "PlayerFirstLastName";
                ddlActorName.DataValueField = "UserID";
                ddlActorName.DataBind();
                ddlActorName.Items.Insert(0, new ListItem("", "-1"));
            }

            if (CharInfo.VisibleToPCs)
                ddlVisible.SelectedValue = "1";
            else
                ddlVisible.SelectedValue = "0";

            if (CharInfo.Teams.Count == 0)
            {
                ddlTeamList.Visible = false;
                tbTeam.Visible = true;
                tbTeam.Text = "No Teams";
            }
            else if (CharInfo.Teams.Count == 1)
            {
                ddlTeamList.Visible = false;
                tbTeam.Visible = true;
                tbTeam.Text = CharInfo.Teams[0].TeamName;
            }
            else
            {
                ddlTeamList.Visible = true;
                tbTeam.Visible = false;
                ddlTeamList.DataSource = CharInfo.Teams;
                ddlTeamList.DataTextField = "TeamName";
                ddlTeamList.DataValueField = "TeamID";
                ddlTeamList.DataBind();

                ddlTeamList.ClearSelection();

                foreach (ListItem litem in ddlTeamList.Items)
                {
                    if (litem.Value == CharInfo.TeamID.ToString())
                    {
                        ddlTeamList.ClearSelection();
                        litem.Selected = true;
                    }
                }
                if (ddlTeamList.SelectedIndex < 0)
                    ddlTeamList.SelectedIndex = 0;
            }

            tbDOB.Text = CharInfo.DateOfBirth;

            tbAKA.Text = CharInfo.AKA;
            tbDOB.Text = CharInfo.DateOfBirth;
            tbHome.Text = CharInfo.CurrentHome;
            tbNumOfDeaths.Text = CharInfo.Deaths.Count().ToString();
            tbOrigin.Text = CharInfo.WhereFrom;

            DataTable dtCharDescriptors = new DataTable();
            dtCharDescriptors = Classes.cUtilities.CreateDataTable(CharInfo.Descriptors);
            BindDescriptors();

            if (CharInfo.ProfilePicture != null)
            {
                ViewState["UserIDPicture"] = CharInfo.ProfilePicture;
                imgCharacterPicture.ImageUrl = CharInfo.ProfilePicture.PictureURL;
                imgCharacterPicture.Visible = true;
                btnClearPicture.Visible = true;
            }
            else
            {
                imgCharacterPicture.Visible = false;
                btnClearPicture.Visible = false;
            }

            Classes.cCampaignRaces Races = new Classes.cCampaignRaces();
            Races.CampaignID = CharInfo.CampaignID;
            Races.Load(Session["LoginName"].ToString());
            DataTable dtRaces = Classes.cUtilities.CreateDataTable(Races.RaceList);
            ddlRace.DataSource = dtRaces;
            ddlRace.DataTextField = "FullRaceName";
            ddlRace.DataValueField = "CampaignRaceID";
            ddlRace.DataBind();
            if (ddlRace.Items.Count > 0)
            {
                ddlRace.SelectedIndex = 0;
                foreach (ListItem dItems in ddlRace.Items)
                {
                    if (dItems.Value == CharInfo.Race.CampaignRaceID.ToString())
                    {
                        dItems.Selected = true;
                        tbRace.Text = dItems.Text;
                    }
                    else
                        dItems.Selected = false;
                }
            }

            SortedList sParam = new SortedList();
            sParam.Add("@CampaignID", CharInfo.CampaignID);
            DataTable dtDescriptors = Classes.cUtilities.LoadDataTable("uspGetCampaignAttributesStandard",
                sParam, "LARPortal", _UserName, lsRoutineName + ".GetCampaignAttributesStandard");

            DataView dvDescriptors = new DataView(dtDescriptors, "", "CharacterDescriptor", DataViewRowState.CurrentRows);
            ddlDescriptor.DataTextField = "CharacterDescriptor";
            ddlDescriptor.DataValueField = "CampaignAttributeStandardID";
            ddlDescriptor.DataSource = dtDescriptors;
            ddlDescriptor.DataBind();

            if (dtDescriptors.Rows.Count > 0)
            {
                ddlDescriptor.SelectedIndex = 0;
                ddlDescriptor_SelectedIndexChanged(null, null);
            }

            tbStaffComments.Text = CharInfo.StaffComments;
            if (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters)
                divStaffComments.Visible = false;
            else
                divStaffComments.Visible = true;

            BindActors(CharInfo.Actors);

            if (CharInfo.Actors.Count > 0)
            {
                var LatestActor = CharInfo.Actors.OrderByDescending(x => x.StartDate).ToList();
                lblPlayer.Text = "Currently played by " + LatestActor[0].loginUserName + " - ";
                if (LatestActor[0].ActorNickName.Length > 0)
                    lblPlayer.Text += LatestActor[0].ActorNickName;
                else
                    lblPlayer.Text += LatestActor[0].ActorFirstName;
                lblPlayer.Text += " " + LatestActor[0].ActorLastName;
            }

            BindDeaths(CharInfo.Deaths);

            ReadOnlyFields();
        }

        protected void ReadOnlyFields()
        {
            if ((oCharSelect.CharacterInfo.CharacterType != 1) && (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters))
            {
                btnSave.Enabled = false;
                btnSave.CssClass = "btn-default";
                btnSave.Style["background-color"] = "grey";
                btnSaveTop.Enabled = false;
                btnSaveTop.CssClass = "btn-default";
                btnSaveTop.Style["background-color"] = "grey";
                divCharDev.Visible = false;
                tbFirstName.Enabled = false;
                tbFirstName.CssClass = "";
                if (tbFirstName.Text.Length == 0)
                    tbFirstName.Text = " ";
                tbMiddleName.Enabled = false;
                tbMiddleName.CssClass = "";
                if (tbMiddleName.Text.Length == 0)
                    tbMiddleName.Text = " ";
                tbLastName.Enabled = false;
                tbLastName.CssClass = "";
                if (tbLastName.Text.Length == 0)
                    tbLastName.Text = " ";
                tbDOB.Enabled = false;
                tbDOB.CssClass = "";
                tbHome.Enabled = false;
                tbHome.CssClass = "";
                tbOrigin.Enabled = false;
                tbOrigin.CssClass = "";
                tbAKA.Enabled = false;
                tbAKA.CssClass = "";
                ddlRace.Visible = false;
                tbRace.Visible = true;
                ulFile.Visible = false;
                btnUpload.Visible = false;
                btnClearPicture.Visible = false;
                lblProfilePictureText.Visible = false;
            }
            else
            {
                btnSave.Enabled = true;
                btnSave.Style["background-color"] = null;
                btnSave.CssClass = "StandardButton";
                btnSaveTop.Enabled = true;
                btnSaveTop.Style["background-color"] = null;
                btnSaveTop.CssClass = "StandardButton";
                divCharDev.Visible = true;
                tbFirstName.Enabled = true;
                tbFirstName.CssClass = "TableTextBox";
                tbMiddleName.Enabled = true;
                tbMiddleName.CssClass = "TableTextBox";
                tbLastName.Enabled = true;
                tbLastName.CssClass = "TableTextBox";
                tbOrigin.Enabled = true;
                tbOrigin.CssClass = "TableTextBox";
                tbDOB.Enabled = true;
                tbDOB.CssClass = "TableTextBox";
                tbHome.Enabled = true;
                tbHome.CssClass = "TableTextBox";
                tbAKA.Enabled = true;
                tbAKA.CssClass = "TableTextBox";
                ddlRace.Visible = true;
                tbRace.Visible = false;
                ulFile.Visible = true;
                btnUpload.Visible = true;
                lblProfilePictureText.Visible = true;
            }
        }

        #region Descriptors

        protected void ddlDescriptor_SelectedIndexChanged(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            int iCampaignAttributeStandardID;

            List<Classes.cDescriptor> Desc = new List<Classes.cDescriptor>();
            Desc = oCharSelect.CharacterInfo.Descriptors;

            if (int.TryParse(ddlDescriptor.SelectedValue, out iCampaignAttributeStandardID))
            {
                SortedList sParams = new SortedList();
                sParams.Add("@CampaignAttributeID", iCampaignAttributeStandardID);
                Classes.cUtilities.LoadDropDownList(ddlName, "uspGetCampaignAttributeDescriptors", sParams, "DescriptorValue", "CampaignAttributeDescriptorID", "LARPortal", "JLB", "");
                if (ddlName.Items.Count > 0)
                    ddlName.SelectedIndex = 0;
                foreach (Classes.cDescriptor dDesc in Desc)
                {
                    if (dDesc.CharacterDescriptor == ddlDescriptor.SelectedItem.Text)
                    {
                        ddlName.SelectedIndex = -1;
                        foreach (ListItem Trait in ddlName.Items)
                            if (Trait.Text == dDesc.DescriptorValue)
                                Trait.Selected = true;
                            else
                                Trait.Selected = false;
                    }
                }
            }
        }

        protected void btnAddDesc_Click(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            Classes.cDescriptor NewDesc = new Classes.cDescriptor();
            NewDesc.DescriptorValue = ddlName.SelectedItem.Text;
            int CampaignAttStandardID;
            int CampaignAttDescriptorID;
            NewDesc.CharacterDescriptor = ddlDescriptor.SelectedItem.Text;

            if (int.TryParse(ddlDescriptor.SelectedValue, out CampaignAttStandardID))
                NewDesc.CampaignAttributeStandardID = CampaignAttStandardID;
            if (int.TryParse(ddlName.SelectedValue, out CampaignAttDescriptorID))
                NewDesc.CampaignAttributeDescriptorID = CampaignAttDescriptorID;
            NewDesc.CharacterAttributesBasicID = -1;
            NewDesc.RecordStatus = Classes.RecordStatuses.Active;
            NewDesc.CharacterSkillSetID = oCharSelect.CharacterInfo.CharacterSkillSetID;

            NewDesc.Save(_UserName, _UserID);
            BindDescriptors();
        }

        protected void BindDescriptors()
        {
            oCharSelect.LoadInfo();

            gvDescriptors.DataSource = null;
            gvDescriptors.DataSource = oCharSelect.CharacterInfo.Descriptors;
            gvDescriptors.DataBind();
        }

        protected void btnDeleteDesc_Click(object sender, EventArgs e)
        {
            int iDescID;
            if (int.TryParse(hidDescID.Value, out iDescID))
            {
                Classes.cDescriptor cDesc = new Classes.cDescriptor();
                cDesc.CharacterAttributesBasicID = iDescID;
                cDesc.RecordStatus = Classes.RecordStatuses.Delete;
                cDesc.Delete(_UserName, _UserID);
                _Reload = true;
            }
        }

        #endregion

        #region Actors

        protected void btnSaveActor_Click(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            List<Classes.cActor> cActors = oCharSelect.CharacterInfo.Actors;
            int iCharacterActorID;
            if (int.TryParse(hidActorID.Value, out iCharacterActorID))
            {
                Classes.cActor NewActor = new Classes.cActor();
                NewActor.CharacterActorID = iCharacterActorID;
                NewActor.CharacterID = Convert.ToInt32(hidCharacterID.Value);
                NewActor.Comments = tbActorComments.Text;
                NewActor.StaffComments = tbActorStaffComments.Text;
                DateTime dtActorStartDate;
                if (DateTime.TryParse(tbActorStartDate.Text, out dtActorStartDate))
                    NewActor.StartDate = dtActorStartDate;
                DateTime dtActorEndDate;
                if (DateTime.TryParse(tbActorEndDate.Text, out dtActorEndDate))
                    NewActor.EndDate = dtActorEndDate;
                NewActor.RecordStatus = Classes.RecordStatuses.Active;
                if (ddlActorName.SelectedIndex >= 0)
                {
                    int iActorID;
                    if (int.TryParse(ddlActorName.SelectedValue, out iActorID))
                    {
                        NewActor.loginUserName = ddlActorName.SelectedItem.Text;
                        NewActor.UserID = iActorID;
                    }
                }

                // If the record is already in the list, remove it so we don't have duplicates.
                cActors.RemoveAll(x => x.CharacterActorID == iCharacterActorID);

                cActors.Add(NewActor);

                // Now we go through and check all of the records for overlaps.
                var ActorList = cActors.OrderBy(x => x.StartDate).ToList();
                hidActorDateProblems.Value = "";

                if (ActorList.Count > 1)
                {
                    // Now we get convoluted. Go through and check each record against the next record.
                    // If the end date of the first record is null, make it 1 day less than the start
                    // of the next record.
                    // if Rec2.StartDate < Rec1.EndDate = raise problem.
                    // Screen will already not allow the end date to be before the start date.
                    for (int i = 0; i < (ActorList.Count - 1); i++)
                    {
                        // If the current record has no end date, fill it in with the date before the start of the next record.
                        if (!ActorList[i].EndDate.HasValue)
                            ActorList[i].EndDate = ActorList[i + 1].StartDate.Value.AddDays(-1);

                        if (ActorList[i + 1].StartDate < ActorList[i].EndDate)
                        {
                            hidActorDateProblems.Value = "Y";
                            lblmodalError.Text = "With this change there are actors with overlapping dates.<br>Please reenter the corrected dates.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openError();", true);
                        }
                    }
                }

                if (hidActorDateProblems.Value == "")
                    NewActor.Save(_UserID);

                _Reload = true;
            }
        }

        protected void btnDeleteActor_Click(object sender, EventArgs e)
        {
            int iCharacterActorID;
            if (int.TryParse(hidDeleteActorID.Value, out iCharacterActorID))
            {
                Classes.cActor cActor = new Classes.cActor();
                cActor.CharacterActorID = iCharacterActorID;
                cActor.RecordStatus = Classes.RecordStatuses.Delete;
                cActor.Save(_UserID);
                _Reload = true;
            }
        }

        protected void BindActors(List<Classes.cActor> Actors)
        {
            var ActorList = Actors.FindAll(x => x.RecordStatus == Classes.RecordStatuses.Active).OrderBy(x => x.StartDate).ToList();
            gvActors.DataSource = ActorList;
            gvActors.DataBind();
        }

        protected void gvActors_DataBound(object sender, EventArgs e)
        {
            if (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters)
            {
                gvActors.Columns[4].Visible = false;
                gvActors.Columns[5].Visible = false;
            }
            else
            {
                gvActors.Columns[4].Visible = true;
                gvActors.Columns[5].Visible = true;
            }
        }

        #endregion

        #region Deaths

        protected void btnSaveCharDeath_Click(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (oCharSelect.CharacterID.HasValue)
            {
                int iDeathID;
                if (int.TryParse(hidDeathID.Value, out iDeathID))
                {
                    Classes.cCharacterDeath cDeath = new Classes.cCharacterDeath();
                    cDeath.CharacterDeathID = iDeathID;
                    cDeath.CharacterID = oCharSelect.CharacterID.Value;
                    cDeath.Comments = tbDeathComments.Text;
                    DateTime dtDeathDate;
                    if (DateTime.TryParse(tbDeathDate.Text, out dtDeathDate))
                        cDeath.DeathDate = dtDeathDate;
                    cDeath.DeathPermanent = cbxDeathPerm.Checked;
                    cDeath.RecordStatus = Classes.RecordStatuses.Active;
                    cDeath.StaffComments = tbDeathStaffComments.Text;

                    cDeath.Save(_UserID);
                    _Reload = true;
                }
            }
        }

        protected void btnDeleteDeath_Click(object sender, EventArgs e)
        {
            int iDeathID;
            if (int.TryParse(hidDeleteDeathID.Value, out iDeathID))
            {
                Classes.cCharacterDeath cDeath = new Classes.cCharacterDeath();
                cDeath.CharacterDeathID = iDeathID;
                cDeath.RecordStatus = Classes.RecordStatuses.Delete;
                cDeath.Save(_UserID);
                _Reload = true;
            }
        }

        protected void BindDeaths(List<Classes.cCharacterDeath> Deaths)
        {
            var DeathList = Deaths.FindAll(x => x.RecordStatus == Classes.RecordStatuses.Active).OrderBy(x => x.DeathDate).ToList();

            gvDeaths.DataSource = DeathList;
            gvDeaths.DataBind();
        }

        protected void gvDeaths_DataBound(object sender, EventArgs e)
        {
            if (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters)
            {
                gvDeaths.Columns[3].Visible = false;
                gvDeaths.Columns[4].Visible = false;
            }
            else
            {
                gvDeaths.Columns[3].Visible = true;
                gvDeaths.Columns[4].Visible = true;
            }
        }

        #endregion
    }
}
