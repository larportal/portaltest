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

                            ddlLocalePlaces.DataTextField = "PlaceName";
                            ddlLocalePlaces.DataValueField = "CampaignPlaceID";
                            ddlLocalePlaces.DataSource = dsCampaignPlaces.Tables[0];
                            ddlLocalePlaces.DataBind();

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
                            ListSkills();
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

        protected void ListSkills()
        {
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

                    ///Todo JLB - Places - check for deleted items.
                    //int CharacterSkillsSetID = -1;

                    //foreach (Classes.cCharacterSkill cSkill in Char.CharacterSkills)
                    //{
                    //    cSkill.RecordStatus = Classes.RecordStatuses.Delete;
                    //    CharacterSkillsSetID = cSkill.CharacterSkillSetID;
                    //}

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

        protected void tvSkills_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (tvSkills.SelectedNode != null)
            {
                TreeNode t = tvSkills.SelectedNode;
                DataTable dtCampaignPlaces = Session["CampaignPlaces"] as DataTable;

                int iSelectedPlace;
                int.TryParse(tvSkills.SelectedNode.Value, out iSelectedPlace);

                DataRow[] PlaceRecord = dtCampaignPlaces.Select("CampaignPlaceID = " + t.Value);
                if (PlaceRecord.Length > 0)
                {
                    mvAddingItems.SetActiveView(vwExistingPlace);
                    lblPlaceName.Text = PlaceRecord[0]["PlaceName"].ToString();
                    lblLocale.Text = PlaceRecord[0]["Locale"].ToString();

                    List<Classes.cPlace> Places = Session["CharacterPlaces"] as List<Classes.cPlace>;
                    var CharPlac = Places.Find(x => x.PlaceID == iSelectedPlace);
                    if (CharPlac != null)
                    {
                        trAlreadySelected.Visible = true;
                        btnCancelAdding.Enabled = false;
                        btnSaveExistingPlace.Enabled = false;
                    }
                    else
                    {
                        trAlreadySelected.Visible = false;
                        btnCancelAdding.Enabled = true;
                        btnSaveExistingPlace.Enabled = true;
                        int NewRecCounter = Convert.ToInt32(ViewState["NewRecCounter"]);
                        NewRecCounter--;
                        hidPlaceID.Value = NewRecCounter.ToString();

                        ViewState["NewRecCounter"] = NewRecCounter;
                    }
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
                                hidPlaceID.Value = iPlaceID.ToString();
                                tbPlaceName.Text = CharPlac.PlaceName;
                                tbPlayerComments.Text = CharPlac.Comments;

                                ddlLocalePlaces.SelectedIndex = -1;

                                foreach (ListItem li in ddlLocalePlaces.Items)
                                {
                                    if (li.Value == CharPlac.LocaleID.ToString())
                                        li.Selected = true;
                                }
                                mvAddingItems.SetActiveView(vwNewPlace);
                            }
                        }
                        break;
                    }

            }
        }

        protected void btnAddNewPlace_Click(object sender, EventArgs e)
        {
            int NewRecCounter = Convert.ToInt32(ViewState["NewRecCounter"]);
            NewRecCounter--;
            hidPlaceID.Value = NewRecCounter.ToString();

            ViewState["NewRecCounter"] = NewRecCounter;

            mvAddingItems.SetActiveView(vwNewPlace);
        }

        public void BindPlaces(List<Classes.cPlace> Places)
        {
            dtCharPlaces = Classes.cUtilities.CreateDataTable(Places);

            DataTable dtCampaignPlaces = Session["CampaignPlaces"] as DataTable;
            if (dtCharPlaces.Columns["ShowButton"] == null)
                dtCharPlaces.Columns.Add(new DataColumn("ShowButton", typeof(Boolean)));

            foreach (DataRow dRow in dtCharPlaces.Rows)
            {
                dRow["Showbutton"] = true;
                string t = dRow["PlaceID"].ToString();

                if (dRow["PlaceID"].ToString() != "0")
                {
                    DataView dvCampaignPlace = new DataView(dtCampaignPlaces, "CampaignPlaceID = " + dRow["PlaceID"].ToString(), "", DataViewRowState.CurrentRows);
                    if (dvCampaignPlace.Count > 0)
                    {
                        dRow["ShowButton"] = false;
                        dRow["Comments"] = dvCampaignPlace[0]["Locale"].ToString();
                    }
                }
            }

            gvPlaces.DataSource = dtCharPlaces;
            gvPlaces.DataBind();
        }

        protected void btnSaveExistingPlace_Click(object sender, EventArgs e)
        {
            List<Classes.cPlace> Places = Session["CharacterPlaces"] as List<Classes.cPlace>;

            if (tvSkills.SelectedNode != null)
            {
                int iPlaceID;
                int.TryParse(tvSkills.SelectedValue, out iPlaceID);

                DataSet CampaignPlaces = Session["CampaignPlaces"] as DataSet;
                var CharPlac = Places.Find(x => x.PlaceID == iPlaceID);
                if (CharPlac != null)
                {
                }
                else
                {
                    int NewRecCounter = Convert.ToInt32(hidPlaceID.Value);

                    Classes.cPlace NewPlace = new Classes.cPlace();
                    NewPlace.CampaignPlaceID = NewRecCounter;

                    NewPlace.PlaceID = iPlaceID;
                    NewPlace.PlaceName = tvSkills.SelectedNode.Text;
                    Places.Add(NewPlace);
                    Session["CharacterPlaces"] = Places;
                    BindPlaces(Places);
                    mvAddingItems.SetActiveView(vwNewItemButton);
                    ViewState["NewRecCounter"] = NewRecCounter;
                }
            }
        }

        protected void btnSaveNewPlace_Click(object sender, EventArgs e)
        {
            List<Classes.cPlace> Places = Session["CharacterPlaces"] as List<Classes.cPlace>;

            int iPlaceID;

            if (int.TryParse(hidPlaceID.Value, out iPlaceID))
            {
                var CharPlac = Places.Find(x => x.CampaignPlaceID == iPlaceID);
                if (CharPlac != null)
                {
                    CharPlac.PlaceName = tbPlaceName.Text;
                    CharPlac.Comments = tbPlayerComments.Text;
                    int iLocaleID;
                    if (int.TryParse(ddlLocalePlaces.SelectedValue, out iLocaleID))
                        CharPlac.LocaleID = iLocaleID;
                }
                else
                {
                    Classes.cPlace NewPlace = new Classes.cPlace();
                    NewPlace.CampaignPlaceID = iPlaceID;

                    NewPlace.PlaceID = 0;
                    NewPlace.PlaceName = tbPlaceName.Text;
                    NewPlace.Comments = tbPlayerComments.Text;
                    int iLocaleID;
                    if (int.TryParse(ddlLocalePlaces.SelectedValue, out iLocaleID))
                        NewPlace.LocaleID = iLocaleID;
                    Places.Add(NewPlace);
                }
            }
            Session["CharacterPlaces"] = Places;
            tbPlayerComments.Text = "";
            tbPlaceName.Text = "";
            ddlLocalePlaces.SelectedIndex = -1;

            BindPlaces(Places);

            mvAddingItems.SetActiveView(vwNewItemButton);
        }

        protected void btnCancelAdding_Click(object sender, EventArgs e)
        {
            mvAddingItems.SetActiveView(vwNewItemButton);
        }
    }
}