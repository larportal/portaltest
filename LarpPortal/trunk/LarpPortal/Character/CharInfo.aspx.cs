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
        public string PictureDirectory = "../Pictures";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["CurrentCharacter"] = "";
                tbFirstName.Attributes.Add("Placeholder", "First Name");
                tbLastName.Attributes.Add("Placeholder", "Last Name");
                lblMessage.Text = "";
                ViewState["NewRecCounter"] = -1;

                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", "CharacterMaster.Page_Load");
                ddlCharacterSelector.DataTextField = "CharacterAKA";
                ddlCharacterSelector.DataValueField = "CharacterID";
                ddlCharacterSelector.DataSource = dtCharacters;
                ddlCharacterSelector.DataBind();

                if (Session["SelectedCharacter"] == null)
                {
                    if (dtCharacters.Rows.Count > 0)
                    {
                        int iCharacterID = 0;
                        if (int.TryParse(dtCharacters.Rows[0]["LastLoggedInCharacter"].ToString(), out iCharacterID))
                            Session["SelectedCharacter"] = iCharacterID;
                    }
                }

                if (ddlCharacterSelector.Items.Count > 0)
                {
                    ddlCharacterSelector.ClearSelection();

                    if (Session["SelectedCharacter"] != null)
                    {
                        DataRow[] drValue = dtCharacters.Select("CharacterID = " + Session["SelectedCharacter"].ToString());
                        foreach (DataRow dRow in drValue)
                        {
                            DateTime DateChanged;
                            if (DateTime.TryParse(dRow["DateChanged"].ToString(), out DateChanged))
                                lblUpdateDate.Text = DateChanged.ToShortDateString();
                            else
                                lblUpdateDate.Text = "Unknown";
                            lblCampaign.Text = dRow["CampaignName"].ToString();
                        }
                        string sCurrentUser = Session["SelectedCharacter"].ToString();
                        foreach (ListItem liAvailableUser in ddlCharacterSelector.Items)
                        {
                            if (sCurrentUser == liAvailableUser.Value)
                                liAvailableUser.Selected = true;
                            else
                                liAvailableUser.Selected = false;
                        }
                    }

                    if (ddlCharacterSelector.SelectedIndex == 0)
                    {
                        ddlCharacterSelector.Items[0].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    }
                    ddlCharacterSelector.Items.Add(new ListItem("Add a new character", "-1"));
                }
                else
                    Response.Redirect("CharAdd.aspx");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["SelectedCharacter"] != null)
            {
                string sCurrent = ViewState["CurrentCharacter"].ToString();
                string sSelected = Session["SelectedCharacter"].ToString();
                if ((!IsPostBack) || (ViewState["CurrentCharacter"].ToString() != Session["SelectedCharacter"].ToString()))
                {
                    int iCharID;
                    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                    {
                        Classes.cCharacter cChar = new Classes.cCharacter();
                        DateTime dStart = DateTime.Now;
                        cChar.LoadCharacter(iCharID);
                        DateTime dEnd = DateTime.Now;

                        double num = (dEnd - dStart).TotalMilliseconds;

                        if (cChar.CampaignID == 0)
                            Response.Redirect("CharNoCampaign.aspx", true);

                        //lblHeader.Text = "Character Info - " + cChar.AKA + " - " + cChar.CampaignName;

                        Session["CharDesc"] = cChar.Descriptors;
                        ViewState["ProfilePictureID"] = cChar.ProfilePictureID;

                        tbFirstName.Text = cChar.FirstName;
                        tbMiddleName.Text = cChar.MiddleName;
                        tbLastName.Text = cChar.LastName;

                        tbOrigin.Text = cChar.CurrentHome;
                        //tbStatus.Text = cChar.Status.StatusName;
                        tbAKA.Text = cChar.AKA;
                        tbHome.Text = cChar.CurrentHome;
                        tbDateLastEvent.Text = "??";
                        //                        tbType.Text = cChar.CharType.Description;
                        tbType.Text = cChar.CharType.Description;

                        if (cChar.Teams.Count == 0)
                        {
                            ddlTeamList.Visible = false;
                            tbTeam.Visible = true;
                            tbTeam.Text = "No Teams";
                        }
                        else if (cChar.Teams.Count == 1)
                        {
                            ddlTeamList.Visible = false;
                            tbTeam.Visible = true;
                            tbTeam.Text = cChar.Teams[0].TeamName;
                        }
                        else
                        {
                            ddlTeamList.Visible = true;
                            tbTeam.Visible = false;
                            ddlTeamList.DataSource = cChar.Teams;
                            ddlTeamList.DataTextField = "TeamName";
                            ddlTeamList.DataValueField = "TeamID";
                            ddlTeamList.DataBind();

                            ddlTeamList.ClearSelection();

                            foreach (ListItem litem in ddlTeamList.Items)
                            {
                                litem.Selected = false;
                                if (litem.Value == cChar.TeamID.ToString())
                                    litem.Selected = true;
                            }
                            if (ddlTeamList.SelectedIndex < 0)
                                ddlTeamList.SelectedIndex = 0;
                        }
                        //tbTeam.Text = "Team";
                        //lblTeam.Text = cChar.TeamName;
                        tbNumOfDeaths.Text = cChar.Deaths.Count.ToString();
                       // lblNumOfDeaths.Text = cChar.Deaths.Count.ToString();
                        tbDOB.Text = cChar.DateOfBirth;
                        //                        tbRace.Text = cChar.Race.Description;
                        if (cChar.Deaths.Count > 0)
                        {
                            Classes.cCharacterDeath LastDeath = cChar.Deaths.OrderByDescending(t => t.DeathDate).First();
                            tbDOD.Text = LastDeath.DeathDate.Value.ToShortDateString();
                            lblDOD.Text = LastDeath.DeathDate.Value.ToShortDateString();
                        }

                        tbAKA.Text = cChar.AKA;
                        tbDOB.Text = cChar.DateOfBirth;
                        //if (cChar.Deaths.Count > 0)
                        //    if (cChar.Deaths[0].DeathDate.HasValue)
                        //        tbDOD.Text = cChar.Deaths[0].DeathDate.Value.ToShortDateString();
                        tbHome.Text = cChar.CurrentHome;
                        tbNumOfDeaths.Text = cChar.Deaths.Count().ToString();
                        tbOrigin.Text = cChar.WhereFrom;
                        //                        tbRace.Text = cChar.Race.Description;

                        DataTable dtCharDescriptors = new DataTable();
                        dtCharDescriptors = Classes.cUtilities.CreateDataTable(cChar.Descriptors);
                        Session["CharDescriptors"] = cChar.Descriptors;
                        BindData();

                        if (cChar.ProfilePicture != null)
                        {
                            ViewState["UserIDPicture"] = cChar.ProfilePicture;
                            imgCharacterPicture.ImageUrl = cChar.ProfilePicture.PictureURL;
                            imgCharacterPicture.Visible = true;
                            btnClearPicture.Visible = true;
                        }
                        else
                        {
                            imgCharacterPicture.Visible = false;
                            btnClearPicture.Visible = false;
                        }

                        Classes.cCampaignRaces Races = new Classes.cCampaignRaces();
                        Races.CampaignID = cChar.CampaignID;
                        Races.Load(Session["LoginName"].ToString());
                        DataTable dtRaces = Classes.cUtilities.CreateDataTable(Races.RaceList);
                        ddlRace.DataSource = dtRaces;
                        ddlRace.DataTextField = "FullRaceName";
                        ddlRace.DataValueField = "CampaignRaceID";
                        ddlRace.DataBind();
                        ddlRace.SelectedIndex = 0;
                        foreach (ListItem dItems in ddlRace.Items)
                        {
                            if (dItems.Value == cChar.Race.CampaignRaceID.ToString())
                                dItems.Selected = true;
                            else
                                dItems.Selected = false;
                        }

                        //ToDo JLB Character Status.
                        DataTable dtStatus = new DataTable();

                        using (SqlConnection connPortal = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
                        {
                            connPortal.Open();
                            using (SqlCommand CmdGetStatus = new SqlCommand("select * from MDBStatus", connPortal))
                            {
                                SqlDataAdapter SDAGetStatus = new SqlDataAdapter(CmdGetStatus);
                                SDAGetStatus.Fill(dtStatus);
                            }
                        }

                        DataView dvCharStatus = new DataView(dtStatus, "StatusType = 'Character'", "StatusName", DataViewRowState.CurrentRows);
                        ddlStatus.DataSource = dvCharStatus;
                        ddlStatus.DataTextField = "StatusName";
                        ddlStatus.DataValueField = "StatusID";
                        ddlStatus.DataBind();

                        ddlStatus.SelectedIndex = -1;
                        foreach (ListItem liStatus in ddlStatus.Items)
                        {
                            if (liStatus.Value == cChar.Status.StatusID.ToString())
                            {
                                liStatus.Selected = true;
                                lblStatus.Text = liStatus.Text;
                            }
                            else
                                liStatus.Selected = false;
                        }

                        MethodBase lmth = MethodBase.GetCurrentMethod();
                        string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                        SortedList sParam = new SortedList();
                        sParam.Add("@CampaignID", cChar.CampaignID);
                        DataTable dtDescriptors = Classes.cUtilities.LoadDataTable("uspGetCampaignAttributesStandard",
                            sParam, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

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
                    }
                    ViewState["CurrentCharacter"] = Session["SelectedCharacter"];
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

                    int iCharacterID = 0;
                    int.TryParse(ViewState["CurrentCharacter"].ToString(), out iCharacterID);
                    NewPicture.CharacterID = iCharacterID;

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
            //                ViewState.Remove("UserIDPicture");
            imgCharacterPicture.Visible = false;
            btnClearPicture.Visible = false;

            SortedList sParam = new SortedList();
            sParam.Add("@CharacterID", Session["SelectedCharacter"].ToString());

            Classes.cUtilities.LoadDataTable("uspClearCharacterProfilePicture", sParam, "LARPortal", Session["UserID"].ToString(), "CharInfo.btnClearPicture"); 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int iTemp;

            if (Session["SelectedCharacter"] != null)
            {
                if ((tbAKA.Text.Length == 0) &&
                    (tbFirstName.Text.Length == 0))
                {
                    // JBradshaw  7/11/2016    Request #1286     Must have at least first name or last name.
                    lblmodalError.Text = "You must fill in at least the first name or the character AKA.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openError();", true);
                    return;
                }

                DataTable dtDesc = Session["CharDescriptors"] as DataTable;

                int iCharID;
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    Classes.cCharacter cChar = new Classes.cCharacter();
                    cChar.LoadCharacter(iCharID);

                    cChar.FirstName = tbFirstName.Text;
                    cChar.MiddleName = tbMiddleName.Text;
                    cChar.LastName = tbLastName.Text;

                    cChar.CurrentHome = tbOrigin.Text;
                    // TODO JLB   cChar.Status.StatusName = tbStatus.Text;
                    //                  cChar.Status.StatusID = Convert.ToInt32(ddlStatus.SelectedValue);
                    cChar.CharacterStatusID = Convert.ToInt32(ddlStatus.SelectedValue);

                    cChar.AKA = tbAKA.Text;
                    cChar.CurrentHome = tbHome.Text;
                    cChar.WhereFrom = tbOrigin.Text;

                    //tbType.Text = cChar.CharType.Description;
                    //tbTeam.Text = "Team";

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

                    cChar.Descriptors = Session["CharDescriptors"] as List<Classes.cDescriptor>;
                    foreach (Classes.cDescriptor Item in cChar.Descriptors)
                    {
                        Item.CharacterSkillSetID = cChar.CharacterSkillSetID;
                        // Put in check for negative values. Replace anything less than 0 with -1 so it will be updated/added. JBradshaw  4/20/15
                        if (Item.CharacterAttributesBasicID < 0)
                            Item.CharacterAttributesBasicID = -1;
                    }

                    cChar.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);

                    // JBradshaw  7/11/2016    Request #1286     Changed over to bootstrap popup.
                    lblmodalMessage.Text = "Character " + cChar.AKA + " has been saved.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMessage();", true);
                }
            }
        }

        protected void ddlDescriptor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCampaignAttributeStandardID;

            List<Classes.cDescriptor> Desc = new List<Classes.cDescriptor>();
            Desc = Session["CharDesc"] as List<Classes.cDescriptor>;

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

        //protected void gvDescriptors_RowEditing(object sender, GridViewEditEventArgs e)
        //{

        //}

        protected void gvDescriptors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //    int index = Convert.ToInt32(e.RowIndex);
            //    List<Classes.cDescriptor> Desc = Session["CharDescriptors"] as List<Classes.cDescriptor>;
            //    Desc[index].RecordStatus = Classes.RecordStatuses.Delete;
            //    Session["CharDescriptors"] = Desc;
            //    BindData();
        }


        protected void BindData()
        {
            uint key = 0;
            try
            {
                key = (uint)(Classes.RecordStatuses)Enum.Parse(typeof(Classes.RecordStatuses), Classes.RecordStatuses.Delete.ToString());
            }
            catch (ArgumentException)
            {
                //unknown string or s is null
            }

            List<Classes.cDescriptor> Desc = Session["CharDescriptors"] as List<Classes.cDescriptor>;

            DataTable dtCharDescriptors = new DataTable();
            dtCharDescriptors = Classes.cUtilities.CreateDataTable(Desc);
            DataView dvCharDescriptors = new DataView(dtCharDescriptors, "RecordStatus = 0 ", "", DataViewRowState.CurrentRows);

            gvDescriptors.DataSource = null;
            gvDescriptors.DataSource = dvCharDescriptors;
            gvDescriptors.DataBind();
        }

        protected void btnAddDesc_Click(object sender, EventArgs e)
        {
            List<Classes.cDescriptor> Desc = Session["CharDescriptors"] as List<Classes.cDescriptor>;
            Classes.cDescriptor NewDesc = new Classes.cDescriptor();
            NewDesc.DescriptorValue = ddlName.SelectedItem.Text;
            int CampaignAttStandardID;
            int CampaignAttDescriptorID;
            NewDesc.CharacterDescriptor = ddlDescriptor.SelectedItem.Text;

            int NewRecCounter = Convert.ToInt32(ViewState["NewRecCounter"]);
            NewRecCounter--;
            ViewState["NewRecCounter"] = NewRecCounter;

            if (int.TryParse(ddlDescriptor.SelectedValue, out CampaignAttStandardID))
                NewDesc.CampaignAttributeStandardID = CampaignAttStandardID;
            if (int.TryParse(ddlName.SelectedValue, out CampaignAttDescriptorID))
                NewDesc.CampaignAttributeDescriptorID = CampaignAttDescriptorID;
            NewDesc.CharacterAttributesBasicID = NewRecCounter;
            NewDesc.RecordStatus = Classes.RecordStatuses.Active;

            Desc.Add(NewDesc);
            Session["CharDescriptors"] = Desc;
            BindData();
        }

        protected void gvDescriptors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToUpper() == "DELETEDESC")
            {
                int iValueToDelete;
                if (int.TryParse(e.CommandArgument.ToString(), out iValueToDelete))
                {
                    List<Classes.cDescriptor> Desc = Session["CharDescriptors"] as List<Classes.cDescriptor>;
                    var FoundList = Desc.Find(x => x.CharacterAttributesBasicID == iValueToDelete);
                    if (FoundList != null)
                        FoundList.RecordStatus = Classes.RecordStatuses.Delete;
                    Session["CharDescriptors"] = Desc;
                    BindData();
                }
            }
        }

        protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCharacterSelector.SelectedValue == "-1")
                Response.Redirect("CharAdd.aspx");

            if (Session["SelectedCharacter"].ToString() != ddlCharacterSelector.SelectedValue)
            {
                Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;

                // Save the character so it will be the last logged in character.
                int iLoggedInChar = 0;
                if (int.TryParse(ddlCharacterSelector.SelectedValue, out iLoggedInChar))
                {
                    Classes.cUser UserInfo = new Classes.cUser(Session["UserName"].ToString(), "PasswordNotNeeded");
                    UserInfo.LastLoggedInCharacter = iLoggedInChar;
                    UserInfo.Save();
                }

                Response.Redirect("CharInfo.aspx");
            }
        }

        protected void btnCloseMessage_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeMessage();", true);
        }

        protected void btnCloseError_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeError();", true);
        }
    }
}
