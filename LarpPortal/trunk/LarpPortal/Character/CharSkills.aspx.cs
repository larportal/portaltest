using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharSkills : System.Web.UI.Page
    {
        public string PictureDirectory = "../Pictures";
        protected DataTable _dtSkills = new DataTable();

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


//            FrameSkills.Src = "CharSkill.aspx";

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

                        DataTable dtSkills = new DataTable();
                        dtSkills.Columns.Add(new DataColumn("CharacterSkillsStandardID", typeof(Int32)));

                        dtSkills.Columns.Add(new DataColumn("SkillSetName", typeof(string)));
                        dtSkills.Columns.Add(new DataColumn("StatusName", typeof(string)));
                        dtSkills.Columns.Add(new DataColumn("SkillSetTypeDescription", typeof(string)));
                        dtSkills.Columns.Add(new DataColumn("SkillName", typeof(string)));
                        dtSkills.Columns.Add(new DataColumn("SkillShortDescription", typeof(string)));
                        dtSkills.Columns.Add(new DataColumn("SkillLongDescription", typeof(string)));
                        dtSkills.Columns.Add(new DataColumn("CampaignSkillsStandardComments", typeof(string)));
                        dtSkills.Columns.Add(new DataColumn("SkillTypeDescription", typeof(string)));
                        dtSkills.Columns.Add(new DataColumn("SkillTypeComments", typeof(string)));

                        foreach (Classes.cCharacterSkill Skill in cChar.CharacterSkills)
                        {
                            DataRow dSkillRow = dtSkills.NewRow();
                            dSkillRow["CharacterSkillsStandardID"] = Skill.CharacterSkillsStandardID;
                            dSkillRow["SkillSetName"] = Skill.SkillSetName;
                            dSkillRow["StatusName"] = Skill.StatusName;
                            dSkillRow["SkillSetTypeDescription"] = Skill.SkillSetTypeDescription;
                            dSkillRow["SkillName"] = Skill.SkillName;
                            dSkillRow["SkillShortDescription"] = Skill.SkillShortDescription;
                            dSkillRow["SkillLongDescription"] = Skill.SkillLongDescription;
                            dSkillRow["CampaignSkillsStandardComments"] = Skill.CampaignSkillsStandardComments;
                            dSkillRow["SkillTypeDescription"] = Skill.SkillTypeDescription;
                            dSkillRow["SkillTypeComments"] = Skill.SkillTypeComments;

                            dtSkills.Rows.Add(dSkillRow);
                        }
                        //gvSkills.DataSource = dtSkills;
                        //gvSkills.DataBind();

                        DataSet dsSkillSets = new DataSet();
                        SortedList sParam = new SortedList();
                        sParam.Add("@CampaignID", 1);
                        dsSkillSets = Classes.cUtilities.LoadDataSet("uspGetCampaignSkills", sParam, "LARPortal", Session["LoginName"].ToString(), "");

                        _dtSkills = dsSkillSets.Tables[1];
                        //TreeNode MainNode = new TreeNode("Skills");
                        //tvSkills.NodeIndent = 0;

                        //DataView dvTopNodes = new DataView(_dtSkills, "PreRequisiteSkillID is null", "", DataViewRowState.CurrentRows);
                        //foreach (DataRowView dvRow in dvTopNodes)
                        //{
                        //    TreeNode NewNode = new TreeNode(dvRow["SkillName"].ToString());
                        //    NewNode.SelectAction = TreeNodeSelectAction.Expand;
                        //    int iNodeID;
                        //    if (int.TryParse(dvRow["CampaignSkillsStandardID"].ToString(), out iNodeID))
                        //    {
                        //        NewNode.Text += " " + iNodeID.ToString();
                        //        PopulateTreeView(iNodeID, NewNode);
                        //    }
                        //    tvSkills.Nodes.Add(NewNode);
                        //}
                    }

                    ViewState["CurrentCharacter"] = Session["SelectedCharacter"];
                }
            }
        }




        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            DataView dvChild = new DataView(_dtSkills, "PreRequisiteSkillID = " + parentId.ToString(), "SkillName", DataViewRowState.CurrentRows);
            foreach (DataRowView dr in dvChild)
            {
                int iNodeID;
                TreeNode childNode;
                if (int.TryParse(dr["CampaignSkillsStandardID"].ToString(), out iNodeID))
                {
                    childNode = new TreeNode(dr["SkillName"].ToString() + " NodeID = " + iNodeID + ", ParentID = " + parentId.ToString());
                    childNode.SelectAction = TreeNodeSelectAction.Expand;
                    PopulateTreeView(iNodeID, childNode);
                    parentNode.ChildNodes.Add(childNode);
                }
            }
        }




        protected void btnAddRelationship_Click(object sender, EventArgs e)
        {

        }

        protected void btnSavePicture_Click(object sender, EventArgs e)
        {
            //if (ulFile.HasFile)
            //{
            //    string sUser = Session["LoginName"].ToString();
            //    Classes.cPicture NewPicture = new Classes.cPicture();
            //    NewPicture.CreateNewPictureRecord(sUser);
            //    string sExtension = Path.GetExtension(ulFile.FileName);
            //    string filename = "CP" + NewPicture.PictureID.ToString("D10") + sExtension;

            //    if (!Directory.Exists(PictureDirectory))
            //        Directory.CreateDirectory(PictureDirectory);

            //    string FinalFileName = Path.Combine(Server.MapPath(PictureDirectory), filename);
            //    ulFile.SaveAs(FinalFileName);

            //    NewPicture.PictureFileName = filename;
            //    NewPicture.Save(sUser);

            //    ViewState["UserIDPicture"] = filename;

            //    imgCharacterPicture.ImageUrl = "../Pictures/" + filename;
            //    imgCharacterPicture.Visible = true;
            //    btnClearPicture.Visible = true;
            //}
        }

        protected void btnClearPicture_Click(object sender, EventArgs e)
        {
            //if (ViewState["UserIDPicture"] != null)
            //    ViewState.Remove("UserIDPicture");
            //imgCharacterPicture.Visible = false;
            //btnClearPicture.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (Session["SelectedCharacter"] != null)
            //{
            //    int iCharID;
            //    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
            //    {
            //        Classes.cCharacter cChar = new Classes.cCharacter();
            //        cChar.LoadCharacter(iCharID);

            //        cChar.FirstName = tbCharacter.Text;
            //        cChar.CurrentHome = tbOrigin.Text;
            //        cChar.Status.StatusName = tbStatus.Text;
            //        cChar.AKA = tbAKA.Text;
            //        cChar.CurrentHome = tbHome.Text;


            //        //tbAKA.Text = cChar.AKA;
            //        //tbHome.Text = cChar.CurrentHome;
            //        //tbDateLastEvent.Text = "??";
            //        //tbType.Text = cChar.CharType.Description;
            //        //tbTeam.Text = "Team";
            //        //tbNumOfDeaths.Text = cChar.Deaths.Count.ToString();

            //        cChar.DateOfBirth = tbDOB.Text;
            //        if (ViewState["UserIDPicture"] != null)
            //            cChar.CharacterPhoto = ViewState["UserIDPicture"].ToString();
            //        else
            //            cChar.CharacterPhoto = "";

            //        cChar.SaveCharacter(Session["LoginName"].ToString());

            //        //tbRace.Text = cChar.Race.Description;
            //        //if (cChar.Deaths.Count > 0)
            //        //{
            //        //    Classes.cCharacterDeath LastDeath = cChar.Deaths.OrderByDescending(t => t.DeathDate).First();
            //        //    tbDOD.Text = LastDeath.DeathDate.Value.ToShortDateString();
            //        //}

            //        //if (cChar.Deaths.Count > 0)
            //        //    if ( cChar.Deaths[0].DeathDate.HasValue )
            //        //        tbDOD.Text = cChar.Deaths[0].DeathDate.Value.ToShortDateString();
            //        //tbHome.Text = cChar.CurrentHome;
            //        //tbCharName.Text = cChar.LastName;
            //        //tbNumOfDeaths.Text = cChar.Deaths.Count().ToString();
            //        //tbOrigin.Text = cChar.WhereFrom;
            //        //tbRace.Text = cChar.Race.Description;

            //        //DataTable dtPictures = new DataTable();
            //        //dtPictures.Columns.Add("PictureID", typeof(Int32));
            //        //dtPictures.Columns.Add("FileName", typeof(String));

            //        //foreach (Classes.cPicture Pict in cChar.Pictures)
            //        //{
            //        //    DataRow dPict = dtPictures.NewRow();
            //        //    dPict["PictureID"] = Pict.PictureID;
            //        //    string sFileName = "/Pictures/" + Pict.PictureFileName;
            //        //    dPict["FileName"] = sFileName;
            //        //    string sFinalFileName = Server.MapPath(sFileName);
            //        //    if ( File.Exists(sFinalFileName))
            //        //        dtPictures.Rows.Add(dPict);
            //        //}

            //        //rptPictures.DataSource = dtPictures;
            //        //rptPictures.DataBind();

            //        //DataTable dtRelationship = new DataTable();

            //        //DataTable dtTraits = new DataTable();
            //        //dtTraits.Columns.Add(new DataColumn("ID", typeof(Int32)));
            //        //dtTraits.Columns.Add(new DataColumn("CharacterDescriptor", typeof(String)));
            //        //dtTraits.Columns.Add(new DataColumn("Name", typeof(string)));
            //        //dtTraits.Columns.Add(new DataColumn("Description", typeof(string)));

            //        //foreach ( Classes.cCharacterSkill cSkill in cChar.CharacterSkills )
            //        //{
            //        //    DataRow dNewRow = dtTraits.NewRow();
            //        //    dNewRow["ID"] = cSkill.CampaignSkillsStandardID;
            //        //    dNewRow["CharacterDescriptor"] = cSkill.CampaignSkillsStandardID.ToString();
            //        //    dNewRow["Name"] = cSkill.SkillName;
            //        //    dtTraits.Rows.Add(dNewRow);
            //        //}

            //        //gvDescriptors.DataSource = dtTraits;
            //        //gvDescriptors.DataBind();

            //        //DataTable dtPictures = new DataTable();
            //        //dtPictures.Columns.Add(new DataColumn("Picture", typeof(string)));

            //        //DataRow dPictureRow = dtPictures.NewRow();
            //        //dPictureRow["Picture"] = "../img/larpPortal.jpg";
            //        //dtPictures.Rows.Add(dPictureRow);

            //        //gvPictures.DataSource = dtPictures;
            //        //gvPictures.DataBind();
            //        //}

            //    }
            //}
        }

        protected void ddlDescriptor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int iCampaignAttributeStandardID;

            //List<Classes.cDescriptor> Desc = new List<Classes.cDescriptor>();
            //Desc = ViewState["CharDesc"] as List<Classes.cDescriptor>;

            //if (int.TryParse(ddlDescriptor.SelectedValue, out iCampaignAttributeStandardID))
            //{
            //    SortedList sParams = new SortedList();
            //    sParams.Add("@CampaignAttributeID", iCampaignAttributeStandardID);
            //    Classes.cUtilities.LoadDropDownList(ddlName, "uspGetCampaignAttributeDescriptors", sParams, "DescriptorValue", "CampaignAttributeDescriptorID", "LARPortal", "JLB", "");
            //    if (ddlName.Items.Count > 0)
            //        ddlName.SelectedIndex = 0;
            //    foreach (Classes.cDescriptor dDesc in Desc)
            //    {
            //        if (dDesc.CharacterDescriptor == ddlDescriptor.SelectedItem.Text)
            //        {
            //            ddlName.SelectedIndex = -1;
            //            foreach (ListItem Trait in ddlName.Items)
            //                if (Trait.Text == dDesc.DescriptorValue)
            //                    Trait.Selected = true;
            //                else
            //                    Trait.Selected = false;
            //        }
            //    }
            //}
        }
    }
}













            //<%--            <asp:UpdatePanel ID="upNonCost" runat="server">
            //    <ContentTemplate>


            //        <div class="col-sm-12">
            //            <div class="row">
            //                <label for="ddlDescriptor" class="control-label col-sm-2">Name</label>
            //                <div class="col-sm-3">
            //                    <asp:DropDownList ID="ddlName" runat="server" CssClass="form-control" />
            //                </div>

            //            </div>
            //        </div>

            //        <div class="col-sm-12">
            //            <div class="row">
            //                <label for="tbDateAdded" class="control-label col-sm-2">Date Added</label>
            //                <div class="col-sm-2">
            //                    <asp:TextBox ID="tbDateAdded" runat="server" CssClass="form-control" />
            //                </div>
            //            </div>
            //        </div>
            //    </ContentTemplate>
            //</asp:UpdatePanel>

            //<div class="col-sm-12">
            //    <div class="row">
            //        <label for="tbDateAdded" class="control-label col-sm-10">&nbsp;</label>
            //        <div class="col-sm-2">
            //            <asp:Button ID="btnSave" runat="server" Text="&nbsp;&nbsp;Save&nbsp;&nbsp;" class="btn btn-default" OnClick="btnSave_Click" />
            //        </div>
            //    </div>
            //</div>--%>
    