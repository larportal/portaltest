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
    public partial class CharPlaces : System.Web.UI.Page
    {
        private DataTable _dtPlaces = new DataTable();

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

                        DataTable dtPlaces = Classes.cUtilities.CreateDataTable(cChar.Places);
                        gvPlaces.DataSource = dtPlaces;
                        gvPlaces.DataBind();

                        SortedList sParam = new SortedList();
                        sParam.Add("@CampaignID", cChar.CampaignID);
                        _dtPlaces = Classes.cUtilities.LoadDataTable("uspGetCampaignPlaces", sParam, "LARPortal", "", lsRoutineName);

                        //ddlPlacesFilter.DataSource = _dtPlaces;
                        //ddlPlacesFilter.DataTextField = "FullName";
                        //ddlPlacesFilter.DataValueField = "CampaignPlaceID";
                        //ddlPlacesFilter.DataBind();

                        Session["Places"] = dtPlaces;

                        DataView dvTopNodes = new DataView(_dtPlaces, "LocaleID = 0", "", DataViewRowState.CurrentRows);
                        foreach (DataRowView dvRow in dvTopNodes)
                        {
                            TreeNode NewNode = new TreeNode();
//                            NewNode.Text = FormatDescString(dvRow);
                            NewNode.Text = dvRow["PlaceName"].ToString();

                            //int iNodeID;
                            //if (int.TryParse(dvRow["CampaignSkillsStandardID"].ToString(), out iNodeID))
                            //{
                            //    NewNode.Value = iNodeID.ToString();
                            //    if (dtCharSkills != null)
                            //    {
                            //        if (dtCharSkills.Select("CampaignSkillsStandardID = " + iNodeID.ToString()).Length > 0)
                            //            NewNode.Checked = true;
                            //        NewNode.SelectAction = TreeNodeSelectAction.None;
                            //        NewNode.NavigateUrl = "javascript:void(0);";
                            //        PopulateTreeView(iNodeID, NewNode);
                            //    }
                            //    NewNode.Expanded = false;
                            //    tvSkills.Nodes.Add(NewNode);
                            //}
                            tvPlaces.Nodes.Add(NewNode);
                        }











                    }
                }
            }
        }

        protected void gvRelationships_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvRelationships_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
                int iCharID;
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    Classes.cCharacter cChar = new Classes.cCharacter();
                    //cChar.CharacterHistory = taHistory.InnerText;
                    //cChar.Comments = taHistory.InnerText;
                    cChar.SaveCharacter("jlb");
                }

        }

        protected void gvPlaces_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string t;
            gvPlaces.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindPlaces();
        }

        protected void gvPlaces_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvPlaces_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPlaces.EditIndex = -1;
            BindPlaces();
        }

        protected void gvPlaces_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string NewValue = ((TextBox)gvPlaces.Rows[e.RowIndex].Cells[0].FindControl("uxTestEditMode")).Text;
            string RowID = gvPlaces.DataKeys[e.RowIndex].Values["ID"].ToString();
            DataTable dtRecs = Session["Places"] as DataTable;
            DataView dv = new DataView(dtRecs, "ID = " + RowID, "", DataViewRowState.CurrentRows);
            if (dv.Count > 0)
                dv[0]["Name"] = RowID;
            Session["Places"] = dtRecs;
            BindPlaces();
        }

        private void BindPlaces()
        {
            gvPlaces.DataSource = Session["Places"];
            gvPlaces.DataBind();
        }

        protected void gvPlaces_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlPlaces");
                    //bind dropdownlist
                    DataTable dtRecs = Session["Places"] as DataTable;
                    ddList.DataSource = dtRecs;
                    ddList.DataTextField = "Name";
                    ddList.DataValueField = "Name";
                    ddList.DataBind();

                    DataRowView dr = e.Row.DataItem as DataRowView;
                    ddList.SelectedValue = dr["Name"].ToString();
                }
            }
        }
    }
}