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
        protected DataTable _dsCampaignPlaces = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                tvSkills.Attributes.Add("onclick", "postBackByObject()");
        }

        DataTable dtCharPlaces = null;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["NewRecCounter"] = 0;

                if (Session["CurrentCharacter"] == null)
                    Session["CurrentCharacter"] = -1;
                if (Session["SelectedCharacter"] == null)
                    Session["SelectedCharacter"] = -1;

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

                            dtCharPlaces = Classes.cUtilities.CreateDataTable(cChar.Places);
                            Session["CurrentCharacter"] = Session["SelectedCharacter"];
                            Session["CharacterPlaces"] = cChar.Places;

                            DataSet dsCampaignPlaces = new DataSet();
                            SortedList sParam = new SortedList();
                            sParam.Add("@CampaignID", cChar.CampaignID);
                            dsCampaignPlaces = Classes.cUtilities.LoadDataSet("uspGetCampaignPlaces", sParam, "LARPortal", Session["LoginName"].ToString(), "");

                            _dsCampaignPlaces = dsCampaignPlaces.Tables[0];
                            Session["CampaignPlaces"] = _dsCampaignPlaces;

                            ddlNonCampLocalePlaces.DataTextField = "PlaceName";
                            ddlNonCampLocalePlaces.DataValueField = "CampaignPlaceID";
                            ddlNonCampLocalePlaces.DataSource = dsCampaignPlaces.Tables[0];
                            ddlNonCampLocalePlaces.DataBind();

                            if (dtCharPlaces.Columns["ShowButton"] == null)
                                dtCharPlaces.Columns.Add(new DataColumn("ShowButton", typeof(Boolean)));

                            foreach (DataRow dRow in dtCharPlaces.Rows)
                            {
                                dRow["Showbutton"] = true;
                                string t = dRow["PlaceID"].ToString();

                                if (dRow["PlaceID"].ToString() != "0")
                                {
                                    DataView dvCampaignPlace = new DataView(_dsCampaignPlaces, "CampaignPlaceID = " + dRow["PlaceID"].ToString(), "", DataViewRowState.CurrentRows);
                                    if (dvCampaignPlace.Count > 0)
                                    {
                                        dRow["ShowButton"] = false;
                                        dRow["Comments"] = dvCampaignPlace[0]["Locale"].ToString();
                                    }
                                }
                            }

                            Session["CharPlaces"] = dtCharPlaces;
                            gvPlaces.DataSource = dtCharPlaces;
                            gvPlaces.DataBind();

                            TreeNode MainNode = new TreeNode("Skills");
                            DataView dvTopNodes = new DataView(_dsCampaignPlaces, "LocaleID = 0", "", DataViewRowState.CurrentRows);
                            foreach (DataRowView dvRow in dvTopNodes)
                            {
                                TreeNode NewNode = new TreeNode();
                                NewNode.Text = dvRow["PlaceName"].ToString();

                                int iNodeID;
                                if (int.TryParse(dvRow["CampaignPlaceID"].ToString(), out iNodeID))
                                {
                                    NewNode.Value = iNodeID.ToString();
                                    PopulateTreeView(iNodeID, NewNode);
                                    NewNode.Expanded = false;
                                    tvSkills.Nodes.Add(NewNode);
                                }
                            }
//                            ListSkills();
                        }
                    }
                }
            }
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            DataView dvChild = new DataView(_dsCampaignPlaces, "LocaleID = " + parentId.ToString(), "PlaceName", DataViewRowState.CurrentRows);
            foreach (DataRowView dr in dvChild)
            {
                int iNodeID;
                if (int.TryParse(dr["CampaignPlaceID"].ToString(), out iNodeID))
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = dr["PlaceName"].ToString();

                    childNode.Value = iNodeID.ToString();
                    childNode.SelectAction = TreeNodeSelectAction.Select;
                    parentNode.ChildNodes.Add(childNode);
                    PopulateTreeView(iNodeID, childNode);
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
                    Classes.cCharacter Char = new Classes.cCharacter();
                    Char.LoadCharacter(iCharID);

                    List<Classes.cPlace> NewList = Session["CharacterPlaces"] as List<Classes.cPlace>;

                    // If there are any new places, they will have a negative number. (Only way to keep them unique.)
                    // If negative, change them to -1 so the system will create the record.

                    foreach (Classes.cPlace Place in NewList)
                        if (Place.CampaignPlaceID < 0)
                            Place.CampaignPlaceID = -1;
                    Char.Places.Clear();
                    Char.Places.AddRange(NewList);
                    Char.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);
                    string jsString = "alert('Character " + Char.AKA + " has been saved.');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                            "MyApplication",
                            jsString,
                            true);
                }
            }
        }

        protected void gvPlaces_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DELETEITEM":
                    {
                        int iPlaceID;
                        if (int.TryParse(e.CommandArgument.ToString(), out iPlaceID))
                        {
                            List<Classes.cPlace> Places = Session["CharacterPlaces"] as List<Classes.cPlace>;
                            var CharPlac = Places.Find(x => x.CampaignPlaceID == iPlaceID);
                            if (CharPlac != null)
                            {
                                Places.Remove(CharPlac);
                                Session["CharacterPlaces"] = Places;
                                BindPlaces(Places);
                            }
                        }
                        break;
                    }

                case "EDITITEM":
                    {
                        int iPlaceID;
                        if (int.TryParse(e.CommandArgument.ToString(), out iPlaceID))
                        {
                            List<Classes.cPlace> Places = Session["CharacterPlaces"] as List<Classes.cPlace>;
                            var CharPlac = Places.Find(x => x.CampaignPlaceID == iPlaceID);
                            if (CharPlac != null)
                            {
                                if (CharPlac.LocaleID == -1)
                                {
                                    hidNonCampPlaceID.Value = iPlaceID.ToString();
                                    hidCampaignPlaceID.Value = "";
                                    tbNonCampPlaceName.Text = CharPlac.PlaceName;
                                    tbNonCampPlayerComments.Text = CharPlac.Comments;
                                    ddlNonCampLocalePlaces.SelectedIndex = -1;

                                    foreach (ListItem li in ddlNonCampLocalePlaces.Items)
                                    {
                                        if (li.Value == CharPlac.LocaleID.ToString())
                                            li.Selected = true;
                                    }
                                    mvAddingItems.SetActiveView(vwNonCampaignPlace);
                                }
                                else
                                {
                                    hidNonCampPlaceID.Value = "";
                                    hidCampaignPlaceID.Value = iPlaceID.ToString();
                                    lblCampaignPlaceName.Text = CharPlac.PlaceName;
                                    lblCampaignLocale.Text = CharPlac.Locale;
                                    tbCampaignPlayerComments.Text = CharPlac.Comments;

                                    mvAddingItems.SetActiveView(vwCampaignPlace);
                                }
                            }
                        }
                        break;
                    }

            }
        }

#region NonCampRoutines
        protected void btnAddNonCampPlace_Click(object sender, EventArgs e)
        {
            int NewRecCounter = Convert.ToInt32(ViewState["NewRecCounter"]);
            NewRecCounter--;
            hidNonCampPlaceID.Value = NewRecCounter.ToString();
            hidCampaignPlaceID.Value = "";

            ViewState["NewRecCounter"] = NewRecCounter;

            mvAddingItems.SetActiveView(vwNonCampaignPlace);
        }

        protected void btnNonCampSave_Click(object sender, EventArgs e)
        {
            List<Classes.cPlace> Places = Session["CharacterPlaces"] as List<Classes.cPlace>;

            int iPlaceID;

            if (int.TryParse(hidNonCampPlaceID.Value, out iPlaceID))
            {
                var CharPlac = Places.Find(x => x.CampaignPlaceID == iPlaceID);
                if (CharPlac != null)
                {
                    CharPlac.PlaceName = tbNonCampPlaceName.Text;
                    CharPlac.Comments = tbNonCampPlayerComments.Text;
                    int iLocaleID;
                    if (int.TryParse(ddlNonCampLocalePlaces.SelectedValue, out iLocaleID))
                    {
                        CharPlac.LocaleID = iLocaleID;
                        CharPlac.Locale = ddlNonCampLocalePlaces.SelectedItem.Text;
                    }
                }
                else
                {
                    Classes.cPlace NewPlace = new Classes.cPlace();
                    NewPlace.CampaignPlaceID = iPlaceID;

                    NewPlace.PlaceID = -1;
                    NewPlace.PlaceName = tbNonCampPlaceName.Text;
                    NewPlace.Comments = tbNonCampPlayerComments.Text;
                    int iLocaleID;
                    if (int.TryParse(ddlNonCampLocalePlaces.SelectedValue, out iLocaleID))
                    {
                        NewPlace.LocaleID = iLocaleID;
                        NewPlace.Locale = ddlNonCampLocalePlaces.SelectedItem.Text;
                    }
                    Places.Add(NewPlace);
                }
            }
            Session["CharacterPlaces"] = Places;
            tbNonCampPlaceName.Text = "";
            tbNonCampPlayerComments.Text = "";
            ddlNonCampLocalePlaces.SelectedIndex = -1;

            BindPlaces(Places);

            mvAddingItems.SetActiveView(vwNewItemButton);
        }

#endregion

#region CampaignPlaceRoutines
        protected void tvSkills_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (tvSkills.SelectedNode != null)
            {
                DataTable dtCampaignPlaces = Session["CampaignPlaces"] as DataTable;

                int iSelectedPlace;
                int.TryParse(tvSkills.SelectedNode.Value, out iSelectedPlace);
                int NewRecCounter;
                if (int.TryParse(ViewState["NewRecCounter"].ToString(), out NewRecCounter))
                {
                    NewRecCounter--;
                    ViewState["NewRecCounter"] = NewRecCounter;
                    hidCampaignPlaceID.Value = NewRecCounter.ToString();

                    Classes.cPlace NewRec = new Classes.cPlace();
                    NewRec.PlaceID = iSelectedPlace;
                    NewRec.CampaignPlaceID = NewRecCounter;

                    DataView dvPlaces = new DataView(dtCampaignPlaces, "CampaignPlaceID = " + iSelectedPlace.ToString(), "", DataViewRowState.CurrentRows);
                    foreach (DataRowView dRow in dvPlaces)
                    {
                        lblCampaignPlaceName.Text = dRow["PlaceName"].ToString();
                        lblCampaignLocale.Text = dRow["Locale"].ToString();
                        tbCampaignPlayerComments.Text = "";
                        NewRec.Locale = dRow["Locale"].ToString(); ;
                        NewRec.PlaceName = dRow["PlaceName"].ToString();
                        NewRec.LocaleID = Convert.ToInt32(dRow["LocaleID"].ToString());
                    }
                    List<Classes.cPlace> Places = Session["CharacterPlaces"] as List<Classes.cPlace>;
                    Places.Add(NewRec);
                    Session["CharacterPlaces"] = Places;
                    mvAddingItems.SetActiveView(vwCampaignPlace);
                }
            }
        }

        protected void btnCampaignSave_Click(object sender, EventArgs e)
        {
            List<Classes.cPlace> Places = Session["CharacterPlaces"] as List<Classes.cPlace>;

            int iCampaignPlaceID;
            if ( int.TryParse(hidCampaignPlaceID.Value, out iCampaignPlaceID))
            {
                var CharPlac = Places.Find(x => x.CampaignPlaceID == iCampaignPlaceID);
                if (CharPlac != null)
                {
                    CharPlac.PlaceName = lblCampaignPlaceName.Text;
                    CharPlac.Comments = tbCampaignPlayerComments.Text;
                    CharPlac.Locale = lblCampaignLocale.Text;
                }
                Session["CharacterPlaces"] = Places;
                BindPlaces(Places);
                mvAddingItems.SetActiveView(vwNewItemButton);
            }
            else
                mvAddingItems.SetActiveView(vwNewItemButton);
        }

#endregion

        protected void btnCancelAdding_Click(object sender, EventArgs e)
        {
            mvAddingItems.SetActiveView(vwNewItemButton);
        }


        public void BindPlaces(List<Classes.cPlace> Places)
        {
            dtCharPlaces = Classes.cUtilities.CreateDataTable(Places);

            DataTable dtCampaignPlaces = Session["CampaignPlaces"] as DataTable;
            if (dtCharPlaces.Columns["ShowButton"] == null)
                dtCharPlaces.Columns.Add(new DataColumn("ShowButton", typeof(Boolean)));

            gvPlaces.DataSource = dtCharPlaces;
            gvPlaces.DataBind();
        }
    }
}