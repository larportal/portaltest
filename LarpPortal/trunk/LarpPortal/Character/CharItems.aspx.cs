using System;
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
//        public string PictureDirectory = "../Pictures";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["CurrentCharacter"] == null)
                ViewState["CurrentCharacter"] = "";
            if (Session["Items"] == null)
                Session["Items"] = "";
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Type comes from Chcharactertype - type
            // select * from mdbStatus for Status

            // 
            if (Session["SelectedCharacter"] != null)
            {
                string sCurrent = ViewState["CurrentCharacter"].ToString();
                string sSelected = Session["SelectedCharacter"].ToString();
                if ((!IsPostBack) || (ViewState["CurrentCharacter"].ToString() != Session["SelectedCharacter"].ToString()))
                {
                    int iCharID;
                    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                    {
                        Classes.cCharacter cChar = new Classes.cCharacter();
                        cChar.LoadCharacter(iCharID);

                        taCostume.InnerText = cChar.Costuming;
                        taWeapons.InnerText = cChar.Weapons;
                        taMakeup.InnerText = cChar.Makeup;
                        taAccessories.InnerText = cChar.Accessories;
                        taItemsOther.InnerText = cChar.Items;

                        DataTable dtPictures = new DataTable();
                        dtPictures = Classes.cUtilities.CreateDataTable(cChar.Pictures);     // CreateDataTable(cChar.Pictures);
                        Session["Items"] = cChar.Pictures;

                        string sFilter = "RecordStatus <> '" + ((int)Classes.RecordStatuses.Delete).ToString() + "' and " +
                            "PictureType = " + ((int)Classes.cPicture.PictureTypes.Item).ToString();
                        DataView dvPictures = new DataView(dtPictures, sFilter, "", DataViewRowState.CurrentRows);
                        dlItems.DataSource = dvPictures;
                        dlItems.DataBind();
                    }
                    ViewState["CurrentCharacter"] = Session["SelectedCharacter"];
                }
            }
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

                int iTemp;
                if (int.TryParse(ViewState["CurrentCharacter"].ToString(), out iTemp))
                    NewPicture.CharacterID = iTemp;

                NewPicture.Save(sUser);

                List<Classes.cPicture> Items = new List<Classes.cPicture>();
                Items = Session["Items"] as List<Classes.cPicture>;
                Items.Add(NewPicture);
                Session["Items"] = Items;

                DataTable dtPictures = Classes.cUtilities.CreateDataTable(Items);

                //if ( dtPictures.Columns["PictureURL"] == null )
                //    dtPictures.Columns.Add(new DataColumn("PictureURL", typeof(string)));

                //foreach ( 
                string sFilter = "RecordStatus <> '" + ((int)Classes.RecordStatuses.Delete).ToString() + "' and " +
                    "PictureType = " + ((int)Classes.cPicture.PictureTypes.Item).ToString();
                DataView dvPictures = new DataView(dtPictures, sFilter, "", DataViewRowState.CurrentRows);
                dlItems.DataSource = dvPictures;
                dlItems.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Classes.cCharacter cChar = new Classes.cCharacter();

            int iCharNum;

            if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharNum))
            {
                cChar.LoadCharacter(iCharNum);

                cChar.Costuming = taCostume.InnerText;
                cChar.Makeup = taMakeup.InnerText;
                cChar.Weapons = taWeapons.InnerText;
                cChar.Items = taItemsOther.InnerText;
                cChar.Accessories = taAccessories.InnerText;
                cChar.Pictures = Session["Items"] as List<Classes.cPicture>;

                cChar.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);
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
    }
}
