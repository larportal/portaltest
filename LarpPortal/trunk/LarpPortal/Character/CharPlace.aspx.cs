using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharPlace : System.Web.UI.Page
    {
        protected DataTable _dtSkills = new DataTable();

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
                    string sCurrent = Session["CurrentCharacter"].ToString();
                    string sSelected = Session["SelectedCharacter"].ToString();
                    if ((!IsPostBack) || (Session["CurrentCharacter"].ToString() != Session["SelectedCharacter"].ToString()))
                    {
                        int iCharID;
                        if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                        {
                            Classes.cCharacter cChar = new Classes.cCharacter();
                            cChar.LoadCharacter(iCharID);

                            dtCharSkills = Classes.cUtilities.CreateDataTable(cChar.Places);
                            ViewState["CharSkills"] = dtCharSkills;

                            Session["CurrentCharacter"] = Session["SelectedCharacter"];

                            DataSet dsSkillSets = new DataSet();
                            SortedList sParam = new SortedList();
                            sParam.Add("@CampaignID", cChar.CampaignID);
                            dsSkillSets = Classes.cUtilities.LoadDataSet("uspGetCampaignPlaces", sParam, "LARPortal", Session["LoginName"].ToString(), "");

                            _dtSkills = dsSkillSets.Tables[0];
                            ViewState["Skills"] = _dtSkills;

                            TreeNode MainNode = new TreeNode("Skills");
                            DataView dvTopNodes = new DataView(_dtSkills, "LocaleID = 0", "", DataViewRowState.CurrentRows);
                            foreach (DataRowView dvRow in dvTopNodes)
                            {
                                TreeNode NewNode = new TreeNode();
                                NewNode.Text = FormatDescString(dvRow);

                                int iNodeID;
                                if (int.TryParse(dvRow["CampaignPlaceID"].ToString(), out iNodeID))
                                {
                                    NewNode.Value = iNodeID.ToString();
                                    if (dtCharSkills != null)
                                    {
                                        //if (dtCharSkills.Select("CampaignSkillsStandardID = " + iNodeID.ToString()).Length > 0)
                                        //    NewNode.Checked = true;
                                        NewNode.SelectAction = TreeNodeSelectAction.None;
                                        NewNode.NavigateUrl = "javascript:void(0);";
                                        PopulateTreeView(iNodeID, NewNode);
                                    }
                                    NewNode.Expanded = false;
                                    tvSkills.Nodes.Add(NewNode);
                                }
                            }
                            //                        double TotalSpent = CalcSkillCost();
                            //lblSpentPoints.Text = string.Format("{0:0.00}", TotalSpent);
                            //lblAvailPoints.Text = string.Format("{0:0.00}", (TotalCP - TotalSpent));
                            ListSkills();
                        }
                    }
                }
            }
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            DataView dvChild = new DataView(_dtSkills, "LocaleID = " + parentId.ToString(), "PlaceName", DataViewRowState.CurrentRows);
            foreach (DataRowView dr in dvChild)
            {
                int iNodeID;
                if (int.TryParse(dr["CampaignPlaceID"].ToString(), out iNodeID))
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = FormatDescString(dr);

                    childNode.Value = iNodeID.ToString();
                    //if (dtCharSkills != null)
                    //    if (dtCharSkills.Select("CampaignSkillsStandardID = " + iNodeID.ToString()).Length > 0)
                    //        childNode.Checked = true;
                    childNode.SelectAction = TreeNodeSelectAction.None;
                    childNode.NavigateUrl = "javascript:void(0);";
                    parentNode.ChildNodes.Add(childNode);
                    PopulateTreeView(iNodeID, childNode);
                }
            }
        }

        protected void tvSkills_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.Checked)
                MarkParentNodes(e.Node);
            ListSkills();
        }

        protected void ListSkills()
        {
            //DataTable dtAllSkills = ViewState["Skills"] as DataTable;
            //double TotalSpent = 0.0;

            //DataTable dtSkillCosts = new DataTable();
            //dtSkillCosts.Columns.Add(new DataColumn("Skill", typeof(string)));
            //dtSkillCosts.Columns.Add(new DataColumn("Cost", typeof(double)));
            //dtSkillCosts.Columns.Add(new DataColumn("SortOrder", typeof(int)));

            //double TotalCP = 0.0;
            //double.TryParse(ViewState["TotalCP"].ToString(), out TotalCP);

            //foreach (TreeNode SkillNode in tvSkills.CheckedNodes)
            //{
            //    int iSkillID;
            //    if (int.TryParse(SkillNode.Value, out iSkillID))
            //    {
            //        DataRow[] dSkillRow = dtAllSkills.Select("CampaignSkillsStandardID = " + iSkillID.ToString());
            //        if (dSkillRow.Length > 0)
            //        {
            //            double SkillCost;
            //            if (double.TryParse(dSkillRow[0]["SkillCPCost"].ToString(), out SkillCost))
            //                TotalSpent += SkillCost;
            //            DataRow dNewRow = dtSkillCosts.NewRow();
            //            dNewRow["Skill"] = dSkillRow[0]["SkillName"].ToString();
            //            dNewRow["Cost"] = SkillCost;
            //            dNewRow["SortOrder"] = 10;
            //            dtSkillCosts.Rows.Add(dNewRow);
            //        }
            //    }
            //}

            //Session["SelectedSkills"] = dtSkillCosts;

            //DataRow NewRow = dtSkillCosts.NewRow();
            //NewRow["Skill"] = "Total Skills";
            //NewRow["Cost"] = TotalCP;
            //NewRow["SortOrder"] = 1;
            //dtSkillCosts.Rows.Add(NewRow);

            //NewRow = dtSkillCosts.NewRow();
            //NewRow["Skill"] = "Total Spent";
            //NewRow["Cost"] = TotalSpent;
            //NewRow["SortOrder"] = 2;
            //dtSkillCosts.Rows.Add(NewRow);

            //NewRow = dtSkillCosts.NewRow();
            //NewRow["Skill"] = "Total Avail";
            //NewRow["Cost"] = (TotalCP - TotalSpent);
            //NewRow["SortOrder"] = 3;
            //dtSkillCosts.Rows.Add(NewRow);

            //NewRow = dtSkillCosts.NewRow();
            //NewRow["Skill"] = "";
            //NewRow["SortOrder"] = 4;
            //dtSkillCosts.Rows.Add(NewRow);

            ////lblSpentPoints.Text = string.Format("{0:0.00}", TotalSpent);
            ////lblAvailPoints.Text = string.Format("{0:0.00}", (TotalCP - TotalSpent));

            //DataView dvSkillCost = new DataView(dtSkillCosts, "", "SortOrder", DataViewRowState.CurrentRows);
            //gvCostList.DataSource = dvSkillCost;
            //gvCostList.DataBind();
        }

        protected void MarkParentNodes(TreeNode NodeToCheck)
        {
            NodeToCheck.Checked = true;
            if (NodeToCheck.Parent != null)
                MarkParentNodes(NodeToCheck.Parent);
        }

        protected string FormatDescString(DataRowView dTreeNode)
        {
            string sTreeNode = dTreeNode["PlaceName"].ToString();
            //string sTreeNode =
            //    @"<a onmouseover=""ShowContent('divDesc'); " +
            //    @"document.getElementById('divDesc').innerHTML = '<b>" + dTreeNode["SkillName"].ToString().Replace("'", "\\'").Replace("\"", "\\'") + @"</b><br>" +
            //    dTreeNode["SkillShortDescription"].ToString().Replace("'", "\\'").Replace("\"", "\\'") + "<br><br>" +
            //    "Cost: " + dTreeNode["SkillCPCost"].ToString() + "<br><br>" +
            //    dTreeNode["SkillLongDescription"].ToString().Replace("'", "\\'").Replace("\"", "\\'") + @"'; return true;"" " +
            //    @"href=""javascript:ShowContent('divDesc')"" style=""text-decoration: none; color: black;"" > " + dTreeNode["SkillName"].ToString() + @"</a>";

            return sTreeNode;
        }

        protected double CalcSkillCost()
        {
            double TotalCost = 0.0;

            DataTable dtSkills = ViewState["CharSkills"] as DataTable;
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
                if ((e.Row.Cells[0].Text.ToUpper() == "TOTAL SKILLS") ||
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
                }
            }

            //foreach (TreeNode SkillNode in tvSkills.CheckedNodes)
            //{
            //    int iSkillID;
            //    if (int.TryParse(SkillNode.Value, out iSkillID))
            //    {
            //        //DataRow[] dSkillRow = dtAllSkills.Select("CampaignSkillsStandardID = " + iSkillID.ToString());
            //        //if (dSkillRow.Length > 0)
            //        //{
            //        //    double SkillCost;
            //        //    if (double.TryParse(dSkillRow[0]["SkillCPCost"].ToString(), out SkillCost))
            //        //        TotalSpent += SkillCost;
            //        //    DataRow dNewRow = dtSkillCosts.NewRow();
            //        //    dNewRow["Skill"] = dSkillRow[0]["SkillName"].ToString();
            //        //    dNewRow["Cost"] = SkillCost;
            //        //    dNewRow["SortOrder"] = 10;
            //        //    dtSkillCosts.Rows.Add(dNewRow);
            //        //}
            //    }
            //}

        }
    }
}