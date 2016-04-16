﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Character
{
    public partial class CharSkill : System.Web.UI.Page
    {
        protected DataTable _dtCampaignSkills = new DataTable();
        DataTable dtCharSkills = null;
        LogWriter oLog = new LogWriter();

        protected void Page_Load(object sender, EventArgs e)
        {
            oLog.AddLogMessage("Starting page load for CharSkill", "CharSkill.Page_Load", "", Session.SessionID);
            if (!IsPostBack)
            {
                tvSkills.Attributes.Add("onclick", "postBackByObject()");

                ViewState["CurrentCharacter"] = "";
            }
            oLog.AddLogMessage("Done page load for CharSkill", "CharSkill.Page_Load", "", Session.SessionID);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oLog.AddLogMessage("Starting postback", "CharSkill.Page_PreRender", "", Session.SessionID);

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
                            oLog.AddLogMessage("About to load character", "CharSkill.Page_PreRender", "", Session.SessionID);
                            cChar.LoadCharacter(iCharID);
                            oLog.AddLogMessage("Done loading character", "CharSkill.Page_PreRender", "", Session.SessionID);

                            Session["CampaignID"] = cChar.CampaignID;
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
                            oLog.AddLogMessage("About to load Campaign Skills", "CharSkill.Page_PreRender", "", Session.SessionID);
                            dsSkillSets = Classes.cUtilities.LoadDataSet("uspGetCampaignSkillsWithNodes", sParam, "LARPortal", Session["LoginName"].ToString(), "");
                            oLog.AddLogMessage("Done loading Campaign Skills", "CharSkill.Page_PreRender", "", Session.SessionID);


                            _dtCampaignSkills = dsSkillSets.Tables[0];
                            Session["SkillNodes"] = _dtCampaignSkills;
                            Session["NodePrerequisites"] = dsSkillSets.Tables[1];
                            Session["SkillTypes"] = dsSkillSets.Tables[2];
                            Session["NodeExclusions"] = dsSkillSets.Tables[3];

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
                                    NewNode.Expanded = false;
                                    NewNode.Value = iNodeID.ToString();
                                    if (dvRow["CharacterHasSkill"].ToString() == "1")
                                    {
                                        NewNode.Checked = true;
                                    }
                                    NewNode.SelectAction = TreeNodeSelectAction.None;
                                    PopulateTreeView(iNodeID, NewNode);
                                    tvSkills.Nodes.Add(NewNode);
                                }
                            }

                            oLog.AddLogMessage("Done loading the tree", "CharSkill.Page_PreRender", "", Session.SessionID);

                            CheckExclusions();

                            oLog.AddLogMessage("Done checking exclusions", "CharSkill.Page_PreRender", "", Session.SessionID);


                            Session["CurrentSkillTree"] = tvSkills;
                            ListSkills();


                            oLog.AddLogMessage("Done listing skills", "CharSkill.Page_PreRender", "", Session.SessionID);
                        }
                    }
                }
                oLog.AddLogMessage("Done with postback session", "CharSkill.Page_PreRender", "", Session.SessionID);
            }
        }


        protected void Page_Unload(object sender, EventArgs e)
        {
            // This is only here to have a message when the page is closing. Used for figuring out where the time is being spent.
            oLog.AddLogMessage("Closing the page", "CharSkill.Page_Unload", "", Session.SessionID);
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
                    {
                        childNode.Checked = true;
                        parentNode.Expand();
                    }
                    childNode.SelectAction = TreeNodeSelectAction.None;
                    parentNode.ChildNodes.Add(childNode);
                    PopulateTreeView(iNodeID, childNode);
                }
            }
        }


        /// <summary>
        /// Tree node changed event. Skill being selected/deselected. Note - this happens AFTER the person clicks. Which means the node is already checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvSkills_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.Checked)
            {
                // Save tree nodes so if they don't have enough points to buy the skill, we have the old one.
                TreeView OrigTreeView = new TreeView();
                CopyTreeNodes(tvSkills, OrigTreeView);

                MarkParentNodes(e.Node);

                List<cSkillPool> oSkillPools = Session["CharacterSkillPools"] as List<cSkillPool>;

                DataTable dtPointsSpent = new DataTable();
                dtPointsSpent.Columns.Add(new DataColumn("PoolID", typeof(int)));
                dtPointsSpent.Columns.Add(new DataColumn("PoolName", typeof(string)));
                dtPointsSpent.Columns.Add(new DataColumn("CPSpent", typeof(double)));
                dtPointsSpent.Columns.Add(new DataColumn("TotalCP", typeof(double)));

                // Go through all of the pools so we have the list on the screen.
                foreach (cSkillPool cSkill in oSkillPools)
                {
                    DataRow dNewRow = dtPointsSpent.NewRow();
                    dNewRow["PoolID"] = cSkill.PoolID;
                    dNewRow["PoolName"] = cSkill.PoolDescription;
                    dNewRow["TotalCP"] = cSkill.TotalPoints;
                    dNewRow["CPSpent"] = 0.0;

                    dtPointsSpent.Rows.Add(dNewRow);
                }

                DataTable dtAllSkills = Session["SkillNodes"] as DataTable;

                int iPool = 0;

                foreach (TreeNode SkillNode in tvSkills.CheckedNodes)
                {
                    int iSkillID;
                    if (int.TryParse(SkillNode.Value, out iSkillID))
                    {
                        double SkillCost = 0.0;

                        DataRow[] drSkillRow = dtAllSkills.Select("CampaignSkillNodeID = " + iSkillID.ToString());
                        if (drSkillRow.Length > 0)
                        {
                            int.TryParse(drSkillRow[0]["CampaignSkillPoolID"].ToString(), out iPool);
                            {
                                if (drSkillRow[0]["CharacterHasSkill"].ToString() == "1")
                                    double.TryParse(drSkillRow[0]["CPCostPaid"].ToString(), out SkillCost);
                                else
                                    double.TryParse(drSkillRow[0]["SkillCPCost"].ToString(), out SkillCost);
                            }
                        }

                        if (iPool != 0)
                        {
                            DataRow[] dPool = dtPointsSpent.Select("PoolID = " + iPool.ToString());
                            if (dPool.Length > 0)
                            {
                                dPool[0]["CPSpent"] = (double)(dPool[0]["CPSpent"]) + SkillCost;
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

                    DisplayAlertMessage("You do not have enough points to buy that.");
                }
                else
                {
                    if (!CheckForRequirements(e.Node.Value))
                    {
                        tvSkills.Nodes.Clear();
                        TreeView OrigTree = Session["CurrentSkillTree"] as TreeView;
                        CopyTreeNodes(OrigTree, tvSkills);
                        e.Node.Checked = false;
                        DisplayAlertMessage("You do not have all the requirements to purchase that item.");
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
                    EnableNodeAndChildren(t);
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
                                DisplayAlertMessage("This campaign is not allowing skills to be rebuilt at this time.  Once a skill is selected and saved, it cannot be changed.");
                                return;
                            }
                        }
                    }
                }

                CheckSkillRequirementExclusions();

                DeselectChildNodes(e.Node);
                CheckAllNodesWithValue(e.Node.Value, false);

                List<TreeNode> FoundNodes = FindNodesByValue(e.Node.Value);
                foreach (TreeNode t in FoundNodes)
                {
                    t.Text = t.Text.Replace("grey", "black");
                    t.ImageUrl = "";
                    t.ShowCheckBox = true;
                    EnableNodeAndChildren(t);
                }
            }

            ListSkills();
            Session["CurrentSkillTree"] = tvSkills;

            lblMessage.Text = "Skills Changed";
            lblMessage.ForeColor = Color.Red;
        }


        protected void ListSkills()
        {
            DataTable dtAllSkills = Session["SkillNodes"] as DataTable;

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

                if (PoolItem.TotalPoints > 0)
                {
                    foreach (DataRowView dItem in new DataView(dtSkillCosts, "PoolID = " + PoolItem.PoolID.ToString(), "SortOrder", DataViewRowState.CurrentRows))
                    {
                        NewRow = dtDisplay.NewRow();
                        NewRow["Skill"] = dItem["Skill"].ToString();
                        NewRow["MainSort"] = PoolOrderOffset;
                        NewRow["SortOrder"] = 10;
                        NewRow["Cost"] = dItem["Cost"];
                        NewRow["Color"] = PoolItem.PoolDisplayColor;
                        dtDisplay.Rows.Add(NewRow);
                    }

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


        /// <summary>
        /// Format the text of the nide so it calls the Javascript that will run the web service and get the skill info.
        /// </summary>
        /// <param name="dTreeNode"></param>
        /// <returns></returns>
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

                    string sUserName = Session["UserName"].ToString();
                    int iUserID = (int)Session["UserID"];

                    foreach (cCharacterSkill dSkill in Char.CharacterSkills)
                    {
                        if (dSkill.RecordStatus == RecordStatuses.Active)
                            dSkill.Save(sUserName, iUserID);
                        else
                            dSkill.Delete(sUserName, iUserID);
                    }

                    DisplayAlertMessage("Character " + Char.AKA + " has been saved.");
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

        private bool CheckForRequirements(string sValueToCheckFor)
        {
            bool bMeetAllRequirements = true;

            SortedList sParams = new SortedList();
            sParams.Add("@SkillNodeID", sValueToCheckFor);
            DataSet dsRequire = cUtilities.LoadDataSet("uspGetNodeRequirements", sParams, "LARPortal", Session["UserName"].ToString(), "CharSkill.aspx_CheckForRequirements");

            // Get the list of items we can't have if we purchased the item.
            DataView dvExcludeRows = new DataView(dsRequire.Tables[0], "ExcludeFromPurchase = true", "SkillNodeID", DataViewRowState.CurrentRows);

            foreach (DataRowView dRow in dvExcludeRows)
            {
                if (dRow["PrerequisiteSkillNodeID"] != DBNull.Value)
                {
                    int iPreReq;
                    if (int.TryParse(dRow["PrerequisiteSkillNodeID"].ToString(), out iPreReq))
                    {
                        List<TreeNode> FoundNodes = FindNodesByValue(iPreReq.ToString());
                        foreach (TreeNode tNode in FoundNodes)
                            DisableNodeAndChildren(tNode);
                    }
                }
            }

            bMeetAllRequirements = true;

            DataView dvRequiredRows = new DataView(dsRequire.Tables[0], "ExcludeFromPurchase = false", "SkillNodeID", DataViewRowState.CurrentRows);

            foreach (DataRowView dRow in dvRequiredRows)
            {
                if (dRow["PrerequisiteSkillNodeID"] != DBNull.Value)
                {
                    int iPreReq;
                    if (int.TryParse(dRow["PrerequisiteSkillNodeID"].ToString(), out iPreReq))
                    {
                        if (iPreReq != 0)
                        {
                            List<TreeNode> FoundNodes = FindNodesByValue(iPreReq.ToString());
                            if (FoundNodes.Count == 0)
                                bMeetAllRequirements = false;
                        }
                    }
                }
            }

            dvRequiredRows = new DataView(dsRequire.Tables[0], "PrerequisiteGroupID is not null", "", DataViewRowState.CurrentRows);

            foreach (DataRowView dRow in dvRequiredRows)
            {
                // Since there is at least one group process it.
                int iPreReqGroup;
                int iNumReq;
                if ((int.TryParse(dRow["PrerequisiteGroupID"].ToString(), out iPreReqGroup)) &&
                    (int.TryParse(dRow["NumGroupSkillsRequired"].ToString(), out iNumReq)))
                {
                    // Get the items for the specific group.
                    DataView dReqGroup = new DataView(dsRequire.Tables[1], "PrerequisiteGroupID = " + iPreReqGroup.ToString(), "", DataViewRowState.CurrentRows);
                    if (dReqGroup.Count > 0)
                    {
                        // There were records. Convert the dataview of reuired nodes convert to a list of string - easier to process.
                        List<string> ReqSkillNodes = dReqGroup.ToTable().AsEnumerable().Select(x => x[1].ToString()).ToList();
                        // If we find the value we are looking for - remove it.
                        ReqSkillNodes.Remove(sValueToCheckFor);
                        List<TreeNode> FoundNode = FindNodesByValueList(ReqSkillNodes);
                        if (FoundNode.Count < iNumReq)
                            bMeetAllRequirements = false;
                    }
                }
            }
            return bMeetAllRequirements;
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


        /// <summary>
        /// Go through a node and it's children and 'disable' them. Disabling really means add an image and remove the check box.
        /// </summary>
        /// <param name="tNode"></param>
        private void DisableNodeAndChildren(TreeNode tNode)
        {
            tNode.Text = tNode.Text.Replace("black", "grey");
            tNode.ImageUrl = "/img/delete.png";
            tNode.ShowCheckBox = false;
            foreach (TreeNode ChildNode in tNode.ChildNodes)
                DisableNodeAndChildren(ChildNode);
        }


        /// <summary>
        /// Go through a node and it's children and 'enable' them. Enabling it removing the image and turning the check box on.
        /// </summary>
        /// <param name="tNode"></param>
        private void EnableNodeAndChildren(TreeNode tNode)
        {
            tNode.Text = tNode.Text.Replace("grey", "black");
            tNode.ImageUrl = "";
            tNode.ShowCheckBox = true;

            foreach (TreeNode tnChild in tNode.ChildNodes)
                EnableNodeAndChildren(tnChild);
        }


        /// <summary>
        /// Given a tree node, see if it's value is in the value we are searching for. For each node, go through the child nodes.
        /// </summary>
        /// <param name="ValueToSearchFor">Single string value to look for. Value is stored in nodes .Value</param>
        /// <returns>List of tree nodes with the value searching. It should only return a single node but use a list just in case.</returns>
        private List<TreeNode> FindNodesByValue(string ValueToSearchFor)
        {
            List<TreeNode> FoundNodes = new List<TreeNode>();

            foreach (TreeNode tNode in tvSkills.Nodes)
            {
                SearchChildren(tNode, FoundNodes, ValueToSearchFor);
            }

            return FoundNodes;
        }


        /// <summary>
        /// Given a tree node, see if it is the value we are looking for. Have to go through all of the children's node.
        /// </summary>
        /// <param name="tNode">The node to check the value and to search the children off.</param>
        /// <param name="FoundNodes">List of nodes to be returned.</param>
        /// <param name="ValueToSearchFor">The value that we are going to search for.</param>
        private void SearchChildren(TreeNode tNode, List<TreeNode> FoundNodes, string ValueToSearchFor)
        {
            if (tNode.Value == ValueToSearchFor)
                FoundNodes.Add(tNode);

            foreach (TreeNode ChildNode in tNode.ChildNodes)
                SearchChildren(ChildNode, FoundNodes, ValueToSearchFor);
        }


        /// <summary>
        /// Given a tree node, see if it's value is in the list we are searching for. For each node, go through the child nodes.
        /// </summary>
        /// <param name="lValueList">List of string values we are searching for.</param>
        /// <returns>List of tree nodes with the value values we have found.</returns>
        private List<TreeNode> FindNodesByValueList(List<string> lValueList)
        {
            List<TreeNode> FoundNodes = new List<TreeNode>();

            foreach (TreeNode tNode in tvSkills.Nodes)
            {
                SearchChildrenList(tNode, FoundNodes, lValueList);
            }

            return FoundNodes;
        }


        /// <summary>
        /// Given a tree node, see if it is one of the values we are looking for. Have to go through all of the children's node.
        /// </summary>
        /// <param name="tNode">The node to check the value and to search the children off.</param>
        /// <param name="FoundNodes">List of nodes to be returned.</param>
        /// <param name="ValueToSearchFor">List of values we are searching for.</param>
        private void SearchChildrenList(TreeNode tNode, List<TreeNode> FoundNodes, List<string> lValueList)
        {
            // See if the tree value is in the list we are search for. If so, add it to the nodes.
            if (tNode.Checked)
                if (lValueList.Exists(x => x == tNode.Value))
                    FoundNodes.Add(tNode);

            foreach (TreeNode ChildNode in tNode.ChildNodes)
                SearchChildrenList(ChildNode, FoundNodes, lValueList);
        }

        protected void CheckExclusions()
        {
            if (Session["NodeExclusions"] == null)
                return;

            foreach (TreeNode tNode in tvSkills.Nodes)
                EnableNodeAndChildren(tNode);

            DataTable dtExclusions;
            dtExclusions = Session["NodeExclusions"] as DataTable;

            foreach (TreeNode CheckedNode in tvSkills.CheckedNodes)
            {
                string sSkill = CheckedNode.Value;
                DataView dvPreReq = new DataView(dtExclusions, "PreRequisiteSkillNodeID = " + sSkill, "", DataViewRowState.CurrentRows);
                foreach (DataRowView dExclude in dvPreReq)
                {
                    string sExc = dExclude["SkillNodeID"].ToString();
                    List<TreeNode> ExcludedNodes = FindNodesByValue(sExc);
                    foreach (TreeNode tnExc in ExcludedNodes)
                        DisableNodeAndChildren(tnExc);
                }
            }
        }

        /// <summary>
        /// Add the Javascript to display an allert.
        /// </summary>
        /// <param name="pvMessage"></param>
        private void DisplayAlertMessage(string pvMessage)
        {
            string AlertMessage = "alert('" + pvMessage + "');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", AlertMessage, true);
        }


        /// <summary>
        /// Go through all of the checked nodes and make sure you have all the requirements. This is for when somebody unchecks something.
        /// You then have to go through all of the nodes to make sure that you still have the requirements for everything else.
        /// </summary>
        private void CheckSkillRequirementExclusions()
        {
            SortedList sParams = new SortedList();
            if (Session["CampaignID"] == null)
                Response.Redirect("/default.aspx", true);

            // Get all the prereqs/exclusions for the entire campaign so we don't have to keep reloading it.
            sParams.Add("@CampaignID", Session["CampaignID"].ToString());
            DataSet dsRequire = cUtilities.LoadDataSet("uspGetCampaignNodeRequirements", sParams, "LARPortal", Session["UserName"].ToString(), "CharSkill.aspx_CheckSkillRequirementExclusions");

            bool bChangesMade = false;

            // Enable everything. Then we will go through and disable nodes as needed.
            foreach (TreeNode tBaseNode in tvSkills.Nodes)
                EnableNodeAndChildren(tBaseNode);

            // As long as we have made a change to the tree, keep rechecking.
            do
            {
                bChangesMade = false;
                foreach (TreeNode tNode in tvSkills.CheckedNodes)
                {
                    // Do we have all of the requirements for this node?
                    if (!CheckNodeRequirement(tNode, dsRequire))
                    {
                        // Don't have the requirements so the node has already been unchecked. We need to start over and check all the requirements.
                        bChangesMade = true;
                        break;
                    }
                }
            } while (bChangesMade);
        }


        /// <summary>
        /// Give a node and the dataset for the campaign, see if the node has all the requirements it needs. If it doesn't uncheck it.
        /// </summary>
        /// <param name="tNode">Checked node that needs to be check if all the requirements are met.</param>
        /// <param name="dsRequire">The dataset with the prereqs/exclusions for the campaign.</param>
        /// <returns>True means it has everything it needs, False it doesn't have all the requirements.</returns>
        private bool CheckNodeRequirement(TreeNode tNode, DataSet dsRequire)
        {
            bool bMetRequirements = true;

            try
            {
                // Table 0 is the prereq of a single node.
                DataView dvRequiredRows = new DataView(dsRequire.Tables[0], "ExcludeFromPurchase = false and PrerequisiteGroupID is null and SkillNodeID = " + tNode.Value,
                    "SkillNodeID", DataViewRowState.CurrentRows);

                // Go through all of the single requirements and make sure they are all there.
                foreach (DataRowView dRow in dvRequiredRows)
                {
                    if (dRow["PrerequisiteSkillNodeID"] != DBNull.Value)
                    {
                        int iPreReq;
                        if (int.TryParse(dRow["PrerequisiteSkillNodeID"].ToString(), out iPreReq))
                        {
                            if (iPreReq != 0)       // May be set to 0 by accident.
                            {
                                // Get the single pre/req skill from the nodes
                                List<TreeNode> tnFoundNode = FindNodesByValue(iPreReq.ToString());
                                if (tnFoundNode.Count == 0)
                                {
                                    bMetRequirements = false;
                                }
                            }
                        }
                    }
                }

                // Check to make sure the node has all the required skills for purchase based on a group of skills.
                dvRequiredRows = new DataView(dsRequire.Tables[0], "PrerequisiteGroupID is not null and SkillNodeID = " + tNode.Value, "", DataViewRowState.CurrentRows);

                foreach (DataRowView dRow in dvRequiredRows)
                {
                    // Since there is at least one group process it.
                    int iPreReqGroup;       // What's the number of the group to process.
                    int iNumReq;            // How many of the requirements do we have to have?
                    if ((int.TryParse(dRow["PrerequisiteGroupID"].ToString(), out iPreReqGroup)) &&
                        (int.TryParse(dRow["NumGroupSkillsRequired"].ToString(), out iNumReq)))
                    {
                        // Get the items for the specific group. Table1 is the group requirements.
                        DataView dReqGroup = new DataView(dsRequire.Tables[1], "PrerequisiteGroupID = " + iPreReqGroup.ToString(), "", DataViewRowState.CurrentRows);
                        if (dReqGroup.Count > 0)
                        {
                            // There were records. Convert the dataview of required nodes to a list of strings - easier to process. The 2nd field is the skill nodes.
                            List<string> ReqSkillNodes = dReqGroup.ToTable().AsEnumerable().Select(x => x[1].ToString()).ToList();
                            // If we find the value we are looking for - remove it.
                            ReqSkillNodes.Remove(tNode.Value);
                            List<TreeNode> FoundNode = FindNodesByValueList(ReqSkillNodes);
                            if (FoundNode.Count < iNumReq)
                                bMetRequirements = false;
                        }
                    }
                }

                // Only need to check exclusions if the all of the requirements have been met.
                if (bMetRequirements)
                {
                    // Check exclusions. Disable all nodes sthat are excluded because of this.
                    dvRequiredRows = new DataView(dsRequire.Tables[0], "ExcludeFromPurchase = true and SkillNodeID = " + tNode.Value, "SkillNodeID", DataViewRowState.CurrentRows);

                    foreach (DataRowView dRow in dvRequiredRows)
                    {
                        if (dRow["PrerequisiteSkillNodeID"] != DBNull.Value)
                        {
                            int iPreReq;
                            if (int.TryParse(dRow["PrerequisiteSkillNodeID"].ToString(), out iPreReq))
                            {
                                if (iPreReq.ToString() != tNode.Value)
                                {
                                    // Get the node that has the value of the prereq and disable it and all of it's children.
                                    List<TreeNode> tnFoundNode = FindNodesByValue(iPreReq.ToString());
                                    foreach (TreeNode tnNodesToExclude in tnFoundNode)
                                    {
                                        DisableNodeAndChildren(tnNodesToExclude);
                                    }
                                }
                            }
                        }
                    }
                }

                if (!bMetRequirements)
                    tNode.Checked = false;
            }
            catch (Exception ex)
            {
                string l = ex.Message;
            }

            return bMetRequirements;
        }
    }
}
