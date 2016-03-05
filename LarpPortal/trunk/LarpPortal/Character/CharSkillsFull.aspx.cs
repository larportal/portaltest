using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Character
{
    public partial class CharSkillsFull : System.Web.UI.Page
    {
        public string PictureDirectory = "../Pictures";
        protected DataTable _dtCampaignSkills = new DataTable();
        private string AlertMessage = "";
        DataTable dtCharSkills = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //                LogWriter oLog = new LogWriter();
                //                oLog.AddLogMessage("Starting page load for CharSkillsFull", "CharSkillsFull.Page_Load", "", Session.SessionID);
                tvSkills.Attributes.Add("onclick", "postBackByObject()");

                ViewState["CurrentCharacter"] = "";

                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", "CharacterMaster.Page_Load");
                ddlCharacterSelector.DataTextField = "CharacterAKA";
                ddlCharacterSelector.DataValueField = "CharacterID";
                ddlCharacterSelector.DataSource = dtCharacters;
                ddlCharacterSelector.DataBind();

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
                    else
                    {
                        ddlCharacterSelector.Items[0].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
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
                //                oLog.AddLogMessage("Done with Page Load", "CharSkillsfull.Page_Load", "", Session.SessionID);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //                LogWriter oLog = new LogWriter();
                //                oLog.AddLogMessage("Starting postback", "CharSkillsFull.Page_PreRender", "", Session.SessionID);

                if (Session["CurrentCharacter"] == null)
                    Session["CurrentCharacter"] = -1;
                if (Session["SelectedCharacter"] == null)
                    Session["SelectedCharacter"] = 7;

                if (Session["SelectedCharacter"] != null)
                {
                    double TotalCP = 0.0;
                    string sCurrent = Session["CurrentCharacter"].ToString();
                    string sSelected = Session["SelectedCharacter"].ToString();
                    if ((!IsPostBack) || (Session["CurrentCharacter"].ToString() != Session["SelectedCharacter"].ToString()))
                    {
                        int iCharID;
                        if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                        {
                            Classes.cCharacter cChar = new Classes.cCharacter();
                            //                            oLog.AddLogMessage("About to load character", "CharSkillsFull.Page_PreRender", "", Session.SessionID);
                            cChar.LoadCharacter(iCharID);
                            //                            oLog.AddLogMessage("Done loading character", "CharSkillsFull.Page_PreRender", "", Session.SessionID);

                            TotalCP = cChar.TotalCP;
                            Session["TotalCP"] = TotalCP;

                            dtCharSkills = Classes.cUtilities.CreateDataTable(cChar.CharacterSkills);
                            Session["CharSkills"] = dtCharSkills;

                            Session["CharacterSkillPools"] = cChar.SkillPools;

                            // Creating a small array of character skills so if the campaign characters are closed you can't sell back a skill.
                            if (ViewState["SkillList"] != null)
                                ViewState.Remove("SkillList");

                            if (cChar.CharacterSkills.Count > 0)
                            {
                                List<int> SkillList = new List<int>();
                                foreach (Classes.cCharacterSkill dSkill in cChar.CharacterSkills)
                                {
                                    SkillList.Add(dSkill.CampaignSkillNodeID);
                                }
                                ViewState["SkillList"] = SkillList;
                            }

                            Session["CurrentCharacter"] = Session["SelectedCharacter"];

                            DataSet dsSkillSets = new DataSet();
                            SortedList sParam = new SortedList();

                            Classes.cCampaignBase cCampaign = new Classes.cCampaignBase(cChar.CampaignID, Session["LoginName"].ToString(), Convert.ToInt32(Session["UserID"].ToString()));
                            if (cCampaign.AllowCharacterRebuild)
                            {
                                hidAllowCharacterRebuild.Value = "1";
                                lblSkillsLocked.Visible = false;
                            }
                            else
                            {
                                hidAllowCharacterRebuild.Value = "0";
                                lblSkillsLocked.Visible = true;
                            }

                            sParam.Add("@CampaignID", cChar.CampaignID);
                            sParam.Add("@CharacterID", Session["CurrentCharacter"].ToString());
                            //                            oLog.AddLogMessage("About to load Campaign Skills", "CharSkillsFull.Page_PreRender", "", Session.SessionID);
                            dsSkillSets = Classes.cUtilities.LoadDataSet("uspGetCampaignSkillsWithNodes", sParam, "LARPortal", Session["LoginName"].ToString(), "");
                            //                            oLog.AddLogMessage("Done loading Campaign Skills", "CharSkillsFull.Page_PreRender", "", Session.SessionID);


                            _dtCampaignSkills = dsSkillSets.Tables[0];
                            Session["SkillNodes"] = _dtCampaignSkills;
                            Session["NodePrerequisites"] = dsSkillSets.Tables[1];
                            Session["SkillTypes"] = dsSkillSets.Tables[2];
                            Session["NodeExclusions"] = dsSkillSets.Tables[3];
                            //                            Session["CharacterSkillNodes"] = dsSkillSets.Tables[4];

                            DataView dvTopNodes = new DataView(_dtCampaignSkills, "ParentSkillNodeID is null", "DisplayOrder", DataViewRowState.CurrentRows);
                            foreach (DataRowView dvRow in dvTopNodes)
                            {
                                TreeNode NewNode = new TreeNode();
                                NewNode.ShowCheckBox = true;
                                NewNode.Text = FormatDescString(dvRow);
                                NewNode.SelectAction = TreeNodeSelectAction.None;

                                int iNodeID;
                                if (int.TryParse(dvRow["CampaignSkillNodeID"].ToString(), out iNodeID))
                                {
                                    NewNode.Value = iNodeID.ToString();
                                    //                                    if (dtCharSkills != null)
                                    //                                    {
                                    if (dvRow["CharacterHasSkill"].ToString() == "1")
                                        //                                        if (dtCharSkills.Select("ParentSkillNodeID = " + iNodeID.ToString()).Length > 0)
                                        NewNode.Checked = true;
                                    NewNode.SelectAction = TreeNodeSelectAction.None;
                                    PopulateTreeView(iNodeID, NewNode);
                                    //                                    }
                                    NewNode.Expanded = false;
                                    tvSkills.Nodes.Add(NewNode);
                                }
                            }

                            //                            oLog.AddLogMessage("Done loading the tree", "CharSkillsFull.Page_PreRender", "", Session.SessionID);

                            CheckExclusions();

                            //                            oLog.AddLogMessage("Done checking exclusions", "CharSkillsFull.Page_PreRender", "", Session.SessionID);


                            Session["CurrentSkillTree"] = tvSkills;
                            ListSkills();


                            //                            oLog.AddLogMessage("Done listing skills", "CharSkillsFull.Page_PreRender", "", Session.SessionID);
                        }
                    }
                }
                //                oLog.AddLogMessage("Done with postback session", "CharSkillsfull.Page_PreRender", "", Session.SessionID);
            }
            if (AlertMessage.Length > 0)
            {
                lblPopMessage.Text = AlertMessage;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            //LogWriter oLog = new LogWriter();
            //oLog.AddLogMessage("Closing the page", "CharSkillsFull.Page_Unload", "", Session.SessionID);
        }
        protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCharacterSelector.SelectedValue == "-1")
                Response.Redirect("CharAdd.aspx");

            if (Session["SelectedCharacter"].ToString() != ddlCharacterSelector.SelectedValue)
            {
                Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                Response.Redirect("CharInfo.aspx");
            }
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            DataView dvChild = new DataView(_dtCampaignSkills, "ParentSkillNodeID = " + parentId.ToString(), "DisplayOrder", DataViewRowState.CurrentRows);
            foreach (DataRowView dr in dvChild)
            {
                int iNodeID;
                if (int.TryParse(dr["CampaignSkillNodeID"].ToString(), out iNodeID))
                {
                    TreeNode childNode = new TreeNode();
                    childNode.ShowCheckBox = true;
                    childNode.Text = FormatDescString(dr);

                    childNode.Expanded = false;
                    childNode.Value = iNodeID.ToString();
                    if (dr["CharacterHasSkill"].ToString() == "1")
                        //if (dtCharSkills != null)
                        //    if (dtCharSkills.Select("CampaignSkillsStandardID = " + iNodeID.ToString()).Length > 0)
                        childNode.Checked = true;
                    childNode.SelectAction = TreeNodeSelectAction.None;
                    parentNode.ChildNodes.Add(childNode);
                    PopulateTreeView(iNodeID, childNode);
                }
            }
        }

        protected void tvSkills_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.Checked)
            {
                TreeView OrigTreeView = new TreeView();
                CopyTreeNodes(tvSkills, OrigTreeView);

                MarkParentNodes(e.Node);

                List<cSkillPool> oSkillList = Session["CharacterSkillPools"] as List<cSkillPool>;

                DataTable dtPointsSpent = new DataTable();
                dtPointsSpent.Columns.Add(new DataColumn("PoolID", typeof(int)));
                dtPointsSpent.Columns.Add(new DataColumn("CPSpent", typeof(double)));
                dtPointsSpent.Columns.Add(new DataColumn("TotalCP", typeof(double)));

                foreach (cSkillPool cSkill in oSkillList)
                {
                    DataRow dNewRow = dtPointsSpent.NewRow();
                    dNewRow["PoolID"] = cSkill.PoolID;
                    dNewRow["TotalCP"] = cSkill.TotalPoints;
                    dNewRow["CPSpent"] = 0.0;

                    dtPointsSpent.Rows.Add(dNewRow);
                }

                DataTable dtAllSkills = Session["SkillNodes"] as DataTable;
                double TotalSpent = 0.0;

                DataTable dtSkillCosts = new DataTable();
                dtSkillCosts.Columns.Add(new DataColumn("Skill", typeof(string)));
                dtSkillCosts.Columns.Add(new DataColumn("Cost", typeof(double)));
                dtSkillCosts.Columns.Add(new DataColumn("SortOrder", typeof(int)));
                dtSkillCosts.Columns.Add(new DataColumn("SkillID", typeof(int)));

                double TotalCP = 0.0;
                double.TryParse(Session["TotalCP"].ToString(), out TotalCP);

                string sSkills = "";
                foreach (TreeNode SkillNode in tvSkills.CheckedNodes)
                {
                    int iSkillID;
                    if (int.TryParse(SkillNode.Value, out iSkillID))
                    {
                        DataRow[] dSkillRow = dtAllSkills.Select("CampaignSkillNodeID = " + iSkillID.ToString());
                        if (dSkillRow.Length > 0)
                        {
                            double SkillCost;
                            int PoolID;
                            if ((double.TryParse(dSkillRow[0]["SkillCPCost"].ToString(), out SkillCost)) &&
                                (int.TryParse(dSkillRow[0]["CampaignSkillPoolID"].ToString(), out PoolID)))
                            {
                                if (dtSkillCosts.Select("SkillID = " + iSkillID.ToString()).Length == 0)
                                {
                                    DataRow[] dSkillCountRow = dtPointsSpent.Select("PoolID = " + PoolID.ToString());
                                    if (dSkillCountRow.Length > 0)
                                        dSkillCountRow[0]["CPSpent"] = (double)(dSkillCountRow[0]["CPSpent"]) + SkillCost;
                                }
                                TotalSpent += SkillCost;
                                if (SkillCost > 0)
                                    sSkills += dSkillRow[0]["SkillName"].ToString() + ":" + SkillCost.ToString() + ", ";
                                DataRow dNewRow = dtSkillCosts.NewRow();
                                dNewRow["Skill"] = dSkillRow[0]["SkillName"].ToString();
                                dNewRow["Cost"] = SkillCost;
                                dNewRow["SkillID"] = iSkillID;
                                dNewRow["SortOrder"] = 10;
                                dtSkillCosts.Rows.Add(dNewRow);
                            }
                        }
                    }
                }

                bool bSpentTooMuch = false;

                foreach (DataRow dCostRow in dtPointsSpent.Rows)
                {
                    double CPSpent;
                    double TotalCPForPool;
                    if ((double.TryParse(dCostRow["TotalCP"].ToString(), out TotalCPForPool)) &&
                         (double.TryParse(dCostRow["CPSpent"].ToString(), out CPSpent)))
                    {
                        if (CPSpent > TotalCPForPool)
                            bSpentTooMuch = true;
                    }
                }

                if (bSpentTooMuch)
                {
                    tvSkills.Nodes.Clear();
                    TreeView OrigTree = Session["CurrentSkillTree"] as TreeView;
                    CopyTreeNodes(OrigTree, tvSkills);

                    AlertMessage = "You do not have enough CP to buy that.";
                }
                else
                {
                    bMeetAllRequirements = true;
                    CheckForRequirements(e.Node.Value);
                    if (!bMeetAllRequirements)
                    {
                        tvSkills.Nodes.Clear();
                        TreeView OrigTree = Session["CurrentSkillTree"] as TreeView;
                        CopyTreeNodes(OrigTree, tvSkills);
                        e.Node.Checked = false;
                        AlertMessage = "You do not have all the requirements to purchase that item.";
                    }
                    else
                    {
                        CheckAllNodesWithValue(e.Node.Value, true);
                    }
                }
                List<TreeNode> FoundNodes = FindNodesByValue(e.Node.Value);
                foreach (TreeNode t in FoundNodes)
                {
                    t.ShowCheckBox = false;
                    EnableChildren(t);
                }
            }
            else
            {
                // Check to see if we should not allow them to sell it back.
                if (ViewState["SkillList"] != null)
                {
                    int iSkillID;
                    if (int.TryParse(e.Node.Value, out iSkillID))
                    {
                        List<int> SkillList = ViewState["SkillList"] as List<int>;
                        if (SkillList.Contains(iSkillID))
                        {
                            if (hidAllowCharacterRebuild.Value == "0")
                            {
                                e.Node.Checked = true;
                                AlertMessage = "This campaign is not allowing skills to be rebuilt at this time.  Once a skill is selected and saved, it cannot be changed.";
                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                //        "MyApplication",
                                //        jsString,
                                //        true);
                                return;
                            }
                        }
                    }
                }
                DeselectChildNodes(e.Node);
                CheckAllNodesWithValue(e.Node.Value, false);

                List<TreeNode> FoundNodes = FindNodesByValue(e.Node.Value);
                foreach (TreeNode t in FoundNodes)
                {
                    t.Text = t.Text.Replace("grey", "black");
                    t.ImageUrl = "";
                    t.ShowCheckBox = true;
                    EnableChildren(t);
                }
            }
            CheckExclusions();
            ListSkills();
            Session["CurrentSkillTree"] = tvSkills;

            lblMessage.Text = "Skills Changed";
            lblMessage.ForeColor = Color.Red;
        }

        protected void ListSkills()
        {
            DataTable dtAllSkills = Session["SkillNodes"] as DataTable;
            //      DataTable dtCharSkills = Session["CharSkills"] as DataTable;

            double TotalSpent = 0.0;

            DataTable dtSkillCosts = new DataTable();
            dtSkillCosts.Columns.Add(new DataColumn("PoolID", typeof(int)));
            dtSkillCosts.Columns.Add(new DataColumn("Skill", typeof(string)));
            dtSkillCosts.Columns.Add(new DataColumn("Cost", typeof(double)));
            dtSkillCosts.Columns.Add(new DataColumn("SortOrder", typeof(int)));
            dtSkillCosts.Columns.Add(new DataColumn("SkillID", typeof(int)));

            double TotalCP = 0.0;
            double.TryParse(Session["TotalCP"].ToString(), out TotalCP);

            foreach (TreeNode SkillNode in tvSkills.CheckedNodes)
            {
                int iSkillID;
                if (int.TryParse(SkillNode.Value, out iSkillID))
                {
                    double SkillCost = 0.0;
                    double DisplayOrder = 10;

                    DataRow[] drPrev = dtSkillCosts.Select("SkillID = " + iSkillID.ToString());
                    if (drPrev.Length == 0)
                    {
                        string sSkillName = "";
                        DataRow[] drCharSkills = dtAllSkills.Select("CampaignSkillNodeID = " + iSkillID.ToString());
                        if (drCharSkills.Length > 0)
                        {
                            if (drCharSkills[0]["CharacterHasSkill"].ToString() == "1")
                                double.TryParse(drCharSkills[0]["CPCostPaid"].ToString(), out SkillCost);
                            else
                                double.TryParse(drCharSkills[0]["SkillCPCost"].ToString(), out SkillCost);

                            sSkillName = drCharSkills[0]["SkillName"].ToString();
                            double.TryParse(drCharSkills[0]["DisplayOrder"].ToString(), out DisplayOrder);
                        }
                        //                        TotalSpent += SkillCost;
                        //                        CampaignSkillPoolID
                        DataRow dNewRow = dtSkillCosts.NewRow();
                        dNewRow["PoolID"] = drCharSkills[0]["CampaignSkillPoolID"];
                        dNewRow["Skill"] = sSkillName;
                        dNewRow["Cost"] = SkillCost;
                        dNewRow["SortOrder"] = DisplayOrder;
                        dNewRow["SkillID"] = iSkillID;
                        dtSkillCosts.Rows.Add(dNewRow);
                    }
                }
            }

            Session["SelectedSkills"] = dtSkillCosts;

            List<cSkillPool> oCampaignPools = (List<cSkillPool>)Session["CharacterSkillPools"];
            cSkillPool DefaultPool = oCampaignPools.Find(x => x.DefaultPool == true);

            DataRow[] dSkillRow = dtSkillCosts.Select("PoolID = " + DefaultPool.PoolID);

            object oResult;
            if (DefaultPool != null)
            {
                string sFilter = "PoolID = " + DefaultPool.PoolID.ToString();
                oResult = dtSkillCosts.Compute("sum(Cost)", sFilter);
                double.TryParse(oResult.ToString(), out TotalSpent);
                TotalCP = DefaultPool.TotalPoints;
            }

            DataTable dtDisplay = new DataTable();
            dtDisplay.Columns.Add(new DataColumn("Skill", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Cost", typeof(double)));
            dtDisplay.Columns.Add(new DataColumn("MainSort", typeof(int)));
            dtDisplay.Columns.Add(new DataColumn("SortOrder", typeof(int)));
            dtDisplay.Columns.Add(new DataColumn("Color", typeof(string)));

            DataRow NewRow = dtDisplay.NewRow();
            NewRow["Skill"] = "Total CP";
            NewRow["Cost"] = TotalCP;
            NewRow["MainSort"] = 1;
            NewRow["SortOrder"] = 1;
            NewRow["Color"] = DefaultPool.PoolDisplayColor;
            dtDisplay.Rows.Add(NewRow);

            NewRow = dtDisplay.NewRow();
            NewRow["Skill"] = "Total Spent";
            NewRow["Cost"] = TotalSpent;
            NewRow["MainSort"] = 1;
            NewRow["SortOrder"] = 2;
            NewRow["Color"] = DefaultPool.PoolDisplayColor;
            dtDisplay.Rows.Add(NewRow);

            NewRow = dtDisplay.NewRow();
            NewRow["Skill"] = "Total Avail";
            NewRow["Cost"] = (TotalCP - TotalSpent);
            NewRow["MainSort"] = 1;
            NewRow["SortOrder"] = 3;
            NewRow["Color"] = DefaultPool.PoolDisplayColor;
            dtDisplay.Rows.Add(NewRow);

            //NewRow = dtDisplay.NewRow();
            //NewRow["Skill"] = "";
            //NewRow["MainSort"] = 1;
            //NewRow["SortOrder"] = 4;
            //NewRow["Color"] = DefaultPool.PoolDisplayColor;
            //dtDisplay.Rows.Add(NewRow);

//            double dTemp = 0;

            foreach (DataRowView dItem in new DataView(dtSkillCosts, "PoolID = " + DefaultPool.PoolID.ToString(), "SortOrder", DataViewRowState.CurrentRows))
            {
                NewRow = dtDisplay.NewRow();
                NewRow["Skill"] = dItem["Skill"].ToString();
                NewRow["MainSort"] = 1;
                NewRow["SortOrder"] = 10;
                NewRow["Cost"] = dItem["Cost"];
                NewRow["Color"] = DefaultPool.PoolDisplayColor;
                dtDisplay.Rows.Add(NewRow);
            }

            int PoolOrderOffset = 10;

            foreach (cSkillPool PoolItem in oCampaignPools.OrderBy(x => x.PoolDescription))
            {
                PoolOrderOffset++;

                if (PoolItem.DefaultPool)       // We've already taken care of this before.
                    continue;

                bool bThereWereRecords = false;

                foreach (DataRowView dItem in new DataView(dtSkillCosts, "PoolID = " + PoolItem.PoolID.ToString(), "SortOrder", DataViewRowState.CurrentRows))
                {
                    NewRow = dtDisplay.NewRow();
                    NewRow["Skill"] = dItem["Skill"].ToString();
                    NewRow["MainSort"] = PoolOrderOffset;
                    NewRow["SortOrder"] = 10;
                    NewRow["Cost"] = dItem["Cost"];
                    NewRow["Color"] = PoolItem.PoolDisplayColor;
                    dtDisplay.Rows.Add(NewRow);
                    bThereWereRecords = true;
                }

                if (bThereWereRecords)
                {
                    string sFilter = "PoolID = " + PoolItem.PoolID.ToString();
                    oResult = dtSkillCosts.Compute("sum(Cost)", sFilter);
                    double.TryParse(oResult.ToString(), out TotalSpent);
                    TotalCP = PoolItem.TotalPoints;

                    NewRow = dtDisplay.NewRow();
                    NewRow["Skill"] = PoolItem.PoolDescription;
                    NewRow["MainSort"] = PoolOrderOffset;
                    NewRow["SortOrder"] = 0;
                    NewRow["Color"] = PoolItem.PoolDisplayColor;
                    dtDisplay.Rows.Add(NewRow);

                    NewRow = dtDisplay.NewRow();
                    NewRow["Skill"] = "Total CP";
                    NewRow["Cost"] = PoolItem.TotalPoints;
                    NewRow["MainSort"] = PoolOrderOffset;
                    NewRow["SortOrder"] = 1;
                    NewRow["Color"] = PoolItem.PoolDisplayColor;
                    dtDisplay.Rows.Add(NewRow);

                    NewRow = dtDisplay.NewRow();
                    NewRow["Skill"] = "Total Spent";
                    NewRow["Cost"] = TotalSpent;
                    NewRow["MainSort"] = PoolOrderOffset;
                    NewRow["SortOrder"] = 2;
                    NewRow["Color"] = PoolItem.PoolDisplayColor;
                    dtDisplay.Rows.Add(NewRow);

                    NewRow = dtDisplay.NewRow();
                    NewRow["Skill"] = "Total Avail";
                    NewRow["Cost"] = (TotalCP - TotalSpent);
                    NewRow["MainSort"] = PoolOrderOffset;
                    NewRow["SortOrder"] = 3;
                    NewRow["Color"] = PoolItem.PoolDisplayColor;
                    dtDisplay.Rows.Add(NewRow);

                    //NewRow = dtDisplay.NewRow();
                    //NewRow["Skill"] = "";
                    //NewRow["MainSort"] = PoolOrderOffset;
                    //NewRow["SortOrder"] = 4;
                    //NewRow["Color"] = PoolItem.PoolDisplayColor;
                    //dtDisplay.Rows.Add(NewRow);

                    NewRow = dtDisplay.NewRow();
                    NewRow["Skill"] = "";
                    NewRow["MainSort"] = PoolOrderOffset;
                    NewRow["SortOrder"] = -1;
                    NewRow["Color"] = PoolItem.PoolDisplayColor;
                    dtDisplay.Rows.Add(NewRow);
                }
            }

            DataView dvSkillCost = new DataView(dtDisplay, "", "MainSort, SortOrder", DataViewRowState.CurrentRows);
            gvCostList.DataSource = dvSkillCost;
            gvCostList.DataBind();
        }

        protected void MarkParentNodes(TreeNode NodeToCheck)
        {
            NodeToCheck.Checked = true;
            if (NodeToCheck.Parent != null)
                MarkParentNodes(NodeToCheck.Parent);
        }

        protected void DeselectChildNodes(TreeNode NodeToCheck)
        {
            NodeToCheck.Checked = false;
            foreach (TreeNode Child in NodeToCheck.ChildNodes)
            {
                Child.Checked = false;
                DeselectChildNodes(Child);
            }
        }

        protected string FormatDescString(DataRowView dTreeNode)
        {
            string sTreeNode = @"<a onmouseover=""GetContent(" + dTreeNode["CampaignSkillNodeID"].ToString() + @"); """ +
                   @"style=""text-decoration: none; color: black; margin-left: 0px; padding-left: 0px;"" > " + dTreeNode["SkillName"].ToString() + @"</a>";
            return sTreeNode;
        }

        protected double CalcSkillCost()
        {
            double TotalCost = 0.0;

            DataTable dtSkills = Session["CharSkills"] as DataTable;
            foreach (DataRow dRow in dtSkills.Rows)
            {
                double CPCost;
                if (double.TryParse(dRow["SkillCPCost"].ToString(), out CPCost))
                {
                    TotalCost += CPCost;
                }
            }
            return TotalCost;
        }

        protected void gvCostList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.Cells[0].Text.ToUpper() == "TOTAL CP") ||
                     (e.Row.Cells[0].Text.ToUpper() == "TOTAL SPENT") ||
                     (e.Row.Cells[0].Text.ToUpper() == "TOTAL AVAIL"))
                {
                    e.Row.Font.Bold = true;
                }
                HiddenField hidColor = (HiddenField)e.Row.FindControl("hidColor");
                if (hidColor != null)
                {
                    e.Row.ForeColor = Color.FromName(hidColor.Value);
                }
                HiddenField hidSortOrder = (HiddenField)e.Row.FindControl("hidSortOrder");
                if (hidSortOrder != null)
                {
                    int iSortOrder;
                    if (int.TryParse(hidSortOrder.Value, out iSortOrder))
                    {
                        if (iSortOrder < 10)
                        {
                            e.Row.Font.Bold = true;
                            e.Row.Font.Size = new FontUnit(12, UnitType.Point);
                        }
                        if (iSortOrder < 0)
                        {
                            e.Row.Font.Size = new FontUnit(12, UnitType.Point);
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int iCharID;

            if (Session["SelectedCharacter"] != null)
            {
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    DataTable dtCampaignSkills = Session["SkillNodes"] as DataTable;

                    Classes.cCharacter Char = new Classes.cCharacter();
                    Char.LoadCharacter(iCharID);

                    int CharacterSkillsSetID = -1;

                    CharacterSkillsSetID = Char.CharacterSkillSetID;

                    foreach (Classes.cCharacterSkill cSkill in Char.CharacterSkills)
                    {
                        cSkill.RecordStatus = Classes.RecordStatuses.Delete;
                        CharacterSkillsSetID = cSkill.CharacterSkillSetID;
                    }

                    foreach (TreeNode SkillNode in tvSkills.CheckedNodes)
                    {
                        int iSkillNodeID;
                        if (int.TryParse(SkillNode.Value, out iSkillNodeID))
                        {
                            var FoundRecord = Char.CharacterSkills.Find(x => x.CampaignSkillNodeID == iSkillNodeID);
                            if (FoundRecord != null)
                                FoundRecord.RecordStatus = Classes.RecordStatuses.Active;
                            else
                            {
                                Classes.cCharacterSkill Newskill = new Classes.cCharacterSkill();
                                Newskill.CharacterSkillID = -1;
                                Newskill.CharacterID = iCharID;
                                Newskill.CampaignSkillNodeID = iSkillNodeID;
                                Newskill.CharacterSkillSetID = CharacterSkillsSetID;
                                Newskill.CPCostPaid = 0;
                                DataView dvCampaignSkill = new DataView(dtCampaignSkills, "CampaignSkillNodeID = " + iSkillNodeID.ToString(), "", DataViewRowState.CurrentRows);
                                if (dvCampaignSkill.Count > 0)
                                {
                                    double dSkillCPCost = 0;
                                    if (double.TryParse(dvCampaignSkill[0]["SkillCPCost"].ToString(), out dSkillCPCost))
                                        Newskill.CPCostPaid = dSkillCPCost;
                                }
                                Char.CharacterSkills.Add(Newskill);
                            }
                        }
                    }

                    string Msg = Char.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);

                    lblPopMessage.Text = "Character " + Char.AKA + " has been saved.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
        }


        public void CopyTreeNodes(TreeView SourceTreeView, TreeView DesSourceView)
        {
            TreeNode newTn = null;
            foreach (TreeNode tn in SourceTreeView.Nodes)
            {
                newTn = new TreeNode(tn.Text, tn.Value);
                newTn.Checked = tn.Checked;
                newTn.Expanded = tn.Expanded;
                newTn.ShowCheckBox = tn.ShowCheckBox;
                newTn.ImageUrl = tn.ImageUrl;

                CopyChildren(newTn, tn);
                DesSourceView.Nodes.Add(newTn);
            }
        }
        public void CopyChildren(TreeNode parent, TreeNode original)
        {
            TreeNode newTn;
            foreach (TreeNode tn in original.ChildNodes)
            {
                newTn = new TreeNode(tn.Text, tn.Value);
                newTn.Checked = tn.Checked;
                newTn.Expanded = tn.Expanded;
                newTn.ShowCheckBox = tn.ShowCheckBox;
                newTn.ImageUrl = tn.ImageUrl;

                parent.ChildNodes.Add(newTn);
                CopyChildren(newTn, tn);
            }
        }

        private bool bMeetAllRequirements = true;

        private void CheckForRequirements(string sValueToCheckFor)
        {
            bMeetAllRequirements = true;

            foreach (TreeNode trMainNodes in tvSkills.Nodes)
                CheckChildNodes(trMainNodes, sValueToCheckFor);
        }

        private void CheckChildNodes(TreeNode NodeToCheck, string sValueToCheckFor)
        {
            if (NodeToCheck.Value == sValueToCheckFor)
                if (NodeToCheck.Parent != null)
                    if (!NodeToCheck.Parent.Checked)
                        bMeetAllRequirements = false;

            foreach (TreeNode trChildNode in NodeToCheck.ChildNodes)
                CheckChildNodes(trChildNode, sValueToCheckFor);
        }

        private void CheckAllNodesWithValue(string sValueToCheckFor, bool bValueToSet)
        {
            foreach (TreeNode trMainNodes in tvSkills.Nodes)
                SetChildNodes(trMainNodes, sValueToCheckFor, bValueToSet);
        }

        private void SetChildNodes(TreeNode NodeToCheck, string sValueToCheckFor, bool bValueToSet)
        {
            if (NodeToCheck.Value == sValueToCheckFor)
                NodeToCheck.Checked = bValueToSet;

            foreach (TreeNode trChildNode in NodeToCheck.ChildNodes)
                SetChildNodes(trChildNode, sValueToCheckFor, bValueToSet);
        }

        private void DisableNodeAndChildren(TreeNode tNode)
        {
            tNode.ShowCheckBox = false;
            foreach (TreeNode ChildNode in tNode.ChildNodes)
                DisableNodeAndChildren(ChildNode);
        }

        private List<TreeNode> FindNodesByValue(string ValueToSearchFor)
        {
            List<TreeNode> FoundNodes = new List<TreeNode>();

            foreach (TreeNode tNode in tvSkills.Nodes)
            {
                //if (tNode.Value == ValueToSearchFor)
                //    FoundNodes.Add(tNode);
                SearchChildren(tNode, FoundNodes, ValueToSearchFor);
            }

            return FoundNodes;
        }

        private void SearchChildren(TreeNode tNode, List<TreeNode> FoundNodes, string ValueToSearchFor)
        {
            if (tNode.Value == ValueToSearchFor)
                FoundNodes.Add(tNode);

            foreach (TreeNode ChildNode in tNode.ChildNodes)
                SearchChildren(ChildNode, FoundNodes, ValueToSearchFor);
        }

        private void DisableChildren(TreeNode tNode)
        {
            tNode.Text = tNode.Text.Replace("black", "grey");
            tNode.ImageUrl = "/img/delete.png";
            tNode.ShowCheckBox = false;

            foreach (TreeNode tnChild in tNode.ChildNodes)
                DisableChildren(tnChild);
        }

        private void EnableChildren(TreeNode tNode)
        {
            tNode.Text = tNode.Text.Replace("grey", "black");
            tNode.ImageUrl = "";
            tNode.ShowCheckBox = true;

            foreach (TreeNode tnChild in tNode.ChildNodes)
                EnableChildren(tnChild);
        }

        protected void CheckExclusions()
        {
            if (Session["NodeExclusions"] == null)
                return;

            DataTable dtExclusions;
            dtExclusions = Session["NodeExclusions"] as DataTable;

            foreach (DataRow dRow in dtExclusions.Rows)
            {
                string sSkill = dRow["SkillNodeID"].ToString();
                List<TreeNode> FoundNodes = FindNodesByValue(sSkill);
                foreach (TreeNode tNode in FoundNodes)
                {
                    if ((tNode.ShowCheckBox.HasValue) && (!(tNode.ShowCheckBox.Value)))
                        continue;

                    DataView dvPreReq = new DataView(dtExclusions, "PreRequisiteSkillNodeID = " + sSkill, "", DataViewRowState.CurrentRows);
                    foreach (DataRowView dExclude in dvPreReq)
                    {
                        string sExc = dExclude["SkillNodeID"].ToString();
                        List<TreeNode> ExcludedNodes = FindNodesByValue(sExc);
                        foreach (TreeNode tnExc in ExcludedNodes)
                            if (tNode.Checked)
                            {
                                if ((!tnExc.ShowCheckBox.HasValue) ||
                                    ((tnExc.ShowCheckBox.HasValue) && (tnExc.ShowCheckBox.Value == true)))
                                    DisableChildren(tnExc);
                            }
                            else
                                EnableChildren(tnExc);
                    }
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
        }
    }
}
