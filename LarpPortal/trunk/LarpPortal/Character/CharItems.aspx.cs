﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharItems : System.Web.UI.Page
    {
        private string _UserName = "";
        private int _UserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
            oCharSelect.CharacterChanged += oCharSelect_CharacterChanged;

            if (ViewState["CurrentCharacter"] == null)
                ViewState["CurrentCharacter"] = "";
            if (Session["Items"] == null)
                Session["Items"] = "";
            if (!IsPostBack)
            {
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (oCharSelect.CharacterID.HasValue)
            {
                tbCostume.Text = oCharSelect.CharacterInfo.Costuming;
                lblCostume.Text = oCharSelect.CharacterInfo.Costuming;
                tbWeapons.Text = oCharSelect.CharacterInfo.Weapons;
                lblWeapons.Text = oCharSelect.CharacterInfo.Weapons;
                tbMakeup.Text = oCharSelect.CharacterInfo.Makeup;
                lblMakeup.Text = oCharSelect.CharacterInfo.Makeup;
                tbAccessories.Text = oCharSelect.CharacterInfo.Accessories;
                lblAccessories.Text = oCharSelect.CharacterInfo.Accessories;
                tbOtherItems.Text = oCharSelect.CharacterInfo.Items;
                lblOtherItems.Text = oCharSelect.CharacterInfo.Items;

                if ((oCharSelect.CharacterInfo.CharacterType != 1) && (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters))
                {
                    btnSave.Enabled = false;
                    btnSave.CssClass = "btn-default";
                    btnSave.Style["background-color"] = "grey";
                    btnSaveTop.Enabled = false;
                    btnSaveTop.CssClass = "btn-default";
                    btnSaveTop.Style["background-color"] = "grey";

                    tbCostume.Visible = false;
                    lblCostume.Visible = true;
                    tbWeapons.Visible = false;
                    lblWeapons.Visible = true;
                    tbMakeup.Visible = false;
                    lblMakeup.Visible = true;
                    tbAccessories.Visible = false;
                    lblAccessories.Visible = true;
                    tbOtherItems.Visible = false;
                    lblOtherItems.Visible = true;
                    divAddPicture.Visible = false;
                }
                else
                {
                    btnSave.Enabled = true;
                    btnSave.Style["background-color"] = null;
                    btnSave.CssClass = "StandardButton";
                    btnSaveTop.Enabled = true;
                    btnSaveTop.Style["background-color"] = null;
                    btnSaveTop.CssClass = "StandardButton";

                    tbCostume.Visible = true;
                    lblCostume.Visible = false;
                    tbWeapons.Visible = true;
                    lblWeapons.Visible = false;
                    tbMakeup.Visible = true;
                    lblMakeup.Visible = false;
                    tbAccessories.Visible = true;
                    lblAccessories.Visible = false;
                    tbOtherItems.Visible = true;
                    lblOtherItems.Visible = false;
                    divAddPicture.Visible = true;
                }

                DataTable dtPictures = new DataTable();
                dtPictures = Classes.cUtilities.CreateDataTable(oCharSelect.CharacterInfo.Pictures);
                Session["Items"] = oCharSelect.CharacterInfo.Pictures;

                string sFilter = "RecordStatus <> '" + ((int)Classes.RecordStatuses.Delete).ToString() + "' and " +
                    "PictureType = " + ((int)Classes.cPicture.PictureTypes.Item).ToString();
                DataView dvPictures = new DataView(dtPictures, sFilter, "", DataViewRowState.CurrentRows);
                dlItems.DataSource = dvPictures;
                dlItems.DataBind();
            }
            ViewState["CurrentCharacter"] = oCharSelect.CharacterID.Value;
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuItem.HasFile)
            {
                string sUser = Session["LoginName"].ToString();
                Classes.cPicture NewPicture = new Classes.cPicture();
                NewPicture.CreateNewPictureRecord(sUser);
                NewPicture.PictureType = Classes.cPicture.PictureTypes.Item;
                string sExtension = Path.GetExtension(fuItem.FileName);
                string filename = "CI" + NewPicture.PictureID.ToString("D10") + sExtension;

                NewPicture.PictureFileName = filename;

                if (!Directory.Exists(Path.GetDirectoryName(NewPicture.PictureLocalName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(NewPicture.PictureLocalName));

                string FinalFileName = NewPicture.PictureLocalName; // Path.Combine(Server.MapPath(Path.GetDirectoryName(NewPicture.PictureLocalName)), filename);
                fuItem.SaveAs(FinalFileName);

                //int iTemp;
                //if (int.TryParse(ViewState["CurrentCharacter"].ToString(), out iTemp))
                //    NewPicture.CharacterID = iTemp;

                if (oCharSelect.CharacterID.HasValue)
                    NewPicture.CharacterID = oCharSelect.CharacterID.Value;

                NewPicture.Save(sUser);

                List<Classes.cPicture> Items = new List<Classes.cPicture>();
                Items = Session["Items"] as List<Classes.cPicture>;
                Items.Add(NewPicture);
                Session["Items"] = Items;

                DataTable dtPictures = Classes.cUtilities.CreateDataTable(Items);

                //if ( dtPictures.Columns["PictureURL"] == null )
                //    dtPictures.Columns.Add(new DataColumn("PictureURL", typeof(string)));

                string sFilter = "RecordStatus <> '" + ((int)Classes.RecordStatuses.Delete).ToString() + "' and " +
                    "PictureType = " + ((int)Classes.cPicture.PictureTypes.Item).ToString();
                DataView dvPictures = new DataView(dtPictures, sFilter, "", DataViewRowState.CurrentRows);
                dlItems.DataSource = dvPictures;
                dlItems.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();
            if (oCharSelect.CharacterID.HasValue)
            {
                oCharSelect.CharacterInfo.Costuming = tbCostume.Text;
                oCharSelect.CharacterInfo.Makeup = tbMakeup.Text;
                oCharSelect.CharacterInfo.Weapons = tbWeapons.Text;
                oCharSelect.CharacterInfo.Items = tbOtherItems.Text;
                oCharSelect.CharacterInfo.Accessories = tbAccessories.Text;
                oCharSelect.CharacterInfo.Pictures = Session["Items"] as List<Classes.cPicture>;
                oCharSelect.CharacterInfo.SaveCharacter(_UserName, _UserID);
                lblmodalMessage.Text = "Character " + oCharSelect.CharacterInfo.AKA + " has been saved.";
                btnCloseMessage.Attributes.Add("data-dismiss", "modal");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", "openMessage();", true);
            }
        }

        protected void dlItems_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            int PictureID;
            if (int.TryParse(e.CommandArgument.ToString(), out PictureID))
            {
                List<Classes.cPicture> Items = new List<Classes.cPicture>();
                Items = Session["Items"] as List<Classes.cPicture>;

                foreach (Classes.cPicture Pict in Items)
                {
                    if (Pict.PictureID == PictureID)
                        Pict.RecordStatus = Classes.RecordStatuses.Delete;
                }
                Session["Items"] = Items;
                DataTable dtPictures = Classes.cUtilities.CreateDataTable(Items);
                string sFilter = "RecordStatus <> '" + ((int)Classes.RecordStatuses.Delete).ToString() + "' and " +
                    "PictureType = " + ((int)Classes.cPicture.PictureTypes.Item).ToString();
                DataView dvPictures = new DataView(dtPictures, sFilter, "", DataViewRowState.CurrentRows);
                dlItems.DataSource = dvPictures;
                dlItems.DataBind();
            }
        }

        protected void oCharSelect_CharacterChanged(object sender, EventArgs e)
        {
            if (oCharSelect.CharacterInfo != null)
            {
                if (oCharSelect.CharacterID.HasValue)
                {
                    Classes.cUser UserInfo = new Classes.cUser(_UserName, "PasswordNotNeeded");
                    UserInfo.LastLoggedInCampaign = oCharSelect.CharacterInfo.CampaignID;
                    UserInfo.LastLoggedInCharacter = oCharSelect.CharacterID.Value;
                    UserInfo.LastLoggedInMyCharOrCamp = (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters ? "M" : "C");
                    UserInfo.Save();
                }
            }
        }
    }
}
