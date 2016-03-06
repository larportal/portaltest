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

namespace LarpPortal
{
    public partial class SkillTable : System.Web.UI.Page
    {
        DataSet _dsSkills = new DataSet();

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SortedList sParam = new SortedList();
                sParam.Add("@CampaignID", 33);
                _dsSkills = Classes.cUtilities.LoadDataSet("uspGetCampaignSkillsWithNodes", sParam, "LARPortal", "SkillTable", "");
                DataView dvTopNodes = new DataView(_dsSkills.Tables[0], "ParentSkillNodeID is null or ParentSkillNodeID= 0", "DisplayOrder", DataViewRowState.CurrentRows);
                foreach (DataRowView dvRow in dvTopNodes)
                {
                    TreeNode NewNode = new TreeNode();
                    NewNode.ShowCheckBox = true;
                    NewNode.Text = dvRow["SkillName"].ToString();
                    NewNode.SelectAction = TreeNodeSelectAction.None;

                    int iNodeID;
                    if (int.TryParse(dvRow["CampaignSkillNodeID"].ToString(), out iNodeID))
                    {
                        NewNode.Value = iNodeID.ToString();
                        if (dvRow["CharacterHasSkill"].ToString() == "1")
                            NewNode.Checked = true;
                        NewNode.SelectAction = TreeNodeSelectAction.None;
                        PopulateTreeView(iNodeID, NewNode);
                        NewNode.Expanded = false;
                        tvSkills.Nodes.Add(NewNode);
                    }
                }
            }
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            DataView dvChild = new DataView(_dsSkills.Tables[0], "ParentSkillNodeID = " + parentId.ToString(), "DisplayOrder", DataViewRowState.CurrentRows);
            foreach (DataRowView dr in dvChild)
            {
                int iNodeID;
                if (int.TryParse(dr["CampaignSkillNodeID"].ToString(), out iNodeID))
                {
                    TreeNode childNode = new TreeNode();
                    childNode.ShowCheckBox = true;
                    childNode.Text = dr["SkillName"].ToString();

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
    }
}
