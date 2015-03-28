using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SelectedCharacter"] != null)
                {
                    int iCharID;
                    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                    {
                        Classes.cCharacter cChar = new Classes.cCharacter();
                        cChar.LoadCharacter(iCharID);

                        lblHeader.Text = "Character History - " + cChar.AKA + " - " + cChar.CampaignName;

//                        DataTable dtRelations = new DataTable();
//                        dtRelations.Columns.Add(new DataColumn("ID", typeof(int)));
//                        dtRelations.Columns.Add(new DataColumn("Name", typeof(string)));
//                        dtRelations.Columns.Add(new DataColumn("RelationDescription", typeof(string)));
//                        dtRelations.Columns.Add(new DataColumn("NamedInRules", typeof(Boolean)));

//                        foreach (Classes.cRelationship Relat in cChar.Relationships)
//                        {
//                            DataRow dNewRow = dtRelations.NewRow();
//                            dNewRow["ID"] = Relat.CharacterRelationshipID;
//                            dNewRow["Name"] = Relat.Name;
//                            dNewRow["RelationDescription"] = Relat.RelationDescription;
//                            dtRelations.Rows.Add(dNewRow);
//                        }

//                        gvRelationships.DataSource = dtRelations;
//                        gvRelationships.DataBind();

//                        ddlCharacterFilter.DataTextField = "Name";
//                        ddlCharacterFilter.DataValueField = "ID";
//                        ddlCharacterFilter.DataSource = dtRelations;
//                        ddlCharacterFilter.DataBind();

//                        DataTable dtPlaces = new DataTable();
//                        dtPlaces.Columns.Add(new DataColumn("ID", typeof(int)));
//                        dtPlaces.Columns.Add(new DataColumn("Name", typeof(string)));
//                        dtPlaces.Columns.Add(new DataColumn("PlaceName", typeof(string)));
//                        dtPlaces.Columns.Add(new DataColumn("NamedInRules", typeof(Boolean)));

////                        for ( int i = 0; i < 5; i ++ )
//                        foreach (Classes.cPlace Place in cChar.Places)
//                        {
//                            DataRow dNewRow = dtPlaces.NewRow();
//                            dNewRow["ID"] = Place.CampaignPlaceID;
//                            dNewRow["Name"] = Place.PlaceName;
//                            dNewRow["PlaceName"] = "??";
//                            dtPlaces.Rows.Add(dNewRow);
//                        }

//                        gvPlaces.DataSource = dtPlaces;
//                        gvPlaces.DataBind();

//                        ddlPlacesFilter.DataTextField = "Name";
//                        ddlPlacesFilter.DataValueField = "ID";
//                        ddlPlacesFilter.DataSource = dtPlaces;
//                        ddlPlacesFilter.DataBind();

//                        Session["Places"] = dtPlaces;

                        taHistory.InnerText = cChar.CharacterHistory;

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
                    cChar.CharacterHistory = taHistory.InnerText;
                    cChar.Comments = taHistory.InnerText;
                    cChar.SaveCharacter(Session["UserName"].ToString(), (int) Session["UserID"]);
                }

        }

        //protected void gvPlaces_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    string t;
        //    gvPlaces.EditIndex = e.NewEditIndex;
        //    //Bind data to the GridView control.
        //    BindPlaces();
        //}

        //protected void gvPlaces_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{

        //}

        //protected void gvPlaces_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gvPlaces.EditIndex = -1;
        //    BindPlaces();
        //}

        //protected void gvPlaces_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    string NewValue = ((TextBox)gvPlaces.Rows[e.RowIndex].Cells[0].FindControl("uxTestEditMode")).Text;
        //    string RowID = gvPlaces.DataKeys[e.RowIndex].Values["ID"].ToString();
        //    DataTable dtRecs = Session["Places"] as DataTable;
        //    DataView dv = new DataView(dtRecs, "ID = " + RowID, "", DataViewRowState.CurrentRows);
        //    if (dv.Count > 0)
        //        dv[0]["Name"] = RowID;
        //    Session["Places"] = dtRecs;
        //    BindPlaces();
        //}

        //private void BindPlaces()
        //{
        //    gvPlaces.DataSource = Session["Places"];
        //    gvPlaces.DataBind();
        //}

        //protected void gvPlaces_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        //        {
        //            DropDownList ddList = (DropDownList)e.Row.FindControl("ddlPlaces");
        //            //bind dropdownlist
        //            DataTable dtRecs = Session["Places"] as DataTable;
        //            ddList.DataSource = dtRecs;
        //            ddList.DataTextField = "Name";
        //            ddList.DataValueField = "Name";
        //            ddList.DataBind();

        //            DataRowView dr = e.Row.DataItem as DataRowView;
        //            ddList.SelectedValue = dr["Name"].ToString();
        //        }
        //    }
        //}
    }
}