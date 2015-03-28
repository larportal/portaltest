using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
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
                //tbName.Attributes.Add("Placeholder", "Character Name");
                //tbStatus.Attributes.Add("Placeholder", "Status");
                //tbAKA.Attributes.Add("Placeholder", "Alias");
                //tbLastEvent.Attributes.Add("Placeholder", "Last Events");
                //tbDOB.Attributes.Add("Placeholder", "Game System");
                //tbNumDeaths.Attributes.Add("Placeholder", "# deaths");
                //tbOrigin.Attributes.Add("Placeholder", "Origin");
                //tbDOD.Attributes.Add("Placeholder", "DOD");
                //tbHome.Attributes.Add("Placeholder", "Home");
                //tbRace.Attributes.Add("Placeholder", "Race");
                //tbTeam.Attributes.Add("Placeholder", "Team USA");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Character Descriptor = chcampaignattributedescriptors
            // Name = chcampaignAttributesStandard  where  CampaignAttributeStandardID = CampaignAttributeID
            // CampaignAttributesStandard - AllowMultipleSelections

            // To get skill set:
            // select * from CHCharacterSkillSets - get for the user.
            // select * from CHCharacterSkillsStandard get what's actually in the skill set
            //      where CharacterSkillSetID = 111   key of CHCharacterSkillSets

            //            pnlCharacterPicture.Visible = false;


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
                        cChar.LoadCharacter(iCharID);

                        lblHeader.Text = "Character Info - " + cChar.AKA + " - " + cChar.CampaignName;

                        ViewState["CharDesc"] = cChar.Descriptors;
                        ViewState["ProfilePictureID"] = cChar.ProfilePictureID;

                        tbFirstName.Text = cChar.FirstName;
                        tbMiddleName.Text = cChar.MiddleName;
                        tbLastName.Text = cChar.LastName;

                        tbOrigin.Text = cChar.CurrentHome;
                        //tbStatus.Text = cChar.Status.StatusName;
                        tbAKA.Text = cChar.AKA;
                        tbHome.Text = cChar.CurrentHome;
                        tbDateLastEvent.Text = "??";
                        tbType.Text = cChar.CharType.Description;
                        tbTeam.Text = "Team";
                        tbNumOfDeaths.Text = cChar.Deaths.Count.ToString();
                        tbDOB.Text = cChar.DateOfBirth;
                        //                        tbRace.Text = cChar.Race.Description;
                        if (cChar.Deaths.Count > 0)
                        {
                            Classes.cCharacterDeath LastDeath = cChar.Deaths.OrderByDescending(t => t.DeathDate).First();
                            tbDOD.Text = LastDeath.DeathDate.Value.ToShortDateString();
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

                        //if (cChar.CharacterPhoto.Length > 0)
                        //{
                        //    Classes.cPicture ProfilePicture = new Classes.cPicture();

                        if (cChar.ProfilePicture != null)
                        {
                            //Classes.cPicture ProfilePicture = new Classes.cPicture();
                            //ProfilePicture.PictureID = cChar.ProfilePictureID;
                            //ProfilePicture.Load(cChar.ProfilePictureID, Session["UserID"].ToString());

                            imgCharacterPicture.ImageUrl = cChar.ProfilePicture.PictureURL;
                            pnlCharacterPicture.Visible = true;
                        }
                        else
                            pnlCharacterPicture.Visible = false;

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
                                liStatus.Selected = true;
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

        //protected void btnAddRelationship_Click(object sender, EventArgs e)
        //{

        //}

        protected void btnSavePicture_Click(object sender, EventArgs e)
        {
            if (ulFile.HasFile)
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

                imgCharacterPicture.ImageUrl = NewPicture.PictureURL;
                pnlCharacterPicture.Visible = true;
            }
        }

        protected void btnClearPicture_Click(object sender, EventArgs e)
        {
            if (ViewState["UserIDPicture"] != null)
                ViewState.Remove("UserIDPicture");
            pnlCharacterPicture.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int iTemp;

            if (Session["SelectedCharacter"] != null)
            {
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

                    cChar.DateOfBirth = tbDOB.Text;
                    if (ViewState["UserIDPicture"] != null)
                        cChar.ProfilePicture = ViewState["UserIDPicture"] as Classes.cPicture;
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
                        Item.CharacterSkillSetID = cChar.CharacterSkillSetID;

                    cChar.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);

                    //tbRace.Text = cChar.Race.Description;
                    //if (cChar.Deaths.Count > 0)
                    //{
                    //    Classes.cCharacterDeath LastDeath = cChar.Deaths.OrderByDescending(t => t.DeathDate).First();
                    //    tbDOD.Text = LastDeath.DeathDate.Value.ToShortDateString();
                    //}

                    //if (cChar.Deaths.Count > 0)
                    //    if ( cChar.Deaths[0].DeathDate.HasValue )
                    //        tbDOD.Text = cChar.Deaths[0].DeathDate.Value.ToShortDateString();
                    //tbHome.Text = cChar.CurrentHome;
                    //tbCharName.Text = cChar.LastName;
                    //tbNumOfDeaths.Text = cChar.Deaths.Count().ToString();
                    //tbOrigin.Text = cChar.WhereFrom;
                    //tbRace.Text = cChar.Race.Description;

                    //DataTable dtPictures = new DataTable();
                    //dtPictures.Columns.Add("PictureID", typeof(Int32));
                    //dtPictures.Columns.Add("FileName", typeof(String));

                    //foreach (Classes.cPicture Pict in cChar.Pictures)
                    //{
                    //    DataRow dPict = dtPictures.NewRow();
                    //    dPict["PictureID"] = Pict.PictureID;
                    //    string sFileName = "/Pictures/" + Pict.PictureFileName;
                    //    dPict["FileName"] = sFileName;
                    //    string sFinalFileName = Server.MapPath(sFileName);
                    //    if ( File.Exists(sFinalFileName))
                    //        dtPictures.Rows.Add(dPict);
                    //}

                    //rptPictures.DataSource = dtPictures;
                    //rptPictures.DataBind();

                    //DataTable dtRelationship = new DataTable();

                    //DataTable dtTraits = new DataTable();
                    //dtTraits.Columns.Add(new DataColumn("ID", typeof(Int32)));
                    //dtTraits.Columns.Add(new DataColumn("CharacterDescriptor", typeof(String)));
                    //dtTraits.Columns.Add(new DataColumn("Name", typeof(string)));
                    //dtTraits.Columns.Add(new DataColumn("Description", typeof(string)));

                    //foreach ( Classes.cCharacterSkill cSkill in cChar.CharacterSkills )
                    //{
                    //    DataRow dNewRow = dtTraits.NewRow();
                    //    dNewRow["ID"] = cSkill.CampaignSkillsStandardID;
                    //    dNewRow["CharacterDescriptor"] = cSkill.CampaignSkillsStandardID.ToString();
                    //    dNewRow["Name"] = cSkill.SkillName;
                    //    dtTraits.Rows.Add(dNewRow);
                    //}

                    //gvDescriptors.DataSource = dtTraits;
                    //gvDescriptors.DataBind();

                    //DataTable dtPictures = new DataTable();
                    //dtPictures.Columns.Add(new DataColumn("Picture", typeof(string)));

                    //DataRow dPictureRow = dtPictures.NewRow();
                    //dPictureRow["Picture"] = "../img/larpPortal.jpg";
                    //dtPictures.Rows.Add(dPictureRow);

                    //gvPictures.DataSource = dtPictures;
                    //gvPictures.DataBind();
                    //}

                }
            }
        }

        protected void ddlDescriptor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCampaignAttributeStandardID;

            List<Classes.cDescriptor> Desc = new List<Classes.cDescriptor>();
            Desc = ViewState["CharDesc"] as List<Classes.cDescriptor>;

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
            DataView dvCharDescriptors = new DataView(dtCharDescriptors, "RecordStatus = 0 "
                
                //+ key.ToString()
                , "", DataViewRowState.CurrentRows);

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

            if (int.TryParse(ddlDescriptor.SelectedValue, out CampaignAttStandardID))
                NewDesc.CampaignAttributeStandardID = CampaignAttStandardID;
            if (int.TryParse(ddlName.SelectedValue, out CampaignAttDescriptorID))
                NewDesc.CampaignAttributeDescriptorID = CampaignAttDescriptorID;
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
    }
}
