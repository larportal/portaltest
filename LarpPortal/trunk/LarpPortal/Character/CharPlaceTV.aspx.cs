using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharPlaceTV : System.Web.UI.Page
    {
        private DataTable _dtPlaces = new DataTable();
        private DataTable _dtCharPlaces = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                if (Session["SelectedCharacter"] != null)
                {
                    int iCharID;
                    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                    {
                        Classes.cCharacter cChar = new Classes.cCharacter();
                        cChar.LoadCharacter(iCharID);

                        _dtCharPlaces = Classes.cUtilities.CreateDataTable(cChar.Places);

                        SortedList sParam = new SortedList();
                        sParam.Add("@CampaignID", cChar.CampaignID);
                        _dtPlaces = Classes.cUtilities.LoadDataTable("uspGetCampaignPlaces", sParam, "LARPortal", "", lsRoutineName);

                        Session["Places"] = _dtCharPlaces;

                        DataView dvTopNodes = new DataView(_dtPlaces, "LocaleID = 0", "", DataViewRowState.CurrentRows);
                        foreach (DataRowView dvRow in dvTopNodes)
                        {
                            TreeNode NewNode = new TreeNode();
                            NewNode.Text = dvRow["PlaceName"].ToString();

                            int iNodeID;
                            if (int.TryParse(dvRow["CampaignPlaceID"].ToString(), out iNodeID))
                            {
                                NewNode.Value = iNodeID.ToString();
                                if (_dtCharPlaces != null)
                                {
                                    if (_dtCharPlaces.Select("CampaignPlaceID = " + iNodeID.ToString()).Length > 0)
                                        NewNode.Checked = true;
                                    NewNode.SelectAction = TreeNodeSelectAction.None;
                                    NewNode.NavigateUrl = "javascript:void(0);";
                                    PopulateTreeView(iNodeID, NewNode);
                                }
                                NewNode.Expanded = false;
//                                tvPlaces.Nodes.Add(NewNode);
                            }
                            tvPlaces.Nodes.Add(NewNode);
                        }
                    }
                }
            }
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            DataView dvChild = new DataView(_dtPlaces, "LocaleID = " + parentId.ToString(), "PlaceName", DataViewRowState.CurrentRows);
            foreach (DataRowView dr in dvChild)
            {
                int iNodeID;
                if (int.TryParse(dr["CampaignPlaceID"].ToString(), out iNodeID))
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = dr["PlaceName"].ToString();

                    childNode.Value = iNodeID.ToString();
                    if (_dtCharPlaces != null)
                        if (_dtCharPlaces.Select("CampaignPlaceID = " + iNodeID.ToString()).Length > 0)
                            childNode.Checked = true;
                    childNode.SelectAction = TreeNodeSelectAction.None;
                    childNode.NavigateUrl = "javascript:void(0);";
                    parentNode.ChildNodes.Add(childNode);
                    PopulateTreeView(iNodeID, childNode);
                }
            }
        }
    }
}