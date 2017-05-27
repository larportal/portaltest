using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharPlace : System.Web.UI.Page
    {
        protected DataTable _dtCampaignPlaces = new DataTable();
        private string _UserName = "";
        private int _UserID = 0;
        private bool _Reload = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            tvCampaignPlaces.Attributes.Add("onclick", "postBackByObject()");
            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["CharPlaceReadOnly"] != null)
                tdCampaignPlaces.Visible = false;
            else
                tdCampaignPlaces.Visible = true;

            if ((!IsPostBack) || (_Reload))
            {
                if (Session["CurrentCharacter"] == null)
                    Session["CurrentCharacter"] = -1;

                if (Session["PlaceCharacterID"] != null)
                {
                    string sCurrent = Session["CurrentCharacter"].ToString();
                    string sSelected = Session["PlaceCharacterID"].ToString();
                    if ((!IsPostBack) || (sCurrent != sSelected) || (_Reload))
                    {
                        int iCharID;
                        if (int.TryParse(Session["PlaceCharacterID"].ToString(), out iCharID))
                        {
                            tvCampaignPlaces.Nodes.Clear();

                            Classes.cCharacter cChar = new Classes.cCharacter();
                            cChar.LoadCharacter(iCharID);

                            Session["CurrentCharacter"] = iCharID;
                            Session["CharacterPlaces"] = cChar.Places;

                            DataSet dsCampaignPlaces = new DataSet();
                            SortedList sParam = new SortedList();
                            sParam.Add("@CampaignID", cChar.CampaignID);
                            dsCampaignPlaces = Classes.cUtilities.LoadDataSet("uspGetCampaignPlaces", sParam, "LARPortal", Session["LoginName"].ToString(), lsRoutineName + ".uspGetCampaignPlaces");

                            _dtCampaignPlaces = dsCampaignPlaces.Tables[0];
                            Session["CampaignPlaces"] = _dtCampaignPlaces;

                            ddlNonCampLocalePlaces.DataTextField = "PlaceName";
                            ddlNonCampLocalePlaces.DataValueField = "CampaignPlaceID";
                            ddlNonCampLocalePlaces.DataSource = dsCampaignPlaces.Tables[0];
                            ddlNonCampLocalePlaces.DataBind();

                            gvPlaces.DataSource = cChar.Places;
                            gvPlaces.DataBind();

                            TreeNode MainNode = new TreeNode("Skills");
                            DataView dvTopNodes = new DataView(_dtCampaignPlaces, "LocaleID = 0", "", DataViewRowState.CurrentRows);
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
                                    tvCampaignPlaces.Nodes.Add(NewNode);
                                }
                            }
                        }
                    }
                }
            }

            if (Session["CharPlaceReadOnly"] != null)
            {
                btnAddNonCampPlace.Visible = false;
                gvPlaces.Columns[gvPlaces.Columns.Count - 1].Visible = false;
                gvPlaces.Columns[gvPlaces.Columns.Count - 2].Visible = false;
            }
            else
            {
                btnAddNonCampPlace.Visible = true;
                gvPlaces.Columns[gvPlaces.Columns.Count - 1].Visible = true;
                gvPlaces.Columns[gvPlaces.Columns.Count - 2].Visible = true;
            }
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            DataView dvChild = new DataView(_dtCampaignPlaces, "LocaleID = " + parentId.ToString(), "PlaceName", DataViewRowState.CurrentRows);
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

        protected void gvPlaces_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int iCharacterID;
            if (int.TryParse(Session["PlaceCharacterID"].ToString(), out iCharacterID))
            {
                switch (e.CommandName.ToUpper())
                {
                    case "DELETEITEM":
                        {
                            int iCharacterPlaceID;
                            if (int.TryParse(e.CommandArgument.ToString(), out iCharacterPlaceID))
                            {
                                Classes.cCharacterPlace cPlaceToDelete = new Classes.cCharacterPlace();
                                cPlaceToDelete.CharacterPlaceID = iCharacterPlaceID;
                                cPlaceToDelete.RecordStatus = Classes.RecordStatuses.Delete;
                                cPlaceToDelete.Save(_UserID);
                                _Reload = true;
                            }
                            break;
                        }

                    case "EDITITEM":
                        {
                            int iCharacterPlaceID;
                            if (int.TryParse(e.CommandArgument.ToString(), out iCharacterPlaceID))
                            {
                                Classes.cCharacterPlace PlaceToEdit = new Classes.cCharacterPlace();
                                PlaceToEdit.CharacterPlaceID = iCharacterPlaceID;
                                PlaceToEdit.Load(_UserName);

                                if (!PlaceToEdit.CampaignPlaceID.HasValue)
                                {
                                    hidCharacterPlaceID.Value = iCharacterPlaceID.ToString();
                                    hidCampaignPlaceID.Value = "";
                                    tbNonCampPlaceName.Text = PlaceToEdit.PlaceName;
                                    tbNonCampPlayerComments.Text = PlaceToEdit.Comments;

                                    ddlNonCampLocalePlaces.ClearSelection();
                                    foreach (ListItem li in ddlNonCampLocalePlaces.Items)
                                    {
                                        if (li.Value == PlaceToEdit.LocaleID.ToString())
                                        {
                                            ddlNonCampLocalePlaces.ClearSelection();
                                            li.Selected = true;
                                        }
                                    }
                                    mvAddingItems.SetActiveView(vwNonCampaignPlace);
                                }
                                else
                                {
                                    hidCharacterPlaceID.Value = iCharacterPlaceID.ToString();
                                    hidCampaignPlaceID.Value = PlaceToEdit.CampaignPlaceID.ToString();
                                    lblCampaignPlaceName.Text = PlaceToEdit.PlaceName;
                                    lblCampaignLocale.Text = PlaceToEdit.LocaleName;
                                    tbCampaignPlayerComments.Text = PlaceToEdit.Comments;

                                    mvAddingItems.SetActiveView(vwCampaignPlace);
                                }
                            }
                            _Reload = true;
                            break;
                        }
                }
            }
        }
        #region NonCampRoutines
        protected void btnAddNonCampPlace_Click(object sender, EventArgs e)
        {
            hidCampaignPlaceID.Value = "";
            hidCharacterPlaceID.Value = "-1";
            mvAddingItems.SetActiveView(vwNonCampaignPlace);
        }

        protected void btnNonCampSave_Click(object sender, EventArgs e)
        {
            int iCharacterID;
            int.TryParse(Session["PlaceCharacterID"].ToString(), out iCharacterID);

            int iPlaceID;

            if (int.TryParse(hidCharacterPlaceID.Value, out iPlaceID))
            {
                if (iPlaceID != -1)
                {
                    Classes.cCharacterPlace PlaceToSave = new Classes.cCharacterPlace();
                    PlaceToSave.CharacterPlaceID = iPlaceID;
                    PlaceToSave.Load(_UserName);

                    PlaceToSave.CampaignPlaceID = null;
                    PlaceToSave.PlaceName = tbNonCampPlaceName.Text;
                    PlaceToSave.Comments = tbNonCampPlayerComments.Text;

                    int iLocaleID;
                    if (int.TryParse(ddlNonCampLocalePlaces.SelectedValue, out iLocaleID))
                    {
                        PlaceToSave.LocaleID = iLocaleID;
                        PlaceToSave.LocaleName = ddlNonCampLocalePlaces.SelectedItem.Text;
                    }

                    PlaceToSave.Save(_UserID);
                }
                else
                {
                    Classes.cCharacterPlace NewPlace = new Classes.cCharacterPlace();
                    NewPlace.CharacterPlaceID = iPlaceID;

                    NewPlace.CampaignPlaceID = null;
                    NewPlace.PlaceName = tbNonCampPlaceName.Text;
                    NewPlace.Comments = tbNonCampPlayerComments.Text;
                    int iLocaleID;
                    if (int.TryParse(ddlNonCampLocalePlaces.SelectedValue, out iLocaleID))
                    {
                        NewPlace.LocaleID = iLocaleID;
                    }
                    NewPlace.CharacterID = iCharacterID;
                    NewPlace.Save(_UserID);
                }
            }
            tbNonCampPlaceName.Text = "";
            tbNonCampPlayerComments.Text = "";
            ddlNonCampLocalePlaces.SelectedIndex = -1;
            hidCharacterPlaceID.Value = "";
            hidCampaignPlaceID.Value = "";
            hidCampaignLocaleID.Value = "";

            _Reload = true;
            mvAddingItems.SetActiveView(vwNewItemButton);
        }

        #endregion

        #region CampaignPlaceRoutines
        protected void tvCampaignPlaces_SelectedNodeChanged(object sender, EventArgs e)
        {
            int iCharacterID;
            int.TryParse(Session["PlaceCharacterID"].ToString(), out iCharacterID);

            if (Session["CharPlaceReadOnly"] != null)
                return;

            if ((tvCampaignPlaces.SelectedNode != null) &&
                (Session["CampaignPlaces"] != null))
            {
                DataTable dtCampaignPlaces = Session["CampaignPlaces"] as DataTable;

                hidCharacterPlaceID.Value = "-1";
                hidCampaignPlaceID.Value = "-1";

                int iSelectedPlace;
                int.TryParse(tvCampaignPlaces.SelectedNode.Value, out iSelectedPlace);

                DataView dvPlaces = new DataView(dtCampaignPlaces, "CampaignPlaceID = " + iSelectedPlace.ToString(), "", DataViewRowState.CurrentRows);
                foreach (DataRowView dRow in dvPlaces)
                {
                    hidCampaignPlaceID.Value = iSelectedPlace.ToString();
                    lblCampaignPlaceName.Text = dRow["PlaceName"].ToString();
                    lblCampaignLocale.Text = dRow["Locale"].ToString();
                    hidCampaignLocaleID.Value = dRow["LocaleID"].ToString();
                    tbCampaignPlayerComments.Text = "";
                }

                BindPlaces();
                mvAddingItems.SetActiveView(vwCampaignPlace);
            }
        }

        protected void btnCampaignSave_Click(object sender, EventArgs e)
        {
            int iCharacterID;
            int.TryParse(Session["PlaceCharacterID"].ToString(), out iCharacterID);

            int iCharacterPlaceID;
            if (int.TryParse(hidCharacterPlaceID.Value, out iCharacterPlaceID))
            {
                Classes.cCharacter cChar = new Classes.cCharacter();
                cChar.LoadCharacter(iCharacterID);

                var CharPlac = cChar.Places.Find(x => x.CharacterPlaceID == iCharacterPlaceID);
                if (CharPlac != null)
                {
                    CharPlac.PlaceName = lblCampaignPlaceName.Text;
                    CharPlac.Comments = tbCampaignPlayerComments.Text;
                    CharPlac.LocaleName = lblCampaignLocale.Text;
                    CharPlac.Save(_UserID);
                }
                else
                {
                    int iCampaignPlaceID;
                    Classes.cCharacterPlace cNewPlace = new Classes.cCharacterPlace();

                    if (int.TryParse(hidCampaignPlaceID.Value, out iCampaignPlaceID))
                        cNewPlace.CampaignPlaceID = iCampaignPlaceID;
                    else
                        cNewPlace.CampaignPlaceID = null;
                    cNewPlace.CharacterID = iCharacterID;

                    cNewPlace.PlaceName = lblCampaignPlaceName.Text;
                    cNewPlace.Comments = tbCampaignPlayerComments.Text;
                    int iLocaleID;

                    if (int.TryParse(hidCampaignLocaleID.Value, out iLocaleID))
                        cNewPlace.LocaleID = iLocaleID;

                    cNewPlace.Save(_UserID);
                }

                BindPlaces();
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


        public void BindPlaces()
        {
            int iCharacterID;
            if (int.TryParse(Session["PlaceCharacterID"].ToString(), out iCharacterID))
            {
                Classes.cCharacter cChar = new Classes.cCharacter();
                cChar.LoadCharacter(iCharacterID);
                gvPlaces.DataSource = cChar.Places;
                gvPlaces.DataBind();
            }
        }
    }
}