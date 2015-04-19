using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharSkill : System.Web.UI.Page
    {
        protected DataTable _dtSkills = new DataTable();
        private string AlertMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                tvSkills.Attributes.Add("onclick", "postBackByObject()");
        }

        DataTable dtCharSkills = null;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                            cChar.LoadCharacter(iCharID);

                            string Msg = "CharacterLoaded: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
                            AddLogMessage(Msg);

                            TotalCP = cChar.TotalCP;
                            Session["TotalCP"] = TotalCP;

                            dtCharSkills = Classes.cUtilities.CreateDataTable(cChar.CharacterSkills);
                            Session["CharSkills"] = dtCharSkills;

                            Session["CurrentCharacter"] = Session["SelectedCharacter"];

                            DataSet dsSkillSets = new DataSet();
                            SortedList sParam = new SortedList();
                            sParam.Add("@CampaignID", cChar.CampaignID);
                            dsSkillSets = Classes.cUtilities.LoadDataSet("uspGetCampaignSkills", sParam, "LARPortal", Session["LoginName"].ToString(), "");

                            _dtSkills = dsSkillSets.Tables[1];
                            Session["Skills"] = _dtSkills;
                            Session["ExcludeSkills"] = dsSkillSets.Tables[2];

//                            TreeNode MainNode = new TreeNode("Skills");

                            Msg = "About to populate skill tree: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
                            AddLogMessage(Msg);

                            DataView dvTopNodes = new DataView(_dtSkills, "PreRequisiteSkillID is null", "", DataViewRowState.CurrentRows);
                            foreach (DataRowView dvRow in dvTopNodes)
                            {
                                TreeNode NewNode = new TreeNode();
                                NewNode.Text = FormatDescString(dvRow);

                                int iNodeID;
                                if (int.TryParse(dvRow["CampaignSkillsStandardID"].ToString(), out iNodeID))
                                {
                                    NewNode.Value = iNodeID.ToString();
                                    if (dtCharSkills != null)
                                    {
                                        if (dtCharSkills.Select("CampaignSkillsStandardID = " + iNodeID.ToString()).Length > 0)
                                            NewNode.Checked = true;
                                        NewNode.SelectAction = TreeNodeSelectAction.None;
//                                        NewNode.NavigateUrl = "javascript:void(0);";
                                        PopulateTreeView(iNodeID, NewNode);
                                    }
                                    NewNode.Expanded = false;
                                    tvSkills.Nodes.Add(NewNode);
                                }
                            }
                            Msg = "Done populating: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
                            AddLogMessage(Msg);

                            Session["CurrentSkillTree"] = tvSkills;
                            ListSkills();
                        }
                    }
                }
            }
            if (AlertMessage.Length > 0)
            {
                if (AlertMessage.Length > 0)
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                         "MyApplication",
                        AlertMessage,
                        true);
            }
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            DataView dvChild = new DataView(_dtSkills, "PreRequisiteSkillID = " + parentId.ToString(), "SkillName", DataViewRowState.CurrentRows);
            foreach (DataRowView dr in dvChild)
            {
                int iNodeID;
                if (int.TryParse(dr["CampaignSkillsStandardID"].ToString(), out iNodeID))
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = FormatDescString(dr);

                    childNode.Expanded = false;
                    childNode.Value = iNodeID.ToString();
                    if (dtCharSkills != null)
                        if (dtCharSkills.Select("CampaignSkillsStandardID = " + iNodeID.ToString()).Length > 0)
                            childNode.Checked = true;
                    childNode.SelectAction = TreeNodeSelectAction.None;
                    childNode.NavigateUrl = "javascript:void(0);";
                    parentNode.ChildNodes.Add(childNode);
                    PopulateTreeView(iNodeID, childNode);
                }
            }
            CheckExclusions();
        }

        protected void tvSkills_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            lblMessage.Text = "Skills Changed";
            lblMessage.ForeColor = Color.Red;

            if (e.Node.Checked)
            {
                TreeView OrigTreeView = new TreeView();
                CopyTreeNodes(tvSkills, OrigTreeView);

                MarkParentNodes(e.Node);

                DataTable dtAllSkills = Session["Skills"] as DataTable;
                double TotalSpent = 0.0;

                DataTable dtSkillCosts = new DataTable();
                dtSkillCosts.Columns.Add(new DataColumn("Skill", typeof(string)));
                dtSkillCosts.Columns.Add(new DataColumn("Cost", typeof(double)));
                dtSkillCosts.Columns.Add(new DataColumn("SortOrder", typeof(int)));

                double TotalCP = 0.0;
                double.TryParse(Session["TotalCP"].ToString(), out TotalCP);

                foreach (TreeNode SkillNode in tvSkills.CheckedNodes)
                {
                    int iSkillID;
                    if (int.TryParse(SkillNode.Value, out iSkillID))
                    {
                        DataRow[] dSkillRow = dtAllSkills.Select("CampaignSkillsStandardID = " + iSkillID.ToString());
                        if (dSkillRow.Length > 0)
                        {
                            double SkillCost;
                            if (double.TryParse(dSkillRow[0]["SkillCPCost"].ToString(), out SkillCost))
                                TotalSpent += SkillCost;
                            DataRow dNewRow = dtSkillCosts.NewRow();
                            dNewRow["Skill"] = dSkillRow[0]["SkillName"].ToString();
                            dNewRow["Cost"] = SkillCost;
                            dNewRow["SortOrder"] = 10;
                            dtSkillCosts.Rows.Add(dNewRow);
                        }
                    }
                }
                if (TotalSpent > TotalCP)
                {
                    tvSkills.Nodes.Clear();
                    TreeView OrigTree = Session["CurrentSkillTree"] as TreeView;
                    CopyTreeNodes(OrigTree, tvSkills);

                    AlertMessage = "alert('You do not have enough CP to buy that.');";
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
                        AlertMessage = "alert('You do not have all the requirements to purchase that item.');";
                    }
                    else
                    {
                        CheckAllNodesWithValue(e.Node.Value, true);
                    }
                }
                List<TreeNode> FoundNodes = FindNodesByValue("961");
                foreach (TreeNode t in FoundNodes)
                {
                    t.Text = t.Text.Replace("black", "grey");
                    t.ImageUrl = "/img/delete.png";
                    t.ShowCheckBox = false;
                    DisableChildren(t);
                }
            }
            else
            {
                DeselectChildNodes(e.Node);
                CheckAllNodesWithValue(e.Node.Value, false);

                List<TreeNode> FoundNodes = FindNodesByValue("961");
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
        }

        protected void ListSkills()
        {
            DataTable dtAllSkills = Session["Skills"] as DataTable;
            double TotalSpent = 0.0;

            DataTable dtSkillCosts = new DataTable();
            dtSkillCosts.Columns.Add(new DataColumn("Skill", typeof(string)));
            dtSkillCosts.Columns.Add(new DataColumn("Cost", typeof(double)));
            dtSkillCosts.Columns.Add(new DataColumn("SortOrder", typeof(int)));

            double TotalCP = 0.0;
            double.TryParse(Session["TotalCP"].ToString(), out TotalCP);

            foreach (TreeNode SkillNode in tvSkills.CheckedNodes)
            {
                int iSkillID;
                if (int.TryParse(SkillNode.Value, out iSkillID))
                {
                    DataRow[] dSkillRow = dtAllSkills.Select("CampaignSkillsStandardID = " + iSkillID.ToString());
                    if (dSkillRow.Length > 0)
                    {
                        double SkillCost;
                        if (double.TryParse(dSkillRow[0]["SkillCPCost"].ToString(), out SkillCost))
                            TotalSpent += SkillCost;
                        DataRow dNewRow = dtSkillCosts.NewRow();
                        dNewRow["Skill"] = dSkillRow[0]["SkillName"].ToString();
                        dNewRow["Cost"] = SkillCost;
                        dNewRow["SortOrder"] = 10;
                        dtSkillCosts.Rows.Add(dNewRow);
                    }
                }
            }

            Session["SelectedSkills"] = dtSkillCosts;

            DataRow NewRow = dtSkillCosts.NewRow();
            NewRow["Skill"] = "Total CP";
            NewRow["Cost"] = TotalCP;
            NewRow["SortOrder"] = 1;
            dtSkillCosts.Rows.Add(NewRow);

            NewRow = dtSkillCosts.NewRow();
            NewRow["Skill"] = "Total Spent";
            NewRow["Cost"] = TotalSpent;
            NewRow["SortOrder"] = 2;
            dtSkillCosts.Rows.Add(NewRow);

            NewRow = dtSkillCosts.NewRow();
            NewRow["Skill"] = "Total Avail";
            NewRow["Cost"] = (TotalCP - TotalSpent);
            NewRow["SortOrder"] = 3;
            dtSkillCosts.Rows.Add(NewRow);

            NewRow = dtSkillCosts.NewRow();
            NewRow["Skill"] = "";
            NewRow["SortOrder"] = 4;
            dtSkillCosts.Rows.Add(NewRow);

            DataView dvSkillCost = new DataView(dtSkillCosts, "", "SortOrder", DataViewRowState.CurrentRows);
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
            string sTreeNode =
                @"<a onmouseover=""ShowContent('divDesc'); " +
                @"document.getElementById('divDesc').innerHTML = '<b>" + dTreeNode["SkillName"].ToString().Replace("'", "\\'").Replace("\"", "\\'") + @"</b><br>" +
                dTreeNode["SkillShortDescription"].ToString().Replace("'", "\\'").Replace("\"", "\\'") + "<br><br>" +
                "Cost: " + dTreeNode["SkillCPCost"].ToString() + "<br><br>" +
                dTreeNode["SkillLongDescription"].ToString().Replace("'", "\\'").Replace("\"", "\\'") + @"'; return true;"" " +
                @"href=""javascript:ShowContent('divDesc')"" style=""text-decoration: none; color: black;"" > " + dTreeNode["SkillName"].ToString() + @"</a>";

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
                    e.Row.Font.Bold = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int iCharID;
            if (Session["SelectedCharacter"] != null)
            {
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    Classes.cCharacter Char = new Classes.cCharacter();
                    Char.LoadCharacter(iCharID);

                    int CharacterSkillsSetID = -1;

                    foreach (Classes.cCharacterSkill cSkill in Char.CharacterSkills)
                    {
                        cSkill.RecordStatus = Classes.RecordStatuses.Delete;
                        CharacterSkillsSetID = cSkill.CharacterSkillSetID;
                    }

                    foreach (TreeNode SkillNode in tvSkills.CheckedNodes)
                    {
                        int iSkillID;
                        if (int.TryParse(SkillNode.Value, out iSkillID))
                        {
                            var FoundRecord = Char.CharacterSkills.Find(x => x.CampaignSkillsStandardID == iSkillID);
                            if (FoundRecord != null)
                                FoundRecord.RecordStatus = Classes.RecordStatuses.Active;
                            else
                            {
                                Classes.cCharacterSkill Newskill = new Classes.cCharacterSkill();
                                Newskill.CharacterSkillsStandardID = -1;
                                Newskill.CharacterID = iCharID;
                                Newskill.CampaignSkillsStandardID = iSkillID;
                                Newskill.CharacterSkillSetID = CharacterSkillsSetID;
                                Newskill.CPCostPaid = 0;
                                Char.CharacterSkills.Add(Newskill);
                            }
                        }
                    }
                    Char.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);
                    lblMessage.Text = "Skills Saved";
                    lblMessage.ForeColor = Color.Black;

                    string jsString = "alert('Character " + Char.AKA + " has been saved.');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                            "MyApplication",
                            jsString,
                            true);
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
                if ( NodeToCheck.Parent != null)
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
                if (tNode.Value == ValueToSearchFor)
                    FoundNodes.Add(tNode);
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
            if (Session["ExcludeSkills"] == null)
                return;

            DataTable dtExclusions;
            dtExclusions = Session["ExcludeSkills"] as DataTable;

            foreach (DataRow dRow in dtExclusions.Rows)
            {
                string sSkill = dRow["CampaignSkillsStandardID"].ToString();
                List<TreeNode> FoundNodes = FindNodesByValue(sSkill);
                foreach (TreeNode tNode in FoundNodes)
                {
                    DataView dvPreReq = new DataView(dtExclusions, "PreRequisiteSkillID = " + sSkill, "", DataViewRowState.CurrentRows);
                    foreach (DataRowView dExclude in dvPreReq)
                    {
                        string sExc = dExclude["CampaignSkillsStandardID"].ToString();
                        List<TreeNode> ExcludedNodes = FindNodesByValue(sExc);
                        foreach (TreeNode tnExc in ExcludedNodes)
                            if (tNode.Checked)
                                DisableChildren(tnExc);
                            else
                                EnableChildren(tnExc);
                    }
                }
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            string Msg = "Page Unload: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
            AddLogMessage(Msg);
        }

        protected void AddLogMessage(string Msg)
        {
            SortedList sParam = new SortedList();
            sParam.Add("@Msg", Msg);
            Classes.cUtilities.PerformNonQuery("uspInsSystemLog", sParam, "LARPortal", Session["UserName"].ToString());
        }
    }
}
